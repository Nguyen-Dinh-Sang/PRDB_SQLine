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
    public partial class frm__new_relation : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;

        public frm__new_relation()
        {
            InitializeComponent();
        }

        public frm__new_relation(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
        }

        private void frm__new_relation_Load(object sender, EventArgs e)
        {
            cbo_SchemeName.Properties.Items.Clear();
            cbo_SchemeName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_SchemeName.Properties.Items.AddRange(this.probDatabase.ListOfSchemeNameToLower());
            cbo_SchemeName.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                errorProvider.SetError(txtRelationName, null);
                if (txtRelationName.Text.Trim().Length <= 0)
                {
                    errorProvider.SetError(txtRelationName, "You must enter a relation name, please try again !");
                    return;
                }



                if (txtRelationName.Text.ToLower() == "select" || txtRelationName.Text.ToLower() == "from" || txtRelationName.Text.ToLower() == "where")
                {
                    errorProvider.SetError(txtRelationName, "Relation name is not valid ( not match with keyword 'select', 'from', 'where')  ");
                    return;

                }


                foreach (var item in this.probDatabase.ListOfRelationNameToLower())
                {
                    if (item.Equals(txtRelationName.Text.ToLower(),StringComparison.OrdinalIgnoreCase))
                    {
                        errorProvider.SetError(txtRelationName, "This relation name has already existed in the database, please try again !");
                        return;
                    }
                }




                ProbScheme scheme = this.probDatabase.Schemes.SingleOrDefault(c => c.SchemeName.ToLower() == cbo_SchemeName.Properties.Items[cbo_SchemeName.SelectedIndex].ToString());
                ProbRelation relation = new ProbRelation();
                relation.RelationName = txtRelationName.Text.ToLower();
                relation.Scheme = scheme;
                relation.InsertSystemRelation();
                relation.CreateTableRelation();
                this.probDatabase.Relations.Add(relation);


                if (MessageBox.Show("Add successfully.Do you want add a new relation name ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    txtRelationName.Focus();
                    txtRelationName.Text = null;
                    this.frm__new_relation_Load(sender, e);
                }
                else
                    this.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
         
                




        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}