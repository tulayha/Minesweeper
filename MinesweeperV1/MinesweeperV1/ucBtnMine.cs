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
        public bool clkState;
        public string mineTxt;

        #region Initialize User Control: ucBtnMine
        public ucBtnMine()
        {
            InitializeComponent();
         }
        #endregion

        #region Set/Reset ucBtnMine
        public void setBtn(string mineData)
        {
            clkState = true;
            if (!btnMine.Visible)
            {
                btnMine.Visible = true;
            }
            mineSet = false;
            lblMine.BackColor = System.Drawing.SystemColors.Control;
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

        private void btnMine_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, EventArgs.Empty);
            clickedMine();
        }
        #endregion

        public void clickedMine()
        {
            clkState = false;
            btnMine.Visible = false;
        }
    }
}
