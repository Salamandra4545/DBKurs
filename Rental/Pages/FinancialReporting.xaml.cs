using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace Rental.Pages
{
    public partial class FinancialReporting : Page
    {
        private string connectionString = Configuration.GetConnectionString();

        public FinancialReporting()
        {
            InitializeComponent();
            LoadRevenueData(); // Загружаем данные о выручке по месяцам при инициализации страницы
        }

        // Метод для загрузки данных о выручке по месяцам
        private void LoadRevenueData()
        {
            string query = "SELECT TOP (1000) [Год],[Месяц],[Общая_выручка] FROM [Rent].[dbo].[Выручка_По_Месяцам]";
            DataTable revenueData = ExecuteQuery(query);
            revenueDataGrid.ItemsSource = revenueData.DefaultView;
        }

        // Метод для загрузки выручки по выбранной дате
        private decimal GetRevenueByDate(DateTime selectedDate)
        {
            string query = "EXEC [dbo].[Подсчитать_выручку_за_дату] @выбранная_дата";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@выбранная_дата", selectedDate);

                connection.Open();
                object result = command.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        // Обработчик изменения выбранной даты для выручки
        private void OnRevenueDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = revenueDatePicker.SelectedDate;
            if (selectedDate.HasValue)
            {
                decimal revenue = GetRevenueByDate(selectedDate.Value);
                revenueTextBlock.Text = $"Выручка на {selectedDate.Value.ToShortDateString()}: {revenue} руб.";
            }
            else
            {
                revenueTextBlock.Text = "Пожалуйста, выберите дату.";
            }
        }

        // Метод для выполнения SQL запроса
        private DataTable ExecuteQuery(string query, params (string param, object value)[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                foreach (var (param, value) in parameters)
                {
                    command.Parameters.AddWithValue(param, value ?? DBNull.Value); // Обрабатываем null значения
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        // Функция для расчета стоимости с учетом скидки
        private decimal CalculateDiscountedPrice(string passportId, int carId, DateTime startDate, DateTime endDate)
        {
            // Пример вычислений стоимости, здесь может быть вызов SQL функции или расчет по логике
            decimal basePrice = 1000m; // Примерная базовая цена
            decimal discount = 0.1m; // Пример скидки 10%

            // Пример расчета стоимости на основе даты аренды
            int rentalDays = (endDate - startDate).Days;
            decimal totalPrice = basePrice * rentalDays;

            // Применяем скидку
            decimal discountedPrice = totalPrice * (1 - discount);

            return discountedPrice;
        }

        // Метод для расчета стоимости с учетом скидки
        private void OnCalculatePriceClick(object sender, RoutedEventArgs e)
        {
            string passportId = passportTextBox.Text;

            // Попробуем преобразовать ID автомобиля в целое число
            if (!int.TryParse(carIdTextBox.Text, out int carId))
            {
                MessageBox.Show("Некорректный ID автомобиля.");
                return;
            }

            // Проверяем, что пользователь выбрал обе даты
            if (startDatePicker.SelectedDate.HasValue && endDatePicker.SelectedDate.HasValue)
            {
                DateTime startDate = startDatePicker.SelectedDate.Value;
                DateTime endDate = endDatePicker.SelectedDate.Value;

                // Проверяем, что дата окончания аренды не раньше даты начала
                if (endDate < startDate)
                {
                    MessageBox.Show("Дата окончания аренды не может быть раньше даты начала.");
                    return;
                }

                // Рассчитываем стоимость аренды с учетом скидки
                decimal price = CalculateDiscountedPrice(passportId, carId, startDate, endDate);
                calculatedPriceTextBlock.Text = $"Стоимость аренды с учетом скидки: {price} руб.";
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите обе даты.");
            }
        }

    }
}
