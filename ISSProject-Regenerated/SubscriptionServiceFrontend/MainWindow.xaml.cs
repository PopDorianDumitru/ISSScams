using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ISSProject.Common.Mock;
using ISSProject_Regenerated.SubscriptionServiceBackend;
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
        private string cvvPattern = @"^\d{3}$";
        private bool validCreditCradNumber = false;
        private bool validExpirationDate = false;
        private bool validCVV = false;
        private ICreditCardValidatorService creditCardInformationValidator;
        private List<string> vendorIdentifier = new List<string>();
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
            validCreditCradNumber = creditCardInformationValidator.ValidCreditCardNumber(creditCardNumber);
            validExpirationDate = creditCardInformationValidator.ValidExpirationDate(expirationDate);
            validCVV = creditCardInformationValidator.ValidCVV(cvvPattern, cVV);

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
