using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TravelPackageGUI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TravelPackageData;

namespace TravelPackageGUI
{
    /// <summary>
    /// a repository of validation methods
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// tests if a textbox contains non-empty string
        /// </summary>
        /// <param name="textBox">text box to check</param>
        /// <returns>true is valid and false if not</returns>
        public static bool IsPresent(TextBox textBox)
        {
            bool isValid = true; //"innocent until proven guilty"
            if (textBox.Text == "")
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} is required");
                textBox.Focus();
            }
            return isValid;
        }

        /// <summary>
        /// tests if a text box contains int that is >= 0
        /// </summary>
        /// <param name="textBox">text box to check</param>
        /// <returns>true is valid and false if not</returns>
        public static bool IsNonNegativeInt(TextBox textBox)
        {
            bool isValid = true;
            int value; // parsed value if successful
            if (!Int32.TryParse(textBox.Text, out value)) // not an int
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be a whole number");
                textBox.SelectAll();
                textBox.Focus();
            }
            else if (value < 0) // an int, but negative
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be positive or zero");
                textBox.SelectAll();
                textBox.Focus();
            }
            return isValid;
        }

        /// <summary>
        /// tests if the text box contains non-negative double value
        /// </summary>
        /// <param name="textBox">text box to check</param>
        /// <returns>true if valid and false if not</returns>       
        public static bool IsNonNegativeDouble(TextBox textBox)
        {
            bool isValid = true;
            double value; // parsed value if successful
            if (!Double.TryParse(textBox.Text, out value)) // not a double
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be a number");
                textBox.SelectAll();
                textBox.Focus();
            }
            else if (value < 0) // a double, but negative
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be positive or zero");
                textBox.SelectAll();
                textBox.Focus();
            }
            return isValid;
        }
        /// <summary>
        /// tests if the text box contains non-negative decimal value
        /// </summary>
        /// <param name="textBox">text box to check</param>
        /// <returns>true if valid and false if not</returns>       
        public static bool IsNonNegativeDecimal(TextBox textBox)
        {
            bool isValid = true;
            decimal value; // parsed value if successful

            // Check if the text box is empty
            if (string.IsNullOrEmpty(textBox.Text))
            {
                // If empty, no need for validation
                return isValid;
            }

            // Remove currency symbols and separators
            string input = textBox.Text.Replace("$", "").Replace(",", "");

            if (!Decimal.TryParse(input, out value)) // not a decimal
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be a number");
                textBox.SelectAll();
                textBox.Focus();
            }
            else if (value < 0) // a decimal, but negative
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be positive or zero");
                textBox.SelectAll();
                textBox.Focus();
            }
            
            return isValid;
        }

        /// <summary>
        /// tests if the text box contains a decimal value within range
        /// </summary>
        /// <param name="textBox"> text box to check</param>
        /// <param name="minValue">minimum value for the range</param>
        /// <param name="maxValue">maximum value for the range</param>
        /// <returns>true if valide, false if not</returns>
        public static bool IsDecimalInRange(TextBox textBox,
            decimal minValue, decimal maxValue)
        {
            bool isValid = true;
            decimal value; // parsed value if successful
            if (!Decimal.TryParse(textBox.Text, out value)) // not a decimal
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be a number");
                textBox.SelectAll();
                textBox.Focus();
            }
            else if (value < minValue || value > maxValue) // a decimal, but out of range
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be between {minValue} and {maxValue}");
                textBox.SelectAll();
                textBox.Focus();
            }
            return isValid;
        }

        /// <summary>
        /// tests if the text box contains a double value within range
        /// </summary>
        /// <param name="textBox"> text box to check</param>
        /// <param name="minValue">minimum value for the range</param>
        /// <param name="maxValue">maximum value for the range</param>
        /// <returns>true if valide, false if not</returns>
        public static bool IsDoubleInRange(TextBox textBox,
            double minValue, double maxValue)
        {
            bool isValid = true;
            double value; // parsed value if successful
            if (!Double.TryParse(textBox.Text, out value)) // not a double
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be a number");
                textBox.SelectAll();
                textBox.Focus();
            }
            else if (value < minValue || value > maxValue) // a double, but out of range
            {
                isValid = false;
                MessageBox.Show($"{textBox.Tag} must be between {minValue} and {maxValue}");
                textBox.SelectAll();
                textBox.Focus();
            }
            return isValid;
        }
        /// <summary>
        /// tests if the text box contains a valid string
        /// </summary>
        /// <param name="textBox"> text box to check</param>
        /// <returns>true if valide, false if not</returns>
        public static bool IsValidString(TextBox textBox)
        {
            bool isValid = true;
            if (!String.IsNullOrEmpty(textBox.Text)) // string exists
            {
                foreach (char c in textBox.Text)
                {
                    if (!Char.IsLetter(c) && c != '-' && c != ' ' && c != '.' && c != '\'')
                    {
                        isValid = false;

                    }
                }
                if (isValid == false)
                {
                    MessageBox.Show($"{textBox.Tag} must consist only of letters, spaces, dashes, dots, and apostrophes.");
                    textBox.SelectAll();
                    textBox.Focus();
                }
            }
            return isValid;
        }
        /// <summary>
        /// checks if selection is selected from Combo box
        /// </summary>
        /// <param name="comboBox">combo box to test</param>
        /// <returns>true if valide, false if not</returns>
        public static bool IsSelected(ComboBox comboBox)
        {
            bool isValid = true; // innocent until proven guilty
            if (comboBox.SelectedIndex == -1) // if there is no selection made
            {
                isValid = false;
                MessageBox.Show($"Please select a {comboBox.Tag} from the drop down list.");

            }
            return isValid;
        }
        /// <summary>
        /// Checks that input is a date is a valid DateTime value.
        /// </summary>
        /// <param name="textBox">text box to test</param>
        /// <returns>true if valide, false if not</returns>
        public static bool IsValidDate(TextBox textBox)
        {
            bool isValid = true; // innocent until proven guilty
            DateTime value; // the date time value provided in the text box
            if(textBox != null && !string.IsNullOrEmpty(textBox.Text) && textBox.Text != "N/A")
            {
                if (!DateTime.TryParse(textBox.Text, out value)) // if not a datetime value
                {
                    isValid = false;
                    MessageBox.Show($"{textBox.Tag} is not a valid date. Please try using the format, YYYY-MM-DD");
                    textBox.SelectAll();
                    textBox.Focus();
                }
            }
            return isValid;
        }
        /// <summary>
        /// check if commission value is less than the base price.
        /// </summary>
        /// <param name="Commission">commission textbox to check</param>
        /// <param name="BasePrice">base price textbox to compare</param>
        /// <returns>true if commission is less than base price, false if not</returns>
        public static bool IsValidCommission(TextBox Commission, TextBox BasePrice)
        {
            bool isValid = true;
            // Remove currency symbols and separators
            string firstInput = Commission.Text.Replace("$", "").Replace(",", "");
            string secondInput = BasePrice.Text.Replace("$", "").Replace(",", "");
            if (firstInput != null && !string.IsNullOrEmpty(Commission.Text))
            {
                if (Convert.ToDecimal(firstInput) >= Convert.ToDecimal(secondInput))
                {
                    isValid = false;
                    MessageBox.Show($"{Commission.Tag} must be less than the {BasePrice.Tag}");
                    Commission.SelectAll();
                    Commission.Focus();
                }
            }
            return isValid;
        }
        /// <summary>
        /// checks if the end date is later than the start date.
        /// </summary>
        /// <param name="endDate">end date textbox to check</param>
        /// <param name="startDate">start date text box to compare</param>
        /// <returns>true if end date is later, false if not</returns>
        public static bool IsValidEndDate(TextBox endDate, TextBox startDate)
        {
            bool isValid = true;
            if (endDate != null && !string.IsNullOrEmpty(endDate.Text) && endDate.Text != "N/A" && startDate != null && !string.IsNullOrEmpty(startDate.Text) && startDate.Text != "N/A")
            {
                if (Convert.ToDateTime(endDate.Text) < Convert.ToDateTime(startDate.Text))
                {
                    isValid = false;
                    MessageBox.Show($"{endDate.Tag} must be later than the {startDate.Tag}.");
                    endDate.SelectAll();
                    endDate.Focus();
                }
            }
            return isValid;
        }
    } // class
} // namespace
