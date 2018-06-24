using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;
using MahApps.Metro.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;
using System.ComponentModel;

namespace DatabaseScript
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow

    {
        IEnumerable<string> folderNames = Enumerable.Empty<string>();
        List<string> selectedFoldersList = new List<string>();
        List<MatchEntry> matches = new List<MatchEntry>();
        public int singlesCounter
        {
            get; set;
        }
        public int doublesCounter
        {
            get; set;

        }



        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            UpdateCounters(singlesCounter, doublesCounter);
            UpdateSelectedFolders(selectedFoldersList);
        }

        private void LoadFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog openFolder = new CommonOpenFileDialog();
            openFolder.Multiselect = true;
            openFolder.IsFolderPicker = true;
            openFolder.Title = "Select folders with matches";
            DialogResult result = (System.Windows.Forms.DialogResult)openFolder.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //Save selected folders in variable folderNames
                folderNames = folderNames.Concat(openFolder.FileNames);
                UpdateSelectedFolders(folderNames);

                //Make a list of all files within the selected folders

                IEnumerable<string> fileNames = new List<string>();
                matches.Clear();
                foreach (string value in folderNames)
                {
                    fileNames = fileNames.Concat(Directory.GetFiles(value, "*", SearchOption.AllDirectories).Select(f => System.IO.Path.GetFileName(f)));
                }
                string completeListOfFiles = string.Join("\n\n", fileNames.ToArray());
                //System.Windows.Forms.MessageBox.Show("Complete list of files:\n" + completeListOfFiles);

                singlesCounter = 0;
                doublesCounter = 0;


                foreach (string value in fileNames)
                {   /*System.Windows.Forms.MessageBox.Show(value.Substring(0, 2)+","+ value.Substring(2, 2)+","+ value.Substring(4, 2));*/
                    string currentFileName = value;

                    //cut .MP4 , .MOV etc
                    int indexMP4 = currentFileName.LastIndexOf(".");
                    currentFileName = currentFileName.Substring(0, indexMP4);

                    //new MatchEntry
                    MatchEntry m = new MatchEntry();

                    int testFormat = currentFileName.Count(f => f == '_');

                    if (testFormat >= 10)
                    {



                        //get Date

                        if (getNextWord(currentFileName).Length == 6)
                            m.DateTime = new DateTime(2000 + Int32.Parse(currentFileName.Substring(0, 2)), Int32.Parse(currentFileName.Substring(2, 2)), Int32.Parse(currentFileName.Substring(4, 2)));
                        currentFileName = cutNextWord(currentFileName);

                        //get Round
                        string round = getLastWord(currentFileName);
                        currentFileName = cutLastWord(currentFileName);
                        try
                        {
                            m.Round = (MatchRound)Enum.Parse(typeof(MatchRound), round);
                        }
                        catch (Exception ex)
                        {
                            //System.Windows.Forms.MessageBox.Show(value +"\n could not be parsed. Wrong ROUND!!" );
                            m.Round = MatchRound.Round;
                            
                        }


                        //get Class if available

                        string disabiltyClass = getLastWord(currentFileName);
                        try
                        {
                            m.DisabilityClass = (DisabilityClass)Enum.Parse(typeof(DisabilityClass), disabiltyClass.Replace("-", "_"));
                            currentFileName = cutLastWord(currentFileName);
                        }
                        catch (Exception ex)
                        {
                            //System.Windows.Forms.MessageBox.Show(value +"\n could not be parsed. Wrong ROUND!!" );
                            m.DisabilityClass = DisabilityClass.NoClass;
                        }

                        

                        //get Category
                        string category = getLastWord(currentFileName);
                        m.Category = (MatchCategory)Enum.Parse(typeof(MatchCategory), category);
                        m.Sex = setSex(m);
                        currentFileName = cutLastWord(currentFileName);

                        //get Year
                        int year = Int32.Parse(getLastWord(currentFileName));
                        m.Year = year;
                        currentFileName = cutLastWord(currentFileName);

                        //get Tournament
                        string tournament = getLastWord(currentFileName);
                        m.Tournament = tournament;
                        currentFileName = cutLastWord(currentFileName);

                        //get players
                        //count "_" first
                        int count = currentFileName.Count(f => f == '_');
                        if (count >= 5 && count < 11)
                        {
                            m.FirstPlayer = new Player();
                            m.SecondPlayer = new Player();

                            //Player 1
                            m.FirstPlayer.Name = getNextWord(currentFileName);
                            currentFileName = cutNextWord(currentFileName);
                            // if more words for family name
                            while (IsAllUpper(getNextWord(currentFileName)))
                            {
                                m.FirstPlayer.Name = m.FirstPlayer.Name + " " + getNextWord(currentFileName);
                                currentFileName = cutNextWord(currentFileName);
                            }
                            m.FirstPlayer.FirstName = getNextWord(currentFileName);
                            currentFileName = cutNextWord(currentFileName);

                            // if more words for first name
                            while (!(IsAllUpper(getNextWord(currentFileName)) && getNextWord(currentFileName).Length == 3))
                            {
                                m.FirstPlayer.FirstName = m.FirstPlayer.FirstName + " " + getNextWord(currentFileName);
                                currentFileName = cutNextWord(currentFileName);
                            }
                            m.FirstPlayer.Nationality = getNextWord(currentFileName);
                            currentFileName = cutNextWord(currentFileName);

                            //Player 2
                            m.SecondPlayer.Name = getNextWord(currentFileName);
                            currentFileName = cutNextWord(currentFileName);
                            // if more words for family name
                            while (IsAllUpper(getNextWord(currentFileName)))
                            {
                                m.SecondPlayer.Name = m.SecondPlayer.Name + " " + getNextWord(currentFileName);
                                currentFileName = cutNextWord(currentFileName);
                            }
                            m.SecondPlayer.FirstName = getNextWord(currentFileName);
                            currentFileName = cutNextWord(currentFileName);
                            // if more words for first name
                            while (!(IsAllUpper(getNextWord(currentFileName)) && currentFileName.Length == 3))
                            {
                                m.SecondPlayer.FirstName = m.SecondPlayer.FirstName + " " + getNextWord(currentFileName);
                                currentFileName = cutNextWord(currentFileName);
                            }
                            m.SecondPlayer.Nationality = getNextWord(currentFileName);
                            currentFileName = cutNextWord(currentFileName);
                            //System.Windows.Forms.MessageBox.Show("Players for match: " + value.ToString());
                            //System.Windows.Forms.MessageBox.Show("Name of P1: " + m.FirstPlayer.Name + "\nFirst Name of P1: " + m.FirstPlayer.FirstName + "\nNationality of P1: " + m.FirstPlayer.Nationality);
                            //System.Windows.Forms.MessageBox.Show("Name of P2: " + m.SecondPlayer.Name + "\nFirst Name of P2: " + m.SecondPlayer.FirstName + "\nNationality of P2: " + m.SecondPlayer.Nationality);
                            singlesCounter++;

                            //add Match Entry to List of all Matches
                            matches.Add(m);
                        }
                        else if (count >= 11)
                        {
                            doublesCounter++;
                        }
                        
                    }
                    //System.Windows.Forms.MessageBox.Show("Singles counter: " + singlesCounter.ToString());
                    //System.Windows.Forms.MessageBox.Show("Doubles counter: " + doublesCounter.ToString());
                    UpdateCounters(singlesCounter, doublesCounter);

                }
            }
        }

        private void ExportSingles_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xlsx)|*.xlsx";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            //saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File To";
            DialogResult result = (System.Windows.Forms.DialogResult)saveFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //System.Windows.Forms.MessageBox.Show("The Save button was clicked or the Enter key was pressed" +
                //                "\nThe file would have been saved as " +
                //                saveFileDialog.FileName);
                ExcelPackage ExcelPkg = new ExcelPackage();
                ExcelWorksheet Singles = ExcelPkg.Workbook.Worksheets.Add("Singles");
                Singles.TabColor = System.Drawing.Color.Blue;
                Singles.Row(1).Style.Font.Bold = true;
                Singles.Column(2).Style.Numberformat.Format = "dd.mm.yyyy";

                Singles.Cells[1, 1].Value = "ID";
                Singles.Cells[1, 2].Value = "Date";
                Singles.Cells[1, 3].Value = "Tournament";
                Singles.Cells[1, 4].Value = "Year";
                Singles.Cells[1, 5].Value = "Category";
                Singles.Cells[1, 6].Value = "Class";
                Singles.Cells[1, 7].Value = "Round";
                Singles.Cells[1, 8].Value = "Sex";
                Singles.Cells[1, 9].Value = "Playtime (gross)";
                Singles.Cells[1, 10].Value = "Surname A";
                Singles.Cells[1, 11].Value = "First Name A";
                Singles.Cells[1, 12].Value = "Country A";
                Singles.Cells[1, 13].Value = "Class A";
                Singles.Cells[1, 14].Value = "Ranking A";
                Singles.Cells[1, 15].Value = "Surname B";
                Singles.Cells[1, 16].Value = "First Name B";
                Singles.Cells[1, 17].Value = "Country B";
                Singles.Cells[1, 18].Value = "Class B";
                Singles.Cells[1, 19].Value = "Ranking B";

                int rowIndex = 2;
                foreach (MatchEntry value in matches)
                {
                    Singles.Cells[rowIndex, 1].Value = "";
                    Singles.Cells[rowIndex, 2].Value = value.DateTime.ToShortDateString();
                    Singles.Cells[rowIndex, 3].Value = value.Tournament;
                    Singles.Cells[rowIndex, 4].Value = value.Year;
                    Singles.Cells[rowIndex, 5].Value = value.Category.ToString();
                    Singles.Cells[rowIndex, 6].Value = value.DisabilityClass.ToString();
                    Singles.Cells[rowIndex, 7].Value = value.Round.ToString();
                    Singles.Cells[rowIndex, 8].Value = value.Sex.ToString();
                    //Singles.Cells[rowIndex, 9].Value = "";
                    Singles.Cells[rowIndex, 10].Value = value.FirstPlayer.Name;
                    Singles.Cells[rowIndex, 11].Value = value.FirstPlayer.FirstName;
                    Singles.Cells[rowIndex, 12].Value = value.FirstPlayer.Nationality;
                    //Singles.Cells[rowIndex, 13].Value = "";
                    //Singles.Cells[rowIndex, 14].Value = value.FirstPlayer.Rank.Position;
                    Singles.Cells[rowIndex, 15].Value = value.SecondPlayer.Name;
                    Singles.Cells[rowIndex, 16].Value = value.SecondPlayer.FirstName;
                    Singles.Cells[rowIndex, 17].Value = value.SecondPlayer.Nationality;
                    //Singles.Cells[rowIndex, 18].Value = "";
                    //Singles.Cells[rowIndex, 19].Value = value.SecondPlayer.Rank.Position;
                    rowIndex++;
                }




                Singles.Protection.IsProtected = false;
                Singles.Protection.AllowSelectLockedCells = false;
                ExcelPkg.SaveAs(new FileInfo(saveFileDialog.FileName));


            }
            //else
            //    System.Windows.Forms.MessageBox.Show("The Cancel button was clicked or Esc was pressed");
        }

        
        private void ClearAllFolders_Click(object sender, RoutedEventArgs e)
        {
            folderNames = Enumerable.Empty<string>();
            matches.Clear();
            singlesCounter = 0;
            doublesCounter = 0;
            UpdateSelectedFolders(folderNames);
            UpdateCounters(singlesCounter, doublesCounter);

        }

        

        private void ClearSelectedFolders_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportDoubles_Click(object sender, RoutedEventArgs e)
        {

        }

        #region Update Methods

        private void UpdateSelectedFolders(IEnumerable<string> sf)
        {
            DisplaySelectedFolders.ItemsSource = sf.ToList();

        }
        private void UpdateCounters(int sc, int dc)
        {
            SinglesCounter.Content = sc;
            DoublesCounter.Content = dc;
        }
        #endregion

        #region Helper Methods
        private string getNextWord(string currentFileName)
        {
            int index = currentFileName.IndexOf("_");
            string nextWord = "";
            if (index > 0)
            {
                nextWord = currentFileName.Substring(0, index);
                return nextWord;
            }
            else
                return currentFileName; ;
        }
        private string cutNextWord(string currentFileName)
        {
            int index = currentFileName.IndexOf("_");
            string remainingFileName = "";
            if (index > 0)
            {
                remainingFileName = currentFileName.Substring(index + 1, (currentFileName.Length - (index + 1)));
            }
            return remainingFileName;
        }

        private string getLastWord(string currentFileName)
        {
            int index = currentFileName.LastIndexOf("_");
            string nextWord = "";
            if (index > 0)
            {
                nextWord = currentFileName.Substring(index + 1, (currentFileName.Length - (index + 1)));
                return nextWord;
            }
            else
                return currentFileName; ;
        }
        public string cutLastWord(string currentFileName)
        {
            int index = currentFileName.LastIndexOf("_");
            string remainingFileName = "";
            if (index > 0)
            {
                remainingFileName = currentFileName.Substring(0, index);
            }
            return remainingFileName;
        }

        private bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }
            return true;
        }

        private MatchSex setSex(MatchEntry m)
        {
            switch (m.Category)
            {
                case MatchCategory.MS:
                    m.Sex = MatchSex.M;
                    break;
                case MatchCategory.MT:
                    m.Sex = MatchSex.M;
                    break;
                case MatchCategory.MD:
                    m.Sex = MatchSex.M;
                    break;
                case MatchCategory.JBS:
                    m.Sex = MatchSex.M;
                    break;
                case MatchCategory.JBT:
                    m.Sex = MatchSex.M;
                    break;
                case MatchCategory.JBD:
                    m.Sex = MatchSex.M;
                    break;
                case MatchCategory.CBS:
                    m.Sex = MatchSex.M;
                    break;
                case MatchCategory.CBT:
                    m.Sex = MatchSex.M;
                    break;
                case MatchCategory.CBD:
                    m.Sex = MatchSex.M;
                    break;
                case MatchCategory.U21BS:
                    m.Sex = MatchSex.M;
                    break;

                case MatchCategory.WS:
                    m.Sex = MatchSex.F;
                    break;
                case MatchCategory.WT:
                    m.Sex = MatchSex.F;
                    break;
                case MatchCategory.WD:
                    m.Sex = MatchSex.F;
                    break;
                case MatchCategory.JGS:
                    m.Sex = MatchSex.F;
                    break;
                case MatchCategory.JGT:
                    m.Sex = MatchSex.F;
                    break;
                case MatchCategory.JGD:
                    m.Sex = MatchSex.F;
                    break;
                case MatchCategory.CGS:
                    m.Sex = MatchSex.F;
                    break;
                case MatchCategory.CGT:
                    m.Sex = MatchSex.F;
                    break;
                case MatchCategory.CGD:
                    m.Sex = MatchSex.F;
                    break;
                case MatchCategory.U21GS:
                    m.Sex = MatchSex.F;
                    break;
                default:
                    m.Sex = MatchSex.Sex;
                    break;
            }
            return m.Sex;
        }

        #endregion

    }
}

