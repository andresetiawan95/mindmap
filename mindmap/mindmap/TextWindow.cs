using mindmap.FormLayout;
using mindmap.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mindmap
{
    public partial class TextWindow : Form
    {
        private ITextBox textbox;
        private IPanel canvas;
        private TextSegment text;
        public TextWindow(IPanel canvas, TextSegment text)
        {
            this.canvas = canvas;
            this.text = text;
            InitializeComponent();
            InitializeUI();
        }
        private void InitializeUI()
        {
            this.textbox = new DefaultTextbox(this);

            this.textbox.getCanvas = canvas;
            this.textbox.getText = text;
            this.textbox.SetInitText();
            //this.toolStripContainer1.ContentPanel.Controls.Add((Control)this.textbox);
            this.Controls.Add((Control)textbox);
        }
    }
}
