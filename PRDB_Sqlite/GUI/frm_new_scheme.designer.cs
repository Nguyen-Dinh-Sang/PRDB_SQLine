namespace PRDB_Sqlite.GUI
{
    partial class frm_new_scheme
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_new_scheme));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSchemeName = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.BindingNavigatorDesign = new System.Windows.Forms.ToolStrip();
            this.btn_Design_Home = new System.Windows.Forms.ToolStripButton();
            this.btn_Design_Pre = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.lblDesignRowNumberIndicator = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_Design_Next = new System.Windows.Forms.ToolStripButton();
            this.btn_Design_End = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_Design_DeleteRow = new System.Windows.Forms.ToolStripButton();
            this.Btn_Design_ClearData = new System.Windows.Forms.ToolStripButton();
            this.Btn_Design_UpdateData = new System.Windows.Forms.ToolStripButton();
            this.GridViewDesign = new System.Windows.Forms.DataGridView();
            this.PrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValueType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txtSchemeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.BindingNavigatorDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDesign)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(38, 47);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Schema Name:";
            // 
            // txtSchemeName
            // 
            this.txtSchemeName.Location = new System.Drawing.Point(137, 40);
            this.txtSchemeName.Margin = new System.Windows.Forms.Padding(8);
            this.txtSchemeName.Name = "txtSchemeName";
            this.txtSchemeName.Properties.Mask.BeepOnError = true;
            this.txtSchemeName.Properties.Mask.EditMask = "[a-zA-Z0-9]+";
            this.txtSchemeName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtSchemeName.Properties.Mask.ShowPlaceHolders = false;
            this.txtSchemeName.Size = new System.Drawing.Size(335, 20);
            this.txtSchemeName.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(558, 488);
            this.btnSave.Margin = new System.Windows.Forms.Padding(21, 22, 21, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 37);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(656, 488);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(21, 22, 21, 22);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 37);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(180, 30);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(21, 22, 21, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(406, 48);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Create New Schema";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSchemeName);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.groupBox1.Location = new System.Drawing.Point(17, 108);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(748, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Schema Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelControl1);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.groupBox2.Location = new System.Drawing.Point(17, 211);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox2.Size = new System.Drawing.Size(748, 272);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Schema Attribute";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.BindingNavigatorDesign);
            this.panelControl1.Controls.Add(this.GridViewDesign);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(8, 22);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(732, 242);
            this.panelControl1.TabIndex = 0;
            // 
            // BindingNavigatorDesign
            // 
            this.BindingNavigatorDesign.BackColor = System.Drawing.Color.Gainsboro;
            this.BindingNavigatorDesign.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BindingNavigatorDesign.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.BindingNavigatorDesign.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Design_Home,
            this.btn_Design_Pre,
            this.toolStripSeparator5,
            this.lblDesignRowNumberIndicator,
            this.toolStripSeparator6,
            this.btn_Design_Next,
            this.btn_Design_End,
            this.toolStripSeparator18,
            this.Btn_Design_DeleteRow,
            this.Btn_Design_ClearData,
            this.Btn_Design_UpdateData});
            this.BindingNavigatorDesign.Location = new System.Drawing.Point(2, 214);
            this.BindingNavigatorDesign.Name = "BindingNavigatorDesign";
            this.BindingNavigatorDesign.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.BindingNavigatorDesign.Size = new System.Drawing.Size(728, 26);
            this.BindingNavigatorDesign.TabIndex = 6;
            this.BindingNavigatorDesign.Text = "toolStrip1";
            // 
            // btn_Design_Home
            // 
            this.btn_Design_Home.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Design_Home.Image = ((System.Drawing.Image)(resources.GetObject("btn_Design_Home.Image")));
            this.btn_Design_Home.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btn_Design_Home.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Design_Home.Name = "btn_Design_Home";
            this.btn_Design_Home.Size = new System.Drawing.Size(23, 23);
            this.btn_Design_Home.Text = "toolStripButton1";
            this.btn_Design_Home.ToolTipText = "Get Home";
            this.btn_Design_Home.Click += new System.EventHandler(this.btn_Design_Home_Click);
            // 
            // btn_Design_Pre
            // 
            this.btn_Design_Pre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Design_Pre.Image = ((System.Drawing.Image)(resources.GetObject("btn_Design_Pre.Image")));
            this.btn_Design_Pre.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btn_Design_Pre.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Design_Pre.Name = "btn_Design_Pre";
            this.btn_Design_Pre.Size = new System.Drawing.Size(23, 23);
            this.btn_Design_Pre.Text = "toolStripButton2";
            this.btn_Design_Pre.ToolTipText = "Previous Row";
            this.btn_Design_Pre.Click += new System.EventHandler(this.btn_Design_Pre_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
            // 
            // lblDesignRowNumberIndicator
            // 
            this.lblDesignRowNumberIndicator.Name = "lblDesignRowNumberIndicator";
            this.lblDesignRowNumberIndicator.Size = new System.Drawing.Size(30, 23);
            this.lblDesignRowNumberIndicator.Text = "0 / 0";
            this.lblDesignRowNumberIndicator.ToolTipText = "Row Number";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 26);
            // 
            // btn_Design_Next
            // 
            this.btn_Design_Next.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Design_Next.Image = ((System.Drawing.Image)(resources.GetObject("btn_Design_Next.Image")));
            this.btn_Design_Next.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btn_Design_Next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Design_Next.Name = "btn_Design_Next";
            this.btn_Design_Next.Size = new System.Drawing.Size(23, 23);
            this.btn_Design_Next.Text = "toolStripButton3";
            this.btn_Design_Next.ToolTipText = "Next Row";
            this.btn_Design_Next.Click += new System.EventHandler(this.btn_Design_Next_Click);
            // 
            // btn_Design_End
            // 
            this.btn_Design_End.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Design_End.Image = ((System.Drawing.Image)(resources.GetObject("btn_Design_End.Image")));
            this.btn_Design_End.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btn_Design_End.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Design_End.Name = "btn_Design_End";
            this.btn_Design_End.Size = new System.Drawing.Size(23, 23);
            this.btn_Design_End.Text = "toolStripButton4";
            this.btn_Design_End.ToolTipText = "Reach End";
            this.btn_Design_End.Click += new System.EventHandler(this.btn_Design_End_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 26);
            // 
            // Btn_Design_DeleteRow
            // 
            this.Btn_Design_DeleteRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Design_DeleteRow.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Design_DeleteRow.Image")));
            this.Btn_Design_DeleteRow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Btn_Design_DeleteRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Design_DeleteRow.Name = "Btn_Design_DeleteRow";
            this.Btn_Design_DeleteRow.Size = new System.Drawing.Size(23, 23);
            this.Btn_Design_DeleteRow.Text = "Delete This Row";
            this.Btn_Design_DeleteRow.Click += new System.EventHandler(this.Btn_Design_DeleteRow_Click);
            // 
            // Btn_Design_ClearData
            // 
            this.Btn_Design_ClearData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Design_ClearData.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Design_ClearData.Image")));
            this.Btn_Design_ClearData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Design_ClearData.Name = "Btn_Design_ClearData";
            this.Btn_Design_ClearData.Size = new System.Drawing.Size(23, 23);
            this.Btn_Design_ClearData.Text = "Reset Scheme";
            this.Btn_Design_ClearData.Click += new System.EventHandler(this.Btn_Design_ClearData_Click);
            // 
            // Btn_Design_UpdateData
            // 
            this.Btn_Design_UpdateData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Design_UpdateData.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Design_UpdateData.Image")));
            this.Btn_Design_UpdateData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Design_UpdateData.Name = "Btn_Design_UpdateData";
            this.Btn_Design_UpdateData.Size = new System.Drawing.Size(23, 23);
            this.Btn_Design_UpdateData.Text = "Save Scheme";
            this.Btn_Design_UpdateData.ToolTipText = "Save Scheme";
            this.Btn_Design_UpdateData.Visible = false;
            // 
            // GridViewDesign
            // 
            this.GridViewDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridViewDesign.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewDesign.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridViewDesign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewDesign.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PrimaryKey,
            this.ColumnName,
            this.ColumnType,
            this.ColumnValueType,
            this.ColumnDescription});
            this.GridViewDesign.GridColor = System.Drawing.Color.Black;
            this.GridViewDesign.Location = new System.Drawing.Point(0, 2);
            this.GridViewDesign.Margin = new System.Windows.Forms.Padding(56, 59, 56, 59);
            this.GridViewDesign.Name = "GridViewDesign";
            this.GridViewDesign.Size = new System.Drawing.Size(730, 211);
            this.GridViewDesign.TabIndex = 3;
            this.GridViewDesign.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewDesign_CellClick);
            this.GridViewDesign.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewDesign_CellEndEdit);
            this.GridViewDesign.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.GridViewDesign_RowsAdded);
            this.GridViewDesign.Click += new System.EventHandler(this.GridViewDesign_Click);
            // 
            // PrimaryKey
            // 
            this.PrimaryKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PrimaryKey.HeaderText = "Primary Key";
            this.PrimaryKey.Name = "PrimaryKey";
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnName.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnName.HeaderText = "Attribute Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnName.ToolTipText = "Enter attribute name";
            // 
            // ColumnType
            // 
            this.ColumnType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            this.ColumnType.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnType.HeaderText = "Data Type";
            this.ColumnType.Name = "ColumnType";
            this.ColumnType.ReadOnly = true;
            this.ColumnType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnType.ToolTipText = "Enter attribute data type";
            // 
            // ColumnValueType
            // 
            this.ColumnValueType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnValueType.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnValueType.HeaderText = "Domain";
            this.ColumnValueType.Name = "ColumnValueType";
            this.ColumnValueType.ReadOnly = true;
            this.ColumnValueType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnValueType.ToolTipText = "Enter attribute value type";
            // 
            // ColumnDescription
            // 
            this.ColumnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnDescription.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnDescription.HeaderText = "Description";
            this.ColumnDescription.Name = "ColumnDescription";
            this.ColumnDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnDescription.ToolTipText = "Decribe attribute";
            // 
            // frm_new_scheme
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 22F);
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 529);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(21, 22, 21, 22);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_new_scheme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create New Schema";
            this.Load += new System.EventHandler(this.frm_new_scheme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSchemeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.BindingNavigatorDesign.ResumeLayout(false);
            this.BindingNavigatorDesign.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDesign)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSchemeName;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip BindingNavigatorDesign;
        private System.Windows.Forms.ToolStripButton btn_Design_Home;
        private System.Windows.Forms.ToolStripButton btn_Design_Pre;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel lblDesignRowNumberIndicator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btn_Design_Next;
        private System.Windows.Forms.ToolStripButton btn_Design_End;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripButton Btn_Design_DeleteRow;
        private System.Windows.Forms.ToolStripButton Btn_Design_ClearData;
        private System.Windows.Forms.ToolStripButton Btn_Design_UpdateData;
        private System.Windows.Forms.DataGridView GridViewDesign;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PrimaryKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValueType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
    }
}