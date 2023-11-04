using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace library_project
{
    public partial class list_authors : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private DataTable dataTable;
        private DataView dataView;


        public list_authors()
        {
            InitializeComponent();
            this.Load += list_authors_Load; // Привязываем событие list_authors_Load к событию загрузки формы
        }

        private void list_authors_Load(object sender, EventArgs e)
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

            // Создание таблицы "Authors", если она не существует
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Authors (Id INTEGER PRIMARY KEY, Name TEXT, Surname TEXT, Patronymic TEXT)";
            using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
            {
                connection.Open();
                createTableCommand.ExecuteNonQuery();
                connection.Close();
            }

            // Загрузка данных из базы данных в DataTable
            LoadData();

            dataView = new DataView(dataTable);
            dgvAuthors.DataSource = dataView;

            dgvAuthors.Columns["Id"].Visible = false;

            // Изменение порядка столбцов и заголовков
            dgvAuthors.Columns["Surname"].DisplayIndex = 0;
            dgvAuthors.Columns["Surname"].HeaderText = "Фамилия";

            dgvAuthors.Columns["Name"].DisplayIndex = 1;
            dgvAuthors.Columns["Name"].HeaderText = "Имя";

            dgvAuthors.Columns["Patronymic"].DisplayIndex = 2;
            dgvAuthors.Columns["Patronymic"].HeaderText = "Отчество";
        }

        private void LoadData()
        {
            string selectQuery = "SELECT Id, Name, Surname, Patronymic FROM Authors";
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            // Формирование условия поиска
            string filterExpression = $"Name LIKE '%{searchText}%' OR Surname LIKE '%{searchText}%' OR Patronymic LIKE '%{searchText}%'";

            // Применение фильтра к DataView
            dataView.RowFilter = filterExpression;
        }
    }
}
