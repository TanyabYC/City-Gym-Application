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
    // Help - Navigation Windows Form
    public partial class HelpNavigation : Form
    {
        // Help screen for the top navigation menu bar
        public HelpNavigation()
        {
            InitializeComponent(); //load window

        } // end of Help screen for the top navigation menu bar

        // Navigate to Main Menu screen
        private void ToolStripMenuItemBackToMainMenu_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Main Menu window
            new MainMenu().Show();

        } // end of Navigate to Main Menu screen

        // Refresh this Help screen for the top navigation menu bar
        private void ToolStripMenuItemNavigation_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help screen
            new HelpNavigation().Show();

        } // end of Refresh this Help screen for the top navigation menu bar

        // Exit Application selected in the top menu bar
        private void ToolStripMenuItemExitApplication_Click(object sender, EventArgs e)
        {
            // Close and exit the whole application
            Application.Exit();

        } // end of Exit Application selected in the top menu bar

    } // end of Help - Navigation Windows Form
}
