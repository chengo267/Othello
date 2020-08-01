using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using B19_Ex02;

namespace Ex05.WinUI
{
    public partial class OtheloGame : Form
    {
        private OtheloSquerButton[,] m_ButtonMatrix;
        private GameSettings m_FormLogin = new GameSettings();
        private Othelo m_OtheloGame = new Othelo();
        private GroupBox m_GroupBoxForMatrix = new GroupBox();
        private int m_Turns = 1;

        public OtheloGame() : base()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
        }

        protected override void OnShown(EventArgs e)
        {
            m_FormLogin.ShowDialog();

            if (m_FormLogin.DialogResult == DialogResult.OK)
            {
                if(m_FormLogin.AgainstComputer)
                {
                    m_OtheloGame.InitializePlayers(m_FormLogin.BoardSize, 1, "Red", null);
                }
                else
                {
                    m_OtheloGame.InitializePlayers(m_FormLogin.BoardSize, 2, "Red", "Yellow");
                }

                Text = string.Format("{0}'s turn!", m_OtheloGame.CurrPlayer.Name);

                m_OtheloGame.CreateBoard(m_FormLogin.BoardSize);
                m_OtheloGame.CurrPlayer.CreatePossibleCoinToMove(m_OtheloGame.FullBoardGame);
                initializeOtheloBoard();
                
                base.OnShown(e);
            }
            else
            {
                Close();
            }
        }

        private void OtheloGame_Load(object sender, EventArgs e)
        {
        }

        private void initializeOtheloBoard()
        {
            m_ButtonMatrix = new OtheloSquerButton[m_FormLogin.BoardSize, m_FormLogin.BoardSize];
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            createOtheloButtonBoard();
            
            Size = new Size(30 + (40 * m_FormLogin.BoardSize), 50 + (40 * m_FormLogin.BoardSize));
            m_GroupBoxForMatrix.Size = new Size(40 * m_FormLogin.BoardSize, 40 * m_FormLogin.BoardSize);
            m_GroupBoxForMatrix.Location = new System.Drawing.Point(10, 10);
            SignPossibleMove(m_OtheloGame.CurrPlayer.PossibleCoin);
            Controls.Add(m_GroupBoxForMatrix);
        }

