using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Win32;

using RandomSteamGame;

namespace RandomSteamGame.RSGLibrary
{
    public class Processing
    {

        public string SetupFileDialog(string steamDirectoryText)
        {
            //Initialise Variables
            string initialPath = "";

            try
            {
                //Get current set directory
                initialPath = steamDirectoryText;

                //If no path is selected, default to 'C:\'
                if (initialPath == null || initialPath == "")
                {
                    initialPath = "C:\\";
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at SetupFileDialog");

                
            }
            return initialPath;
        }

        public bool CheckDirectory(string steamDirectoryText)
        {
            //Initialise Variables
            bool success = false;

            try
            {
                //If the steamDirectory field is empty (Read-Only)
                if (steamDirectoryText == null || steamDirectoryText == "")
                {
                    //Error message
                    MessageBox.Show("No directory added, please click browse and select a directory.");
                }
                else
                {
                    //Returns true so function can continue
                    success = true;
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CheckDirectory");
            }
            return success;
        }

        public string CleanDirectoryForList(string fileLine, string libraryNumberString, Regex rgx)
        {
            try
            {
                //Strip directory count from file line
                fileLine = fileLine.Replace(libraryNumberString, "");

                //Strip white space
                fileLine = Regex.Replace(fileLine, @"\s+", "");

                //Strip speech marks
                fileLine = fileLine.Replace("\"", "");

                //Append '\steamapps' to directory
                fileLine = Path.Combine(fileLine, "steamapps");

                //Removes additional '\'
                fileLine = rgx.Replace(fileLine, "", 1);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CleanDirectoryForList");
            }
            return fileLine;
        }

        public string CleanAppIDName(string fileLine, string steamGamePath)
        {
            //Initialise Variables
            string appName = "";

            try
            {
                //Strip speech marks
                fileLine = fileLine.Replace("\"", "");

                //Strip speech marks
                fileLine = fileLine.Replace("\t", "");

                //Strip speech marks
                fileLine = fileLine.Replace("name", "");

                //Sets the appmanifest_*.acf path
                appName = Path.GetFileName(steamGamePath);

                //Strips the 'appmanifest_'
                appName = appName.Replace("appmanifest_", "");

                //Strips the '.acf'
                appName = appName.Replace(".acf", "");

                //Adds a '|' for distinguishing later
                appName = appName + "|" + fileLine;
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CleanAppIDName");
            }
            return appName;
        }

        public string SelectRandomAppID(string[] steamAppIDs)
        {
            //Initialise Variables
            int randomNum = 0;
            int endElement = 0;
            string appID = "";

            try
            {
                //Initialise randomiser
                Random random = new Random();

                //Catch when random can't generate
                try
                {
                    //For each item in the array
                    if (steamAppIDs.Length > 1)
                    {
                        //Get the last element number of the array
                        endElement = steamAppIDs.Length;

                        //Gets a random element number
                        randomNum = random.Next(0, endElement);

                        //Gets the appID from that random element
                        appID = steamAppIDs[randomNum];
                    }
                    else if(steamAppIDs.Length == 1)
                    {
                        //Gets the appID from that random element
                        appID = steamAppIDs[0];
                    }
                    else
                    {
                        //Error here (no games found)
                    }
                    

                }
                catch
                {
                    //Error here
                }

            }
            catch
            {
                //Error message
                MessageBox.Show("Error at SelectRandomAppID");
            }
            return appID;
        }

        public void StartSteamApp(string appID, string launchOptionsString, string steamDirectory)
        {
            //Initialise Variables
            string steamLaunch = "";
            string[] appIDSplit = null;

            try
            {
                if (appID != "")
                {
                    //Splits app ID from game name
                    appIDSplit = appID.Split('|');

                    //Sets the appID correctly
                    appID = appIDSplit[0];

                    //Append '\Steam.exe' to directory
                    steamDirectory = Path.Combine(steamDirectory, "Steam.exe");

                    if(File.Exists(steamDirectory))
                    {
                        //Initialise start info
                        ProcessStartInfo startInfo = new ProcessStartInfo();

                        Console.WriteLine("steam://rungameid/" + appID + launchOptionsString);

                        //Set up steam uri
                        startInfo.FileName = @steamDirectory;
                        startInfo.Arguments = "-applaunch " + appID + " " + launchOptionsString;

                        Console.WriteLine(steamDirectory + " -applaunch " + appID + " " + launchOptionsString);

                        //Start process
                        Process.Start(startInfo); //Re-enable when ready
                    }
                }
                else
                {
                    //Error message
                    MessageBox.Show("No games were found, or all games were filtered. Please try again.");
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at StartSteamApp");
            }
        }

        public string[] GenerateSaveFileHeader(string executionPath, string fileType)
        {
            //Initialise Variables
            string[] configHeader = null;
            string dateTime = "";

            try
            {
                //Creates the config header list
                List<string> listConfigHeader = new List<string>();

                //Gets current date and time
                dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                //Content for config as a string
                listConfigHeader.Add("#");
                listConfigHeader.Add("# This " + fileType + " file for RSG (RandomSteamGame) was generated by User: " + Environment.UserName + ".");
                listConfigHeader.Add("# Only edit this file if you understand what you are doing or bad things may happen.");
                listConfigHeader.Add("# This config file was generated at \"" + executionPath + "\" on " + dateTime + ".");
                listConfigHeader.Add("#");

                //Converts list into an array of strings
                configHeader = listConfigHeader.ToArray();
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at GenerateSaveFileHeader");
            }
            return configHeader;
        }
    }
}
