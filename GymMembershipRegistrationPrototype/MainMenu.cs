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
    // Main Menu Windows Form
    public partial class MainMenu : Form
    {
        // Main Menu screen
        public MainMenu()
        {
            InitializeComponent(); //load window

        } // end of Main Menu screen

        // Navigate to Main Menu screen
        private void ToolStripMenuItemMainMenu_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Main Menu window
            new MainMenu().Show();

        } // end of Navigate to Main Menu screen

        // Search for a Member screen
        private void ButtonMemberSearch_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Members window
            new SearchMembers().Show();

        } // end of Search for a Member screen

        // Add a Member screen
        private void ButtonAddMember_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Membership Registration window
            new AddAMember().Show();

        } // end of Add a Member screen

        // Book a Fitness Class screen
        private void ButtonBookFitnessClass_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Fitness Classes window
            new BookFitnessClass().Show();

        } // end of Book a Fitness Class screen

        // Exit Application selected in the top menu bar
        private void ToolStripMenuItemExitApplication_Click(object sender, EventArgs e)
        {
            // Close and exit the whole application
            Application.Exit();

        } // end of Exit Application selected in the top menu bar

        // Navigate to Help screen for Main Menu
        private void ToolStripMenuItemMainMenuScreen_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help screen
            new HelpMainMenu().Show();

        } // end of Navigate to Help screen for Main Menu

        // Navigate to Help screen for the top navigation menu bar
        private void ToolStripMenuItemNavigation_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help screen
            new HelpNavigation().Show();

        } // end of Navigate to Help screen for the top navigation menu bar

    } // end of Main Menu Windows Form
}
