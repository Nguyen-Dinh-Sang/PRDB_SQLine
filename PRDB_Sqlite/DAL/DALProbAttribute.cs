using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PRDB_Sqlite.BLL;
namespace PRDB_Sqlite.DAL
{
    public class DALProbAttribute
    {
        internal static List<BLL.ProbAttribute> getListAttributeByIDScheme(int IDScheme)
        {
            List<ProbAttribute> probAttributes = new List<ProbAttribute>();
            DataBase db = new DataBase();
            DataTable dtb = db.GetDataTable("SELECT * FROM SystemAttribute Where SchemeID=" + IDScheme);
            
            if (dtb != null)
                foreach (DataRow attrRow in dtb.Rows)
                {
                    ProbAttribute NewAttr = new ProbAttribute();
                    NewAttr.PrimaryKey = Convert.ToBoolean(attrRow[1]);
                    NewAttr.AttributeName = Convert.ToString(attrRow[2]);
                    NewAttr.Type.TypeName = Convert.ToString(attrRow[3]);
                    NewAttr.Type.GetDomain(Convert.ToString(attrRow[4]));
                    NewAttr.Type.GetDataType();
                    NewAttr.Description = Convert.ToString(attrRow[5]);
                    probAttributes.Add(NewAttr);
                }


            return probAttributes;
        }

        internal static void DeleteAllAttribute()
        {
            DataBase db = new DataBase();
            if (!db.Update("DELETE FROM SystemAttribute"))
                throw new Exception(db.errorMessage);
        }

        internal static void Insert(ProbAttribute probAttribute)
        {
            DataBase db = new DataBase();
            string  SQL = "";
            SQL += "INSERT INTO SystemAttribute VALUES ( ";
            SQL += probAttribute.IDAttribute + ",";
            SQL += "'" + probAttribute.PrimaryKey + "'" + ",";
            SQL += "'" + probAttribute.AttributeName + "'" + ",";
            SQL += "'" + probAttribute.Type.TypeName + "'" + ",";
            SQL += "'" + probAttribute.Type.DomainString + "'" + ",";
            SQL += "'" + probAttribute.Description + "'" + ",";
            SQL += probAttribute.probScheme.IDScheme;
            SQL += " );";

            if (!db.Update(SQL))
                throw new Exception(db.errorMessage);
        }


        internal static int getMaxIdinTable()
        {
            return new DataBase().GetMaxIdInTable("SystemAttribute");
        }

        internal static void DeleteAllAttributeByIdScheme(ProbAttribute probAttribute)
        {
            DataBase db = new DataBase();
            if (!db.Update("DELETE FROM SystemAttribute Where SchemeID = " + probAttribute.probScheme.IDScheme))
                throw new Exception(db.errorMessage);
        }
    }
}
