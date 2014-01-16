using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TileEditor.Forms
{
    public partial class NewTileLayerForm : Form
    {
        public bool ok_pressed = false;

        public NewTileLayerForm()
        {
            InitializeComponent();
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
