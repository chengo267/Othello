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
    public partial class GameSettings : Form
    {
        private int m_BoardSize = 6;
        private bool m_IsAgainstComputer = true;

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public bool AgainstComputer
        {
            get { return m_IsAgainstComputer; }
        }

        public GameSettings() : base()
        {
            StartPosition = FormStartPosition.CenterParent;
        }

        private void PlayAgainstCompButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PlayAgainstFriendButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
            m_IsAgainstComputer = false;
        }

        private void BoardSizeButton_Click(object sender, EventArgs e)
        {
            if (m_BoardSize == 6)
            {
                BoardSizeButton.Text = "Board Size: 8X8 (click to increase)";
                m_BoardSize = 8;
            }
            else if (m_BoardSize == 8)
            {
                BoardSizeButton.Text = "Board Size: 10X10 (click to increase)";
                m_BoardSize = 10;
            }
            else if (m_BoardSize == 10)
            {
                BoardSizeButton.Text = "Board Size: 12X12 (click to increase)";
                m_BoardSize = 12;
            }
            else if (m_BoardSize == 12)
            {
                BoardSizeButton.Text = "Board Size: 6X6 (click to increase)";
                m_BoardSize = 6;
            }
        }

        private void GameSettings_Load(object sender, EventArgs e)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeComponent();
        }
    }
}
