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
    public class FileManipulation
    {
        public string GetSteamFilePath()
        {
            //Initialise Variables
            string filePath = "";
            object objRejKey = null;

            try
            {
                //Opens registry key for current user
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");

                //Get the registry key value
                objRejKey = regKey.GetValue("SteamPath");

                //Converts object to string
                filePath = objRejKey.ToString();

                //Replace incorrect slash
                filePath = filePath.Replace("/", "\\");

                //Close Registry Key
                regKey.Close();
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at GetSteamFilePath");
            }
            return filePath;
        }

        public string GetIconFilePath(string appID)
        {
            //Initialise Variables
            string filePath = "";
            string path = "";
            RegistryKey localKey = null;
            RegistryKey localSubKey = null;

            try
            {
                //Sets the path of the registry for the appID
                path = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App " + appID;

                //Opens the Local Machine (Bypass 64-bit requirement)
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

                //Gets sub-key at the path from Local Machine
                localSubKey = localKey.OpenSubKey(@path, false);

                //Returns value as a string
                filePath = (string)localSubKey.GetValue("DisplayIcon");

                //Close Local Machine key
                localKey.Close();

                //Close subkey
                localSubKey.Close();
            }
            catch
            {
                //Error here
            }
            return filePath;
        }

        public bool TestFilePath(string filePath)
        {
            //Initialise Variables
            bool successful = false;

            try
            {
                //Append '\steamapps' to directory
                filePath = Path.Combine(filePath, "steamapps");

                //Append '\libraryfolders.vdf' to directory
                filePath = Path.Combine(filePath, "libraryfolders.vdf");

                //Checks if the libraryfolders.vdf file exists
                if (File.Exists(filePath))
                {
                    //Sets bool to true
                    successful = true;
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at TestFilePath");
            }
            return successful;
        }

        public string GetSteamLibraryFoldersFile(string filePath)
        {
            try
            {
                //Append '\steamapps' to directory
                filePath = Path.Combine(filePath, "steamapps");

                //Append '\libraryfolders.vdf' to directory
                filePath = Path.Combine(filePath, "libraryfolders.vdf");
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at GetSteamLibraryFoldersFile");
            }
            return filePath;
        }

        public string[] GetSteamLibraries(string filePath)
        {
            //Initialise Variables
            string fileLine = "";
            string pattern = @"\\";
            string libraryNumberString = "";
            string[] arrayDirectories = null;
            int libraryNumber = 1;

            try
            {
                //Library Include
                Processing processing = new Processing();

                //Set up list to be converted to an array
                List<string> listDirectories = new List<string>();

                //Set up reader for the libraryfolders.vdf
                StreamReader file = new StreamReader(filePath);

                //Initialise regex pattern to catch '\'
                Regex rgx = new Regex(pattern);

                //Add default steam directory as the first library
                filePath = Path.GetDirectoryName(filePath);

                //Adds default steam directory to List
                listDirectories.Add(filePath);

                //Scan through lines of the file
                while ((fileLine = file.ReadLine()) != null)
                {
                    //Set up directory iterator for the file clean
                    libraryNumberString = "\"" + libraryNumber + "\"";

                    //If directory is found
                    if (fileLine.IndexOf(libraryNumberString) != -1)
                    {
                        //Increment directory count
                        libraryNumber++;

                        //Clean up the string to be used later
                        fileLine = processing.CleanDirectoryForList(fileLine, libraryNumberString, rgx);

                        //Adds line to list of directories
                        listDirectories.Add(fileLine);
                    }
                }

                //Close the reader
                file.Close();

                //Converts list into an array of strings
                arrayDirectories = listDirectories.ToArray();
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at GetSteamLibraries");
            }
            return arrayDirectories;
        }

        public string GetAppIDName(string steamGamePath)
        {
            //Initialise Variables
            string appName = "";
            string fileLine = "";

            try
            {
                //Library Include
                Processing processing = new Processing();

                //Set up reader for the appmanifest_*.acf
                StreamReader file = new StreamReader(steamGamePath);

                //Scan through lines of the file
                while ((fileLine = file.ReadLine()) != null)
                {
                    //If directory is found
                    if (fileLine.IndexOf("name") != -1)
                    {
                        //Cleans the app id for use
                        appName = processing.CleanAppIDName(fileLine, steamGamePath);
                    }
                }

                //Close the reader
                file.Close();
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at GetAppIDNames");
            }
            return appName;
        }

        public string CheckCreateConfig()
        {
            //Initialise variables
            string executionPath = "";

            try
            {
                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append '\config.rsg' to directory
                executionPath = Path.Combine(executionPath, "config.rsgc");

                //If config exists
                if (File.Exists(executionPath))
                {
                    //Delete old config file
                    File.Delete(executionPath);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CheckCreateConfig");
            }
            return executionPath;
        }

        public string CheckCreateTempFilters()
        {
            //Initialise variables
            string executionPath = "";

            try
            {
                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append '\tempFilters.rsg' to directory
                executionPath = Path.Combine(executionPath, "tempFilters.rsgf");

                //If temp filter exists
                if (File.Exists(executionPath))
                {
                    //Delete old temp filter file
                    File.Delete(executionPath);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CheckCreateTempFilters");
            }
            return executionPath;
        }

        public string CheckCreateTempOptions()
        {
            //Initialise variables
            string executionPath = "";

            try
            {
                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append '\tempOptions.rsgo' to directory
                executionPath = Path.Combine(executionPath, "tempOptions.rsgo");

                //If temp filter exists
                if (File.Exists(executionPath))
                {
                    //Delete old temp filter file
                    File.Delete(executionPath);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CheckCreateTempOptions");
            }
            return executionPath;
        }

        public string CheckCreateDirectory(string fileName)
        {
            //Initialise variables
            string executionPath = "";

            try
            {
                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append file name to directory
                executionPath = Path.Combine(executionPath, fileName);

                //If folder doesn't exist
                if (!Directory.Exists(executionPath))
                {
                    //Create folder
                    Directory.CreateDirectory(executionPath);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CheckCreateDirectory");
            }
            return executionPath;
        }

        public void LogToFile(string logLine, string fileName)
        {
            //Initialise variables
            string executionPath = "";
            string dateTime = "";

            try
            {
                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append '\LogFiles' to directory
                executionPath = Path.Combine(executionPath, "LogFiles");

                //Append Log File Name to directory
                executionPath = Path.Combine(executionPath, fileName);

                //Gets current date and time
                dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                //Sets up the line for logging
                logLine = dateTime + ": " + logLine;

                if(File.Exists(executionPath))
                {
                    //Wrap text writer so not to over-write itself
                    using (StreamWriter logWrite = new StreamWriter(executionPath, true))
                    {
                        //Write to the file
                        logWrite.WriteLine(logLine);

                        //Close file writer
                        logWrite.Close();
                    }
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at LogToFile");
            }
        }

        public string CreateLogFileName()
        {
            //Initialise variables
            string dateTime = "";
            string fileName = "";
            string executionPath = "";
            int sessionId = 0;
            int processId = 0;

            try
            {
                //Gets current date and time
                dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                //Replaces '/' with '-'
                dateTime = dateTime.Replace("/", "-");

                //Replaces ' ' with '_'
                dateTime = dateTime.Replace(" ", "_");

                //Replaces ':' with '-'
                dateTime = dateTime.Replace(":", "-");

                //Gets process ID
                processId = Process.GetCurrentProcess().Id;

                //Gets session ID
                sessionId = Process.GetCurrentProcess().SessionId;

                //Generates log file name
                fileName = "Log_" + dateTime + "_" + processId + "_" + sessionId + ".rsgl";

                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append '\LogFiles' to directory
                executionPath = Path.Combine(executionPath, "LogFiles");

                //If folder doesn't exist
                if (!Directory.Exists(executionPath))
                {
                    //Create folder
                    Directory.CreateDirectory(executionPath);
                }

                //Append file name to directory
                executionPath = Path.Combine(executionPath, fileName);

                //If log file is not yet generated
                if (!File.Exists(executionPath))
                {
                    //Create the file
                    var file = File.Create(executionPath);

                    //Close the file
                    file.Close();
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CreateLogFileName");
            }
            return fileName;
        }
    }
}

