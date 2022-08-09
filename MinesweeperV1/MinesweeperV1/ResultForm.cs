using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperV1
{
    public partial class ResultForm : Form
    {
        public ResultForm(bool result)
        {
            InitializeComponent();
            lblResult.Text = result?"You have WON!":"You have LOST!";
        }
    }
}
