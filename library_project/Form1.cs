using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

using System.Windows.Forms;
using System.Data.SQLite;

namespace library_project
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private SQLiteDataAdapter loansDataAdapter;
        private DataTable dataTable;
        private DataTable bookLoansTable;
        private SQLiteCommand deleteCommand;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Подключение к базе данных SQLite
            string connectionString = "Data Source=library.db;Version=3;";
            connection = new SQLiteConnection(connectionString);
            connection.Open();

            string createTableQuery = "CREATE TABLE IF NOT EXISTS BookLoans (Id INTEGER PRIMARY KEY, ReaderId INTEGER, BookId INTEGER, IssueDate TEXT, ReturnDate TEXT, Returned INTEGER)";
            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            // Создайте DataTable для хранения данных о выданных книгах
            bookLoansTable = new DataTable();

            loansDataAdapter = new SQLiteDataAdapter("SELECT * FROM BookLoans", connection);
          
            // Настройка команд обновления и вставки
            loansDataAdapter.UpdateCommand = new SQLiteCommand("UPDATE BookLoans SET ReaderId = @readerId, BookId = @bookId, IssueDate = @issueDate, ReturnDate = @returnDate, Returned = @returned WHERE Id = @loanId", connection);
            loansDataAdapter.UpdateCommand.Parameters.Add("@readerId", DbType.Int32, 0, "ReaderId");
            loansDataAdapter.UpdateCommand.Parameters.Add("@bookId", DbType.Int32, 0, "BookId");
            loansDataAdapter.UpdateCommand.Parameters.Add("@issueDate", DbType.String, 0, "IssueDate");
            loansDataAdapter.UpdateCommand.Parameters.Add("@returnDate", DbType.String, 0, "ReturnDate");
            loansDataAdapter.UpdateCommand.Parameters.Add("@loanId", DbType.Int32, 0, "LoanId");
            loansDataAdapter.UpdateCommand.Parameters.Add("@returned", DbType.Int32, 0, "Returned");



            loansDataAdapter.InsertCommand = new SQLiteCommand("INSERT INTO BookLoans (ReaderId, BookId, IssueDate, ReturnDate, Returned) VALUES (@readerId, @bookId, @issueDate, @returnDate, @returned)", connection);
            loansDataAdapter.InsertCommand.Parameters.Add("@readerId", DbType.Int32, 0, "ReaderId");
            loansDataAdapter.InsertCommand.Parameters.Add("@bookId", DbType.Int32, 0, "BookId");
            loansDataAdapter.InsertCommand.Parameters.Add("@issueDate", DbType.String, 0, "IssueDate");
            loansDataAdapter.InsertCommand.Parameters.Add("@returnDate", DbType.String, 0, "ReturnDate");
            loansDataAdapter.InsertCommand.Parameters.Add("@returned", DbType.Int32, 0, "Returned");

            loansDataAdapter.DeleteCommand = new SQLiteCommand("DELETE FROM BookLoans WHERE Id = @loanId", connection);
            loansDataAdapter.DeleteCommand.Parameters.Add("@loanId", DbType.Int32, 0, "LoanId");

            // Определите столбцы в DataTable
            bookLoansTable.Columns.Add("ReaderId", typeof(int));
            bookLoansTable.Columns.Add("ReaderFullName", typeof(string));
            bookLoansTable.Columns.Add("BookId", typeof(int));
            bookLoansTable.Columns.Add("BookName", typeof(string));
            bookLoansTable.Columns.Add("IssueDate", typeof(string));
            bookLoansTable.Columns.Add("Returned", typeof(int));
            bookLoansTable.Columns.Add("ReturnDate", typeof(string));

            // Создание скрытого столбца "Id"
            DataGridViewTextBoxColumn idColumn1 = new DataGridViewTextBoxColumn();
            idColumn1.Name = "Id";
            idColumn1.Visible = false; // Скрываем столбец
            dgvReaders.Columns.Add(idColumn1);

            // Создание столбца "Фамилия и инициалы"
            DataGridViewTextBoxColumn fullNameColumn = new DataGridViewTextBoxColumn();
            fullNameColumn.HeaderText = "Фамилия и инициалы";
            fullNameColumn.Name = "FullName";
            dgvReaders.Columns.Add(fullNameColumn);

            // Создание столбца "Преподаватель"
            DataGridViewCheckBoxColumn isTeacherColumn = new DataGridViewCheckBoxColumn();
            isTeacherColumn.DataPropertyName = "IsTeacher";
            isTeacherColumn.HeaderText = "Преподаватель";
            isTeacherColumn.Name = "IsTeacher";
            dgvReaders.Columns.Add(isTeacherColumn);

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "Id";
            idColumn.Visible = false; // Скрываем столбец
            dgvBooksAndInstances.Columns.Add(idColumn);

            // Создание столбца "Название книги"
            dgvBooksAndInstances.Columns.Add("BookName", "Название книги");
            // Создание столбца "Дата получения"
            dgvBooksAndInstances.Columns.Add("AcquisitionDate", "Дата получения");
            // Создание столбца "Номер в библиотеке"
            dgvBooksAndInstances.Columns.Add("LibraryNumber", "Номер в библиотеке");

            // Создание столбца "Доступна"
            DataGridViewCheckBoxColumn availableColumn = new DataGridViewCheckBoxColumn();
            availableColumn.DataPropertyName = "IsAvailable";
            availableColumn.HeaderText = "Доступна";
            availableColumn.Name = "IsAvailable";
            dgvBooksAndInstances.Columns.Add(availableColumn);

            // Создание столбца "Утеряна"
            DataGridViewCheckBoxColumn lostColumn = new DataGridViewCheckBoxColumn();
            lostColumn.DataPropertyName = "IsLost";
            lostColumn.HeaderText = "Утеряна";
            lostColumn.Name = "IsLost";
            dgvBooksAndInstances.Columns.Add(lostColumn);

            // Свяжите DataTable с dgvBookLoans
            dgvBookLoans.DataSource = bookLoansTable;

            // Перевод названий столбцов
            dgvBookLoans.Columns["ReaderId"].HeaderText = "ИД читателя";
            dgvBookLoans.Columns["ReaderFullName"].HeaderText = "ФИО читателя";
            dgvBookLoans.Columns["BookId"].HeaderText = "ИД книги";
            dgvBookLoans.Columns["BookName"].HeaderText = "Название книги";
            dgvBookLoans.Columns["IssueDate"].HeaderText = "Дата выдачи";
            dgvBookLoans.Columns["Returned"].HeaderText = "Возвращена";

            LoadReadersDataGridView();
            LoadBooksAndInstancesDataGridView();
            LoadBookLoansDataGridView();

        }
        private void LoadBookLoansDataGridView()
        {
            string query = "SELECT bl.Id, r.LastName || ' ' || r.FirstName || ' ' || r.MiddleName AS FullName, b.Name AS BookName, bl.IssueDate, bl.ReturnDate, bl.Returned " +
                               "FROM BookLoans bl " +
                               "INNER JOIN Readers r ON bl.ReaderId = r.Id " +
                               "INNER JOIN Books b ON bl.BookId = b.Id";

            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
            {

                dataTable = new DataTable();
                adapter.DeleteCommand = deleteCommand;

                adapter.Fill(dataTable);
                dataAdapter = adapter;
                dataAdapter.DeleteCommand = deleteCommand;

                dataTable.Columns["Id"].ColumnName = "LoanId";
                dataTable.Columns["FullName"].ColumnName = "ReaderFullName";
                dataTable.Columns["BookName"].ColumnName = "BookName";
                dataTable.Columns["IssueDate"].ColumnName = "IssueDate";
                dataTable.Columns["ReturnDate"].ColumnName = "ReturnDate";
                dataTable.Columns["Returned"].ColumnName = "Returned";

                dgvBookLoans.DataSource = dataTable;

                dgvBookLoans.Columns["ReturnDate"].HeaderText = "Дата возврата";
                dgvBookLoans.Columns["LoanId"].Visible = false;
                

            }
        }


        private void LoadReadersDataGridView()
        {
            string query = "SELECT Id, LastName, FirstName, MiddleName, IsTeacher FROM Readers";
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
            {
                // Очистка DataGridView перед заполнением
                dgvReaders.Rows.Clear();

                // Заполнение DataGridView данными
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    int readerId = Convert.ToInt32(row["Id"]); // Получаем Id читателя
                    string lastName = row["LastName"].ToString();
                    string firstName = row["FirstName"].ToString();
                    string middleName = row["MiddleName"].ToString();

                    bool isTeacher = false;
                    if (row["IsTeacher"] != DBNull.Value)
                    {
                        isTeacher = Convert.ToBoolean(row["IsTeacher"]);
                    }

                    // Создание значения для столбца "Фамилия и инициалы"
                    string fullName = $"{lastName} {firstName[0]}. {middleName[0]}.";
                    dgvReaders.Rows.Add(readerId, fullName, isTeacher);
                }
            }
        }



        private void LoadBooksAndInstancesDataGridView()
        {
            string query = "SELECT b.Id, b.Name AS BookName, bi.AcquisitionDate, bi.LibraryNumber, bi.IsAvailable, bi.IsLost " +
                           "FROM BookInstances bi " +
                           "INNER JOIN Books b ON bi.BookId = b.Id";
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
            {
                // Очистка DataGridView перед заполнением
                dgvBooksAndInstances.Rows.Clear();

                // Заполнение DataGridView данными
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    int bookId = Convert.ToInt32(row["Id"]); // Получаем Id книги
                    dgvBooksAndInstances.Rows.Add(bookId, row["BookName"], row["AcquisitionDate"], row["LibraryNumber"], row["IsAvailable"], row["IsLost"]);
                }
            }
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Закрытие подключения при закрытии формы
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private void группыКнигToolStripMenuItem_Click(object sender, EventArgs e)
        {
            book_groups bookGroupsForm = new book_groups();
            bookGroupsForm.ShowDialog();
        }

        private void издателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            list_publishers listPublishers = new list_publishers();
            listPublishers.ShowDialog();
        }

        private void авторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            list_authors listAuthors = new list_authors();
            listAuthors.ShowDialog();
        }

        private void книгиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            list_books listBooks = new list_books();
            listBooks.ShowDialog();
        }

        private void читателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            list_readers listReaders = new list_readers();
            listReaders.ShowDialog();
        }

        private void экземплярToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookInstancesForm BookInstancesFormOpen = new BookInstancesForm();
            BookInstancesFormOpen.ShowDialog();
        }

        private void btnAddLoan_Click(object sender, EventArgs e)
        {
            if (dgvReaders.SelectedRows.Count > 0 && dgvBooksAndInstances.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedReaderRow = dgvReaders.SelectedRows[0];
                int readerId = Convert.ToInt32(selectedReaderRow.Cells["Id"].Value);
                string readerFullName = selectedReaderRow.Cells["FullName"].Value.ToString();

                DataGridViewRow selectedBookRow = dgvBooksAndInstances.SelectedRows[0];
                int bookId = Convert.ToInt32(selectedBookRow.Cells["Id"].Value);
                string bookName = selectedBookRow.Cells["BookName"].Value.ToString();

                string issueDate = DateTime.Now.ToShortDateString();
                DateTime returnDate = DateTime.Parse(issueDate).AddDays(7);
                int returned = 0;

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO BookLoans (ReaderId, BookId, IssueDate, ReturnDate, Returned) VALUES (@readerId, @bookId, @issueDate, @returnDate, @returned)";
                    command.Parameters.AddWithValue("@readerId", readerId);
                    command.Parameters.AddWithValue("@bookId", bookId);
                    command.Parameters.AddWithValue("@issueDate", issueDate);
                    command.Parameters.AddWithValue("@returnDate", returnDate.ToShortDateString());
                    command.Parameters.AddWithValue("@returned", returned);
                    command.ExecuteNonQuery();
                }

                DataRow newRow = bookLoansTable.NewRow();
                newRow["ReaderId"] = readerId;
                newRow["ReaderFullName"] = readerFullName;
                newRow["BookId"] = bookId;
                newRow["BookName"] = bookName;
                newRow["IssueDate"] = issueDate;
                newRow["Returned"] = returned;
                newRow["ReturnDate"] = returnDate;
         
                // Очистка таблицы и повторная загрузка данных
                dataTable.Clear();
                LoadBookLoansDataGridView();
            }
            else
            {
                MessageBox.Show("Выберите читателя и книгу для оформления выдачи.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
     

        private void txtSearchReader_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtSearchReader.Text.Trim();

            foreach (DataGridViewRow row in dgvReaders.Rows)
            {
                // Проверка на статус строки NewRow
                if (!row.IsNewRow)
                {
                    var cellValue = row.Cells["FullName"].Value;
                    if (cellValue != null && cellValue.ToString().Contains(searchValue))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim().ToLower();

            foreach (DataGridViewRow row in dgvBooksAndInstances.Rows)
            {
                // Проверка на статус строки NewRow
                if (!row.IsNewRow)
                {
                    var libraryNumberCellValue = row.Cells["LibraryNumber"].Value;
                    var bookNameCellValue = row.Cells["BookName"].Value;

                    if (libraryNumberCellValue != null && libraryNumberCellValue.ToString().ToLower().Contains(searchValue) ||
                        bookNameCellValue != null && bookNameCellValue.ToString().ToLower().Contains(searchValue))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Применение изменений к адаптеру данных
                dataAdapter.Update(dataTable);

                MessageBox.Show("Изменения сохранены успешно.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Очистка таблицы и повторная загрузка данных
            dataTable.Clear();
            LoadBookLoansDataGridView();
        }

        private void dgvBookLoans_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridViewRow selectedRow = e.Row;
            int loanId = Convert.ToInt32(selectedRow.Cells["LoanId"].Value);

            string deleteQuery = "DELETE FROM BookLoans WHERE Id = @loanId";
            using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@loanId", loanId);
                command.ExecuteNonQuery();
            }
        }

        private void утерянныеКнигиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создание экземпляра приложения Excel
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;

            // Создание новой рабочей книги
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            workbook.Title = "Утерянные книги";
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            // Заголовки столбцов
            int columnIndex = 1;
            for (int i = 0; i < dgvBooksAndInstances.Columns.Count; i++)
            {
                string columnName = dgvBooksAndInstances.Columns[i].Name;
                if (columnName != "IsAvailable" && columnName != "Id" && columnName != "IsLost")
                {
                    worksheet.Cells[1, columnIndex] = dgvBooksAndInstances.Columns[i].HeaderText;
                    columnIndex++;
                }
            }

            // Данные из DataGridView
            int rowIndex = 2;
            foreach (DataGridViewRow row in dgvBooksAndInstances.Rows)
            {
                bool isLost = Convert.ToBoolean(row.Cells["IsLost"].Value);
                if (isLost)
                {
                    columnIndex = 1;
                    for (int colIndex = 0; colIndex < dgvBooksAndInstances.Columns.Count; colIndex++)
                    {
                        string columnName = dgvBooksAndInstances.Columns[colIndex].Name;
                        if (columnName != "IsAvailable" && columnName != "Id" && columnName != "IsLost")
                        {
                            worksheet.Cells[rowIndex, columnIndex] = row.Cells[colIndex].Value.ToString();
                            columnIndex++;
                        }
                    }
                    rowIndex++;
                }
            }

            // Автоизменение ширины столбцов для лучшего отображения данных
            worksheet.Columns.AutoFit();

        }

        private void выданныеКнигиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создание экземпляра приложения Excel
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;

            // Создание новой рабочей книги
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            workbook.Title = "Отчет о книжных выдачах";
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            // Заголовки столбцов
            int columnIndex = 1;
            for (int i = 0; i < dgvBookLoans.Columns.Count; i++)
            {
                string columnName = dgvBookLoans.Columns[i].Name;
                if (columnName != "LoanId" && columnName != "Id" && columnName != "Returned")
                {
                    worksheet.Cells[1, columnIndex] = dgvBookLoans.Columns[i].HeaderText;
                    columnIndex++;
                }
            }

            // Данные из DataGridView
            int rowIndex = 2;
            foreach (DataGridViewRow row in dgvBookLoans.Rows)
            {
                columnIndex = 1;
                for (int colIndex = 0; colIndex < dgvBookLoans.Columns.Count; colIndex++)
                {
                    string columnName = dgvBookLoans.Columns[colIndex].Name;
                    if (columnName != "LoanId" && columnName != "Id" && columnName != "Returned")
                    {
                        worksheet.Cells[rowIndex, columnIndex] = row.Cells[colIndex].Value.ToString();
                        columnIndex++;
                    }
                }
                rowIndex++;
            }

            // Автоизменение ширины столбцов для лучшего отображения данных
            worksheet.Columns.AutoFit();

        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // Создание команды удаления
                string deleteCommandText = "DELETE FROM BookLoans WHERE Id = @Id";
                SQLiteCommand deleteCommand = new SQLiteCommand(deleteCommandText, connection);
                deleteCommand.Parameters.Add("@Id", DbType.Int32, 0, "Id").SourceVersion = DataRowVersion.Original;

                // Присвоение команды удаления свойству DeleteCommand у loansDataAdapter
                loansDataAdapter.DeleteCommand = deleteCommand;

                // Создание команды вставки
                string insertCommandText = "INSERT INTO BookLoans (ReaderId, BookId, IssueDate, ReturnDate, Returned) VALUES (@readerId, @bookId, @issueDate, @returnDate, @returned)";
                SQLiteCommand insertCommand = new SQLiteCommand(insertCommandText, connection);
                insertCommand.Parameters.Add("@readerId", DbType.Int32, 0, "ReaderId");
                insertCommand.Parameters.Add("@bookId", DbType.Int32, 0, "BookId");
                insertCommand.Parameters.Add("@issueDate", DbType.String, 0, "IssueDate");
                insertCommand.Parameters.Add("@returnDate", DbType.String, 0, "ReturnDate");
                insertCommand.Parameters.Add("@returned", DbType.Boolean, 0, "Returned");

                // Присвоение команды вставки свойству InsertCommand у loansDataAdapter
                loansDataAdapter.InsertCommand = insertCommand;

                // Создание команды обновления (необходимо только если команда обновления уже не была создана в другом месте)
                string updateCommandText = "UPDATE BookLoans SET ReaderId = @readerId, BookId = @bookId, IssueDate = @issueDate, ReturnDate = @returnDate, Returned = @returned WHERE Id = @loanId";
                SQLiteCommand updateCommand = new SQLiteCommand(updateCommandText, connection);
                updateCommand.Parameters.Add("@readerId", DbType.Int32, 0, "ReaderId");
                updateCommand.Parameters.Add("@bookId", DbType.Int32, 0, "BookId");
                updateCommand.Parameters.Add("@issueDate", DbType.String, 0, "IssueDate");
                updateCommand.Parameters.Add("@returnDate", DbType.String, 0, "ReturnDate");
                updateCommand.Parameters.Add("@returned", DbType.Boolean, 0, "Returned");
                updateCommand.Parameters.Add("@loanId", DbType.Int32, 0, "Id").SourceVersion = DataRowVersion.Original;

                // Присвоение команды обновления свойству UpdateCommand у loansDataAdapter
                loansDataAdapter.UpdateCommand = updateCommand;

                // Сохранение изменений в базе данных
                loansDataAdapter.Update(bookLoansTable);
            }
            catch { }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            LoadReadersDataGridView();
            LoadBooksAndInstancesDataGridView();
            LoadBookLoansDataGridView();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
