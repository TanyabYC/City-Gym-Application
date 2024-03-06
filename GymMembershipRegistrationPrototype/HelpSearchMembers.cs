using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymMembershipRegistrationPrototype
{
    // Help - Search for a Member Windows Form
    public partial class HelpSearchMembers : Form
    {
        // Help screen for Search for a Member
        public HelpSearchMembers()
        {
            InitializeComponent(); //load window

        } // end of Help screen for Search for a Member

        // Navigate to Main Menu screen
        private void ToolStripMenuItemBackToMainMenu_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Main Menu window
            new MainMenu().Show();

        } // end of Navigate to Main Menu screen

        // Refresh this Help screen for Searching for a Member
        private void ToolStripMenuItemSearchForAMemberScreen_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help screen
            new HelpSearchMembers().Show();

        } // end of Refresh this Help screen for Searching for a Member

        // Exit Application selected in the top menu bar
        private void ToolStripMenuItemExitApplication_Click(object sender, EventArgs e)
        {
            // Close and exit the whole application
            Application.Exit();

        } // end of Exit Application selected in the top menu bar

        // Navigate to Search for a Member screen
        private void searchForAMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Search for a Member screen
            new SearchMembers().Show();

        } // end of Navigate to Search for a Member screen

    } // end of Help - Search for a Member Windows Form
}
