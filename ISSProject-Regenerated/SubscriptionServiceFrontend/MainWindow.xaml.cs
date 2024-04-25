using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ISSProject.Common.Mock;
using ISSProject_Regenerated.SubscriptionServiceBackend.Controllers;
using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;
using SubscriptionServicePart.MVVM.ViewModel;

namespace SubscriptionServicePart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string creditCardHolder = string.Empty;
        private string creditCardNumber = string.Empty;
        private string expirationDate = string.Empty;
        private string cVV = string.Empty;
        private bool validCreditCradNumber = false;
        private bool validExpirationDate = false;
        private bool validCVV = false;
        public MainWindow(MainViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            creditCardHolder = NameTextBox.Text.Trim();
            creditCardNumber = CardNumberTextBox.Text.Trim();
            expirationDate = ExpirationDateTextBox.Text.Trim();
            cVV = CVVTextBox.Text.Trim();
            if ((bool)this.DataContext.GetType().GetMethod("ValidCreditCardInformation").Invoke(this.DataContext, new object[] { creditCardNumber, cVV, expirationDate }) == true)
            {
                var result = MessageBox.Show("ok");
                Console.WriteLine("[+] SUCCESSFULLY VALIDATED DATA. PREPARING TO COMMIT TO DATABASE");
                this.DataContext.GetType().GetMethod("SaveCreditCard").Invoke(this.DataContext, new object[] { creditCardHolder, creditCardNumber, cVV, expirationDate });
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
