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
    public partial class frm_Delete_Scheme : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;
        private List<string> list;

        private string SchemeName = string.Empty;

        public frm_Delete_Scheme()
        {
            InitializeComponent();
        }

        public frm_Delete_Scheme(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
            this.list = this.probDatabase.ListOfSchemeNameToLower();
        }

        public frm_Delete_Scheme(ProbDatabase probDatabase, string SchemeName)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
            this.SchemeName = SchemeName.ToLower();
            this.list = this.probDatabase.ListOfSchemeNameToLower();
        }

        private void frm_Delete_Scheme_Load(object sender, EventArgs e)
        {

            cbo_schemeName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_schemeName.Properties.Items.AddRange(this.list);
            if (SchemeName == string.Empty)
                cbo_schemeName.SelectedIndex = this.list.Count > 0 ? 0 : -1;
            else
                cbo_schemeName.SelectedIndex = this.list.IndexOf(SchemeName);

            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbo_schemeName.SelectedIndex != -1)
            {
                ProbScheme currentScheme = this.probDatabase.Schemes.SingleOrDefault(c => c.SchemeName.ToLower() == cbo_schemeName.Properties.Items[cbo_schemeName.SelectedIndex].ToString());
                
                 if (currentScheme.isInherited(this.probDatabase.Relations))
                    {
                        MessageBox.Show(" Cannot delete this schema because it is inherited by some relations, please try again !", "Infomation ", MessageBoxButtons.OK, MessageBoxIcon.Error);        
                    }
                 else 
                     if (MessageBox.Show(" Are you sure delete this schema  ?", "Delete Schema " + currentScheme.SchemeName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                     {
                         this.probDatabase.Schemes.Remove(currentScheme);
                         currentScheme.DeleteSchemeById();
                         MessageBox.Show(" Delete successfully !", "Infomation ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         this.Close();
                     }             
            }
            else
                this.Close();
        }
    }
}