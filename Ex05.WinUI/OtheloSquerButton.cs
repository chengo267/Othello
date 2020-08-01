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
    public partial class OtheloSquerButton : PictureBox
    {
        private readonly Coin r_OtheloCoin;

        public OtheloSquerButton(Coin i_OtheloCoin) : base()
        {
            r_OtheloCoin = i_OtheloCoin;
            Text = string.Empty;
            Enabled = false;
            BackgroundImage = null;
        }

        public Coin OtheloCoin
        {
            get { return r_OtheloCoin; }
        }
    }
}
