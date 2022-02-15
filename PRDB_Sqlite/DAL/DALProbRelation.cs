using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRDB_Sqlite.BLL;
using System.Data;
using System.Windows.Forms;

namespace PRDB_Sqlite.DAL
{
   public class DALProbRelation
    {
        internal static List<BLL.ProbRelation> getAllRelation()
        {
            //"SELECT * FROM SystemRelation", "system_relation")

            List<ProbRelation> relations = new List<ProbRelation>();
            DataBase db = new DataBase();
            DataSet dts = new DataSet();
            dts.Tables.Add(db.GetDataTable("SELECT * FROM SystemRelation", "system_relation"));

            foreach (DataRow row in dts.Tables["system_relation"].Rows)
            {
                string relationname = row[1].ToString();
                int schemeID = Convert.ToInt16(row[2]);
                ProbScheme schemeName = new ProbScheme(schemeID).getSchemeById();
                List<ProbTuple> probTuples = new List<ProbTuple>();
                int nTriples = schemeName.Attributes.Count;
                probTuples = new ProbTuple().getAllTypleByRelationName(relationname, nTriples);
                ProbRelation relation = new ProbRelation(Convert.ToInt16(row[0]), relationname, probTuples, schemeName);
                relations.Add(relation);
            }

            return relations;            
        }

       

        internal static void DropTableByTableName(ProbRelation probRelation)
        {
            DataBase db = new DataBase();
            if (!db.DropTable(probRelation.RelationName))
                throw new Exception(db.errorMessage);

        }

        internal static void DeleteAllRelation()
        {
            DataBase db = new DataBase();
            if (!db.Update("DELETE FROM SystemRelation"))
                throw new Exception(db.errorMessage);
        }
     
       internal static void InsertSystemRelation(ProbRelation probRelation)
        {

            if (probRelation.IDRelation == -1)
            {
                probRelation.IDRelation = new DataBase().GetMaxIdInTable("SystemRelation");
            }


            DataBase db = new DataBase();
            string SQL = "";
            SQL = "";
            SQL += "INSERT INTO SystemRelation VALUES ( ";
            SQL += probRelation.IDRelation + ",";
            SQL += "'" + probRelation.RelationName + "'" + ",";
            SQL += probRelation.Scheme.IDScheme;
            SQL += " );";
            if (!db.Update(SQL))
            {
             
                throw new Exception(db.errorMessage);
            }
        }

       internal static void CreateTableRelation(ProbRelation probRelation)
       {
           if (probRelation.Scheme.Attributes.Count > 0)
           {
               DataBase db = new DataBase();
               string SQL = "";
               SQL += "CREATE TABLE " + probRelation.RelationName + " ( ";
               foreach (ProbAttribute attribute in probRelation.Scheme.Attributes)
               {
                   SQL += attribute.AttributeName + " " + "TEXT" + ", ";
               }
               SQL = SQL.Remove(SQL.LastIndexOf(','), 1);
               SQL += " ); ";

               if (!db.CreateTable(SQL))
                   throw new Exception(db.errorMessage);
           }
        }

       internal static void InsertTupleIntoTableRelation(ProbRelation probRelation)
       {
           DataBase db = new DataBase();
           if (probRelation.tuples.Count > 0)
           {
               foreach (ProbTuple tuple in probRelation.tuples)
               {
                  string SQL = "";
                  SQL += "INSERT INTO " + probRelation.RelationName + " VALUES (";
                   foreach (ProbTriple triple in tuple.Triples)
                   {
                       SQL += "'" + triple.GetStrValue() + "'" + ",";
                   }
                   SQL = SQL.Remove(SQL.LastIndexOf(','), 1);
                   SQL += " );  ";

                   if (!db.Update(SQL))
                       throw new Exception(db.errorMessage);
               }
           
           }


       
       }

       internal static void DeleteRelationById(ProbRelation probRelation)
       {
         
           DataBase db = new DataBase();
           if (!db.Update("DELETE FROM SystemRelation where ID = " + probRelation.IDRelation))
               throw new Exception(db.errorMessage);
       }
    }
}
