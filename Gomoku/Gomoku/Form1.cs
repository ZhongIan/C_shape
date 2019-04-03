using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Form1 : Form
    {
        // 遊戲判斷
        private Game game = new Game();

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // 事件 MouseDown
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            Piece piece = game.PlaceAPiece(e.X, e.Y);
            if (piece != null)
            {
                this.Controls.Add(piece);

                // 檢查是剖有人獲勝
                if (game.Winner == PieceType.BLACK)
                {
                    MessageBox.Show("黑色獲勝");
                    game.ClearBoard();
                }
                else if (game.Winner == PieceType.WHITE)
                {
                    MessageBox.Show("白色獲勝");
                    game.ClearBoard();
                }
            }

        }

        // 事件 MouseMove
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // 若可放棋
            if (game.CanBePlace(e.X, e.Y))
            {
                // 游標變手
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
