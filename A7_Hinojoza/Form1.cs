using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A7_Hinojoza
{
    /*Jose Hinojoza
     * A7
     * Program uses a provided database let the user see key information about American Baseball teams
    */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   //When the program loads the last tab shows the best teams per baseball conference

            //This line of code loads data into the 'mlB_ResultsDataSet.Results' table. You can move, or remove it, as needed.
            this.resultsTableAdapter.Fill(this.mlB_ResultsDataSet.Results);
            List<string> divisionList = new List<string>();
            


            txtbestDivisionTeams.Text += "Best Teams by Division\r\n";
            txtbestDivisionTeams.Text += "======================\r\n";
            foreach (DataRowView row in bindingSource)
            {
                if (!divisionList.Contains(((string)row["Division"])))
                {
                    divisionList.Add(((string)row["Division"]));
                }

            }

            foreach (String division in divisionList)
            {
                String bestDivisionTeam = "";
                int divisionTeamWins = 0;
                foreach (DataRowView row in bindingSource)
                {
                    if (((string)row["Division"]).Equals(division))
                    {
                        if ((int)(row["Wins"]) > divisionTeamWins)
                        {
                            divisionTeamWins = ((int)row["Wins"]);
                            bestDivisionTeam = ((string)row["Team"]);
                        }
                    }
                }
                txtbestDivisionTeams.Text += String.Format("{0, -30} {1, -20}\r\n", division, bestDivisionTeam);
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            //when the user clicks this button the teams containing the text in the txtBox get added to bigg txtBox with their record
            //The average wins for the teams are also calculated at bottom
            int found = 0;
            int totalWins = 0, totalTeams = 0;
            double averageWins;
            txtSearchResults.Text = "";
            txtSearchResults.Text += String.Format("{0, -30} {1, -20}\r\n", "Team", "Record(W-L)");
            txtSearchResults.Text += String.Format("{0, -30} {1, -20}\r\n", "====", "===========");
            foreach (DataRowView row in bindingSource)
            {
                if (((string)row["Team"]).ToUpper().Contains(txtSearch.Text.ToUpper()))
                {
                    String WinLoss = (row["Wins"]) + "-" + (row["Losses"]);
                    txtSearchResults.Text += String.Format("{0, -30} {1, -20}\r\n", ((string)row["Team"]), WinLoss);
                    totalWins += (int)(row["Wins"]);
                    totalTeams++;
                    found = 1;
                }
            }
            if (found == 0)
            {
                txtSearchResults.Text += "No Results Found\r\n";

            }
            averageWins = (double)totalWins / totalTeams;
            txtSearchResults.Text += "==========================================\r\n";
            txtSearchResults.Text += "Average wins: " + averageWins.ToString(".0");
        }
    }
}
