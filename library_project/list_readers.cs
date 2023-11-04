using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace library_project
{
    public partial class list_readers : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private DataTable dataTable;

        public list_readers()
        {
            InitializeComponent();
            this.Load += list_readers_Load;
        }

        private void list_readers_Load(object sender, EventArgs e)
        {
            // Путь к файлу базы данных SQLite
            string dbPath = "library.db";

            // Подключение к базе данных SQLite
            string connectionString = $"Data Source={dbPath};Version=3;";
            connection = new SQLiteConnection(connectionString);

            // Создание таблицы "Readers", если она не существует
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Readers (Id INTEGER PRIMARY KEY, LastName TEXT, FirstName TEXT, MiddleName TEXT, IsTeacher INTEGER, Information TEXT)";
            using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
            {
                connection.Open();
                createTableCommand.ExecuteNonQuery();
                connection.Close();
            }

            // Загрузка данных из базы данных в DataTable
            LoadData();

            // Отображение данных в DataGridView
            dgvReaders.DataSource = dataTable;

            dgvReaders.Columns["Id"].Visible = false;
            dgvReaders.Columns["LastName"].HeaderText = "Фамилия";
            dgvReaders.Columns["FirstName"].HeaderText = "Имя";
            dgvReaders.Columns["MiddleName"].HeaderText = "Отчество";
            dgvReaders.Columns["IsTeacher"].HeaderText = "Является учителем";
            dgvReaders.Columns["Information"].HeaderText = "Информация";


            // Удаление столбца с типом данных INTEGER
            dgvReaders.Columns.Remove("IsTeacher");

            // Создание столбца с галочкой для поля "IsTeacher"
            DataGridViewCheckBoxColumn isTeacherColumn = new DataGridViewCheckBoxColumn();
            isTeacherColumn.DataPropertyName = "IsTeacher";
            isTeacherColumn.HeaderText = "Является учителем";
            isTeacherColumn.Name = "IsTeacher";
            dgvReaders.Columns.Add(isTeacherColumn);

            // Установка ширины столбца с галочкой
            dgvReaders.Columns["IsTeacher"].Width = 80;
        }

        private void LoadData()
        {
            string selectQuery = "SELECT * FROM Readers";
            dataAdapter = new SQLiteDataAdapter(selectQuery, connection);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter))
                {
                    // Установка команды InsertCommand
                    commandBuilder.GetInsertCommand();

                    // Установка команды UpdateCommand
                    commandBuilder.GetUpdateCommand();

                    // Сохраняем изменения в базе данных
                    dataAdapter.Update(dataTable);
                }
            }
            catch (DBConcurrencyException ex)
            {
                // Перезагрузка данных из базы данных
                LoadData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Закрытие формы без сохранения
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Получаем значение из TextBox и состояние CheckBox
            string searchText = txtSearch.Text.Trim();
            bool searchIsTeacher = chkIsTeacher.Checked;

            // Формируем строку фильтрации
            string filterExpression = "";
            if (!string.IsNullOrEmpty(searchText))
            {
                filterExpression += $"LastName LIKE '%{searchText}%' OR FirstName LIKE '%{searchText}%' OR MiddleName LIKE '%{searchText}%'";
            }

            if (searchIsTeacher)
            {
                if (!string.IsNullOrEmpty(filterExpression))
                {
                    filterExpression = $"({filterExpression}) AND IsTeacher = 1";
                }
                else
                {
                    filterExpression = "IsTeacher = 1";
                }
            }

            // Применяем фильтрацию к DataTable
            dataTable.DefaultView.RowFilter = filterExpression;
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            // Сбрасываем фильтр
            dataTable.DefaultView.RowFilter = "";

            // Очищаем текстовое поле и снимаем флажок с чекбокса
            txtSearch.Text = "";
            chkIsTeacher.Checked = false;
        }

        private void btnHistoryForm_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли выбранная строка
            if (dgvReaders.SelectedRows.Count > 0)
            {
                // Получаем значение ячейки "Id"
                object idValue = dgvReaders.SelectedRows[0].Cells["Id"].Value;

                // Проверяем, является ли значение пустым
                if (idValue != DBNull.Value)
                {
                    // Пытаемся преобразовать значение к типу int
                    if (int.TryParse(idValue.ToString(), out int readerId))
                    {
                        // Открываем новую форму ReaderHistoryForm и передаем ей Id читателя
                        ReaderHistoryForm historyForm = new ReaderHistoryForm(readerId);
                        historyForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Невозможно получить Id читателя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите читателя из списка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
