using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Loging
{
    /// <summary>
    /// Make small file records.
    /// </summary>
    public static class Log
    {
        #region Variables
        
        // Specify a name for your applicatin folder. 
        private static string AppFolderName = System.AppDomain.CurrentDomain.FriendlyName.Replace("vshost", "").Replace(".", "").Replace("exe", "");
        // List of messages that must be write to a file.
        private static List<String> Messages = new List<String>();
        // Maximum messages count in the message list.
        private static int MessageColectionSize = 1;
        // Enable loging.
        public static bool Enable = true;
        // Enable beep signal to every log record.
        public static bool EnableBeep = false;

        #endregion

        /// <summary>
        /// This method will create automaticly.
        /// Log file in folder with staic path.
        /// Every new day will be create a one new file.
        /// </summary>
        /// <param name="LogSource">Who send this message log.</param>
        /// <param name="MessageText">Concreet message.</param>
        public static void CreateRecord(string LogSource, string MessageText, LogMessageTypes MessageType, bool EndOfLogs = false)
        {
            // Write LOG record to the message buffer if is enabled.
            if (Enable)
            {
                // Structre of the message.
                // LogSource\tYear.Month.Day/Hour:Minute:Seconds.Miliseconds\tType\tMessageText
                string dateAndTime = DateTime.Now.ToString("yyyy.MM.dd/HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string message = LogSource + "\t" + dateAndTime + "\t" + MessageType.ToString() + "\t" + MessageText;
                
                // Add message to the message buffer.
                Messages.Add(message);
                
                // Write end of log line
                if(EndOfLogs)
                {
                    Messages.Add("\r\n===================================================================================\r\n");
                }

                // If filr are critical count, just write it to a file.
                if ((Messages.Count > MessageColectionSize) || EndOfLogs)
                {
                    Log.WtriteToLogFile();
                }

                /*
                if ((MessageType == LogMessageTypes.ERROR) && EnableBeep)
                {
                    Console.Beep(5000, 20);
                }
                */
            }
        }

        /// <summary>
        /// Write messages to LOG file.
        /// </summary>
        private static void WtriteToLogFile()
        {
            // Write buffer to the file if is enabled.
            if (Enable)
            {
                string subFolderName = "Logs";
                // Create Log file name.
                // Structure of file name.
                // Log_DateAndTime.txt
                string dateAndTime = DateTime.Now.ToString("yyyy.MM.dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string fileName = "Log_" + dateAndTime + ".txt";
                
                // The folder for the roaming current user. 
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Combine the base AppData folder with your specific folder (AppFolderName).
                string applicationLogFolder = Path.Combine(folder, AppFolderName);
                applicationLogFolder = Path.Combine(applicationLogFolder, subFolderName);


                // Check if folder exists and if not, create it.
                if (!Directory.Exists(applicationLogFolder))
                {
                    Directory.CreateDirectory(applicationLogFolder);
                }

                // Generate full path log file folder.
                string fullPath = Path.Combine(applicationLogFolder, fileName);

                // Check if file exists and if not, create it.
                if (!System.IO.File.Exists(fullPath))
                {
                    try
                    {
                        // File writer it use for writing a LOG file.
                        // Create the file.
                        System.IO.StreamWriter theFile = new System.IO.StreamWriter(fullPath);

                        // Write header.
                        string header = "This file is automatic generated.\r\nLOG SOURCE\tDATE & TIME        \tTYPE\tMESSAGE";
                        theFile.WriteLine(header);

                        for (int messageCount = 0; messageCount < Messages.Count; messageCount++)
                        {
                            // Write the string to a file.
                            theFile.WriteLine(Messages[messageCount]);
                        }
                        // Close the log file.
                        theFile.Close();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Exception message from logfile write: {0}", exception.Message);
                    }
                }
                else
                {
                    // Append data to file.
                    try
                    {
                        // File writer it use for writing a LOG file.
                        // Create the file.
                        System.IO.StreamWriter theFile = new System.IO.StreamWriter(fullPath, true);

                        for (int messageCount = 0; messageCount < Messages.Count; messageCount++)
                        {
                            // Write the string to a file.
                            theFile.WriteLine(Messages[messageCount]);
                        }

                        // Close the log file.
                        theFile.Close();

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Exception message from logfile write: {0}", exception.Message);
                    }
                }

                // Clear the message tail.
                Messages.Clear();
            }
        }
    }
}
