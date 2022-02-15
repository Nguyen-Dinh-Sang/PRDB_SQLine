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
    public partial class frm_delete_query : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;
       
        private string QueryName = string.Empty;

        public frm_delete_query()
        {
            InitializeComponent();
        }

        public frm_delete_query(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
        }

        public frm_delete_query(ProbDatabase probDatabase, string QueryName)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
            this.QueryName = QueryName;
        }

        private void cbo_QueryName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frm_delete_query_Load(object sender, EventArgs e)
        {
            cbo_QueryName.Properties.Items.Clear();
            cbo_QueryName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_QueryName.Properties.Items.AddRange(this.probDatabase.ListOfQueryNameToLower());

            if (QueryName != string.Empty)
            {
                cbo_QueryName.SelectedIndex = this.probDatabase.ListOfQueryNameToLower().IndexOf(QueryName);

            }
            else
            { 
                 cbo_QueryName.SelectedIndex = 0;
            }



        }

        private void btnOk_Click(object sender, EventArgs e)
        {

                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure want to delete this query ?", "Delete  Queries", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    string selectName = cbo_QueryName.Properties.Items[cbo_QueryName.SelectedIndex].ToString();
                    ProbQuery query = this.probDatabase.Queries.SingleOrDefault(c => c.QueryName.ToLower() == selectName);
                    query.DeleteById();
                    QueryNameRemove = query.QueryName;
                    this.probDatabase.Queries.Remove(query);
                    MessageBox.Show(" Delete successfully !", "Infomation ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                
                }


        }

        public string QueryNameRemove { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}