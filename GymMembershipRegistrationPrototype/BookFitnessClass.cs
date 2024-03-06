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
    // Book a Fitness Class Windows Form
    public partial class BookFitnessClass : Form
    {
        // Book a Fitness Class screen
        public BookFitnessClass()
        {
            InitializeComponent(); //load window

        } // end of Book a Fitness Class screen

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

        // Navigate to Help screen for Booking a Fitness Class
        private void ToolStripMenuItemBookAFitnessClassScreen_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help screen
            new HelpBookFitnessClass().Show();

        } // end of Navigate to Help screen for Booking a Fitness Class

    } // end of Book a Fitness Class Windows Form
}
