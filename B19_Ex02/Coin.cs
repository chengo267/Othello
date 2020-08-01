using System;
using System.Collections.Generic;
using System.Text;

namespace B19_Ex02
{
    public class Coin
    {
        private int m_Row;
        private int m_Colum;

        public Coin()
        {
            m_Row = 0;
            m_Colum = 0;
        }

        public Coin(int i_Row, int i_Colum)
        {
            m_Row = i_Row;
            m_Colum = i_Colum;
        }

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Colum
        {
            get { return m_Colum; }
            set { m_Colum = value; }
        }

        public bool IsEqualPoints(Coin i_CoinToCheck)
        {
            return (m_Row == i_CoinToCheck.Row) && (m_Colum == i_CoinToCheck.Colum);
        }

        public void SetSquare(Player i_CurrentPlayer)
        {
            i_CurrentPlayer.PlayerBoard.OtheloBoardGame[m_Row, m_Colum] = i_CurrentPlayer.CoinType;
        }

        public void CleanSquare(Player i_CurrentPlayer)
        {
            i_CurrentPlayer.PlayerBoard.OtheloBoardGame[m_Row, m_Colum] = '\0';
        }
    }
}
