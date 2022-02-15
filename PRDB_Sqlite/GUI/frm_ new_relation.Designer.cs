namespace PRDB_Sqlite.GUI
{
    partial class frm__new_relation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm__new_relation));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtRelationName = new DevExpress.XtraEditors.TextEdit();
            this.cbo_SchemeName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtRelationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_SchemeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 22F);
            this.labelControl1.Location = new System.Drawing.Point(114, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(260, 35);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Create New Relation";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(23, 73);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Relation Name :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(23, 122);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(75, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Select schema :";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(237, 154);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtRelationName
            // 
            this.txtRelationName.Location = new System.Drawing.Point(114, 66);
            this.txtRelationName.Name = "txtRelationName";
            this.txtRelationName.Properties.Mask.BeepOnError = true;
            this.txtRelationName.Properties.Mask.EditMask = "[a-zA-Z][a-zA-Z0-9]+";
            this.txtRelationName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtRelationName.Properties.Mask.ShowPlaceHolders = false;
            this.txtRelationName.Size = new System.Drawing.Size(288, 20);
            this.txtRelationName.TabIndex = 0;
            // 
            // cbo_SchemeName
            // 
            this.cbo_SchemeName.Location = new System.Drawing.Point(114, 115);
            this.cbo_SchemeName.Name = "cbo_SchemeName";
            this.cbo_SchemeName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_SchemeName.Size = new System.Drawing.Size(288, 20);
            this.cbo_SchemeName.TabIndex = 1;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // frm__new_relation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 197);
            this.Controls.Add(this.cbo_SchemeName);
            this.Controls.Add(this.txtRelationName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(453, 235);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(453, 235);
            this.Name = "frm__new_relation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create New Relation";
            this.Load += new System.EventHandler(this.frm__new_relation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtRelationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_SchemeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtRelationName;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_SchemeName;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}