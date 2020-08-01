using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace B19_Ex02
{
    public class Othelo
    {
        private const int k_SmallBoardSize = 6;
        private const int k_BigBoardSize = 8;
        private Player m_CurrentPlayer;
        private BoardGame m_FullBoardGame = null;
        public Player m_XPlayer;
        public Player m_OPlayer;

        public char CurrentCoinPlayer
        {
            get { return m_CurrentPlayer.CoinType; }
            set { m_CurrentPlayer.CoinType = value; }
        }

        public Player XPlayer
        {
            get { return m_XPlayer; }
            set { m_XPlayer = value; }
        }

        public Player OPlayer
        {
            get { return m_OPlayer; }
            set { m_OPlayer = value; }
        }

        public Player CurrPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; }
        }

        public BoardGame FullBoardGame
        {
            get { return m_FullBoardGame; }
            set { m_FullBoardGame = value; }
        }

        public void CreateBoard(int i_Size)
        {
            m_FullBoardGame = new BoardGame(i_Size);
            m_FullBoardGame.BuildBoard(m_XPlayer.PlayerBoard.OtheloBoardGame,  m_OPlayer.PlayerBoard.OtheloBoardGame);
        }

        public void EndGame()
        {
            Environment.Exit(0);
        }
      
        public bool IsValidInput(string i_InputPlayer, bool i_IsSize)
        {
            bool o_ValidInput = true;
            int size = 0;

            if (i_IsSize == true)
            {
                if (!(int.TryParse(i_InputPlayer, out size) && (size == k_SmallBoardSize || size == k_BigBoardSize)))
                {
                    o_ValidInput = false;
                }
            }
            else
            {
                if (i_InputPlayer.Length != 3)
                {
                    o_ValidInput = false;
                }
                else
                {
                    if (!(char.IsDigit(i_InputPlayer[0]) || char.IsDigit(i_InputPlayer[2])) || i_InputPlayer[1] != ',')
                    {
                        o_ValidInput = false;
                    }
                }
            }

            return o_ValidInput;
        }

        public void SwitchTurn(Player i_CurrentPlayer)
        {
            if (i_CurrentPlayer.CoinType == m_FullBoardGame.XType)
            {
                m_CurrentPlayer = m_OPlayer;
            }
            else
            {
                m_CurrentPlayer = m_XPlayer;
            }
        }

        public void InitializePlayers(int i_Size, int i_NumOfPlayers, string i_NamePlayer1, string i_NamePlayer2)
        {
            m_XPlayer = new Player(i_NamePlayer1, 'X', i_Size);
            m_OPlayer = new Player('O', i_Size);

            if (i_NumOfPlayers == 2)
            {
                m_OPlayer.Name = i_NamePlayer2;
                m_OPlayer.PlayerType = ePlayerType.HUMAN;
            }

            m_CurrentPlayer = new Player('X', i_Size);
            m_CurrentPlayer = m_XPlayer;
        }
    }
}