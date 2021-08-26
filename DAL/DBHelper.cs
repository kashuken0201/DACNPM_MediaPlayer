using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace MediaPlayer.DAL
{
    class DBHelper
    {
        private SqlConnection cnn;
        private static DBHelper _Instance;  // singleton
        public static DBHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    string cnstr = @"Data Source=103.95.197.121,9696;Initial Catalog=MVH_19;Persist Security Info=True;User ID=DACNPM;Password=khoa19@itf";
                    //string cnstr = @"Data Source=LAPTOP-9Q4S4J6D\SQLEXPRESS;Initial Catalog=MP3App1;Integrated Security=True";
                    _Instance = new DBHelper(cnstr);
                }
                return _Instance;
            }
            private set { }
        }
        public DBHelper(string s)
        {
            cnn = new SqlConnection(s);
        }
        public void ExecuteDB(string querry) // insert update delete
        {
            try
            {
                SqlCommand cmd = new SqlCommand(querry, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Không thể truy cập cơ sở dữ liệu");
            }
            
        }
        public DataTable GetRecords(string query) // select
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, cnn);
                cnn.Open();
                da.Fill(dt);
                cnn.Close();
                return dt;
            }
            catch
            { 
                MessageBox.Show("Không thể truy cập cơ sở dữ liệu");
                return null;
            }
        }
    }
}
