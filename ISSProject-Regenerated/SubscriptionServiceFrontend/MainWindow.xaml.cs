using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ISSProject.Common.Mock;
using SubscriptionServicePart.MVVM.ViewModel;

namespace SubscriptionServicePart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MockUser userToInsertCreditCard = new MockUser(58, "8h717xii2u8", "nicol.nikolaus@yahoo.com", "Johnathan", "Bins", DateTime.Now, "+40780880054");

        private string creditCardHolder = string.Empty;
        private string creditCardNumber = string.Empty;
        private string expirationDate = string.Empty;
        private string cVV = string.Empty;
        private string numberPattern = @"^(?:\d[ -]*?){16}$";
        private string cvvPattern = @"^\d{3}$";
        private string[] dateComponent;
        private bool validCreditCradNumber = false;
        private bool validExpirationDate = false;
        private bool validCVV = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            creditCardHolder = NameTextBox.Text.Trim();
            creditCardNumber = CardNumberTextBox.Text.Trim();
            expirationDate = ExpirationDateTextBox.Text.Trim();
            cVV = CVVTextBox.Text.Trim();

            Regex regexCard = new Regex(numberPattern);
            MatchCollection matchesCardNumber = regexCard.Matches(creditCardNumber);
            if (matchesCardNumber.Count > 0)
            {
                validCreditCradNumber = true;
            }

            dateComponent = expirationDate.Split('/');
            int month, year;

            var successfullyParsedMonth = int.TryParse(dateComponent[0], out month);
            var successfullyParsedYear = int.TryParse(dateComponent[1], out year);

            if (successfullyParsedMonth && successfullyParsedYear && dateComponent[0].Length == 2 && dateComponent[1].Length == 2)
            {
                if (month <= 12 && year >= 25)
                {
                    validExpirationDate = true;
                }
            }

            Regex regexCVV = new Regex(cvvPattern);
            MatchCollection matchesCVV = regexCVV.Matches(cVV);
            if (matchesCVV.Count > 0)
            {
                validCVV = true;
            }

            if (validCreditCradNumber && validExpirationDate && validCVV)
            {
                var result = MessageBox.Show("ok");
                Console.WriteLine("[+] SUCCESSFULLY VALIDATED DATA. PREPARING TO COMMIT TO DATABASE");
                CommitToDatabase.Commit(userToInsertCreditCard.GetId(), creditCardHolder, creditCardNumber, expirationDate, cVV);
            }
        }

        private void PayButton_MouseEnter(object sender, MouseEventArgs e)
        {
            PayButton.Background = new BrushConverter().ConvertFrom("#757fe0") as Brush;
            PayButton.Cursor = Cursors.Hand;
        }

        private void PayButton_MouseLeave(object sender, MouseEventArgs e)
        {
            PayButton.Background = Brushes.Black;
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "Name on card")
            {
                NameTextBox.Text = string.Empty;
                NameTextBox.BorderBrush = new BrushConverter().ConvertFrom("#828282") as Brush;
            }
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == string.Empty)
            {
                NameTextBox.Text = "Name on card";
                NameTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void CardNumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CardNumberTextBox.Text == "- - - -   - - - -   - - - -   - - - -")
            {
                CardNumberTextBox.Text = string.Empty;
                CardNumberTextBox.BorderBrush = new BrushConverter().ConvertFrom("#828282") as Brush;
            }
        }

        private void CardNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CardNumberTextBox.Text == string.Empty)
            {
                CardNumberTextBox.Text = "- - - -   - - - -   - - - -   - - - -";
                CardNumberTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void ExpirationDateTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ExpirationDateTextBox.Text == "MM/YY")
            {
                ExpirationDateTextBox.Text = string.Empty;
                ExpirationDateTextBox.BorderBrush = new BrushConverter().ConvertFrom("#828282") as Brush;
            }
        }

        private void ExpirationDateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ExpirationDateTextBox.Text == string.Empty)
            {
                ExpirationDateTextBox.Text = "MM/YY";
                ExpirationDateTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void CVVTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CVVTextBox.Text == "- - -")
            {
                CVVTextBox.Text = string.Empty;
                CVVTextBox.BorderBrush = new BrushConverter().ConvertFrom("#828282") as Brush;
            }
        }

        private void CVVTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CVVTextBox.Text == string.Empty)
            {
                CVVTextBox.Text = "- - -";
                CVVTextBox.BorderBrush = Brushes.Red;
            }
        }
    }
}
