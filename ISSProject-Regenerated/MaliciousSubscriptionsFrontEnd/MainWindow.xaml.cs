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
using ISSProject.CompanyForm.Controller;

namespace GUICompanyForm
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

        private void ClearFormat(object sender, RoutedEventArgs e)
        {
            // Here is actually should just return to previous page
            // we don't currently have a previous page
            // so for now the cancel button will help with clearing the window of information
            this.companyName.Clear();
            this.emailAddress.Clear();
            this.validationToken.Clear();
            this.serviceAPI.Clear();
            this.additionalInfo.Clear();

            this.radioButtonServices.IsChecked = true;
            this.radioButtonSubscription.IsChecked = false;
            this.warningLabel.Visibility = Visibility.Hidden;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Takes the company information and saves it to the database
            int severity = 0;

            if (radioButtonSubscription.IsChecked.Value)
            {
                severity = 1;
            }

            ProcessedCompanyInformation controller = new ProcessedCompanyInformation(
            this.companyName.Text, this.serviceAPI.Text, severity, this.validationToken.Text);

            if (controller.ValidateCompanyToken())
            {
                if (controller.CommitTokenToDatabase())
                {
                    this.warningLabel.Visibility = Visibility.Hidden;
                    this.ClearFormat(sender, e);
                }
                else
                {
                    this.warningLabel.Content = "Errors at adding to the data base";
                    this.warningLabel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                this.warningLabel.Content = "Incorect company information. Please reformat";
                this.warningLabel.Visibility = Visibility.Visible;
            }
        }
    }
}