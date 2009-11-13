using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard.GUI
{
    delegate void ItemMouseOver(int selectedIndex);
    delegate void ItemMouseClick(int selectedIndex);

    class ClickableList : ListBox
    {
        public event ItemMouseOver ItemMouseOver;
        public event ItemMouseClick ItemMouseClick;

        public ClickableList()
            : base()
        {
            this.BorderStyle = BorderStyle.None;

            this.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (this.SelectedIndex != -1)
                {
                    if(this.ItemMouseClick != null)
                        this.ItemMouseClick(this.SelectedIndex);
                }
            };
            this.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                this.SelectedIndex = this.IndexFromPoint(e.X, e.Y);
                if (this.SelectedIndex != -1)
                {
                    Cursor.Current = Cursors.Hand;
                    if (this.ItemMouseOver != null)
                        this.ItemMouseOver(this.SelectedIndex);
                }
            };
            this.MouseLeave += delegate(object sender, EventArgs e)
            {
                this.SelectedIndex = -1;
            };   
        }
    }
}
