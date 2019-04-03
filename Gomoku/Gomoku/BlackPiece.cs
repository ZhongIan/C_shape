
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;



namespace Gomoku
{
    class BlackPiece : Piece
    {
        //新增黑棋 Image
        public BlackPiece(int x, int y) : base(x, y)
        {
            //新增黑棋，從Properties.Resources
            this.Image = Properties.Resources.black;

        }
        // 返回 Type
        public override PieceType GetPieceType()
        {
            return PieceType.BLACK;
        }
    }
}