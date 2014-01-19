using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TileEditor.Forms.EngineHelpers;

namespace TileEditor.Forms
{
    public partial class NewTileLayerForm : Form
    {
        public bool ok_pressed = false;

        public NewTileLayerForm()
        {
            InitializeComponent();

            InitializeComboBox();
        }

        //To be honest the non-view logic should probably be abstracted to a data class, but it works here, is easy to read, and this form isn't used a lot.
        private void InitializeComboBox()
        {
            //**Note the list will indexes will always be returned in the order they are declared in the LayerType enum
            combobox_layerTypes.DataSource = EngineUtility.GetLayerTypeList();

            //Get the index by passing the current items as a list of strings
            combobox_layerTypes.SelectedIndex = EngineUtility.GetDefaultIndex( combobox_layerTypes.Items.Cast<string>().ToList());
            combobox_layerTypes.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textbox_name.Text))
            {
                ok_pressed = true;
                Close();
            }
            else
            {
                //Generic error message, should probably improve on this later
                MessageBox.Show("Name cannot be empty!");
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            ok_pressed = false;
            Close();
        }
    }
}
