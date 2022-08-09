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
   
    public partial class Minesweeper : Form
    {
        public int numMines, sizeRow, sizeCol;
        private int clickCount;
        private string[,] mineLyt;
        private List<ucBtnMine> lsMines = new List<ucBtnMine>();
        public Minesweeper()
        {
            sizeRow = 9;
            sizeCol = 9;
            numMines = 10;
            InitializeComponent();
            shuffleMine();
            setMines();
        }
        #region Creating Matrix with Mines
        private string[,] shuffleMine()
        {
            string[,] mineLayout = new string[sizeRow,sizeCol];

            int row, col;
            int x = 0;
            List<MinePosition> lsMinePos = new List<MinePosition>();
            MinePosition minePos;
            Random rand = new Random();
            while (x < numMines)
            {
                row = rand.Next(sizeRow);
                col = rand.Next(sizeCol);
                if (mineLayout[row, col] == null)
                {
                    lsMinePos.Add(minePos = new MinePosition(row, col));
                    mineLayout[row, col] = "X";
                    x++;
                }
            }
            foreach (MinePosition item in lsMinePos)
            {
                bool top, bottom, right, left;
                top = bottom = right = left = true;
                if (item.col != 0)
                {
                    if (mineLayout[item.row, item.col - 1] == null)
                    {
                        mineLayout[item.row, item.col - 1] = "1";
                    }
                    else if (mineLayout[item.row, item.col - 1] != "X")
                    {
                        mineLayout[item.row, item.col - 1] = (int.Parse(mineLayout[item.row, item.col - 1]) + 1).ToString();
                    }
                }
                else { top = false; }
                if (item.row != 0)
                {
                    if (mineLayout[item.row - 1, item.col] == null)
                    {
                        mineLayout[item.row - 1, item.col] = "1";
                    }
                    else if (mineLayout[item.row - 1, item.col] != "X")
                    {
                        mineLayout[item.row - 1, item.col] = (int.Parse(mineLayout[item.row - 1, item.col]) + 1).ToString();
                    }
                }
                else { left = false; }
                if (item.col < sizeCol - 1)
                {
                    if (mineLayout[item.row, item.col + 1] == null)
                    {
                        mineLayout[item.row, item.col + 1] = "1";
                    }
                    else if (mineLayout[item.row, item.col + 1] != "X")
                    {
                        mineLayout[item.row, item.col + 1] = (int.Parse(mineLayout[item.row, item.col + 1]) + 1).ToString();
                    }
                }
                else { bottom = false; }
                if (item.row < sizeRow - 1)
                {
                    if (mineLayout[item.row + 1, item.col] == null)
                    {
                        mineLayout[item.row + 1, item.col] = "1";
                    }
                    else if (mineLayout[item.row + 1, item.col] != "X")
                    {
                        mineLayout[item.row + 1, item.col] = (int.Parse(mineLayout[item.row + 1, item.col]) + 1).ToString();
                    }
                }
                else { right = false; }
                if (top && left)
                {
                    if (mineLayout[item.row - 1, item.col - 1] == null)
                    {
                        mineLayout[item.row - 1, item.col - 1] = "1";
                    }
                    else if (mineLayout[item.row - 1, item.col - 1] != "X")
                    {
                        mineLayout[item.row - 1, item.col - 1] = (int.Parse(mineLayout[item.row - 1, item.col - 1]) + 1).ToString();
                    }
                }
                if (top && right)
                {
                    if (mineLayout[item.row + 1, item.col - 1] == null)
                    {
                        mineLayout[item.row + 1, item.col - 1] = "1";
                    }
                    else if (mineLayout[item.row + 1, item.col - 1] != "X")
                    {
                        mineLayout[item.row + 1, item.col - 1] = (int.Parse(mineLayout[item.row + 1, item.col - 1]) + 1).ToString();
                    }
                }
                if (bottom && left)
                {
                    if (mineLayout[item.row - 1, item.col + 1] == null)
                    {
                        mineLayout[item.row - 1, item.col + 1] = "1";
                    }
                    else if (mineLayout[item.row - 1, item.col + 1] != "X")
                    {
                        mineLayout[item.row - 1, item.col + 1] = (int.Parse(mineLayout[item.row - 1, item.col + 1]) + 1).ToString();
                    }
                }
                if (bottom && right)
                {
                    if (mineLayout[item.row + 1, item.col + 1] == null)
                    {
                        mineLayout[item.row + 1, item.col + 1] = "1";
                    }
                    else if (mineLayout[item.row + 1, item.col + 1] != "X")
                    {
                        mineLayout[item.row + 1, item.col + 1] = (int.Parse(mineLayout[item.row + 1, item.col + 1]) + 1).ToString();
                    }
                }
            }
            for (int i = 0; i < sizeRow; i++ )
            {
                for (int j = 0; j< sizeCol; j++)
                {
                    if (mineLayout[i,j] == null)
                    {
                        mineLayout[i, j] = " ";
                    }
                }
            }
                return mineLayout;
            
        }
        #endregion

      

        #region Setup game
        // Setups up the board of size from scratch
        private void setMines(){
            if (lsMines.Count > 0)
            {
                lsMines.Clear();
            }
            resetTimer();
            lblMines.Text = numMines.ToString();
            clickCount = (sizeRow * sizeCol)- numMines;
            TableLayoutPanel tblPanel = new TableLayoutPanel();
            this.AutoSize = true;
            tblPanel.AutoSize = true;
            mineLyt = shuffleMine();
            for (int i = 0; i < sizeRow; i++)
            {

                tblPanel.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
                for (int j = 0; j < sizeCol; j++)
                {
                    tblPanel.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
                    ucBtnMine mine = new ucBtnMine();
                    mine.setBtn(mineLyt[i,j]);
                    mine.Click += new EventHandler(clickedMinefield);
                    mine.Dock = DockStyle.Fill;
                    tblPanel.Controls.Add(mine, j, i);
                    
                    if (mine.mineSet)
                    {
                        lsMines.Add(mine);
                    }                }
            }
            if (pnlMain.Controls.Count > 0)
            {
                pnlMain.Controls.Clear();
            }
            pnlMain.Controls.Add(tblPanel);
        }
        #endregion 

        private void clickedMinefield(object sender, EventArgs e)
        {
            ucBtnMine mine = sender as ucBtnMine;
            if (!timerGame.Enabled)
            {
                timerGame.Enabled = true;
            }

            if (mine.clkState)
            {
                mine.clickedMine();
                clickCount--;
                Console.WriteLine("Clicked.");
                if (mine.mineSet)
                {
                    resultDisp(false);
                    foreach (ucBtnMine item in lsMines)
                    {
                        item.clickedMine();
                    }
                    pnlMain.Enabled = false;
                } 
                else if (clickCount == 0)
                {
                    //win condition function
                    resultDisp(true);
                }
                else if (mine.mineTxt == " ")
                {
                    checkAdjacent(mine);
                }
            }
            
        }

        private void checkAdjacent(ucBtnMine midMine)
        {
            bool top, left, right, bottom;
            top = left = right = bottom = true;
            Console.WriteLine("Checking..");
            TableLayoutPanel tblPanel = pnlMain.Controls[0] as TableLayoutPanel;
            int mineCol = tblPanel.GetColumn(midMine);
            int mineRow = tblPanel.GetRow(midMine);
            // Check mine upwards
            if (mineCol > 0)
            {
                checkEmpty(tblPanel.GetControlFromPosition(mineCol - 1, mineRow) as ucBtnMine);
            }
            else { top = false; }
            // Check mine downwards
            if (mineCol < sizeCol - 1)
            {
                checkEmpty(tblPanel.GetControlFromPosition(mineCol + 1, mineRow) as ucBtnMine);
            }
            else { bottom = false; }
            // Check mine on left
            if (mineRow > 0)
            {
                checkEmpty(tblPanel.GetControlFromPosition(mineCol, mineRow - 1) as ucBtnMine);
            }
            else { left = false; }
            // Check mine right
            if (mineRow < sizeRow - 1)
            {
                checkEmpty(tblPanel.GetControlFromPosition(mineCol, mineRow + 1) as ucBtnMine);
            }
            else { right = false; }

            if (top && right)
            {
                checkEmpty(tblPanel.GetControlFromPosition(mineCol - 1, mineRow + 1) as ucBtnMine);
            }
            if (top && left)
            {
                checkEmpty(tblPanel.GetControlFromPosition(mineCol - 1, mineRow - 1) as ucBtnMine);
            }
            if (bottom && left)
            {
                checkEmpty(tblPanel.GetControlFromPosition(mineCol + 1, mineRow - 1) as ucBtnMine);
            }
            if (bottom && right)
            {
                checkEmpty(tblPanel.GetControlFromPosition(mineCol + 1, mineRow + 1) as ucBtnMine);
            }
            Console.WriteLine(mineCol);
            Console.WriteLine(mineRow);
        }

        private void checkEmpty(ucBtnMine mine)
        {
            if (mine.clkState)
            {
                mine.clickedMine();
                clickCount--;
                if (clickCount == 0)
                {
                    resultDisp(true);
                }
                else if (mine.mineTxt == " ")
                {
                    checkAdjacent(mine);
                }
            }
        }

        #region Fn to Reset the Game, reshuffling mines
        private void resetMine()
        {
            resetTimer();
            if (lsMines.Count > 0)
            {
                lsMines.Clear();
            }

            clickCount = (sizeRow * sizeCol) - numMines;
            pnlMain.Enabled = true;
            TableLayoutPanel tblPanel = pnlMain.Controls[0] as TableLayoutPanel;
            mineLyt = shuffleMine();
            for (int i = 0; i<sizeRow;i++)
            {
                for (int j = 0; j<sizeCol; j++){
                    ucBtnMine mine = tblPanel.GetControlFromPosition(j, i) as ucBtnMine;
                    mine.setBtn(mineLyt[i, j]);
                    if (mine.mineSet)
                    {
                        lsMines.Add(mine);
                    }
                }
               
            }
        }
        #endregion

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetMine();
        }

        private void resultDisp(bool result)
        {
            ResultForm form = new ResultForm(result);
            timerGame.Enabled = false;
            form.ShowDialog();
        }
        private void btnDifficulty_Click(object sender, System.EventArgs e)
        {
            FormDificulty form = new FormDificulty(this);
            form.ShowDialog();
            Console.WriteLine("Back from form");
            setMines();
            Console.WriteLine("Set new board");
            if (!pnlMain.Enabled)
            {
                pnlMain.Enabled = true;
            }
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = (int.Parse(lblTimer.Text) + 1).ToString();
        }

        private void resetTimer()
        {
            timerGame.Enabled = false;
            lblTimer.Text = "000";
        }
    }
}
