using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Linq;

namespace library_project
{
    public partial class BookInstancesForm : Form
    {
        private SQLiteConnection connection;
        private DataTable bookDataTable;
        private DataTable dataTable;
        private DataTable instanceDataTable;
        private SQLiteDataAdapter bookDataAdapter;
        private SQLiteDataAdapter instanceDataAdapter;
        private DataTable authorTable; // Добавлено объявление переменной authorTable
        private DataTable bookGroupTable; // Добавлено объявление переменной bookGroupTable
        private DataTable publisherTable;
        private DataTable searchBookGroupTable;
        private DataTable searchAuthorTable;



        public BookInstancesForm()
        {
            InitializeComponent();
            this.Load += BookInstancesForm_Load;
            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;
        }

        private void BookInstancesForm_Load(object sender, EventArgs e)
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

            // Создание таблицы "BookInstances", если она не существует
            string createInstanceTableQuery = "CREATE TABLE IF NOT EXISTS BookInstances (Id INTEGER PRIMARY KEY, BookId INTEGER, AcquisitionDate TEXT, LibraryNumber TEXT, IsAvailable INTEGER, IsLost INTEGER)";
            using (SQLiteCommand createInstanceTableCommand = new SQLiteCommand(createInstanceTableQuery, connection))
            {
                connection.Open();
                createInstanceTableCommand.ExecuteNonQuery();
                connection.Close();
            }

            // Создание адаптера для таблицы "BookInstances" и задание команд
            instanceDataAdapter = new SQLiteDataAdapter("SELECT * FROM BookInstances", connection);

            // Команда для добавления строк
            instanceDataAdapter.InsertCommand = new SQLiteCommand("INSERT INTO BookInstances (BookId, AcquisitionDate, LibraryNumber, IsAvailable, IsLost) VALUES (@BookId, @AcquisitionDate, @LibraryNumber, @IsAvailable, @IsLost)", connection);
            instanceDataAdapter.InsertCommand.Parameters.Add("@BookId", DbType.Int32, 0, "BookId");
            instanceDataAdapter.InsertCommand.Parameters.Add("@AcquisitionDate", DbType.String, 0, "AcquisitionDate");
            instanceDataAdapter.InsertCommand.Parameters.Add("@LibraryNumber", DbType.String, 0, "LibraryNumber");
            instanceDataAdapter.InsertCommand.Parameters.Add("@IsAvailable", DbType.Int32, 0, "IsAvailable");
            instanceDataAdapter.InsertCommand.Parameters.Add("@IsLost", DbType.Int32, 0, "IsLost");

            // Команда для обновления строк
            instanceDataAdapter.UpdateCommand = new SQLiteCommand("UPDATE BookInstances SET BookId = @BookId, AcquisitionDate = @AcquisitionDate, LibraryNumber = @LibraryNumber, IsAvailable = @IsAvailable, IsLost = @IsLost WHERE Id = @Id", connection);
            instanceDataAdapter.UpdateCommand.Parameters.Add("@BookId", DbType.Int32, 0, "BookId");
            instanceDataAdapter.UpdateCommand.Parameters.Add("@AcquisitionDate", DbType.String, 0, "AcquisitionDate");
            instanceDataAdapter.UpdateCommand.Parameters.Add("@LibraryNumber", DbType.String, 0, "LibraryNumber");
            instanceDataAdapter.UpdateCommand.Parameters.Add("@IsAvailable", DbType.Int32, 0, "IsAvailable");
            instanceDataAdapter.UpdateCommand.Parameters.Add("@IsLost", DbType.Int32, 0, "IsLost");
            instanceDataAdapter.UpdateCommand.Parameters.Add("@Id", DbType.Int32, 0, "Id").SourceVersion = DataRowVersion.Original;



            // Команда для удаления строк
            instanceDataAdapter.DeleteCommand = new SQLiteCommand("DELETE FROM BookInstances WHERE Id = @Id", connection);
            instanceDataAdapter.DeleteCommand.Parameters.Add("@Id", DbType.Int32, 0, "Id").SourceVersion = DataRowVersion.Original;


