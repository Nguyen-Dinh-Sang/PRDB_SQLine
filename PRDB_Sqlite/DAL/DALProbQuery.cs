using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PRDB_Sqlite.BLL;
using System.Windows.Forms;
namespace PRDB_Sqlite.DAL
{
   public class DALProbQuery
    {
        internal static List<BLL.ProbQuery> getAllQuery()
        {
            List<ProbQuery> probQuerys = new List<ProbQuery>();
            DataBase db = new DataBase();
            DataSet dts = dts = new DataSet();               
            dts.Tables.Add(db.GetDataTable("SELECT * FROM SystemQuery", "system_query"));

            foreach (DataRow queryRow in dts.Tables["system_query"].Rows)
            {
                ProbQuery NewQuery = new ProbQuery();
                NewQuery.IDQuery = Convert.ToInt16(queryRow[0].ToString());
                NewQuery.QueryName = queryRow[1].ToString();
                if( queryRow[2].ToString() != "Empty")
                     NewQuery.GetQueryString(queryRow[2].ToString());
                else
                    NewQuery.GetQueryString("");
                probQuerys.Add(NewQuery);
            }

            return probQuerys;
        }

        internal static void Insert(ProbQuery probQuery)
        {
            if (probQuery.IDQuery == -1)
            {
                probQuery.IDQuery = new DataBase().GetMaxIdInTable("SystemQuery");
            }

            DataBase db = new DataBase();
            string   SQL = "";

            if (probQuery.QueryString == "")
                probQuery.QueryString = "Empty";
         
                SQL += "INSERT INTO SystemQuery VALUES (";
                SQL += probQuery.IDQuery + ",";
                SQL += "'" + probQuery.QueryName + "'" + ",";
                SQL += "'" + probQuery.QueryString + "'";
                SQL += " );";


            if (!db.Update(SQL))
                throw new Exception(db.errorMessage);
        }

        internal static void DeleteAllQuery()
        {
            DataBase db = new DataBase();
            if (!db.Update("DELETE FROM SystemQuery"))
                throw new Exception(db.errorMessage);
        }

        internal static void Update(ProbQuery probQuery)
        {

      

            string SQL = "";
            SQL += "Update SystemQuery  SET ";
            SQL += " QueryName  = '" + probQuery.QueryName + "'";
            SQL += " , QueryString = '" + probQuery.QueryString.Replace("'", "''") + "'";
            SQL += " Where  ID = " + probQuery.IDQuery;
         
            new DataBase().Update(SQL);

           
        }

        internal static ProbQuery getQueryByName(ProbQuery probQuery)
        {
            
            DataBase db = new DataBase();
            DataSet dts = dts = new DataSet();
            dts.Tables.Add(db.GetDataTable("SELECT * FROM SystemQuery where QueryName = '" + probQuery.QueryName + "'", "system_query"));

            ProbQuery NewQuery = new ProbQuery();
            foreach (DataRow queryRow in dts.Tables["system_query"].Rows)
            {
                NewQuery = new ProbQuery();
                NewQuery.IDQuery = Convert.ToInt16(queryRow[0].ToString());
                NewQuery.QueryName = queryRow[1].ToString();
                NewQuery.GetQueryString(queryRow[2].ToString());
             
                
            }

            return NewQuery;
        }

        internal static void DeleteById(ProbQuery probQuery)
        {
            DataBase db = new DataBase();
            if (!db.Update("DELETE FROM SystemQuery where ID = " + probQuery.IDQuery))
                throw new Exception(db.errorMessage);
        }
    }
}
