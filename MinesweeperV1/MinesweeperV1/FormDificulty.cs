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
    public partial class FormDificulty : Form
    {
        Minesweeper gameForm;
        public FormDificulty(Minesweeper mainForm)
        {
            InitializeComponent();
            gameForm = mainForm;
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            gameForm.sizeCol = 9;
            gameForm.sizeRow = 9;
            gameForm.numMines = 10;
            this.Close();
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            gameForm.sizeCol = 15;
            gameForm.sizeRow = 15;
            gameForm.numMines = 40;
            this.Close();
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            gameForm.sizeCol = 22;
            gameForm.sizeRow = 22;
            gameForm.numMines = 99;
            this.Close();
        }
    }
}
