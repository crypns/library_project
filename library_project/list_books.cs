using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;

namespace library_project
{
    public partial class list_books : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private DataTable dataTable;
        private DataTable authorTable;
        private DataTable bookGroupTable;
        private DataTable publisherTable;
        private DataTable searchAuthorTable;
        private DataTable searchBookGroupTable;

        public list_books()
        {
            InitializeComponent();
            this.Load += list_books_Load; // Привязываем событие list_books_Load к событию загрузки формы
        }

        private void list_books_Load(object sender, EventArgs e)
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

            // Создание таблицы "Books", если она не существует
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Books (Id INTEGER PRIMARY KEY, AuthorId INTEGER, Name TEXT, PublicationYear INTEGER, RegistrationYear INTEGER, GroupId INTEGER, PublisherId INTEGER)";
            using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
            {
                connection.Open();
                createTableCommand.ExecuteNonQuery();
                connection.Close();
            }

            // Загрузка данных из базы данных в DataTable
            LoadData();

            // Загрузка данных для выпадающих списков
            LoadAuthorData();
            LoadBookGroupData();
            LoadPublisherData();

            // Создание команды удаления
            string deleteCommandQuery = "DELETE FROM Books WHERE Id = @Id";
            SQLiteCommand deleteCommand = new SQLiteCommand(deleteCommandQuery, connection);
            deleteCommand.Parameters.Add("@Id", DbType.Int32, 0, "Id");

