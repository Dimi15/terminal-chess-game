using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game
{
    public partial class Program
    {
        /// <summary>
        /// Move a piece from Start square to End square
        /// </summary>
        public static void Move(int startX, int startY, int endX, int endY)
        {
            board[endX, endY] = board[startX, startY];
            board[startX, startY] = _;
        }

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <returns>If the move is legal</returns>
        static bool LegalMove(int startX, int startY, int endX, int endY)
        {
            // Checks if it is inside the matrix
            if (startX < 0 || startX > 7 || startY < 0 || startY > 7)
                return false;

            if (endX < 0 || endX > 7 || endY < 0 || endY > 7)
                return false;


            // Checks if the move is legal based on the type of piece

            switch(board[startX, startY])
            {
                case _:
                    return false; 
                    break;

                    //WHITE PAWN
                case WP:
                    //Checks if the ending square contains a white piece
                    if (board[endX, endY] != WP && board[endX, endY] != WN && board[endX, endY] != WB && board[endX, endY] != WR && board[endX, endY] != WQ && board[endX, endY] != WK)
                    {
                       return false;
                    }
                    //Checks if the pawn can eat a piece and if the piece is a white one
                    if (board[startX + 1, startY - 1] != _ && (endX == startX + 1 && endY == startY - 1))
                    { }
                    else if (board[startX + 1, startY + 1] != _ && (endX == startX + 1 && endY == startY - 1))
                    { }
                    else
                    {
                        //Checks if the pawn is on the starting square
                        if ((startX == 6 && board[5, startY] != _ && startX == 6 && board[4, startY] != _) && (endX == 4 && endY == startY))
                        { }
                        else
                        {

                            //Checks everything else
                            if(endY != startY || endX >= startX || endX < startX - 1)
                            {
                                return false;
                            }
                        }
                    }
                    break;

                    //BLACK PAWN
                case BP:
                    //Checks if the ending square contains a black piece
                    if(board[endX, endY] != BP && board[endX, endY] != BN && board[endX, endY] != BB && board[endX, endY] != BR && board[endX, endY] != BQ && board[endX, endY] != BK)
                    {
                        return false;
                    }

                    //Checks if the pawn can eat a piece and if the piece is a white one
                    if (board[startX - 1, startY - 1] != _ && (endX == startX - 1 && endY == startY - 1))
                    { }
                    else if (board[startX - 1, startY + 1] != _ && (endX == startX - 1 && endY == startY - 1))
                    { }
                    else
                    {

                        //Checks if the pawn is on the starting square
                        if ((startX == 1 && board[2, startY] != _ && startX == 1 && board[3, startY] != _) && (endX == 3 && endY == startY))
                        { }
                        else
                        {

                            //Checks everything else
                            if (endY != startY || endX <= startX || endX > startX + 1)
                            {
                                return false;
                            }
                        }
                    }
                    break;

                    //KNIGHTS
                case WN, BN:
                    //Checks if the ending position of the knight is legal
                    if(endX == startX - 2)
                    {
                        if(endY == startY - 1  endY == startY + 1)
                    }
                    else if (endX == startX + 2)
                    {
                        if (endY == startY - 1 || endY == startY + 1)
                    }
                    else if (endX == startX - 1)
                    {
                        if (endY == startY - 2 || endY == startY + 2)
                    }
                    else if (endX == startX + 1)
                    {
                        if (endY == startY - 2 || endY == startY + 2)
                    }
                    else
                    {
                        //Checks for white knights if the ending square contains a white piece
                        if (board[startX,startY]==WN && (board[endX, endY] != WP && board[endX, endY] != WN && board[endX, endY] != WB && board[endX, endY] != WR && board[endX, endY] != WQ && board[endX, endY] != WK))
                        {
                            return false;
                        }
                        //Checks for black knights if the ending square contains a black piece
                        else if (board[startX, startY] == BN && (board[endX, endY] != BP && board[endX, endY] != BN && board[endX, endY] != BB && board[endX, endY] != BR && board[endX, endY] != BQ && board[endX, endY] != BK))
                        {
                            return false;
                        }
                    }
                    break;






            } 
        }
    }
}
