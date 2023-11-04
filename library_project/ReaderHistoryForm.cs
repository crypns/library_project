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
    public partial class ReaderHistoryForm : Form
    {
        private int readerId;
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private DataTable dataTable;

        public ReaderHistoryForm(int readerId)
        {
            InitializeComponent();

            this.readerId = readerId;

            // Путь к файлу базы данных SQLite
            string dbPath = "library.db";

            // Подключение к базе данных SQLite
            string connectionString = $"Data Source={dbPath};Version=3;";
            connection = new SQLiteConnection(connectionString);

            // Загрузка данных из базы данных в DataTable
            LoadData();

            // Отображение данных в DataGridView
            dgvHistory.DataSource = dataTable;


            // Изменение названий столбцов на русский язык
            dgvHistory.Columns["BookId"].HeaderText = "Код книги";
            dgvHistory.Columns["BookName"].HeaderText = "Название книги";
            dgvHistory.Columns["IssueDate"].HeaderText = "Дата выдачи";
            dgvHistory.Columns["ReturnDate"].HeaderText = "Дата возврата";
            dgvHistory.Columns["BookId"].Visible = false;

            // Настройка отображения столбцов и других элементов управления в форме
            // ...
        }

        private void LoadData()
        {
            string selectQuery = "SELECT bl.BookId, b.Name AS BookName, bl.IssueDate, bl.ReturnDate " +
                                 "FROM BookLoans bl " +
                                 "INNER JOIN Books b ON bl.BookId = b.Id " +
                                 "WHERE bl.ReaderId = @readerId";

            dataAdapter = new SQLiteDataAdapter(selectQuery, connection);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@readerId", readerId);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            

            // Подсчет общего количества записей
            int totalRecords = dataTable.Rows.Count;

            // Отображение количества в Label
            lblTotalRecords.Text = $"Общее количество книг: {totalRecords}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
