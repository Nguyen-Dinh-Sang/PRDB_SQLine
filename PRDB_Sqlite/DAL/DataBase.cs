using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace PRDB_Sqlite.DAL
{
    public   class DataBase
    {

        #region declare Properties
     
        public  SQLiteConnection connection { get; set; }
        public  SQLiteCommand command { get; set; }
        public  SQLiteDataAdapter adapter { get; set; }
        public string  errorMessage { get; set; }
        public Object [] valueCollection { get; set; }
        #endregion

        #region Connection


        public DataBase()
        {
            this.connection = new SQLiteConnection(Resource.ConnectionString);
            this.command = new SQLiteCommand();
            this.adapter = new SQLiteDataAdapter();
        }     


        public DataBase(string connectionString)
        {
            this.connection = new SQLiteConnection(connectionString);
            this.command = new SQLiteCommand();
            this.adapter = new SQLiteDataAdapter();
        }       
        private void OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (SQLiteException sqliteEx)
            {

                errorMessage = sqliteEx.Message;
            }
        }
        private void CloseConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
            }
        }
        #endregion

        #region Query
  
        public DataSet GetDataSet(string QueryString, string TblName)
        {
            DataSet dts = new DataSet();
            OpenConnection();
            try
            {
                command.CommandText = QueryString;
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                adapter = new SQLiteDataAdapter(command);
                adapter.Fill(dts, TblName);
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                return null;
            }
            finally
            {
                CloseConnection();
            }
            return dts;
        }
        public DataSet GetDataSet(string QueryString)
        {
            DataSet dts = new DataSet();
            OpenConnection();
            try
            {
                command.CommandText = QueryString;
                command.Connection = connection;
                command.CommandType = CommandType.Text;


                adapter = new SQLiteDataAdapter(command);
                adapter.Fill(dts);
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                return null;
            }
            finally
            {
                CloseConnection();
            }
            return dts;
        }
        public DataTable GetDataTable(string QueryString, string tablename)
        {
            DataTable dtb = new DataTable(tablename);
            OpenConnection();
            try
            {
                command.CommandText = QueryString;
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                adapter = new SQLiteDataAdapter(command);
                adapter.Fill(dtb);
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                return null;
            }
            finally
            {
                CloseConnection();
            }
            return dtb;
        }
        public DataTable GetDataTable(string QueryString)
        {
            DataTable dtb = new DataTable();
            OpenConnection();
            try
            {
                command.CommandText = QueryString;
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                adapter = new SQLiteDataAdapter(command);
                adapter.Fill(dtb);
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                return null;
            }
            finally
            {
                CloseConnection();
            }
            return dtb;
        }
        public bool TableExist(string TableName) // Check if the table had been created
        {
            OpenConnection();
            try
            {
                command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + TableName + "'";
                command.Connection = connection;
                SQLiteDataReader dtreader = command.ExecuteReader();
                if (dtreader.HasRows)
                    return true;
                return false;
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
            }
            finally
            {
                CloseConnection();
            }
            
            return false;
        }


        #endregion
        
        #region Update
        public bool CreateTable(string stringCreateTable)
        {
            int rows = 0;

            try
            {
                OpenConnection();
                command.CommandText = stringCreateTable;
                command.Connection = connection;
                rows = command.ExecuteNonQuery();

                if (rows < 0)
                    return false;
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                return false;
            }
            finally
            {
                CloseConnection();
            }
            return true;
        }
        public bool DropTable(string TableName)
        {
            try
            {
                OpenConnection();
                command.Connection = connection;
                command.CommandText = "DROP TABLE IF EXISTS " + TableName;
                command.CommandType = CommandType.Text;
              
                int result = command.ExecuteNonQuery();
                if (result < 0) return false;
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                return false;
            }
            finally
            {

                CloseConnection();
            }
            return true;
        }
        public bool Update(string stringUpdate)
        {
            try
            {
                OpenConnection();
                command.CommandText = stringUpdate;
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                int result = command.ExecuteNonQuery();
                if (result < 0) return false;
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                return false;
            }
            finally
            {
                CloseConnection();
            }
            return true;
        }
        public int GetSchemeID(string SchName)
        {
            OpenConnection();
            int id;
            try
            {
                command.CommandText = "SELECT ID FROM SystemScheme WHERE SchemeName=" + "'" + SchName + "'";
                command.Connection = connection;
                id = Convert.ToInt16(command.ExecuteScalar());
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                return -1;
            }
            finally
            {
                CloseConnection();
            }
            return id;
        }

        public int GetMaxIdInTable(string table)
        {
            OpenConnection();
            int id;
            try
            {
                command.CommandText = "SELECT max(ID) FROM " + table ;
                command.Connection = connection;

                object tmp = command.ExecuteScalar();
                if(tmp.ToString() != "")
                    id =  Convert.ToInt16(tmp) + 1;
                else
                    id = 1;

            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;              
                return 1;
            }
            finally
            {
                CloseConnection();
            }
            return id;
        }
        public object GetValueField(string Query)
        {
            OpenConnection();
            try
            {
                command.CommandText = Query;
                command.Connection = connection;
                return command.ExecuteScalar();
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
            }
            finally
            {
                CloseConnection();
            }
            return null;
        }
        public int GetNumberOfTuple(string TableName)
        {
            OpenConnection();
            int number;
            try
            {
                command.CommandText = "SELECT Count(*) FROM " + TableName;
                command.Connection = connection;
                number = Convert.ToInt16(command.ExecuteScalar());
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                return -1;
            }
            finally
            {
                CloseConnection();   
            }
            return number;
        }

        #endregion





    }
}
