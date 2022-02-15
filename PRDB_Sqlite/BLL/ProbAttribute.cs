using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRDB_Sqlite.DAL;

namespace PRDB_Sqlite.BLL
{
    public class ProbAttribute
    {
        #region Properties
        public bool PrimaryKey { get;  set; }

        public int IDAttribute { get; set; }
        // Tên thuộc tính
        public string AttributeName { get; set; }

        public ProbDataType Type { get; set; }

        public string Description { get; set; }

        public ProbScheme probScheme { get; set; }

        public string TypeName { get { return this.Type.TypeName; } set { }  }
        public string DomainString { get { return this.Type.DomainString; } set { } }

        #endregion

        #region Methods

        public ProbAttribute()
        {
            this.IDAttribute = -1;
            Type = new ProbDataType();
           
        }


        public ProbAttribute(ProbAttribute attr)
        {
            this.IDAttribute = attr.IDAttribute;
            this.Type = new ProbDataType(attr.Type);
            this.Description = attr.Description;
            this.PrimaryKey = attr.PrimaryKey;
            this.AttributeName = attr.AttributeName;
            this.probScheme = attr.probScheme;

        }

        #endregion


        internal List<ProbAttribute> getListAttributeByIDScheme(int IDScheme)
        {         
            return DALProbAttribute.getListAttributeByIDScheme(IDScheme);
        }

        internal void DeleteAllAttribute()
        {
            DALProbAttribute.DeleteAllAttribute();
        }

        internal void Insert()
        {
            DALProbAttribute.Insert(this);
        }



      


        internal int getMaxIdinTable()
        {
            return DALProbAttribute.getMaxIdinTable();
        }

        internal void DeleteAllAttributeByIdScheme()
        {
            DALProbAttribute.DeleteAllAttributeByIdScheme(this);
        }
    }
}
