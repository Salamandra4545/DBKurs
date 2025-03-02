using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace Rental.Pages
{
    public partial class DiscountAndPricingManagement : Page
    {
        private string connectionString = Configuration.GetConnectionString();

        public DiscountAndPricingManagement()
        {
            InitializeComponent();
            LoadServicesData();
        }

        // Загрузка данных прайс-листа услуг
        private void LoadServicesData()
        {
            string query = "SELECT TOP 1000 [id], [название], [описание], [стоимость] FROM [Rent].[dbo].[Дополнительные_услуги]";
            servicesDataGrid.ItemsSource = ExecuteQuery(query).DefaultView;
        }

        private DataTable ExecuteQuery(string query, params (string param, object value)[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Добавляем параметры в команду
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

        // Добавление новой услуги
        private void OnAddServiceClick(object sender, RoutedEventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Введите название услуги", "Добавление услуги", "");
            string description = Microsoft.VisualBasic.Interaction.InputBox("Введите описание услуги", "Добавление услуги", "");
            string costStr = Microsoft.VisualBasic.Interaction.InputBox("Введите стоимость услуги", "Добавление услуги", "0");

            if (decimal.TryParse(costStr, out decimal cost))
            {
                string query = "EXEC [dbo].[Добавить_Услугу] @Название, @Описание, @Стоимость";
                ExecuteNonQuery(query,
                    ("@Название", name),
                    ("@Описание", description),
                    ("@Стоимость", cost)
                );
                LoadServicesData();
            }
            else
            {
                MessageBox.Show("Неверный формат стоимости.");
            }
        }

        // Удаление услуги
        private void OnDeleteServiceClick(object sender, RoutedEventArgs e)
        {
            string serviceId = Microsoft.VisualBasic.Interaction.InputBox("Введите ID услуги для удаления", "Удаление услуги", "");
            if (string.IsNullOrEmpty(serviceId)) return;

            string query = "DELETE FROM [Rent].[dbo].[Дополнительные_услуги] WHERE [id] = @id";
            ExecuteNonQuery(query, ("@id", serviceId));
            LoadServicesData();
        }

        private void ExecuteNonQuery(string query, params (string param, object value)[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var (param, value) in parameters)
                    {
                        command.Parameters.AddWithValue(param, value ?? DBNull.Value); // Обрабатываем null значения
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Логируем или выводим сообщение об ошибке
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                MessageBox.Show("Ошибка при выполнении запроса. Проверьте данные.");
            }
        }
    }
}
