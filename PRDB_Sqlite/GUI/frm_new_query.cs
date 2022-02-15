using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PRDB_Sqlite.BLL;

namespace PRDB_Sqlite.GUI
{
    public partial class frm_new_query : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;
        public string queryName = string.Empty;

        public frm_new_query()
        {
            InitializeComponent();
        }

        public frm_new_query(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(txtQueryName, null);
            if (txtQueryName.Text.Trim().Length < 0)
            {
                errorProvider.SetError(txtQueryName, "You must enter a query name, please try again !");
                return;
            }

            foreach (string item in this.probDatabase.ListOfQueryNameToLower())
            {
                if (item == txtQueryName.Text.ToLower())
                {
                    errorProvider.SetError(txtQueryName, "The query name has already existed in the database, please try again !");
                    return;
                }
            }
            

            this.queryName = txtQueryName.Text.Trim().ToLower();

            ProbQuery query = new ProbQuery(this.queryName);
            query.Insert();
            query = query.getQueryByName();


            this.probDatabase.Queries.Add(query);


            if (MessageBox.Show("Add successfully. Do you want add a new query name ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                txtQueryName.Text = "";
                txtQueryName.Focus();
            }
            else
                this.Close();

               

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }
    }
}