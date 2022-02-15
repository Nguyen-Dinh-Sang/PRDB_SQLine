using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRDB_Sqlite.BLL;
using System.Data;

namespace PRDB_Sqlite.DAL
{
   public static class DALProbScheme
    {                   
        internal static List<ProbScheme> getAllScheme()
        {
            List<ProbScheme> newSchemes = new List<ProbScheme>();
            DataBase db = new DataBase();
            DataSet dts = new DataSet();
            dts.Tables.Add(db.GetDataTable("SELECT * FROM SystemScheme", "system_scheme"));            
            foreach (DataRow row in dts.Tables["system_scheme"].Rows)
            {               
                List<ProbAttribute> attributes = new ProbAttribute().getListAttributeByIDScheme(Convert.ToInt16(row[0]));
                newSchemes.Add(new ProbScheme(Convert.ToInt16(row[0]), row[1].ToString(), attributes));
            }
            return newSchemes;
        }

        internal static ProbScheme getSchemeById(ProbScheme probScheme)
        {
            ProbScheme newSchemes = new ProbScheme();
            DataBase db = new DataBase();
            DataSet dts = new DataSet();
            dts.Tables.Add(db.GetDataTable("SELECT * FROM SystemScheme where ID = " + probScheme.IDScheme, "system_scheme"));
            

            foreach (DataRow row in dts.Tables["system_scheme"].Rows)
            {
                List<ProbAttribute> attributes = new ProbAttribute().getListAttributeByIDScheme(Convert.ToInt16(row[0]));
                newSchemes = new ProbScheme(Convert.ToInt16(row[0]), row[1].ToString(), attributes);
            }
            return newSchemes;
        }

        internal static void DeleteAllScheme()
        {
            DataBase db = new DataBase();
            if(!db.Update("DELETE FROM SystemScheme "))
                throw new Exception(db.errorMessage);
        }

        internal static void Insert(ProbScheme probScheme)
        {
            DataBase db = new DataBase();
            string SQL = "";
            SQL += "INSERT INTO SystemScheme VALUES (";
            SQL += probScheme.IDScheme + ",";
            SQL += "'" + probScheme.SchemeName + "'";
            SQL += " );";
            if (!db.Update(SQL))
                throw new Exception(db.errorMessage);
                    
            
        }
            
        internal static int getMaxIdinTable()
        {
            return new DataBase().GetMaxIdInTable("SystemScheme");
        }
       
        internal static void Update(ProbScheme probScheme)
        {
            string SQL = "";
            SQL += "Update SystemScheme  SET ";
            SQL += " SchemeName  = " + probScheme.SchemeName;
            SQL += " Where  ID = " + probScheme.IDScheme;
            new DataBase().Update(SQL);
        }

        internal static void DeleteSchemeById(ProbScheme probScheme)
        {
            foreach (ProbAttribute attr in probScheme.Attributes)
            {
                attr.probScheme = probScheme;
                attr.DeleteAllAttributeByIdScheme();
            }

            DataBase db = new DataBase();
            if (!db.Update("DELETE FROM SystemScheme Where ID = " + probScheme.IDScheme))
                throw new Exception(db.errorMessage);
        }
    }
}
