using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Media;

using Microsoft.Data.SqlClient;
using ISSProject;
using ISSProject_Regenerated.ScamBotsPhishingFrontend.ScamBotsPhishingService;
namespace Credit_card_donation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Name
        private void TextNameChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBlockName.Text) && TextBlockName.Text.Length > 0)
            {
                TextBlockName.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockName.Visibility = Visibility.Visible;
            }
        }

        private void TextNameMD(object sender, RoutedEventArgs e)
        {
            TextBoxName.Focus();
        }

        // CreditNr
        private void TextCreditNrChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBlockCreditNr.Text) && TextBlockCreditNr.Text.Length > 0)
            {
                TextBlockCreditNr.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockCreditNr.Visibility = Visibility.Visible;
            }
        }

        private void TextCreditNrMD(object sender, RoutedEventArgs e)
        {
            TextBoxCreditNr.Focus();
        }

        // CVV
        private void TextCVVChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBlockCVV.Text) && TextBlockCVV.Text.Length > 0)
            {
                TextBlockCVV.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockCVV.Visibility = Visibility.Visible;
            }
        }

        private void TextCVVMD(object sender, RoutedEventArgs e)
        {
            TextBoxCVV.Focus();
        }

        // ExpYear
        private void TextExpYearChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBlockExpYear.Text) && TextBlockExpYear.Text.Length > 0)
            {
                TextBlockExpYear.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextBlockExpYear.Visibility = Visibility.Visible;
            }
        }

        private void TextExpYearMD(object sender, RoutedEventArgs e)
        {
            TextBoxExpYear.Focus();
        }

        private bool ContainsOnlyLetters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != '-' && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }
        private bool ContainsOnlyNumbers(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsExpirationDateFormatValid(string input)
        {
            string[] parts = input.Split('/');

            if (parts.Length != 2)
            {
                return false;
            }

            if (!ContainsOnlyNumbers(parts[0]) || !ContainsOnlyNumbers(parts[1]))
            {
                return false;
            }

            int month = int.Parse(parts[0]);
            int year = int.Parse(parts[1]);

            if (month < 1 || month > 12)
            {
                return false;
            }

            // checking over 24 to be valid card, you can refactor this if needed (you = razvan)
            if (parts[1].Length == 2 && year <= 24)
                {
                return false;
            }

            // checking over 2024 to be valid card, you can refactor this if needed (you = razvan)
            if (parts[1].Length == 4 && year <= 2024)
            {
                return false;
            }

            return true;
        }

        private void DonateButton_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBoxName.Text;
            string creditNr = TextBoxCreditNr.Text;
            string cvv = TextBoxCVV.Text;
            string expYear = TextBoxExpYear.Text;
            int randomCCId = new Random().Next(1000);   // YOU CAN REFACTOR IF NEEDED
            int randomUserId = 65;

            if (!ContainsOnlyLetters(name) || name.Length == 0)
            {
                MessageBox.Show("Name can only contain letters.");
                return;
            }

            if (!ContainsOnlyNumbers(creditNr) || creditNr.Length != 16)
            {
                MessageBox.Show("Credit card number must be 16 digits long with no letters.");
                return;
            }

            if (!ContainsOnlyNumbers(cvv) || cvv.Length != 3)
            {
                MessageBox.Show("CVV must be exactly 3 digits long.");
                return;
            }

            if (!IsExpirationDateFormatValid(expYear))
            {
                MessageBox.Show("Expiration year must be of the form MM/YY with year > 24 or MM/YYYY with year > 2024.");
                return;
            }

            try
            {
                ScamBotsPhishingService.AddToDatabase(name,
                                                      creditNr,
                                                      cvv,
                                                      expYear,
                                                      randomUserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred " + ex.Message);
                // MessageBox.Show("An error occurred: " + ex.Message);
            }
            SoundPlayer sound = new SoundPlayer("../../../ScamBotsPhishingFrontend/vine-boom.wav");
            sound.Play();
        }
    }
}