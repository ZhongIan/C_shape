
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// 為使用 PictureBox
using System.Windows.Forms;
// 為使用 BackColor
using System.Drawing;


namespace Gomoku
{
    //abstract 抽象類別，防止創造出其他棋
    abstract class Piece : PictureBox
    {
        // static readonly 確保是常數
        private static readonly int IMAGE_WIDTH = 50;

        // 新增以下 BackColor Location Size
        public Piece(int x, int y)
        {
            this.BackColor = Color.Transparent;
            // 滑鼠點在圖片中心，預設為左上
            this.Location = new Point(x - IMAGE_WIDTH/2, y- IMAGE_WIDTH/2);
            this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH);

        }
        // 讓 Piece 回傳自己的 PieceType
        // abstract 在此先定義框架，讓繼承他的實作
        public abstract PieceType GetPieceType();
    
    }
}