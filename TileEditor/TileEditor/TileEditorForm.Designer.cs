namespace TileEditor
{
    partial class TileEditorForm
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
            this.menustrip_editorMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkbox_includeOutlining = new System.Windows.Forms.CheckBox();
            this.openFileBrowserControl = new System.Windows.Forms.OpenFileDialog();
            this.saveFileBrowserControl = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserControl = new System.Windows.Forms.FolderBrowserDialog();
            this.scrollbar_hDisplay = new System.Windows.Forms.HScrollBar();
            this.scrollbar_vDisplay = new System.Windows.Forms.VScrollBar();
            this.textbox_contentPath = new System.Windows.Forms.TextBox();
            this.listbox_tileLayers = new System.Windows.Forms.ListBox();
            this.listbox_textures = new System.Windows.Forms.ListBox();
            this.button_addNewLayer = new System.Windows.Forms.Button();
            this.button_removeLayer = new System.Windows.Forms.Button();
            this.button_addTexture = new System.Windows.Forms.Button();
            this.button_removeTexture = new System.Windows.Forms.Button();
            this.label_tileLayerFiles = new System.Windows.Forms.Label();
            this.label_textureFiles = new System.Windows.Forms.Label();
            this.groupBox_EditTools = new System.Windows.Forms.GroupBox();
            this.label_alphaChannel = new System.Windows.Forms.Label();
            this.trackbar_alphaChannel = new System.Windows.Forms.TrackBar();
            this.checkbox_fill = new System.Windows.Forms.CheckBox();
            this.radiobutton_erase = new System.Windows.Forms.RadioButton();
            this.radiobutton_draw = new System.Windows.Forms.RadioButton();
            this.picturebox_TexturePreview = new System.Windows.Forms.PictureBox();
            this.label_contentRoot = new System.Windows.Forms.Label();
            this.button_addExistingLayer = new System.Windows.Forms.Button();
            this.textbox_layerType = new System.Windows.Forms.TextBox();
            this.MapGraphicsEditor = new TileEditor.Controls.GraphicsEditor();
            this.label_layerType = new System.Windows.Forms.Label();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menustrip_editorMenu.SuspendLayout();
            this.groupBox_EditTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar_alphaChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_TexturePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // menustrip_editorMenu
            // 
            this.menustrip_editorMenu.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menustrip_editorMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menustrip_editorMenu.Location = new System.Drawing.Point(0, 0);
            this.menustrip_editorMenu.Name = "menustrip_editorMenu";
            this.menustrip_editorMenu.Size = new System.Drawing.Size(987, 24);
            this.menustrip_editorMenu.TabIndex = 1;
            this.menustrip_editorMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // checkbox_includeOutlining
            // 
            this.checkbox_includeOutlining.AutoSize = true;
            this.checkbox_includeOutlining.Checked = true;
            this.checkbox_includeOutlining.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_includeOutlining.Location = new System.Drawing.Point(513, 647);
            this.checkbox_includeOutlining.Name = "checkbox_includeOutlining";
            this.checkbox_includeOutlining.Size = new System.Drawing.Size(149, 17);
            this.checkbox_includeOutlining.TabIndex = 2;
            this.checkbox_includeOutlining.Text = "Include Empty Tile Outling";
            this.checkbox_includeOutlining.UseVisualStyleBackColor = true;
            // 
            // scrollbar_hDisplay
            // 
            this.scrollbar_hDisplay.Location = new System.Drawing.Point(12, 617);
            this.scrollbar_hDisplay.Name = "scrollbar_hDisplay";
            this.scrollbar_hDisplay.Size = new System.Drawing.Size(650, 17);
            this.scrollbar_hDisplay.TabIndex = 3;
            // 
            // scrollbar_vDisplay
            // 
            this.scrollbar_vDisplay.LargeChange = 9;
            this.scrollbar_vDisplay.Location = new System.Drawing.Point(665, 27);
            this.scrollbar_vDisplay.Name = "scrollbar_vDisplay";
            this.scrollbar_vDisplay.Size = new System.Drawing.Size(17, 587);
            this.scrollbar_vDisplay.TabIndex = 4;
            // 
            // textbox_contentPath
            // 
            this.textbox_contentPath.Location = new System.Drawing.Point(769, 28);
            this.textbox_contentPath.Name = "textbox_contentPath";
            this.textbox_contentPath.ReadOnly = true;
            this.textbox_contentPath.Size = new System.Drawing.Size(208, 20);
            this.textbox_contentPath.TabIndex = 5;
            // 
            // listbox_tileLayers
            // 
            this.listbox_tileLayers.FormattingEnabled = true;
            this.listbox_tileLayers.Location = new System.Drawing.Point(685, 82);
            this.listbox_tileLayers.Name = "listbox_tileLayers";
            this.listbox_tileLayers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listbox_tileLayers.Size = new System.Drawing.Size(209, 82);
            this.listbox_tileLayers.TabIndex = 6;
            this.listbox_tileLayers.SelectedIndexChanged += new System.EventHandler(this.listbox_tileLayers_SelectedIndexChanged);
            // 
            // listbox_textures
            // 
            this.listbox_textures.FormattingEnabled = true;
            this.listbox_textures.Location = new System.Drawing.Point(685, 221);
            this.listbox_textures.Name = "listbox_textures";
            this.listbox_textures.Size = new System.Drawing.Size(209, 134);
            this.listbox_textures.TabIndex = 7;
            this.listbox_textures.SelectedIndexChanged += new System.EventHandler(this.listbox_textures_SelectedIndexChanged);
            // 
            // button_addNewLayer
            // 
            this.button_addNewLayer.Location = new System.Drawing.Point(898, 111);
            this.button_addNewLayer.Name = "button_addNewLayer";
            this.button_addNewLayer.Size = new System.Drawing.Size(64, 23);
            this.button_addNewLayer.TabIndex = 8;
            this.button_addNewLayer.Text = "Add New";
            this.button_addNewLayer.UseVisualStyleBackColor = true;
            this.button_addNewLayer.Click += new System.EventHandler(this.button_addNewLayer_Click);
            // 
            // button_removeLayer
            // 
            this.button_removeLayer.Location = new System.Drawing.Point(898, 141);
            this.button_removeLayer.Name = "button_removeLayer";
            this.button_removeLayer.Size = new System.Drawing.Size(64, 23);
            this.button_removeLayer.TabIndex = 9;
            this.button_removeLayer.Text = "Remove";
            this.button_removeLayer.UseVisualStyleBackColor = true;
            this.button_removeLayer.Click += new System.EventHandler(this.button_removeLayer_Click);
            // 
            // button_addTexture
            // 
            this.button_addTexture.Location = new System.Drawing.Point(900, 221);
            this.button_addTexture.Name = "button_addTexture";
            this.button_addTexture.Size = new System.Drawing.Size(64, 23);
            this.button_addTexture.TabIndex = 10;
            this.button_addTexture.Text = "Add";
            this.button_addTexture.UseVisualStyleBackColor = true;
            this.button_addTexture.Click += new System.EventHandler(this.button_addTexture_Click);
            // 
            // button_removeTexture
            // 
            this.button_removeTexture.Location = new System.Drawing.Point(900, 250);
            this.button_removeTexture.Name = "button_removeTexture";
            this.button_removeTexture.Size = new System.Drawing.Size(64, 23);
            this.button_removeTexture.TabIndex = 11;
            this.button_removeTexture.Text = "Remove";
            this.button_removeTexture.UseVisualStyleBackColor = true;
            this.button_removeTexture.Click += new System.EventHandler(this.button_removeTexture_Click);
            // 
            // label_tileLayerFiles
            // 
            this.label_tileLayerFiles.AutoSize = true;
            this.label_tileLayerFiles.Location = new System.Drawing.Point(685, 66);
            this.label_tileLayerFiles.Name = "label_tileLayerFiles";
            this.label_tileLayerFiles.Size = new System.Drawing.Size(77, 13);
            this.label_tileLayerFiles.TabIndex = 12;
            this.label_tileLayerFiles.Text = "Tile Layer Files";
            // 
            // label_textureFiles
            // 
            this.label_textureFiles.AutoSize = true;
            this.label_textureFiles.Location = new System.Drawing.Point(685, 205);
            this.label_textureFiles.Name = "label_textureFiles";
            this.label_textureFiles.Size = new System.Drawing.Size(67, 13);
            this.label_textureFiles.TabIndex = 13;
            this.label_textureFiles.Text = "Texture Files";
            // 
            // groupBox_EditTools
            // 
            this.groupBox_EditTools.Controls.Add(this.label_alphaChannel);
            this.groupBox_EditTools.Controls.Add(this.trackbar_alphaChannel);
            this.groupBox_EditTools.Controls.Add(this.checkbox_fill);
            this.groupBox_EditTools.Controls.Add(this.radiobutton_erase);
            this.groupBox_EditTools.Controls.Add(this.radiobutton_draw);
            this.groupBox_EditTools.Location = new System.Drawing.Point(12, 647);
            this.groupBox_EditTools.Name = "groupBox_EditTools";
            this.groupBox_EditTools.Size = new System.Drawing.Size(495, 73);
            this.groupBox_EditTools.TabIndex = 14;
            this.groupBox_EditTools.TabStop = false;
            this.groupBox_EditTools.Text = "Edit Tools";
            // 
            // label_alphaChannel
            // 
            this.label_alphaChannel.AutoSize = true;
            this.label_alphaChannel.Location = new System.Drawing.Point(236, 19);
            this.label_alphaChannel.Name = "label_alphaChannel";
            this.label_alphaChannel.Size = new System.Drawing.Size(76, 13);
            this.label_alphaChannel.TabIndex = 4;
            this.label_alphaChannel.Text = "Alpha Channel";
            // 
            // trackbar_alphaChannel
            // 
            this.trackbar_alphaChannel.Location = new System.Drawing.Point(318, 19);
            this.trackbar_alphaChannel.Maximum = 100;
            this.trackbar_alphaChannel.Name = "trackbar_alphaChannel";
            this.trackbar_alphaChannel.Size = new System.Drawing.Size(171, 45);
            this.trackbar_alphaChannel.TabIndex = 3;
            this.trackbar_alphaChannel.TickFrequency = 5;
            this.trackbar_alphaChannel.Value = 100;
            this.trackbar_alphaChannel.Scroll += new System.EventHandler(this.trackbar_alphaChannel_Scroll);
            // 
            // checkbox_fill
            // 
            this.checkbox_fill.AutoSize = true;
            this.checkbox_fill.Location = new System.Drawing.Point(52, 20);
            this.checkbox_fill.Name = "checkbox_fill";
            this.checkbox_fill.Size = new System.Drawing.Size(38, 17);
            this.checkbox_fill.TabIndex = 2;
            this.checkbox_fill.Text = "Fill";
            this.checkbox_fill.UseVisualStyleBackColor = true;
            // 
            // radiobutton_erase
            // 
            this.radiobutton_erase.AutoSize = true;
            this.radiobutton_erase.Location = new System.Drawing.Point(169, 19);
            this.radiobutton_erase.Name = "radiobutton_erase";
            this.radiobutton_erase.Size = new System.Drawing.Size(52, 17);
            this.radiobutton_erase.TabIndex = 1;
            this.radiobutton_erase.TabStop = true;
            this.radiobutton_erase.Text = "Erase";
            this.radiobutton_erase.UseVisualStyleBackColor = true;
            // 
            // radiobutton_draw
            // 
            this.radiobutton_draw.AutoSize = true;
            this.radiobutton_draw.Location = new System.Drawing.Point(113, 19);
            this.radiobutton_draw.Name = "radiobutton_draw";
            this.radiobutton_draw.Size = new System.Drawing.Size(50, 17);
            this.radiobutton_draw.TabIndex = 0;
            this.radiobutton_draw.TabStop = true;
            this.radiobutton_draw.Text = "Draw";
            this.radiobutton_draw.UseVisualStyleBackColor = true;
            // 
            // picturebox_TexturePreview
            // 
            this.picturebox_TexturePreview.BackColor = System.Drawing.SystemColors.ControlText;
            this.picturebox_TexturePreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picturebox_TexturePreview.Location = new System.Drawing.Point(685, 361);
            this.picturebox_TexturePreview.Name = "picturebox_TexturePreview";
            this.picturebox_TexturePreview.Size = new System.Drawing.Size(200, 200);
            this.picturebox_TexturePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picturebox_TexturePreview.TabIndex = 15;
            this.picturebox_TexturePreview.TabStop = false;
            // 
            // label_contentRoot
            // 
            this.label_contentRoot.AutoSize = true;
            this.label_contentRoot.Location = new System.Drawing.Point(693, 31);
            this.label_contentRoot.Name = "label_contentRoot";
            this.label_contentRoot.Size = new System.Drawing.Size(73, 13);
            this.label_contentRoot.TabIndex = 16;
            this.label_contentRoot.Text = "Content Root:";
            // 
            // button_addExistingLayer
            // 
            this.button_addExistingLayer.Location = new System.Drawing.Point(898, 82);
            this.button_addExistingLayer.Name = "button_addExistingLayer";
            this.button_addExistingLayer.Size = new System.Drawing.Size(77, 23);
            this.button_addExistingLayer.TabIndex = 17;
            this.button_addExistingLayer.Text = "Add Existing";
            this.button_addExistingLayer.UseVisualStyleBackColor = true;
            this.button_addExistingLayer.Click += new System.EventHandler(this.button_addExistingLayer_Click);
            // 
            // textbox_layerType
            // 
            this.textbox_layerType.Location = new System.Drawing.Point(758, 170);
            this.textbox_layerType.Name = "textbox_layerType";
            this.textbox_layerType.ReadOnly = true;
            this.textbox_layerType.Size = new System.Drawing.Size(100, 20);
            this.textbox_layerType.TabIndex = 18;
            // 
            // MapGraphicsEditor
            // 
            this.MapGraphicsEditor.BackColor = System.Drawing.Color.Black;
            this.MapGraphicsEditor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MapGraphicsEditor.Location = new System.Drawing.Point(12, 27);
            this.MapGraphicsEditor.Name = "MapGraphicsEditor";
            this.MapGraphicsEditor.Size = new System.Drawing.Size(650, 587);
            this.MapGraphicsEditor.TabIndex = 0;
            this.MapGraphicsEditor.VSync = false;
            this.MapGraphicsEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapGraphicsEditor_MouseDown);
            // 
            // label_layerType
            // 
            this.label_layerType.AutoSize = true;
            this.label_layerType.Location = new System.Drawing.Point(693, 173);
            this.label_layerType.Name = "label_layerType";
            this.label_layerType.Size = new System.Drawing.Size(60, 13);
            this.label_layerType.TabIndex = 19;
            this.label_layerType.Text = "Layer Type";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.layerToolStripMenuItem.Text = "Layer";
            this.layerToolStripMenuItem.Click += new System.EventHandler(this.layerToolStripMenuItem_Click);
            // 
            // TileEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(987, 727);
            this.Controls.Add(this.label_layerType);
            this.Controls.Add(this.textbox_layerType);
            this.Controls.Add(this.button_addExistingLayer);
            this.Controls.Add(this.label_contentRoot);
            this.Controls.Add(this.picturebox_TexturePreview);
            this.Controls.Add(this.groupBox_EditTools);
            this.Controls.Add(this.label_textureFiles);
            this.Controls.Add(this.label_tileLayerFiles);
            this.Controls.Add(this.button_removeTexture);
            this.Controls.Add(this.button_addTexture);
            this.Controls.Add(this.button_removeLayer);
            this.Controls.Add(this.button_addNewLayer);
            this.Controls.Add(this.listbox_textures);
            this.Controls.Add(this.listbox_tileLayers);
            this.Controls.Add(this.textbox_contentPath);
            this.Controls.Add(this.scrollbar_vDisplay);
            this.Controls.Add(this.scrollbar_hDisplay);
            this.Controls.Add(this.checkbox_includeOutlining);
            this.Controls.Add(this.MapGraphicsEditor);
            this.Controls.Add(this.menustrip_editorMenu);
            this.MainMenuStrip = this.menustrip_editorMenu;
            this.Name = "TileEditorForm";
            this.Text = "Tile Editor";
            this.Shown += new System.EventHandler(this.TileEditorForm_Shown);
            this.menustrip_editorMenu.ResumeLayout(false);
            this.menustrip_editorMenu.PerformLayout();
            this.groupBox_EditTools.ResumeLayout(false);
            this.groupBox_EditTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar_alphaChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_TexturePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.GraphicsEditor MapGraphicsEditor;
        private System.Windows.Forms.MenuStrip menustrip_editorMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkbox_includeOutlining;
        private System.Windows.Forms.OpenFileDialog openFileBrowserControl;
        private System.Windows.Forms.SaveFileDialog saveFileBrowserControl;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserControl;
        private System.Windows.Forms.HScrollBar scrollbar_hDisplay;
        private System.Windows.Forms.VScrollBar scrollbar_vDisplay;
        private System.Windows.Forms.TextBox textbox_contentPath;
        private System.Windows.Forms.ListBox listbox_tileLayers;
        private System.Windows.Forms.ListBox listbox_textures;
        private System.Windows.Forms.Button button_addNewLayer;
        private System.Windows.Forms.Button button_removeLayer;
        private System.Windows.Forms.Button button_addTexture;
        private System.Windows.Forms.Button button_removeTexture;
        private System.Windows.Forms.Label label_tileLayerFiles;
        private System.Windows.Forms.Label label_textureFiles;
        private System.Windows.Forms.GroupBox groupBox_EditTools;
        private System.Windows.Forms.PictureBox picturebox_TexturePreview;
        private System.Windows.Forms.RadioButton radiobutton_erase;
        private System.Windows.Forms.RadioButton radiobutton_draw;
        private System.Windows.Forms.CheckBox checkbox_fill;
        private System.Windows.Forms.Label label_alphaChannel;
        private System.Windows.Forms.TrackBar trackbar_alphaChannel;
        private System.Windows.Forms.Label label_contentRoot;
        private System.Windows.Forms.Button button_addExistingLayer;
        private System.Windows.Forms.TextBox textbox_layerType;
        private System.Windows.Forms.Label label_layerType;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
    }
}

