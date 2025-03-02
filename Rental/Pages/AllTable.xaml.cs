using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Rental.Pages
{
    public partial class AllTable : Page
    {
        private string connectionString;
        private DataTable currentDataTable;
        private string currentTableName;

        public AllTable()
        {
            InitializeComponent();
            connectionString = Configuration.GetConnectionString();
            LoadTables();
        }

        private void LoadTables()
        {
            try
            {
                List<string> tableNames = new List<string>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'dbo'",
                        connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tableNames.Add(reader["TABLE_NAME"].ToString());
                    }
                }

                TablesList.ItemsSource = tableNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке таблиц: {ex.Message}");
            }
        }

        private void TablesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TablesList.SelectedItem is string selectedTable)
            {
                currentTableName = selectedTable;
                LoadTableData(selectedTable);
            }
        }

        private void LoadTableData(string tableName)
        {
            try
            {
                currentDataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Экранируем имя таблицы
                    string query = $"SELECT * FROM [{tableName}]";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.Fill(currentDataTable);
                }

                TableDataGrid.ItemsSource = currentDataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных таблицы '{tableName}': {ex.Message}");
            }
        }

        private void DeleteRowButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableDataGrid.SelectedItem is DataRowView selectedRow)
            {
                try
                {
                    DataRow row = selectedRow.Row;
                    currentDataTable.Rows.Remove(row);

                    // Удаление строки из базы данных
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand($"DELETE FROM [{currentTableName}] WHERE id = @ID", connection);
                        command.Parameters.AddWithValue("@ID", row["id"]); // "id" должно быть корректным ключевым полем
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении строки: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM [{currentTableName}]", connection);

                    // Создаём команду для обновления
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                    // Обновляем изменения в базе данных
                    adapter.Update(currentDataTable);
                    MessageBox.Show("Изменения сохранены!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }

        private void TableDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                // Здесь можно обработать конкретные изменения ячеек, если нужно
                // Например, можно добавить дополнительные проверки данных
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании ячейки: {ex.Message}");
            }
        }
    }
}
