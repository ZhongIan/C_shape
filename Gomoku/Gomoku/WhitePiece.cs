
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gomoku
{
    class WhitePiece : Piece
    {
        //新增白棋 Image
        public WhitePiece(int x, int y) : base(x, y)
        {
            //新增白棋，從Properties.Resources
            this.Image = Properties.Resources.white;

        }
        // 返回 Type
        public override PieceType GetPieceType()
        {
            return PieceType.WHITE;
        }

    }
}