using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace Rental.Pages
{
    public partial class CarRentalManagement : Page
    {
        private string connectionString = Configuration.GetConnectionString();

        public CarRentalManagement()
        {
            InitializeComponent();
            LoadClientsData();
            LoadCarsData();
            LoadOrdersData();
        }

        // Загрузка данных из представлений
        private void LoadClientsData()
        {
            string query = "SELECT TOP (1000) [паспорт_id]\r\n      ,[Имя]\r\n      ,[Телефон]\r\n      ,[Электронная_почта]\r\n      ,[Скидка]\r\n  FROM [Rent].[dbo].[Список_Клиентов]";
            clientsDataGrid.ItemsSource = ExecuteQuery(query).DefaultView;
        }

        private void LoadCarsData()
        {
            string query = "SELECT TOP (1000) [Автомобиль_ID]\r\n      ,[Марка]\r\n      ,[Модель]\r\n      ,[Стоимость_за_день]\r\n      ,[Статус]\r\n  FROM [Rent].[dbo].[Список_Автомобилей]";
            carsDataGrid.ItemsSource = ExecuteQuery(query).DefaultView;
        }

        private void LoadOrdersData()
        {
            string query = "SELECT TOP (1000) [Заказ_ID]\r\n      ,[Клиент]\r\n      ,[Автомобиль]\r\n      ,[Модель]\r\n      ,[Дата_начала]\r\n      ,[Дата_окончания]\r\n      ,[Итоговая_стоимость]\r\n  FROM [Rent].[dbo].[Информация_О_Заказах]"; 
            ordersDataGrid.ItemsSource = ExecuteQuery(query).DefaultView;
        }

        private DataTable ExecuteQuery(string query, params (string param, object value)[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                foreach (var (param, value) in parameters)
                {
                    command.Parameters.AddWithValue(param, value ?? DBNull.Value);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        // Добавление клиента
        private void OnAddClientClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string имя = Microsoft.VisualBasic.Interaction.InputBox("Введите имя клиента", "Добавление клиента", "");
                if (string.IsNullOrEmpty(имя)) return;

                string телефон = Microsoft.VisualBasic.Interaction.InputBox("Введите телефон", "Добавление клиента", "");
                if (string.IsNullOrEmpty(телефон)) return;

                string электроннаяПочта = Microsoft.VisualBasic.Interaction.InputBox("Введите email", "Добавление клиента", "");
                if (string.IsNullOrEmpty(электроннаяПочта)) return;

                string паспортID = Microsoft.VisualBasic.Interaction.InputBox("Введите Паспорт ID клиента", "Добавление клиента", "");
                if (string.IsNullOrEmpty(паспортID)) return;

                string скидкаStr = Microsoft.VisualBasic.Interaction.InputBox("Введите скидку (процент)", "Добавление клиента", "0");
                decimal скидка = 0;
                if (!decimal.TryParse(скидкаStr, out скидка))
                {
                    MessageBox.Show("Неверный формат для скидки. Установлено значение 0.");
                    скидка = 0;
                }

                MessageBox.Show($"Параметры: Имя={имя}, Телефон={телефон}, Email={электроннаяПочта}, Паспорт={паспортID}, Скидка={скидка}");
                string query = "EXEC [dbo].[Добавить_Клиента] @ПаспортID, @Имя, @Телефон, @ЭлектроннаяПочта, @Скидка";

                ExecuteNonQuery(query,
                    ("@ПаспортID", паспортID),
                    ("@Имя", имя),
                    ("@Телефон", телефон),
                    ("@ЭлектроннаяПочта", электроннаяПочта),
                    ("@Скидка", скидка)
                );

                LoadClientsData();
            }
            catch (Exception ex)
            {
                // Обрабатываем ошибки
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}");
            }
        }

        private void OnEditClientClick(object sender, RoutedEventArgs e)
        {
            string passportId = Microsoft.VisualBasic.Interaction.InputBox("Введите Паспорт ID клиента для редактирования", "Редактирование клиента", "");
            if (string.IsNullOrEmpty(passportId)) return;

            string query = "SELECT [паспорт_id], [имя], [телефон], [электронная_почта], [скидка] FROM [dbo].[Клиенты] WHERE [паспорт_id] = @ПаспортID";
            DataTable clientData = ExecuteQuery(query, ("@ПаспортID", passportId));

            if (clientData.Rows.Count > 0)
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое имя клиента", "Редактирование клиента", clientData.Rows[0]["имя"].ToString());
                string newPhone = Microsoft.VisualBasic.Interaction.InputBox("Введите новый телефон клиента", "Редактирование клиента", clientData.Rows[0]["телефон"].ToString());
                string newEmail = Microsoft.VisualBasic.Interaction.InputBox("Введите новый email клиента", "Редактирование клиента", clientData.Rows[0]["электронная_почта"].ToString());

                string newDiscount = Microsoft.VisualBasic.Interaction.InputBox("Введите новый процент скидки", "Редактирование клиента", clientData.Rows[0]["скидка"].ToString());
                decimal discountValue = 0;
                if (!decimal.TryParse(newDiscount, out discountValue))
                {
                    MessageBox.Show("Неверный формат для скидки. Установлено значение 0.");
                    discountValue = 0;
                }

                string updateQuery = "EXEC [dbo].[Обновить_Клиента] @ПаспортID, @Имя, @Телефон, @ЭлектроннаяПочта, @Скидка";
                ExecuteNonQuery(updateQuery,
                    ("@ПаспортID", passportId),
                    ("@Имя", newName),
                    ("@Телефон", newPhone),
                    ("@ЭлектроннаяПочта", newEmail),
                    ("@Скидка", discountValue)
                );

                LoadClientsData();
            }
            else
            {
                MessageBox.Show("Клиент с таким Паспорт ID не найден.");
            }
        }

        private void OnDeleteClientClick(object sender, RoutedEventArgs e)
        {
            if (clientsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента для удаления!");
                return;
            }

            DataRowView selectedRow = clientsDataGrid.SelectedItem as DataRowView;
            string clientId = selectedRow["паспорт_id"].ToString();

            string query = "DELETE FROM [dbo].[Клиенты] WHERE [паспорт_id] = @ПаспортID";

            try
            {
                ExecuteNonQuery(query, ("@ПаспортID", clientId));
                LoadClientsData();
                MessageBox.Show("Клиент успешно удален");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }

        private void OnAddCarClick(object sender, RoutedEventArgs e)
        {
            string query = "EXEC [dbo].[Добавить_Автомобиль] @Марка, @Модель, @СтоимостьЗаДень, @Статус";
            ExecuteNonQuery(query,
                ("@Марка", "Введите марку автомобиля"),
                ("@Модель", "Введите модель"),
                ("@СтоимостьЗаДень", 1000),
                ("@Статус", "доступен")
            );
            LoadCarsData();
        }


        private void OnUpdateCarStatusClick(object sender, RoutedEventArgs e)
        {
            string carId = Microsoft.VisualBasic.Interaction.InputBox("Введите ID автомобиля для обновления статуса", "Обновление статуса автомобиля", "");
            if (string.IsNullOrEmpty(carId)) return;

            string newStatus = Microsoft.VisualBasic.Interaction.InputBox("Введите новый статус автомобиля", "Обновление статуса автомобиля", "");
            if (string.IsNullOrEmpty(newStatus)) return;

            string updateQuery = "EXEC [dbo].[Обновить_Статус_Автомобиля] @АвтомобильID, @Статус";
            ExecuteNonQuery(updateQuery,
                ("@АвтомобильID", carId),
                ("@Статус", newStatus)
            );

            LoadCarsData();
        }


        private void OnCreateOrderClick(object sender, RoutedEventArgs e)
        {
            string passportId = Microsoft.VisualBasic.Interaction.InputBox("Введите паспорт клиента", "Создание заказа", "");
            if (string.IsNullOrEmpty(passportId)) return;

            string clientQuery = "SELECT скидка FROM Клиенты WHERE паспорт_id = @ПаспортID";
            decimal clientDiscount = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(clientQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ПаспортID", passportId);
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        clientDiscount = Convert.ToDecimal(result);
                    }
                }
            }

            string carIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID автомобиля", "Создание заказа", "1");
            if (string.IsNullOrEmpty(carIdStr)) return;

            int carId;
            if (!int.TryParse(carIdStr, out carId))
            {
                MessageBox.Show("Неверный формат ID автомобиля.");
                return;
            }

            string startDateStr = Microsoft.VisualBasic.Interaction.InputBox("Введите дату начала аренды (yyyy-MM-dd)", "Создание заказа", DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime startDate;
            if (!DateTime.TryParse(startDateStr, out startDate))
            {
                MessageBox.Show("Неверный формат даты начала.");
                return;
            }

            string endDateStr = Microsoft.VisualBasic.Interaction.InputBox("Введите дату окончания аренды (yyyy-MM-dd)", "Создание заказа", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            DateTime endDate;
            if (!DateTime.TryParse(endDateStr, out endDate))
            {
                MessageBox.Show("Неверный формат даты окончания.");
                return;
            }

            string services = Microsoft.VisualBasic.Interaction.InputBox("Введите список услуг (через запятую, например: 1,2,3)", "Создание заказа", "");

            // Выполняем хранимую процедуру для создания заказа с учетом скидки
            string query = "EXEC [dbo].[Создать_Заказ] @ПаспортID, @АвтомобильID, @ДатаНачала, @ДатаОкончания, @Услуги";
            ExecuteNonQuery(query,
                ("@ПаспортID", passportId),
                ("@АвтомобильID", carId),
                ("@ДатаНачала", startDate),
                ("@ДатаОкончания", endDate),
                ("@Услуги", services)
            );

            LoadOrdersData();

            MessageBox.Show($"Заказ создан. Скидка клиента: {clientDiscount}%");
        }


        // Редактирование заказа
        private void OnEditOrderClick(object sender, RoutedEventArgs e)
        {
            string orderIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID заказа для редактирования", "Редактирование заказа", "");
            if (string.IsNullOrEmpty(orderIdStr)) return;

            int orderId;
            if (!int.TryParse(orderIdStr, out orderId))
            {
                MessageBox.Show("Неверный формат ID заказа.");
                return;
            }

            string newCarIdStr = Microsoft.VisualBasic.Interaction.InputBox("Введите новый ID автомобиля (или оставьте пустым, чтобы оставить прежний)", "Редактирование заказа", "");
            int? newCarId = string.IsNullOrEmpty(newCarIdStr) ? (int?)null : int.Parse(newCarIdStr);

            string newStartDateStr = Microsoft.VisualBasic.Interaction.InputBox("Введите новую дату начала аренды (yyyy-MM-dd)", "Редактирование заказа", DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime newStartDate;
            if (!DateTime.TryParse(newStartDateStr, out newStartDate))
            {
                MessageBox.Show("Неверный формат новой даты начала.");
                return;
            }

            string newEndDateStr = Microsoft.VisualBasic.Interaction.InputBox("Введите новую дату окончания аренды (yyyy-MM-dd)", "Редактирование заказа", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            DateTime newEndDate;
            if (!DateTime.TryParse(newEndDateStr, out newEndDate))
            {
                MessageBox.Show("Неверный формат новой даты окончания.");
                return;
            }

            string newServices = Microsoft.VisualBasic.Interaction.InputBox("Введите новый список услуг (через запятую, например: 1,2,3)", "Редактирование заказа", "");

            // Выполняем хранимую процедуру для редактирования заказа
            string query = "EXEC [dbo].[Редактировать_Заказ] @ЗаказID, @НовыйАвтомобильID, @НоваяДатаНачала, @НоваяДатаОкончания, @Услуги";
            ExecuteNonQuery(query,
                ("@ЗаказID", orderId),
                ("@НовыйАвтомобильID", newCarId),
                ("@НоваяДатаНачала", newStartDate),
                ("@НоваяДатаОкончания", newEndDate),
                ("@Услуги", newServices)
            );

            // Перезагружаем данные заказов после редактирования
            LoadOrdersData();
        }

        // Завершение аренды
        private void OnCompleteOrderClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Запрашиваем ID заказа у пользователя через всплывающее окно
                string idStr = Microsoft.VisualBasic.Interaction.InputBox("Введите ID заказа", "Завершение аренды", "");

                // Проверяем, что введено значение и оно является числом
                if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int orderId))
                {
                    MessageBox.Show("Неверный формат ID. Попробуйте еще раз.");
                    return;
                }

                // Выполнение хранимой процедуры с передачей параметра
                string query = "EXEC [dbo].[Завершить_Аренду] @ЗаказID";
                ExecuteNonQuery(query, ("@ЗаказID", orderId));

                // Перезагрузка данных заказов после завершения аренды
                LoadOrdersData();
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при завершении аренды: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                // Например, вывод в консоль или логирование в файл:
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");

                // Если необходимо, можно перебросить исключение или обработать по-другому
                // throw; // выбрасывает исключение дальше
            }
        }


        // Метод для выполнения хранимой процедуры и отображения результатов в таблице
        private void OnShowCarAvailabilityClick(object sender, RoutedEventArgs e)
        {
            string query = "EXEC [dbo].[Наличие_авто]"; // Запрос к хранимой процедуре

            // Получаем данные из базы данных
            DataTable availabilityData = ExecuteQuery(query);

            // Отображаем данные в DataGrid
            carAvailabilityDataGrid.ItemsSource = availabilityData.DefaultView;
        }

        // Метод для выполнения хранимой процедуры и отображения результатов популярности автомобилей
        private void OnShowCarPopularityClick(object sender, RoutedEventArgs e)
        {
            string query = "EXEC [dbo].[Популярность_авто]"; // Запрос к хранимой процедуре

            // Получаем данные из базы данных
            DataTable popularityData = ExecuteQuery(query);

            // Отображаем данные в DataGrid
            carPopularityDataGrid.ItemsSource = popularityData.DefaultView;
        }
        // Метод для выполнения хранимой процедуры и отображения сведений о клиенте
        private void OnShowClientDetailsClick(object sender, RoutedEventArgs e)
        {
            // Проверяем наличие выбранного клиента в таблице
            if (clientsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента из таблицы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Получаем данные выбранной строки
            DataRowView selectedClient = (DataRowView)clientsDataGrid.SelectedItem;

            // Предполагаем, что столбец с ФИО называется "клиент_имя" (уточните имя вашего столбца)
            string clientFIO = selectedClient["клиент_имя"].ToString();

            // Запрос к хранимой процедуре
            string query = "EXEC [dbo].[Сведения_О_Клиенте] @ФИО";
            DataTable clientDetails = ExecuteQuery(query, ("@ФИО", clientFIO));

            if (clientDetails.Rows.Count > 0)
            {
                // Формируем сообщение со сведениями
                var clientInfo = clientDetails.Rows[0];
                string clientInfoMessage = $"Имя: {clientInfo["клиент_имя"]}\n" +
                                           $"Телефон: {clientInfo["клиент_телефон"]}\n" +
                                           $"Электронная почта: {clientInfo["клиент_почта"]}\n" +
                                           $"Количество аренд: {clientInfo["количество_аренд"]}\n" +
                                           $"Автомобили в прокате: {clientInfo["автомобили_в_прокате"]}";

                MessageBox.Show(clientInfoMessage, "Сведения о клиенте", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Сведения о клиенте не найдены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ordersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
