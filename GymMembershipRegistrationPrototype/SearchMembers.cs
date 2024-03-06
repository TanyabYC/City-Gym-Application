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
    // Search for a Member Windows Form
    public partial class SearchMembers : Form
    {
        // Search for a Member screen
        public SearchMembers()
        {
            InitializeComponent(); //load window

        } // end of search for a member screen

        private void MemberBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.memberBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cityGymDataSet);

        }

        // Search for a Member screen
        private void SearchMembers_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cityGymDataSet.MemberMembershipDetails' table. You can move, or remove it, as needed.
            this.memberMembershipDetailsTableAdapter.Fill(this.cityGymDataSet.MemberMembershipDetails);
            // TODO: This line of code loads data into the 'cityGymDataSet.Member' table. You can move, or remove it, as needed.
            this.memberTableAdapter.Fill(this.cityGymDataSet.Member);

        } // end of Search for a Member screen

        // Navigate to Main Menu screen
        private void ToolStripMenuItemMainMenu_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Main Menu window
            new MainMenu().Show();

        } // end of Main Menu screen

        // Exit Application selected in the top menu bar
        private void ToolStripMenuItemExitApplication_Click(object sender, EventArgs e)
        {
            // Close and exit the whole application
            Application.Exit();

        } // end of exit Application selected in the top menu bar

        // Navigate to Help screen for Searching for a Member
        private void ToolStripMenuItemSearchForAMemberScreen_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help screen
            new HelpSearchMembers().Show();

        } // end of navigate to Help screen for Searching for a Member

        // Search for a member - occurs when search button is clicked
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            // Store search entered by user in a variable
            string searchFirstName = textBoxSearchFirstName.Text;

            // Store search selected by user
            bool searchBasic = radioButtonBasic.Checked; //basic membership type selection
            bool searchRegular = radioButtonRegular.Checked; //regular membership type selection
            bool searchPremium = radioButtonPremium.Checked; //premium membership type selection

            // Error control - check if user entered search criteria to search on
                // First name not entered
            if ((searchFirstName == "") && 
                // Basic membership type not selected
                (searchBasic == false) && 
                // Regular membership type not selected
                (searchRegular == false) && 
                // Premium membership type not selected
                (searchPremium == false))
            {
                // Display error message in a pop up window
                MessageBox.Show("Please enter search criteria.");

                // Exit this function, because no search criteria was entered
                return;
            } // end of if statement - error control

            // User entered search criteria
            else
            {
                // Create a data view for the member membership details table
                DataView memberDV;

                // Search on member first name
                if (searchFirstName != "")
                {
                    // Create a dataview for the member membership details table
                    memberDV = new DataView(cityGymDataSet.MemberMembershipDetails);

                    // Create filter string for member membership details table
                    string filter;

                    // Filter first name
                    filter = "[FirstName] LIKE '*" + searchFirstName + "*'";

                    // Add search on basic membership
                    if (searchBasic)
                    {
                        // Filter basic membership
                        filter += " AND [Description] LIKE 'Basic'";

                    } // end of if statement - add search on basic membership

                    // Add search on regular membership
                    else if (searchRegular)
                    {
                        // Filter regular membership
                        filter += " AND [Description] LIKE 'Regular'";

                    } // end of else if statement - add search on regular membership

                    // Add search on premium membership
                    else if (searchPremium)
                    {
                        // Filter premium membership
                        filter += " AND [Description] LIKE 'Premium'";

                    } // end of else if statement - add search on premium membership

                    // Apply filter
                    memberDV.RowFilter = filter;

                    // Apply sort order
                    memberDV.Sort = "[FirstName]";

                    // Show this data view in the data grid view of member membership details
                    memberMembershipDetailsDataGridView.DataSource = memberDV;

                    // Data grid view is empty
                    // Member was not found
                    if (memberMembershipDetailsDataGridView.RowCount == 0)
                    {
                        // Display error message in a pop up window
                        MessageBox.Show("The member you searched for was not found. Please try again.");

                        // Exit this function, because no such member found
                        return;

                    } // end of if statement - data grid view is empty | member was not found

                } // end of if statement - search on member first name

                // Search on basic membership
                else if (searchBasic)
                {
                    // Create a dataview for the member membership details table
                    memberDV = new DataView(cityGymDataSet.MemberMembershipDetails,
                        // Filter first name column
                        "[Description] LIKE 'Basic'",
                        // Sort first name column
                        "[Description]",
                        // view current rows
                        DataViewRowState.CurrentRows);

                    // Show this data view in the data grid view of member membership details
                    memberMembershipDetailsDataGridView.DataSource = memberDV;

                } // end of else if statement - search on basic membership

                // Search on regular membership
                else if (searchRegular)
                {
                    // Create a dataview for the member membership details table
                    memberDV = new DataView(cityGymDataSet.MemberMembershipDetails,
                        // Filter first name column
                        "[Description] LIKE 'Regular'",
                        // Sort first name column
                        "[Description]",
                        // view current rows
                        DataViewRowState.CurrentRows);

                    // Show this data view in the data grid view of member membership details
                    memberMembershipDetailsDataGridView.DataSource = memberDV;

                } // end of else if statement - search on regular membership

                // Search on premium membership
                else if (searchPremium)
                {
                    // Create a dataview for the member membership details table
                    memberDV = new DataView(cityGymDataSet.MemberMembershipDetails,
                        // Filter first name column
                        "[Description] LIKE 'Premium'",
                        // Sort first name column
                        "[Description]",
                        // view current rows
                        DataViewRowState.CurrentRows);

                    // Show this data view in the data grid view of member membership details
                    memberMembershipDetailsDataGridView.DataSource = memberDV;

                } // end of else if statement - search on premium membership

            } // end of else statement - user entered search criteria

        } // end of search for a member

        // Clear search for a member - occurs when clear button is clicked
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            // Show full data set in the data grid view of member membership details
            memberMembershipDetailsDataGridView.DataSource = cityGymDataSet.MemberMembershipDetails;

            // Clear search text box for member first name
            textBoxSearchFirstName.Text = "";

            // Clear selections made for membership type
            radioButtonBasic.Checked = false; // reset basic
            radioButtonRegular.Checked = false; // reset regular
            radioButtonPremium.Checked = false; // reset premium

        } // end of clear search for a member

    } // end of Search for a Members Windows Form
}
