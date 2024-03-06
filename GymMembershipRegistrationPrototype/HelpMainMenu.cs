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
    // Help - Main Menu Windows Form
    public partial class HelpMainMenu : Form
    {
        // Help screen for Main Menu
        public HelpMainMenu()
        {
            InitializeComponent(); //load window

        } // end of Help screen for Main Menu

        // Navigate to Main Menu screen
        private void ToolStripMenuItemMainMenu_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Main Menu window
            new MainMenu().Show();

        } // end of Navigate to Main Menu screen

        // Exit Application selected in the top menu bar
        private void ToolStripMenuItemExitApplication_Click(object sender, EventArgs e)
        {
            // Close and exit the whole application
            Application.Exit();

        } // end of Exit Application selected in the top menu bar

        // Refresh this Help screen for Main Menu
        private void ToolStripMenuItemMainMenuScreen_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help window
            new HelpMainMenu().Show();

        } // end of Refresh this Help screen for Main Menu

    } // end of Help - Main Menu Windows Form
}
