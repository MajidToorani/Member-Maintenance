using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

//--------------------------
//Majid Tooranisama
//#7725070
//June 12, 2018
//Assn2
//--------------------------

namespace MTMember
{
    public partial class MTMember : Form
    {
        public MTMember()
        {
            InitializeComponent();
        }
        string firstName ="";
        string lastName = "";
        string spousFirst = "";
        string spouseLast = "";
        string street = "";
        string city = "";
        string province = "";
        string postal = "";
        string phone = "";
        string email = "";
        string fee = "";

        public string MTCapitalize(string name)                            //Method for Capitalizing
        {
            int cntr = 0;
            string nameCap = "";
            string nameTrim = name.Trim().ToLower();
            Regex rgx = new Regex("");
            string[] nameArr = rgx.Split(nameTrim);

            foreach (char item in nameTrim)
            {
                if (char.IsDigit(item) || char.IsWhiteSpace(item))
                {
                    cntr++;
                }
            }
            nameArr[cntr] = nameArr[cntr].ToUpper();
            foreach (char item in nameTrim)
            {
                if (char.IsWhiteSpace(item))
                {
                    int spaceIndex1 = nameTrim.LastIndexOf(" ");
                    cntr = spaceIndex1 + 1;
                    nameTrim.Remove(spaceIndex1, 1);
                }
            }
            nameArr[cntr+1] = nameArr[cntr+1].ToUpper();
            foreach (char item in nameTrim)
            {
                if (char.IsWhiteSpace(item))
                {
                    int spaceIndex2 = nameTrim.IndexOf(" ");
                    cntr = spaceIndex2 + 1;
                    nameTrim.Remove(spaceIndex2, 1);
                }
            }
            nameArr[cntr+1] = nameArr[cntr+1].ToUpper();

            for (int i = 0; i < nameArr.Length; i++)
            {
                nameCap += nameArr[i];
            }
            return nameCap;
        }
        public bool MTPostalCodeValidation(string postalCode)    //Method for postal code validation
        {
            string errors = "";
            bool boolean=false;
            if (postalCode.Length != 0)
            {
                Regex pattern = new Regex(@"^[A-Za-z][0-9][A-Za-z]\s?[0-9][A-Za-z][0-9]$");
                if (pattern.IsMatch(postalCode))
                {
                    MessageBox.Show("Postal code is valid!");
                    boolean=true;
                }
                else
                {
                    MessageBox.Show("Postal code is invalid!");
                    boolean= false;
                }
            }
            else
            {
                errors += "Empty postalcode!\n";
                MessageBox.Show(errors);
            }
            return boolean;
        }
        public bool MTPhoneNumberValidation(string phoneNumber)  //Method for phone number validation
        {
            string errors = "";
            bool boolean = false;
            if (phoneNumber.Length != 0)
            {
                Regex pattern = new Regex(@"^[0-9]{3}-?[0-9]{3}-?[0-9]{4}$");
                if (pattern.IsMatch(phoneNumber))
                {
                    MessageBox.Show("Phone number is valid!");
                    boolean = true;
                }
                else
                {
                    MessageBox.Show("Phone number is invalid!");
                    boolean = false;
                }
            }
            else
            {
                errors += "Empty phone number!\n";
                MessageBox.Show(errors);
            }
            return boolean;
        }
        public bool MTIsNumeric(string input)                         //Method for numeric validation
        {
            string errors = "";
            int count = 0;
            int dashIndex = input.IndexOf("-");
            if (dashIndex != -1) 
            {
                if (dashIndex > 0)
                {
                    errors += "There is a dash inside the number,it should be at the beginning\n";
                    input.Remove(dashIndex, 1);
                }
            }
            int firstDecimal = input.IndexOf(".");
            if (firstDecimal != -1) 
            {
                if (firstDecimal != input.LastIndexOf("."))
                {
                    errors += "Maximum one decimal allowed\n";
                    input.Remove(firstDecimal, 1);
                }
            }
            if (input.Length == 0)
                errors += "False, must have at least one digit\n";
            else
            {
                foreach (char item in input)
                {
                    if (!char.IsDigit(item))
                    {
                        count++;
                    }
                }
                errors += "It has " + count + " character(s) not digit\n";
            }
            if (errors == "" )
                return true;
            else if (input.Length != 0 && count == 0)
            {
                return true;
            }
            else if (firstDecimal == input.LastIndexOf(".") && dashIndex == -1 && count-1 == 0 )
            {
                return true;
            }
            else if (firstDecimal == input.LastIndexOf(".")  && dashIndex == 0 && count - 2 == 0)
            {
                return true;
            }
            else if (dashIndex == 0 && count - 1 == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show(errors); 
                return false;
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)             //When submit button clicks
        {
            firstName = txtbxMemFirst.Text;
            lastName = txtbxMemLast.Text;
            spousFirst = txtbxSpsFirst.Text;
            spouseLast = txtbxSpsLast.Text;
            street = txtbxStreet.Text;
            city = txtbxCity.Text;
            province = txtbxProvince.Text;
            postal = txtbxPostal.Text;
            phone= txtbxPhone.Text;
            email = txtbxEmail.Text;
            fee = txtbxFee.Text;

            if (firstName == "" || lastName == "")                    //first name and last name required
            {
                if (firstName == "")
                {
                    MessageBox.Show("First name is required!");
                    txtbxMemFirst.Focus();
                }
                if(lastName == "")
                {
                    MessageBox.Show("Last name is required!");
                    txtbxMemLast.Focus();
                }
            }
            else
            {                                                                //call capitalizing method
                txtbxMemFirst.Text= MTCapitalize(firstName);
                txtbxMemLast.Text= MTCapitalize(lastName);
                txtbxSpsFirst.Text= MTCapitalize(spousFirst);
                txtbxSpsLast.Text= MTCapitalize(spouseLast);
            }

            lblFullName.UseMnemonic = false;                                        //derive full name 
            if (spousFirst=="" && spouseLast=="")
            {
                lblFullName.Text = lastName + ", " + firstName;
            }
            else if (spouseLast == lastName || spouseLast == "" )
            {
                lblFullName.Text = lastName + ", " + firstName+" & "+spousFirst;
            }
            else
            {
                lblFullName.Text = lastName + ", " + firstName + " & " + spouseLast + ", " + spousFirst;
            }
            
            if (email == "" )                                   //option for email or postal information
            {
                MessageBox.Show("Postal information(Street Address, City and Postal code) is required");
                txtbxStreet.Focus();
                if (street!="")
                {
                    txtbxCity.Focus();
                    if (city!="")
                    {
                        txtbxPostal.Focus();
                        if (postal!="")
                        {
                            txtbxEmail.Focus();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Postal information(Street Address, City and Postal code) is optional");
            }
            txtbxStreet.Text = MTCapitalize(street);                        //capitalizing street and city
            txtbxCity.Text = MTCapitalize(city);

            Regex patt = new Regex(@"^[a-zA-Z]{2}$");                 //formatting province to two letters
            if (patt.IsMatch(province))
            {
                MessageBox.Show("Province is valid!");
                txtbxProvince.Text = province.ToUpper();
            }
            else
            {
                MessageBox.Show("Province should be two letter!");
                txtbxProvince.Focus();
            }

            if (!MTPostalCodeValidation(postal))                       //formatting postal code to pattern
            {
                txtbxPostal.Focus();
            }
            else if (postal.Length == 6)
            {
                postal = postal.Insert(3, " ").ToUpper();
                txtbxPostal.Text = postal;
            }
        
            if (!MTPhoneNumberValidation(phone))                       //formatting phone number to pattern
            {
                txtbxPhone.Focus();
            }
            else if (phone.Length == 10)
            {
                phone = phone.Insert(3, "-").Insert(7,"-");
                txtbxPhone.Text = phone;
            }
                                                                                      //validating the email
            Regex pattMail = new Regex(@"^[A-Za-z0-9][-A-Za-z0-9.!#$%&'*+-=?^_`{|}~\/]+@([-A-Za-z0-9]+\.)+[A-Za-z]{2,5}$");
            if (pattMail.IsMatch(email))
            {
                MessageBox.Show("Email is valid!");
                email = email.ToLower();
                txtbxEmail.Text = email;
            }
            else
            {
                MessageBox.Show("Email is invalid!");
            }
                                                               //validating the fee and edit to two decimal
            if (MTIsNumeric(fee) && Convert.ToDouble(fee) >= 0)             
            {
                MessageBox.Show("Fee is valid!");
                txtbxFee.Text = Convert.ToDouble(fee).ToString("f2");
            }
            else
            {
                MessageBox.Show("Pease enter a valid fee value and more than zero!");
                txtbxFee.Focus();
            }
        }
        private void btnPreFill_Click(object sender, EventArgs e)              //When prefill button clicks
        {
            txtbxMemFirst.Text = "Majid";
            txtbxMemLast.Text = "Tooranisama";
            txtbxSpsFirst.Text = "Taraneh";
            txtbxSpsLast.Text = "Khaleghi";
            txtbxStreet.Text = "150 Sunrise Place";
            txtbxCity.Text = "Kitchener";
            txtbxProvince.Text = "ON";
            txtbxPostal.Text = "N2B 3S9";
            txtbxPhone.Text = "647-995-9091";
            txtbxEmail.Text = "mtooranisama5070@conestogac.on.ca";
            txtbxFee.Text = "250.25";
        }
        private void btnClose_Click(object sender, EventArgs e)                  //When close button clicks
        {
            Close();
        }
    }
}
