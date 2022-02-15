namespace PRDB_Sqlite.GUI
{
    partial class frm_rename_relation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_rename_relation));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cbo_relation_name = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtNewNameRelation = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cbo_relation_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewNameRelation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Select a relation :";
            // 
            // cbo_relation_name
            // 
            this.cbo_relation_name.Location = new System.Drawing.Point(131, 19);
            this.cbo_relation_name.Name = "cbo_relation_name";
            this.cbo_relation_name.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_relation_name.Size = new System.Drawing.Size(271, 20);
            this.cbo_relation_name.TabIndex = 1;
            this.cbo_relation_name.SelectedIndexChanged += new System.EventHandler(this.cbo_relation_name_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(28, 64);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Enter new name :";
            // 
            // txtNewNameRelation
            // 
            this.txtNewNameRelation.Location = new System.Drawing.Point(131, 56);
            this.txtNewNameRelation.Name = "txtNewNameRelation";
            this.txtNewNameRelation.Properties.Mask.BeepOnError = true;
            this.txtNewNameRelation.Properties.Mask.EditMask = "[a-zA-Z][a-zA-Z0-9]+";
            this.txtNewNameRelation.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNewNameRelation.Properties.Mask.ShowPlaceHolders = false;
            this.txtNewNameRelation.Size = new System.Drawing.Size(271, 20);
            this.txtNewNameRelation.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(233, 85);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // frm_rename_relation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 120);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNewNameRelation);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cbo_relation_name);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(430, 158);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(430, 158);
            this.Name = "frm_rename_relation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rename Relation";
            this.Load += new System.EventHandler(this.frm_rename_relation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbo_relation_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewNameRelation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_relation_name;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtNewNameRelation;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}