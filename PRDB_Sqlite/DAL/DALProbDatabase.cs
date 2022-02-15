using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using PRDB_Sqlite.BLL;
using System.Data;

namespace PRDB_Sqlite.DAL
{
    public static class DALProbDatabase 
    {       
        
        internal static bool CreateNewDatabase(BLL.ProbDatabase probDatabase)
        {
            try
            {                
                SQLiteConnection.CreateFile(probDatabase.DBPath);                   

                DataBase db = new DataBase(probDatabase.ConnectString);
               
                string strSQL = "";

                // Record set of schemes to the database system
                strSQL += "CREATE TABLE SystemScheme ( ID INT, SchemeName NVARCHAR(255) );";
                if (!db.CreateTable(strSQL))
                    throw new Exception(db.errorMessage);

                // Record set of relations to the database system
                strSQL = "";
                strSQL += "CREATE TABLE SystemRelation (ID INT,RelationName NVARCHAR(255), SchemeID INT );";
                if (!db.CreateTable(strSQL))
                    throw new Exception(db.errorMessage);


                // Record set of attributes to the database system  
                strSQL = "";
                strSQL += "CREATE TABLE SystemAttribute (ID INT,PrimaryKey NVARCHAR(10),AttributeName NVARCHAR(255),DataType NVARCHAR(255),Domain TEXT,Description TEXT,SchemeID INT ); ";
                if (!db.CreateTable(strSQL))
                    throw new Exception(db.errorMessage);


                // Record set of queries to the database system
                strSQL = "";
                strSQL += "CREATE TABLE SystemQuery (ID INT,QueryName NVARCHAR(255),QueryString TEXT );";
                if (!db.CreateTable(strSQL))
                {
                    MessageBox.Show("Error : " + db.errorMessage + " please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return false;

            }


            return true;
        }
        internal static ProbDatabase OpenExistingDatabase(ProbDatabase probDatabase)
        {
            ProbDatabase newProbDatabase = new ProbDatabase(probDatabase);
            try
            {              
          
                List<ProbScheme> Schemes = new List<ProbScheme>();
                Schemes = new ProbScheme().getAllScheme();  
                newProbDatabase.Schemes = Schemes;


                List<ProbRelation> relations = new List<ProbRelation>();
                relations = new ProbRelation().getAllRelation();
                newProbDatabase.Relations = relations;

                List<ProbQuery> querys = new List<ProbQuery>();
                querys = new ProbQuery().getAllQuery();
                newProbDatabase.Queries = querys;


            }
            catch (Exception)
            {
                return null;
            }
            return newProbDatabase;

        }
        internal static bool SaveDatabase(ProbDatabase probDatabase)
        {
            try
            {               
                //Drop data base
                DropDatabaseData();
                int schemeID = 0;
                int attributeID = 0;

                #region Save Scheme
                foreach (ProbScheme scheme in probDatabase.Schemes)
                {
                    /// Save Schemes 
                    schemeID++;
                    scheme.SchemeName = scheme.SchemeName.ToLower().Trim();
                    scheme.IDScheme = schemeID;
                    scheme.Insert();


                    ///Save attributes of the scheme to the System Attribute Table
                    foreach (ProbAttribute attr in scheme.Attributes)
                    {
                        attributeID++;
                        attr.probScheme = scheme;
                        attr.AttributeName = attr.AttributeName.Trim();
                        attr.IDAttribute = attributeID;
                        attr.Insert();
                    }
                }
                #endregion

                #region // Save Relations //


                int relationID = 0;
                foreach (ProbRelation relation in probDatabase.Relations)
                {
                    relationID++;
                    relation.IDRelation = relationID;
                    relation.RelationName = relation.RelationName.ToLower().Trim();
                    relation.InsertSystemRelation();

                    /// Create Table <Relation> //
                    relation.CreateTableRelation();

                    // Insert tuples into Talbe <Relation> //
                    relation.InsertTupleIntoTableRelation();

                }

                #endregion

                #region Save Query
                int queryID = 0;
                foreach (ProbQuery item in probDatabase.Queries)
                {

                    queryID++;
                    item.QueryName = item.QueryName.ToLower().Trim();
                    item.IDQuery = queryID;
                    item.Insert();
                }

                #endregion


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return false;   
            }
            return true;
        }
        private static bool DropDatabaseData()
        {
            try
            {
                    List<ProbRelation> relations = new List<ProbRelation>();
                    relations = new ProbRelation().getAllRelation();

                    foreach (ProbRelation item in relations)
                    {
                        item.DropTableByTableName();
                    }
                
                    ProbScheme probScheme = new ProbScheme();
                    probScheme.DeleteAllScheme();

                    ProbRelation probRelation = new ProbRelation();
                    probRelation.DeleteAllRelation();

                    ProbAttribute probAttribute = new ProbAttribute();
                    probAttribute.DeleteAllAttribute();

                    ProbQuery probQuery = new ProbQuery();
                    probQuery.DeleteAllQuery();
                
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
                return false;
            }

            return true;
        }

    }
}
