using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace library_project
{
    public partial class AddInstanceForm : Form
    {
        private int bookId;
        private SQLiteConnection connection;

        public AddInstanceForm(int bookId, SQLiteConnection connection)
        {
            InitializeComponent();
            this.bookId = bookId;
            this.connection = connection;
        }

        private void AddInstanceForm_Load(object sender, EventArgs e)
        {
            // Настройка начальных значений элементов формы, если необходимо

        }

        private void btnAddInstance_Click(object sender, EventArgs e)
        {
            // Получение данных из элементов формы
            string acquisitionDate = dtpAcquisitionDate.Value.ToString("yyyy-MM-dd");
            string libraryNumber = txtLibraryNumber.Text;
            bool isAvailable = chkIsAvailable.Checked ? true : false;
            bool isLost = chkIsLost.Checked ? true : false;

            // Вставка нового экземпляра в базу данных
            string insertQuery = "INSERT INTO BookInstances (BookId, AcquisitionDate, LibraryNumber, IsAvailable, IsLost) VALUES (@BookId, @AcquisitionDate, @LibraryNumber, @IsAvailable, @IsLost)";
            using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@BookId", bookId);
                insertCommand.Parameters.AddWithValue("@AcquisitionDate", acquisitionDate);
                insertCommand.Parameters.AddWithValue("@LibraryNumber", libraryNumber);
                insertCommand.Parameters.AddWithValue("@IsAvailable", isAvailable);
                insertCommand.Parameters.AddWithValue("@IsLost", isLost);

                connection.Open();
                insertCommand.ExecuteNonQuery();
                connection.Close();
            }

            // Закрытие формы
            this.Close();
        }
    }
}
