using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRDB_Sqlite.DAL;

namespace PRDB_Sqlite.BLL
{
    public class ProbQuery
    {
      

        #region Properties

        public int IDQuery { get; set; }
        public string QueryName { get; set; }

        public string QueryString { get; set; }

        #endregion
        #region Methods

        public ProbQuery()
        {
          this.IDQuery = -1;
          this.QueryString = "";
          this.QueryName = "";
        }

        public ProbQuery(string p)
        {
            // TODO: Complete member initialization
            this.QueryName = p;
            this.IDQuery = -1;
            this.QueryString = "";
        }

        public ProbQuery(string p, int Id , string QueryString)
        {
            // TODO: Complete member initialization
            this.QueryName = p;
            this.IDQuery = Id;
            this.QueryString = QueryString;
                 
        }

        public void GetQueryString(string query)
        {
            this.QueryString = query;
        }

        public string CutSpareSpace(string S)
        {
            string result = "";
            for (int i = 0; i < S.Length; i++)
                if (S[i] == ' ')
                {
                    if (S[i - 1] != ' ') result += S[i];
                }
                else result += S[i];
            return result;
        }

        #endregion

        internal List<ProbQuery> getAllQuery()
        {
            return DALProbQuery.getAllQuery();
        }

        internal void Insert()
        {
            DALProbQuery.Insert(this);
        }

        internal void DeleteAllQuery()
        {
            DALProbQuery.DeleteAllQuery();
        }

        internal void Update()
        {
            DALProbQuery.Update(this);
        }

        internal ProbQuery getQueryByName()
        {
          return  DALProbQuery.getQueryByName(this);
        }

        internal void DeleteById()
        {
            DALProbQuery.DeleteById(this);
        }
    }
}
