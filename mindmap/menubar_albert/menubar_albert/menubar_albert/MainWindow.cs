using System;
using menubar_albert.MenuItems;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace menubar_albert
{
    public partial class MainWindow : Form
    {
        private IMenubar menubar;

        public MainWindow()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            Debug.WriteLine("Initializing UI objects.");

                #region Menubar
                Debug.WriteLine("Loading menubar...");
                this.menubar = new DefaultMenubar();
                this.Controls.Add((Control)this.menubar);

                DefaultMenuItem exampleMenuItem1 = new DefaultMenuItem("File");
                this.menubar.AddMenuItem(exampleMenuItem1);

                DefaultMenuItem exampleMenuItem11 = new DefaultMenuItem("New Mindmap");
                exampleMenuItem1.AddMenuItem(exampleMenuItem11);
                DefaultMenuItem exampleMenuItem12 = new DefaultMenuItem("Open Mindmap");
                exampleMenuItem1.AddMenuItem(exampleMenuItem12);
                DefaultMenuItem exampleMenuItem13 = new DefaultMenuItem("Close");
                exampleMenuItem1.AddMenuItem(exampleMenuItem13);

                DefaultMenuItem exampleMenuItem2 = new DefaultMenuItem("Edit");
                this.menubar.AddMenuItem(exampleMenuItem2);

                DefaultMenuItem exampleMenuItem21 = new DefaultMenuItem("Add Proccess");
                exampleMenuItem2.AddMenuItem(exampleMenuItem21);

                DefaultMenuItem exampleMenuItem3 = new DefaultMenuItem("Export");
                this.menubar.AddMenuItem(exampleMenuItem3);

               #endregion 
        }
        
    }
}
