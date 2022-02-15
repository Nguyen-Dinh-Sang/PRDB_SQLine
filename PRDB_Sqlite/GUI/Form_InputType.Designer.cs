namespace PRDB_Sqlite.GUI
{
    partial class Form_InputType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_InputType));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblTypeName = new DevExpress.XtraEditors.LabelControl();
            this.lblListValueType = new DevExpress.XtraEditors.LabelControl();
            this.txtUserDefined = new DevExpress.XtraEditors.TextEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtListValue = new DevExpress.XtraEditors.MemoEdit();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ComboBox_DataType = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserDefined.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtListValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBox_DataType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(79, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(244, 35);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Select Data Type";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(23, 101);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Data Type :";
            // 
            // lblTypeName
            // 
            this.lblTypeName.Enabled = false;
            this.lblTypeName.Location = new System.Drawing.Point(23, 146);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(61, 13);
            this.lblTypeName.TabIndex = 2;
            this.lblTypeName.Text = "Type Name :";
            // 
            // lblListValueType
            // 
            this.lblListValueType.Enabled = false;
            this.lblListValueType.Location = new System.Drawing.Point(23, 191);
            this.lblListValueType.Name = "lblListValueType";
            this.lblListValueType.Size = new System.Drawing.Size(92, 13);
            this.lblListValueType.TabIndex = 3;
            this.lblListValueType.Text = "List of Value Type :";
            // 
            // txtUserDefined
            // 
            this.txtUserDefined.Enabled = false;
            this.txtUserDefined.Location = new System.Drawing.Point(122, 139);
            this.txtUserDefined.Name = "txtUserDefined";
            this.txtUserDefined.Properties.Mask.BeepOnError = true;
            this.txtUserDefined.Properties.Mask.EditMask = "[a-zA-Z0-9]+";
            this.txtUserDefined.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtUserDefined.Properties.Mask.ShowPlaceHolders = false;
            this.txtUserDefined.Size = new System.Drawing.Size(265, 20);
            this.txtUserDefined.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(213, 346);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(312, 346);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtListValue
            // 
            this.txtListValue.Enabled = false;
            this.txtListValue.Location = new System.Drawing.Point(23, 220);
            this.txtListValue.Name = "txtListValue";
            this.txtListValue.Size = new System.Drawing.Size(364, 109);
            this.txtListValue.TabIndex = 2;
            this.txtListValue.TextChanged += new System.EventHandler(this.txtListValue_TextChanged_1);
            this.txtListValue.Leave += new System.EventHandler(this.txtListValue_Leave);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ComboBox_DataType
            // 
            this.ComboBox_DataType.Location = new System.Drawing.Point(122, 94);
            this.ComboBox_DataType.Name = "ComboBox_DataType";
            this.ComboBox_DataType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ComboBox_DataType.Properties.Items.AddRange(new object[] {
            "Int16",
            "Int32",
            "Int64",
            "Byte",
            "String",
            "Single",
            "Double",
            "Boolean",
            "Decimal",
            "DateTime",
            "Binary",
            "Currency",
            "UserDefined"});
            this.ComboBox_DataType.Size = new System.Drawing.Size(265, 20);
            this.ComboBox_DataType.TabIndex = 0;
            this.ComboBox_DataType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_DataType_SelectedIndexChanged);
            // 
            // Form_InputType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 381);
            this.Controls.Add(this.ComboBox_DataType);
            this.Controls.Add(this.txtListValue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtUserDefined);
            this.Controls.Add(this.lblListValueType);
            this.Controls.Add(this.lblTypeName);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(430, 419);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(430, 419);
            this.Name = "Form_InputType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Data Type...";
            this.Load += new System.EventHandler(this.Form_InputType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtUserDefined.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtListValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBox_DataType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblTypeName;
        private DevExpress.XtraEditors.LabelControl lblListValueType;
        private DevExpress.XtraEditors.TextEdit txtUserDefined;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.MemoEdit txtListValue;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private DevExpress.XtraEditors.ComboBoxEdit ComboBox_DataType;
    }
}