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
    public partial class frm_rename_query : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;
        public string queryName = string.Empty;
        public List<string> listNameQuery = new List<string>();
        public string QueryNameRename = string.Empty;


        public frm_rename_query()
        {
            InitializeComponent();
        }

        public frm_rename_query(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
        }

        public frm_rename_query(BLL.ProbDatabase probDatabase, string queryName)
        {
            // TODO: Complete member initialization
            this.probDatabase = probDatabase;
            listNameQuery.AddRange(this.probDatabase.ListOfQueryNameToLower());
            InitializeComponent();
            this.queryName = queryName;
        }

        private void frm_rename_query_Load(object sender, EventArgs e)
        {
            cbo_QueryName.Properties.Items.Clear();
            cbo_QueryName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_QueryName.Properties.Items.AddRange(this.listNameQuery);
            cbo_QueryName.SelectedIndex = this.listNameQuery.IndexOf(this.queryName);
            txtNewName.Text = queryName;

        }

        private void cbo_QueryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNewName.Text = cbo_QueryName.Properties.Items[cbo_QueryName.SelectedIndex].ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(txtNewName, null);
            if (txtNewName.Text.Trim().Length <= 0)
            {
                errorProvider.SetError(txtNewName, "You did not enter a query name");
                return;
            }

            foreach (string item in this.listNameQuery)
            {
                if (item == txtNewName.Text.Trim().ToLower() && txtNewName.Text.Trim() != this.listNameQuery[cbo_QueryName.SelectedIndex])
                {
                    errorProvider.SetError(txtNewName, "This query name has already existed in the database");
                    return;
                }
            }


            ProbQuery query = this.probDatabase.Queries.SingleOrDefault(c => c.QueryName == this.listNameQuery[cbo_QueryName.SelectedIndex]);
            QueryNameRename = query.QueryName;
            query.QueryName = txtNewName.Text.Trim().ToLower();
          
            query.Update();

            queryName = query.QueryName;
            MessageBox.Show("Rename query successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();






        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}