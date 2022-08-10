using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperV1
{
    public partial class ucBtnMine : UserControl
    {
        public bool mineSet;
        public bool clkState, mineFlag;
        public string mineTxt;
        public int mouseClickID = 0;

        #region Initialize User Control: ucBtnMine
        public ucBtnMine()
        {
            InitializeComponent();
            mineFlag = false;
            btnMine.MouseDown += new MouseEventHandler(btnMine_MouseDown);
            btnMine.BackgroundImageLayout = ImageLayout.Zoom;
            lblMine.Click += lblMine_Click;
         }

        protected void lblMine_Click(object sender, EventArgs e)
        {
            mouseClickID = 5;
            this.InvokeOnClick(this, EventArgs.Empty);
        }
        #endregion

        #region Set/Reset ucBtnMine
        public void setBtn(string mineData)
        {
            resetMine();
            switch (mineData)
            {
                case "X":
                    mineSet = true;
                    lblMine.ForeColor = System.Drawing.Color.Black;
                    lblMine.BackColor = System.Drawing.Color.Red;
                    break;
                case "1":
                    lblMine.ForeColor = System.Drawing.Color.MidnightBlue;
                    break;
                case "2":
                    lblMine.ForeColor = System.Drawing.Color.Blue;
                    break;
                case "3":
                    lblMine.ForeColor = System.Drawing.Color.Green;
                    break;
                case "4":
                    lblMine.ForeColor = System.Drawing.Color.Orange;
                    break;
                case "5":
                    lblMine.ForeColor = System.Drawing.Color.Red;
                    break;
                case "6":
                    lblMine.ForeColor = System.Drawing.Color.DarkRed;
                    break;
                case "7":
                    lblMine.ForeColor = System.Drawing.Color.DarkMagenta;
                    break;
                case "8":
                    lblMine.ForeColor = System.Drawing.Color.Black;
                    break;
                default:
                    mineData = " ";
                    break;
            }
            lblMine.Text = mineData;
            mineTxt = mineData;
        }
        #endregion

        #region Functions for this userControl

        public void resetMine()
        {
            lblMine.Enabled = false;
            clkState = true;
            mineFlag = false;
            btnMine.BackgroundImage = null;
            if (!btnMine.Visible)
            {
                btnMine.Visible = true;
            }
            mineSet = false;
            lblMine.BackColor = System.Drawing.SystemColors.Control;
        }
        protected void btnMine_MouseDown(object sender, MouseEventArgs e)
        {
            
            switch (e.Button)
            {
                case MouseButtons.Left:
                   // mineFlag = false;
                    mouseClickID = -1;
                    //clickedMine();
                    this.InvokeOnClick(this, EventArgs.Empty);
                    break;
                case MouseButtons.Right:
                    mouseClickID = 1;
                    this.InvokeOnClick(this, EventArgs.Empty);
                    break;
                default:
                    break;
            }
        }

        #endregion
        public void toggleFlag()
        {
            mineFlag = !mineFlag;
            btnMine.BackgroundImage = mineFlag ? MinesweeperV1.Properties.Resources.Flag_red: null;
            //btnMine.BackColor = mineFlag ? System.Drawing.Color.Red : System.Drawing.SystemColors.ControlDark;
                    
        }
        public void clickedMine()
        {
            lblMine.Enabled = true;
            clkState = false;
            btnMine.Visible = false;
        }
    }
}
