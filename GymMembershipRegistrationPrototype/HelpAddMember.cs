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
    // Help - Add a Member Windows Form
    public partial class HelpAddMember : Form
    {
        // Help screen for Adding a Member
        public HelpAddMember()
        {
            InitializeComponent(); //load window

        } // Help screen for Adding a Member

        // Navigate to Main Menu screen
        private void ToolStripMenuItemBackToMainMenu_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Main Menu window
            new MainMenu().Show();

        } // Navigate to Main Menu screen

        // Navigate to Help screen for Adding a Member
        private void ToolStripMenuItemAddAMemberScreen_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help screen
            new HelpAddMember().Show();

        } // Navigate to Help screen for Adding a Member

        // Exit Application selected in the top menu bar
        private void ApplicationToolStripMenuItemExitApplication_Click(object sender, EventArgs e)
        {
            // Close and exit the whole application
            Application.Exit();

        } // Exit Application selected in the top menu bar

        // Navigate to Add a Member screen
        private void ToolStripMenuItemAddAMember_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Add a Member screen
            new AddAMember().Show();

        } // Navigate to Add a Member screen

    } // end of Help - Add a Member Windows Form
}
