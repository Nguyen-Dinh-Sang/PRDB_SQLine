using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PRDB_Sqlite.BLL;
using System.Data.SQLite;
using PRDB_Sqlite.DAL;
using System.Linq;
using DevExpress.XtraBars.Ribbon;

namespace PRDB_Sqlite.GUI
{
    public partial class Form_Main : DevExpress.XtraEditors.XtraForm
    {

        #region  *ProbDatabase

        ProbDatabase probDatabase;
        ProbRelation currentRelationOpen = new ProbRelation();
        ProbScheme currentScheme;
        ProbQuery currentQuery;
        #endregion


        #region * TreeView
        TreeNode NodeDB, NodeScheme, NodeRelation, NodeQuery, NewNode, NodeAttribute;
        #endregion

        #region * Images
        public struct ImageIndex
        {
            public int UnselectedState;
            public int SelectedState;
        }
        #endregion

        #region * Variables
        ImageIndex DB_ImgIndex, Folder_ImgIndex, Scheme_ImgIndex, Relation_ImgIndex, Query_ImgIndex, Attribute_ImgIndex;
        static public string[] Operator = new string[17] { "and", "or", "<", ">", "<=", ">=", "=", "!=", "⊗_in", "⊗_ig", "⊗_me", "⊕_in", "⊕_ig", "⊕_me", "equal_in", "equal_ig", "equal_me" };
        public ProbTriple row { get; set; }


        #endregion

