
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// 為了使用 Point
using System.Drawing;

namespace Gomoku
{
    class Board
    {
        // 節點(Node)常數，以防操出邊界
        public static readonly int NODE_COUNT = 9;

        // 若無可放棋子(代表沒有找到符合的交叉點)
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);

        // 棋盤到邊界的距離
        private static readonly int OFFSET = 75;
        // 多靠近一個點會被判斷在"附近"
        private static readonly int NODE_RADIUS = 10;
        // 相鄰兩點(線)的距離
        private static readonly int NODE_DISTANCE = 75;

        // 棋盤格位 
        private Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT];

        // 供內部使用
        private Point lastPlaceNode = NO_MATCH_NODE;
        // 外部只能看，不能改
        public Point LastPlaceNode { get { return lastPlaceNode; } }

        // 清除棋盤
        public void ClearBoard()
        {      
           for(int i = 0; i < NODE_COUNT; i++)
            {
                for (int j = 0; j < NODE_COUNT; j++)
                {
                    if (pieces[i, j] != null)
                    {
                        // 釋放資源，如image
                        pieces[i, j].Dispose();
                        // 清除 BALCK WHITE
                        pieces[i, j] = null;
                    }
                }
            }
            
        }
        
        
        // 回傳 棋盤格位的 PieceType
        public PieceType GetPieceType(int nodeidX,int nodeidY)
        {
            if (pieces[nodeidX, nodeidY] == null)
                return PieceType.NONE;
            return pieces[nodeidX, nodeidY].GetPieceType(); 
        }

        // 判斷可否放棋子
        public bool CanBePlace(int x, int y)
        {
            // 找出最近的節點(Node)
            Point nodeid = FindTheCloseNode(x, y);

            // 如果沒有，回傳 false
            if (nodeid == NO_MATCH_NODE)
                return false;

            // 如果有，檢查是否有棋子
            if (pieces[nodeid.X, nodeid.Y] != null)
                return false;

            return true;
        }

        // 放棋子處理
        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            // 找出最近的節點(Node)
            Point nodeid = FindTheCloseNode(x, y);

            // 如果沒有，回傳 false
            if (nodeid == NO_MATCH_NODE)
                return null;

            // 如果有，檢查是否有棋子
            if (pieces[nodeid.X, nodeid.Y] != null)
                return null;

            // 根據 type 產生對應棋子
            Point formPos = convertToFormPosition(nodeid);
            if (type == PieceType.BLACK)
                pieces[nodeid.X, nodeid.Y] = new BlackPiece(formPos.X, formPos.Y);
            else if ((type == PieceType.WHITE))
                pieces[nodeid.X, nodeid.Y] = new WhitePiece(formPos.X, formPos.Y);

            // 紀錄最後下棋位置
            lastPlaceNode = nodeid;

            return pieces[nodeid.X, nodeid.Y];
        }

        // 轉換座標(格位->畫面座標)
        private Point convertToFormPosition(Point nodeid)
        {
            Point formPosition = new Point();

            formPosition.X = nodeid.X * NODE_DISTANCE + OFFSET;
            formPosition.Y = nodeid.Y * NODE_DISTANCE + OFFSET;

            return formPosition;
        }

        // 找出最近節點
        private Point FindTheCloseNode(int x,int y)
        {
            
            // 判斷X軸
            int nodeidX = FindTheCloseNode(x);
            //
            if (nodeidX == -1 || nodeidX >= NODE_COUNT)
                return NO_MATCH_NODE;
            // 判斷Y軸
            int nodeidY = FindTheCloseNode(y);
            //
            if (nodeidY == -1 || nodeidY >= NODE_COUNT)
                return NO_MATCH_NODE;
            // 都不是-1，則回傳
            return new Point(nodeidX, nodeidY);

        }

        // 先判斷一維
        private int FindTheCloseNode(int pos)
        {

            if (pos < NODE_DISTANCE - NODE_RADIUS)
                return -1;

            pos -= OFFSET;
            // 商，判斷第幾點
            int qutient = pos / NODE_DISTANCE;
            // 餘，判斷是否夠進
            int remainder = pos % NODE_DISTANCE;

            // 夠近
            if (remainder <= NODE_RADIUS)
                // 回傳第幾點
                return qutient;
            // 距下一點夠近
            else if (remainder > NODE_DISTANCE - NODE_RADIUS)
                // 回傳下一點
                return qutient + 1;
            else
                return -1;
        } 
    }
}