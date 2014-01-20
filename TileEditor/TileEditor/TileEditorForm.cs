using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TileEditor.Manager;
using TileEditor.Forms;

namespace TileEditor
{
    public partial class TileEditorForm : Form
    {
        //Data manager to take care of all form data types (textures, images, layers etc... )
        private DataManager m_dataManager;

        //Graphics manager to deal with updating and drawing of the graphics controls
        private GraphicsManager m_graphicsManager;

        public TileEditorForm()
        {
            InitializeComponent();
            InitializeView();
        }

        //Called after the user has selected a root content file path, as to avoid unnecessary actions
        private void CompleteInitialization()
        {
            InitializeDataHandler();

            RegisterEvents();
        }

        private void InitializeDataHandler()
        {
            //Initialize data manager with the graphics device this form uses, so the data manager can give certain handlers
            //the ability to handle graphical data
            m_dataManager = new DataManager(MapGraphicsEditor.GraphicsDevice, textbox_contentPath.Text);
            //use our new datamanager to initialize the graphics manager for the MapGraphicsEditor control
            m_graphicsManager = new GraphicsManager(MapGraphicsEditor, m_dataManager);
        }

        //Initialize default aspects of the view
        private void InitializeView()
        {
            DisableScrollBars();

            //Set draw to layer combo box to be of type drop down list
            combobox_drawLayer.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void RegisterEvents()
        {
            //Whenever the form is idle, update / redraw the tile display.
            Application.Idle += new EventHandler(Update_OnIdle);

            //Whenever the graphics editor control is re-painted, the OnDraw event will fire here (Abstract away unnecessary further implementation of the graphics editor)
            MapGraphicsEditor.OnDraw += new EventHandler(MapGraphicsEditor_OnDraw);
        }

        private void CloseForm()
        {
            Close();
        }

        #region Events

        //After form is shown for first time
        private void TileEditorForm_Shown(object sender, EventArgs e)
        {
            folderBrowserControl.Description = "Please select the content folder of the tile engine";

            //user must enter the root content folder path of the content this instance of the tile editor will use
            //if user does not enter a content folder path, close the editor
            //TODO: Further down the road there should be a way to disallow the user from selecting an 
            //      invalid folder path (directory tree must be at least similar to the engine content root directory)
            if (folderBrowserControl.ShowDialog() == DialogResult.OK)
            {
                //Once user has selected path, display path in the content root text box
                textbox_contentPath.Text = folderBrowserControl.SelectedPath;
                folderBrowserControl.Description = "";
                //Now we can finnish all other initializations.
                CompleteInitialization();
            }
            else
                CloseForm();
        }

        //Add new layer
        private void button_addNewLayer_Click(object sender, EventArgs e)
        {
            AddNewLayer();
        }
        //Add new layer from menu
        private void layerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewLayer();
        }

        //Add existing Tile layer
        private void button_addExistingLayer_Click(object sender, EventArgs e)
        {
            AddExistingLayer();
        }

        //Open existing layer ( same as add existing layer)
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddExistingLayer();
        }

        //User has selected a layer or changed selection of a layer

