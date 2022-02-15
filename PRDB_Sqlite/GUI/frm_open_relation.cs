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
    public partial class frm_open_relation : DevExpress.XtraEditors.XtraForm
    {
        private List<string> list;
        public string relationName = string.Empty;

        public frm_open_relation()
        {
            InitializeComponent();
        }

        public frm_open_relation(List<string> list)
        {
            // TODO: Complete member initialization

            InitializeComponent();
            this.list = list;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.relationName = string.Empty;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.relationName =    cbo_RelationName.Properties.Items[cbo_RelationName.SelectedIndex].ToString();
            this.Close();
        }

        private void frm_open_relation_Load(object sender, EventArgs e)
        {
            cbo_RelationName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cbo_RelationName.Properties.Items.AddRange(this.list);
            cbo_RelationName.SelectedIndex = 0;

        }
    }
}