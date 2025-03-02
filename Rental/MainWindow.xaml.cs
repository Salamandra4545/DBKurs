using Rental.Pages;
using System.Windows;

namespace Rental
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewTablesButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем страницу AllTable в правой части окна
            MainFrame.Navigate(new AllTable());
        }

        private void ClientsPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем другую страницу (можно заменить на реальную)
            MainFrame.Navigate(new CarRentalManagement());
        }

        private void CarsPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем другую страницу (можно заменить на реальную)
            MainFrame.Navigate(new DiscountAndPricingManagement());
        }

        private void FinancialPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем другую страницу (можно заменить на реальную)
            MainFrame.Navigate(new FinancialReporting());
        }
    }
}