        public Form_Main()
        {
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {

            this.gridControlScheme.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControlScheme.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControlScheme.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControlScheme.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControlScheme.EmbeddedNavigator.Buttons.Remove.Visible = false;


            LoadPRDB();


        }

        #region Active DB

        private void LoadPRDB()
        {
            BindingNavigatorData.Visible = true;
            BindingNavigatorValue.Visible = true;
            SwitchValueState(true);
            ActivateDatabase(false);
        }

        private void ActivateDatabase(bool state)
        {

            ResetSchemePage(state);
            ResetRelationPage(state);
            ResetQueryPage(state);
            ResetInputValue(state);
            ResetMenuBar(state);

        }

        private void ResetMenuBar(bool state)
        {

            barButtonItemSaveDB.Enabled = state;
            barButtonItemCloseDb.Enabled = state;
            ribbonPageScheme.Visible = state;
            ribbonPageRelation.Visible = state;
            ribbonPageQuery.Visible = state;

        }

        private void ResetInputValue(bool state)
        {
            txtMinProb.Text = "";
            txtMaxProb.Text = "";
            txtValue.ResetText();
            GridViewValue.Rows.Clear();
            UpdateValueRowNumber();

            Checkbox_UUD.Enabled = state;
            Checkbox_UD.Enabled = state;

            txtMinProb.Enabled = state;
            txtMaxProb.Enabled = state;


            btn_Value_AddNewRow.Enabled = state;
            btn_Value_DeleteRow.Enabled = state;
            Btn_Value_ClearData.Enabled = state;
            btn_Value_UpdateValue.Enabled = state;
        }

        private void ResetQueryPage(bool state)
        {
            currentQuery = null;
            xtraTabDatabase.TabPages[2].Text = "Query";
            txtQuery.Text = "";
            barButtonItemExcuteQuery.Enabled = false;
            barButtonItemSaveQuery.Enabled = false;
            barButtonItemCloseCurrentQuery.Enabled = false;

            ribbonPageGroupEquality.Visible = false;
            ribbonPageGroupConjuntion.Visible = false;
            ribbonPageGroupDisjunction.Visible = false;
            ribbonPageGroupDifference.Visible = false;

            //
            ribbonPageGroupOperationsOnSet.Visible = false;
            //

            xtraTabPageQuery.PageEnabled = false;

        }

        private void ResetRelationPage(bool state)
        {
            xtraTabDatabase.TabPages[1].Text = "Relation";
            GridViewData.Rows.Clear();
            GridViewData.Columns.Clear();
            UpdateDataRowNumber();
            GridViewValue.Rows.Clear();
            UpdateValueRowNumber();
            Btn_Data_DeleteRow.Enabled = state;
            Btn_Data_ClearData.Enabled = state;
            Btn_Data_UpdateData.Enabled = state;
            xtraTabPageRelation.PageEnabled = state;
        }

        private void ResetSchemePage(bool state)
        {
            xtraTabDatabase.TabPages[0].Text = "Scheme";
            gridControlScheme.DataSource = null;
            gridControlScheme.Enabled = state;
            xtraTabPageScheme.PageEnabled = state;
        }

        #endregion



        #region TreeView


        private void LoadImageCollection()
        {
            try
            {
                TreeView.ImageList = ImageList_TreeView;
                DB_ImgIndex.SelectedState = DB_ImgIndex.UnselectedState = 0;
                Folder_ImgIndex.UnselectedState = 1;
                Folder_ImgIndex.SelectedState = 2;
                Scheme_ImgIndex.SelectedState = Scheme_ImgIndex.UnselectedState = 3;
                Relation_ImgIndex.SelectedState = Relation_ImgIndex.UnselectedState = 3;
                Query_ImgIndex.SelectedState = Query_ImgIndex.UnselectedState = 3;
                Attribute_ImgIndex.SelectedState = 5;
                Attribute_ImgIndex.UnselectedState = 6;
            }
            catch (Exception)
            {


            }

        }
        private void Load_TreeView()
        {
            try
            {
                TreeView.Nodes.Clear();
                TreeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(TreeView_NodeMouseClick);
                LoadImageCollection();

                NodeDB = new TreeNode();
                NodeDB.Text = probDatabase.DBName.ToUpper();
                NodeDB.ToolTipText = "Database " + probDatabase.DBName.ToUpper();
                NodeDB.ContextMenuStrip = ContextMenu_Database;
                NodeDB.ImageIndex = DB_ImgIndex.UnselectedState;
                NodeDB.SelectedImageIndex = DB_ImgIndex.SelectedState;
                TreeView.Nodes.Add(NodeDB);

                NodeScheme = new TreeNode();
                NodeScheme.Text = "Schemas";
                NodeScheme.ToolTipText = "Schemas";
                NodeScheme.ContextMenuStrip = ContextMenu_Schema;
                NodeScheme.ImageIndex = Folder_ImgIndex.UnselectedState;
                NodeScheme.SelectedImageIndex = Folder_ImgIndex.UnselectedState;
                NodeDB.Nodes.Add(NodeScheme);

                NodeRelation = new TreeNode();
                NodeRelation.Text = "Relations";
                NodeRelation.ToolTipText = "Relations";
                NodeRelation.ContextMenuStrip = ContextMenu_Relation;
                NodeRelation.ImageIndex = Folder_ImgIndex.UnselectedState;
                NodeRelation.SelectedImageIndex = Folder_ImgIndex.UnselectedState;
                NodeDB.Nodes.Add(NodeRelation);

                NodeQuery = new TreeNode();
                NodeQuery.Text = "Queries";
                NodeQuery.ToolTipText = "Queries";
                NodeQuery.ContextMenuStrip = ContextMenu_Query;
                NodeQuery.ImageIndex = Folder_ImgIndex.UnselectedState;
                NodeQuery.SelectedImageIndex = Folder_ImgIndex.UnselectedState;
                NodeDB.Nodes.Add(NodeQuery);

                LoadTreeViewNode();
            }
            catch
            {


            }

        }

        void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeView.SelectedNode = e.Node;

            }

            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    string nodeName = e.Node.Name;
                    switch (nodeName)
                    {
                        case "Schemas":
                            {
                                OpenSchemeByNameScheme(nodeName);
                                break;
                            }
                        case "Relations":
                            {
                                OpenRelationByName(nodeName);
                                break;
                            }
                        case "Queries":
                            {
                                OpenQueryByName(nodeName);
                                break;
                            }
                    }
                }
                catch
                {


                }
            }

        }

        public void LoadAttributeScheme()
        {
            try
            {


                if (NodeAttribute != null)
                    NodeAttribute.Nodes.Clear();

                for (int i = 0; i < NodeScheme.GetNodeCount(true); i++)
                {
                    string schemeName = NodeScheme.Nodes[i].Name;
                    ProbScheme schme = this.probDatabase.Schemes.SingleOrDefault(c => c.SchemeName == schemeName);
                    foreach (ProbAttribute attr in schme.Attributes)
                    {
                        NodeAttribute = new TreeNode();
                        NodeAttribute.Text = attr.AttributeName;
                        NodeAttribute.Name = attr.AttributeName;
                        NodeAttribute.ToolTipText = "Attribute " + attr.AttributeName;

                        if (attr.PrimaryKey == true)
                        {
                            NodeAttribute.ImageIndex = 5;
                            NodeAttribute.SelectedImageIndex = 5;
                        }
                        else
                        {
                            NodeAttribute.ImageIndex = 6;
                            NodeAttribute.SelectedImageIndex = 6;

                        }
                        NodeScheme.Nodes[i].Nodes.Add(NodeAttribute);
                    }

                }
            }
            catch (Exception)
            {


            }

        }



        public void LoadAttributeRelation()
        {
            try
            {
                if (NodeAttribute != null)
                    NodeAttribute.Nodes.Clear();

                for (int i = 0; i < NodeRelation.GetNodeCount(true); i++)
                {
                    string relationName = NodeRelation.Nodes[i].Name;
                    ProbRelation relation = this.probDatabase.Relations.SingleOrDefault(c => c.RelationName == relationName);

                    foreach (ProbAttribute attr in relation.Scheme.Attributes)
                    {
                        NodeAttribute = new TreeNode();
                        NodeAttribute.Text = attr.AttributeName;
                        NodeAttribute.Name = attr.AttributeName + "relation";
                        NodeAttribute.ToolTipText = "Attribute " + attr.AttributeName;

                        if (attr.PrimaryKey == true)
                        {
                            NodeAttribute.ImageIndex = 5;
                            NodeAttribute.SelectedImageIndex = 5;
                        }
                        else
                        {
                            NodeAttribute.ImageIndex = 6;
                            NodeAttribute.SelectedImageIndex = 6;

                        }
                        NodeRelation.Nodes[i].Nodes.Add(NodeAttribute);
                    }

                }






            }
            catch
            {


            }






        }

        public void LoadSchemeNode()
        {
            try
            {
                NodeScheme.Nodes.Clear();
                foreach (ProbScheme scheme in probDatabase.Schemes)
                {
                    NewNode = new TreeNode();
                    NewNode.Text = scheme.SchemeName.ToLower();
                    NewNode.Name = scheme.SchemeName;
                    NewNode.ToolTipText = "Schema " + scheme.SchemeName;
                    NewNode.ContextMenuStrip = ContextMenu_SchemaNode;
                    NewNode.ImageIndex = Scheme_ImgIndex.UnselectedState;
                    NewNode.SelectedImageIndex = Scheme_ImgIndex.UnselectedState;
                    NodeScheme.Nodes.Add(NewNode);
                }
                LoadAttributeScheme();
            }
            catch
            {


            }

        }

        public void LoadRelationNode()
        {
            try
            {
                NodeRelation.Nodes.Clear();
                foreach (ProbRelation relation in probDatabase.Relations)
                {
                    NewNode = new TreeNode();
                    NewNode.Text = relation.RelationName.ToLower();
                    NewNode.Name = relation.RelationName;
                    NewNode.ToolTipText = "Relation " + relation.RelationName.ToLower();
                    NewNode.ContextMenuStrip = ContextMenu_RelationNode;
                    NewNode.ImageIndex = Relation_ImgIndex.UnselectedState;
                    NewNode.SelectedImageIndex = Relation_ImgIndex.UnselectedState;
                    NodeRelation.Nodes.Add(NewNode);
                }
                LoadAttributeRelation();
            }
            catch
            {


            }

        }

        public void LoadQueryNode()
        {
            try
            {
                NodeQuery.Nodes.Clear();
                foreach (ProbQuery query in probDatabase.Queries)
                {
                    NewNode = new TreeNode();
                    NewNode.Text = query.QueryName.ToLower();
                    NewNode.Name = query.QueryName;
                    NewNode.ToolTipText = "Query " + query.QueryName;
                    NewNode.ContextMenuStrip = contextMenu_QueryNode;
                    NewNode.ImageIndex = Query_ImgIndex.UnselectedState;
                    NewNode.SelectedImageIndex = Query_ImgIndex.SelectedState;
                    NodeQuery.Nodes.Add(NewNode);
                }

            }
            catch
            {


            }


        }
        private void LoadTreeViewNode()
        {
            try
            {
                LoadSchemeNode();
                LoadRelationNode();
                LoadQueryNode();


            }
            catch
            {


            }

        }
        #endregion


        #region New,Open, Save, Close, Exit Database, CTMenuDB close, Rename

        private string GetRootPath(string path)
        {

            string root = "";
            try
            {

                for (int i = 0; i < path.Length; i++)
                    if (path[i] == '\\')
                    {
                        root = path.Substring(0, i + 1);
                        break;
                    }
                return root;

            }
            catch (Exception)
            {


            }
            return root;

        }




        private void barButtonItemNewDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {



            try
            {
                SaveFileDialog DialogSave = new SaveFileDialog();                                   // Save dialog
                DialogSave.DefaultExt = "pdb";                                                      // Default extension
                DialogSave.Filter = "Database file (*.pdb)|*.pdb";                                  // add extension to dialog
                //DialogSave.Filter = "Database file (*.pdb)|*.pdb|All files (*.*)|*.*";               
                DialogSave.AddExtension = true;                                                     // enable adding extension
                DialogSave.RestoreDirectory = true;                                                 // Tu dong phuc hoi duong dan cho lan sau
                DialogSave.Title = "Create new database...";
                DialogSave.InitialDirectory = GetRootPath(AppDomain.CurrentDomain.BaseDirectory.ToString());
                DialogSave.SupportMultiDottedExtensions = true;

                if (DialogSave.ShowDialog() == DialogResult.OK)
                {





                    this.probDatabase = new ProbDatabase(DialogSave.FileName);
                    Resource.ConnectionString = probDatabase.ConnectString;

                    if (!probDatabase.CreateNewDatabase())
                    {
                        MessageBox.Show("Error : Cannot create new database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        this.Load_TreeView();
                        this.ActivateDatabase(true);
                    }
                }
                DialogSave.Dispose();
            }
            catch
            {

            }

        }

        private void barButtonItemOpenDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog DialogOpen = new OpenFileDialog();
                DialogOpen.DefaultExt = "pdb";
                DialogOpen.Filter = "Database file (*.pdb)|*.pdb";
                DialogOpen.AddExtension = true;
                DialogOpen.RestoreDirectory = true;
                DialogOpen.Title = "Open database...";
                DialogOpen.InitialDirectory = GetRootPath(AppDomain.CurrentDomain.BaseDirectory.ToString());
                DialogOpen.SupportMultiDottedExtensions = true;

                if (DialogOpen.ShowDialog() == DialogResult.OK)
                {

                    this.probDatabase = new ProbDatabase(DialogOpen.FileName);
                    Resource.ConnectionString = this.probDatabase.ConnectString;
                    this.probDatabase = probDatabase.OpenExistingDatabase();
                    if (probDatabase == null)
                    {
                        MessageBox.Show("Error : Cannot connect to the database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        this.Load_TreeView();
                        this.ActivateDatabase(true);
                    }

                }
                DialogOpen.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        private void barButtonItemSaveDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!probDatabase.SaveDatabase())
                {
                    MessageBox.Show("Error : Cannot save the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {

                    MessageBox.Show("Save successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            catch (Exception)
            {


            }
        }

        private void barButtonItemCloseDb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CloseDatabase();
        }

        private void barButtonItemExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure want to exit?", "Exit PRDB Visual Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CloseDatabase()
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure want to close this Database ?", "Close database " + probDatabase.DBName + "...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.TreeView.Nodes.Clear();
                    this.probDatabase = null;
                    ActivateDatabase(false);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuDB_CloseDB_Click(object sender, EventArgs e)
        {
            CloseDatabase();
        }
        private void CTMenuDB_Rename_Click(object sender, EventArgs e)
        {
            frm_Rename_DB frm = new frm_Rename_DB(this.probDatabase);
            frm.ShowDialog();
            this.probDatabase = frm.probDatabase;
            NodeDB.Text = this.probDatabase.DBName.ToUpper();
            NodeDB.ToolTipText = "Database " + this.probDatabase.DBName.ToUpper(); ;



        }


        #endregion



        #region New, Open, Save, Delete, Close, CTMenu Scheme
        private void OpenSchemeByNameScheme(string schemeName)
        {

            try
            {
                xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[0];
                schemeName = schemeName.ToLower();
                xtraTabPageScheme.Text = "Schema " + schemeName;

                currentScheme = this.probDatabase.Schemes.SingleOrDefault(c => c.SchemeName.ToLower() == schemeName);
                //add attribute into GridViewDesign


                gridControlScheme.DataSource = null;
                gridControlScheme.DataSource = currentScheme.Attributes;
                gridColumnPrimary.FieldName = "PrimaryKey";
                gridColumnDomain.FieldName = "DomainString";
                gridColumnDescription.FieldName = "Description";
                gridColumnDataType.FieldName = "TypeName";
                gridColumnAttribute.FieldName = "AttributeName";

                barButtonItemCloseCurrentScheme.Enabled = true;
                ribbonControl1.SelectedPage = ribbonPageScheme;

            }
            catch
            {


            }

        }
        private void OpenScheme()
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frm_Open_Scheme frm = new frm_Open_Scheme(this.probDatabase.ListOfSchemeNameToLower());
                frm.ShowDialog();

                string schemeSelected = frm.selected;
                if (schemeSelected != string.Empty)
                {
                    OpenSchemeByNameScheme(schemeSelected);
                    barButtonItemCloseCurrentScheme.Enabled = true;
                }
                ribbonControl1.SelectedPage = ribbonPageScheme;
            }
            catch (Exception)
            {


            }

        }
        private void DeleteScheme()
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                frm_Delete_Scheme frm = new frm_Delete_Scheme(this.probDatabase);
                frm.ShowDialog();
                this.probDatabase = frm.probDatabase;

                if (!this.probDatabase.ListOfSchemeNameToLower().Contains((xtraTabPageScheme.Text.Substring(xtraTabPageScheme.Text.IndexOf("Schema") + 1)).Trim().ToLower()))
                {
                    xtraTabPageScheme.Text = "Schema";
                    gridControlScheme.DataSource = null;
                    gridControlScheme.Update();
                    barButtonItemCloseCurrentScheme.Enabled = false;

                }

                if (xtraTabPageScheme.Text == "Schema")
                    barButtonItemCloseCurrentScheme.Enabled = false;
                else
                    barButtonItemCloseCurrentScheme.Enabled = true;

                LoadSchemeNode();

            }
            catch (Exception)
            {


            }

        }

        private void DeleteScheme(string SchemeName)
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                frm_Delete_Scheme frm = new frm_Delete_Scheme(this.probDatabase, SchemeName);
                frm.ShowDialog();
                this.probDatabase = frm.probDatabase;
                if (!this.probDatabase.ListOfSchemeNameToLower().Contains((xtraTabPageScheme.Text.Substring(xtraTabPageScheme.Text.IndexOf("Schema") + 1)).Trim().ToLower()))
                {

                    xtraTabPageScheme.Text = "Schema";
                    gridControlScheme.DataSource = null;
                    gridControlScheme.Update();
                }

                if (xtraTabPageScheme.Text == "Schema")
                    barButtonItemCloseCurrentScheme.Enabled = false;
                else
                    barButtonItemCloseCurrentScheme.Enabled = true;


                LoadSchemeNode();
            }
            catch (Exception)
            {


            }

        }

        private void EditScheme()
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                frm_edit_scheme frm = new frm_edit_scheme(this.probDatabase);
                frm.ShowDialog();

                if (currentScheme != null)
                {
                    xtraTabPageScheme.Text = "Schema " + currentScheme.SchemeName;
                }



                this.probDatabase = frm.probDatabase;
                this.LoadSchemeNode();


            }
            catch
            {
            }
        }

        private void EditScheme(string schemeName)
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Cannot find the Database !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                frm_edit_scheme frm = new frm_edit_scheme(this.probDatabase, schemeName);
                frm.ShowDialog();


                this.probDatabase = frm.probDatabase;
                this.LoadSchemeNode();
                if (currentScheme != null)
                {
                    xtraTabPageScheme.Text = "Schema " + currentScheme.SchemeName;
                }
                xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[0];

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }



        #region new scheme name
        private void barButtonItemNewScheme_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                CreateNewScheme();
            }
            catch (Exception)
            {


            }

        }

        private void CreateNewScheme()
        {
            try
            {

                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                frm_new_scheme frm = new frm_new_scheme(probDatabase);
                frm.ShowDialog();

                foreach (ProbScheme scheme in frm.listProbScheme)
                {
                    this.probDatabase.Schemes.Add(scheme);
                    TreeNode NewNode = new TreeNode();
                    NewNode.Name = scheme.SchemeName;
                    NewNode.Text = scheme.SchemeName;
                    NewNode.ToolTipText = "Schema " + scheme.SchemeName;
                    NewNode.ContextMenuStrip = ContextMenu_SchemaNode;
                    NewNode.ImageIndex = Scheme_ImgIndex.UnselectedState;
                    NewNode.SelectedImageIndex = Scheme_ImgIndex.UnselectedState;
                    NodeScheme.Nodes.Add(NewNode);

                }
                LoadAttributeScheme();

            }
            catch
            {

            }
        }


        #endregion

        private void barButtonItemSaveScheme_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                EditScheme();

            }
            catch (Exception)
            {

            }

        }
        private void barButtonItemOpenScheme_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenScheme();
            }
            catch (Exception)
            {


            }


        }
        private void barButtonItemDeleteScheme_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DeleteScheme();

            }
            catch
            {


            }

        }

        private void barButtonItemCloseCurrentScheme_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabPageScheme.Text = "Schema ";
            gridControlScheme.DataSource = null;
            barButtonItemCloseCurrentScheme.Enabled = false;


        }

        private void CTMenuSchema_NewSchema_Click(object sender, EventArgs e)
        {
            try
            {
                CreateNewScheme();
            }
            catch (Exception)
            {


            }

        }

        private void CTMenuSchema_DelSchemas_Click(object sender, EventArgs e)
        {
            try
            {
                EditScheme();
            }
            catch (Exception)
            {


            }

        }

        private void openSchemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenScheme();
            }
            catch (Exception)
            {


            }

        }

        private void deleteSchemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteScheme();
            }
            catch (Exception)
            {

            }

        }

        private void closeCurrentSchemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                xtraTabPageScheme.Text = "Schema ";
                gridControlScheme.DataSource = null;
            }
            catch (Exception)
            {

            }

        }

        private void CTMenuSchNode_OpenSchema_Click(object sender, EventArgs e)
        {
            try
            {
                string schemeName = TreeView.SelectedNode.Name;
                OpenSchemeByNameScheme(schemeName);

            }
            catch (Exception)
            {


            }


        }

        private void CTMenuSchNode_DeleteSchema_Click(object sender, EventArgs e)
        {
            try
            {
                string schemeName = TreeView.SelectedNode.Name;
                DeleteScheme(schemeName);

            }
            catch (Exception)
            {


            }
        }

        private void CTMenuSchNode_EditSchema_Click(object sender, EventArgs e)
        {
            try
            {
                string schemeName = TreeView.SelectedNode.Name;
                EditScheme(schemeName.ToLower());

            }
            catch (Exception)
            {


            }
        }

        #endregion



        #region Relation

        private void NewRelation()
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (this.probDatabase.Schemes.Count == 0)
                {
                    MessageBox.Show("The first, you must create some schema, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                frm__new_relation frm = new frm__new_relation(this.probDatabase);
                frm.ShowDialog();
                this.probDatabase = frm.probDatabase;
                LoadRelationNode();
            }
            catch (Exception)
            {
            }
        }

        private void barButtonItemNewRelation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                NewRelation();
            }
            catch (Exception)
            {
            }
        }

        private void OpenRelationByName(string RelationName)
        {
            try
            {
                xtraTabPageRelation.PageEnabled = true;
                xtraTabPageRelation.Text = "Relation " + RelationName;
                xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[1];
                GridViewData.Rows.Clear();
                GridViewData.Columns.Clear();

                currentRelationOpen = this.probDatabase.Relations.SingleOrDefault(c => c.RelationName == RelationName);

                int i = 0;
                foreach (ProbAttribute attr in currentRelationOpen.Scheme.Attributes)
                {
                    GridViewData.Columns.Add("Column " + i, attr.AttributeName);
                    i++;
                }

                if (currentRelationOpen.tuples.Count > 0)
                {
                    int nRow = currentRelationOpen.tuples.Count;
                    int nCol = currentRelationOpen.Scheme.Attributes.Count;

                    ProbTuple tuple;

                    for (i = 0; i < nRow; i++)      // Assign data for GridViewData
                    {
                        tuple = currentRelationOpen.tuples[i];
                        GridViewData.Rows.Add();
                        for (int j = 0; j < nCol; j++)
                            GridViewData.Rows[i].Cells[j].Value = tuple.Triples[j].GetStrValue();
                    }
                    UpdateDataRowNumber();

                }
                barButtonItemCloseCurrentRelation.Enabled = true;
                ribbonControl1.SelectedPage = ribbonPageRelation;

            }
            catch (Exception)
            {
            }
        }

        private void UpdateDataRowNumber()
        {
            try
            {
                if (GridViewData.Rows.Count == 0)
                    lblDataRowNumberIndicator.Text = "0 / 0";
                else if (GridViewData.CurrentRow != null)
                    lblDataRowNumberIndicator.Text = (GridViewData.CurrentRow.Index + 1) + " / " + GridViewData.Rows.Count;
                else lblDataRowNumberIndicator.Text = "1 / " + GridViewData.Rows.Count;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void barButtonItemOpenRelation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.probDatabase.Relations.Count == 0)
                {
                    MessageBox.Show("The first, you must create some relation, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frm_open_relation frm = new frm_open_relation(this.probDatabase.ListOfRelationNameToLower());
                frm.ShowDialog();

                if (frm.relationName != string.Empty)
                {
                    OpenRelationByName(frm.relationName);
                }
            }
            catch (Exception)
            {


            }



        }

        private void ribbonControl1_SelectedPageChanging(object sender, DevExpress.XtraBars.Ribbon.RibbonPageChangingEventArgs e)
        {
            string value = e.Page.Name;
            switch (value)
            {
                case "ribbonPageScheme":
                    xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[0];

                    if (xtraTabDatabase.TabPages[0].Text.Trim() == "Schema")
                    {

                        barButtonItemCloseCurrentScheme.Enabled = false;
                    }
                    else
                        barButtonItemCloseCurrentScheme.Enabled = true;

                    break;
                case "ribbonPageRelation":
                    xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[1];

                    if (xtraTabDatabase.TabPages[1].Text.Trim() == "Relation")
                    {
                        xtraTabDatabase.TabPages[1].PageEnabled = false;
                        barButtonItemCloseCurrentRelation.Enabled = false;
                    }
                    else
                        barButtonItemCloseCurrentRelation.Enabled = true;



                    break;
                case "ribbonPageQuery":
                    xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[2];

                    if (xtraTabDatabase.TabPages[2].Text.Trim() == "Query")
                    {

                        barButtonItemCloseCurrentQuery.Enabled = false;
                    }
                    else
                        barButtonItemCloseCurrentQuery.Enabled = true;


                    break;
                case "ribbonAbout":
                    frm_About frm = new frm_About();
                    frm.ShowDialog();

                    break;

            }



        }

        private void CTMenuRelNode_OpenRelation_Click(object sender, EventArgs e)
        {
            try
            {

                string relationName = TreeView.SelectedNode.Name;
                OpenRelationByName(relationName);


            }
            catch (Exception)
            {
            }

        }

        private void barButtonItemDeleteRelation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.probDatabase.Relations.Count == 0)
                {
                    MessageBox.Show("The first, you must create some relation, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frm_Delete_relation frm = new frm_Delete_relation(this.probDatabase);
                frm.ShowDialog();

                if (xtraTabDatabase.TabPages[1].Text.Contains(frm.relationNameRemove))
                {
                    xtraTabDatabase.TabPages[1].Text = "Relation";
                    GridViewData.Rows.Clear();
                    GridViewData.Columns.Clear();
                    UpdateDataRowNumber();
                    xtraTabDatabase.TabPages[1].PageEnabled = false;
                }

                if (xtraTabPageScheme.Text.Trim() == "Relation")
                    barButtonItemCloseCurrentRelation.Enabled = false;
                else
                    barButtonItemCloseCurrentRelation.Enabled = true;


                this.probDatabase = frm.probDatabase;
                LoadRelationNode();
            }
            catch (Exception)
            {


            }


        }

        private void barButtonItemCloseCurrentRelation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xtraTabDatabase.TabPages[1].Text = "Relation";
            GridViewData.Rows.Clear();
            GridViewData.Columns.Clear();
            UpdateDataRowNumber();
            xtraTabDatabase.TabPages[1].PageEnabled = false;
            barButtonItemCloseCurrentRelation.Enabled = false;
        }

        private void CTMenuRelation_NewRelation_Click(object sender, EventArgs e)
        {
            try
            {
                NewRelation();
            }
            catch (Exception)
            {


            }

        }

        private void CTMenuRelation_DeleteRelations_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.probDatabase.Relations.Count <= 0)
                {
                    MessageBox.Show("No relation to delete ", "Delete All Relations", MessageBoxButtons.OK);
                }

                else
                {


                    DialogResult result = new DialogResult();
                    result = MessageBox.Show("Are you sure want to delete all relations ?", "Delete All Relations", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        NodeRelation.Nodes.Clear();

                        xtraTabDatabase.TabPages[1].Text = "Relation";
                        GridViewData.Rows.Clear();
                        GridViewData.Columns.Clear();
                        UpdateDataRowNumber();




                        if (xtraTabPageScheme.Text.Trim() == "Relation")
                            barButtonItemCloseCurrentRelation.Enabled = false;
                        else
                            barButtonItemCloseCurrentRelation.Enabled = true;

                        foreach (ProbRelation relation in this.probDatabase.Relations)
                        {
                            relation.DropTableByTableName();
                            relation.DeleteRelationById();
                        }
                        this.probDatabase.Relations = new List<ProbRelation>();
                        NodeRelation.ImageIndex = NodeRelation.SelectedImageIndex = Folder_ImgIndex.UnselectedState;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuRelNode_DeleteRelation_Click(object sender, EventArgs e)
        {
            try
            {
                string relationName = TreeView.SelectedNode.Name;
                frm_Delete_relation frm = new frm_Delete_relation(this.probDatabase, relationName);
                frm.ShowDialog();
                this.probDatabase = frm.probDatabase;

                if (xtraTabDatabase.TabPages[1].Text.Contains(frm.relationNameRemove))
                {
                    xtraTabDatabase.TabPages[1].Text = "Relation";
                    GridViewData.Rows.Clear();
                    GridViewData.Columns.Clear();
                    UpdateDataRowNumber();
                    xtraTabDatabase.TabPages[1].PageEnabled = false;
                }

                if (xtraTabPageScheme.Text.Trim() == "Relation")
                    barButtonItemCloseCurrentRelation.Enabled = false;
                else
                    barButtonItemCloseCurrentRelation.Enabled = true;

                LoadRelationNode();
            }
            catch (Exception)
            {

            }

        }

        private void CTMenuRelNode_RenameRelation_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Cannot find the Database !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.probDatabase.Relations.Count == 0)
                {
                    MessageBox.Show("You must create some relation first !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frm_rename_relation frm = new frm_rename_relation(this.probDatabase);
                frm.ShowDialog();

                this.probDatabase = frm.probDatabase;

                xtraTabPageRelation.Text = "Relation " + this.currentRelationOpen.RelationName;

                LoadRelationNode();



            }
            catch (Exception)
            {


            }

        }

        // Click cell in grid
        private void GridViewData_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDataRowNumber();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        // Relation btn
        private void btn_Data_Next_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridViewData.Rows.Count > 0)
                {
                    int nRow = GridViewData.Rows.Count;
                    int NextRow = GridViewData.CurrentRow.Index + 1;
                    NextRow = (NextRow < nRow - 1 ? NextRow : nRow - 1);
                    GridViewData.CurrentCell = GridViewData.Rows[NextRow].Cells[0];
                    lblDataRowNumberIndicator.Text = (NextRow + 1) + " / " + GridViewData.Rows.Count;
                }
            }
            catch (Exception)
            {


            }

        }

        private void btn_Data_Pre_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridViewData.Rows.Count > 0)
                {
                    int PreRow = GridViewData.CurrentRow.Index - 1;
                    PreRow = (PreRow > 0 ? PreRow : 0);
                    GridViewData.CurrentCell = GridViewData.Rows[PreRow].Cells[0];
                    lblDataRowNumberIndicator.Text = (PreRow + 1) + " / " + GridViewData.Rows.Count;
                }
            }
            catch (Exception)
            {


            }

        }

        private void btn_Data_Home_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridViewData.Rows.Count > 0)
                {
                    GridViewData.CurrentCell = GridViewData.Rows[0].Cells[0];
                    lblDataRowNumberIndicator.Text = "1 / " + GridViewData.Rows.Count;
                }
            }
            catch (Exception)
            {

            }

        }

        private void btn_Data_End_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridViewData.Rows.Count > 0)
                {
                    int nRow = GridViewData.Rows.Count;
                    GridViewData.CurrentCell = GridViewData.Rows[nRow - 1].Cells[0];
                    lblDataRowNumberIndicator.Text = nRow + " / " + nRow;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Btn_Data_DeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewData.Rows.Remove(GridViewData.CurrentRow);
                UpdateDataRowNumber();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Btn_Data_ClearData_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure want to clear all data?", "Clear All Data", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    GridViewData.Rows.Clear();
                    UpdateDataRowNumber();



                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Btn_Data_UpdateData_Click(object sender, EventArgs e)
        {
            SaveRelation();

        }

        private void SaveRelation()
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Cannot find the Database !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string RelationName = currentRelationOpen.RelationName;
                int nRow, nCol;
                nRow = GridViewData.Rows.Count - 1;
                nCol = GridViewData.Columns.Count;

                GridViewData.CurrentCell = GridViewData.Rows[nRow].Cells[0];



                if (nRow == 0)
                {
                    return;
                }




                for (int i = 0; i < nRow; i++)
                {
                    for (int j = 0; j < nCol; j++)
                    {
                        GridViewData.Rows[i].Cells[j].ErrorText = null;
                        if (GridViewData.Rows[i].Cells[j].Value == null)
                        {

                            string defaultValue = currentRelationOpen.Scheme.Attributes[j].Type.getDefaultValue();
                            GridViewData.Rows[i].Cells[j].Value = defaultValue;
                            GridViewData.CurrentCell = GridViewData.Rows[i].Cells[j];

                        }
                        else
                        {

                            if (!new ProbTriple().isProbTripleValue(GridViewData.Rows[i].Cells[j].Value.ToString()))
                            {
                                GridViewData.Rows[i].Cells[j].ErrorText = "Syntax Error! Cannot convert this value to a Probabilistic Triple!";
                                GridViewData.CurrentCell = GridViewData.Rows[i].Cells[j];
                                return;
                            }

                            if (!currentRelationOpen.Scheme.Attributes[j].Type.CheckDataType(GridViewData.Rows[i].Cells[j].Value.ToString()))
                            {
                                GridViewData.Rows[i].Cells[j].ErrorText = "Attribute value does not match the data type !";
                                GridViewData.CurrentCell = GridViewData.Rows[i].Cells[j];
                                return;
                            }
                        }
                    }

                }






                #region check primary key
                List<int> indexPrimaryKey = currentRelationOpen.Scheme.ListIndexPrimaryKey();




                for (int i = 0; i < nRow; i++)
                {
                    for (int k = 0; k < indexPrimaryKey.Count; k++)
                    {
                        ProbTriple triple = new ProbTriple(GridViewData.Rows[i].Cells[k].Value.ToString());

                        if (triple.Value2.Count != 1)
                        {
                            GridViewData.Rows[i].Cells[k].ErrorText = "This object is a primary key it only accepts single value ";
                            GridViewData.CurrentCell = GridViewData.Rows[i].Cells[k];
                            return;
                        }
                        //ktr xac xuat duy nhat

                        if (triple.MinProb[0] != 1.0 || triple.MaxProb[0] != 1.0)
                        {
                            GridViewData.Rows[i].Cells[k].ErrorText = "This object is a primary key Its minprob and maxprob must be 1";
                            GridViewData.CurrentCell = GridViewData.Rows[i].Cells[k];
                            return;
                        }

                    }

                }





                for (int i = 0; i < nRow - 1; i++)
                {
                    for (int j = i + 1; j < nRow; j++)
                    {



                        int k = 0;

                        for (k = 0; k < indexPrimaryKey.Count; k++)
                        {

                            ProbTriple tripleOne = new ProbTriple(GridViewData.Rows[i].Cells[k].Value.ToString());
                            ProbTriple tripleTwo = new ProbTriple(GridViewData.Rows[j].Cells[k].Value.ToString());


                            if (tripleOne.GetStrValue() != tripleTwo.GetStrValue())
                                break;
                        }

                        if (k == indexPrimaryKey.Count)
                        {
                            GridViewData.Rows[i].Cells[indexPrimaryKey[k - 1]].ErrorText = " Cannot insert duplicate key in this object ";
                            GridViewData.Rows[j].Cells[indexPrimaryKey[k - 1]].ErrorText = " Cannot insert duplicate key in this object ";
                            GridViewData.CurrentCell = GridViewData.Rows[j].Cells[indexPrimaryKey[k - 1]];
                            return;
                        }

                    }
                }





                #endregion




                currentRelationOpen.DropTableByTableName();
                currentRelationOpen.CreateTableRelation();
                currentRelationOpen.tuples.Clear();


                for (int i = 0; i < nRow; i++)
                {
                    ProbTuple tuple = new ProbTuple();
                    for (int j = 0; j < nCol; j++)
                    {
                        ProbTriple triple = new ProbTriple(GridViewData.Rows[i].Cells[j].Value.ToString().Trim());
                        tuple.Triples.Add(triple);


                    }
                    currentRelationOpen.tuples.Add(tuple);
                }
                currentRelationOpen.InsertTupleIntoTableRelation();
                MessageBox.Show("Update successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch
            {

            }
        }

        private void GridViewData_SelectionChanged(object sender, EventArgs e)
        {

        }

        // Vào sau khi edit cell xong qua cell khác
        private void GridViewData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
                if (GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    string value = GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();



                    ProbTriple newProbTriple = new ProbTriple(value);

                    if (!newProbTriple.isProbTripleValue(value))
                    {
                        GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Syntax Error! Cannot convert this value to a Probabilistic Triple!";
                        return;
                    }

                    value = newProbTriple.GetStrValue();
                    GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value;


                    if (!currentRelationOpen.Scheme.Attributes[e.ColumnIndex].Type.CheckDataType(value))
                    {
                        GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Attribute value does not match the data type !";
                        return;
                    }




                    ProbTriple probTriple = new ProbTriple(value, currentRelationOpen.Scheme.Attributes[e.ColumnIndex].Type.TypeName);
                    GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = probTriple.GetStrValue();



                    #region check primarykey
                    List<int> indexPrimaryKey = currentRelationOpen.Scheme.ListIndexPrimaryKey();





                    int count = 0;
                    bool flagEditOneCellPrimaryKey = false;


                    foreach (int index in indexPrimaryKey)
                    {
                        ProbTriple triple = new ProbTriple();
                        if (GridViewData.Rows[e.RowIndex].Cells[index].Value != null)
                            triple = new ProbTriple(GridViewData.Rows[e.RowIndex].Cells[index].Value.ToString());

                        if (checkPrimeryKey(index, e.RowIndex, triple))
                        {
                            count++;
                        }
                        if (index == e.ColumnIndex)
                            flagEditOneCellPrimaryKey = true;
                    }


                    //ktr khoang xac xuat
                    if (flagEditOneCellPrimaryKey == true && newProbTriple.Value2.Count != 1)
                    {
                        GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "This object is a primary key it only accepts single value ";
                        return;
                    }
                    //ktr xac xuat duy nhat

                    if (flagEditOneCellPrimaryKey == true && (newProbTriple.MinProb[0] != 1.0 || newProbTriple.MaxProb[0] != 1.0))
                    {
                        GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "This object is a primary key Its minprob and maxprob must be 1";
                        return;

                    }



                    if (count == indexPrimaryKey.Count)
                    {
                        for (int i = 0; i < indexPrimaryKey.Count; i++)
                        {
                            if (indexPrimaryKey[i] == e.ColumnIndex)
                            {
                                GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = " Cannot insert duplicate key in this object ";

                                GridViewData.CurrentCell = GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                return;
                            }
                        }

                        GridViewData.Rows[e.RowIndex].Cells[indexPrimaryKey[indexPrimaryKey.Count - 1]].ErrorText = " Cannot insert duplicate key in this object ";
                        GridViewData.CurrentCell = GridViewData.Rows[e.RowIndex].Cells[indexPrimaryKey[indexPrimaryKey.Count - 1]];
                        return;

                    }

                    #endregion

                }
                else
                {
                    string defaultValue = currentRelationOpen.Scheme.Attributes[e.ColumnIndex].Type.getDefaultValue();
                    GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = defaultValue;

                }
            }
            catch (Exception)
            {


            }

        }

        private bool checkPrimeryKey(int index, int row, ProbTriple triple)
        {

            int nRow = GridViewData.Rows.Count;

            for (int i = 0; i < nRow; i++)
            {
                if (i != row)
                {
                    ProbTriple trip = new ProbTriple();
                    if (GridViewData.Rows[i].Cells[index].Value != null)
                        trip = new ProbTriple(GridViewData.Rows[i].Cells[index].Value.ToString());

                    if (trip.GetStrValue() == triple.GetStrValue())
                        return true;
                }
            }

            return false;
        }

        private void GridViewData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                UpdateDataRowNumber();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Checkbox_UD_CheckedChanged(object sender, EventArgs e)
        {
            if (Checkbox_UD.Checked) SwitchValueState(false);
        }

        private void SwitchValueState(bool state)
        {
            GridViewValue.Visible = state;
            errorProvider.SetError(txtMinProb, null);
            errorProvider.SetError(txtMaxProb, null);

            btn_Value_Home.Enabled = state;
            btn_Value_Pre.Enabled = state;
            btn_Value_Next.Enabled = state;
            btn_Value_End.Enabled = state;
            btn_Value_DeleteRow.Enabled = state;
            btn_Value_AddNewRow.Enabled = state;
            lblValueRowNumberIndicator.Enabled = state;
            Checkbox_UUD.Checked = state;
            Checkbox_UD.Checked = !state;
            label1.Enabled = !state;
            label2.Enabled = !state;
            txtMinProb.Enabled = !state;
            txtMaxProb.Enabled = !state;

            txtValue.Visible = !state;
        }

        private void Checkbox_UUD_CheckedChanged(object sender, EventArgs e)
        {
            if (Checkbox_UUD.Checked) SwitchValueState(true);
        }

        public bool CheckMinProbAndMaxProb()
        {

            errorProvider.SetError(txtMinProb, null);
            errorProvider.SetError(txtMaxProb, null);

            if (txtMinProb.Text == "")
            {
                errorProvider.SetError(txtMinProb, " Sum of MinProb is missing!");
                return false;
            }


            if (txtMaxProb.Text == "")
            {
                errorProvider.SetError(txtMaxProb, " Sum of MaxProb is missing!");
                return false;
            }

            try
            {
                double minPro = double.Parse(txtMinProb.Text.Trim());

            }
            catch (Exception)
            {

                errorProvider.SetError(txtMinProb, "  Sum of MaxProb value must be a real number! ");
                return false;
            }


            try
            {
                double maxPro = double.Parse(txtMaxProb.Text.Trim());

            }
            catch (Exception)
            {

                errorProvider.SetError(txtMaxProb, " Sum of MaxProb value must be a real number!");
                return false;
            }


            if (double.Parse(txtMinProb.Text.Trim()) < 0)
            {
                errorProvider.SetError(txtMinProb, "  Sum of MinProb have to more than 0");
                return false;
            }

            if (double.Parse(txtMinProb.Text.Trim()) > 1)
            {
                errorProvider.SetError(txtMinProb, "  Sum of MinProb have to less than 1");
                return false;
            }

            if (double.Parse(txtMaxProb.Text.Trim()) < 0)
            {
                errorProvider.SetError(txtMaxProb, "  Sum of MaxProb have to more than 0 ");
                return false;
            }


            if (double.Parse(txtMinProb.Text.Trim()) > double.Parse(txtMaxProb.Text.Trim()))
            {
                errorProvider.SetError(txtMinProb, "  Sum of MinProb must less than Sum of MaxProb ");
                return false;
            }


            return true;
        }

        private string Stdize(string S)     // Chuẩn hóa chuỗi cắt bỏ các dấu , dư thừa
        {
            string R = "";
            int i = 0;
            while (S[i] == ',') i++;
            int k = S.Length - 1;
            while (S[k] == ',') k--;
            for (int j = i; j <= k; j++)
                if (S[j] != ',') R += S[j];
                else if (S[j - 1] != ',') R += S[j];
            return R;
        }

        private void btn_Value_UpdateValue_Click(object sender, EventArgs e)
        {
            int UpdateRow, UpdateCell;
            if (Checkbox_UUD.Checked)
            {
                if (GridViewValue.Rows.Count == 0)
                {
                    MessageBox.Show("The value is not entered!");
                    return;
                }



                int n = GridViewValue.Rows.Count;
                ProbTriple triple = new ProbTriple();
                GridViewValue.CurrentCell = GridViewValue.CurrentRow.Cells[0];


                for (int i = 0; i < n; i++)
                {
                    GridViewValue.Rows[i].Cells["ColumnValue"].ErrorText = null;
                    GridViewValue.Rows[i].Cells["ColumnMaxProb"].ErrorText = null;
                    GridViewValue.Rows[i].Cells["ColumnMinProb"].ErrorText = null;

                    if (GridViewValue.Rows[i].Cells["ColumnValue"].Value == null)
                    {
                        GridViewValue.Rows[i].Cells["ColumnValue"].ErrorText = "Required";
                        GridViewValue.CurrentCell = GridViewValue.Rows[i].Cells["ColumnValue"];
                        return;
                    }

                    if (GridViewValue.Rows[i].Cells["ColumnMinProb"].Value == null)
                    {
                        GridViewValue.Rows[i].Cells["ColumnMinProb"].ErrorText = "Required";
                        GridViewValue.CurrentCell = GridViewValue.Rows[i].Cells["ColumnMinProb"];
                        return;
                    }

                    if (GridViewValue.Rows[i].Cells["ColumnMaxProb"].Value == null)
                    {
                        GridViewValue.Rows[i].Cells["ColumnMaxProb"].ErrorText = "Required";
                        GridViewValue.CurrentCell = GridViewValue.Rows[i].Cells["ColumnMaxProb"];
                        return;
                    }


                    try
                    {
                        double value = Convert.ToDouble(GridViewValue.Rows[i].Cells["ColumnMinProb"].Value);
                        if (value < 0.0 || value > 1.0)
                        {
                            GridViewValue.Rows[i].Cells["ColumnMinProb"].ErrorText = "Probabilistic value must belong to [0,1]!";
                            GridViewValue.CurrentCell = GridViewValue.Rows[i].Cells["ColumnMinProb"];
                            return;
                        }

                    }
                    catch (Exception)
                    {
                        GridViewValue.Rows[i].Cells["ColumnMinProb"].ErrorText = "Probabilistic value must be a real number!";
                        GridViewValue.CurrentCell = GridViewValue.Rows[i].Cells["ColumnMinProb"];
                        return;
                    }




                    try
                    {
                        double value = Convert.ToDouble(GridViewValue.Rows[i].Cells["ColumnMaxProb"].Value);
                        if (value < 0.0 || value > 1.0)
                        {
                            GridViewValue.Rows[i].Cells["ColumnMaxProb"].ErrorText = "Probabilistic value must belong to [0,1]!";
                            GridViewValue.CurrentCell = GridViewValue.Rows[i].Cells["ColumnMaxProb"];
                            return;
                        }



                    }
                    catch (Exception)
                    {
                        GridViewValue.Rows[i].Cells["ColumnMaxProb"].ErrorText = "Probabilistic value must be a real number!";
                        GridViewValue.CurrentCell = GridViewValue.Rows[i].Cells["ColumnMaxProb"];

                        return;
                    }

                    if (Convert.ToDouble(GridViewValue.Rows[i].Cells["ColumnMaxProb"].Value) < Convert.ToDouble(GridViewValue.Rows[i].Cells["ColumnMinProb"].Value))
                    {
                        GridViewValue.Rows[i].Cells["ColumnMinProb"].ErrorText = "MinProb must less than MaxProb ";
                        GridViewValue.CurrentCell = GridViewValue.Rows[i].Cells["ColumnMinProb"];

                        return;
                    }



                    triple.MinProb.Add(Convert.ToDouble(GridViewValue.Rows[i].Cells["ColumnMinProb"].Value));
                    triple.MaxProb.Add(Convert.ToDouble(GridViewValue.Rows[i].Cells["ColumnMaxProb"].Value));

                    if (currentRelationOpen.Scheme.Attributes[GridViewData.CurrentCell.ColumnIndex].Type.TypeName != "String")
                    {
                        GridViewValue.Rows[i].Cells["ColumnValue"].Value = GridViewValue.Rows[i].Cells["ColumnValue"].Value.ToString().Trim().Replace(" ", "");

                    }
                    triple.Value2.Add(new ValueOfTriple(GridViewValue.Rows[i].Cells["ColumnValue"].Value.ToString().Trim()));
                }

                UpdateRow = GridViewData.CurrentRow.Index;
                UpdateCell = GridViewData.CurrentCell.ColumnIndex;

                if (UpdateRow == GridViewData.Rows.Count - 1)
                {
                    GridViewData.Rows.Add();
                    UpdateDataRowNumber();
                }


                GridViewData.CurrentCell.ErrorText = null;
                GridViewData.CurrentCell = GridViewData.Rows[UpdateRow].Cells[UpdateCell];



                if (!triple.isProbTripleValue(triple.GetStrValue()))
                {

                    MessageBox.Show("Syntax Error! Cannot convert this value to a Probabilistic Triple!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                GridViewData.CurrentCell.Value = triple.GetStrValue();



            }
            else
            {
                if (!CheckMinProbAndMaxProb())
                    return;

                if (txtValue.Text == "")
                {
                    MessageBox.Show("Attribute values are not entered!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    errorProvider.SetError(txtMinProb, null);
                    errorProvider.SetError(txtMaxProb, null);
                    double minprob, maxprob;
                    string[] value;
                    value = Stdize(txtValue.Text.Trim()).Split(
                                new string[] { Environment.NewLine },
                                StringSplitOptions.None
                            );

                    for (int i = 0; i < value.Length; i++)
                    {
                        value[i] = value[i].Trim();
                    }

                    minprob = Convert.ToDouble(txtMinProb.Text);
                    maxprob = Convert.ToDouble(txtMaxProb.Text);
                    int n = value.Length;

                    if (minprob > 1.0)
                    {
                        //The sum of minProb must be less or equal than 1
                        errorProvider.SetError(txtMinProb, "The sum of minProb must be less or equal than 1");
                        return;
                    }

                    if (maxprob / n > 1.0)
                    {
                        errorProvider.SetError(txtMaxProb, "Upper bound of the value is larger than 1:  " + (maxprob / n));
                        return;
                    }


                    ProbTriple triple = new ProbTriple();
                    for (int i = 0; i < n; i++)
                    {
                        triple.Value2.Add(new ValueOfTriple(value[i]));
                        triple.MinProb.Add(Math.Round((minprob / n), 2));
                        triple.MaxProb.Add(Math.Round((maxprob / n), 2));
                    }

                    UpdateRow = GridViewData.CurrentRow.Index;
                    UpdateCell = GridViewData.CurrentCell.ColumnIndex;

                    if (UpdateRow == GridViewData.Rows.Count - 1)
                    {
                        GridViewData.Rows.Add();
                        UpdateDataRowNumber();
                    }

                    if (!triple.isProbTripleValue(triple.GetStrValue()))
                    {

                        MessageBox.Show("Syntax Error! Cannot convert this value to a Probabilistic Triple!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                    GridViewData.CurrentCell = GridViewData.Rows[UpdateRow].Cells[UpdateCell];
                    GridViewData.CurrentCell.Value = triple.GetStrValue();



                }
                catch
                {
                }
            }
        }

        private void btn_Value_AddNewRow_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewValue.Rows.Add();
                UpdateValueRowNumber();
            }
            catch
            {

            }
        }


        private void UpdateValueRowNumber()
        {
            try
            {
                if (GridViewValue.Rows.Count == 0)
                    lblValueRowNumberIndicator.Text = "0 / 0";
                else if (GridViewValue.CurrentRow != null)
                    lblValueRowNumberIndicator.Text = (GridViewValue.CurrentRow.Index + 1) + " / " + GridViewValue.Rows.Count;
                else lblValueRowNumberIndicator.Text = "1 / " + GridViewValue.Rows.Count;
            }
            catch
            {

            }
        }

        private void btn_Value_Pre_Click(object sender, EventArgs e)
        {
            if (GridViewValue.Rows.Count > 0)
            {
                int PreRow = GridViewValue.CurrentRow.Index - 1;
                PreRow = (PreRow > 0 ? PreRow : 0);
                GridViewValue.CurrentCell = GridViewValue.Rows[PreRow].Cells[0];
                lblValueRowNumberIndicator.Text = (PreRow + 1).ToString() + " / " + GridViewValue.Rows.Count.ToString();
            }
        }

        private void btn_Value_Home_Click(object sender, EventArgs e)
        {
            if (GridViewValue.Rows.Count > 0)
            {
                GridViewValue.CurrentCell = GridViewValue.Rows[0].Cells[0];
                lblValueRowNumberIndicator.Text = "1 / " + GridViewValue.Rows.Count.ToString();
            }
        }

        private void btn_Value_Next_Click(object sender, EventArgs e)
        {
            if (GridViewValue.Rows.Count > 0)
            {
                int nRow = GridViewValue.Rows.Count;
                int NextRow = GridViewValue.CurrentRow.Index + 1;
                NextRow = (NextRow < nRow - 1 ? NextRow : nRow - 1);
                GridViewValue.CurrentCell = GridViewValue.Rows[NextRow].Cells[0];
                lblValueRowNumberIndicator.Text = (NextRow + 1).ToString() + " / " + GridViewValue.Rows.Count.ToString();
            }
        }

        private void btn_Value_End_Click(object sender, EventArgs e)
        {
            if (GridViewValue.Rows.Count > 0)
            {
                int nRow = GridViewValue.Rows.Count;
                GridViewValue.CurrentCell = GridViewValue.Rows[nRow - 1].Cells[0];
                lblValueRowNumberIndicator.Text = nRow.ToString() + " / " + nRow.ToString();
            }
        }

        private void btn_Value_DeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridViewValue.Rows.Count > 0)
                {
                    GridViewValue.Rows.Remove(GridViewValue.CurrentRow);
                    UpdateValueRowNumber();
                }
            }
            catch
            {

            }
        }

        private void Btn_Value_ClearData_Click(object sender, EventArgs e)
        {
            if (Checkbox_UUD.Checked)
            {
                GridViewValue.Rows.Clear();
                UpdateValueRowNumber();
            }
            else
            {
                txtMaxProb.Text = "";
                txtMinProb.Text = "";
                txtValue.Text = "";
            }
        }

        private void GridViewValue_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (GridViewValue.CurrentCell.Value != null)
            {
                if (e.ColumnIndex > 0)  // Giá trị nhập vào các ô MinProb và MaxProb
                {
                    GridViewValue.CurrentCell.ErrorText = null;
                    try
                    {
                        double Prob = Convert.ToDouble(GridViewValue.CurrentCell.Value);

                        if (Prob < 0.0 || Prob > 1.0)
                            GridViewValue.CurrentCell.ErrorText = "Probabilistic value must belong to [0,1]!";




                    }
                    catch
                    {
                        GridViewValue.CurrentCell.ErrorText = "Probabilistic value must be a real number!";

                    }
                }
                else          // Giá trị nhập vào ô Value
                {
                    string StrValue = GridViewValue.CurrentCell.Value.ToString();
                }
            }
        }

        private void GridViewValue_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            UpdateValueRowNumber();
        }

        // Click cell and set data
        private void GridViewData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                GridViewValue.Rows.Clear();
                ProbTriple triple;

                if (GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    triple = new ProbTriple(GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                else
                {
                    triple = new ProbTriple();
                }

                // lỗi đoạn này
                if (Checkbox_UUD.Checked)
                {
                    // Chưa có data nên lấy mặc định từ đây
                    GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = triple.GetStrValue();

                    for (int i = 0; i < triple.Value2.Count; i++)
                    {
                        GridViewValue.Rows.Add();
                        GridViewValue.Rows[i].Cells[0].Value = triple.Value2[i].GetStrValue();
                        GridViewValue.Rows[i].Cells[1].Value = triple.MinProb[i];
                        GridViewValue.Rows[i].Cells[2].Value = triple.MaxProb[i];
                    }


                    UpdateValueRowNumber();
                }
                else
                {
                    txtValue.Text = "";
                    txtMaxProb.Text = "";
                    txtMinProb.Text = "";

                    GridViewData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = triple.GetStrValue();


                    if (triple.UniformDistribution())
                    {

                        double minProb = 0.0;
                        double maxProb = 0.0;

                        for (int i = 0; i < triple.Value2.Count; i++)
                        {
                            txtValue.Text += triple.Value2[i].GetStrValue() + System.Environment.NewLine;
                            minProb += triple.MinProb[i];
                            maxProb += triple.MaxProb[i];

                        }
                        txtMinProb.Text = minProb.ToString();
                        txtMaxProb.Text = maxProb.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion


        #region Query
        private void barButtonItemNewQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CreateNewQuery();
        }

        private void CreateNewQuery()
        {
            try
            {

                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frm_new_query frm = new frm_new_query(this.probDatabase);
                frm.ShowDialog();

                if (frm.queryName != string.Empty)
                {
                    this.probDatabase = frm.probDatabase;
                    LoadQueryNode();
                }


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }


        public void OpenQueryByName(string NameQuery)
        {
            currentQuery = this.probDatabase.Queries.SingleOrDefault(c => c.QueryName == NameQuery);
            xtraTabPageQuery.Text = "Query " + currentQuery.QueryName;
            xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[2];
            ribbonControl1.SelectedPage = ribbonPageQuery;

            txtQuery.Clear();
            txtQuery.Text = currentQuery.QueryString == "Empty" ? "" : currentQuery.QueryString;
            barButtonItemCloseCurrentQuery.Enabled = true;
            barButtonItemSaveQuery.Enabled = true;


            ribbonPageGroupEquality.Visible = true;
            ribbonPageGroupConjuntion.Visible = true;
            ribbonPageGroupDisjunction.Visible = true;
            ribbonPageGroupDifference.Visible = true;

            //
            ribbonPageGroupOperationsOnSet.Visible = true;
            //

            xtraTabPageQuery.PageEnabled = true;
            xtraTabDatabase.SelectedTabPage = xtraTabPageQuery;

            if (txtQuery.Text.Trim().Length <= 0 || currentQuery.QueryString == string.Empty)
            {
                barButtonItemExcuteQuery.Enabled = false;

            }
            else
            {
                barButtonItemExcuteQuery.Enabled = true;

                txtQuery.ForeColor = Color.Black;

            }


        }

        private void barButtonItemOpenQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.probDatabase == null)
                {
                    MessageBox.Show("Error : Cannot find the Database, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.probDatabase.Relations.Count == 0)
                {
                    MessageBox.Show("The first, you must create some query, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                frm_open_query frm = new frm_open_query(this.probDatabase);
                frm.ShowDialog();
                if (frm.QueryName != string.Empty)
                {

                    OpenQueryByName(frm.QueryName);

                }

            }
            catch (Exception)
            {


            }
        }

        private void barButtonItemSaveQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (currentQuery != null)
                    SaveQuery();

            }
            catch (Exception)
            {


            }


        }

        public void SaveQuery()
        {
            currentQuery.QueryString = txtQuery.Text.Trim();
            currentQuery.Update();
            MessageBox.Show("Save successfully !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadQueryNode();
        }



        private void barButtonItemCloseCurrentQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                currentQuery = null;
                xtraTabDatabase.TabPages[2].Text = "Query";
                txtQuery.Text = "";
                barButtonItemExcuteQuery.Enabled = false;
                barButtonItemSaveQuery.Enabled = false;
                barButtonItemCloseCurrentQuery.Enabled = false;

                ribbonPageGroupEquality.Visible = false;
                ribbonPageGroupConjuntion.Visible = false;
                ribbonPageGroupDisjunction.Visible = false;
                ribbonPageGroupDifference.Visible = false;

                //
                ribbonPageGroupOperationsOnSet.Visible = false;
                //

                xtraTabPageQuery.PageEnabled = false;

            }
            catch (Exception)
            {


            }


        }


        #endregion

        private void CTMenuQuery_NewQuery_Click(object sender, EventArgs e)
        {
            CreateNewQuery();
        }

        private void CTMenuQuery_DeleteQueries_Click(object sender, EventArgs e)
        {

            try
            {

                if (this.probDatabase.Queries.Count == 0)
                {
                    MessageBox.Show("You must create some query first !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure want to delete all queries ?", "Delete All Queries", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    NodeQuery.Nodes.Clear();

                    this.probDatabase.Queries.Clear();
                    new ProbQuery().DeleteAllQuery();


                    NodeQuery.ImageIndex = NodeQuery.SelectedImageIndex = Folder_ImgIndex.UnselectedState;
                    currentQuery = null;
                    xtraTabDatabase.TabPages[2].Text = "Query";
                    txtQuery.Text = "";
                    barButtonItemExcuteQuery.Enabled = false;
                    barButtonItemSaveQuery.Enabled = false;
                    barButtonItemCloseCurrentQuery.Enabled = false;

                    ribbonPageGroupEquality.Visible = false;
                    ribbonPageGroupConjuntion.Visible = false;
                    ribbonPageGroupDisjunction.Visible = false;
                    ribbonPageGroupDifference.Visible = false;
                    ribbonPageGroupEquality.Visible = false;
                    xtraTabPageQuery.PageEnabled = false;

                    //
                    ribbonPageGroupOperationsOnSet.Visible = false;
                    //

                    GridViewResult.Rows.Clear();
                    GridViewResult.Columns.Clear();
                    txtQuery.Text = "";

                    barButtonItemExcuteQuery.Enabled = false;

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void barButtonItemDeleteQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.probDatabase.Queries.Count == 0)
                {
                    MessageBox.Show("The first, you must create some query, please try again!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                frm_delete_query frm = new frm_delete_query(this.probDatabase);
                frm.ShowDialog();



                this.probDatabase = frm.probDatabase;

                if (xtraTabDatabase.TabPages[2].Text.Contains(frm.QueryNameRemove))
                {
                    xtraTabDatabase.TabPages[2].Text = "Query";
                    txtQuery.Text = "";
                    GridViewResult.Rows.Clear();
                    GridViewResult.Columns.Clear();

                    barButtonItemExcuteQuery.Enabled = false;
                    barButtonItemSaveQuery.Enabled = false;
                    barButtonItemCloseCurrentQuery.Enabled = false;
                    ribbonPageGroupEquality.Visible = false;
                    ribbonPageGroupConjuntion.Visible = false;
                    ribbonPageGroupDisjunction.Visible = false;
                    ribbonPageGroupEquality.Visible = false;
                    ribbonPageGroupDifference.Visible = false;
                    xtraTabPageQuery.PageEnabled = false;
                    barButtonItemExcuteQuery.Enabled = false;

                    //
                    ribbonPageGroupOperationsOnSet.Visible = false;
                    //
                }


                LoadQueryNode();

            }
            catch (Exception)
            {

            }


        }

        private void CTMenuQueryNode_OpenQuery_Click(object sender, EventArgs e)
        {
            string QueryName = TreeView.SelectedNode.Name;
            OpenQueryByName(QueryName);
        }

        private void CTMenuQuery_DeleteQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.probDatabase.Queries.Count == 0)
                {
                    MessageBox.Show("You must create some query first !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string QueryName = TreeView.SelectedNode.Name;


                frm_delete_query frm = new frm_delete_query(this.probDatabase, QueryName);
                frm.ShowDialog();
                this.probDatabase = frm.probDatabase;

                if (xtraTabDatabase.TabPages[2].Text.Contains(frm.QueryNameRemove))
                {
                    xtraTabDatabase.TabPages[2].Text = "Query";
                    txtQuery.Text = "";
                    barButtonItemCloseCurrentQuery.Enabled = false;
                    ribbonPageGroupConjuntion.Visible = false;
                    ribbonPageGroupDisjunction.Visible = false;
                    ribbonPageGroupDifference.Visible = false;
                    barButtonItemSaveQuery.Enabled = false;

                    //
                    ribbonPageGroupOperationsOnSet.Visible = false;
                    //

                    GridViewResult.Rows.Clear();
                    GridViewResult.Columns.Clear();

                    barButtonItemExcuteQuery.Enabled = false;
                }

                LoadQueryNode();

            }
            catch (Exception)
            {

            }

        }

        private void CTMenuQuery_RenameQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string queryName = TreeView.SelectedNode.Name;
                frm_rename_query frm = new frm_rename_query(this.probDatabase, queryName);
                frm.ShowDialog();


                if (frm.queryName != string.Empty)
                {
                    this.probDatabase = frm.probDatabase;
                    if (xtraTabDatabase.TabPages[2].Text.Contains(frm.QueryNameRename))
                    {
                        xtraTabDatabase.TabPages[2].Text = "Query" + frm.queryName;

                    }


                    LoadQueryNode();
                }

            }
            catch (Exception)
            {
            }
        }

        private void ribbonControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            RibbonControl ribbonControl = (RibbonControl)sender;
            if (ribbonControl.SelectedPage.Name == "ribbonAbout")
            {
                ribbonControl.SelectedPage = ribbonPageDB;
                return;
            }
        }

        // Excute query button
        private void barButtonItemExcuteQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExecuteQuery();
        }

        private void ExecuteQuery()
        {
            Console.WriteLine("ExecuteQuery: " + txtQuery.Text);

            try
            {
                GridViewResult.Rows.Clear();
                GridViewResult.Columns.Clear();

                QueryExecution query = new QueryExecution(this.probDatabase);
                txtMessage.Text = "";

                if (query.ExecuteQuery(txtQuery.Text))
                {
                    txtMessage.Text = string.Empty;

                    if (query.relationResult.tuples.Count <= 0)
                    {
                        txtMessage.Text = "No tuple satisfies the condition";
                        xtraTabControlQueryResult.SelectedTabPageIndex = 1;
                    }
                    else
                    {
                        GridViewResult.Columns.Add("NoNumber", "  Number ");

                        foreach (ProbAttribute attribute in query.selectedAttributes)
                        {
                            GridViewResult.Columns.Add(attribute.AttributeName, attribute.AttributeName);
                            GridViewResult.Columns[attribute.AttributeName].MinimumWidth = 150;
                        }

                        int j, i = -1;
                        foreach (ProbTuple tuple in query.relationResult.tuples)
                        {
                            GridViewResult.Rows.Add();

                            i++; j = 1;
                            GridViewResult.Rows[i].Cells[0].Value = i + 1;

                            foreach (ProbTriple triple in tuple.Triples)
                            {
                                GridViewResult.Rows[i].Cells[j++].Value = triple.GetStrValue();
                            }
                        }

                        xtraTabControlQueryResult.SelectedTabPageIndex = 0;
                    }
                }
                else
                {
                    txtMessage.Text = query.MessageError;
                    xtraTabControlQueryResult.SelectedTabPageIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ClearAll(); // đưa csdl về trạng thái ban đầu
            }
        }




        private void txtQuery_TextChanged_1(object sender, EventArgs e)
        {
            #region old

            if (txtQuery.Text.Trim().Length <= 0)
            {
                barButtonItemExcuteQuery.Enabled = false;
                txtQuery.SelectionColor = Color.Black;
            }
            else
            {
                barButtonItemExcuteQuery.Enabled = true;
                int index = txtQuery.SelectionStart;
                string tmp = txtQuery.Text;
                txtQuery.Clear();
                txtQuery.Text = tmp;

                txtQuery.DeselectAll();
                string tmpString = txtQuery.Text.ToLower();


                int indexSelect = tmpString.IndexOf("select ");
                int indexFrom = tmpString.IndexOf("from ");
                int indexWhere = tmpString.IndexOf("where ");






                string[] array = tmp.Split(new char[] { ' ' });

                for (int i = 0; i < array.Length; i++)
                {
                    if (Operator.ToList().Contains(array[i].ToLower()))
                    {
                        int count = 0;
                        for (int j = 0; j < i; j++)
                        {
                            count += array[j].Length + 1;
                        }

                        txtQuery.Select(count, array[i].Length + 1);
                        txtQuery.SelectionColor = Color.Blue;
                    }
                }





                int indexNaturalJoin = tmp.IndexOf(" natural join in");
                if (tmp.IndexOf(" natural join in") != -1)
                {
                    indexNaturalJoin = tmp.IndexOf(" natural join in");
                    txtQuery.Select(indexNaturalJoin, 17);
                    txtQuery.SelectionColor = Color.Blue;
                }
                else
                    if (tmp.IndexOf(" natural join ig") != -1)
                {

                    indexNaturalJoin = tmp.IndexOf(" natural join ig");
                    txtQuery.Select(indexNaturalJoin, 17);
                    txtQuery.SelectionColor = Color.Blue;

                }
                if (tmp.IndexOf(" natural join me") != -1)
                {
                    indexNaturalJoin = tmp.IndexOf(" natural join me");
                    txtQuery.Select(indexNaturalJoin, 17);
                    txtQuery.SelectionColor = Color.Blue;
                }

                if (tmp.Contains("union") || tmp.Contains("intersect") || tmp.Contains("except"))
                {
                    int indexX;
                    if (tmp.IndexOf("union ") != -1)
                    {
                        indexX = tmp.IndexOf("union ");
                        txtQuery.Select(indexX, 5);
                        txtQuery.SelectionColor = Color.Blue;
                    }

                    if (tmp.IndexOf("union all ") != -1)
                    {
                        indexX = tmp.IndexOf("union all ");
                        txtQuery.Select(indexX, 9);
                        txtQuery.SelectionColor = Color.Blue;
                    }

                    if (tmp.IndexOf("intersect ") != -1)
                    {
                        indexX = tmp.IndexOf("intersect ");
                        txtQuery.Select(indexX, 9);
                        txtQuery.SelectionColor = Color.Blue;
                    }

                    if (tmp.IndexOf("except ") != -1)
                    {
                        indexX = tmp.IndexOf("except ");
                        txtQuery.Select(indexX, 7);
                        txtQuery.SelectionColor = Color.Blue;
                    }

                    if (tmp.LastIndexOf("select ") != -1)
                    {
                        indexX = tmp.LastIndexOf("select ");
                        txtQuery.Select(indexX, 7);
                        txtQuery.SelectionColor = Color.Blue;
                    }

                    if (tmp.LastIndexOf("from ") != -1)
                    {
                        indexX = tmp.LastIndexOf("from ");
                        txtQuery.Select(indexX, 5);
                        txtQuery.SelectionColor = Color.Blue;
                    }

                    if (tmp.LastIndexOf("where ") != -1)
                    {
                        indexX = tmp.LastIndexOf("where ");
                        txtQuery.Select(indexX, 6);
                        txtQuery.SelectionColor = Color.Blue;
                    }
                }
                
                if (indexSelect != -1)
                {
                    if (indexSelect != 0)
                    {
                        string t = txtQuery.Text.Substring(indexSelect - 1, 1).Trim();
                        if (t.Length == 0)
                        {
                            txtQuery.Select(indexSelect, 6);
                            txtQuery.SelectionColor = Color.Blue;
                        }
                    }
                    else
                    {

                        txtQuery.Select(indexSelect, 6);
                        txtQuery.SelectionColor = Color.Blue;
                    }
                }


                if (indexFrom != -1)
                {

                    if (indexFrom != 0)
                    {
                        string t = txtQuery.Text.Substring(indexFrom - 1, 1).Trim();
                        if (t.Length == 0)
                        {
                            txtQuery.Select(indexFrom, 5);
                            txtQuery.SelectionColor = Color.Blue;
                        }
                    }
                    else
                    {

                        txtQuery.Select(indexFrom, 5);
                        txtQuery.SelectionColor = Color.Blue;
                    }


                }



                if (indexWhere != -1)
                {

                    if (indexWhere != 0)
                    {
                        string t = txtQuery.Text.Substring(indexWhere - 1, 1).Trim();
                        if (t.Length == 0)
                        {
                            txtQuery.Select(indexWhere, 6);
                            txtQuery.SelectionColor = Color.Blue;
                        }
                    }
                    else
                    {
                        txtQuery.Select(indexWhere, 6);
                        txtQuery.SelectionColor = Color.Blue;

                    }
                }

                if (tmp.Contains("where "))
                {
                    string tmpWhere = tmp.Substring(tmp.IndexOf("where") + 6);
                    for (int i = 0; i < tmpWhere.Length - 1;)
                    {
                        int j = i + 1;
                        if (tmpWhere[i] == '\'')
                        {

                            for (int k = j; k < tmpWhere.Length; k++)
                            {
                                if (tmpWhere[k] == '\'')
                                {
                                    j = k + 1;
                                    txtQuery.Select(i + tmp.IndexOf("where") + 6, j - i);
                                    txtQuery.SelectionColor = Color.Red;
                                    break;
                                }
                                else
                                    if (tmpWhere[k] != '\'' && k == tmpWhere.Length - 1)
                                {
                                    txtQuery.Select(i + tmp.IndexOf("where") + 6, k - i + 1);
                                    txtQuery.SelectionColor = Color.Red;
                                }
                            }
                        }

                        if (tmpWhere[i] == '{')
                        {

                            for (int k = j; k < tmpWhere.Length; k++)
                            {
                                if (tmpWhere[k] == '}')
                                {
                                    j = k + 1;
                                    txtQuery.Select(i + tmp.IndexOf("where") + 6, j - i);
                                    txtQuery.SelectionColor = Color.Red;
                                    break;
                                }
                                else
                                    if (tmpWhere[k] != '}' && k == tmpWhere.Length - 1)
                                {
                                    txtQuery.Select(i + tmp.IndexOf("where") + 6, k - i + 1);
                                    txtQuery.SelectionColor = Color.Red;
                                }
                            }
                        }

                        i = j;
                    }
                }


                txtQuery.Select(index, 1);
                txtQuery.SelectionLength = 0;
                txtQuery.SelectionStart = index;


            }
            #endregion
        }

        private void barButtonItem_Hoi_Ignor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {




            if (txtQuery.Text == "") txtQuery.Text = @" ⊗_ig ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" ⊗_ig ");
                txtQuery.SelectionStart = index + 6;
            }


        }

        private void barButtonItem_Hoi_independence_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" ⊗_in ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" ⊗_in ");
                txtQuery.SelectionStart = index + 6;
            }

        }

        private void barButtonItem_Hoi_mutualexclusion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" ⊗_me ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" ⊗_me ");
                txtQuery.SelectionStart = index + 6;
            }



        }

        private void barButtonItem_Tuyen_ignorance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" ⊕_ig ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" ⊕_ig ");
                txtQuery.SelectionStart = index + 6;
            }
        }

        private void barButtonItem_Tuyen_independence_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" ⊕_in ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" ⊕_in ");
                txtQuery.SelectionStart = index + 6;
            }

        }

        private void barButtonItem_Tuyen_mutualexclusion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" ⊕_me ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" ⊕_me ");
                txtQuery.SelectionStart = index + 6;
            }


        }

        private void barButtonItem_Tru_ignorance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" ⊖_ig ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" ⊖_ig ");
                txtQuery.SelectionStart = index + 6;
            }

        }

        private void barButtonItem_Tru_independence_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" ⊖_in ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" ⊖_in ");
                txtQuery.SelectionStart = index + 6;
            }

        }

        private void barButtonItem_Tru_mutualexclusion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" ⊖_me ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" ⊖_me ");
                txtQuery.SelectionStart = index + 6;
            }

        }




        private void barButtonItem_Bang_ignorance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (txtQuery.Text == "") txtQuery.Text = @" EQUAL_ig ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" EQUAL_ig ");
                txtQuery.SelectionStart = index + 10;
            }

        }

        private void barButtonItem_Bang_independence_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" EQUAL_in ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" EQUAL_in ");
                txtQuery.SelectionStart = index + 10;
            }
        }

        private void barButtonItem_Bang_mutualexclusion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtQuery.Text == "") txtQuery.Text = @" EQUAL_me ";

            else
            {

                int index = txtQuery.SelectionStart;
                txtQuery.Text = txtQuery.Text.Insert(index, @" EQUAL_me ");
                txtQuery.SelectionStart = index + 10;
            }
        }

        //
        private void barButtonLargerOperationsItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonSmallerOperationsItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonDifferentOperationsItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        //


        private void ClearAll()
        {

            foreach (ProbRelation relation in this.probDatabase.Relations)
            {
                relation.ListRenameRelation.Clear();
                relation.RenameRelationName = string.Empty;

                foreach (ProbAttribute attr in relation.Scheme.Attributes)
                {
                    if (attr.AttributeName.Contains("."))
                    {
                        attr.AttributeName = attr.AttributeName.Substring(attr.AttributeName.IndexOf(".") + 1);
                    }
                }
            }



        }

        private void barButtonItemEditRelation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void GridViewData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {




        }

        private void BindingNavigatorValue_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtMinProb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMinProb.Text.Length <= 0)
                    return;
                double minprob = double.Parse(txtMinProb.Text);
                errorProvider.SetError(txtMinProb, null);
                if (minprob > 1.0)
                {
                    // The sum of minProb must be less or equal than 1
                    errorProvider.SetError(txtMinProb, "The sum of minProb must be less or equal than 1");
                    return;
                }
            }
            catch (Exception)
            {
                errorProvider.SetError(txtMinProb, "Sum of minProb value must be a real number!");

            }
        }

        private void txtMaxProb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMaxProb.Text.Length <= 0)
                    return;

                double maxprob = double.Parse(txtMaxProb.Text);

                errorProvider.SetError(txtMaxProb, null);
            }
            catch (Exception)
            {
                // errorProvider.SetError(txtMinProb, "The sum of minProb must be less or equal than 1");                   
                errorProvider.SetError(txtMaxProb, "Sum of MaxProb value must be a real number!");
            }
        }
    }
}