            // Загрузка данных из базы данных в DataTable
            LoadBookData();
            LoadInstanceData();
            LoadAuthorData();
            LoadBookGroupData();
            LoadPublisherData();


            dgvBooks.CellContentClick -= dgvBooks_CellContentClick;

            // Добавление обработчика события dgvBooks_CellContentClick
            dgvBooks.CellContentClick += dgvBooks_CellContentClick;

            // Отображение данных в DataGridView
            dgvBooks.DataSource = bookDataTable;
            dgvBookInstances.DataSource = instanceDataTable;

            // Изменение заголовков столбцов
            dgvBooks.Columns["Id"].Visible = false;
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

            dgvBookInstances.Columns["Id"].Visible = false;
            dgvBookInstances.Columns["BookId"].Visible = false;
            dgvBookInstances.Columns["AcquisitionDate"].HeaderText = "Дата получения";
            dgvBookInstances.Columns["LibraryNumber"].HeaderText = "Библиотечный номер";
            dgvBookInstances.Columns["IsAvailable"].HeaderText = "Доступна";
            dgvBookInstances.Columns["IsLost"].HeaderText = "Утеряна";


            dgvBookInstances.Columns.Remove("IsAvailable");
            dgvBookInstances.Columns.Remove("IsLost");
            // Создание столбца "Доступна"
            DataGridViewCheckBoxColumn availableColumn = new DataGridViewCheckBoxColumn();
            availableColumn.DataPropertyName = "IsAvailable";
            availableColumn.HeaderText = "Доступна";
            availableColumn.Name = "Available";
            dgvBookInstances.Columns.Add(availableColumn);

            // Создание столбца "Утеряна"
            DataGridViewCheckBoxColumn lostColumn = new DataGridViewCheckBoxColumn();
            lostColumn.DataPropertyName = "IsLost";
            lostColumn.HeaderText = "Утеряна";
            lostColumn.Name = "Lost";
            dgvBookInstances.Columns.Add(lostColumn);

            DataGridViewButtonColumn addInstanceColumn = new DataGridViewButtonColumn();
            addInstanceColumn.Name = "AddInstance";
            addInstanceColumn.HeaderText = "Добавить экземпляр";
            addInstanceColumn.Text = "Добавить";
            addInstanceColumn.UseColumnTextForButtonValue = true;

            // Добавление столбца "Добавить экземпляр" в конец таблицы
            dgvBooks.Columns.Insert(dgvBooks.Columns.Count, addInstanceColumn);

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

        private void LoadBookData()
        {
            string selectQuery = "SELECT * FROM Books";

            bookDataTable = new DataTable();
            bookDataAdapter = new SQLiteDataAdapter(selectQuery, connection);
            bookDataAdapter.Fill(bookDataTable);
        }

        private void LoadInstanceData()
        {
            string selectQuery = "SELECT * FROM BookInstances";

            instanceDataTable = new DataTable();
            instanceDataAdapter = new SQLiteDataAdapter(selectQuery, connection);
            instanceDataAdapter.Fill(instanceDataTable);
        }

        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                // Получение выбранной строки
                DataGridViewRow selectedRow = dgvBooks.SelectedRows[0];

