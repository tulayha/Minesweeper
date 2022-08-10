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
        private bool firstClick;
        public Minesweeper()
        {
            sizeRow = 9;
            sizeCol = 9;
            numMines = 10;
            InitializeComponent();
            setMines();
        }
        #region Creating Matrix with Mines
        private void shuffleMine(ucBtnMine mine)
        {
            string[,] mineLayout = new string[sizeRow,sizeCol];
            TableLayoutPanel tblPanel = pnlMain.Controls[0] as TableLayoutPanel;
            int mineCol = tblPanel.GetColumn(mine);
            int mineRow = tblPanel.GetRow(mine);
            List<MinePosition> lsFirstClick = new List<MinePosition>();
            for (int i = mineRow - 1; i <= mineRow + 1; i++)
            {
                if (i >= 0 && i < sizeRow)
                {
                    for (int j = mineCol - 1; j <= mineCol + 1; j++)
                    {
                        if (j >= 0 && j < sizeCol)
                        {
                            lsFirstClick.Add(new MinePosition(i, j));
                        }
                    }
                }
            }
            int row, col;
            int x = 0;
            List<MinePosition> lsMinePos = new List<MinePosition>();
            MinePosition minePos;
            Random rand = new Random();
            while (x < numMines)
            {
                row = rand.Next(sizeRow);
                col = rand.Next(sizeCol);
                if (mineLayout[row, col] == null && !lsFirstClick.Contains(new MinePosition(row,col)))
                {
                    lsMinePos.Add(minePos = new MinePosition(row, col));
                    mineLayout[row, col] = "X";
                    x++;
                }
            }
            foreach (MinePosition item in lsMinePos)
            {
                for (int i = item.row - 1; i <= item.row + 1; i++)
                {
                    if (i >= 0 && i < sizeRow)
                    {
                        for (int j = item.col - 1; j <= item.col + 1; j++)
                        {
                            if (j >= 0 && j < sizeCol)
                            {
                                string posText = mineLayout[i, j];
                                mineLayout[i, j] = posText == null ? "1" : posText != "X" ? (int.Parse(posText) + 1).ToString() : posText;

                            }
                        }
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
                    mine = tblPanel.GetControlFromPosition(j, i) as ucBtnMine;
                    mine.setBtn(mineLayout[i, j]);
                }
            }
            
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
            //mineLyt = shuffleMine();
            for (int i = 0; i < sizeRow; i++)
            {

                tblPanel.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
                for (int j = 0; j < sizeCol; j++)
                {
                    tblPanel.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
                    ucBtnMine mine = new ucBtnMine();
                    //mine.setBtn(mineLyt[i,j]);
                    mine.Click += new EventHandler(clickedMinefield);
                    mine.Dock = DockStyle.Fill;
                    tblPanel.Controls.Add(mine, j, i);
                    
                    //if (mine.mineSet)
                    //{
                    //    lsMines.Add(mine);
                    //}                
                }
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
            Console.WriteLine(mine.mouseClickID);
            if (firstClick)
            {
                firstClick = false;
                shuffleMine(mine);
                mine.mouseClickID = -1;
            }
            if (mine.mouseClickID == -1)
            {
                if (mine.clkState)
                {
                    mine.clickedMine();
                    if (mine.mineFlag)
                    {
                        lblMines.Text = (int.Parse(lblMines.Text) + 1).ToString(); 
                    }
                    clickCount--;
                    Console.WriteLine("Clicked.");
                    if (mine.mineSet)
                    {
                        foreach (ucBtnMine item in lsMines)
                        {
                            item.clickedMine();
                        }
                        resultDisp(false);
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
                mine.mouseClickID = 0;
            }
            else if (mine.mouseClickID == 1)
            {
                if (!mine.mineFlag && int.Parse(lblMines.Text) > 0)
                {
                    mine.toggleFlag();
                    lblMines.Text = (int.Parse(lblMines.Text) - 1).ToString();
                }
                else if(mine.mineFlag && int.Parse(lblMines.Text) <= numMines)
                {
                    mine.toggleFlag();
                    lblMines.Text = (int.Parse(lblMines.Text) + 1).ToString(); 
                }
                mine.mouseClickID = 0;
            }
            else if (mine.mouseClickID == 5)
            {

                openAdjacent(mine);
                mine.mouseClickID = 0;
            }
            
            
        }

        #region Check Functions
        private void openAdjacent(ucBtnMine midMine)
        {
            TableLayoutPanel tblPanel = pnlMain.Controls[0] as TableLayoutPanel;
            int mineCol = tblPanel.GetColumn(midMine);
            int mineRow = tblPanel.GetRow(midMine);

            for (int i = mineRow - 1; i <= mineRow + 1; i++)
            {
                if (i >= 0 && i < sizeRow)
                {
                    for (int j = mineCol - 1; j <= mineCol + 1; j++)
                    {
                        if (j >= 0 && j < sizeCol)
                        {
                            ucBtnMine btn = tblPanel.GetControlFromPosition(j, i) as ucBtnMine;
                            if (btn.clkState && !btn.mineFlag)
                            {
                                btn.clickedMine();
                                clickCount--;
                                if (btn.mineSet)
                                {
                                    foreach (ucBtnMine item in lsMines)
                                    {
                                        item.clickedMine();
                                    }
                                    resultDisp(false);
                                    pnlMain.Enabled = false;
                                }
                                else if (clickCount == 0)
                                {
                                    resultDisp(true);
                                }
                                else if (btn.mineTxt == " ")
                                {
                                    checkAdjacent(btn);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void checkAdjacent(ucBtnMine midMine)
        {
            
            Console.WriteLine("Checking..");
            TableLayoutPanel tblPanel = pnlMain.Controls[0] as TableLayoutPanel;
            int mineCol = tblPanel.GetColumn(midMine);
            int mineRow = tblPanel.GetRow(midMine);
            
            for (int i = mineRow - 1; i <= mineRow + 1; i++)
            {
                if (i >= 0 && i < sizeRow)
                {
                    for (int j = mineCol - 1; j <= mineCol + 1; j++)
                    {
                        if (j >= 0 && j < sizeCol)
                        {
                            checkEmpty(tblPanel.GetControlFromPosition(j, i) as ucBtnMine);
                        }
                    }
                }
            }
           
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
        #endregion

        #region Fn to Reset the Game, reshuffling mines
        private void resetMine()
        {
            resetTimer();
            if (lsMines.Count > 0)
            {
                lsMines.Clear();
            }
            lblMines.Text = numMines.ToString();
            clickCount = (sizeRow * sizeCol) - numMines;
            pnlMain.Enabled = true;
            TableLayoutPanel tblPanel = pnlMain.Controls[0] as TableLayoutPanel;
            for (int i = 0; i<sizeRow;i++)
            {
                for (int j = 0; j<sizeCol; j++){
                    ucBtnMine mine = tblPanel.GetControlFromPosition(j, i) as ucBtnMine;
                    mine.resetMine();
                }
            }
            firstClick = false;
        }
        #endregion

        #region Control functions to call forms
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

        #endregion

        #region Timer Control Functions
        private void timerGame_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = (int.Parse(lblTimer.Text) + 1).ToString();
        }

        private void resetTimer()
        {
            timerGame.Enabled = false;
            lblTimer.Text = "000";
        }
        #endregion

    }
}
