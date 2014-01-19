namespace TileEditor.Forms
{
    partial class NewTileLayerForm
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
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label_name = new System.Windows.Forms.Label();
            this.label_width = new System.Windows.Forms.Label();
            this.label_height = new System.Windows.Forms.Label();
            this.textbox_name = new System.Windows.Forms.TextBox();
            this.textbox_width = new System.Windows.Forms.TextBox();
            this.textbox_height = new System.Windows.Forms.TextBox();
            this.combobox_layerTypes = new System.Windows.Forms.ComboBox();
            this.label_layerType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(192, 167);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(82, 23);
            this.button_ok.TabIndex = 3;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(280, 167);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(77, 23);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(53, 32);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(35, 13);
            this.label_name.TabIndex = 2;
            this.label_name.Text = "Name";
            // 
            // label_width
            // 
            this.label_width.AutoSize = true;
            this.label_width.Location = new System.Drawing.Point(53, 62);
            this.label_width.Name = "label_width";
            this.label_width.Size = new System.Drawing.Size(35, 13);
            this.label_width.TabIndex = 3;
            this.label_width.Text = "Width";
            // 
            // label_height
            // 
            this.label_height.AutoSize = true;
            this.label_height.Location = new System.Drawing.Point(53, 89);
            this.label_height.Name = "label_height";
            this.label_height.Size = new System.Drawing.Size(38, 13);
            this.label_height.TabIndex = 4;
            this.label_height.Text = "Height";
            // 
            // textbox_name
            // 
            this.textbox_name.Location = new System.Drawing.Point(94, 32);
            this.textbox_name.Name = "textbox_name";
            this.textbox_name.Size = new System.Drawing.Size(223, 20);
            this.textbox_name.TabIndex = 0;
            // 
            // textbox_width
            // 
            this.textbox_width.Location = new System.Drawing.Point(94, 62);
            this.textbox_width.Name = "textbox_width";
            this.textbox_width.Size = new System.Drawing.Size(100, 20);
            this.textbox_width.TabIndex = 1;
            // 
            // textbox_height
            // 
            this.textbox_height.Location = new System.Drawing.Point(94, 89);
            this.textbox_height.Name = "textbox_height";
            this.textbox_height.Size = new System.Drawing.Size(100, 20);
            this.textbox_height.TabIndex = 2;
            // 
            // combobox_layerTypes
            // 
            this.combobox_layerTypes.FormattingEnabled = true;
            this.combobox_layerTypes.Location = new System.Drawing.Point(94, 115);
            this.combobox_layerTypes.Name = "combobox_layerTypes";
            this.combobox_layerTypes.Size = new System.Drawing.Size(121, 21);
            this.combobox_layerTypes.TabIndex = 5;
            // 
            // label_layerType
            // 
            this.label_layerType.AutoSize = true;
            this.label_layerType.Location = new System.Drawing.Point(31, 118);
            this.label_layerType.Name = "label_layerType";
            this.label_layerType.Size = new System.Drawing.Size(60, 13);
            this.label_layerType.TabIndex = 6;
            this.label_layerType.Text = "Layer Type";
            // 
            // NewTileLayerForm
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(369, 202);
            this.Controls.Add(this.label_layerType);
            this.Controls.Add(this.combobox_layerTypes);
            this.Controls.Add(this.textbox_height);
            this.Controls.Add(this.textbox_width);
            this.Controls.Add(this.textbox_name);
            this.Controls.Add(this.label_height);
            this.Controls.Add(this.label_width);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Name = "NewTileLayerForm";
            this.Text = "Add New Layer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_width;
        private System.Windows.Forms.Label label_height;
        public System.Windows.Forms.TextBox textbox_name;
        public System.Windows.Forms.TextBox textbox_width;
        public System.Windows.Forms.TextBox textbox_height;
        private System.Windows.Forms.Label label_layerType;
        public System.Windows.Forms.ComboBox combobox_layerTypes;
    }
}