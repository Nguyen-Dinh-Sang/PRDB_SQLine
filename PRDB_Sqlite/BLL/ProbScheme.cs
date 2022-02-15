using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PRDB_Sqlite.DAL;

namespace PRDB_Sqlite.BLL
{
    public class ProbScheme
    {
        #region Declare Properties

        //ID lược đồ quan hệ

        public int IDScheme { get; set; }

        //Tên lược đồ quan hệ
        public string SchemeName { get; set; }
        //Danh sách các thuộc tính của lược đồ
        public List<ProbAttribute> Attributes { get; set; }        



        #endregion

        #region Methods
        public ProbScheme()
        {
            this.IDScheme = -1;
            this.SchemeName = "No Name";
            this.Attributes = new List<ProbAttribute>();
        }

        public ProbScheme(int IDScheme)
        {
            this.IDScheme = IDScheme;
            this.SchemeName = "No Name";
            this.Attributes = new List<ProbAttribute>();
        }

        public ProbScheme(string schemename)
        {
            this.IDScheme = -1;
            this.SchemeName = schemename;
            this.Attributes = new List<ProbAttribute>();
        }

        public ProbScheme(int IDScheme,string schemename, List<ProbAttribute> Attributes)
        {
            this.IDScheme = IDScheme;
            this.SchemeName = schemename;
            this.Attributes = new List<ProbAttribute>();

             foreach (ProbAttribute item in Attributes)
             {
                 ProbAttribute attr = new ProbAttribute();
                 attr.AttributeName = item.AttributeName;
                 attr.Description = item.Description;
                 attr.DomainString = item.DomainString;
                 attr.IDAttribute = item.IDAttribute;
                 attr.PrimaryKey = item.PrimaryKey;
                 attr.Type = new ProbDataType(item.Type);
                 this.Attributes.Add(attr);
             }

        }



       #endregion


        internal List<string> ListOfAttributeNameToUpper()
        {
            List<string> list = new List<string>();

            foreach (ProbAttribute item in this.Attributes)
            {
                list.Add(item.AttributeName);
                
            }

            return list;
        }


        internal List<string> ListOfAttributeNameToLower()
        {
            List<string> list = new List<string>();

            foreach (ProbAttribute item in this.Attributes)
            {
                list.Add(item.AttributeName.ToLower());

            }

            return list;
        }

        internal List<int> ListIndexPrimaryKey()
        {
            List<int> indexs = new List<int>();

            for (int i = 0; i < this.Attributes.Count; i++)
            {
                if (this.Attributes[i].PrimaryKey)
                    indexs.Add(i);
            }

            return indexs;
        }



        public bool isInherited(List<ProbRelation> Relations)
        {
            try
            {
                   foreach (ProbRelation relation in Relations)
                        if ( this.SchemeName.Equals(relation.Scheme.SchemeName,StringComparison.OrdinalIgnoreCase))
                             return true;
                
            }
            catch
            {
                
            }

            return false;
        }


        internal List<ProbScheme> getAllScheme()
        {
            return DALProbScheme.getAllScheme();
        }
        
        internal ProbScheme getSchemeById()
        {
            return DALProbScheme.getSchemeById(this);
        }
        

        internal void DeleteAllScheme()
        {
            DALProbScheme.DeleteAllScheme();
        }

      

        internal void Insert()
        {
            DALProbScheme.Insert(this);
        }



        internal int getMaxIdinTable()
        {
            return DALProbScheme.getMaxIdinTable();
        }

        internal void Update()
        {
            DALProbScheme.Update(this);
        }

        internal void DeleteSchemeById()
        {
            DALProbScheme.DeleteSchemeById(this);
        }
    }

}
