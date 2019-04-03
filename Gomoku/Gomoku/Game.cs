using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class Game
    {
        // 關於棋盤的操作
        private Board board = new Board();

        // 判斷黑白棋，先手黑棋
        // 以enum 判斷
        //private PieceType nextPieceType = PieceType.BLACK;
        // 現在選手
        private PieceType currentPlayer = PieceType.BLACK;

        // 供內部使用
        private PieceType winner = PieceType.NONE;
        // 外部只能看，不能改
        public PieceType Winner { get { return winner; } }


        public bool CanBePlace(int x,int y)
        {
            return board.CanBePlace(x, y);
        }

        // 放棋子處理，判斷勝負，交換選手
        public Piece PlaceAPiece(int x, int y)
        {
            Piece piece = board.PlaceAPiece(x, y, currentPlayer);
            if (piece != null)
            {
                // 檢查現在選手是否獲勝
                CheckWinner();

                // 交換選手
                if (currentPlayer == PieceType.BLACK)
                    currentPlayer = PieceType.WHITE;
                else if (currentPlayer == PieceType.WHITE)
                    currentPlayer = PieceType.BLACK;

                return piece;
            }
            return null;
        }

        // 清空棋盤，清除勝利者
        public void ClearBoard()
        {
            if (winner != PieceType.NONE)
            {
                board.ClearBoard();
                winner = PieceType.NONE;
            }
        }

        // 檢查勝利者
        private void CheckWinner()
        {
            int centerX = board.LastPlaceNode.X;
            int centerY = board.LastPlaceNode.Y;

            // 檢查八個不同方向
            for (int xDir = -1; xDir <= 1; xDir++)
            {
                for (int yDir = -1; yDir <= 1; yDir++)
                {
                    // 排除中間的情況
                    if (xDir == 0 && yDir == 0)
                        continue;
                    // 紀錄有幾棵相同棋子
                    int count = 1;
                    // 方向(正反)
                    int a = 1;
                    // 大小
                    int b = 1;
                    // 轉向(限一次)
                    bool c = true;


                    while (count < 5)
                    {
                        int targetX = centerX + a * b * xDir;
                        int targetY = centerY + a * b * yDir;

                        // 是否超出邊界
                        if (
                            targetX < 0 || targetX >= Board.NODE_COUNT ||
                            targetY < 0 || targetY >= Board.NODE_COUNT
                           )
                        {
                            break;
                        }
                        // 檢查顏色是否相同，
                        else if (board.GetPieceType(targetX, targetY) == currentPlayer)
                        {
                            b++;
                            count++;
                        }
                        // 轉向一次
                        else if (board.GetPieceType(targetX, targetY) != currentPlayer && c)
                        {
                            a = -1;
                            b = 1;
                            c = false;
                        }
                        else
                            break;

                    }

                    // 檢查是否有五棵棋子
                    if (count == 5)
                        winner = currentPlayer;
                }
            }
        }

    }
}
