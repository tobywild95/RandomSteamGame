using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Win32;

using RandomSteamGame.RSGLibrary;

namespace RandomSteamGame
{
    public partial class randomSteamGame : Form
    {
        //Library Includes
        FileManipulation fileManipulation = new FileManipulation();
        Processing processing = new Processing();

        //Global Variables
        string LOG_FILE_ID = "";

        public randomSteamGame()
        {
            //Initialise Variables
            bool autoRunChecked = false;

            try
            {
                //Standard function to initialise
                InitializeComponent();

                //Initialise form
                Initialise();

                //Set up the file name
                LOG_FILE_ID = fileManipulation.CreateLogFileName();

                //Checks if auto run is checked
                autoRunChecked = autoRun.Checked;

                //If auto run is selected
                if(autoRunChecked)
                {
                    //Launch a game
                    LaunchGame();
                }

                //Log line in file
                fileManipulation.LogToFile("Application created successfully", LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at randomSteamGame");

                //Log line in file
                fileManipulation.LogToFile("Error at randomSteamGame", LOG_FILE_ID);
            }    
        }

        private void browse1_Click(object sender, EventArgs e)
        {
            //Initialise Variables
            string filePath = "";
            string initialPath = "";
            string steamDirectoryText = "";

            try
            {
                //Set up the file browser
                OpenFileDialog fileDialog = new OpenFileDialog();

                //Get steam directory set
                steamDirectoryText = steamDirectory.Text;

                //Get initial directory for file browser
                initialPath = processing.SetupFileDialog(steamDirectoryText);

                //Set initial directory for file browser
                fileDialog.InitialDirectory = initialPath;

                //Sets filters for file browser
                fileDialog.Filter = "Steam (Steam.exe)|Steam.exe|All Files (*.*)|*.*";

                //Shows the file browser
                DialogResult filePathResult = fileDialog.ShowDialog();

                //If a result is found
                if (filePathResult == DialogResult.OK)
                {
                    //Gets the selected file path from the file browser
                    filePath = fileDialog.FileName;

                    //Strips the '\Steam.exe'
                    filePath = Path.GetDirectoryName(filePath);

                    //Sets the Read-Only field for use later
                    steamDirectory.Text = filePath;

                    //Log line in file
                    fileManipulation.LogToFile("Steam directory set using the 'Browse...' button: " + filePath, LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at browse1_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at browse1_Click", LOG_FILE_ID);
            }
        }

        private void openGame_Click(object sender, EventArgs e)
        {
            try
            {
                //Runs the launch game process
                LaunchGame();

                //Log line in file
                fileManipulation.LogToFile("'Open Game' was clicked", LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at openGame_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at openGame_Click", LOG_FILE_ID);
            }
        }

        private void steamDirectory_TextChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            string filePath = "";
            string[] arrayDirectories = null;
            string[] steamAppIDs = null;
            bool successful = false;

            try
            {
                //Get the found directory from field (Read-Only)
                filePath = steamDirectory.Text;

                //Log line in file
                fileManipulation.LogToFile("Directory set: " + filePath, LOG_FILE_ID);

                //Test registry file path for steam
                successful = fileManipulation.TestFilePath(filePath);

                //If directory was found
                if (successful)
                {
                    //Gets the file which contains the user's Steam library locations
                    filePath = fileManipulation.GetSteamLibraryFoldersFile(filePath);

                    //Gets the Steam library locations as an array of strings
                    arrayDirectories = fileManipulation.GetSteamLibraries(filePath);

                    //Gets an array of the Steam app IDs
                    steamAppIDs = GetSteamAppIDs(arrayDirectories);

                    //Populates the List Box
                    PopulateListBox(steamAppIDs);

                    //Log line in file
                    fileManipulation.LogToFile("Filter box populated with installed games", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at steamDirectory_TextChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at steamDirectory_TextChanged", LOG_FILE_ID);
            }
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            try
            {
                //Select all items in filter box
                SetSelectListBox(true);

                //Log line in file
                fileManipulation.LogToFile("All filter items selected", LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at selectAll_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at selectAll_Click", LOG_FILE_ID);
            }
        }

        private void deselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                //Deselect all items in filter box
                SetSelectListBox(false);

                //Log line in file
                fileManipulation.LogToFile("All filter items de-selected", LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at deselectAll_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at deselectAll_Click", LOG_FILE_ID);
            }
        }

        private void randomSteamGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //Writes all settings to a config file
                WriteToConfig();

                //Log line in file
                fileManipulation.LogToFile("Application closed", LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at randomSteamGame_FormClosing");

                //Log line in file
                fileManipulation.LogToFile("Error at randomSteamGame_FormClosing", LOG_FILE_ID);
            }
        }

        private void showLogo_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool showLogoChecked = false;

            try
            {
                //Gets status of showLogo
                showLogoChecked = showLogo.Checked;

                //If it has changed to unchecked
                if (showLogoChecked == false)
                {
                    //Unsets the image
                    gameLoadImage.Image = new Bitmap(Properties.Resources.RSGLogoTransparent, 32, 32);

                    //Unsets the text
                    gameLoaded.Text = "Welcome to Random Steam Game";

                    //Log line in file
                    fileManipulation.LogToFile("'Show Logo' was un-checked", LOG_FILE_ID);
                }
                else if(showLogoChecked == true)
                {
                    //Log line in file
                    fileManipulation.LogToFile("'Show Logo' was checked", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at showLogo_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at showLogo_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void saveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                //Recreates a config file
                WriteToConfig();

                //Log line in file
                fileManipulation.LogToFile("Current settings were saved", LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at saveSettings_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at saveSettings_Click", LOG_FILE_ID);
            }
        }

        private void resetSettings_Click(object sender, EventArgs e)
        {
            try
            {
                //Deselect all items in filter box
                SetSelectListBox(false);

                //Recreates a config file
                ReadFromConfig();

                //Log line in file
                fileManipulation.LogToFile("Current settings were reset", LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at resetSettings_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at resetSettings_Click", LOG_FILE_ID);
            }
        }

        private void applyFilters_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Sets or unsets the filter box
                HandleFilterBox();
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at applyFilters_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at applyFilters_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void browse2_Click(object sender, EventArgs e)
        {
            //Initialise Variables
            string filterDirectoryText = "";
            string filePath = "";

            try
            {
                //Sets up Folder Browser
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();

                //Get steam directory set
                filterDirectoryText = filterLocation.Text;

                //Sets initial directory
                folderDialog.SelectedPath = filterDirectoryText; 

                //Shows the file browser
                DialogResult filePathResult = folderDialog.ShowDialog();

                //If a result is found
                if (filePathResult == DialogResult.OK)
                {
                    //Gets the selected file path from the file browser
                    filePath = folderDialog.SelectedPath;

                    //Sets the Read-Only field for use later
                    filterLocation.Text = filePath;

                    //Log line in file
                    fileManipulation.LogToFile("Saved Filters path was selected using the 'Browse...' button: " + filePath, LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at browse2_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at browse2_Click", LOG_FILE_ID);
            }
        }

        private void saveFilter_Click(object sender, EventArgs e)
        {
            //Initialise Variables
            string nameInput = "";
            string setDirectory = "";

            try
            {
                //Gets the name inputted by the user
                nameInput = saveFileName.Text;

                //If a name was inputted
                if(nameInput != "" && nameInput != null)
                {
                    //Appends file extension
                    nameInput += ".rsgf";

                    //Gets the filter path set
                    setDirectory = filterLocation.Text;

                    //Sets up the file directory
                    setDirectory = Path.Combine(setDirectory, nameInput);

                    //If file doesn't exist
                    if (!File.Exists(setDirectory))
                    {
                        //Create the file
                        CreateFilterFile(setDirectory);

                        //Log line in file
                        fileManipulation.LogToFile("Filter file was created: " + nameInput, LOG_FILE_ID);
                    }
                    else
                    {
                        //If it should be overwritten
                        if (overwriteFilter.Checked)
                        {
                            //Delete file
                            File.Delete(setDirectory);

                            //Create the file
                            CreateFilterFile(setDirectory);

                            //Log line in file
                            fileManipulation.LogToFile("Filter file was overwritten: " + nameInput, LOG_FILE_ID);
                        }
                        else
                        {
                            //Error message
                            MessageBox.Show("File already exists, please use a different name or check the 'Overwrite Filter' box.");

                            //Log line in file
                            fileManipulation.LogToFile("File exists when attempting to save: " + nameInput, LOG_FILE_ID);
                        }
                    }
                }
                else
                {
                    //Error message
                    MessageBox.Show("Please enter a file name to save.");

                    //Log line in file
                    fileManipulation.LogToFile("Please enter a file name to save", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at saveFilter_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at saveFilter_Click", LOG_FILE_ID);
            }
        }

        private void loadFilter_Click(object sender, EventArgs e)
        {
            //Initialise Variables
            string filterDirectory = "";
            string filePath = "";
            string fileLine = "";

            try
            {
                //Set up the file browser
                OpenFileDialog fileDialog = new OpenFileDialog();

                //Get steam directory set
                filterDirectory = filterLocation.Text;

                //Set initial directory for file browser
                fileDialog.InitialDirectory = filterDirectory;

                //Sets filters for file browser
                fileDialog.Filter = "RSG Filter Files (*.rsgf)|*.rsgf|All Files (*.*)|*.*";

                //Shows the file browser
                DialogResult filePathResult = fileDialog.ShowDialog();

                //If a result is found
                if (filePathResult == DialogResult.OK)
                {
                    //Gets the selected file path from the file browser
                    filePath = fileDialog.FileName;

                    //Set up reader for the RSGF file
                    StreamReader file = new StreamReader(filePath);

                    //Deselect all items in filter box
                    SetSelectListBox(false);

                    //Scan through lines of the file
                    while ((fileLine = file.ReadLine()) != null)
                    {
                        //Sets the filter box for each found string
                        filterBox.SelectedItem = fileLine;
                    }

                    //Log line in file
                    fileManipulation.LogToFile("Filter file was loaded: " + filePath, LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at loadFilter_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at loadFilter_Click", LOG_FILE_ID);
            }
        }

        private void customLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool customLaunchCheck = false;

            try
            {
                //Gets the status of Custom Launch
                customLaunchCheck = customLaunch.Checked;

                //If it is checked
                if (!customLaunchCheck)
                {
                    //Enables the custom options box
                    customLaunchOptions.ReadOnly = true;

                    //Removes custom launch params
                    customLaunchOptions.Text = "";

                    //Log line in file
                    fileManipulation.LogToFile("Custom launch options box enabled", LOG_FILE_ID);
                }
                else
                {
                    //Disables the custom options box
                    customLaunchOptions.ReadOnly = false;

                    //Log line in file
                    fileManipulation.LogToFile("Custom launch options box disabled", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at customLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at customLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void enableOptions_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool enableLaunchCheck = false;

            try
            {
                //Gets the status of Custom Launch
                enableLaunchCheck = enableOptions.Checked;

                //If it is checked
                if (enableLaunchCheck)
                {
                    //Enables the custom options boxes
                    ToggleOptionFields(true);
                }
                else
                {
                    //Uncheck fields
                    UncheckOptionFields(false, false);

                    //Enables the custom options boxes
                    ToggleOptionFields(false);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at enableOptions_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at enableOptions_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void windowedLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool windowLaunchCheck = false;

            try
            {
                //Gets the status of launch option
                windowLaunchCheck = windowedLaunch.Checked;

                //If it is checked
                if (windowLaunchCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-windowed ";

                    //Log line in file
                    fileManipulation.LogToFile("-windowed was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-windowed ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-windowed was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at windowedLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at windowedLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void fullscreenLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool fullscreenLaunchCheck = false;

            try
            {
                //Gets the status of launch option
                fullscreenLaunchCheck = fullscreenLaunch.Checked;

                //If it is checked
                if (fullscreenLaunchCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-fullscreen ";

                    //Log line in file
                    fileManipulation.LogToFile("-fullscreen was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-fullscreen ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-fullscreen was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at fullscreenLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at fullscreenLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void consoleLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool consoleLaunchCheck = false;

            try
            {
                //Gets the status of launch option
                consoleLaunchCheck = consoleLaunch.Checked;

                //If it is checked
                if (consoleLaunchCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-console ";

                    //Log line in file
                    fileManipulation.LogToFile("-console was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-console ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-console was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at consoleLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at consoleLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void autoConfigLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool autoConfigCheck = false;

            try
            {
                //Gets the status of launch option
                autoConfigCheck = autoConfigLaunch.Checked;

                //If it is checked
                if (autoConfigCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-autoconfig ";

                    //Log line in file
                    fileManipulation.LogToFile("-autoconfig was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-autoconfig ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-autoconfig was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at autoConfigLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at autoConfigLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void antiAddictionLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool antiAddictionCheck = false;

            try
            {
                //Gets the status of launch option
                antiAddictionCheck = antiAddictionLaunch.Checked;

                //If it is checked
                if (antiAddictionCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-antiaddiction_test ";

                    //Log line in file
                    fileManipulation.LogToFile("-antiaddiction_test was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-antiaddiction_test ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-antiaddiction_test was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at antiAddictionLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at antiAddictionLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void developerLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool developerCheck = false;

            try
            {
                //Gets the status of launch option
                developerCheck = developerLaunch.Checked;

                //If it is checked
                if (developerCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-dev ";

                    //Log line in file
                    fileManipulation.LogToFile("-dev was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-dev ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-dev was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at developerLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at developerLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void noVideoLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool noVideoCheck = false;

            try
            {
                //Gets the status of launch option
                noVideoCheck = noVideoLaunch.Checked;

                //If it is checked
                if (noVideoCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-novideo ";

                    //Log line in file
                    fileManipulation.LogToFile("-novideo was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-novideo ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-novideo was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at noVideoLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at noVideoLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void highPriorityLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool highPriorityCheck = false;

            try
            {
                //Gets the status of launch option
                highPriorityCheck = highPriorityLaunch.Checked;

                //If it is checked
                if (highPriorityCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-high ";

                    //Log line in file
                    fileManipulation.LogToFile("-high was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-high ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-high was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at highPriorityLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at highPriorityLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void nod9xLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool nod9xCheck = false;

            try
            {
                //Gets the status of launch option
                nod9xCheck = nod9xLaunch.Checked;

                //If it is checked
                if (nod9xCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-nod3d9ex ";

                    //Log line in file
                    fileManipulation.LogToFile("-nod3d9ex was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-nod3d9ex ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-nod3d9ex was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at nod9xLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at nod9xLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void force32BitLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool force32BitCheck = false;

            try
            {
                //Gets the status of launch option
                force32BitCheck = force32BitLaunch.Checked;

                //If it is checked
                if (force32BitCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-32bit ";

                    //Log line in file
                    fileManipulation.LogToFile("-32bit was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-32bit ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-32bit was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at force32BitLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at force32BitLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void noMicLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool noMicCheck = false;

            try
            {
                //Gets the status of launch option
                noMicCheck = noMicLaunch.Checked;

                //If it is checked
                if (noMicCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-nomicsettings ";

                    //Log line in file
                    fileManipulation.LogToFile("-nomicsettings was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-nomicsettings ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-nomicsettings was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at noMicLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at noMicLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void noSoundLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool noSoundCheck = false;

            try
            {
                //Gets the status of launch option
                noSoundCheck = noSoundLaunch.Checked;

                //If it is checked
                if (noSoundCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-nosound ";

                    //Log line in file
                    fileManipulation.LogToFile("-nosound was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-nosound ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-nosound was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at noSoundLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at noSoundLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void dx9Launch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool dx9Check = false;

            try
            {
                //Gets the status of launch option
                dx9Check = dx9Launch.Checked;

                //If it is checked
                if (dx9Check)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-dx9 ";

                    //Log line in file
                    fileManipulation.LogToFile("-dx9 was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-dx9 ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-dx9 was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at dx9Launch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at dx9Launch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void dx11Launch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool dx11Check = false;

            try
            {
                //Gets the status of launch option
                dx11Check = dx11Launch.Checked;

                //If it is checked
                if (dx11Check)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-dx11 ";

                    //Log line in file
                    fileManipulation.LogToFile("-dx11 was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-dx11 ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-dx11 was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at dx11Launch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at dx11Launch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void glLaunch_CheckedChanged(object sender, EventArgs e)
        {
            //Initialise Variables
            bool glCheck = false;

            try
            {
                //Gets the status of launch option
                glCheck = glLaunch.Checked;

                //If it is checked
                if (glCheck)
                {
                    //Adds option into the box
                    launchOptions.Text = launchOptions.Text + "-gl ";

                    //Log line in file
                    fileManipulation.LogToFile("-gl was added to Steam launch options", LOG_FILE_ID);
                }
                else
                {
                    //Removes option from the box
                    launchOptions.Text = launchOptions.Text.Replace("-gl ", "");

                    //Log line in file
                    fileManipulation.LogToFile("-gl was removed from Steam launch options", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at glLaunch_CheckedChanged");

                //Log line in file
                fileManipulation.LogToFile("Error at glLaunch_CheckedChanged", LOG_FILE_ID);
            }
        }

        private void browse3_Click(object sender, EventArgs e)
        {
            //Initialise Variables
            string optionsDirectoryText = "";
            string filePath = "";

            try
            {
                //Sets up Folder Browser
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();

                //Get steam directory set
                optionsDirectoryText = optionsPath.Text;

                //Sets initial directory
                folderDialog.SelectedPath = optionsDirectoryText;

                //Shows the file browser
                DialogResult filePathResult = folderDialog.ShowDialog();

                //If a result is found
                if (filePathResult == DialogResult.OK)
                {
                    //Gets the selected file path from the file browser
                    filePath = folderDialog.SelectedPath;

                    //Sets the Read-Only field for use later
                    optionsPath.Text = filePath;

                    //Log line in file
                    fileManipulation.LogToFile("Saved Options path was selected using the 'Browse...' button: " + filePath, LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at browse3_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at browse3_Click", LOG_FILE_ID);
            }
        }

        private void saveOptions_Click(object sender, EventArgs e)
        {
            //Initialise Variables
            string nameInput = "";
            string setDirectory = "";

            try
            {
                //Gets the name inputted by the user
                nameInput = optionsName.Text;

                //If a name was inputted
                if (nameInput != "" && nameInput != null)
                {
                    //Appends file extension
                    nameInput += ".rsgo";

                    //Gets the options path set
                    setDirectory = optionsPath.Text;

                    //Sets up the file directory
                    setDirectory = Path.Combine(setDirectory, nameInput);

                    //If file doesn't exist
                    if (!File.Exists(setDirectory))
                    {
                        //Create the file
                        CreateOptionsFile(setDirectory);

                        //Log line in file
                        fileManipulation.LogToFile("Options file was created: " + nameInput, LOG_FILE_ID);
                    }
                    else
                    {
                        //If it should be overwritten
                        if (overwriteOptions.Checked)
                        {
                            //Delete file
                            File.Delete(setDirectory);

                            //Create the file
                            CreateOptionsFile(setDirectory);

                            //Log line in file
                            fileManipulation.LogToFile("Options file was overwritten: " + nameInput, LOG_FILE_ID);
                        }
                        else
                        {
                            //Error message
                            MessageBox.Show("File already exists, please use a different name or check the 'Overwrite Options' box.");

                            //Log line in file
                            fileManipulation.LogToFile("File exists when attempting to save: " + nameInput, LOG_FILE_ID);
                        }
                    }
                }
                else
                {
                    //Error message
                    MessageBox.Show("Please enter a file name to save.");

                    //Log line in file
                    fileManipulation.LogToFile("Please enter a file name to save", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at saveOptions_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at saveOptions_Click", LOG_FILE_ID);
            }
        }

        private void loadOptions_Click(object sender, EventArgs e)
        {
            //Initialise Variables
            string optionsDirectory = "";
            string filePath = "";
            string fileLine = "";
            string customLaunchLine = "";
            bool customCheck = false;

            try
            {
                //Set up the file browser
                OpenFileDialog fileDialog = new OpenFileDialog();

                //Get options directory set
                optionsDirectory = optionsPath.Text;

                //Set initial directory for file browser
                fileDialog.InitialDirectory = optionsDirectory;

                //Sets filters for file browser
                fileDialog.Filter = "RSG Options Files (*.rsgo)|*.rsgo|All Files (*.*)|*.*";

                //Shows the file browser
                DialogResult filePathResult = fileDialog.ShowDialog();

                //If a result is found
                if (filePathResult == DialogResult.OK)
                {
                    //Gets the selected file path from the file browser
                    filePath = fileDialog.FileName;

                    //Set up reader for the RSGO file
                    StreamReader file = new StreamReader(filePath);

                    //Deselect all items in options section
                    UncheckOptionFields(false, true);

                    //Scan through lines of the file
                    while ((fileLine = file.ReadLine()) != null)
                    {
                        if (fileLine.StartsWith("Windowed="))
                        {
                            //Sets the check box
                            windowedLaunch.Checked = SetCheckFieldFromLine(fileLine, "Windowed=");
                        }
                        else if (fileLine.StartsWith("Fullscreen="))
                        {
                            //Sets the check box
                            fullscreenLaunch.Checked = SetCheckFieldFromLine(fileLine, "Fullscreen=");
                        }
                        else if (fileLine.StartsWith("Console="))
                        {
                            //Sets the check box
                            consoleLaunch.Checked = SetCheckFieldFromLine(fileLine, "Console=");
                        }
                        else if (fileLine.StartsWith("AutoConfig="))
                        {
                            //Sets the check box
                            autoConfigLaunch.Checked = SetCheckFieldFromLine(fileLine, "AutoConfig=");
                        }
                        else if (fileLine.StartsWith("AntiAddiction="))
                        {
                            //Sets the check box
                            antiAddictionLaunch.Checked = SetCheckFieldFromLine(fileLine, "AntiAddiction=");
                        }
                        else if (fileLine.StartsWith("Developer="))
                        {
                            //Sets the check box
                            developerLaunch.Checked = SetCheckFieldFromLine(fileLine, "Developer=");
                        }
                        else if (fileLine.StartsWith("NoVideo="))
                        {
                            //Sets the check box
                            noVideoLaunch.Checked = SetCheckFieldFromLine(fileLine, "NoVideo=");
                        }
                        else if (fileLine.StartsWith("HighPriority="))
                        {
                            //Sets the check box
                            highPriorityLaunch.Checked = SetCheckFieldFromLine(fileLine, "HighPriority=");
                        }
                        else if (fileLine.StartsWith("NoD3D9Ex="))
                        {
                            //Sets the check box
                            nod9xLaunch.Checked = SetCheckFieldFromLine(fileLine, "NoD3D9Ex=");
                        }
                        else if (fileLine.StartsWith("Force32Bit="))
                        {
                            //Sets the check box
                            force32BitLaunch.Checked = SetCheckFieldFromLine(fileLine, "Force32Bit=");
                        }
                        else if (fileLine.StartsWith("NoMic="))
                        {
                            //Sets the check box
                            noMicLaunch.Checked = SetCheckFieldFromLine(fileLine, "NoMic=");
                        }
                        else if (fileLine.StartsWith("NoSound="))
                        {
                            //Sets the check box
                            noSoundLaunch.Checked = SetCheckFieldFromLine(fileLine, "NoSound=");
                        }
                        else if (fileLine.StartsWith("ForceDX9="))
                        {
                            //Sets the check box
                            dx9Launch.Checked = SetCheckFieldFromLine(fileLine, "ForceDX9=");
                        }
                        else if (fileLine.StartsWith("ForceDX11="))
                        {
                            //Sets the check box
                            dx11Launch.Checked = SetCheckFieldFromLine(fileLine, "ForceDX11=");
                        }
                        else if (fileLine.StartsWith("ForceGL="))
                        {
                            //Sets the check box
                            glLaunch.Checked = SetCheckFieldFromLine(fileLine, "ForceGL=");
                        }
                        else if (fileLine.StartsWith("CustomLaunchOptions="))
                        {
                            //Clean line
                            customLaunchLine = fileLine.Replace("CustomLaunchOptions=", "");

                            //Sets the check box
                            customLaunchOptions.Text = customLaunchLine;

                            //If there is custom options set
                            if(customLaunchLine != "" && customLaunchLine != null)
                            {
                                //Set the custom check box
                                customLaunch.Checked = true;
                            }
                        }
                    }

                    //Close file
                    file.Close();

                    //Log line in file
                    fileManipulation.LogToFile("Options file was loaded: " + filePath, LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at loadOptions_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at loadOptions_Click", LOG_FILE_ID);
            }
        }

        private void selectAllOptions_Click(object sender, EventArgs e)
        {
            //Initialise Variables
            bool customCheck = false;

            try
            {
                //If options are enabled
                if (enableOptions.Checked)
                {
                    //Uncheck all check boxes
                    UncheckOptionFields(true, true);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at selectAllOptions_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at selectAllOptions_Click", LOG_FILE_ID);
            }
        }

        private void deselectAllOptions_Click(object sender, EventArgs e)
        {
            //Initialise Variables
            bool customCheck = false;

            try
            {
                //If options are enabled
                if(enableOptions.Checked)
                {
                    //Uncheck all check boxes
                    UncheckOptionFields(false, true);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at deselectAllOptions_Click");

                //Log line in file
                fileManipulation.LogToFile("Error at deselectAllOptions_Click", LOG_FILE_ID);
            }
        }

        private void optionsName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //If key down is triggered correctly
                if (e.KeyCode == Keys.Enter)
                {
                    //Trigger save event
                    saveOptions_Click(this, new EventArgs());
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at saveFileName_KeyDown");

                //Log line in file
                fileManipulation.LogToFile("Error at saveFileName_KeyDown", LOG_FILE_ID);
            }
        }

        private void saveFileName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //If key down is triggered correctly
                if (e.KeyCode == Keys.Enter)
                {
                    //Trigger save event
                    saveFilter_Click(this, new EventArgs());
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at saveFileName_KeyDown");

                //Log line in file
                fileManipulation.LogToFile("Error at saveFileName_KeyDown", LOG_FILE_ID);
            }
        }



        //###########################################################################################################################
        //###########################################################################################################################
        // Functions
        //###########################################################################################################################
        //###########################################################################################################################

        public void Initialise()
        {
            //Initialise Variables
            string filePath = "";
            string folderPath = ""; 
            bool successful = false;
            bool customCheck = false;

            try
            {
                //Checks for the Saved Filters folder
                fileManipulation.CheckCreateDirectory("SavedFilters");

                //Checks for the Saved Options folder
                fileManipulation.CheckCreateDirectory("SavedOptions");

                //Sets fields from config
                ReadFromConfig();

                //If config hasn't already set the field
                if (steamDirectory.Text == "" || steamDirectory.Text == null)
                {
                    //Gets Steam's installation location
                    filePath = fileManipulation.GetSteamFilePath();

                    //Test registry file path for steam
                    successful = fileManipulation.TestFilePath(filePath);

                    //If Steam registry key was found
                    if (successful)
                    {
                        //Sets the Read-Only field for use later
                        steamDirectory.Text = filePath;
                    }
                    else
                    {
                        //Set to default file path
                        filePath = "C:\\Program Files (x86)\\Steam";

                        //Test default file path for steam
                        successful = fileManipulation.TestFilePath(filePath);

                        //If steam was found in default file path
                        if (successful)
                        {
                            //Sets the Read-Only field for use later
                            steamDirectory.Text = filePath;
                        }
                        else
                        {
                            //Sets to blank for user to input
                            steamDirectory.Text = "";
                        }
                    }

                    //If file path is not populated
                    if(filterLocation.Text == "" || filterLocation.Text == null)
                    {
                        //Get .exe's current path
                        folderPath = Directory.GetCurrentDirectory();

                        //Append file name to directory
                        folderPath = Path.Combine(folderPath, "SavedFilters");

                        //Sets the filter file path
                        filterLocation.Text = folderPath;
                    }

                    //If file path is not populated
                    if (optionsPath.Text == "" || optionsPath.Text == null)
                    {
                        //Get .exe's current path
                        folderPath = Directory.GetCurrentDirectory();

                        //Append file name to directory
                        folderPath = Path.Combine(folderPath, "SavedOptions");

                        //Sets the filter file path
                        optionsPath.Text = folderPath;
                    }
                }

                //Sets or unsets the filter box
                HandleFilterBox();

                //If enable options is unchecked
                if(!enableOptions.Checked)
                {
                    //Uncheck all check boxes
                    UncheckOptionFields(false, true);

                    //Disable the check boxes
                    ToggleOptionFields(false);
                }

                //Load a placeholder image if it can't find one for the app ID
                gameLoadImage.Image = new Bitmap(Properties.Resources.RSGLogoTransparent, 32, 32);

                //Log line in file
                fileManipulation.LogToFile("Initialisation complete", LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at Initialise");

                //Log line in file
                fileManipulation.LogToFile("Error at Initialise", LOG_FILE_ID);
            }
        }

        public void LaunchGame()
        {
            //Initialise Variables
            string filePath = "";
            string filePathTest = "";
            string appID = "";
            string steamDirectoryText = "";
            string launchOptionsString = "";
            string[] arrayDirectories = null;
            string[] steamAppIDs = null;
            bool success = false;
            bool filtersCheck = false;
            bool showLogoCheck = false;
            bool autoCloseChecked = false;

            try
            {
                //Gets the steam directory set
                steamDirectoryText = steamDirectory.Text;

                //Checks that the user selected a directory
                success = processing.CheckDirectory(steamDirectoryText);

                //Checks if filters are applied
                filtersCheck = applyFilters.Checked;

                //Checks if show logo is checked
                showLogoCheck = showLogo.Checked;

                //Checks if auto close is checked
                autoCloseChecked = autoClose.Checked;

                //Creates a test version of the file path
                filePathTest = steamDirectoryText;

                //Append '\Steam.exe' to directory
                filePathTest = Path.Combine(filePathTest, "Steam.exe");

                //If the user did select a directory
                if (success && File.Exists(filePathTest))
                {
                    //Get the found directory from field (Read-Only)
                    filePath = steamDirectory.Text;

                    //Gets the file which contains the user's Steam library locations
                    filePath = fileManipulation.GetSteamLibraryFoldersFile(filePath);

                    //Gets the Steam library locations as an array of strings
                    arrayDirectories = fileManipulation.GetSteamLibraries(filePath);

                    //Gets an array of the Steam app IDs
                    steamAppIDs = GetSteamAppIDs(arrayDirectories);

                    //Selects a random Steam app ID
                    appID = processing.SelectRandomAppID(steamAppIDs);

                    Console.WriteLine(appID);

                    //If show logo is checked
                    if (showLogoCheck)
                    {
                        //Sets the icon and label
                        GetIconFile(appID);
                    }

                    //If launch options is enabled
                    if(enableOptions.Checked)
                    {
                        //Adds launch options
                        launchOptionsString += launchOptions.Text;

                        //Custom launch options are enabled
                        if(customLaunch.Checked)
                        {
                            //Adds a space for usability
                            launchOptionsString += " ";

                            //Adds custom launch options
                            launchOptionsString += customLaunchOptions.Text;
                        }
                    }

                    //Starts that Steam app
                    processing.StartSteamApp(appID, launchOptionsString, steamDirectory.Text);

                    //Log line in file
                    fileManipulation.LogToFile("Steam game was launched: " + appID, LOG_FILE_ID);

                    //If launch options is enabled
                    if (enableOptions.Checked)
                    {
                        //Log line in file
                        fileManipulation.LogToFile("Launch parameters used: " + launchOptionsString, LOG_FILE_ID);
                    }

                    //If auto close is checked
                    if (autoCloseChecked)
                    {
                        //Close Application - Triggers the on close event
                        Application.Exit();
                    }
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at LaunchGame");

                //Log line in file
                fileManipulation.LogToFile("Error at LaunchGame", LOG_FILE_ID);
            }
        }

        public void PopulateListBox(string[] appIDs)
        {
            //Initialise Variables
            int i = 0;
            char[] splitPattern = { '|' };
            string[] strippedNameArray = null;
            string strippedName = "";

            try
            {
                //If there are no app ids
                if(appIDs.Length != 0)
                {
                    //For every app id / name
                    for (i = 0; i < appIDs.Length; i++)
                    {
                        //Splits the array element
                        strippedNameArray = appIDs[i].Split(splitPattern);

                        //Sets the name (always second element)
                        strippedName = strippedNameArray[1];

                        //Adds names to the filter box
                        filterBox.Items.Add(strippedName);
                    }
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at PopulateListBox");

                //Log line in file
                fileManipulation.LogToFile("Error at PopulateListBox", LOG_FILE_ID);
            }
        }

        public void HandleFilterBox()
        {
            //Initialise Variables
            bool applyFiltersCheck = false;

            try
            {
                //Gets the status of Apply Filters
                applyFiltersCheck = applyFilters.Checked;

                //If it is checked
                if (applyFiltersCheck)
                {
                    //Enables the filter box
                    filterBox.Enabled = true;

                    //Log line in file
                    fileManipulation.LogToFile("Filter box enabled", LOG_FILE_ID);
                }
                else
                {
                    //Deselect all items in filter box
                    SetSelectListBox(false);

                    //Disables the filter box
                    filterBox.Enabled = false;

                    //Log line in file
                    fileManipulation.LogToFile("Filter box disabled", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at HandleFilterBox");

                //Log line in file
                fileManipulation.LogToFile("Error at HandleFilterBox", LOG_FILE_ID);
            }
        }

        public string[] GetSteamAppIDs(string[] arrayDirectories)
        {
            //Initialise Variables
            string[] steamAppIDs = null;
            string[] steamGames = null;
            string[] filteredGames = null;
            string steamGamePath = "";
            string appName = "";
            bool successful = true;
            bool filterCheck = false;
            int i;
            int j;
            int k;

            try
            {
                //Creates the Steam app IDs list
                List<string> listSteamAppIDs = new List<string>();

                //Checks if filters should be applied
                filterCheck = applyFilters.Checked;

                //Gets the filtered games
                filteredGames = GetFilterItems();

                //For every Steam library folder
                for (i = 0; i < arrayDirectories.Length; i++)
                {
                    try
                    {
                        //Log line in file
                        fileManipulation.LogToFile("Steam library found at: " + arrayDirectories[i], LOG_FILE_ID);

                        //Looks for all appmanifest_*.acf files in the Steam library
                        steamGames = Directory.GetFiles(arrayDirectories[i], "*.acf");

                        //For each appmanifest_*.acf in the Steam library
                        for (j = 0; j < steamGames.Length; j++)
                        {
                            //Sets the appmanifest_*.acf path
                            steamGamePath = Path.GetFileName(steamGames[j]);

                            //Gets the name of the application matched to the appmanifest file
                            appName = fileManipulation.GetAppIDName(steamGames[j]);

                            //Log line in file
                            fileManipulation.LogToFile(appName + " found", LOG_FILE_ID);

                            //If apply filters is checked
                            if (filterCheck)
                            {
                                //For each filtered game
                                for (k = 0; k < filteredGames.Length; k++)
                                {
                                    //Check if the app contains filtered game
                                    if (Regex.Match(appName, filteredGames[k]).Index != 0)
                                    {
                                        //If so, set successful flag
                                        successful = false;
                                    }
                                }
                            }

                            //Reset variable
                            k = 0;

                            //If game was not in the filter list
                            if(successful)
                            {
                                //Add app ID to the list
                                listSteamAppIDs.Add(appName);
                            }

                            //Reset variable
                            successful = true;
                        }
                        //Reset variable
                        j = 0;
                    }
                    catch
                    {
                        //Error because a steam library was found without anything installed
                //      MessageBox.Show("Steam directory found without content.");
                    }
                }

                //Converts list into an array of strings
                steamAppIDs = listSteamAppIDs.ToArray();
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at GetSteamAppIDs");

                //Log line in file
                fileManipulation.LogToFile("Error at GetSteamAppIDs", LOG_FILE_ID);
            }
            return steamAppIDs;
        }

        private string[] GetFilterItems()
        {
            //Initialise Variables
            string selectedItem = "";
            string[] filtersArray = null;

            try
            {
                //Creates the Steam app IDs list
                List<string> filteredGames = new List<string>();

                //For each item selected in the filter box
                for (int i = 0; i < filterBox.SelectedIndices.Count; i++)
                {
                    //Gets each selected item and converts to a string
                    selectedItem = filterBox.Items[filterBox.SelectedIndices[i]].ToString();

                    //Adds item to the list
                    filteredGames.Add(selectedItem);
                }

                //Converts list into an array of strings
                filtersArray = filteredGames.ToArray();
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at GetFilterItems");

                //Log line in file
                fileManipulation.LogToFile("Error at GetFilterItems", LOG_FILE_ID);
            }
            return filtersArray;
        }

        public void SetSelectListBox(bool selection)
        {
            //Initialise Variables
            int i;

            try
            {
                //For each item in the filter box
                for (i = 0; i < filterBox.Items.Count; i++)
                {
                    //Set each to selection (true / false)
                    filterBox.SetSelected(i, selection);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at SetSelectListBox");

                //Log line in file
                fileManipulation.LogToFile("Error at SetSelectListBox", LOG_FILE_ID);
            }
        }

        public void WriteToConfig()
        {
            //Initialise variables
            string[] filtersSet = null;
            string[] configHeader = null;
            string[] configBody = null;
            string executionPath = "";
            string filePathFieldText = "";
            string filtersToSet = "";
            string filtersSavePath = "";
            string optionsPathSet = "";
            bool applyFiltersCheck = false;
            bool autoRunCheck = false;
            bool autoCloseCheck = false;
            bool showLogoCheck = false;
            bool overwriteFileCheck = false;
            bool customLaunchCheck = false;
            bool enableLaunchCheck = false;
            bool overwriteOptionsCheck = false;
            int i;
            int j;
            int k;

            try
            {
                //Creates the config body list
                List<string> listConfigBody = new List<string>();

                //Get config path
                executionPath = fileManipulation.CheckCreateConfig();

                //Gets set directory
                filePathFieldText = steamDirectory.Text;

                //Gets filters save directory
                filtersSavePath = filterLocation.Text;

                //Gets applied filters check state
                applyFiltersCheck = applyFilters.Checked;

                //Gets auto run check state
                autoRunCheck = autoRun.Checked;

                //Gets auto close check state
                autoCloseCheck = autoClose.Checked;

                //Gets show logo check state
                showLogoCheck = showLogo.Checked;

                //Gets overwrite file check state
                overwriteFileCheck = overwriteFilter.Checked;

                //Gets custom launch check state
                customLaunchCheck = customLaunch.Checked;

                //Gets enable launch check state
                enableLaunchCheck = enableOptions.Checked;

                //Gets the options path
                optionsPathSet = optionsPath.Text;

                //Gets overwrite options check state
                overwriteOptionsCheck = overwriteOptions.Checked;

                //Gets the config header
                configHeader = processing.GenerateSaveFileHeader(executionPath, "configuration");

                //Adds the splitter for options
                listConfigBody.Add("# RSG Home Page Section");

                //Adds the path
                listConfigBody.Add("Path=" + filePathFieldText);

                //Adds the filters path
                listConfigBody.Add("FiltersPath=" + filtersSavePath);

                //Adds the applied filters
                listConfigBody.Add("ApplyFilters=" + applyFiltersCheck.ToString());

                //Adds the auto run
                listConfigBody.Add("AutoRun=" + autoRunCheck.ToString());

                //Adds the auto close
                listConfigBody.Add("AutoClose=" + autoCloseCheck.ToString());

                //Adds the show logo
                listConfigBody.Add("ShowLogo=" + showLogoCheck.ToString());

                //Adds the overwrite file
                listConfigBody.Add("OverwriteFilter=" + overwriteFileCheck.ToString());

                //Adds the splitter for options
                listConfigBody.Add("# Steam Launch Options Section");

                //Adds the enable launch options
                listConfigBody.Add("EnableLaunchOptions=" + enableLaunchCheck.ToString());

                //Adds the custom launch options
                listConfigBody.Add("CustomLaunch=" + customLaunchCheck.ToString());

                //Adds the options path
                listConfigBody.Add("OptionsPath=" + optionsPathSet);

                //Adds the overwrite options
                listConfigBody.Add("OverwriteOptions=" + overwriteOptionsCheck.ToString());

                //Converts list into an array of strings
                configBody = listConfigBody.ToArray();

                //Wrap text writer so not to over-write itself
                using (StreamWriter configWrite = new StreamWriter(executionPath, true))
                {
                    //For each header line
                    for(j = 0; j < configHeader.Length; j++)
                    {
                        //Write to the file
                        configWrite.WriteLine(configHeader[j]);
                    }

                    //For each body line
                    for (k = 0; k < configBody.Length; k++)
                    {
                        //Write to the file
                        configWrite.WriteLine(configBody[k]);
                    }

                    //Close file writer
                    configWrite.Close();
                }

                //Write temporary filters
                SaveTempFilter();

                //Write temporary options
                SaveTempOptions();

                //Log line in file
                fileManipulation.LogToFile("Config has been saved", LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at WriteToConfig");

                //Log line in file
                fileManipulation.LogToFile("Error at WriteToConfig", LOG_FILE_ID);
            }
        }

        public void GetIconFile(string appID)
        {
            //Initialise variables
            string iconPath = "";
            string appName = "";
            string[] appIDSplit = null;

            try
            {
                //If a game was found
                if(appID != "" && appID != null)
                {
                    //Splits app ID from game name
                    appIDSplit = appID.Split('|');

                    //Sets the appID correctly
                    appID = appIDSplit[0];

                    //Sets the appName correctly
                    appName = appIDSplit[1];

                    //If name is longer than 40
                    if (appName.Length > 30)
                    {
                        //Shorten it
                        appName = appName.Substring(0, 30) + "...";
                    }

                    //Sets the label to the game being loaded
                    gameLoaded.Text = "Launching: " + appName;

                    try
                    {
                        //Gets the selected app ID's .ico path
                        iconPath = fileManipulation.GetIconFilePath(appID);

                        //Sets the image box to the icon found for the appID
                        gameLoadImage.Image = new Icon(iconPath, 32, 32).ToBitmap();

                        //Sets the image box to the icon found for the appID
                        gameLoadImage.Image = new Icon(iconPath, 32, 32).ToBitmap();
                    }
                    catch
                    {
                        //Load a placeholder image if it can't find one for the app ID
                        gameLoadImage.Image = new Bitmap(Properties.Resources.RSGLogoTransparent, 32, 32);

                        //Log line in file
                        fileManipulation.LogToFile("Logo not found in Registry for: " + appName, LOG_FILE_ID);
                    }
                }
                else
                {
                    //Log line in file
                    fileManipulation.LogToFile("No app id could be found", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at GetIconFile");

                //Log line in file
                fileManipulation.LogToFile("Error at GetIconFile", LOG_FILE_ID);
            }
        }

        public void ReadFromConfig()
        {
            //Initialise Variables
            string executionPath = "";
            string fileLine = "";
            string filePath = "";
            string filterPath = "";
            string optionsSetPath = "";
            string[] filtersToSet = null;
            bool successful = false;
            bool customCheck = false;
            int i;

            try
            {
                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append '\config.rsg' to directory
                executionPath = Path.Combine(executionPath, "config.rsgc");

                //If config exists
                if (File.Exists(executionPath))
                {
                    //Set up reader for the appmanifest_*.acf
                    StreamReader file = new StreamReader(executionPath);

                    //Scan through lines of the file
                    while ((fileLine = file.ReadLine()) != null)
                    {
                        //Check each line for starting string
                        if (fileLine.StartsWith("Path="))
                        {
                            //Set to default file path
                            filePath = fileLine.Replace("Path=", "");
                            
                            //Test default file path for steam
                            successful = fileManipulation.TestFilePath(filePath);

                            //If steam was found in default file path
                            if (successful)
                            {
                                //Sets the Read-Only field for use later
                                steamDirectory.Text = filePath;
                            }
                            else
                            {
                                //Sets to blank for user to input
                                steamDirectory.Text = "";
                            }
                        }
                        else if (fileLine.StartsWith("FiltersPath="))
                        {
                            //Set to default file path
                            filterPath = fileLine.Replace("FiltersPath=", "");

                            //If filter directory was found
                            if (Directory.Exists(filterPath) && filterPath != "")
                            {
                                //Sets the Read-Only field for use later
                                filterLocation.Text = filterPath;
                            }
                            else
                            {
                                //Get .exe's current path
                                filterPath = Directory.GetCurrentDirectory();

                                //Append '\SavedFilters' to directory
                                filterPath = Path.Combine(filterPath, "SavedFilters");

                                //If current directory has a Saved Filters folder
                                if (Directory.Exists(filterPath))
                                {
                                    //Sets the Read-Only field for use later
                                    filterLocation.Text = filterPath;
                                }
                                else
                                {
                                    //Sets to nothing for user to input
                                    filterLocation.Text = "";
                                }
                            }
                        }
                        else if(fileLine.StartsWith("ApplyFilters="))
                        {
                            //Sets the apply filters check box
                            applyFilters.Checked = SetCheckFieldFromLine(fileLine, "ApplyFilters=");
                        }
                        else if (fileLine.StartsWith("AutoRun="))
                        {
                            //Sets the auto run check box
                            autoRun.Checked = SetCheckFieldFromLine(fileLine, "AutoRun=");
                        }
                        else if (fileLine.StartsWith("AutoClose="))
                        {
                            //Sets the auto close check box
                            autoClose.Checked = SetCheckFieldFromLine(fileLine, "AutoClose=");
                        }
                        else if (fileLine.StartsWith("ShowLogo="))
                        {
                            //Sets the show logo check box
                            showLogo.Checked = SetCheckFieldFromLine(fileLine, "ShowLogo=");
                        }
                        else if (fileLine.StartsWith("OverwriteFilter="))
                        {
                            //Sets the show logo check box
                            overwriteFilter.Checked = SetCheckFieldFromLine(fileLine, "OverwriteFilter=");
                        }
                        else if (fileLine.StartsWith("EnableLaunchOptions="))
                        {
                            //Sets the show logo check box
                            enableOptions.Checked = SetCheckFieldFromLine(fileLine, "EnableLaunchOptions=");

                            //If it is checked
                            if (enableOptions.Checked)
                            {
                                //Enables the custom options boxes
                                ToggleOptionFields(true);
                            }
                            else
                            {
                                //Uncheck fields
                                UncheckOptionFields(false, false);

                                //Enables the custom options boxes
                                ToggleOptionFields(false);
                            }
                        }
                        else if (fileLine.StartsWith("CustomLaunch="))
                        {
                            //Sets the show logo check box
                            customLaunch.Checked = SetCheckFieldFromLine(fileLine, "CustomLaunch=");

                            //Sets the read only on custom launch text field
                            customLaunchOptions.ReadOnly = !(customLaunch.Checked);
                        }
                        else if (fileLine.StartsWith("OverwriteOptions="))
                        {
                            //Sets the show logo check box
                            overwriteOptions.Checked = SetCheckFieldFromLine(fileLine, "OverwriteOptions=");
                        }
                        else if (fileLine.StartsWith("OptionsPath="))
                        {
                            //Set to default file path
                            optionsSetPath = fileLine.Replace("OptionsPath=", "");

                            //If filter directory was found
                            if (Directory.Exists(optionsSetPath) && optionsSetPath != "")
                            {
                                //Sets the Read-Only field for use later
                                optionsPath.Text = optionsSetPath;
                            }
                            else
                            {
                                //Get .exe's current path
                                optionsSetPath = Directory.GetCurrentDirectory();

                                //Append '\SavedOptions' to directory
                                optionsSetPath = Path.Combine(optionsSetPath, "SavedOptions");

                                //If current directory has a Saved Filters folder
                                if (Directory.Exists(optionsSetPath))
                                {
                                    //Sets the Read-Only field for use later
                                    optionsPath.Text = optionsSetPath;
                                }
                                else
                                {
                                    //Sets to nothing for user to input
                                    optionsPath.Text = "";
                                }
                            }
                        }
                    }

                    //Close the reader
                    file.Close();

                    //Load temporary filter
                    LoadTempFilter();

                    //Load temporary options
                    LoadTempOptions();

                    //Log line in file
                    fileManipulation.LogToFile("Config has been read from", LOG_FILE_ID);
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at ReadFromConfig");

                //Log line in file
                fileManipulation.LogToFile("Error at ReadFromConfig", LOG_FILE_ID);
            }
        }

        public bool SetCheckFieldFromLine(string configLine, string startsWith)
        {
            //Initialise Variables
            bool fieldCheck = false;

            try
            {
                //Strips the config line
                configLine = configLine.Replace(startsWith, "");

                //If line is true
                if(configLine == "True" || configLine == "true")
                {
                    //Set check box to true
                    fieldCheck = true;
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at SetCheckFieldFromLine");

                //Log line in file
                fileManipulation.LogToFile("Error at SetCheckFieldFromLine", LOG_FILE_ID);
            }
            return fieldCheck;
        }

        public void CreateFilterFile(string filePath)
        {
            //Initialise Variables
            string[] filtersSet = null;
            string[] filterHeader = null;
            string executionPath = "";
            int i;
            int j;

            try
            {
                //Gets each filter set
                filtersSet = GetFilterItems();

                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append file name to directory
                executionPath = Path.Combine(executionPath, "SavedFilters");

                //Generate the header file
                filterHeader = processing.GenerateSaveFileHeader(executionPath, "filter");

                //Create the .rsgf file
                var file = File.Create(filePath);

                //Close file after creating
                file.Close();

                //Wrap text writer so not to over-write itself
                using (StreamWriter filterWrite = new StreamWriter(filePath, true))
                {
                    //For each filter line
                    for (i = 0; i < filterHeader.Length; i++)
                    {
                        //Write to the file
                        filterWrite.WriteLine(filterHeader[i]);
                    }

                    //For each filter line
                    for (j = 0; j < filtersSet.Length; j++)
                    {
                        //Write to the file
                        filterWrite.WriteLine(filtersSet[j]);
                    }

                    //Close file writer
                    filterWrite.Close();
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CreateFilterFile");

                //Log line in file
                fileManipulation.LogToFile("Error at CreateFilterFile", LOG_FILE_ID);
            }
        }

        public void SaveTempFilter()
        {
            //Initialise Variables
            string filePath = "";

            try
            {
                //Test the temporary file path
                filePath = fileManipulation.CheckCreateTempFilters();

                //Create the file
                WriteTempFilter(filePath);

                //Log line in file
                fileManipulation.LogToFile("Filter file was created: " + filePath, LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at SaveTempFilter");

                //Log line in file
                fileManipulation.LogToFile("Error at SaveTempFilter", LOG_FILE_ID);
            }
        }

        public void LoadTempFilter()
        {
            //Initialise Variables
            string filePath = "";
            string fileLine = "";

            try
            {
                //Get .exe's current path
                filePath = Directory.GetCurrentDirectory();

                //Append '\tempFilters.rsg' to directory
                filePath = Path.Combine(filePath, "tempFilters.rsgf");

                //Deselect all items in filter box
                SetSelectListBox(false);

                //Set up reader for the RSGF file
                using (StreamReader file = new StreamReader(filePath))
                {
                    //Scan through lines of the file
                    while ((fileLine = file.ReadLine()) != null)
                    {
                        //Sets the filter box for each found string
                        filterBox.SelectedItem = fileLine;
                    }

                    //Close file
                    file.Close();
                }

                //Log line in file
                fileManipulation.LogToFile("Filter file was loaded: " + filePath, LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at LoadTempFilter");

                //Log line in file
                fileManipulation.LogToFile("Error at LoadTempFilter", LOG_FILE_ID);
            }
        }

        public void WriteTempFilter(string filePath)
        {
            //Initialise Variables
            string[] filtersSet = null;
            string[] filterHeader = null;
            string executionPath = "";
            int i = 0;
            int j = 0;

            try
            {
                //Gets each filter set
                filtersSet = GetFilterItems();

                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append file name to directory
                executionPath = Path.Combine(executionPath, "SavedFilters");

                //Generate the header file
                filterHeader = processing.GenerateSaveFileHeader(executionPath, "filter");

                //Wrap text writer so not to over-write itself
                using (StreamWriter filterWrite = new StreamWriter(filePath, true))
                {
                    //For each filter header line
                    for (i = 0; i < filterHeader.Length; i++)
                    {
                        //Write to the file
                        filterWrite.WriteLine(filterHeader[i]);
                    }

                    //For each filter line
                    for (j = 0; j < filtersSet.Length; j++)
                    {
                        //Write to the file
                        filterWrite.WriteLine(filtersSet[j]);
                    }

                    //Close file writer
                    filterWrite.Close();
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at WriteTempFilter");

                //Log line in file
                fileManipulation.LogToFile("Error at WriteTempFilter", LOG_FILE_ID);
            }
        }

        public void ToggleOptionFields(bool setType)
        {
            try
            {
                //Enables the custom options box
                launchOptions.Enabled = setType;

                //Enables the custom options box
                customLaunchOptions.Enabled = setType;

                //Enables the custom check box
                customLaunch.Enabled = setType;

                //Enables the custom check box
                windowedLaunch.Enabled = setType;

                //Enables the custom check box
                fullscreenLaunch.Enabled = setType;

                //Enables the custom check box
                consoleLaunch.Enabled = setType;

                //Enables the custom check box
                autoConfigLaunch.Enabled = setType;

                //Enables the custom check box
                antiAddictionLaunch.Enabled = setType;

                //Enables the custom check box
                developerLaunch.Enabled = setType;

                //Enables the custom check box
                noVideoLaunch.Enabled = setType;

                //Enables the custom check box
                highPriorityLaunch.Enabled = setType;

                //Enables the custom check box
                nod9xLaunch.Enabled = setType;

                //Enables the custom check box
                force32BitLaunch.Enabled = setType;

                //Enables the custom check box
                noMicLaunch.Enabled = setType;

                //Enables the custom check box
                noSoundLaunch.Enabled = setType;

                //Enables the custom check box
                dx9Launch.Enabled = setType;

                //Enables the custom check box
                dx11Launch.Enabled = setType;

                //Enables the custom check box
                glLaunch.Enabled = setType;

                //Log line in file
                fileManipulation.LogToFile("Launch options set to: " + setType, LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at ToggleOptionFields");

                //Log line in file
                fileManipulation.LogToFile("Error at ToggleOptionFields", LOG_FILE_ID);
            }
        }

        public void UncheckOptionFields(bool setType, bool skipCust)
        {
            try
            {
                //Skips custom launch
                if(!skipCust)
                {
                    //Enables the custom check box
                    customLaunch.Checked = setType;
                }

                //Enables the custom check box
                windowedLaunch.Checked = setType;

                //Enables the custom check box
                fullscreenLaunch.Checked = setType;

                //Enables the custom check box
                consoleLaunch.Checked = setType;

                //Enables the custom check box
                autoConfigLaunch.Checked = setType;

                //Enables the custom check box
                antiAddictionLaunch.Checked = setType;

                //Enables the custom check box
                developerLaunch.Checked = setType;

                //Enables the custom check box
                noVideoLaunch.Checked = setType;

                //Enables the custom check box
                highPriorityLaunch.Checked = setType;

                //Enables the custom check box
                nod9xLaunch.Checked = setType;

                //Enables the custom check box
                force32BitLaunch.Checked = setType;

                //Enables the custom check box
                noMicLaunch.Checked = setType;

                //Enables the custom check box
                noSoundLaunch.Checked = setType;

                //Enables the custom check box
                dx9Launch.Checked = setType;

                //Enables the custom check box
                dx11Launch.Checked = setType;

                //Enables the custom check box
                glLaunch.Checked = setType;
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at UncheckOptionFields");

                //Log line in file
                fileManipulation.LogToFile("Error at UncheckOptionFields", LOG_FILE_ID);
            }
        }

        public void CreateOptionsFile(string filePath)
        {
            //Initialise Variables
            string[] optionsList = null;
            string[] optionsHeader = null;
            string executionPath = "";
            int i;
            int j;

            try
            {
                //Gets each filter set
                optionsList = GenerateOptionsStatus();

                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append file name to directory
                executionPath = Path.Combine(executionPath, "SavedOptions");

                //Generate the header file
                optionsHeader = processing.GenerateSaveFileHeader(executionPath, "options");

                //Create the .rsgf file
                var file = File.Create(filePath);

                //Close file after creating
                file.Close();

                //Wrap text writer so not to over-write itself
                using (StreamWriter optionWrite = new StreamWriter(filePath, true))
                {
                    //For each options header line
                    for (i = 0; i < optionsHeader.Length; i++)
                    {
                        //Write to the file
                        optionWrite.WriteLine(optionsHeader[i]);
                    }

                    //For each options line
                    for (j = 0; j < optionsList.Length; j++)
                    {
                        //Write to the file
                        optionWrite.WriteLine(optionsList[j]);
                    }

                    //Close file writer
                    optionWrite.Close();
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at CreateOptionsFile");

                //Log line in file
                fileManipulation.LogToFile("Error at CreateOptionsFile", LOG_FILE_ID);
            }
        }
        
        public string[] GenerateOptionsStatus()
        {
            //Initialise variables
            string[] optionsList = null;
            string cleanCustom = "";

            try
            {
                //Creates the Steam app IDs list
                List<string> listOptionsList = new List<string>();

                //Adds to list of options
                listOptionsList.Add("Windowed=" + windowedLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("Fullscreen=" + fullscreenLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("Console=" + consoleLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("AutoConfig=" + autoConfigLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("AntiAddiction=" + antiAddictionLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("Developer=" + developerLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("NoVideo=" + noVideoLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("HighPriority=" + highPriorityLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("NoD3D9ex=" + nod9xLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("Force32Bit=" + force32BitLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("NoMic=" + noMicLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("NoSound=" + noSoundLaunch.Checked);

                //Adds to list of options
                listOptionsList.Add("ForceDX9=" + dx9Launch.Checked);

                //Adds to list of options
                listOptionsList.Add("ForceDX11=" + dx11Launch.Checked);

                //Adds to list of options
                listOptionsList.Add("ForceGL=" + glLaunch.Checked);

                //Cleans custom options
                cleanCustom = customLaunchOptions.Text.Replace(Environment.NewLine, " ");

                //Adds to list of options
                listOptionsList.Add("CustomLaunchOptions=" + cleanCustom);

                //Converts list to array
                optionsList = listOptionsList.ToArray();
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at GenerateOptionsStatus");

                //Log line in file
                fileManipulation.LogToFile("Error at GenerateOptionsStatus", LOG_FILE_ID);
            }
            return optionsList;
        }

        public void SaveTempOptions()
        {
            //Initialise Variables
            string filePath = "";

            try
            {
                //Test the temporary file path
                filePath = fileManipulation.CheckCreateTempOptions();

                //Create the file
                WriteTempOptions(filePath);

                //Log line in file
                fileManipulation.LogToFile("Options file was created: " + filePath, LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at SaveTempOptions");

                //Log line in file
                fileManipulation.LogToFile("Error at SaveTempOptions", LOG_FILE_ID);
            }
        }

        public void LoadTempOptions()
        {
            //Initialise Variables
            string filePath = "";
            string fileLine = "";
            string customLaunchLine = "";
            bool customCheck = false;

            try
            {
                //Get .exe's current path
                filePath = Directory.GetCurrentDirectory();

                //Append '\tempOptionss.rsgo' to directory
                filePath = Path.Combine(filePath, "tempOptions.rsgo");

                //Deselect all items in options section
                UncheckOptionFields(false, true);

                //Set up reader for the RSGO file
                using (StreamReader file = new StreamReader(filePath))
                {
                    //Scan through lines of the file
                    while ((fileLine = file.ReadLine()) != null)
                    {
                        if (fileLine.StartsWith("Windowed="))
                        {
                            //Sets the check box
                            windowedLaunch.Checked = SetCheckFieldFromLine(fileLine, "Windowed=");
                        }
                        else if (fileLine.StartsWith("Fullscreen="))
                        {
                            //Sets the check box
                            fullscreenLaunch.Checked = SetCheckFieldFromLine(fileLine, "Fullscreen=");
                        }
                        else if (fileLine.StartsWith("Console="))
                        {
                            //Sets the check box
                            consoleLaunch.Checked = SetCheckFieldFromLine(fileLine, "Console=");
                        }
                        else if (fileLine.StartsWith("AutoConfig="))
                        {
                            //Sets the check box
                            autoConfigLaunch.Checked = SetCheckFieldFromLine(fileLine, "AutoConfig=");
                        }
                        else if (fileLine.StartsWith("AntiAddiction="))
                        {
                            //Sets the check box
                            antiAddictionLaunch.Checked = SetCheckFieldFromLine(fileLine, "AntiAddiction=");
                        }
                        else if (fileLine.StartsWith("Developer="))
                        {
                            //Sets the check box
                            developerLaunch.Checked = SetCheckFieldFromLine(fileLine, "Developer=");
                        }
                        else if (fileLine.StartsWith("NoVideo="))
                        {
                            //Sets the check box
                            noVideoLaunch.Checked = SetCheckFieldFromLine(fileLine, "NoVideo=");
                        }
                        else if (fileLine.StartsWith("HighPriority="))
                        {
                            //Sets the check box
                            highPriorityLaunch.Checked = SetCheckFieldFromLine(fileLine, "HighPriority=");
                        }
                        else if (fileLine.StartsWith("NoD3D9ex="))
                        {
                            //Sets the check box
                            nod9xLaunch.Checked = SetCheckFieldFromLine(fileLine, "NoD3D9ex=");
                        }
                        else if (fileLine.StartsWith("Force32Bit="))
                        {
                            //Sets the check box
                            force32BitLaunch.Checked = SetCheckFieldFromLine(fileLine, "Force32Bit=");
                        }
                        else if (fileLine.StartsWith("NoMic="))
                        {
                            //Sets the check box
                            noMicLaunch.Checked = SetCheckFieldFromLine(fileLine, "NoMic=");
                        }
                        else if (fileLine.StartsWith("NoSound="))
                        {
                            //Sets the check box
                            noSoundLaunch.Checked = SetCheckFieldFromLine(fileLine, "NoSound=");
                        }
                        else if (fileLine.StartsWith("ForceDX9="))
                        {
                            //Sets the check box
                            dx9Launch.Checked = SetCheckFieldFromLine(fileLine, "ForceDX9=");
                        }
                        else if (fileLine.StartsWith("ForceDX11="))
                        {
                            //Sets the check box
                            dx11Launch.Checked = SetCheckFieldFromLine(fileLine, "ForceDX11=");
                        }
                        else if (fileLine.StartsWith("ForceGL="))
                        {
                            //Sets the check box
                            glLaunch.Checked = SetCheckFieldFromLine(fileLine, "ForceGL=");
                        }
                        else if (fileLine.StartsWith("CustomLaunchOptions="))
                        {
                            //Clean line
                            customLaunchLine = fileLine.Replace("CustomLaunchOptions=", "");

                            //Sets the check box
                            customLaunchOptions.Text = customLaunchLine;

                            //If there is custom options set
                            if (customLaunchLine != "" && customLaunchLine != null)
                            {
                                //Set the custom check box
                                customLaunch.Checked = true;
                            }
                        }
                    }

                    //Close file
                    file.Close();
                }

                //Log line in file
                fileManipulation.LogToFile("Options file was loaded: " + filePath, LOG_FILE_ID);
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at LoadTempOptions");

                //Log line in file
                fileManipulation.LogToFile("Error at LoadTempOptions", LOG_FILE_ID);
            }
        }

        public void WriteTempOptions(string filePath)
        {
            //Initialise Variables
            string[] optionsList = null;
            string[] optionsHeader = null;
            string fileLine = "";
            string executionPath = "";
            int i;
            int j;

            try
            {
                //Gets each filter set
                optionsList = GenerateOptionsStatus();

                //Get .exe's current path
                executionPath = Directory.GetCurrentDirectory();

                //Append file name to directory
                executionPath = Path.Combine(executionPath, "SavedOptions");

                //Generate the header file
                optionsHeader = processing.GenerateSaveFileHeader(executionPath, "options");

                //Wrap text writer so not to over-write itself
                using (StreamWriter optionsWrite = new StreamWriter(filePath, true))
                {
                    //For each options header line
                    for (i = 0; i < optionsHeader.Length; i++)
                    {
                        //Write to the file
                        optionsWrite.WriteLine(optionsHeader[i]);
                    }

                    //For each options line
                    for (j = 0; j < optionsList.Length; j++)
                    {
                        //Write to the file
                        optionsWrite.WriteLine(optionsList[j]);
                    }
                    
                    //Close file writer
                    optionsWrite.Close();
                }
            }
            catch
            {
                //Error message
                MessageBox.Show("Error at WriteTempOptions");

                //Log line in file
                fileManipulation.LogToFile("Error at WriteTempOptions", LOG_FILE_ID);
            }
        }
    }
}
