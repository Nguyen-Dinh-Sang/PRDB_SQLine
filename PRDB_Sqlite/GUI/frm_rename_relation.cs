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
    public partial class frm_rename_relation : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;
        public string CurrentNameRelation = string.Empty;


        public frm_rename_relation()
        {
            InitializeComponent();
        }

        public frm_rename_relation(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization

            InitializeComponent();
            this.probDatabase = probDatabase;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_rename_relation_Load(object sender, EventArgs e)
        {
            cbo_relation_name.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_relation_name.Properties.Items.AddRange(this.probDatabase.ListOfRelationNameToLower());
            cbo_relation_name.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(txtNewNameRelation, null);
            if (txtNewNameRelation.Text.Trim().Length <= 0)
            {
                errorProvider.SetError(txtNewNameRelation, "You did not enter a relation name");
                return;
            }

            if (txtNewNameRelation.Text.ToLower() == "select" || txtNewNameRelation.Text.ToLower() == "from" || txtNewNameRelation.Text.ToLower() == "where")
            {
                errorProvider.SetError(txtNewNameRelation, "Relation name is not valid ( not match with keyword 'select', 'from', 'where')  ");
                return;

            }


            if (this.CurrentNameRelation == txtNewNameRelation.Text.Trim().ToLower())
            {
                return;                
            }

            
            if (this.CurrentNameRelation != txtNewNameRelation.Text.Trim().ToLower())
            {
                foreach (var item in this.probDatabase.ListOfRelationNameToLower())
                {
                    if (item.Equals(txtNewNameRelation.Text.ToLower()))
                    {
                        errorProvider.SetError(txtNewNameRelation, "This relation name has already existed in the database ");
                        return;
                    }
                }
            }

            ProbRelation relation = this.probDatabase.Relations.SingleOrDefault(c=> c.RelationName.ToLower() == CurrentNameRelation);
            this.probDatabase.Relations.Remove(relation);
            relation.DropTableByTableName();
            relation.DeleteRelationById();
            relation.RelationName = txtNewNameRelation.Text.Trim();
            relation.InsertSystemRelation();
            relation.CreateTableRelation();
            relation.InsertTupleIntoTableRelation();
            this.probDatabase.Relations.Add(relation);
            MessageBox.Show("Rename relation successful", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();


        }

        private void cbo_relation_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentNameRelation = cbo_relation_name.Properties.Items[cbo_relation_name.SelectedIndex].ToString();
            txtNewNameRelation.Text = CurrentNameRelation;
        }
    }
}