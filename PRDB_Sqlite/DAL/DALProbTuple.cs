using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRDB_Sqlite.BLL;
using System.Data;

namespace PRDB_Sqlite.DAL
{
   public class DALProbTuple
    {
       

        internal static List<ProbTuple> getAllTypleByRelationName(string relationname, int nTriples)
        {
            List<ProbTuple> probTuple = new List<ProbTuple>();
            DataBase db = new DataBase();
            DataTable dtb = db.GetDataTable("Select * From " + relationname);
            


            foreach (DataRow tuplerow in dtb.Rows)
            {
                ProbTuple NewTuple = new ProbTuple();

                for (int i = 0; i < nTriples; i++)
                {
                    ProbTriple NewTriple = new ProbTriple(tuplerow[i].ToString());
                    NewTuple.Triples.Add(NewTriple);
                }

                probTuple.Add(NewTuple);
            }
            return probTuple;
        }

        internal static void DeleteTypeById(ProbTuple probTuple)
        {
           
        }
    }
}
