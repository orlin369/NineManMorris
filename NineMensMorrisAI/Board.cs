using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtifitialInteligence
{
    /// <summary>
    /// Definition of the board for the AI understanding.
    /// </summary>
    public class Board : IDisposable
    {
        #region Variables

        /// <summary>
        /// Maximum move count 
        /// </summary>
        public const int MAX_MOVES = 50;

        /// <summary>
        /// Number of generated boards.
        /// </summary>
        private static int OurBoardsGenerated;

        /// <summary>
        /// Number of deleted boards.
        /// </summary>
        private static int OurBoardsDeleted;

        /// <summary>
        /// The player who's turn it is on this board
        /// </summary>
	    private Player MyPlayerTurn;

        /// <summary>
        /// All the positions on the board.
        /// This is the machine representation of the board.
        /// </summary>
	    private Position[] MyPositions = new Position[24];

        /// <summary>
        /// The number of unplaced pieces.
        /// First element is WHITE, second is BLACK.
        /// </summary>
        private byte[] MyUnplaced = new byte[2];
        
        /// <summary>
        /// The number of placed pieces.
        /// First element is WHITE, second is BLACK.
        /// </summary>
        private byte[] MyPlaced = new byte[2];

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Board">By board</param>
        public Board(Board Board)
        {
            // first initialize as blank new board
            this.Initialize();
            // now do the deep copy stuff
            MyUnplaced[(int)Player.White] = Board.MyUnplaced[(int)Player.White];
            MyUnplaced[(int)Player.Black] = Board.MyUnplaced[(int)Player.Black];
            MyPlaced[(int)Player.White] = Board.MyPlaced[(int)Player.White];
            MyPlaced[(int)Player.Black] = Board.MyPlaced[(int)Player.Black];
            MyPlayerTurn = Board.MyPlayerTurn;
            for (int i = 0; i < 24; i++)
            {
                MyPositions[i].Player = Board.MyPositions[i].Player;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pPlayer">By player</param>
        public Board(Player pPlayer)
        {
            this.MyPlayerTurn = pPlayer;
	        this.Initialize();
        }

        /// <summary>
        /// Desrtructor
        /// </summary>
        ~Board()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose, call from destructor.
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < 24; i++)
            {
                MyPositions[i].Dispose();
            }
	        OurBoardsDeleted++;
        }

        #endregion

        /// <summary>
        /// Check if the move is posible.
        /// </summary>
        /// <param name="StartPosition">Currnet position</param>
        /// <param name="EndPos">Wanted position</param>
        /// <returns></returns>
        private bool IsAdjacent(BoardIndex StartPosition, BoardIndex EndPosition)
        {
            if (MyPositions[(int)StartPosition].Up != null)
            {
                if (MyPositions[(int)StartPosition].Up.GetLocation() == EndPosition)
                    return true;
            }
            if (MyPositions[(int)StartPosition].Down != null)
            {
                if (MyPositions[(int)StartPosition].Down.GetLocation() == EndPosition)
                    return true;
            }
            if (MyPositions[(int)StartPosition].Left != null)
            {
                if (MyPositions[(int)StartPosition].Left.GetLocation() == EndPosition)
                    return true;
            }
            if (MyPositions[(int)StartPosition].Right != null)
            {
                if (MyPositions[(int)StartPosition].Right.GetLocation() == EndPosition)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Is vertical mill possible.
        /// </summary>
        /// <param name="Position">For current position</param>
        /// <param name="Player">Current player</param>
        /// <returns>Is possible vertical mill</returns>
        private bool IsVerticalMill(BoardIndex Position, Player Player)
        {
	        if (MyPositions[(int)Position].Up == null)
            {  // both neighbors are below
		        return (MyPositions[(int)Position].Down.Player == MyPositions[(int)Position].Down.Down.Player) &&
                    (MyPositions[(int)Position].Down.Player == Player);
	        }
	        else if (MyPositions[(int)Position].Down == null)
            {  // both above
                return (MyPositions[(int)Position].Up.Player == MyPositions[(int)Position].Up.Up.Player) &&
			        (MyPositions[(int)Position].Up.Player == Player);
	        }
	        else
            {
                return (MyPositions[(int)Position].Up.Player == MyPositions[(int)Position].Down.Player) &&
                    (MyPositions[(int)Position].Up.Player == Player);
	        }
        }

        /// <summary>
        /// Is horizontal mill possible.
        /// </summary>
        /// <param name="Position">For current position</param>
        /// <param name="Player">Current player</param>
        /// <returns>Is possible horizontal mill</returns>
        private bool IsHorizontalMill(BoardIndex Position, Player Player)
        {
	        if (MyPositions[(int)Position].Left == null)
            { // both to right
		        return (MyPositions[(int)Position].Right.Player == MyPositions[(int)Position].Right.Right.Player) &&
			        (MyPositions[(int)Position].Right.Player == Player);
	        }
	        else if (MyPositions[(int)Position].Right == null)
            { // both to left
                return (MyPositions[(int)Position].Left.Player == MyPositions[(int)Position].Left.Left.Player) &&
			        (MyPositions[(int)Position].Left.Player == Player);
	        }
	        else
            {
                return (MyPositions[(int)Position].Left.Player == MyPositions[(int)Position].Right.Player) &&
			        (MyPositions[(int)Position].Left.Player == Player);
	        }
        }

        /// <summary>
        /// Is mill possible
        /// </summary>
        /// <param name="Position">For current position</param>
        /// <param name="Player">Current player</param>
        /// <returns>Is possible mill</returns>
        private bool IsMill(BoardIndex Position, Player Player)
        {
            // Active vertical
            if (IsVerticalMill(Position, Player))
            {
                return true;
            }
            
            // Active horizontal
            return IsHorizontalMill(Position, Player);
        }

        /// <summary>
        /// Add moves & capture moves.
        /// </summary>
        /// <param name="Moves">Moves to operate</param>
        /// <param name="MovesGenerated">Generated moves</param>
        /// <param name="StartPosition">Begin positon</param>
        /// <param name="EndPosition">End move position</param>
        private void AddMoveAndCaptureMoves(ref Move[] Moves, ref int MovesGenerated, BoardIndex StartPosition, BoardIndex EndPosition)
        {
	        // change the start position toPlayer.Neutral, we'll change it back
	        MyPositions[(int)StartPosition].SetPlayer(Player.Neutral);
	        if (this.IsMill(EndPosition, this.MyPlayerTurn))
            {
		        int captureMoves = 0;
		        Player capturePlayer = (this.MyPlayerTurn == Player.White) ? Player.Black : Player.White;
		        for (int j = 0; j < 24; j++)
                {
			        if (MyPositions[j].Player == capturePlayer && !IsMill((BoardIndex)j, MyPositions[j].Player) && MovesGenerated < MAX_MOVES)
                    {
				        captureMoves++;
				        Move m = new Move(MoveType.MoveAndCapture, StartPosition, EndPosition, (BoardIndex)j);
				        Moves[MovesGenerated++] = m;
			        }
		        }

                // exception rule.  if all the opponenet's pieces are in mills, we can pull one out
		        if (captureMoves == 0)
                {
			        for (int j = 0; j < 24; j++)
                    {
				        if (MyPositions[j].Player == capturePlayer && MovesGenerated < MAX_MOVES)
                        {
					        Move m = new Move(MoveType.MoveAndCapture, StartPosition, EndPosition, (BoardIndex)j);
					        Moves[MovesGenerated++] = m;
				        }
			        }
		        }
	        }
        	else if (MovesGenerated < MAX_MOVES)
            {
                Move m = new Move(MoveType.Move, StartPosition, EndPosition);
		        Moves[MovesGenerated++] = m;
	        }

            MyPositions[(int)StartPosition].SetPlayer(this.MyPlayerTurn);
        }

        /// <summary>
        /// Initialise the board
        /// </summary>
        private void Initialize()
        {
	        OurBoardsGenerated++;
	        this.MyUnplaced[(int)Player.White] = 9;
            this.MyUnplaced[(int)Player.Black] = 9;
            this.MyPlaced[(int)Player.White] = 0;
            this.MyPlaced[(int)Player.Black] = 0;

            for (int i = 0; i < 24; i++)
		        MyPositions[i] = new Position((BoardIndex)i);

	        // now set the up, down, left, and right pointers for each position on board
	        MyPositions[0].Right = MyPositions[1]; MyPositions[0].Down = MyPositions[9];
	        MyPositions[1].Left = MyPositions[0]; MyPositions[1].Down = MyPositions[4]; MyPositions[1].Right = MyPositions[2];
	        MyPositions[2].Left = MyPositions[1]; MyPositions[2].Down = MyPositions[14];
	        MyPositions[3].Right = MyPositions[4]; MyPositions[3].Down = MyPositions[10];
	        MyPositions[4].Left = MyPositions[3]; MyPositions[4].Right = MyPositions[5]; MyPositions[4].Up = MyPositions[1]; MyPositions[4].Down = MyPositions[7];
	        MyPositions[5].Left = MyPositions[4]; MyPositions[5].Down = MyPositions[13];
	        MyPositions[6].Right = MyPositions[7]; MyPositions[6].Down = MyPositions[11];
	        MyPositions[7].Left = MyPositions[6]; MyPositions[7].Right = MyPositions[8]; MyPositions[7].Up = MyPositions[4];
	        MyPositions[8].Left = MyPositions[7]; MyPositions[8].Down = MyPositions[12];
	        MyPositions[9].Right = MyPositions[10]; MyPositions[9].Up = MyPositions[0]; MyPositions[9].Down = MyPositions[21];
	        MyPositions[10].Left = MyPositions[9]; MyPositions[10].Right = MyPositions[11]; MyPositions[10].Up = MyPositions[3]; MyPositions[10].Down = MyPositions[18];
	        MyPositions[11].Left = MyPositions[10]; MyPositions[11].Up = MyPositions[6]; MyPositions[11].Down = MyPositions[15];
	        MyPositions[12].Right = MyPositions[13]; MyPositions[12].Up = MyPositions[8]; MyPositions[12].Down = MyPositions[17];
	        MyPositions[13].Left = MyPositions[12]; MyPositions[13].Right = MyPositions[14]; MyPositions[13].Up = MyPositions[5]; MyPositions[13].Down = MyPositions[20];
	        MyPositions[14].Left = MyPositions[13]; MyPositions[14].Up = MyPositions[2]; MyPositions[14].Down = MyPositions[23];
	        MyPositions[15].Right = MyPositions[16]; MyPositions[15].Up = MyPositions[11];
	        MyPositions[16].Left = MyPositions[15]; MyPositions[16].Right = MyPositions[17]; MyPositions[16].Down = MyPositions[19];
	        MyPositions[17].Left = MyPositions[16]; MyPositions[17].Up = MyPositions[12];
	        MyPositions[18].Right = MyPositions[19]; MyPositions[18].Up = MyPositions[10];
	        MyPositions[19].Left = MyPositions[18]; MyPositions[19].Right = MyPositions[20]; MyPositions[19].Up = MyPositions[16]; MyPositions[19].Down = MyPositions[22];
	        MyPositions[20].Left = MyPositions[19]; MyPositions[20].Up = MyPositions[13];
	        MyPositions[21].Right = MyPositions[22]; MyPositions[21].Up = MyPositions[9];
	        MyPositions[22].Left = MyPositions[21]; MyPositions[22].Right = MyPositions[23]; MyPositions[22].Up = MyPositions[19];
	        MyPositions[23].Left = MyPositions[22]; MyPositions[23].Up = MyPositions[14];
        }

        /// <summary>
        /// Is all peaces mill
        /// </summary>
        /// <param name="Player">For the player</param>
        /// <returns>Is all placed for this player</returns>
        private bool AllPiecesInMills(Player Player)
        {
            int piecesInMills = 0;
            for (int i = 0; i < 24; i++)
            {
                if (MyPositions[i].Player == Player && this.IsMill(MyPositions[i].GetLocation(), Player))
                    piecesInMills++;
            }

            if (piecesInMills == MyPlaced[(int)Player])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Move the figures
        /// </summary>
        /// <param name="Command">Command</param>
        /// <returns>State</returns>
        public GameState Move(MoveCommand Command)
        {
	        GameState state = GameState.IllegalMove;

	        // first we need to figure out if it's a placement, placement and capture, move, or move and capture
            if(Command.CommandType == MoveType.Drop)
            {
                if((this.GetStage() == GameState.One) && (this.MyUnplaced[(int)this.MyPlayerTurn] > 0))
                {
                    int index = (int)Command.StartPosition;

                    if(this.MyPositions[index].Player == Player.Neutral)
                    {
                        this.MyPositions[index].SetPlayer(this.MyPlayerTurn);
                        this.MyUnplaced[(int)this.MyPlayerTurn]--;
                        this.MyPlaced[(int)this.MyPlayerTurn]++;
                        this.MyPlayerTurn = this.MyPlayerTurn == Player.White ? Player.Black : Player.White;
                        state = this.GetStage();
                    }
                }
            }
	        // next case is either a simple move or a drop and capture
	        else if (Command.CommandType == MoveType.DropAndCapture)
            {
				BoardIndex dropPosition = Command.StartPosition;
				BoardIndex capturePosition = Command.CapturePosition;
                // verify that the drop forms a mill and the piece they're trying to capture isn't in a mill
                if(this.IsMill(dropPosition, this.MyPlayerTurn) && (!IsMill(capturePosition, MyPositions[(int)capturePosition].Player) || AllPiecesInMills(MyPositions[(int)capturePosition].Player)))
                {
                    // make the move
					this.MyPositions[(int)dropPosition].SetPlayer(MyPlayerTurn);
                    this.MyPositions[(int)capturePosition].SetPlayer(Player.Neutral);
					this.MyUnplaced[(int)this.MyPlayerTurn]--;
					this.MyPlaced[(int)this.MyPlayerTurn]++;
					this.MyPlayerTurn = this.MyPlayerTurn == Player.White ? Player.Black : Player.White;
					this.MyPlaced[(int)this.MyPlayerTurn]--;
					state = this.GetStage();
                }
            }
            else if(Command.CommandType == MoveType.Move)
            {  // it's a simple move
                BoardIndex startPosition = Command.StartPosition;
				BoardIndex endPosition = Command.CapturePosition;
				// the startPos must be owned by the player and the endPos must bePlayer.Neutral
				if (this.MyPositions[(int)startPosition].Player == this.MyPlayerTurn && this.MyPositions[(int)endPosition].Player == Player.Neutral)
                {
					// the move must be to an adjacent location or if not the player must have only 3 pieces
					if (this.IsAdjacent(startPosition, endPosition) || this.MyPlaced[(int)this.MyPlayerTurn] == 3)
                    {
						// the move is legal, do it and update board state
						this.MyPositions[(int)startPosition].SetPlayer(Player.Neutral);
						this.MyPositions[(int)endPosition].SetPlayer(this.MyPlayerTurn);
						this.MyPlayerTurn = this.MyPlayerTurn == Player.White ? Player.Black : Player.White;
						state = this.GetStage();
					}
				}
			}
	        // last case is a move and capture
            else if (Command.CommandType == MoveType.MoveAndCapture)
            {
                BoardIndex startPosition = Command.StartPosition;
                BoardIndex endPosition = Command.EndPosition;
                BoardIndex capturePosition = Command.CapturePosition;

                // the startPos must be owned by the player and the endPos must be Player.Neutral
                // the move must form a mill (endPos)
                // the capPos must also not be in a mill
                if (MyPositions[(int)startPosition].Player == this.MyPlayerTurn &&
                    MyPositions[(int)endPosition].Player == Player.Neutral &&
                    this.IsMill(endPosition, this.MyPlayerTurn) &&
                    (!this.IsMill(capturePosition, MyPositions[(int)capturePosition].Player) || this.AllPiecesInMills(this.MyPositions[(int)capturePosition].Player)))
                {
                    // the move must be to an adjacent location or if not the player must have only 3 pieces
                    if (this.IsAdjacent(startPosition, endPosition) || MyPlaced[(int)this.MyPlayerTurn] == 3)
                    {
                        // the move is legal, do it and update board state
                        MyPositions[(int)startPosition].SetPlayer(Player.Neutral);
                        MyPositions[(int)endPosition].SetPlayer(this.MyPlayerTurn);
                        MyPositions[(int)capturePosition].SetPlayer(Player.Neutral);
                        this.MyPlayerTurn = this.MyPlayerTurn == Player.White ? Player.Black : Player.White;
                        MyPlaced[(int)MyPlayerTurn]--;
                        state = this.GetStage();
                    }
                }
            }

            
	        if (this.HasWon(Player.Black))
            {
		        state = GameState.BlackWins;
            }
            else if (this.HasWon(Player.White))
            {
                state = GameState.WhiteWins;
            }
            return state;
        }

        /// <summary>
        /// Move by start and end position
        /// </summary>
        /// <param name="StartPosition">Start index</param>
        /// <param name="EndPosition">End index</param>
        public void Move(BoardIndex StartPosition, BoardIndex EndPosition)
        {
            MyPositions[(int)StartPosition].SetPlayer(Player.Neutral);
            MyPositions[(int)EndPosition].SetPlayer(this.MyPlayerTurn);
        }

        /// <summary>
        /// Calculate "Move" by the move.
        /// </summary>
        /// <param name="Move">Move</param>
        public void Move(Move Move)
        {
            if (Move.GetMoveType() == MoveType.Drop)
            {
                this.Drop(Move.GetEndPosition());
            }
            else if (Move.GetMoveType() == MoveType.DropAndCapture)
            {
                this.Drop(Move.GetEndPosition());
                this.Capture(Move.GetCapturePosition());
            }
            else if (Move.GetMoveType() == MoveType.Move)
            {
                this.Move(Move.GetStartPosition(), Move.GetEndPosition());
            }
            else
            {
                this.Move(Move.GetStartPosition(), Move.GetEndPosition());
                this.Capture(Move.GetCapturePosition());
            }

	        this.ChangeTurn();
        }

        /// <summary>
        /// If the player hase won.
        /// </summary>
        /// <param name="Player">Player that we want know is he won</param>
        /// <returns>true If won, else false</returns>
        public bool HasWon(Player Player)
        {
        	Player opponent = (Player == Player.White) ? Player.Black : Player.White;
            byte figureCount = 3;

	        // if the opponent has unplaced pieces, we're still in stage one and we haven't won
            if (MyUnplaced[(int)opponent] > 0)
            {
                return false;
            }
            // if the opponent has less than three pieces then we've won!
            if (MyPlaced[(int)opponent] + MyUnplaced[(int)opponent] < figureCount)
            {
                return true;
            }
            // if the opponent is blocked then we've won!
	        return this.Blocked(opponent);
        }

        /// <summary>
        ///  Returns true if all the pieces on the board for the player are blocked
        /// </summary>
        /// <param name="player">Player</param>
        /// <returns>True if is blocked.</returns>
        private bool Blocked(Player Player)
        {
	        // check every piece on the board
	        for (int i = 0; i < 24; i++)
            {
		        // if it's the player's piece check if it can move
		        if (this.MyPositions[i].Player == Player)
                {
			        // if it can move up, down, left, or right then the player isn't blocked
                    if (this.MyPositions[i].Up != null && this.MyPositions[i].Up.Player == Player.Neutral)
				        return false;
                    if (this.MyPositions[i].Down != null && this.MyPositions[i].Down.Player == Player.Neutral)
				        return false;
                    if (this.MyPositions[i].Left != null && this.MyPositions[i].Left.Player == Player.Neutral)
				        return false;
                    if (this.MyPositions[i].Right != null && this.MyPositions[i].Right.Player == Player.Neutral)
				        return false;
		        }
	        }
	        // we must be blocked
	        return true;
        }

        /// <summary>
        /// Drop the figure
        /// </summary>
        /// <param name="Position">Position to drop</param>
        private void Drop(BoardIndex Position)
        {
	        MyPositions[(int)Position].Player = this.MyPlayerTurn;
	        MyUnplaced[(int)this.MyPlayerTurn]--;
            MyPlaced[(int)this.MyPlayerTurn]++;
        }

        /// <summary>
        /// Capture position
        /// </summary>
        /// <param name="Position">Index that we capture.</param>
        private void Capture(BoardIndex Position)
        {
	        Player capturePlayer = MyPositions[(int)Position].Player;
            MyPositions[(int)Position].SetPlayer(Player.Neutral);
            MyPlaced[(int)capturePlayer]--;
        }

        /// <summary>
        /// Change the player turn
        /// </summary>
        private void ChangeTurn()
        {
	        MyPlayerTurn = (MyPlayerTurn == Player.White) ? Player.Black : Player.White;
        }

        /// <summary>
        /// Returns the GameState representing the stage of the game.
        /// </summary>
        /// <returns>Game stage</returns>
        public GameState GetStage()
        {
            if (MyUnplaced[(int)Player.White] > 0 || MyUnplaced[(int)Player.Black] > 0)
            {
                return GameState.One;
            }
            else if (MyPlaced[(int)Player.White] < 4 || MyPlaced[(int)Player.Black] < 4)
            {
                return GameState.Three;
            }
            else
            {
                return GameState.Two;
            }
        }

        /// <summary>
        /// Get the turn
        /// </summary>
        /// <returns>Current player turn</returns>
        private Player GetTurn()
        {
	        return MyPlayerTurn;
        }

        /// <summary>
        /// Get the moves from current game state
        /// </summary>
        /// <returns>The moves</returns>
        public Move[] GetMoves()
        {
            Move[] moves = new Move[MAX_MOVES];
            for (int index = 0; index < MAX_MOVES; index++)
            {
                moves[index] = null;
            }

            int movesGenerated = 0;

            // if we're in stage one enumerate all the possible drops
            if (MyUnplaced[(int)this.MyPlayerTurn] > 0)
            {
                for (int index = 0; index < 24; index++)
                {
                    if (MyPositions[index].Player == Player.Neutral)
                    {
                        if (this.IsMill((BoardIndex)index, this.MyPlayerTurn))
                        {
                            Player capturePlayer = (MyPlayerTurn == Player.White) ? Player.Black : Player.White;

                            // the captureMoves part added for the exception rule.  if all the other player's pieces are in mills
                            // we can remove one from them
                            int captureMoves = 0;
                            for (int j = 0; j < 24; j++)
                            {
                                if (MyPositions[j].Player == capturePlayer && !this.IsMill((BoardIndex)j, MyPositions[j].Player) && movesGenerated < MAX_MOVES)
                                {
                                    captureMoves++;
                                    Move m = new Move(MoveType.DropAndCapture, (BoardIndex)index, (BoardIndex)j);
                                    moves[movesGenerated++] = m;
                                }
                            }
                            // exception rule
                            if (captureMoves == 0)
                            {
                                for (int j = 0; j < 24; j++)
                                {
                                    if (MyPositions[j].Player == capturePlayer && movesGenerated < MAX_MOVES)
                                    {
                                        Move m = new Move(MoveType.DropAndCapture, (BoardIndex)index, (BoardIndex)j);
                                        moves[movesGenerated++] = m;
                                    }
                                }
                            }
                            // end exception
                        }
                        else if (movesGenerated < MAX_MOVES)
                        {
                            Move m = new Move(MoveType.Drop, (BoardIndex)index);
                            moves[movesGenerated++] = m;
                        }
                    }
                }
            }
            // if we're in stage two enumerate all the possible adjacent moves
            else if (MyPlaced[(int)MyPlayerTurn] > 3)
            {
                for (int index = 0; index < 24; index++)
                {
                    // first figure out which pieces we own
                    if (MyPositions[index].Player == MyPlayerTurn)
                    {
                        // now look at each adjacent empty spot
                        // for each one, if making the move forms a mill, enumerate the possible captures
                        if (MyPositions[index].Up != null && MyPositions[index].Up.Player == Player.Neutral)
                        {
                            this.AddMoveAndCaptureMoves(ref moves, ref movesGenerated, (BoardIndex)index, MyPositions[index].Up.GetLocation());
                        }
                        if (MyPositions[index].Down != null && MyPositions[index].Down.Player == Player.Neutral)
                        {
                            this.AddMoveAndCaptureMoves(ref moves, ref movesGenerated, (BoardIndex)index, MyPositions[index].Down.GetLocation());
                        }
                        if (MyPositions[index].Left != null && MyPositions[index].Left.Player == Player.Neutral)
                        {
                            this.AddMoveAndCaptureMoves(ref moves, ref movesGenerated, (BoardIndex)index, MyPositions[index].Left.GetLocation());
                        }
                        if (MyPositions[index].Right != null && MyPositions[index].Right.Player == Player.Neutral)
                        {
                            this.AddMoveAndCaptureMoves(ref moves, ref movesGenerated, (BoardIndex)index, MyPositions[index].Right.GetLocation());
                        }
                    }
                }
            }
            // if we're in stage three enumerate all possible move/drops
            else
            {
                for (int index = 0; index < 24; index++)
                {
                    // first we find which pieces we own
                    if (this.MyPositions[index].Player == MyPlayerTurn)
                    {
                        // now look at each empty spot
                        // for each one, if making the move forms a mill, enumberate the possible captures
                        for (int j = 0; j < 24; j++)
                        {
                            if (this.MyPositions[j].Player == Player.Neutral)
                            {
                                this.AddMoveAndCaptureMoves(ref moves, ref movesGenerated, (BoardIndex)index, (BoardIndex)j);
                            }
                        }
                    }
                }
            }

            // Sort the elemrnts
            List<Move> listMoves = new List<Move>(moves);
            listMoves.Sort(ArtifitialInteligence.Move.CompareMoves);

            // return like array
            return listMoves.ToArray();
        }

        /// <summary>
        /// Check if the boardis the same
        /// </summary>
        /// <param name="Board">Current board</param>
        /// <returns>true if the passed in board contains the same state as this one</returns>
        public bool IsSameBoardState(Board Board)
        {
            if (this.MyPlaced[(int)Player.White] != Board.MyPlaced[(int)Player.White])
            {
                return false;
            }
            else if (this.MyPlaced[(int)Player.Black] != Board.MyPlaced[(int)Player.Black])
            {
                return false;
            }
            else if (this.MyUnplaced[(int)Player.White] != Board.MyUnplaced[(int)Player.White])
            {
                return false;
            }
            else if (this.MyUnplaced[(int)Player.Black] != Board.MyUnplaced[(int)Player.Black])
            {
                return false;
            }

            for (int index = 0; index < 24; index++)
            {
                if (MyPositions[index].Player != Board.MyPositions[index].Player)
                {
                    return false;
		        }
	        }

	        return true;
        }

        /// <summary>
        /// Count of mills
        /// </summary>
        /// <param name="StartPlayer">Start Player</param>
        /// <param name="Player">Player</param>
        /// <returns>Mills count</returns>
        private int CountMills(Player StartPlayer, Player Player)
        {
	        int ret = 0;

	        bool[] locInH = new bool[24];
	        bool[] locInV = new bool[24];

            for (int index = 0; index < 24; index++)
            {
                if (this.MyPositions[index].Player == StartPlayer)
                {
                    if (!locInH[index] && this.IsHorizontalMill((BoardIndex)index, Player))
                    {
                        locInH[index] = true;
				        ret++;
                        if (this.MyPositions[index].Left == null)
                        {
                            locInH[(int)this.MyPositions[index].Right.GetLocation()] = true;
                            locInH[(int)this.MyPositions[index].Right.Right.GetLocation()] = true;
				        }
				        else if (this.MyPositions[index].Right == null)
                        {
                            locInH[(int)this.MyPositions[index].Left.GetLocation()] = true;
                            locInH[(int)this.MyPositions[index].Left.Left.GetLocation()] = true;
                        }
				        else
                        {
                            locInH[(int)MyPositions[index].Right.GetLocation()] = true;
                            locInH[(int)MyPositions[index].Left.GetLocation()] = true;
				        }
			        }

			        if (!locInV[index] && this.IsVerticalMill((BoardIndex)index, Player))
                    {
				        locInV[index] = true;
				        ret++;

                        if (this.MyPositions[index].Up == null)
                        {
					        locInV[(int)MyPositions[index].Down.GetLocation()] = true;
					        locInV[(int)MyPositions[index].Down.Down.GetLocation()] = true;
				        }
				        else if (this.MyPositions[index].Down == null)
                        {
					        locInV[(int)MyPositions[index].Up.GetLocation()] = true;
					        locInV[(int)MyPositions[index].Up.Up.GetLocation()] = true;
				        }
				        else
                        {
					        locInV[(int)MyPositions[index].Down.GetLocation()] = true;
					        locInV[(int)MyPositions[index].Up.GetLocation()] = true;
				        }
			        }
		        }
	        }

	        return ret;
        }

        /// <summary>
        /// The count of placed peaces
        /// </summary>
        /// <returns>Count</returns>
        private int PlacedPieces()
        {
            return this.MyPlaced[(int)Player.Black] + this.MyPlaced[(int)Player.White];
        }

        /// <summary>
        /// Get unplaced peaces
        /// </summary>
        /// <param name="Player">By player</param>
        /// <returns>Count figures</returns>
        private int GetUnplacedPieces(Player Player)
        {
	        return MyUnplaced[(int)Player];
        }

        /// <summary>
        /// Evaluate the move by the evaluation settings.
        /// </summary>
        /// <param name="Evals"></param>
        /// <returns></returns>
        public int Evaluate(EvalSettings Evals)
        {
	        if (this.GetStage() == GameState.One) 
		        return this.EvalOne(Evals);
            else if (this.GetStage() == GameState.Two)
		        return this.EvalTwo(Evals);
	        else
		        return this.EvalThree(Evals);
        }

        /// <summary>
        /// Evaluation test
        /// </summary>
        /// <param name="Evals">By evaluation settings</param>
        /// <returns>Stage depth</returns>
        private int EvalTest(EvalSettings Evals)
        {
	        if (this.GetStage() == GameState.One) 
		        return this.EvalOneTest(Evals);
	        else if (this.GetStage() == GameState.Two)
		        return this.EvalTwoTest(Evals);
	        else
		        return this.EvalThreeTest(Evals);
        }

        /// <summary>
        /// Evaluation test by evaluation settings.
        /// </summary>
        /// <param name="Evals">Evaluation settings</param>
        /// <returns>Stage depth</returns>
        private int EvalOne(EvalSettings Evals)
        {
	        Player opponent = (MyPlayerTurn == Player.White) ? Player.Black : Player.White;

            int ret = 0;
	        //// a board that gives me an opportunity to form a mill is good
	        //ret+= (Evals.MillFormable*this.CountMills(GameController::NEUTRAL, MyPlayerTurn));
	        //// give me points for each mill I have
	        //ret+= (Evals.mill_formed*this.CountMills(MyPlayerTurn, MyPlayerTurn));
	        // give me points for each mill I blocked
	        ret += (Evals.MillBlocked * this.CountMills(MyPlayerTurn, opponent));

	        // give me points for each piece I have on the board.  I get points for range of movement
	        // this is specific for stage one
	        for (int i = 0; i < 24; i++)
            {
		        if (MyPositions[i].Player == MyPlayerTurn)
                {
			        if (this.MyPositions[i].Up != null)
				    {
                        ret += Evals.AdjacentSpot;
			        }
                    if (this.MyPositions[i].Down != null)
				    {
                        ret += Evals.AdjacentSpot;
			        }
                    if (this.MyPositions[i].Left != null)
                    {
                        ret += Evals.AdjacentSpot;
                    }
                    if (this.MyPositions[i].Right != null)
				    {
                        ret += Evals.AdjacentSpot;
		            }
                }
	        }
	        // give me points for each piece I have captured
	        for (int i = 9; i > (MyPlaced[(int)opponent] + MyUnplaced[(int)opponent]); i--)
            {
		        ret += Evals.CapturedPiece;
	        }
            // take away points for each piece my opponent has captured
	        for (int i = 9; i > (MyPlaced[(int)this.MyPlayerTurn] + MyUnplaced[(int)this.MyPlayerTurn]); i--)
		    {
                ret += Evals.LostPiece;
            }
            // take away points for each mill my opponent has formed
	        ret += (Evals.MillOpponent * this.CountMills(opponent, opponent));
	        return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Evals">Evaluation settings.</param>
        /// <returns></returns>
        private int EvalOneTest(EvalSettings Evals)
        {
        	Player opponent = (MyPlayerTurn == Player.White) ? Player.Black : Player.White;
	
            int ret = 0;

	        // give me points for each piece I have on the board.  I get points for range of movement
	        // this is specific for stage one
	        for (int i = 0; i < 24; i++)
            {
		        if (MyPositions[i].Player == MyPlayerTurn)
                {
			        if (MyPositions[i].Up != null)
				    {
                        ret+= Evals.AdjacentSpot;
                    }
                    if (MyPositions[i].Down != null)
                    {
				        ret+= Evals.AdjacentSpot;
			        }
                    if (MyPositions[i].Left != null)
				    {
                        ret+= Evals.AdjacentSpot;
			        }
                    if (MyPositions[i].Right != null)
				    {
                        ret+= Evals.AdjacentSpot;
		            }
                }
	        }

	        // give me points for each piece I have captured
	        for (int i = 9; i > (MyPlaced[(int)opponent] + MyUnplaced[(int)opponent]); i--)
            {
		        ret += Evals.CapturedPiece;
            }
            
            // take away points for each piece my opponent has captured
	        for (int i = 9; i > (MyPlaced[(int)this.MyPlayerTurn] + MyUnplaced[(int)this.MyPlayerTurn]); i--)
		    {
                ret += Evals.LostPiece;
	        }
            
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Evals"></param>
        /// <returns></returns>
        private int EvalTwo(EvalSettings Evals)
        {
        	Player opponent = (MyPlayerTurn == Player.White) ? Player.Black : Player.White;

            int ret = 0;

        	// a board in which I lose is the worst!  we can stop right here.
	        if (this.HasWon(opponent))
            {
		        return Evals.WorstScore;
	        }
            
            // a board in which I win is the best!
	        if (this.HasWon(this.MyPlayerTurn))
            {
		        return Evals.BestScore;
	        }
            
            // give me points for each piece I have captured
	        for (int i = 9; i > (this.MyPlaced[(int)opponent] + MyUnplaced[(int)opponent]); i--)
            {
		        ret += Evals.CapturedPiece;
            }

	        // take away points for each piece my opponent has captured
	        for (int i = 9; i > (MyPlaced[(int)this.MyPlayerTurn] + MyUnplaced[(int)this.MyPlayerTurn]); i--)
            {
		        ret += Evals.LostPiece;
            }

	        // a board that gives me an opportunity to form a mill is good
	        ret += (Evals.MillFormable * this.CountMills(Player.Neutral, this.MyPlayerTurn));
	        
            // give me points for each mill I have
	        ret += (Evals.MillFormed * this.CountMills(this.MyPlayerTurn, this.MyPlayerTurn));
	        
            // take away points for each mill my opponent has formed
	        ret += (Evals.MillOpponent * this.CountMills(opponent, opponent));
	        
            // give me points for each spot of my opponent that is blocked
	        for (int i = 0; i < 24; i++)
            {
		        if (MyPositions[i].Player == opponent)
                {
			        bool blocked = true;
			        if (MyPositions[i].Up != null && MyPositions[i].Up.Player == Player.Neutral)
				        blocked = false;
			        else if (MyPositions[i].Down != null && MyPositions[i].Down.Player == Player.Neutral)
				        blocked = false;
			        else if (MyPositions[i].Left != null && MyPositions[i].Left.Player == Player.Neutral)
				        blocked = false;
			        else if (MyPositions[i].Right != null && MyPositions[i].Right.Player == Player.Neutral)
				        blocked = false;
			        if (blocked)
				        ret += Evals.BlockedOpponentSpot;
		        }
	        }

	        return ret;
        }

        private int EvalTwoTest(EvalSettings Evals)
        {
	        //GameController::Player opponent = (MyPlayerTurn == GameController::WHITE) ? GameController::BLACK : GameController::WHITE;
	        //int ret = 0;
	        //// a board in which I lose is the worst!  we can stop right here.
	        //if (hasWon(opponent))
	        //	return Evals.worst_score;
	        //// a board in which I win is great!
	        //if (hasWon(MyPlayerTurn))
	        //	return Evals.best_score;
	        //// give me points for each piece I have captured
	        //for (int i = 9; i > (MyPlaced[opponent] + MyUnplaced[opponent]); i--)
	        //	ret+= Evals.CapturedPiece;
	        //// take away points for each piece my opponent has captured
	        //for (int i = 9; i > (MyPlaced[MyPlayerTurn] + MyUnplaced[MyPlayerTurn]); i--)
	        //	ret+= Evals.LostPiece;
	        //return ret;
	        return this.EvalOne(Evals);
        }

        private int EvalThree(EvalSettings Evals)
        {
        	Player opponent = (MyPlayerTurn == Player.White) ? Player.Black : Player.White;

	        int ret = 0;

            // a board in which I lose is the worst!  we can stop right here.
	        if (this.HasWon(opponent))
            {
		        return Evals.WorstScore;
	        }

            // give me points for each piece I have captured
	        for (int i = 9; i > (this.MyPlaced[(int)opponent] + MyUnplaced[(int)opponent]); i--)
            {
		        ret+= Evals.CapturedPiece;
	        }

            // a board that gives me an opportunity to form a mill is good
	        ret+= (Evals.MillFormable*this.CountMills(Player.Neutral, MyPlayerTurn));
	        
            // give me points for each mill I blocked
	        ret+= (Evals.MillBlocked * this.CountMills(this.MyPlayerTurn, opponent));
	        
            return ret;
        }

        private int EvalThreeTest(EvalSettings Evals)
        {
	        //GameController::Player opponent = (MyPlayerTurn == GameController::WHITE) ? GameController::BLACK : GameController::WHITE;
	        //int ret = 0;
	        //// a board in which I lose is the worst!  we can stop right here.
	        //if (hasWon(opponent))
	        //	return Evals.worst_score;
	        //// a board in which I win is great!
	        //if (hasWon(MyPlayerTurn))
	        //	return Evals.best_score;
	        //// give me points for each piece I have captured
	        //for (int i = 9; i > (MyPlaced[opponent] + MyUnplaced[opponent]); i--)
	        //	ret+= Evals.CapturedPiece;
	        //// take away points for each piece my opponent has captured
	        //for (int i = 9; i > (MyPlaced[MyPlayerTurn] + MyUnplaced[MyPlayerTurn]); i--)
	        //	ret+= Evals.LostPiece;
	        //return ret;
	        return this.EvalOne(Evals);
        }


        /// <summary>
        /// The interface between outher world and AI.
        /// </summary>
        /// <param name="Figures">Array of figure positions.</param>
        /// <param name="ComputerIndex">Computer player index</param>
        /// <param name="HumanIndex">Human player index</param>
        public void FillTheBoard(int[] Figures, int ComputerIndex, int HumanIndex)
        {
            for (int index = 0; index < 24; index++)
            {
                if (Figures[index] == ComputerIndex)
                {
                    MyPositions[index].SetPlayer(Player.Black);
                    MyPlaced[(int)Player.Black]++;
                    MyUnplaced[(int)Player.Black]--;
                }
                else if (Figures[index] == HumanIndex)
                {
                    MyPositions[index].SetPlayer(Player.White);
                    MyPlaced[(int)Player.White]++;
                    MyUnplaced[(int)Player.White]--;
                }
                else
                {
                    MyPositions[index].SetPlayer(Player.Neutral);
                }
            }

            this.MyPlayerTurn = Player.Black;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Figures"></param>
        /// <param name="ComputerIndex"></param>
        /// <param name="HumanIndex"></param>
        /// <param name="ComputerUnplaced"></param>
        /// <param name="HumanUnplaced"></param>
        public void FillTheBoard(int[] Figures, int ComputerIndex, int HumanIndex, byte ComputerUnplaced, byte HumanUnplaced)
        {
            for (int index = 0; index < 24; index++)
            {
                if (Figures[index] == ComputerIndex)
                {
                    MyPositions[index].SetPlayer(Player.Black);
                    MyPlaced[(int)Player.Black]++;
                    MyUnplaced[(int)Player.Black]--;
                }
                else if (Figures[index] == HumanIndex)
                {
                    MyPositions[index].SetPlayer(Player.White);
                    MyPlaced[(int)Player.White]++;
                    MyUnplaced[(int)Player.White]--;
                }
                else
                {
                    MyPositions[index].SetPlayer(Player.Neutral);
                }
            }

            this.MyPlayerTurn = Player.Black;
            this.MyUnplaced[(int)Player.Black] -= ComputerUnplaced;
            this.MyUnplaced[(int)Player.White] -= HumanUnplaced;
        }
    }
}