        private void createOtheloButtonBoard()
        {
            for (int i = 0; i < m_FormLogin.BoardSize; i++)
            {
                for (int j = 0; j < m_FormLogin.BoardSize; j++)
                {
                    m_ButtonMatrix[i, j] = new OtheloSquerButton(new Coin(i, j));

                    m_ButtonMatrix[i, j].Size = new Size(40, 40);
                    m_ButtonMatrix[i, j].Location = new System.Drawing.Point(40 * j, 40 * i);
                    m_ButtonMatrix[i, j].BorderStyle = BorderStyle.Fixed3D;
                    m_ButtonMatrix[i, j].Click += new EventHandler(OtheloSquerButton_Click);
                   
                    if((i == m_FormLogin.BoardSize / 2 && j == m_FormLogin.BoardSize / 2)
                        || (i == (m_FormLogin.BoardSize / 2) - 1 && j == (m_FormLogin.BoardSize / 2) - 1))
                    {
                        m_ButtonMatrix[i, j].Image = new Bitmap(@"C:\Temp\CoinYellow.png");
                        m_ButtonMatrix[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if((i == m_FormLogin.BoardSize / 2 && j == (m_FormLogin.BoardSize / 2) - 1) 
                        || (i == (m_FormLogin.BoardSize / 2) - 1 && j == m_FormLogin.BoardSize / 2))
                    {
                        m_ButtonMatrix[i, j].Image = new Bitmap(@"C:\Temp\CoinRed.png");
                        m_ButtonMatrix[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    m_GroupBoxForMatrix.Controls.Add(m_ButtonMatrix[i, j]);
                }
            }
         }

        private void OtheloSquerButton_Click(object sender, EventArgs e)
        {
            OtheloSquerButton choosenButton = sender as OtheloSquerButton; // אולי צריך להוסיף בדיקה האם ההמרה בוצעה - יש בכלל אפשרות ללחוץ על משהו שאינו כפתור?? 

            if (choosenButton.BackColor == Color.LawnGreen)
            {
                Coin choosenCoinByPlayer = new Coin();

                choosenCoinByPlayer.Row = choosenButton.OtheloCoin.Row;
                choosenCoinByPlayer.Colum = choosenButton.OtheloCoin.Colum;
                MoveAction(choosenCoinByPlayer);


                while (m_FormLogin.AgainstComputer && m_OtheloGame.CurrPlayer.PlayerType == ePlayerType.COMPUTER)
                {
                    MakeComputerMove();
                }
            }
            else
            {
                MessageBoxButtons button = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Invalid choice", "Error", button);
            }
        }

        public void SignPossibleMove(List<Coin> i_PossibleMoves)
        {
            foreach(Coin possition in i_PossibleMoves)
            {
                m_ButtonMatrix[possition.Row, possition.Colum].Enabled = true;
                m_ButtonMatrix[possition.Row, possition.Colum].BackColor = Color.LawnGreen;
                m_ButtonMatrix[possition.Row, possition.Colum].Click += new EventHandler(OtheloSquerButton_Click);
            }
        }

        public void MoveAction(Coin i_Coin)
        {
            List<Coin> coinsToFlip = new List<Coin>(2);

            i_Coin.SetSquare(m_OtheloGame.CurrPlayer);
            coinsToFlip = m_OtheloGame.CurrPlayer.PlayerBoard.SquersToFlip(m_OtheloGame.CurrPlayer, i_Coin, m_OtheloGame.FullBoardGame);
            m_OtheloGame.FullBoardGame.UpdateBoard(m_OtheloGame.m_XPlayer, m_OtheloGame.m_OPlayer, coinsToFlip);
           
            UpdateButtonMatrix();
            m_OtheloGame.SwitchTurn(m_OtheloGame.CurrPlayer);
            Text = string.Format("{0}'s turn!", m_OtheloGame.CurrPlayer.Name);
            CheckBoardGame();
           
            m_OtheloGame.CurrPlayer.CreatePossibleCoinToMove(m_OtheloGame.FullBoardGame);
            SignPossibleMove(m_OtheloGame.CurrPlayer.PossibleCoin);
        }

        public void MakeComputerMove()
        {
            Coin choosenCoinByComputer = new Coin();
            Random computerChoosing = new Random();

            int numOfPossibleCoins = m_OtheloGame.CurrPlayer.PossibleCoin.Count;
            choosenCoinByComputer = m_OtheloGame.CurrPlayer.PossibleCoin[computerChoosing.Next(0, numOfPossibleCoins)];
            MoveAction(choosenCoinByComputer);
        }

        public void UpdateButtonMatrix()
        {
            for (int i = 0; i < m_FormLogin.BoardSize; i++)
            {
                for (int j = 0; j < m_FormLogin.BoardSize; j++)
                {
                    if (m_OtheloGame.FullBoardGame.OtheloBoardGame[i, j] == 'X')
                    {
                        m_ButtonMatrix[i, j].Image = new Bitmap(@"C:\Temp\CoinRed.png");
                        m_ButtonMatrix[i, j].BackColor = Color.Empty;
                    }
                    else if (m_OtheloGame.FullBoardGame.OtheloBoardGame[i, j] == 'O')
                    {
                        m_ButtonMatrix[i, j].Image = new Bitmap(@"C:\Temp\CoinYellow.png");
                        m_ButtonMatrix[i, j].BackColor = Color.Empty;
                    }
                    else
                    {
                        m_ButtonMatrix[i, j].BackColor = Color.Empty;
                        m_ButtonMatrix[i, j].Enabled = false;
                    }

                    m_ButtonMatrix[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        public void CheckBoardGame()
        {
            if (m_OtheloGame.FullBoardGame.IsFull() || !(m_OtheloGame.OPlayer.CanMove(m_OtheloGame.FullBoardGame) || m_OtheloGame.XPlayer.CanMove(m_OtheloGame.FullBoardGame)))
            {
                GameOver();
            }

            if (!m_OtheloGame.CurrPlayer.CanMove(m_OtheloGame.FullBoardGame))
            {
                m_OtheloGame.SwitchTurn(m_OtheloGame.CurrPlayer);
                Text = string.Format("{0}'s turn", m_OtheloGame.CurrPlayer.Name);
            }
         }

        public void GameOver()
        {
            Player winner;
            Player loser;
            string title = "Othelo";
            string message;

            if (m_OtheloGame.OPlayer.NumOfCoins < m_OtheloGame.XPlayer.NumOfCoins)
            {
                winner = m_OtheloGame.XPlayer;
                loser = m_OtheloGame.OPlayer;
                winner.Winnings++;
                message = string.Format("{0} Won!! ({1}/{2}) ({3}/{4}) \nWould you like another round?", winner.Name, winner.NumOfCoins, loser.NumOfCoins, winner.Winnings, m_Turns);
            }
            else if (m_OtheloGame.OPlayer.NumOfCoins > m_OtheloGame.XPlayer.NumOfCoins)
            {
                winner = m_OtheloGame.OPlayer;
                loser = m_OtheloGame.XPlayer;
                winner.Winnings++;
                message = string.Format("{0} Won!! ({1}/{2}) ({3}/{4}) \nWould you like another round?", winner.Name, winner.NumOfCoins, loser.NumOfCoins, winner.Winnings, m_Turns);
            }
            else
            {
                m_OtheloGame.OPlayer.Winnings++;
                m_OtheloGame.XPlayer.Winnings++;
                message = "It's a Tie!!";
            }

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                m_Turns++;
                ResetGame();
            }
            else
            {
                m_OtheloGame.EndGame();
            }
        }

        public void ResetGame()
        {
            int size = m_FormLogin.BoardSize;

            m_OtheloGame.OPlayer.PlayerBoard.ClearBoard();
            m_OtheloGame.XPlayer.PlayerBoard.ClearBoard();

            m_OtheloGame.OPlayer.NumOfCoins = 2;
            m_OtheloGame.XPlayer.NumOfCoins = 2;

            m_OtheloGame.OPlayer.PlayerBoard.OtheloBoardGame[size / 2, size / 2] = m_OtheloGame.OPlayer.PlayerBoard.OtheloBoardGame[(size / 2) - 1, (size / 2) - 1] = 'O';
            m_OtheloGame.XPlayer.PlayerBoard.OtheloBoardGame[size / 2, (size / 2) - 1] = m_OtheloGame.XPlayer.PlayerBoard.OtheloBoardGame[(size / 2) - 1, size / 2] = 'X';

            m_OtheloGame.FullBoardGame.BuildBoard(m_OtheloGame.XPlayer.PlayerBoard.OtheloBoardGame, m_OtheloGame.OPlayer.PlayerBoard.OtheloBoardGame);
            ResetMatrix();
            UpdateButtonMatrix();
        }

        public void ResetMatrix()
        {
            for (int i = 0; i < m_FormLogin.BoardSize; i++)
            {
                for (int j = 0; j < m_FormLogin.BoardSize; j++)
                {
                    m_ButtonMatrix[i, j].Image = null;
                    m_ButtonMatrix[i, j].BackColor = Color.Empty;
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }
    }
}
