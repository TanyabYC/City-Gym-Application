using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace GymMembershipRegistrationPrototype
{
    // Add a Member Windows Form
    public partial class AddAMember : Form
    {
        // Add a Member screen
        public AddAMember()
        {
            InitializeComponent(); //load window

        } // end of add a member screen

        // Calculate Membership Cost - occurs when button is clicked on membership registration form
        private void ButtonCalculateMembership_Click(object sender, EventArgs e)
        {
            // Error control - default color values
            labelRequiredSelectionsToCalculateYourMembershipCost.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelSelectYourPreferredMembershipType.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelSelectYourPreferredContractDuration.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelSelectHowYouWouldLikeToPay.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelIWouldLikeToPayWithDirectDebit.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelPreferredPaymentFrequency.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelRequiredMembershipTypes.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelRequiredContractDuration.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelRequiredPaymentOptions.ForeColor = Color.DarkViolet; //reset color of label to default color

            // Membership Type - variables
            bool checkBasic = radioButtonBasic_Membership.Checked; //checks whether basic membership type is selected
            bool checkRegular = radioButtonRegular_Membership.Checked; //checks whether regular membership type is selected
            bool checkPremium = radioButtonPremium_Membership.Checked; //checks whether premium membership type is selected

            // Contract Duration - variables
            bool check3Months = radioButton3Months_ContractDuration.Checked; //checks whether 3 months contract duration is selected
            bool check12Months = radioButton12Months_ContractDuration.Checked; //checks whether 12 months contract duration is selected
            bool check24Months = radioButton24Months_ContractDuration.Checked; //checks whether 24 months contract duration is selected

            // Payment Options - direct debit - variables
            bool checkPmtDD = radioButtonYes_DirectDebit.Checked; //checks whether direct debit option is selected
            bool checkPmtNoDD = radioButtonNo_DirectDebit.Checked; //checks whether no direct debit option is selected

            // Payment Options - payment frequency - variables
            bool checkPmtWeekly = radioButtonWeekly_PaymentFrequency.Checked; //checks whether weekly payment is selected
            bool checkPmtMonthly = radioButtonMonthly_PaymentFrequency.Checked; //checks whether monthly payment is selected

            // Selections required to calculate membership cost
                                                // membership type
            bool requiredMemCostSelections =    (checkBasic || checkRegular || checkPremium) && 
                                                // contract duration
                                                (check3Months || check12Months || check24Months) &&
                                                // payment option - direct debit
                                                (checkPmtDD || checkPmtNoDD) &&
                                                // payment option - payment frequency
                                                (checkPmtWeekly || checkPmtMonthly);

            // Selections not complete
            if (!requiredMemCostSelections)
            {
                // method to help user complete options to calculate membership cost
                CheckCalculationErrors(); //returns when form is completed with no data type
            } // end of selections not complete

            // Selections complete
            else
            {
                // Membership Cost - variables
                double baseMembershipCost = 0; //define base membership cost
                string baseMembershipChoice; //define base membership chosen

                //  Membership Type - selections
                // Basic
                if (checkBasic)
                {
                    // Base Membership Cost
                    baseMembershipCost = 10.00D; //assign value to base membership cost
                    textBoxBaseMembershipCost_Amount.Text = String.Format("${0:0.00}", baseMembershipCost); //display value for regular membership

                    // Chosen Membership Type
                    baseMembershipChoice = "Basic membership"; //chosen membership type notification
                    labelBaseMembership_Choice.Text = baseMembershipChoice; //display chosen membership type

                } // end of basic

                // Regular
                else if (checkRegular)
                {
                    // Base Membership Cost
                    baseMembershipCost = 15.00D; //assign value to base membership cost
                    textBoxBaseMembershipCost_Amount.Text = String.Format("${0:0.00}", baseMembershipCost); //display value for regular membership

                    // Chosen Membership Type
                    baseMembershipChoice = "Regular membership"; //chosen membership type notification
                    labelBaseMembership_Choice.Text = baseMembershipChoice; //display chosen membership type

                } // end of regular
                
                // Premium
                else if (checkPremium)
                {
                    // Base Membership Cost
                    baseMembershipCost = 20.00D; //value to base membership cost notification
                    textBoxBaseMembershipCost_Amount.Text = String.Format("${0:0.00}", baseMembershipCost); //display value for regular membership

                    // Chosen Membership Type
                    baseMembershipChoice = "Premium membership"; //assign chosen membership type
                    labelBaseMembership_Choice.Text = baseMembershipChoice; //display chosen membership type

                } // end of premium
                // end of membership type - selections

                // Discount amounts
                double totalDiscount = 0D; //define initial zero discount amount
                int discount12Months = 2; //define discount amount
                int discount24Months = 5; //define discount amount
                double discountDD = baseMembershipCost * 0.01; //define discount on base membership cost

                // Discount choices
                string choice12Months = "12 months"; //define chosen contract duration
                string choice24Months = "24 months"; //define chosen contract duration
                string choiceDD = " | Direct debit"; //define chosen direct debit
                string choiceDD_only = "Direct debit only"; //define only direct debit chosen
                string choiceNone = "No discounts selected"; //define no discount chosen
                labelDiscount_Choice.Text = ""; //initiate textbox that displays discount choice

                // Discount totals calculation
                // Direct debit - yes
                if (checkPmtDD)
                {
                    totalDiscount += discountDD; //add direct debit discount

                    // 12 months contract
                    if (check12Months)
                    {
                        totalDiscount += discount12Months; //add 12 months discount
                        labelDiscount_Choice.Text = choice12Months + choiceDD; //display choices
                    }
                    // 24 months contract
                    if (check24Months)
                    {
                        totalDiscount += discount24Months; //add 24 months discount
                        labelDiscount_Choice.Text = choice24Months + choiceDD; //display choices
                    }
                    // Direct debit - only
                    if (check3Months)
                    {
                        labelDiscount_Choice.Text += choiceDD_only; //display choice
                    }
                } // end of direct debit - yes

                // Direct debit - no
                else if (checkPmtNoDD)
                {
                    // 12 months contract
                    if (check12Months)
                    {
                        totalDiscount += discount12Months; //add 12 months discount
                        labelDiscount_Choice.Text = choice12Months; //display choice
                    }
                    // 24 months contract
                    else if (check24Months)
                    {
                        totalDiscount += discount24Months; //add 24 months discount
                        labelDiscount_Choice.Text = choice24Months; //display choice
                    }
                    // 3 months contract (no discount)
                    else
                    {
                        labelDiscount_Choice.Text += choiceNone; //display choice
                    }                        
                } // end of direct debit - no
                // end of discount totals calculation

                // Discount total
                textBoxTotalDiscount_Amount.Text = String.Format("-${0:0.00}", totalDiscount); //display total discount amount

                // Value-Added Extras Amounts
                double add24_7Access = 1.00D; //define selected extra cost
                double addPersonalTrainer = 20.00D; //define selected extra cost
                double addDietConsultation = 20.00D; //define selected extra cost
                double addOnlineFitnessVideoAccess = 2.00D; //define selected extra cost
                double valueAddedExtrasCost = 0.00D; //define value-added extras cost variable

                // Value-Added Extras - selections
                bool check24_7 = checkBox24_7Access_Extra.Checked; //checks whether 24/7 access is selected
                bool checkPersonalTrainer = checkBoxPersonalTrainer_Extra.Checked; //checks whether personal trainer is selected
                bool checkDietConsultation = checkBoxDietConsultation_Extra.Checked; //checks whether diet consultation is selected
                bool checkAccessOnlineFitnessVideos = checkBoxAccessOnlineFitnessVideos_Extra.Checked; //checks whether access online fitness videos is selected

                // Value-Added Extras - calculations
                // 24/7 Access
                if (check24_7)
                {
                    valueAddedExtrasCost += add24_7Access; //add to cost of extras
                } // end of 24/7 access

                // Personal Trainer
                if (checkPersonalTrainer)
                {
                    valueAddedExtrasCost += addPersonalTrainer; //add to cost of extras
                } // end of personal trainer

                // Diet Consultation
                if (checkDietConsultation)
                {
                    valueAddedExtrasCost += addDietConsultation; //add to cost of extras
                } // end of diet consultation

                // Access Online Fitness Video Access
                if (checkAccessOnlineFitnessVideos)
                {
                    valueAddedExtrasCost += addOnlineFitnessVideoAccess; //add to cost of extras
                } // end of access online fitness videos
                // end of value-added extras - calculations

                // Value-Added Extras - cost
                textBoxValueAddedExtras_Amount.Text = String.Format("${0:0.00}", valueAddedExtrasCost); //display cost with correct format

                // Value-Added Extras notifications - variables
                string extraAll = "Four extras selected"; //define all extras
                string extraThree = "Three extras selected"; //define 3 extras
                string extraTwo = "Two extras selected"; //define 2 extras
                string extra24_7Access = "24/7 Access"; //define 1 extra - 24/7 access
                string extraPersonalTrainer = "Personal trainer"; //define 1 extra - personal trainer
                string extraDietConsultation = "Diet consultation"; //define 1 extra - diet consultation
                string extraAccessOnlineFitnessVideos = "Access online fitness videos"; //define 1 extra - access online fitness videos
                string extraNone = "No extras selected"; //define no extras
                string chosenExtras = ""; //define initial value for extras selected

                // Switch statement index
                int extras = 0; //define index

                // Value-Added Extras - selections
                // 1 extra selected
                    // 24/7 access; or
                if (check24_7 || 
                    // personal trainer; or
                    checkPersonalTrainer ||
                    // diet consultation; or
                    checkDietConsultation ||
                    // access online fitness videos; or
                    checkAccessOnlineFitnessVideos) 
                {
                    extras = 1; //1 extra selected; go to switch, case 1
                }

                // 2 extras selected
                    // 24/7 access; personal trainer
                if ((check24_7 && checkPersonalTrainer) ||
                    // 24/7 access;  diet consultation
                    (check24_7 && checkDietConsultation) ||
                    // 24/7 access;  access online fitness videos
                    (check24_7 && checkAccessOnlineFitnessVideos) ||
                    // personal trainer;  access online fitness videos
                    (checkPersonalTrainer && checkDietConsultation) ||
                    // personal trainer;  access online fitness videos
                    (checkPersonalTrainer && checkAccessOnlineFitnessVideos) ||
                    // diet consultation;  access online fitness videos
                    (checkDietConsultation && checkAccessOnlineFitnessVideos)) 
                {
                    extras = 2; //2 extras selected; go to switch, case 2
                }

                // 3 extras selected
                    // 24/7 access; personal trainer; diet consultation
                if ((check24_7 && checkPersonalTrainer && checkDietConsultation) ||
                    // 24/7 access; personal trainer; access online fitness videos
                    (check24_7 && checkPersonalTrainer && checkAccessOnlineFitnessVideos) ||
                    // 24/7 access; diet consultation; access online fitness videos
                    (check24_7 && checkDietConsultation && checkAccessOnlineFitnessVideos) ||
                    // personal trainer; diet consultation; access online fitness videos
                    (checkPersonalTrainer && checkDietConsultation && checkAccessOnlineFitnessVideos))
                {
                    extras = 3; //3 extras selected; go to switch, case 3
                }

                // 4 (all) extras selected
                    // 24/7 access; and
                if (check24_7 &&
                    // personal trainer; and
                    checkPersonalTrainer &&
                    // diet consultation; and
                    checkDietConsultation &&
                    // access online fitness videos
                    checkAccessOnlineFitnessVideos)
                {
                    extras = 4; //4 extras selected; go to switch, case 4
                }

                // Switch through selections
                switch (extras)
                {
                    // 1 value-added extra selected
                    case 1:
                        {
                            // 24/7 Access
                            if (check24_7)
                            {
                                // Extras message next to value-added extras amount
                                chosenExtras = extra24_7Access; //choice
                            }
                            // Personal Trainer
                            if (checkPersonalTrainer)
                            {
                                // Extras message next to value-added extras amount
                                chosenExtras = extraPersonalTrainer; //choice
                            }
                            // Diet Consultation
                            if (checkDietConsultation)
                            {
                                // Extras message next to value-added extras amount
                                chosenExtras = extraDietConsultation; //choice
                            }
                            // Access Online Fitness Videos
                            if (checkAccessOnlineFitnessVideos)
                            {
                                // Extras message next to value-added extras amount
                                chosenExtras = extraAccessOnlineFitnessVideos; //choice
                            }

                            break;
                        } // end of one extra selected

                    // 2 value-added extras selected
                    case 2:
                        {
                            // Extras message next to value-added extras amount
                            chosenExtras = extraTwo; //define choice

                            break;
                        } // end of two extras selected

                    // 3 value-added extras selected
                    case 3:
                        {
                            // Extras message next to value-added extras amount
                            chosenExtras = extraThree; //define choice

                            break;
                        } // end of three extras selected

                    // 4 (all) value-added extras selected
                    case 4:
                        {
                            // Extras message next to value-added extras amount
                            chosenExtras = extraAll; //define choice

                            break;
                        } // end of four extras selected

                    // No value-added extras selected
                    default:
                        {
                            // Extras message next to value-added extras amount
                            chosenExtras = extraNone; //define choice

                            break;
                        } // end of no value-added extras selected

                } // end of value-added extras

                // Value Addes Extras - choices
                labelValueAddedExtras_Choice.Text = chosenExtras; //display choices

                // Net Membership Cost - calculation
                double netMembershipCost = baseMembershipCost - totalDiscount + valueAddedExtrasCost; //calculate net membership cost
                textBoxNetMembershipCost_Amount.Text = String.Format("${0:0.00}", netMembershipCost); //display net membership cost

                // Regular Payment Amount - selections
                // Weekly payment
                if (checkPmtWeekly)
                {
                    string weeklyPmtAmt = String.Format("${0:0.00}", netMembershipCost); //format weekly payment amount (same as net membership cost)
                    
                    // Display regular payment
                    labelYourRegularPayment.Text = "Your regular weekly payment amount will be...";
                    labelRegularPmtAmt.Text = weeklyPmtAmt; //weekly regular payment amount
                    labelYourRegularPayment.Visible = true; //display start of sentance
                    labelRegularPmtAmt.Visible = true; //display weekly regular payment amount

                } // end of weekly payment

                // Monthly payment
                else
                {
                    double paymentCalculation = netMembershipCost * 52 / 12; //calculate monthly payment
                    string monthlyPmtAmt = String.Format("${0:0.00}", paymentCalculation); //format monthly payment

                    // Display regular payment
                    labelYourRegularPayment.Text = "Your regular monthly payment amount will be...";
                    labelRegularPmtAmt.Text = monthlyPmtAmt; //monthly regular payment amount
                    labelYourRegularPayment.Visible = true; //display start of sentance
                    labelRegularPmtAmt.Visible = true; //display monthly regular payment amount

                } // end of monthly payment
                // end of regular payment amount

                // Submit membership - message
                labelSubmitMsg.ForeColor = Color.Red; //change color of message
                string submitMessage = "Click 'SUBMIT' to register your membership"; //define message
                labelSubmitMsg.Text = submitMessage; //show message

            } // end of selections complete

        } // end of Calculate Membership button clicked
        
        // Method to check errors when calculating membership
        private void CheckCalculationErrors()
        {
            // Label - ***  See required selections to calculate your membership cost
            labelRequiredSelectionsToCalculateYourMembershipCost.ForeColor = Color.Red; //change color of label

            // loop to check selections made
            for (int i = 0; i < 5; i++)
            {
                // Membership Type - variables
                bool checkBasic = radioButtonBasic_Membership.Checked; //checks whether basic membership type is selected
                bool checkRegular = radioButtonRegular_Membership.Checked; //checks whether regular membership type is selected
                bool checkPremium = radioButtonPremium_Membership.Checked; //checks whether premium membership type is selected

                // Contract Duration - variables
                bool check3Months = radioButton3Months_ContractDuration.Checked; //checks whether 3 months contract duration is selected
                bool check12Months = radioButton12Months_ContractDuration.Checked; //checks whether 12 months contract duration is selected
                bool check24Months = radioButton24Months_ContractDuration.Checked; //checks whether 24 months contract duration is selected

                // Payment Options - direct debit - variables
                bool checkPmtDD = radioButtonYes_DirectDebit.Checked; //checks whether direct debit option is selected
                bool checkPmtNoDD = radioButtonNo_DirectDebit.Checked; //checks whether no direct debit option is selected

                // Payment Options - payment frequency - variables
                bool checkPmtWeekly = radioButtonWeekly_PaymentFrequency.Checked; //checks whether weekly payment is selected
                bool checkPmtMonthly = radioButtonMonthly_PaymentFrequency.Checked; //checks whether monthly payment is selected

                // Switch statement index
                int selectionsMade = 0; //define index

                // Membership types - selections
                if ((checkBasic || checkRegular || checkPremium ) == false)
                {
                    selectionsMade = 1; //go to switch statement case 1
                } // end of membership types

                // Contract duration - selections
                else if ((check3Months || check12Months || check24Months) == false)
                {
                    selectionsMade = 2; //go to switch statement case 2
                } // end of contract duration

                // Payment options - direct debit - selections
                else if ((checkPmtDD || checkPmtNoDD) == false)
                {
                    selectionsMade = 3; //go to switch statement case 3
                } // end of direct debit payment

                // Payment options - payment frequency - selections
                else if ((checkPmtWeekly || checkPmtMonthly) == false)
                {
                    selectionsMade = 4; //go to swithc statement case 4
                } // end of payment frequency

                // Switch through selections made to calculate membership cost
                switch (selectionsMade)
                {
                    // Membership types
                    case 1:
                        {
                            // *** label underneath Membership Types
                            labelRequiredMembershipTypes.ForeColor = Color.Red; //reset color of label to default color

                            // Select your preferred membership type - label
                            labelSelectYourPreferredMembershipType.ForeColor = Color.Red; //change color of label

                            break;
                        } // end of membership types

                    // Contract duration
                    case 2:
                        {
                            // *** label underneath Contract Duration
                            labelRequiredContractDuration.ForeColor = Color.Red; //reset color of label to default color

                            // Select your preferred contract duration - label
                            labelSelectYourPreferredContractDuration.ForeColor = Color.Red; //change color of label

                            break;
                        } // end of contract duration

                    // Payment option - direct debit
                    case 3:
                        {
                            // *** label underneath Payment Options
                            labelRequiredPaymentOptions.ForeColor = Color.Red; //reset color of label to default color

                            // Select how you would like to pay - label
                            labelSelectHowYouWouldLikeToPay.ForeColor = Color.Red; //change color of label

                            // Would you like to pay with direct debit? - label
                            labelIWouldLikeToPayWithDirectDebit.ForeColor = Color.Red; //change color of label

                            break;
                        } // end of payment option - direct debit

                    // Payment option - payment frequency
                    case 4:
                        {
                            // *** label underneath Payment Options
                            labelRequiredPaymentOptions.ForeColor = Color.Red; //reset color of label to default color

                            // Select how you would like to pay - label
                            labelSelectHowYouWouldLikeToPay.ForeColor = Color.Red; //change color of label

                            // Your preferred payment frequency? - label
                            labelPreferredPaymentFrequency.ForeColor = Color.Red; //change color of label

                            break;
                        } // end of payment option - payment frequency

                    // All required selections made
                    default:
                        {
                            // Form complete - message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            string submitMessage = "Click 'SUBMIT' to register your membership."; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            break;
                        } // end of all required selections made

                } // end of switch through selections made to calculate membership cost

            } // end of loop to check selections made

        } // end of method CheckCalculationErrors()

        // Submit Form - occurs when button is clicked on membership registration form
        private void ButtonSubmit_Registration_Click(object sender, EventArgs e)
        {
            const string blank = ""; //blank string

            // Create a variable to check whether the user completed the form correctly
            bool complete = false;

            // method to help user complete form
            CompleteForm(complete); //returns when form is completed with no data type                     

            // Form was correctly completed by the user
            if (CompleteForm(true))
            {
                // If form was completed correctly, City Gym has received consent to process the new member's contact information
                // No need to check this further

                // Create a connection to the City Gym database
                var connectionString = ConfigurationManager.ConnectionStrings["CityGymConnectionString"].ConnectionString;

                // Customer Details - variables
                string firstName = textBoxFirstName.Text; //store first name into a variable
                string lastName = textBoxLastName.Text; //store last name into a variable
                string mobileNo = textBoxMobileNumber.Text; //store mobile number into a variable
                string address1 = textBoxAddress1.Text; //store address 1 into a variable
                string address2Optional = textBoxAddress2Optional.Text; //define address 2 variable
                string suburbOrCity = textBoxSuburbOrCity.Text; //store suburb or city into a variable
                string region = comboBoxRegion.Text; //store region into a variable
                string postalCode = textBoxPostalCode.Text; //store postal code into a variable
                
                // Create a string to hold the address
                string fullAddress = address1 + ", ";

                bool address2NotCompleted = address2Optional.Contains("Your street address..."); //returns true if address 2 is not completed

                // Address 2 (optional) completed and is not blank
                if (address2NotCompleted == false && !(address2Optional == blank))
                {
                    // Add address 2 (optional
                    fullAddress += address2Optional + ", ";

                } // end of address 2 (optional) completed and is not blank

                // Add suburb, region and postal code to address
                fullAddress += suburbOrCity + ", " + region + " " + postalCode;

                // Payment Frequency - variables
                bool checkPmtWeekly = radioButtonWeekly_PaymentFrequency.Checked; //checks whether weekly payment option is selected
                bool checkPmtMonthly = radioButtonMonthly_PaymentFrequency.Checked; //checks whether monthly payment option is selected
                string weekly = "Weekly"; //define weekly payment
                string monthly = "Monthly"; //define monthly payment
                string pmtFrequency = ""; //create variable to hold payment frequency

                // Payment Frequency - Weekly
                if (checkPmtWeekly)
                {
                    // Define payment frequency
                    pmtFrequency = weekly;

                } // end of payment frequency weekly

                // Payment Frequency - Monthly
                else if (checkPmtMonthly)
                {
                    // Define payment frequency
                    pmtFrequency = monthly;

                } // end of payment frequency - monthly

                // Create a variable to hold membership expiry date
                string membershipExpiryDate = "";

                // Contract Duration - selections
                // 3 months
                if (radioButton3Months_ContractDuration.Checked)
                {
                    // Calculate Membership Expiry Date
                    System.DateTime contract3Month = DateTime.Today.AddMonths(3); //add 3 months to today's date

                    // Define membership expiry date
                    membershipExpiryDate = contract3Month.ToString();

                } // end of 3 months

                // 12 months
                else if (radioButton12Months_ContractDuration.Checked)
                {
                    // Calculate Membership Expiry Date
                    System.DateTime contract12Month = DateTime.Today.AddYears(1); //add 1 year (12 months) to today's date

                    // Define membership expiry date
                    membershipExpiryDate = contract12Month.ToString();

                } // end of 12 months

                // 24 months
                else if (radioButton24Months_ContractDuration.Checked)
                {
                    // Calculate Membership Expiry Date
                    System.DateTime contract24Month = DateTime.Today.AddYears(2); //add 2 years (24 months) to today's date

                    // Define membership expiry date
                    membershipExpiryDate = contract24Month.ToString();

                } // end of 24 months
                  // end of contract duration - selections
  
                // Value-Added Extras - variables
                string access24_7 = "24/7 access"; //define 24/7 access
                string personalTrainer = "personal trainer"; //define personal trainer
                string dietConsultation = "diet consultation"; //define diet consultation
                string accessOnlineFitnessVideos = "access online fitness videos"; //define access online fitness video
                string noExtras; //define no extras
                string[] extras = new string[4];  //define size of array to store value-added extras

                // Value-Added Extras - selections
                bool check24_7 = checkBox24_7Access_Extra.Checked; //checks whether 24/7 access is selected
                bool checkPersonalTrainer = checkBoxPersonalTrainer_Extra.Checked; //checks whether personal trainer is selected
                bool checkDietConsultation = checkBoxDietConsultation_Extra.Checked; //checks whether diet consultation is selected
                bool checkAccessOnlineFitnessVideos = checkBoxAccessOnlineFitnessVideos_Extra.Checked; //checks whether access online fitness videos is selected

                // Create a string to hold all the extras
                string valueForExtras = "";

                // Value-Added Extras - selections
                // At least 1 extra selected
                if (check24_7 || checkPersonalTrainer || checkDietConsultation || checkAccessOnlineFitnessVideos)
                {
                    // 24/7 access - selected
                    if (check24_7)
                    {
                        extras[0] = access24_7; //assign value to extra 1

                        // Add 24/7 access
                        valueForExtras += extras[0];

                        // More extras to follow (personal trainer or diet consultation or access online fitness videos)
                        if (checkPersonalTrainer || checkDietConsultation || checkAccessOnlineFitnessVideos)
                        {
                            //add semi-colon after extra
                            valueForExtras += "; "; 
                        } 
                        // end of more extras to follow (personal trainer or diet consultation or access online fitness videos)

                    } // end of 24/7 access - selected

                    // Personal trainer - selected
                    if (checkPersonalTrainer)
                    {
                        extras[1] = personalTrainer; //assign value to extra 2

                        // Add personal trainer
                        valueForExtras += extras[1];

                        // More extras to follow (diet consultation or access online fitness videos)
                        if (checkDietConsultation || checkAccessOnlineFitnessVideos)
                        {
                            //add semi-colon after extra
                            valueForExtras += "; "; 
                        } 
                        // end of more extras to follow (diet consultation or access online fitness videos)

                    } // end of personal trainer - selected

                    // Diet consultation - selected
                    if (checkDietConsultation)
                    {
                        extras[2] = dietConsultation; //assign value to extra 3

                        // Add diet consultation
                        valueForExtras += extras[2];

                        // More extras to follow (access online fitness videos)
                        if (checkAccessOnlineFitnessVideos)
                        {
                            //add semi-colon after extra
                            valueForExtras += "; ";

                        } // end of more extras to follow (access online fitness videos)

                    } // end of diet consultation - selected

                    // Access Online fitness videos - selected
                    if (checkAccessOnlineFitnessVideos)
                    {
                        extras[3] += accessOnlineFitnessVideos; //assign value to extra 4

                        // Add access online fitness videos
                        valueForExtras += extras[3];  

                    } // end of access online fitness videos - selected

                } // end of at least 1 extra selected

                // No extas selected
                else
                {
                    noExtras = "no extras"; //no extras added

                    // Add no extras
                    valueForExtras = noExtras; 

                } // end of no extras selected                
                // end of value-added extras - selections

                // Initialise membership ID
                int membershipID = 0;

                // Membership Types - selections
                // basic
                if (radioButtonBasic_Membership.Checked)
                {
                    // Assign basic membership ID
                    membershipID = 1;

                } // end of basic

                // regular
                else if (radioButtonRegular_Membership.Checked)
                {
                    // Assign regular membership ID
                    membershipID = 2;

                } // end of regular

                // premium
                else if (radioButtonPremium_Membership.Checked)
                {
                    // Assign premium membership ID
                    membershipID = 3;

                } // end of premium
                  // end of membership types - selections

                // Try to add the member into the database
                try
                {
                    // Build the query for the SQL command
                    string query = $"INSERT INTO Member (FirstName, LastName, Address, Mobile, PaymentFrequency, MembershipExpiryDate, Extras, MembershipID) VALUES ('{firstName}', '{lastName}', '{fullAddress}', '{mobileNo}', '{pmtFrequency}', '{membershipExpiryDate}', '{valueForExtras}', '{membershipID}');";
                    
                    // Use the Sql connection string
                    using (var connectionVar = new SqlConnection(connectionString))
                    {
                        // Run the SQL command on the SQL server connection
                        var command = new SqlCommand(query, connectionVar);

                        connectionVar.Open();

                        // Execute the Sql command
                        command.ExecuteNonQuery();

                    } // end of use the Sql connection string

                    // Thank you message to user
                    labelSubmitMsg.ForeColor = Color.DarkViolet; //change color of message
                    string submitMessage = "Thank you for registering."; //define message
                    labelSubmitMsg.Text = submitMessage; //show message

                    // Display message to user
                    MessageBox.Show("You have successfully added a member!");

                } // end of try to add the member into the database

                // Use a catch (SqlException) to suppress any errors that might stop the program
                catch (SqlException z)
                {
                    MessageBox.Show($"Error - Record could not be added: \r\n{z}");
                    return;
                } // end of try catch statement

            } // end of form was correctly completed by the user

        } // end of submit form

        // Method to help user complete form
        private bool CompleteForm(bool checkCompleteForm)
        {
            // Error control - default color values
            labelEnterYourContactDetails.ForeColor = Color.Black; //reset color of label to default color
            labelFirstName.ForeColor = Color.Black; //reset color of label to default color
            labelLastName.ForeColor = Color.Black; //reset color of label to default color
            labelMobileNumber.ForeColor = Color.Black; //reset color of label to default color
            labelSuburbOrCity.ForeColor = Color.Black; //reset color of label to default color
            labelRegion.ForeColor = Color.Black; //reset color of label to default color
            labelPostalCode.ForeColor = Color.Black; //reset color of label to default color
            labelPleaseSelect.ForeColor = Color.DarkViolet; //reset color of label to default color
            checkBoxCityGymConsent.ForeColor = Color.Black; //reset color of city gym consent checkbox to default color

            const string blank = ""; //blank string

            // Customer Details - default values
            string detailsFirstName = "Your first name..."; //default value for fist name
            string detailsLastName = "Your last name..."; //default value for last name
            string detailsMobileNo = "Your mobile number..."; //default value for mobile number
            string detailsAddress1 = "Unit number and street address..."; //default value for address (line 1)
            string detailsAddress2 = "Your street address..."; //default value for address (line 2)
            string detailsSuburbOrCity = "Your suburb or city..."; //default value for suburb/city
            string detailsRegion = blank; //default value for region
            string detailsPCode = "0000"; //default value for postal code

            // Customer Details - array loaded with default values
            string[] customerDetails =
            {
                detailsFirstName,   //[0] - fist name
                detailsLastName,    //[1] - last name
                detailsMobileNo,    //[2] - mobile number
                detailsAddress1,    //[3] - address (line 1)
                detailsAddress2,    //[4] - address (line 2)
                detailsSuburbOrCity,      //[5] - suburb or city
                detailsRegion,      //[6] - region
                detailsPCode,       //[7] - postal code
            }; // end of customer details - array loaded with default values

            // Customer Details - default values changed to user inputs
            customerDetails[0] = textBoxFirstName.Text; //define customer first name
            customerDetails[1] = textBoxLastName.Text; //define customer last name
            customerDetails[2] = textBoxMobileNumber.Text; //define customer mobile number
            customerDetails[3] = textBoxAddress1.Text; //define customer address (line 1)
            customerDetails[4] = textBoxAddress2Optional.Text; //define customer address (line 2)
            customerDetails[5] = textBoxSuburbOrCity.Text; //define customer suburb or city
            customerDetails[6] = comboBoxRegion.Text; //define customer region
            customerDetails[7] = textBoxPostalCode.Text; //define customer postal code

            // City Gym consent
            bool consentGym = checkBoxCityGymConsent.Checked; //returns true if City Gym Consent checkbox is checked, else false

            // Net Membership Cost calculated
            string netMemCostCalc = textBoxNetMembershipCost_Amount.Text; //define the calculated net membership cost

            // Loop to check form for completion
            for (int f = 0; f < 15; f++)
            {
                // Switch statement index
                string errorMessage = "0"; //define index

                // Submit message
                string submitMessage; //create variable

                // Error control
                // default values remained and blank entries
                if ((customerDetails[0] == detailsFirstName || customerDetails[0] == blank) ||
                    (customerDetails[1] == detailsLastName || customerDetails[1] == blank) ||
                    (customerDetails[2] == detailsMobileNo || customerDetails[2] == blank) ||
                    (customerDetails[3] == detailsAddress1 || customerDetails[3] == blank) ||
                    (customerDetails[5] == detailsSuburbOrCity || customerDetails[5] == blank) ||
                    (customerDetails[7] == detailsPCode || customerDetails[7] == blank))
                {
                    errorMessage = "1"; //go to switch statement case 1

                } // end of default values remained and blank entries

                // No default values; no blanks
                else
                {
                    // Customer details - variables

                    // First name contains a number
                    if (
                            customerDetails[0].Contains("0") ||
                            customerDetails[0].Contains("1") ||
                            customerDetails[0].Contains("2") ||
                            customerDetails[0].Contains("3") ||
                            customerDetails[0].Contains("4") ||
                            customerDetails[0].Contains("5") ||
                            customerDetails[0].Contains("6") ||
                            customerDetails[0].Contains("7") ||
                            customerDetails[0].Contains("8") ||
                            customerDetails[0].Contains("9")
                            )
                    {
                        errorMessage = "2"; //go to switch statement case 2

                    } // end of first name contains a number

                    // last name contains a number
                    else if (
                            customerDetails[1].Contains("0") ||
                            customerDetails[1].Contains("1") ||
                            customerDetails[1].Contains("2") ||
                            customerDetails[1].Contains("3") ||
                            customerDetails[1].Contains("5") ||
                            customerDetails[1].Contains("6") ||
                            customerDetails[1].Contains("7") ||
                            customerDetails[1].Contains("8") ||
                            customerDetails[1].Contains("9")
                            )
                    {
                        errorMessage = "3"; //go to switch statement case 3
                    } // end of last name contains a number

                    // mobile number contains characters
                    else if (!Int64.TryParse(customerDetails[2], out _))
                    {
                        // mobile number contains (-)
                        if (customerDetails[2].Contains("-"))
                        {
                            errorMessage = "4a"; //go to switch statement case 4a

                        } // end of number contains (-)

                        // mobile number contains other characters
                        else
                        {
                            errorMessage = "4b"; //go to switch statement case 4b

                        } // end of mobile number contains other characters

                    } // end of mobile number contains characters

                    // mobile number starts with (+) or (64) country code
                    else if (customerDetails[2].StartsWith("+") || customerDetails[2].StartsWith("64"))
                    {
                        errorMessage = "4c"; //go to switch statement case 4c

                    } // end of mobile number starts with (+) or (64) country code

                    // mobile number does not start with 02
                    else if (customerDetails[2].StartsWith("02") == false)
                    {
                        errorMessage = "4d"; //go to switch statement case 4d

                    } // end of mobile number does not start with 02       

                    // mobile number starts with 02; too short; too long
                    else if (
                            customerDetails[2].StartsWith("02") &&
                            (customerDetails[2].Length < 9 ||
                            customerDetails[2].Length > 11)
                            )
                    {
                        errorMessage = "4e"; //go to switch statement case 4e

                    } // end of mobile number starts with 02; too short; too long

                    // suburb or city contains a number
                    else if (customerDetails[5].Contains("0") ||
                            customerDetails[5].Contains("1") ||
                            customerDetails[5].Contains("2") ||
                            customerDetails[5].Contains("3") ||
                            customerDetails[5].Contains("4") ||
                            customerDetails[5].Contains("5") ||
                            customerDetails[5].Contains("6") ||
                            customerDetails[5].Contains("7") ||
                            customerDetails[5].Contains("8") ||
                            customerDetails[5].Contains("9"))
                    {
                        errorMessage = "5"; //go to switch statement case 5
                    } // end of suburb or city contains a number

                    // region contains a number
                    else if (customerDetails[6] == blank)
                    {
                        errorMessage = "6"; //go to switch statement case 6
                    } // end of region contain a number

                    // postal code - not a number; not 4 digits long
                    else if ((Int32.TryParse(customerDetails[7], out _) && customerDetails[7].Length == 4) == false)
                    {
                        errorMessage = "7"; //go to switch statement case 7

                    } // end of postal code - not a number; not 4 digits long

                    // City Gym consent - error control
                    else if (consentGym == false)
                    {
                        errorMessage = "8"; //go to switch statement case 8
                    } // end of city gym consent - error control

                    // Membership Cost not calculated
                    else if (netMemCostCalc == blank)
                    {
                        errorMessage = "9"; //go to switch statement case 9
                    } // end of membership cost not calculated

                } // end of no default values; not blanks
                // end of error control

                // Switch through error messages located above submit button
                switch (errorMessage)
                {
                    // default values remained and blank entries
                    case "1":
                        {
                            // Enter your contact details - label
                            labelEnterYourContactDetails.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please complete your contact details to submit your registration"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of default values remained and blank entries

                    // First name contains a number
                    case "2":
                        {
                            // First name - label
                            labelFirstName.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please remove the number from your first name"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of first name contains a number

                    // last name contains a number
                    case "3":
                        {
                            // Last name - label
                            labelLastName.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please remove the number from your last name"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of last name contains a number

                    // mobile number contains (-)
                    case "4a":
                        {
                            // Mobile number - label
                            labelMobileNumber.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please remove any dashes (-) from your mobile number"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of mobile number contains (-)

                    // mobile number contains other characters
                    case "4b":
                        {
                            // Mobile number - label
                            labelMobileNumber.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please remove the characters from your mobile number"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of mobile number contains other characters

                    // mobile number starts with (+) or (64) country code
                    case "4c":
                        {
                            // Mobile number - label
                            labelMobileNumber.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please remove the country code + and/or 64 from your mobile number"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of mobile number starts with (+) or (64) country code

                    // mobile number does not start with 02
                    case "4d":
                        {
                            // Mobile number - label
                            labelMobileNumber.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please enter a valid mobile number that starts with 02"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of mobile number does not start with 02

                    // mobile number is too short or too long
                    case "4e":
                        {
                            // Mobile number - label
                            labelMobileNumber.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please enter a mobile number with the correct length"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of mobile number is too short or too long

                    // suburb or city contains a number
                    case "5":
                        {
                            // Suburb / City - label
                            labelSuburbOrCity.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please remove the number from your suburb or city"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of suburb / city contains a number

                    // region contains a number
                    case "6":
                        {
                            // Region - label
                            labelRegion.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please select your region"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of region contains a number

                    // postal code - not a number; not 4 digits long
                    case "7":
                        {
                            // Postal code - label
                            labelPostalCode.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please enter a valid postal code with 4 numbers"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of postal code - not a number; not 4 digits long

                    // city gym consent not given
                    case "8":
                        {
                            // Customer consent - labels
                            labelPleaseSelect.ForeColor = Color.Red; //change color of label
                            checkBoxCityGymConsent.ForeColor = Color.Red; //change color of label

                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please give us consent to process your information"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message

                            checkCompleteForm = false;
                            break;
                        } // end of city gym consent not give

                    // membership cost not calculated
                    case "9":
                        {
                            // Registration not successful - error message
                            labelSubmitMsg.ForeColor = Color.Red; //change color of message
                            submitMessage = "Please first calculate your membership"; //define message
                            labelSubmitMsg.Text = submitMessage; //show message
                            
                            checkCompleteForm = false;
                            break;
                        } // end of membership cost not calculated

                    // Registration successful - customer details & City Gym consent complete
                    default:
                        {
                            checkCompleteForm = true;
                            break;
                        } // end of registration successful - customer details & City Gym consent complete

                } // end of switch through error messages located above submit button

            } // end of loop to check form for completion

            return checkCompleteForm;

        } // end of method CompleteForm()

        // Reset Form - occurs when button is clicked on membership registration form
        private void ButtonResetForm_Click(object sender, EventArgs e)
        {
            const string blank = ""; //blank string

            // Customer Details - completed fields
            textBoxFirstName.Text = "Your first name..."; //reset to default setting
            textBoxLastName.Text = "Your last name..."; //reset to default setting
            textBoxMobileNumber.Text = "Your mobile number..."; //reset to default setting
            textBoxAddress1.Text = "Unit number and street address..."; //reset to default setting
            textBoxAddress2Optional.Text = "Your street address..."; //reset to default setting
            textBoxSuburbOrCity.Text = "Your suburb or city..."; //reset to default setting
            comboBoxRegion.SelectedIndex = -1; //reset to default setting
            textBoxPostalCode.Text = "0000"; //reset to default setting

            // Customer Details - labels
            labelEnterYourContactDetails.ForeColor = Color.DarkViolet; //reset to default setting
            labelFirstName.ForeColor = Color.Black; //reset to default setting
            labelLastName.ForeColor = Color.Black; //reset to default setting
            labelMobileNumber.ForeColor = Color.Black; //reset to default setting
            labelAddress1.ForeColor = Color.Black; //reset to default setting
            labelAddress2Optional.ForeColor = Color.Black; //reset to default setting
            labelSuburbOrCity.ForeColor = Color.Black; //reset to default setting
            labelRegion.ForeColor = Color.Black; //reset to default setting
            labelPostalCode.ForeColor = Color.Black; //reset to default setting

            // Customer Details - submit message
            labelSubmitMsg.ForeColor = Color.DarkViolet; //reset default setting
            labelSubmitMsg.Text = "Calculate your membership before clicking \"SUBMIT\""; //reset to default setting

            // Customer Consent - selections
            checkBoxCityGymConsent.Checked = false; //clear selection
            checkBoxCityGymConsent.ForeColor = Color.Black; //reset to default setting
            checkBoxThirdPartyConsent.Checked = false; //clear selection

            // Customer Consent - labels
            labelPleaseSelect.ForeColor = Color.DarkViolet; //reset to default setting

            // Membership Types - selections
            radioButtonBasic_Membership.Checked = false; //reset to default setting
            radioButtonRegular_Membership.Checked = false;  //reset to default setting
            radioButtonPremium_Membership.Checked = false; //reset to default setting

            // Value-Added Extras - selections
            checkBox24_7Access_Extra.Checked = false; //clear selection
            checkBoxPersonalTrainer_Extra.Checked = false; //clear selection
            checkBoxDietConsultation_Extra.Checked = false; //clear selection
            checkBoxAccessOnlineFitnessVideos_Extra.Checked = false; //clear selection

            // Contract Duration - selections
            radioButton3Months_ContractDuration.Checked = false; //clear selection
            radioButton12Months_ContractDuration.Checked = false; //clear selection
            radioButton24Months_ContractDuration.Checked = false; //clear selection

            // Debit Order - selections
            radioButtonYes_DirectDebit.Checked = false; //clear selection
            radioButtonNo_DirectDebit.Checked = false; //clear selection

            // Payment Frequency - selections
            radioButtonWeekly_PaymentFrequency.Checked = false; //clear selection
            radioButtonMonthly_PaymentFrequency.Checked = false; //clear selection

            // Membership Cost - amounts shown
            textBoxBaseMembershipCost_Amount.Text = blank; //clear base membership cost
            textBoxTotalDiscount_Amount.Text = blank; //clear total discount amount
            textBoxValueAddedExtras_Amount.Text = blank; //clear value-added extra amount
            textBoxNetMembershipCost_Amount.Text = blank; //clear net membership cost amount
            labelYourRegularPayment.Visible = false; //make start of regular payment sentance inviisble
            labelRegularPmtAmt.Text = "$000.00"; //reset regular payment amount to default value
            labelRegularPmtAmt.Visible = false; //make regular payment amount invisible

            // Membership Cost - choices shown
            labelBaseMembership_Choice.Text = blank; //clear base membership type chosen
            labelDiscount_Choice.Text = blank; //clear discounts chosen
            labelValueAddedExtras_Choice.Text = blank; //clear value-added extras chosen

            // Error control - membership cost
            labelRequiredSelectionsToCalculateYourMembershipCost.ForeColor = Color.DarkViolet; //reset to default setting
            labelSelectYourPreferredMembershipType.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelSelectYourPreferredContractDuration.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelSelectHowYouWouldLikeToPay.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelIWouldLikeToPayWithDirectDebit.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelPreferredPaymentFrequency.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelRequiredMembershipTypes.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelRequiredContractDuration.ForeColor = Color.DarkViolet; //reset color of label to default color
            labelRequiredPaymentOptions.ForeColor = Color.DarkViolet; //reset color of label to default color

            // Reset radio buttons to allow keyboard tab to stop at radio buttons
            radioButtonBasic_Membership.TabStop = true; //reset basic membership type
            radioButton3Months_ContractDuration.TabStop = true; //reset 3 months contract duration
            radioButtonYes_DirectDebit.TabStop = true; //reset direct debit payment option
            radioButtonWeekly_PaymentFrequency.TabStop = true; //reset weekly payment frequency

        } // end of reset form

        // First name to be entered - occurs when field is clicked
        private void TextBoxFirstName_Click(object sender, EventArgs e)
        {
            textBoxFirstName.Text = ""; //clear text box for user to enter their first name

        } // end of first name entered

        // Last name to be entered - occurs when field is clicked
        private void TextBoxLastName_Click(object sender, EventArgs e)
        {
            textBoxLastName.Text = ""; //clear text box for user to enter their last name

        } // end of last name entered

        // Mobile number to be entered - occurs when field is clicked
        private void TextBoxMobileNumber_Click(object sender, EventArgs e)
        {
            textBoxMobileNumber.Text = ""; //clear text box for user to enter their mobile number

        } // end of mobile number entered

        // Address 1 to be entered - occurs when field is clicked
        private void TextBoxAddress1_Click(object sender, EventArgs e)
        {
            textBoxAddress1.Text = ""; //clear text box for user to enter their address 1

        } // end of address 1 entered

        // Address 2 (optional) to be entered - occurs when field is clicked
        private void TextBoxAddress2Optional_Click(object sender, EventArgs e)
        {
            textBoxAddress2Optional.Text = ""; //clear text box for user to enter their address 2

        } // end of address 1 (optional) entered

        // Suburb or city to be entered - occurs when field is clicked
        private void TextBoxSuburbOrCity_Click(object sender, EventArgs e)
        {
            textBoxSuburbOrCity.Text = ""; //clear text box for user to enter their suburb or city

        } // end of suburb or city entered

        // Region to be selected - occurs when field is clicked
        private void ComboBoxRegion_Click(object sender, EventArgs e)
        {
            comboBoxRegion.Text = ""; //clear combo box for user to select their region

        } // end of region selected

        // Postal code to be entered - occurs when field is clicked
        private void TextBoxPostalCode_Click(object sender, EventArgs e)
        {
            textBoxPostalCode.Text = ""; //clear text box for user to enter their postal code

        } // end of postal code entered

        // Navigate to Main Menu screen
        private void ToolStripMenuItemMainMenu_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Main Menu window
            new MainMenu().Show();

        } // end of navigate to Main Menu screen

        // Exit Application selected in the top menu bar
        private void ToolStripMenuItemExitApplication_Click(object sender, EventArgs e)
        {
            // Close and exit the whole application
            Application.Exit();

        } // end of exit Application selected in the top menu bar

        // Navigate to Help screen for Adding a Member
        private void ToolStripMenuItemAddAMemberScreen_Click(object sender, EventArgs e)
        {
            // Hide this screen
            this.Hide();

            // Open the Help screen
            new HelpAddMember().Show();

        } // end of navigate to Help screen for Adding a Member

    } // end of Add a Member Windows Form
}
