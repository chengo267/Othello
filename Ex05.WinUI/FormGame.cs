using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B19_Ex02;

namespace Ex05.WinUI
{
    partial class FormGame : Form
    {
        private Button[][] m_ButtonMatrix;
        private bool m_IsLoggedIn = false;
        private GameSettings m_FormLogin = new GameSettings();
        private Player m_Player1;
        private Player m_Player2;
        private BoardGame m_BoardGame;

        public FormGame()
        {
            InitializeComponent();
        }


        private void initilizeGameParams()
        {
            m_FormLogin.Close();
            m_Player1 = new Player(m_FormLogin.Player1, true);
            m_Player1.SetColor((char)eCheckerGame.WhitePlayer);
            m_Player2 = new Player(m_FormLogin.Player2, gameSettingsForm.IsPlayer2Human);
            m_Player2.SetColor((char)eCheckerGame.BlackPlayer);
            m_BoardGame = new BoardGame(m_FormLogin.BoardSize);

            initializeBoardDamka();
            updateGameDetails();
            m_Board.ResetGameBoard();
        }


    }
}
