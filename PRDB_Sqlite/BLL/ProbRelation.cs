using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRDB_Sqlite.DAL;
namespace PRDB_Sqlite.BLL
{
   public class ProbRelation
    {
        #region Properties

       //ID relation
        public int IDRelation { get; set; }

        // Lược đồ quan hệ tương ứng
        public ProbScheme Scheme { get; set; }

        // Tên quan hệ
        public string RelationName { get; set; }

        public  string RenameRelationName { get; set; }

        public List<string> ListRenameRelation { get; set; }
    
        // Tập các bộ trên quan hệ
        public List<ProbTuple> tuples { get; set; }
        #endregion

        #region Methods

        public ProbRelation() // Constructor
        {
            this.IDRelation = -1;
            this.Scheme = new ProbScheme();
            this.tuples = new List<ProbTuple>();
            this.RelationName = "No Name";
            this.RenameRelationName = string.Empty;
            this.ListRenameRelation = new List<string>();
          }

        public ProbRelation(string relationName) // Constructor
        {
            this.IDRelation = -1;
            this.Scheme = new ProbScheme();
            this.tuples = new List<ProbTuple>();
            this.RelationName = relationName;
            this.RenameRelationName = string.Empty;
            this.ListRenameRelation = new List<string>();
         }

        public ProbRelation(int IDRelation,string relationName, List<ProbTuple> tuples,ProbScheme scheme  ) // Constructor
        {
            this.IDRelation = IDRelation;
            this.Scheme = scheme;
            this.tuples = tuples;
            this.RelationName = relationName;
            this.RenameRelationName = string.Empty;
            this.ListRenameRelation = new List<string>();
        }


        #endregion



        internal List<ProbRelation> getAllRelation()
        {
            return DALProbRelation.getAllRelation();
        }

        internal void DropTableByTableName()
        {
            DALProbRelation.DropTableByTableName(this);
        }


        internal void DeleteAllRelation()
        {
            DALProbRelation.DeleteAllRelation();
        }

     
        internal void InsertSystemRelation()
        {
            DALProbRelation.InsertSystemRelation(this);
        }

        internal void CreateTableRelation()
        {
            DALProbRelation.CreateTableRelation(this);
        }

        internal void InsertTupleIntoTableRelation()
        {
            DALProbRelation.InsertTupleIntoTableRelation(this);
        }

        internal void DeleteRelationById()
        {
            DALProbRelation.DeleteRelationById(this);
        }

      
    }
}
