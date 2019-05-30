using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FederalIncomeTax_6._2_X
{
    /*
     Drew Watson
     Lab assignment 3
     6.2X
     Friedman
     Component Spring 2018

        *The example in the book or lab sheet is off by about 75 cents or so.
        * It says the value to be returned for 35350 is 4840.50 when in reality it should be 4841.25
     */

    public partial class frmIncomeTaxCalculator : Form
    {
        public frmIncomeTaxCalculator()
        {
            InitializeComponent();
        }

        //Loads the form 
        private void frmIncomeTaxCalculator_Load(object sender, EventArgs e)
        {
            //When the form is loaded disables controls and places focus on entername txtbox
            txtEnterName.Focus();

            txtIncomeTaxOwed.Visible = false;
            lblIncomeTaxOwed.Visible = false;

            txtTaxableIncome.Visible = false;
            lblTaxableIncome.Visible = false;
            btnCalculate.Visible = false;

            
        }

        //OK  button click event that enables controls
        private void btnOK_Click(object sender, EventArgs e)
        {
            //Checks if txtEnterName is blank or not
            if (IsPresent(txtEnterName))
            {
                //Enables controls is there is an entry 
                txtIncomeTaxOwed.Visible = true;
                lblIncomeTaxOwed.Visible = true;

                txtTaxableIncome.Visible = true;
                lblTaxableIncome.Visible = true;
                btnCalculate.Visible = true;
            }

        }

        //Click event that carries out calculation
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //Variables
            decimal taxableIncome = 0, incomeTaxOwed = 0;

            //Try catch block  that catches any generic errors to be found
            try
            {
                //Checks that the data in the txtbox is valid, returns boolean
                if (textboxValidation())
                {
                    //Takes input from user
                    taxableIncome = Convert.ToDecimal(txtTaxableIncome.Text);

                    //Calculates taxes owed
                    incomeTaxOwed = taxTableLookup(taxableIncome);
                    
                    //Displays taxes owed and changes focus to txtTaxableIncome
                    txtIncomeTaxOwed.Text = Convert.ToString(incomeTaxOwed);
                    txtTaxableIncome.Focus();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + "Please try again." + "\n\n" + "Type of error encountered: " + ex.GetType().ToString(), "Error: Exception found.");
            }
        }

        //Checks to make sure all criteria are meet for a txtbox
        private bool textboxValidation()
        {
            //Variables
            bool dataValid = false;

                //Checks that txtTaxableIncome has an entry, that is a decimal and that it is not negative
                if (IsPresent(txtTaxableIncome) && IsDecimal(txtTaxableIncome) && IsNonNegative(txtTaxableIncome))
                {
                    dataValid = true;
                }

                //Sets datavalid to false if data is not valid
                else
                {
                    dataValid = false;  
                }

                //Returns the boolean
                return dataValid;
        }

        //Checks to see if there is an entry within a txtbox
        private bool IsPresent(TextBox txtbox)
        {
            //Variables
            bool validData = false;
           
            //Checks that the txtbox is not empty if so returns true
            if (txtbox.Text != "")
            {
                validData = true;
            }

            //Shows a message if it is not and sets the bool to false
            else
            {
                MessageBox.Show("Entry can not be blank. Please try again.", "Empty Entry");
                txtbox.Focus();
                validData = false;
            }

            //Returns the boolean
            return validData;

        }

        //Checks to see if there is an entry within a txtbox
        private bool IsNonNegative(TextBox txtbox)
        {
            //Variables
            bool validData = false;
            decimal entry = 0;
            
            //Converts entry in txtbox to a decimal variable
            entry = Convert.ToDecimal(txtbox.Text);

            //Checks that the entry is greater than zero
            if (entry > 0)
            {
                validData = true;
            }

            //Shows a message if it is not and sets the bool to false
            else
            {
                MessageBox.Show("Entry must be a non negative value. Please try again.", "Enrty Error");
                txtbox.Focus();
                validData = false;
            }

            //Returns the boolean
            return validData;

        }

        //Checks to see if there is an entry within a txtbox
        private bool IsDecimal (TextBox txtbox)
        {
            //Variables
            bool validData = false;
            decimal validDecimal = 0;

            //Checks that the entry is a decimal
            if (Decimal.TryParse(txtbox.Text, out validDecimal))
                {
                    validData = true;
                }

            //Shows a message if it is not and sets the bool to false
            else
            {
                MessageBox.Show("Entry must be a decimal value. Please try again.", "Enrty Error");
                txtbox.Focus();
                validData = false;
            }

            //Returns the boolean
            return validData;

        }

        //Calculates the proper tax amount 
        private decimal taxTableLookup( decimal taxableIncome)
        {
        
        //Constance because they never change and I refer to them later
        const decimal taxableIncome1 = 9225, taxableIncome2 = 37450, taxableIncome3 = 90750, taxableIncome4 = 189300, taxableIncome5 = 411500, taxableIncome6 = 413200;
        const decimal taxBracket1 = .10m, taxBracket2 = .15m, taxBracket3 = .25m, taxBracket4 = .28m, taxBracket5 = .33m, taxBracket6 = .35m, taxBracket7 = .396m;
        const decimal taxPlus0 = 0m, taxPlus1 = 922.50m, taxPlus2 = 5156.25m, taxPlus3 = 18481.25m, taxPlus4 = 46075.25m, taxPlus5 = 119401.25m, taxPlus6 = 119996.25m;
        
        //Variables
        decimal taxOwed = 0, tempTax = 0, totalTax = 0,taxIncome = 0; ;

        // Sets taxincome to the variable that is being passed to the method through income
        taxIncome = taxableIncome;

            // Does all the calculations
            //Subtracts previous tiered tax, taxes leftovers in the new bracket, adds the previously lower tiered taxed income to the upper/current teir income together
            if (taxIncome >= 0 && taxIncome <= taxableIncome1)
            {
                totalTax = (taxIncome * taxBracket1) + taxPlus0;
            }

            else if (taxIncome > taxableIncome1 && taxIncome <= taxableIncome2)
            {
                tempTax = taxIncome - taxableIncome1;
                tempTax = tempTax * taxBracket2;
                totalTax = tempTax + taxPlus1;
            }

            else if (taxIncome > taxableIncome2 && taxIncome <= taxableIncome3)
            {
                tempTax = taxIncome - taxableIncome2;
                tempTax = tempTax * taxBracket3;
                totalTax = tempTax + taxPlus2;
            }

            else if (taxIncome > taxableIncome3 && taxIncome <= taxableIncome4)
            {
                tempTax = taxIncome - taxableIncome3;
                tempTax = tempTax * taxBracket4;
                totalTax = tempTax + taxPlus3;
            }

            else if (taxIncome > taxableIncome4 && taxIncome <= taxableIncome5)
            {
                tempTax = taxIncome - taxableIncome4;
                tempTax = tempTax * taxBracket5;
                totalTax = tempTax + taxPlus4;
            }

            else if (taxIncome > taxableIncome5 && taxIncome <= taxableIncome6)
            {
                tempTax = taxIncome - taxableIncome5;
                tempTax = tempTax * taxBracket6;
                totalTax = tempTax + taxPlus5;
            }

            else
            {
                tempTax = taxIncome - taxableIncome6;
                tempTax = tempTax * taxBracket7;
                totalTax = tempTax + taxPlus6;
            }


            taxOwed = totalTax;
            return taxOwed;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
