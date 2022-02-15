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
    public partial class frm_edit_scheme : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;
        public string currentSchemeName;
        public ProbScheme currentScheme;
      
        public string scheme = string.Empty;
        public frm_edit_scheme()
        {
            InitializeComponent();
        }



        public frm_edit_scheme(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
        }

        public frm_edit_scheme(ProbDatabase probDatabase_2, string scheme)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase_2;
            this.scheme = scheme;
        }
        
        private void frm_edit_scheme_Load(object sender, EventArgs e)
        {
            comboBox_SchemeName.Properties.Items.Clear();
            comboBox_SchemeName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            comboBox_SchemeName.Properties.Items.AddRange(this.probDatabase.ListOfSchemeNameToLower());
            comboBox_SchemeName.SelectedIndex = scheme == string.Empty ? 0 : comboBox_SchemeName.Properties.Items.IndexOf(scheme);

        }

        private void comboBox_SchemeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSchemeName = comboBox_SchemeName.Properties.Items[comboBox_SchemeName.SelectedIndex].ToString();

            txtSchemeName.Text = currentSchemeName;

            currentScheme = this.probDatabase.Schemes.SingleOrDefault(c => c.SchemeName.ToLower() == currentSchemeName);

        

            // check inherited
            if (new ProbScheme(currentSchemeName).isInherited(this.probDatabase.Relations) == true)
            {
              
                lblInfomation.Visible = true;
                lblCurrentName.Enabled = false;
                GridViewDesign.ReadOnly = true;
                btnSave.Enabled = false;
                txtSchemeName.Enabled = false;
                Btn_Design_ClearData.Enabled = false;
                Btn_Design_DeleteRow.Enabled = false;
            }
            else
            {
                lblInfomation.Visible = false;
                lblCurrentName.Enabled = true;
                GridViewDesign.ReadOnly = false;
                btnSave.Enabled = true;
                txtSchemeName.Enabled = true;
                Btn_Design_ClearData.Enabled = true;
                Btn_Design_DeleteRow.Enabled = true;
            }


            //add attribute into GridViewDesign
            GridViewDesign.Rows.Clear();
            int i = 0;
            CheckBox chkbox;
            foreach (ProbAttribute attr in currentScheme.Attributes)
            {
                GridViewDesign.Rows.Add();
                chkbox = new CheckBox();
                chkbox.Checked = attr.PrimaryKey;
                GridViewDesign.Rows[i].Cells[0].Value = chkbox.CheckState;
                GridViewDesign.Rows[i].Cells[1].Value = attr.AttributeName;
                GridViewDesign.Rows[i].Cells[2].Value = attr.Type.TypeName;
                GridViewDesign.Rows[i].Cells[3].Value = attr.Type.DomainString;
                GridViewDesign.Rows[i].Cells[4].Value = (attr.Description != null ? attr.Description : null);
                i++;
            }
            GridViewDesign.CurrentCell = GridViewDesign.Rows[i].Cells[0];
            if (GridViewDesign.CurrentRow != null)
                lblDesignRowNumberIndicator.Text = (GridViewDesign.CurrentRow.Index + 1).ToString() + " / " + GridViewDesign.Rows.Count.ToString();
            else lblDesignRowNumberIndicator.Text = "1 / " + GridViewDesign.Rows.Count.ToString();

        }

        private void GridViewDesign_Click(object sender, EventArgs e)
        {
            if (this.GridViewDesign.CurrentRow != null)
                lblDesignRowNumberIndicator.Text = (GridViewDesign.CurrentRow.Index + 1) + " / " + GridViewDesign.Rows.Count;
            else lblDesignRowNumberIndicator.Text = "1 / " + GridViewDesign.Rows.Count;
   
        }

        private void btn_Design_Home_Click(object sender, EventArgs e)
        {
            if (GridViewDesign.Rows.Count > 1)
            {
                GridViewDesign.CurrentCell = GridViewDesign.Rows[0].Cells[0];
                lblDesignRowNumberIndicator.Text = "1 / " + GridViewDesign.Rows.Count.ToString();
            }
        }

        private void btn_Design_End_Click(object sender, EventArgs e)
        {
            if (GridViewDesign.Rows.Count > 1)
            {
                int nRow = GridViewDesign.Rows.Count;
                GridViewDesign.CurrentCell = GridViewDesign.Rows[nRow - 1].Cells[0];
                lblDesignRowNumberIndicator.Text = nRow + " / " + nRow;
            }
        }

        private void btn_Design_Pre_Click(object sender, EventArgs e)
        {
            if (GridViewDesign.Rows.Count > 1)
            {
                int PreRow = GridViewDesign.CurrentRow.Index - 1;
                PreRow = (PreRow > 0 ? PreRow : 0);
                GridViewDesign.CurrentCell = GridViewDesign.Rows[PreRow].Cells[0];
                lblDesignRowNumberIndicator.Text = (PreRow + 1).ToString() + " / " + GridViewDesign.Rows.Count.ToString();
            }
        }

        private void btn_Design_Next_Click(object sender, EventArgs e)
        {
            if (GridViewDesign.Rows.Count > 1)
            {
                int nRow = GridViewDesign.Rows.Count;
                int NextRow = GridViewDesign.CurrentRow.Index + 1;
                NextRow = (NextRow < nRow - 1 ? NextRow : nRow - 1);
                GridViewDesign.CurrentCell = GridViewDesign.Rows[NextRow].Cells[0];
                lblDesignRowNumberIndicator.Text = (NextRow + 1).ToString() + " / " + GridViewDesign.Rows.Count.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(txtSchemeName, null);
            if (txtSchemeName.Text.Trim().Length <= 0)
            {
                errorProvider.SetError(txtSchemeName, "You must enter a schema name, please try again!");
                return;
            }
            errorProvider.SetError(txtSchemeName, null);



            if (txtSchemeName.Text.ToLower() == "select" || txtSchemeName.Text.ToLower() == "from" || txtSchemeName.Text.ToLower() == "where")
            {
                errorProvider.SetError(txtSchemeName, "Schema name is not valid (not match with keyword 'select', 'from', 'where')  ");
                return;

            }



            if (this.currentSchemeName != txtSchemeName.Text.Trim().ToLower())
            {
                foreach (var item in this.probDatabase.ListOfSchemeNameToLower())
                {
                    if (item.Equals(txtSchemeName.Text.ToLower()))
                    {
                        errorProvider.SetError(txtSchemeName, "This schema name has already existed in the database, please try again !");
                        return;
                    }
                }
            }



            errorProvider.SetError(txtSchemeName, null);

            

            if (new frm_new_scheme().CheckValidatedDataGridView(this.GridViewDesign) == false)
            {
                return;
            }
            
            currentScheme.SchemeName = txtSchemeName.Text;
            currentScheme.Update();



            foreach (ProbAttribute attr in currentScheme.Attributes)
            {
                attr.probScheme = currentScheme;
                attr.DeleteAllAttributeByIdScheme();
            }

            currentScheme.Attributes.Clear();

            currentScheme.Attributes = new frm_new_scheme().getAllAttributeFromDataGridView(this.GridViewDesign);

            int ID = new ProbAttribute().getMaxIdinTable();

            foreach (ProbAttribute attr in currentScheme.Attributes)
            {
                attr.IDAttribute = ID;
                attr.probScheme = currentScheme;
                attr.Insert();
                ID++;
            }
                 


            if (MessageBox.Show("Edit successfully, Do you want edit another schema  ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                comboBox_SchemeName.Focus();
                this.scheme = currentScheme.SchemeName;
                frm_edit_scheme_Load(sender, e);

            }
            else
                this.Close();
            

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
              
        private void GridViewDesign_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                GridViewDesign.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";


                if (GridViewDesign.CurrentCell.Value != null)
                {
                    if (e.ColumnIndex == 1)
                        for (int i = 0; i < GridViewDesign.Rows.Count - 1; i++)
                            if (GridViewDesign.CurrentCell.Value.ToString().CompareTo(GridViewDesign.Rows[i].Cells[1].Value.ToString()) == 0 && GridViewDesign.CurrentCell.RowIndex != i)
                            {
                                GridViewDesign.Rows[e.RowIndex].Cells[1].ErrorText = "There is already an attribute with the same name!";
                                GridViewDesign.ClearSelection();
                                GridViewDesign.CurrentCell.Selected = true;
                                break;
                            }
                    string temp = GridViewDesign.CurrentCell.Value.ToString();
                    GridViewDesign.CurrentCell.ToolTipText = temp;

                }


                if (e.ColumnIndex == 2 && GridViewDesign.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    GridViewDesign.Rows[e.RowIndex].Cells[2].ErrorText = "Required !";
                    GridViewDesign.CurrentCell.Selected = true;

                }


                if (e.ColumnIndex == 1 && GridViewDesign.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    GridViewDesign.Rows[e.RowIndex].Cells[1].ErrorText = "Required !";
                    GridViewDesign.CurrentCell.Selected = true;
                }


            }
            catch (Exception)
            {


            }
        }

        private void GridViewDesign_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (GridViewDesign.CurrentRow != null)
                lblDesignRowNumberIndicator.Text = (GridViewDesign.CurrentRow.Index + 1) + " / " + GridViewDesign.Rows.Count;
            else lblDesignRowNumberIndicator.Text = "1 / " + GridViewDesign.Rows.Count;
  
        }
        
        private void GridViewDesign_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (lblInfomation.Visible == false && e.ColumnIndex == 2)
                {
                    Form_InputType frm;

                    if (GridViewDesign.Rows[e.RowIndex].Cells[2].Value != null)
                    {
                        frm = new Form_InputType(GridViewDesign.Rows[e.RowIndex].Cells[2].Value.ToString(), GridViewDesign.Rows[e.RowIndex].Cells[3].Value.ToString());
               
                    }
                    else
                    {
                        frm = new Form_InputType();
                    }



                    frm.ShowDialog();
                    if (frm.dataType.DataType != new ProbDataType().DataType)
                    {
                        if (frm.dataType.TypeName == "")
                        {
                            GridViewDesign.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = frm.dataType.DataType;
                        }
                        else
                            GridViewDesign.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = frm.dataType.TypeName;
                        GridViewDesign.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = frm.dataType.DomainString;
                    }
                }
            }
            catch (Exception)
            {
                
            }           

           
        }

        private void Btn_Design_DeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewDesign.Rows.Remove(GridViewDesign.CurrentRow);
                lblDesignRowNumberIndicator.Text = GridViewDesign.CurrentRow.Index + 1 + " / " + GridViewDesign.Rows.Count;
            }
            catch { }
        }

        private void Btn_Design_ClearData_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure want to clear all attribute value ?", "Clear All Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int n = GridViewDesign.Rows.Count - 2;
                    for (int i = n; i >= 0; i--)
                        GridViewDesign.Rows.Remove(GridViewDesign.Rows[i]);
                    lblDesignRowNumberIndicator.Text = "1 / 1";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}