        //Note** the OnDraw method will be called before the selection index changed, since the selection index changed
        //only occurs after the mouse click occurs. So.. the layer will be drawn before the view can update according
        //to the new layer. This graphical behavior can be changed by calling the selection changed on mouse down events, instead of
        //mouse click.
        private void listbox_tileLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if selection is not null, update the current layer to be the top most selected layer
            //Update the alpha tracker, and the scroll bars accordingly
            if (listbox_tileLayers.SelectedItem != null)
                m_dataManager.LayerSelectionChanged(listbox_tileLayers.SelectedItem.ToString());
            //Call helper function to update view objects when the layer selection changes
            NewLayerViewUpdate();
        }

        //Remove selected layer(s)
        private void button_removeLayer_Click(object sender, EventArgs e)
        {
            NewLayerViewUpdate();
            if (listbox_tileLayers.SelectedItems.Count > 0)
            {
                m_dataManager.RemoveSelectedLayers(listbox_tileLayers.SelectedItems.Cast<string>().ToList() );

                //For every selected item, we remove it from the list box
                while (listbox_tileLayers.SelectedItems.Count > 0)
                {
                    listbox_tileLayers.Items.Remove(listbox_tileLayers.SelectedItem);
                }
            }
        }

        //Add new texture
        private void button_addTexture_Click(object sender, EventArgs e)
        {
            openFileBrowserControl.FileName = "";
            //Filter file types that will be allowed
            openFileBrowserControl.Multiselect = true;
            openFileBrowserControl.Filter = "Image Files (*.png, *.jpg, *.gif)|*.png;*.jpg;*.gif";
            openFileBrowserControl.InitialDirectory = textbox_contentPath.Text;

            if (openFileBrowserControl.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in openFileBrowserControl.FileNames)
                {
                    //Call the data manager to handle the texture file.
                    //if successfully handled, the string != empty string, then add the parsed file name into the texture list box
                    string trimmedFilePath = m_dataManager.HandleTexture(fileName);

                    if (!string.IsNullOrEmpty(trimmedFilePath))
                        listbox_textures.Items.Add(trimmedFilePath);
                }
            }
        }

        //User has selected a texture, or changed their selection
        private void listbox_textures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listbox_textures.SelectedItem != null)
            {
                picturebox_TexturePreview.Image = m_dataManager.GetPreviewImage(listbox_textures.SelectedItem.ToString());
            }
        }

        //When selection is changed within the draw layer combo box we set the current selected item to be the current layer
        //we are drawing to
        private void combobox_drawLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combobox_drawLayer.Items.Count > 0)
            {
                //Update the current editing layer
                string layerName = combobox_drawLayer.SelectedItem.ToString();
                m_dataManager.LayerSelectionChanged(layerName);
                //Update the layer type text box to the current editing layer
                textbox_layerType.Text = m_dataManager.GetLayerTypeAsString( layerName );
            }
        }

        //User wants to remove selected texture
        private void button_removeTexture_Click(object sender, EventArgs e)
        {
            if (listbox_textures.SelectedItem != null)
            {
                //If preview box image is currently the selected texture, which should always be the case, then clear the preview box
                if (picturebox_TexturePreview.Image == m_dataManager.GetPreviewImage(listbox_textures.SelectedItem.ToString()))
                    picturebox_TexturePreview.Image = null;

                //Remove texture from data management
                m_dataManager.RemoveTexture(listbox_textures.SelectedItem.ToString());

                listbox_textures.Items.Remove(listbox_textures.SelectedItem);
            }
        }

        //Mouse down event on the map graphics editor, see if user wants to draw / erase / drawfill / erasefill
        private void MapGraphicsEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (radiobutton_draw.Checked)
            {
                if (listbox_textures.SelectedItem != null)
                {
                    m_dataManager.EditLayerDataDraw(checkbox_fill.Checked, listbox_textures.SelectedItem.ToString());
                }
            }
            else if (radiobutton_erase.Checked)
            {
                m_dataManager.EditLayerDataErase(checkbox_fill.Checked);
            }
        }

        private void Update_OnIdle(Object sender, EventArgs e)
        {
            //Redraw the map editor when form is idle
            MapGraphicsEditor.Invalidate();
        }

        private void MapGraphicsEditor_OnDraw(Object sender, EventArgs e)
        {
            //Only draw if there is a selected layer layout.
            //If no selected layer layouts then we clear the graphics device.
            if (listbox_tileLayers.SelectedItems.Count > 0)
            {
                //First Update the display based on the scrollbars / location of display (Mouse pointer)
                m_graphicsManager.Update( scrollbar_hDisplay.Value, scrollbar_vDisplay.Value, MousePosition);
                //Now draw the scene (all selected layer layouts) into the graphics control with it's spritebatch

                //Parameters to be passed in... list of layers selected, and is tile outlining enabled
                m_graphicsManager.Draw(MapGraphicsEditor.spriteBatch, listbox_tileLayers.SelectedItems.Cast<string>().ToList(), checkbox_includeOutlining.Checked);
            }
            else
                m_graphicsManager.ClearControlScreen();
        }

        //Changing the alpha channel of a layer
        private void trackbar_alphaChannel_Scroll(object sender, EventArgs e)
        {
            if (listbox_tileLayers.SelectedItems.Count > 0)
            {
                //dividing by 100.0f converts to a float
                m_dataManager.ChangeLayerAlpha(trackbar_alphaChannel.Value / 100.0f);
            }
        }

        //File events

        //Save layer
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listbox_tileLayers.SelectedItems.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you would like to save the layer: " + listbox_tileLayers.SelectedItem.ToString() + "? ",
                                                "Form saving",
                                                MessageBoxButtons.YesNoCancel,
                                                MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    saveFileBrowserControl.FileName = listbox_tileLayers.SelectedItem.ToString();
                    saveFileBrowserControl.InitialDirectory = textbox_contentPath.Text;

                    saveFileBrowserControl.Filter = DataManager.TileLayerFileFilter;
                    //Save the top most selected layer
                    if( saveFileBrowserControl.ShowDialog() == DialogResult.OK )
                        m_dataManager.SaveLayer(textbox_contentPath.Text,
                                                saveFileBrowserControl.FileName, 
                                                listbox_tileLayers.SelectedItem.ToString(), 
                                                listbox_textures.Items.Cast<string>().ToList());
                }
            }
        }

        //Exit menu button selected
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        #endregion

        //Should probably wrap these helper methods in another class.
        #region HelperMethods

        //Add new layer
        private void AddNewLayer()
        {
            NewTileLayerForm newTileLayerForm = new NewTileLayerForm();

            newTileLayerForm.ShowDialog();
            //If user presses ok process form parameters to create new Tile Layer
            if (newTileLayerForm.ok_pressed)
            {
                // if data manager was able to handle the new layer, add the tile layer name, which will also be the key of the layer to the tile layer list box
                if (m_dataManager.HandleNewLayer(newTileLayerForm.textbox_name.Text, newTileLayerForm.textbox_width.Text,
                                                    newTileLayerForm.textbox_height.Text, newTileLayerForm.combobox_layerTypes.SelectedIndex))
                    listbox_tileLayers.Items.Add(newTileLayerForm.textbox_name.Text);
            }
        }

        //Add an existing layer
        private void AddExistingLayer()
        {
            openFileBrowserControl.Multiselect = false;
            openFileBrowserControl.Filter = "Layer File|*.layer";

            openFileBrowserControl.InitialDirectory = textbox_contentPath.Text;

            if (openFileBrowserControl.ShowDialog() == DialogResult.OK)
            {
                //list of textures retrieved with the layer
                Tuple< string, List<string> > openedLayerData;

                openedLayerData = m_dataManager.OpenLayer(openFileBrowserControl.FileName);

                if (!string.IsNullOrEmpty(openedLayerData.Item1))
                {
                    listbox_tileLayers.Items.Add(openedLayerData.Item1);
                    //Add texture names into texture list box as well
                    foreach (string textureName in openedLayerData.Item2)
                    {
                        listbox_textures.Items.Add(textureName);
                    }
                }
            }
        }

        private void DisableScrollBars()
        {
            scrollbar_hDisplay.Visible = false;
            scrollbar_vDisplay.Visible = false;
        }

        private void NewLayerViewUpdate()
        {
            //Get layer dimensions of the largest or current selected layout, if there is no current selected layout then disable scroll bars.
            //Also get and update the alpha channel track bar
            //Tuple pair might not be the best way of getting the width / height dimensions... but for now it'll do
            if (listbox_tileLayers.SelectedItems.Count > 0)
            {
                Tuple<int, int> layoutDimensions = m_dataManager.GetLayerDimensions(listbox_tileLayers.SelectedItems.Cast<string>().ToList());
                scrollbar_vDisplay.Visible = true;
                scrollbar_hDisplay.Visible = true;
                scrollbar_hDisplay.Minimum = 0;
                scrollbar_vDisplay.Minimum = 0;

                scrollbar_hDisplay.Maximum = layoutDimensions.Item1;
                scrollbar_vDisplay.Maximum = layoutDimensions.Item2;

                //If viewable page dimension is greater than the layout dimensions, they make the scroll bar not visible. 

                //If scrollbar's current value is larger than the current scroll bars maximum value (from going from a larger layer to a smaller layer while the
                //scrollbar is at it's maximum), then transform the dimensions to be within context of the current scrollbar
                if (scrollbar_hDisplay.LargeChange <= scrollbar_hDisplay.Maximum)
                {
                    if (scrollbar_hDisplay.Value >= scrollbar_hDisplay.Maximum - scrollbar_hDisplay.LargeChange)
                        scrollbar_hDisplay.Value = scrollbar_hDisplay.Maximum - scrollbar_hDisplay.LargeChange;
                }
                else
                {
                    scrollbar_hDisplay.Value = 0;
                    scrollbar_hDisplay.Visible = false;
                }

                if (scrollbar_vDisplay.LargeChange <= scrollbar_vDisplay.Maximum)
                {
                    if (scrollbar_vDisplay.Value >= scrollbar_vDisplay.Maximum - scrollbar_vDisplay.LargeChange)
                        scrollbar_vDisplay.Value = scrollbar_vDisplay.Maximum - scrollbar_vDisplay.LargeChange;
                }
                else
                {
                    scrollbar_vDisplay.Value = 0;
                    scrollbar_vDisplay.Visible = false;
                }

                //Get the alpha channel of the top most selected layer
                trackbar_alphaChannel.Value = m_dataManager.GetLayerAlpha(listbox_tileLayers.SelectedItem as string);
                //Set the layer type text of the top most selected layer
                textbox_layerType.Text = m_dataManager.GetLayerTypeAsString(listbox_tileLayers.SelectedItem as string);

                //Every time, the layer selection has changed, we clear the draw layer combo-box and re-populate it with all selected layers
                combobox_drawLayer.Items.Clear();
                foreach (object item in listbox_tileLayers.SelectedItems)
                {
                    combobox_drawLayer.Items.Add(item.ToString());
                }

                //if there are items in the combo box, always set the default selected index to be the first item (Which will be the first item selected in the 
                //layer listbox
                if (combobox_drawLayer.Items.Count > 0)
                    combobox_drawLayer.SelectedIndex = 0;
            }
            else
            {
                textbox_layerType.Text = string.Empty;
                DisableScrollBars();
                trackbar_alphaChannel.Value = trackbar_alphaChannel.Maximum;

                combobox_drawLayer.Items.Clear();
            }
        }

        #endregion
    }
}
