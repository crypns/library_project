using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace library_project
{
    public partial class list_publishers : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private DataTable dataTable;

        public list_publishers()
        {
            InitializeComponent();
            this.Load += list_publishers_Load; // Привязываем событие list_publishers_Load к событию загрузки формы
        }

        private void list_publishers_Load(object sender, EventArgs e)
        {
            // Путь к файлу базы данных SQLite
            string dbPath = "library.db";

            // Проверка наличия файла базы данных
            if (!System.IO.File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath); // Создание новой базы данных, если файл не существует
            }

            // Подключение к базе данных SQLite
            string connectionString = $"Data Source={dbPath};Version=3;";
            connection = new SQLiteConnection(connectionString);

            // Создание таблицы "Publishers", если она не существует
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Publishers (Id INTEGER PRIMARY KEY, Name TEXT, City TEXT)";
            using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
            {
                connection.Open();
                createTableCommand.ExecuteNonQuery();
                connection.Close();
            }

            // Загрузка данных из базы данных в DataTable
            LoadData();

            // Отображение данных в DataGridView
            dgvPublishers.DataSource = new BindingSource(dataTable, null);

            dgvPublishers.Columns["Id"].Visible = false;

            // Изменение заголовка столбца "Name" на "Название"
            dgvPublishers.Columns["Name"].HeaderText = "Название";
            dgvPublishers.Columns["City"].HeaderText = "Город";
        }

        private void LoadData()
        {
            string selectQuery = "SELECT Id, Name, City FROM Publishers";
            dataAdapter = new SQLiteDataAdapter(selectQuery, connection);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Сохранение изменений в базе данных
            using (SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter))
            {
                connection.Open();
                dataAdapter.Update(dataTable);
                connection.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Закрытие формы без сохранения
            this.Close();
        }
    }
}
