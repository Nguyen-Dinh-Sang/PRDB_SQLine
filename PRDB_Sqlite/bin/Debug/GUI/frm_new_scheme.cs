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
    public partial class frm_new_scheme : DevExpress.XtraEditors.XtraForm
    {
        public BLL.ProbDatabase probDatabase;
        public List<ProbScheme> listProbScheme = new List<ProbScheme>();


        public frm_new_scheme()
        {
            InitializeComponent();
        }

        public frm_new_scheme(BLL.ProbDatabase probDatabase)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.probDatabase = probDatabase;
        }


        public List<ProbAttribute> getAllAttributeFromDataGridView(DataGridView gridViewDesign)
        {
            List<ProbAttribute> probAttributes = new List<ProbAttribute>();

            int nRow = gridViewDesign.Rows.Count - 1;
           
            for (int i = 0; i < nRow; i++)
            {
                ProbAttribute  attribute = new ProbAttribute();
                attribute.PrimaryKey = Convert.ToBoolean(gridViewDesign.Rows[i].Cells[0].Value);
                attribute.AttributeName = gridViewDesign.Rows[i].Cells[1].Value.ToString();
                attribute.Type.TypeName = gridViewDesign.Rows[i].Cells[2].Value.ToString();
                attribute.Type.GetDomain(gridViewDesign.Rows[i].Cells[3].Value.ToString());
                attribute.Description = (gridViewDesign.Rows[i].Cells[4].Value == null ? "" : gridViewDesign.Rows[i].Cells[4].Value.ToString());
                attribute.Type.GetDataType();
                probAttributes.Add(attribute);             
            }
            return probAttributes;        
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
                errorProvider.SetError(txtSchemeName, "Schema name is not valid ( not match with keyword 'select', 'from', 'where')  ");
                return;
            
            }



            foreach (var item in this.probDatabase.ListOfSchemeNameToLower())
            {
                if(item.Equals(txtSchemeName.Text.ToLower()))
                {
                    errorProvider.SetError(txtSchemeName, "This schema name has already existed in the database, please try again !");
                     return;
                }
            }

                       

            errorProvider.SetError(txtSchemeName, null);



            if (CheckValidatedDataGridView(this.GridViewDesign) == false)
            {
                return;
            }


            //insert scheme
            ProbScheme scheme = new ProbScheme(txtSchemeName.Text);
            scheme.IDScheme = scheme.getMaxIdinTable();          
            scheme.Insert();
            scheme.Attributes = getAllAttributeFromDataGridView(GridViewDesign);


            //insert attribute
            int attributeID = new ProbAttribute().getMaxIdinTable();
            foreach (ProbAttribute attr in scheme.Attributes)
            {                        
                 attr.probScheme = scheme;
                 attr.IDAttribute = attributeID;
                 attr.Insert();
                 attributeID++;
            }
            

            /// add scheme 
            this.listProbScheme.Add(scheme);



            if (MessageBox.Show("Add successfully. Do you want add new schema ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                
                txtSchemeName.Text = "";
                GridViewDesign.Rows.Clear();
                txtSchemeName.Focus();
            }
            else
                this.Close();
                


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public bool CheckValidatedDataGridView(DataGridView gridView)
        {
           
                int nRow = gridView.Rows.Count - 1;
            
                if (nRow <= 0)
                {
                    MessageBox.Show("Error: Unable to create Schema. Schema attribute is required !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }




                for (int i = 0; i < nRow ; i++)
                {

                    gridView.Rows[i].Cells[1].ErrorText = null;
                    gridView.Rows[i].Cells[2].ErrorText = null;
                    
                                            
                        if (gridView.Rows[i].Cells[1].Value == null )
                        {
                            gridView.Rows[i].Cells[1].ErrorText = "Required !";
                            gridView.CurrentCell = gridView.Rows[i].Cells[1];
                            return false;
                        }



                        string attributeName = gridView.Rows[i].Cells[1].Value.ToString().Trim();
                        if (attributeName.ToLower() == "select" || attributeName.ToLower() == "from" || attributeName.ToLower() == "where")
                        {
                            gridView.Rows[i].Cells[1].ErrorText = "Attribute name is not valid ( not match with keyword 'select', 'from', 'where')";
                            return false;
                        }



                        for (int j = 0; j < gridView.Rows.Count - 1; j++)
                        {
                            if (i != j &&  gridView.Rows[j].Cells[1].Value != null && gridView.Rows[j].Cells[1].Value.ToString().ToLower().CompareTo(gridView.Rows[i].Cells[1].Value.ToString().ToLower()) == 0 && i != j)
                            {
                                gridView.Rows[i].Cells[1].ErrorText = "There is already an attribute with the same name!";
                                gridView.ClearSelection();
                                gridView.CurrentCell = gridView.Rows[i].Cells[1];
                                return false;
                            }

                        }



                        if (gridView.Rows[i].Cells[2].Value == null )
                        {
                            gridView.Rows[i].Cells[2].ErrorText = "Required !";
                            gridView.CurrentCell = gridView.Rows[i].Cells[1];
                            return false;
                        }

                      

                }                
                      
            return true;
           
        }


         private void GridViewDesign_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
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

        private void GridViewDesign_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                GridViewDesign.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
                

                if (GridViewDesign.CurrentCell.Value != null)
                {
                    if (e.ColumnIndex == 1)
                        for (int i = 0; i < GridViewDesign.Rows.Count - 1; i++)
                            if (GridViewDesign.CurrentCell.Value.ToString().CompareTo(GridViewDesign.Rows[i].Cells[1].Value.ToString()) == 0 && GridViewDesign.CurrentCell.RowIndex != i)
                            {
                                GridViewDesign.Rows[e.RowIndex].Cells[1].ErrorText = "There is already an attribute with the same name, please try again !";
                                GridViewDesign.ClearSelection();
                                GridViewDesign.CurrentCell.Selected = true;
                                break;
                            }
                    string temp = GridViewDesign.CurrentCell.Value.ToString();
                    GridViewDesign.CurrentCell.ToolTipText = temp;

                }


                if (e.ColumnIndex == 1 && GridViewDesign.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    GridViewDesign.Rows[e.RowIndex].Cells[1].ErrorText = "Required !";
                    GridViewDesign.CurrentCell.Selected = true;
                    return;
                }

                if (e.ColumnIndex == 2 && GridViewDesign.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    GridViewDesign.Rows[e.RowIndex].Cells[2].ErrorText = "Required !";
                    GridViewDesign.CurrentCell.Selected = true;
                    return;

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

        private void frm_new_scheme_Load(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }


     

      

       
    }
}