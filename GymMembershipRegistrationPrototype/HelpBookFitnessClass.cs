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
    // Help - Book a Fitness Class Windows Form
    public partial class HelpBookFitnessClass : Form
    {
        // Help screen for Book a Fitness Class
        public HelpBookFitnessClass()
        {
            InitializeComponent(); //load window

        } // end of Help screen for Book a Fitness Class

        // Navigate to Main Menu screen
        private void ToolStripMenuItemBackToMainMenu_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Main Menu window
            new MainMenu().Show();

        } // end of Navigate to Main Menu screen

        // Navigate to Help screen for Booking a Fitness Class
        private void ToolStripMenuItemBookAFitnessClassScreen_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help screen
            new HelpBookFitnessClass().Show();

        } // end of Navigate to Help screen for Booking a Fitness Class

        // Exit Application selected in the top menu bar
        private void ToolStripMenuItemExitApplication_Click(object sender, EventArgs e)
        {
            // Close and exit the whole application
            Application.Exit();

        } // end of Exit Application selected in the top menu bar

        // Navigate to Book a Fitness Class screen
        private void ToolStripMenuItemBookAFitnessClass_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Book a Fitness Class screen
            new BookFitnessClass().Show();

        } // end of Navigate to Book a Fitness Class screen

    } // end of Help - Book a Fitness Class Windows Form
}
