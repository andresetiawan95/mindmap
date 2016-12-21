using mindmap.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mindmap.FormLayout
{
    public class DefaultTextbox : Control, ITextBox
    {
        private IPanel canvas;
        private Button btn;
        private TextSegment text;
        private TextBox textbox;
        private Control controls;
        public DefaultTextbox(Control controls)
        {
            this.controls = controls;
            this.controls.BackColor = Color.White;
            this.textbox = new TextBox()
            {
                Left = 50,
                Top = 50,
                Width = 400
            };
            this.controls.Controls.Add(this.textbox);
            InitializeButton();
        }
        public IPanel getCanvas
        {
            set
            {
                this.canvas = value;
            }
            get
            {
                return this.canvas;
            }
        }
        public TextSegment getText
        {
            set
            {
                this.text = value;
            }
            get
            {
                return this.text;
            }
        }
        private void InitializeButton()
        {
            btn = new Button();
            btn.Text = "Edit";
            btn.Size = new Size(70, 30);
            btn.Location = new Point(180, 100);
            btn.Click += Btn_Click;
            this.controls.Controls.Add(btn);

        }
        private void Btn_Click(object sender, EventArgs e)
        {
            this.text.Value = this.textbox.Text;
            this.canvas.Repaint();
        }
        public void SetInitText()
        {
            this.textbox.Text = this.text.Value;
        }
        public void SetText(String text)
        {

        }
        public void UpdateText()
        {

        }
    }
}