                // Получение значения поля "Id" выбранной книги
                int bookId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Фильтрация данных в таблице "BookInstances" по выбранной книге
                instanceDataTable.DefaultView.RowFilter = $"BookId = {bookId}";
            }
        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверка, что была нажата кнопка "Добавить экземпляр"
            if (e.ColumnIndex == dgvBooks.Columns["AddInstance"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvBooks.Rows[e.RowIndex];
                int bookId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Открытие формы для добавления экземпляра
                AddInstanceForm addInstanceForm = new AddInstanceForm(bookId, connection);
                addInstanceForm.ShowDialog();

                // Обновление данных экземпляров после добавления
                LoadInstanceData();
                instanceDataTable.DefaultView.RowFilter = $"BookId = {bookId}";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Создание команды удаления
            string deleteCommandText = "DELETE FROM BookInstances WHERE Id = @Id";
            SQLiteCommand deleteCommand = new SQLiteCommand(deleteCommandText, connection);
            deleteCommand.Parameters.Add("@Id", DbType.Int32, 0, "Id").SourceVersion = DataRowVersion.Original;

            // Присвоение команды удаления свойству DeleteCommand у instanceDataAdapter
            instanceDataAdapter.DeleteCommand = deleteCommand;

            // Создание команды вставки
            string insertCommandText = "INSERT INTO BookInstances (BookId, AcquisitionDate, LibraryNumber, IsAvailable, IsLost) VALUES (@BookId, @AcquisitionDate, @LibraryNumber, @IsAvailable, @IsLost)";
            SQLiteCommand insertCommand = new SQLiteCommand(insertCommandText, connection);
            insertCommand.Parameters.Add("@BookId", DbType.Int32, 0, "BookId");
            insertCommand.Parameters.Add("@AcquisitionDate", DbType.String, 0, "AcquisitionDate");
            insertCommand.Parameters.Add("@LibraryNumber", DbType.String, 0, "LibraryNumber");
            insertCommand.Parameters.Add("@IsAvailable", DbType.Int32, 0, "IsAvailable");
            insertCommand.Parameters.Add("@IsLost", DbType.Int32, 0, "IsLost");

            // Присвоение команды вставки свойству InsertCommand у instanceDataAdapter
            instanceDataAdapter.InsertCommand = insertCommand;

            // Создание команды обновления (необходимо только если команда обновления уже не была создана в другом месте)
            string updateCommandText = "UPDATE BookInstances SET BookId = @BookId, AcquisitionDate = @AcquisitionDate, LibraryNumber = @LibraryNumber, IsAvailable = @IsAvailable, IsLost = @IsLost WHERE Id = @Id";
            SQLiteCommand updateCommand = new SQLiteCommand(updateCommandText, connection);
            updateCommand.Parameters.Add("@BookId", DbType.Int32, 0, "BookId");
            updateCommand.Parameters.Add("@AcquisitionDate", DbType.String, 0, "AcquisitionDate");
            updateCommand.Parameters.Add("@LibraryNumber", DbType.String, 0, "LibraryNumber");
            updateCommand.Parameters.Add("@IsAvailable", DbType.Int32, 0, "IsAvailable");
            updateCommand.Parameters.Add("@IsLost", DbType.Int32, 0, "IsLost");
            updateCommand.Parameters.Add("@Id", DbType.Int32, 0, "Id").SourceVersion = DataRowVersion.Original;

            // Присвоение команды обновления свойству UpdateCommand у instanceDataAdapter
            instanceDataAdapter.UpdateCommand = updateCommand;

            // Сохранение изменений в базе данных
            instanceDataAdapter.Update(instanceDataTable);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            // Отменить все изменения в таблице экземпляров
            instanceDataTable.RejectChanges();

            // Закрыть форму
            this.Close();
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

            // Проверка на null и инициализация dataTable
            if (bookDataTable == null)
            {
                bookDataTable = new DataTable();
            }

            // Фильтрация данных в DataTable
            bookDataTable.DefaultView.RowFilter = searchCondition;

            // Очистка полей после поиска
            cmbAuthor.SelectedIndex = -1;
            txtName.Clear();
            txtPublicationYear.Clear();
            cmbGroup.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Обновление всех данных
            LoadBookData();
            LoadInstanceData();
            LoadAuthorData();
            LoadBookGroupData();
            LoadPublisherData();

            // Обновление источников данных для DataGridView
            dgvBooks.DataSource = bookDataTable;
            dgvBookInstances.DataSource = instanceDataTable;

            // Очистка полей поиска
            cmbAuthor.SelectedIndex = -1;
            txtName.Clear();
            txtPublicationYear.Clear();
            cmbGroup.SelectedIndex = -1;
        }
    }
}