            // Создание объекта SQLiteCommandBuilder для автоматической генерации команд обновления
            using (SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter))
            {
                // Задаем команду удаления
                dataAdapter.DeleteCommand = deleteCommand;

                // Отображение данных в DataGridView
                dgvBooks.DataSource = dataTable;

                // ...
            }

            // Отображение данных в DataGridView
            dgvBooks.DataSource = dataTable;

            dgvBooks.Columns["Id"].Visible = false;

            // Изменение заголовков столбцов
            dgvBooks.Columns["AuthorId"].Visible = false;
            dgvBooks.Columns["Name"].HeaderText = "Название";
            dgvBooks.Columns["PublicationYear"].HeaderText = "Год публикации";
            dgvBooks.Columns["RegistrationYear"].HeaderText = "Год регистрации";
            dgvBooks.Columns["GroupId"].Visible = false;
            dgvBooks.Columns["PublisherId"].Visible = false;

            // Создание и настройка столбцов с выпадающими списками
            DataGridViewComboBoxColumn authorColumn = new DataGridViewComboBoxColumn();
            authorColumn.DataPropertyName = "AuthorId";
            authorColumn.HeaderText = "Автор";
            authorColumn.DisplayMember = "FullName";
            authorColumn.ValueMember = "Id";
            authorColumn.DataSource = authorTable;

            DataGridViewComboBoxColumn groupColumn = new DataGridViewComboBoxColumn();
            groupColumn.DataPropertyName = "GroupId";
            groupColumn.HeaderText = "Группа";
            groupColumn.DisplayMember = "Name";
            groupColumn.ValueMember = "Id";
            groupColumn.DataSource = bookGroupTable;

            DataGridViewComboBoxColumn publisherColumn = new DataGridViewComboBoxColumn();
            publisherColumn.DataPropertyName = "PublisherId";
            publisherColumn.HeaderText = "Издатель";
            publisherColumn.DisplayMember = "Name";
            publisherColumn.ValueMember = "Id";
            publisherColumn.DataSource = publisherTable;

            // Определение позиции для добавления столбцов с выпадающими списками
            int authorColumnIndex = 1; // Позиция столбца "AuthorId" в таблице
            int groupColumnIndex = 6; // Позиция столбца "RegistrationYear" в таблице
            int publisherColumnIndex = 7; // Позиция столбца "GroupId" в таблице

            // Добавление столбцов с выпадающими списками в DataGridView
            dgvBooks.Columns.Insert(authorColumnIndex, authorColumn);
            dgvBooks.Columns.Insert(groupColumnIndex, groupColumn);
            dgvBooks.Columns.Insert(publisherColumnIndex, publisherColumn);

            // Клонирование данных для выпадающих списков
            searchAuthorTable = authorTable.Clone();
            searchBookGroupTable = bookGroupTable.Clone();

            // Копирование данных во временные таблицы
            foreach (DataRow row in authorTable.Rows)
            {
                searchAuthorTable.ImportRow(row);
            }

            foreach (DataRow row in bookGroupTable.Rows)
            {
                searchBookGroupTable.ImportRow(row);
            }

            // Заполнение выпадающего списка авторов
            cmbAuthor.DataSource = authorTable;
            cmbAuthor.DisplayMember = "FullName";
            cmbAuthor.ValueMember = "Id";

            // Заполнение выпадающего списка групп
            cmbGroup.DataSource = bookGroupTable;
            cmbGroup.DisplayMember = "Name";
            cmbGroup.ValueMember = "Id";


            // Очистка полей после поиска
            cmbAuthor.SelectedIndex = -1;
            txtName.Clear();
            txtPublicationYear.Clear();
            cmbGroup.SelectedIndex = -1;
        }

        private void LoadData()
        {
            string selectQuery = "SELECT * FROM Books";
            dataAdapter = new SQLiteDataAdapter(selectQuery, connection);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
        }

        private void LoadAuthorData()
        {
            string selectQuery = "SELECT Id, Name, Surname, Patronymic, Surname || ' ' || substr(Name, 1, 1) || '. ' || substr(Patronymic, 1, 1) || '.' AS FullName FROM Authors";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectQuery, connection);
            authorTable = new DataTable();
            adapter.Fill(authorTable);
        }


        private void LoadBookGroupData()
        {
            string selectQuery = "SELECT Id, Name FROM BookGroups";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectQuery, connection);
            bookGroupTable = new DataTable();
            adapter.Fill(bookGroupTable);
        }

        private void LoadPublisherData()
        {
            string selectQuery = "SELECT Id, Name FROM Publishers";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectQuery, connection);
            publisherTable = new DataTable();
            adapter.Fill(publisherTable);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter))
            {
                // Открываем соединение с базой данных
                connection.Open();

                // Сохраняем изменения в базе данных
                dataAdapter.Update(dataTable);

                // Закрываем соединение с базой данных
                connection.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Закрытие формы без сохранения
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Формирование условий для поиска
            string authorCondition = cmbAuthor.SelectedValue != null ? $"AuthorId = {cmbAuthor.SelectedValue}" : string.Empty;
            string nameCondition = !string.IsNullOrWhiteSpace(txtName.Text) ? $"Name LIKE '%{txtName.Text}%'" : string.Empty;
            string publicationYearCondition = !string.IsNullOrWhiteSpace(txtPublicationYear.Text) ? $"PublicationYear = {txtPublicationYear.Text}" : string.Empty;
            string groupCondition = cmbGroup.SelectedValue != null ? $"GroupId = {cmbGroup.SelectedValue}" : string.Empty;

            // Сборка условий поиска
            List<string> conditions = new List<string> { authorCondition, nameCondition, publicationYearCondition, groupCondition };
            string searchCondition = string.Join(" AND ", conditions.Where(c => !string.IsNullOrWhiteSpace(c)));

            // Фильтрация данных в DataTable
            dataTable.DefaultView.RowFilter = searchCondition;

            // Обновление временных таблиц для выпадающих списков
            searchAuthorTable.Clear();
            searchBookGroupTable.Clear();

            // Копирование данных во временные таблицы из оригинальных таблиц
            foreach (DataRow row in authorTable.Rows)
            {
                searchAuthorTable.ImportRow(row);
            }

            foreach (DataRow row in bookGroupTable.Rows)
            {
                searchBookGroupTable.ImportRow(row);
            }

            // Очистка полей после поиска
            cmbAuthor.SelectedIndex = -1;
            txtName.Clear();
            txtPublicationYear.Clear();
            cmbGroup.SelectedIndex = -1;
        }

        private void cmbAuthor_DropDown(object sender, EventArgs e)
        {
            // Обновление данных в ComboBox для поиска
            cmbAuthor.DataSource = searchAuthorTable;
        }

        private void cmbGroup_DropDown(object sender, EventArgs e)
        {
            // Обновление данных в ComboBox для поиска
            cmbGroup.DataSource = searchBookGroupTable;
        }

        private void cmbPublisher_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvBooks_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли кнопка Delete
            if (e.KeyCode == Keys.Delete)
            {
                DataGridViewRow selectedRow = dgvBooks.SelectedRows[0];
                DataRowView selectedRowView = selectedRow.DataBoundItem as DataRowView;

                if (selectedRowView != null)
                {
                    // Удаляем выбранную строку из источника данных (dataTable)
                    selectedRowView.Row.Delete();

                    // Сохраняем изменения в базе данных
                    dataAdapter.Update(dataTable);
                }
            }
        }
    }
}
