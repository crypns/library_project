using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace library_project
{
    public partial class book_groups : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private DataTable dataTable;

        public book_groups()
        {
            InitializeComponent();
            this.Load += book_groups_Load; // Привязываем событие book_groups_Load к событию загрузки формы
        }


        private void book_groups_Load(object sender, EventArgs e)
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

            // Создание таблицы "BookGroups", если она не существует
            string createTableQuery = "CREATE TABLE IF NOT EXISTS BookGroups (Id INTEGER PRIMARY KEY, Name TEXT)";
            using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
            {
                connection.Open();
                createTableCommand.ExecuteNonQuery();
                connection.Close();
            }

            // Загрузка данных из базы данных в DataTable
            LoadData();

            // Отображение данных в DataGridView
            dgvBookGroups.DataSource = new BindingSource(dataTable, null);

            dgvBookGroups.Columns["Id"].Visible = false;

            // Изменение заголовка столбца "Name" на "Название"
            dgvBookGroups.Columns["Name"].HeaderText = "Название";
        }

        private void LoadData()
        {
            string selectQuery = "SELECT Id, Name FROM BookGroups";
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
