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
    public partial class frm_Open_Scheme : DevExpress.XtraEditors.XtraForm
    {
        private List<string> list;
        public string selected;

        public frm_Open_Scheme()
        {
            InitializeComponent();
        }

        public frm_Open_Scheme(List<string> list)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.list = list;
        }

        private void frm_Open_Scheme_Load(object sender, EventArgs e)
        {
            cbo_schemeName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_schemeName.Properties.Items.AddRange(this.list);
            cbo_schemeName.SelectedIndex = this.list.Count > 0 ? 0 : -1;

             
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            selected = string.Empty;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            selected = cbo_schemeName.SelectedIndex != -1 ? cbo_schemeName.Properties.Items[cbo_schemeName.SelectedIndex].ToString() : string.Empty;
            this.Close();
        }

       
    }
}