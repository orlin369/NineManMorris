using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Timer
using System.Diagnostics;

namespace ArtifitialInteligence
{
    public class GameController : IDisposable
    {
        #region Variables

        // Time 
        private Stopwatch CurrentTime;
        private int MyTimeLimit = 0;
        private bool MyHitTimeCutoff = false;

        private EvaluationBoardDelegate MyEvalDelegate = null;

        private Board MyLastBoard = null;

        private Board MyBoard = null;

        private EvalSettings MyEvalSettings = null;
        // the eval function to use when looking for a best move
        private EvaluationBoardDelegate MyEvalBoardDelegate = null;
	    // Diffculty level.
        private byte Depth = 3;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timeLimit">Tieme limit for work.</param>
        public GameController(int TimeLimit, byte Depth)
        {
            this.MyTimeLimit = TimeLimit;
            this.MyEvalSettings = new EvalSettings();
            this.Depth = Depth;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~GameController()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose, call frm destrucotr.
        /// </summary>
        public void Dispose()
        {
            this.MyBoard.Dispose();
            MyEvalSettings.Dispose();
            if (this.MyLastBoard != null)
            {
                this.MyLastBoard.Dispose();
            }
        }

        #endregion

        // the recursive bestmove function using alpha-beta!
        // NOTE: we need to keep track of if this is the first call to make sure that we don't evaluate moves
        //       that would stick us into an infinite move loop
        public GameNode BestMove(Board CurrentBoard, int Depth, int MyBest, int HisBest, bool FirstCall)
        {
            
	        if (Depth == 0)
            {
                return new GameNode(this.MyEvalDelegate(this.MyEvalSettings), null);
            }

            // Create time mesure unit.
            CurrentTime = new Stopwatch();
            // Parametrize
            CurrentTime.Stop();
            CurrentTime.Reset();
            CurrentTime.Start();

	        if (CurrentTime.ElapsedMilliseconds > this.MyTimeLimit)
            {
		        this.MyHitTimeCutoff = true;
                Console.WriteLine("Stuppit time limit .... FU.");
		        return null;
	        }

	        Move[] moveList = CurrentBoard.GetMoves();
	        int movesEvaluated = 0;
	        int bestScore = MyBest;
	        Move bestMove = null;

	        while (moveList[movesEvaluated] != null && movesEvaluated < Board.MAX_MOVES)
            {
		        Board evalBoard = new Board(CurrentBoard);
		        evalBoard.Move(moveList[movesEvaluated]);
                if (FirstCall && (this.MyLastBoard != null) && evalBoard.IsSameBoardState(this.MyLastBoard))
                {
                    // don't eval this move
		        }
		        else
                {
			        GameNode attempt = this.BestMove(evalBoard, Depth - 1, 0 - HisBest, 0 - bestScore, false);

			        if (attempt != null && (0 - attempt.Score) > bestScore)
                    {
				        bestScore = (0 - attempt.Score);
                        attempt.Dispose();

				        if (bestMove != null)
                        {
					        bestMove.Dispose();
				        }

                        bestMove = new Move(moveList[movesEvaluated]);
			        }
			        else if (attempt != null)
                    {
				        attempt.Dispose();
			        }
			        if (bestScore > HisBest)
                    {
				        evalBoard.Dispose();
				        break;
			        }
		        }
		        evalBoard.Dispose();
		        movesEvaluated++;
	        }

	        // free memory!
	        movesEvaluated = 0;
	        while ((moveList[movesEvaluated] != null) && (movesEvaluated < Board.MAX_MOVES)) {
		        moveList[movesEvaluated].Dispose();
		        movesEvaluated++;
	        }
	        moveList = null;
            
	        return new GameNode(bestScore, bestMove);
        }
        
        // this is the wrapper function for the recursive alpha-beta bestmove function.
        // this is an iterative deepening going 2 ply deeper if there's still time to search
        public GameNode BestMove(EvalSettings EvalSettings)
        {
            Console.WriteLine("BestMove Start");
	        GameNode best = null;
	        MyEvalSettings = EvalSettings;
            this.MyHitTimeCutoff = false;

	        for (int Depth = 2; Depth <= this.Depth; Depth++)
            {
                Console.WriteLine("BestMove Depth: " + Depth.ToString());
		        GameNode temp;
                temp = this.BestMove(this.MyBoard, Depth, EvalSettings.WorstScore, EvalSettings.BestScore, true);
		        if (temp != null && temp.Move != null)
                {
			        if (best != null)
				        best.Dispose();
                    best = temp;
		        }
		        else if (temp == null)
			        break;
	        }

            Console.WriteLine("BestMove End");
	        return best;
        }

        // function to perform a computer move on the board
        public Move ComputerMove(EvalSettings EvalSettings, EvaluationBoardDelegate EvalBoardDelegate)
        {
	        Move ret = null;
	        MyEvalBoardDelegate = EvalBoardDelegate;
	        Console.WriteLine("Determining computer's move...");
	        Move.OurMovesGenerated = 0;
	        
	        GameNode gameNode = BestMove(EvalSettings);
	        
            if (gameNode == null)
            {
                if (this.MyBoard.HasWon(Player.Black))
                {
			        Console.WriteLine("Black has won!");
		        }
                else if (this.MyBoard.HasWon(Player.White))
                {
			        Console.WriteLine("White has won!");
		        }
                // this next bit is because we didn't find a move.  I'm guessing it's because we're only a move or two away
		        // from losing.  So get the list of moves and just toss out the first one.
		        else
                {
			        Console.WriteLine("No best move found. You may be about to win!");
                    Move[] moveList = this.MyBoard.GetMoves();

			        if (moveList[0] != null)
                    {
                        this.MyBoard.Move(moveList[0]);
                        ret = moveList[0];
				        for (int i = 0; moveList[i] != null && i < Board.MAX_MOVES; i++)
					        moveList[i].Dispose();
			        }
			        else
                    {
				        ret = null;
			        }
                    // Delete
			        moveList = null;
		        }
	        }
	        else
            {
		        Console.WriteLine("Score: {0}", gameNode.Score);
                this.MyBoard.Move(gameNode.Move);
                ret = gameNode.Move;
	        }
	        return ret;
        }

        public Move PassBoard(int[] Positions, int White, int Black)
        {
            this.MyBoard = new Board(Player.Neutral);

            this.MyBoard.FillTheBoard(Positions, Black, White);
            this.MyEvalDelegate = this.MyBoard.Evaluate;

            return this.ComputerMove(this.MyEvalSettings, this.MyBoard.Evaluate);
        }

        //TODO: To make other method in the game controller.
    }
}
