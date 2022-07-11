using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Labs_OOP9
{
    public partial class Form1 : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRows;
        enum RowState
        {
            Existed,
            New,
            Modified,
            ModifiedNew,
            Deleted
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Имя");
            dataGridView1.Columns.Add("pet", "Питомец");
            dataGridView1.Columns.Add("nameofillness", "Название болезни");
            dataGridView1.Columns.Add("numberofattendences", "Количество посещениий");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv,IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0),record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4));
        }
        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select * from Hospital";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgv,reader);
            }
            reader.Close();
        }
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
    }
}
