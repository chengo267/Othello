using System;
using System.Collections.Generic;
using System.Text;

namespace B19_Ex02
{
    public enum ePlayerType
    {
        HUMAN = 0,
        COMPUTER = 1,
    }

    // $G$ CSS-999 (-5) Every Class/Enum which is not nested should be in a separate file.
    public class Player
    {
        private string m_Name;
        private char m_CoinType;
        private ePlayerType m_PlayerType;
        private BoardGame m_PlayerBoard;
        private int m_NumOfCoins;
        private List<Coin> m_PossibleCoin = new List<Coin>(2);
        private int m_Winnings = 0;

        public Player(char coinType, int boardSize)
        {
            m_Name = "Yellow";
            m_NumOfCoins = 2;
            m_CoinType = coinType;
            m_PlayerBoard = new BoardGame(boardSize, m_CoinType);
            PlayerType = ePlayerType.COMPUTER;
        }

        public Player(string name, char coinType, int boardSize)
        {
            m_Name = name;
            m_NumOfCoins = 2;
            m_CoinType = coinType;
            m_PlayerBoard = new BoardGame(boardSize, m_CoinType);
            PlayerType = ePlayerType.HUMAN;
        }

        public int Winnings
        {
            get { return m_Winnings; }
            set { m_Winnings = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public BoardGame PlayerBoard
        {
            get { return m_PlayerBoard; }
            set { m_PlayerBoard = value; }
        }

        public ePlayerType PlayerType
        {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        public char CoinType
        {
            get { return m_CoinType; }
            set { m_CoinType = value; }
        }

        public int NumOfCoins
        {
            get { return m_NumOfCoins; }
            set { m_NumOfCoins = value; }
        }

        public List<Coin> PossibleCoin
        {
            get { return m_PossibleCoin; }
            set { m_PossibleCoin = value; }
        }

        public bool CanMove(BoardGame i_FullBoardGame)
        {
            Coin optionCoin = new Coin();
            bool o_CanMove = false;

            for (int i = 0; i < this.m_PlayerBoard.Size && o_CanMove == false; i++)
            {
                optionCoin.Row = i;

                for (int j = 0; j < this.m_PlayerBoard.Size && o_CanMove == false; j++)
                {
                    optionCoin.Colum = j;
                    if (this.m_PlayerBoard.OptionToMove(optionCoin, this, i_FullBoardGame) > 0)
                    {
                        o_CanMove = true;
                    }
                }
            }

            return o_CanMove;
        }

        public void CreatePossibleCoinToMove(BoardGame i_FullBoardGame)
        {
            m_PossibleCoin.Clear();
            Coin coinToCheck = new Coin();
            Coin optionCoin;
            for (int i = 0; i < m_PlayerBoard.Size; i++)
            {
                coinToCheck.Row = i;

                for (int j = 0; j < m_PlayerBoard.Size; j++)
                {
                    coinToCheck.Colum = j;
                    if (m_PlayerBoard.OptionToMove(coinToCheck, this, i_FullBoardGame) > 0)
                    {
                        optionCoin = new Coin(coinToCheck.Row, coinToCheck.Colum);
                        m_PossibleCoin.Add(optionCoin);
                    }
                }
            }
        }
    }
}
