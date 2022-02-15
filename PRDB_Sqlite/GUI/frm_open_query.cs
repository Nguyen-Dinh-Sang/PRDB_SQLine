using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PRDB_Sqlite.GUI
{
    public partial class frm_open_query : DevExpress.XtraEditors.XtraForm
    {
        private BLL.ProbDatabase probDatabase;
        public string QueryName = string.Empty;
        public frm_open_query()
        {
            InitializeComponent();
        }

        public frm_open_query(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
        }

        private void frm_open_query_Load(object sender, EventArgs e)
        {
            cbo_queryname.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_queryname.Properties.Items.AddRange(this.probDatabase.ListOfQueryNameToLower());
            cbo_queryname.SelectedIndex = 0;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QueryName = cbo_queryname.Properties.Items[cbo_queryname.SelectedIndex].ToString();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.QueryName = string.Empty;
            this.Close();
        }
    }
}