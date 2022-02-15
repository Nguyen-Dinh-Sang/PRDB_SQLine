using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using PRDB_Sqlite.DAL;
using System.Windows.Forms;


namespace PRDB_Sqlite.BLL
{
    public class ProbDatabase
    {
        #region Properties

        // Tên cơ sở dữ liệu
        public string DBName { get; set; }
        //Chuổi kết nối
        public string ConnectString { get; set; }
        // Đường dẫn đến CSDL
        public string DBPath { get; set; }
        // Tập các lược đồ cơ sở dữ liệu
        public List<ProbScheme> Schemes { get; set; }

        // Tập các quan hệ cơ sở dữ liệu
        public List<ProbRelation> Relations { get; set; }
        // Tập các truy vấn cơ sở dữ liệu
        public List<ProbQuery> Queries { get; set; }
        //DataSet
        public DataSet DataSet { get; set; }
        #endregion

        #region methods
        public ProbDatabase(string path)
        {
            // Lấy đường dẫn cho CSDL 
            this.DBPath = path;
            this.DBName = "";

            for (int i = path.Length - 1; i >= 0; i--)
            {
                if (path[i] == '\\') break;
                else this.DBName = path[i] + DBName;
            }
            // Đặt chuỗi kết nối
            this.ConnectString = "Data Source=" + DBPath + ";Version=3;";
        
            this.DBName = CutExtension(DBName);
            this.Relations = new List<ProbRelation>();
            this.Queries = new List<ProbQuery>();
            this.Schemes = new List<ProbScheme>();
        }
        public ProbDatabase(ProbDatabase probDatabase)
        {
            this.DBPath = probDatabase.DBPath;
            this.DBName = probDatabase.DBName;
            this.ConnectString = probDatabase.ConnectString;
            this.DataSet = probDatabase.DataSet;
            this.Relations = probDatabase.Relations;
            this.Queries = probDatabase.Queries;
            this.Schemes = probDatabase.Schemes;
            
        }

        public ProbDatabase()
        {
            // TODO: Complete member initialization
        }
                
        public bool CreateNewDatabase()
        {
            return DALProbDatabase.CreateNewDatabase(this);
        }
        internal ProbDatabase OpenExistingDatabase()
        {
            return DALProbDatabase.OpenExistingDatabase(this);
        }
        private string CutExtension(string name)
        {
            for (int i = name.Length - 1; i >= 0; i--)
                if (name[i] == '.')
                {
                    name = name.Remove(i);
                    break;
                }
            return name;
        }


        public List<string> ListOfSchemeNameToLower()
        {
            List<string> List = new List<string>();
            foreach (ProbScheme schema in this.Schemes)
                List.Add(schema.SchemeName.ToLower().Trim());
            return List;
        }
        

        public List<string> ListOfRelationNameToLower()
        {
            List<string> List = new List<string>();
            foreach (ProbRelation relation in this.Relations)
                List.Add(relation.RelationName.ToLower().Trim());
            return List;
        }



        public List<string> ListOfQueryNameToLower()
        {
            List<string> List = new List<string>();
            foreach (ProbQuery query in this.Queries)
                List.Add(query.QueryName.ToLower().Trim());
            return List;
        }

   

        #endregion

        
        
        internal bool SaveDatabase()
        {
            return DALProbDatabase.SaveDatabase(this);
        }

        
    }
}
