using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PRDB_Sqlite.BLL;
using System.Linq;

namespace PRDB_Sqlite.GUI
{
    public partial class frm_Delete_relation : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;
        public string relationName = string.Empty;
        public string relationNameRemove = string.Empty;

        public frm_Delete_relation()
        {
            InitializeComponent();
        }

        public frm_Delete_relation(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
        }

        public frm_Delete_relation(ProbDatabase probDatabase, string relationName)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
            this.relationName = relationName.ToLower();
        }

        private void cbo_RelationName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frm_Delete_relation_Load(object sender, EventArgs e)
        {
            cbo_RelationName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_RelationName.Properties.Items.AddRange(this.probDatabase.ListOfRelationNameToLower());
            cbo_RelationName.SelectedIndex = relationName == string.Empty ? 0 : cbo_RelationName.Properties.Items.IndexOf(relationName);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbo_RelationName.SelectedIndex != -1)
            {
                ProbRelation relation = this.probDatabase.Relations.SingleOrDefault(c =>  c.RelationName.ToLower() == cbo_RelationName.Properties.Items[cbo_RelationName.SelectedIndex].ToString());

                if (MessageBox.Show(" Are you sure delete this relation  ?", "Delete Relation " + relation.RelationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.probDatabase.Relations.Remove(relation);
                    relation.DropTableByTableName();
                    relation.DeleteRelationById();
                    relationNameRemove = relation.RelationName;
                    MessageBox.Show(" Delete successfully !", "Infomation ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
                this.Close();
        }
    }
}