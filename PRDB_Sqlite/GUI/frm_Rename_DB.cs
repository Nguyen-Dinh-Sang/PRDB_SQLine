using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace PRDB_Sqlite.GUI
{
    public partial class frm_Rename_DB : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;

        public frm_Rename_DB()
        {
            InitializeComponent();
        }

        public frm_Rename_DB(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(txtNewNameDB,null);

            if (txtNewNameDB.Text.Trim().Length <= 0)
            {
                errorProvider.SetError(txtNewNameDB, "You did not enter a database name");
                return;
            }



            if (MessageBox.Show(" Do you want save this database name ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
             
                try
                {
                    string oldPath = this.probDatabase.DBPath;
                    string oldName = this.probDatabase.DBName;
                    this.probDatabase.DBName = txtNewNameDB.Text.Trim();
                    File.Move(this.probDatabase.DBPath, this.probDatabase.DBPath.Replace(oldName,this.probDatabase.DBName) ); // Try to move
                    this.Close();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }
            else
            {
                this.Close();
            }

            



        }

        private void frm_Rename_DB_Load(object sender, EventArgs e)
        {
            txtNewNameDB.Text = this.probDatabase.DBName;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}