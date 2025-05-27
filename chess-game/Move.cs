using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chess_game
{
    public partial class Program
    {
        /// <summary>
        /// Move a piece from Start square to End square
        /// </summary>
        /// <returns>If the move is legal</returns>
        public static bool Move(int startX, int startY, int endX, int endY, bool blackWhite, ref bool whiteCheck, ref bool blackCheck,ref bool whiteCheckmate, ref bool blackCheckmate,ref bool legal)
        {
            if (LegalMove(startX, startY, endX, endY, blackWhite, ref whiteCheck, ref blackCheck,ref whiteCheckmate,ref blackCheckmate) == false)
            {
                if (whiteCheck == false && blackCheck == false)
                {
                    Console.WriteLine("La mossa non è valida\n");
                    legal = false;
                }
                return false;
            }
            else
            {
                board[endX, endY] = board[startX, startY];
                board[startX, startY] = _;
                legal = true;
                return true;
            }
        }



        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startX">starting x position</param>
        /// <param name="startY">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <param name="blackWhite">False if the current move is made by black and True if it is made by white</param>
        /// <param name="whiteCheck">true if the white king is under check</param>
        /// <param name="blackCheck">true if the black king is under check</param>
        /// <param name="whiteCheckmate">true if the white king is under checkmate</param>
        /// <param name="blackCheckmate">true if the black king is under checkmate</param>
        /// <returns>if the move is legal</returns>
        static bool LegalMove(int startX, int startY, int endX, int endY, bool blackWhite, ref bool whiteCheck, ref bool blackCheck,ref bool whiteCheckmate,ref bool blackCheckmate)
        {
            int kingX = 0, kingY = 0;
            int reversMove = 0;


            //GENERAL CONTROLS
            //Finds the king
            FindKing(ref kingX, ref kingY, blackWhite);
            //Simulates the move to check if the king would be in check after the move
            reversMove = board[endX, endY];
            board[endX, endY] = board[startX, startY];
            board[startX, startY] = _;

            if (blackWhite == false)
            {
                if (Program.AttackedBy(kingX, kingY, true) == true)
                {
                    blackCheck = true;
                    return false;
                }
            }
            else
            {
                if (Program.AttackedBy(kingX, kingY, false) == true)
                {
                    whiteCheck = true;
                    return false;
                }
            }
            board[startX, startY] = board[endX, endY];
            board[endX, endY] = reversMove;

            //Checks if the piece moved is the right color
            if (blackWhite == false && (board[startX, startY] != BP && board[startX, startY] != BN && board[startX, startY] != BB && board[startX, startY] != BR && board[startX, startY] != BQ && board[startX, startY] != BK))
            {
                return false;
            }
            else if (blackWhite == true && (board[startX, startY] != WP && board[startX, startY] != WN && board[startX, startY] != WB && board[startX, startY] != WR && board[startX, startY] != WQ && board[startX, startY] != BK))
            {
                return false;
            }

            // Checks if either the starting or the ending position are inside the matrix
            if (startX < 0 || startX > 7 || startY < 0 || startY > 7)
            {
                return false;
            }
            if (endX < 0 || endX > 7 || endY < 0 || endY > 7)
            {
                return false;
            }

            //Checks if the moved piece is capturing a piece of the same color
            //Checks for white pieces if the ending square contains a white piece
            if (board[startX, startX] == WP || board[startX, startX] == WN || board[startX, startX] == WB || board[startX, startX] == WR || board[startX, startX] == WQ || board[startX, startX] == WK)
            {
                if (board[endX, endY] == WP && board[endX, endY] == WN && board[endX, endY] == WB && board[endX, endY] == WR && board[endX, endY] == WQ && board[endX, endY] == WK)
                {
                    return false;
                }
            }
            //Checks for black pieces if the ending square contains a black piece
            else if (board[startX, startX] == BP || board[startX, startX] == BN || board[startX, startX] == BB || board[startX, startX] == BR || board[startX, startX] == BQ || board[startX, startX] == BK)
            {
                if (board[endX, endY] == BP && board[endX, endY] == BN && board[endX, endY] == BB && board[endX, endY] == BR && board[endX, endY] == BQ && board[endX, endY] == BK)
                {
                    return false;
                }
            }

            //Checks if the piece has been moved
            if (startX == endX && startY == endY)
            {
                return false;
            }



            // Checks if the move is legal based on the type of piece
            switch (board[startX, startY])
            {
                case _:
                    return false;
                    break;


                //PAWNS
                //white pawn
                case WP:
                      return WhitePawn(startX, startY, endX, endY);
                    break;

                //BLACK PAWN
                //black pawn
                case BP:
                    return BlackPawn(startX, startY, endX, endY);
                    break;

                //KNIGHTS
                case WN:
                case BN:
                    return Knights(startX, startY, endX, endY);
                    break;

                //BISHOPS
                case WB:
                case BB:
                    return Bishops(startX, startY, endX, endY);
                    break;

                //ROOKS
                case WR:
                case BR:
                    return Rooks(startX, startY, endX, endY);
                    break;

                //QUEENS
                case WQ:
                case BQ:
                    return Queens(startX, startY, endX, endY);
                    break;

                //KINGS
                case WK:
                case BK:
                    return Queens(startX, startY, endX, endY);
                    break;

                //NOTHING
                default:
                    return false;
                    break;
            }

        }




        /// <summary>
        /// Finds the x and y of the king desired
        /// </summary>
        /// <param name="X">x position of the king</param>
        /// <param name="Y">y position of the king</param>
        /// <param name="blackWhite">False if the king is black and true if it is white</param>
        static void FindKing(ref int X, ref int Y, bool blackWhite)
        {
            //finds the playing king's position
            if (blackWhite == false)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i, j] == BK)
                        {
                            X = i;
                            Y = j;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i, j] == WK)
                        {
                            X = i;
                            Y = j;
                        }
                    }
                }
            }
        }



        /// <summary>
        /// Checks the legality of a move made by a white pawn
        /// </summary>
        /// <param name="startX">starting x position</param>
        /// <param name="startY">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool WhitePawn(int startX, int startY, int endX, int endY)
        {
            //Checks if the pawn is on the limits of the matrix
            if (startY == 0)
            {
                if (board[startX - 1, startY + 1] != _ && (endX == startX - 1 && endY == startY + 1))
                {
                    return true;
                }
            }
            else if (startY == 7)
            {
                if (board[startX - 1, startY - 1] != _ && (endX == startX - 1 && endY == startY - 1))
                {
                    return true;
                }
            }
            else
            {
                //Checks if the pawn can eat a piece and if the piece is a white one
                if (board[startX - 1, startY - 1] != _ && (endX == startX - 1 && endY == startY - 1))
                {
                    return true;
                }
                else if (board[startX - 1, startY + 1] != _ && (endX == startX - 1 && endY == startY + 1))
                {
                    //Checks if the pawn is on the starting square
                    if ((startX == 6 && board[5, startY] == _ && board[4, startY] == _) && (endX == 4 && endY == startY))
                    {
                        return true;
                    }
                    //Checks if te piece in front is occupied
                    else if ((endX == startX - 1 && endY == startY) && board[startX - 1, endY] != _)
                    {
                        return false;
                    }
                    else
                    {
                        //Checks everything else
                        if (endY != startY || endX >= startX || endX < startX - 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }



        /// <summary>
        /// Checks the legality of a move made by a black pawn
        /// </summary>
        /// <param name="startX">starting x position</param>
        /// <param name="startY">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool BlackPawn(int startX, int startY, int endX, int endY)
        {

            //Checks if the pawn is on the limits of the matrix
            if (startY == 0)
            {
                if (board[startX + 1, startY + 1] != _ && (endX == startX + 1 && endY == startY + 1))
                {
                    return true;
                }
            }
            else if (startY == 7)
            {
                if (board[startX + 1, startY - 1] != _ && (endX == startX + 1 && endY == startY - 1))
                { 
                    return true;
                }
            }
            else
            {
                //Checks if the pawn can eat a piece and if the piece is a white one
                if (board[startX + 1, startY - 1] != _ && (endX == startX + 1 && endY == startY - 1))
                { 
                    return true;
                }
                else if (board[startX + 1, startY + 1] != _ && (endX == startX + 1 && endY == startY + 1))
                { 
                    return true;
                }
            }
            //Checks if the pawn is on the starting square
            if ((startX == 1 && board[2, startY] == _ && board[3, startY] == _) && (endX == 3 && endY == startY))
            {
                return true;
            }
            //Checks if te piece in front is occupied
            else if ((endX == startX + 1 && endY == startY) && board[startX + 1, endY] != _)
            {
                return false;
            }
            else
            {
                //Checks everything else
                if (endY != startY || endX <= startX || endX > startX + 1)
                {
                    return false;
                }
            }
            return true;
        }




        /// <summary>
        /// Checks the legality of a move made by a knight
        /// </summary>
        /// <param name="startX">starting x position</param>
        /// <param name="startY">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Knights(int startX, int startY, int endX, int endY)
        {
            //Checks if the knight is at the limits of the matrix
            //Checks the first row
            if (startX == 0)
            {
                //Checks the first column
                if (startY == 0)
                {
                    if ((endX == startX + 2 && endY == startY + 1) || (endX == startX + 1 && endY == startY + 2))
                    {
                        return true;
                    }
                }
                //Checks the last column
                else if (startY == 7)
                {
                    if ((endX == startX + 2 && endY == startY - 1) || (endX == startX + 1 && endY == startY - 2))
                    {
                        return true;
                    }
                }
                //Checks the second column
                else if (startY == 1)
                {
                    if ((endX == startX + 2 && (endY == startY + 1 || endY == startY - 1)) || (endX == startX + 1 && endY == startY + 2))
                    {
                        return true;
                    }
                }
                //Checks the penultimate column
                else if (startY == 6)
                {
                    if ((endX == startX + 2 && (endY == startY - 1 || endY == startY + 1)) || (endX == startX + 1 && endY == startY - 2))
                    {
                        return true;
                    }
                }
            }
            //Checks the bottom row
            else if (startX == 7)
            {
                //Checks the first column
                if (startY == 0)
                {
                    if ((endX == startX - 2 && endY == startY + 1) || (endX == startX - 1 && endY == startY + 2))
                    {
                        return true;
                    }
                }
                //Checks the last column
                else if (startY == 7)
                {
                    if ((endX == startX - 2 && endY == startY - 1) || (endX == startX - 1 && endY == startY - 2))
                    {
                        return true;
                    }
                }
                //Checks the second column
                else if (startY == 1)
                {
                    if ((endX == startX - 2 && (endY == startY + 1 || endY == startY - 1)) || (endX == startX - 1 && endY == startY + 2))
                    {
                        return true;
                    }
                }
                //Checks the penultimate column
                else if (startY == 6)
                {
                    if ((endX == startX - 2 && (endY == startY - 1 || endY == startY + 1)) || (endX == startX - 1 && endY == startY - 2))
                    {
                        return true;
                    }
                }
            }
            //Checks the second row
            else if (startX == 1)
            {
                //Checks the first column
                if (startY == 0)
                {
                    if ((endX == startX + 2 && endY == startY + 1) || (endX == startX + 1 && endY == startY + 2) || (endX == startX - 1 && endY == startY + 2))
                    {
                        return true;
                    }
                }
                //Checks the last column
                else if (startY == 7)
                {
                    if ((endX == startX + 2 && endY == startY - 1) || (endX == startX + 1 && endY == startY - 2) || (endX == startX - 1 && endY == startY - 2))
                    {
                        return true;
                    }
                }
                //Checks the second column
                else if (startY == 1)
                {
                    if ((endX == startX + 2 && (endY == startY + 1 || endY == startY - 1)) || ((endX == startX + 1 || endX == startX - 1) && endY == startY + 2))
                    {
                        return true;
                    }
                }
                //Checks the penultimate column
                else if (startY == 6)
                {
                    if ((endX == startX + 2 && (endY == startY - 1 || endY == startY + 1)) || ((endX == startX + 1 || endX == startX - 1) && endY == startY - 2))
                    {
                        return true;
                    }
                }
            }
            //Checks the penultimate row
            else if (startX == 6)
            {
                //Checks the first column
                if (startY == 0)
                {
                    if ((endX == startX - 2 && endY == startY + 1) || (endX == startX - 1 && endY == startY + 2) || (endX == startX + 1 && endY == startY + 2))
                    {
                        return true;
                    }
                }
                //Checks the last column
                else if (startY == 7)
                {
                    if ((endX == startX - 2 && endY == startY - 1) || (endX == startX - 1 && endY == startY - 2) || (endX == startX + 1 && endY == startY - 2))
                    {
                        return true;
                    }
                }
                //Checks the second column
                else if (startY == 1)
                {
                    if ((endX == startX - 2 && (endY == startY + 1 || endY == startY - 1)) || ((endX == startX - 1 || endX == startX + 1) && endY == startY + 2))
                    {
                        return true;
                    }
                }
                //Checks the penultimate column
                else if (startY == 6)
                {
                    if ((endX == startX - 2 && (endY == startY - 1 || endY == startY + 1)) || ((endX == startX - 1 || endX == startX + 1) && endY == startY - 2))
                    {
                        return true;
                    }
                }
            }
            //Checks for knights that aren't at the limits
            else
            {
                if (endX == startX - 2 && (endY == startY - 1 || endY == startY + 1))
                {
                    return true;
                }
                else if (endX == startX + 2 && (endY == startY - 1 || endY == startY + 1))
                {
                    return true;
                }
                else if (endX == startX - 1 && (endY == startY - 2 || endY == startY + 2))
                {
                    return true;
                }
                else if (endX == startX + 1 && (endY == startY - 2 || endY == startY + 2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }



        /// <summary>
        /// Checks the legality of a move made by a bishop
        /// </summary>
        /// <param name="startX">starting x position</param>
        /// <param name="startY">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Bishops(int startX, int startY, int endX, int endY)
        {
            //Checks if the move is legal
            if (startX + startY == endX + endY || startX - startY == endX - endY)
            {
                //Checks if there is a piece between 
                for (int i = 1; i <= Math.Sqrt(((endX - startX) * (endX - startX)) + ((endY - startY) * (endY - startY))); i++)
                {
                    if (board[startX + i, startY + i] != _)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// Checks the legality of a move made by a white rook
        /// </summary>
        /// <param name="startX">starting x position</param>
        /// <param name="startY">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Rooks(int startX, int startY, int endX, int endY)
        {
            //Checks if the move is legal
            if (startX == endX && startY != endY)
            {
                //Checks if there is a piece between 
                for (int i = 1; i <= endY - startY; i++)
                {
                    if (board[endX, i] != 0)
                    {
                        return false;
                    }
                }
            }
            else if (startX != endX && startY == endY)
            {
                //Checks if there is a piece between 
                for (int i = 1; i <= endX - startX; i++)
                {
                    if (board[i, endY] != 0)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// Checks the legality of a move made by a queen
        /// </summary>
        /// <param name="startX">starting x position</param>
        /// <param name="startY">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Queens(int startX, int startY, int endX, int endY)
        {
            if ((startX + startY == endX + endY || startX - startY == endX - endY) || (startX == endX && startY != endY) || (startX != endX && startY == endY))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Checks the legality of a move made by a king
        /// </summary>
        /// <param name="startX">starting x position</param>
        /// <param name="startY">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Kings(int startX, int startY, int endX, int endY)
        {
            if (Math.Abs(endX - startX) > 1 || Math.Abs(endY - startY) > 1)
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// Check if a square is under attack
        /// </summary>
        /// <param name="white">The square is attacked by white</param>
        /// <returns>true if the square is attacked</returns>
        static bool attackedBy(int x, int y, bool white)
        {
            //BY PAWN

            //left
            if (white)
            {
                if (x > 0 && y < 7)
                {
                    if (board[y + 1, x - 1] == WP)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (x > 0 && y > 0)
                {
                    if (board[y - 1, x - 1] == BP)
                    {
                        return true;
                    }
                }
            }

            //right
            if (white)
            {
                if (x < 7 && y < 7)
                {
                    if (board[y + 1, x + 1] == WP)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (x < 7 && y > 0)
                {
                    if (board[y - 1, x + 1] == BP)
                    {
                        return true;
                    }
                }
            }

            //BY KNIGHT

            //top
            if (y > 1)
            {
                //left
                if (x > 0)
                {
                    if (white)
                    {
                        if (board[y - 2, x - 1] == WN)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 2, x - 1] == BN)
                        {
                            return true;
                        }
                    }
                }

                //right
                if (x < 7)
                {
                    if (white)
                    {
                        if (board[y - 2, x + 1] == WN)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 2, x + 1] == BN)
                        {
                            return true;
                        }
                    }
                }
            }

            //bottom
            if (y < 6)
            {
                //left
                if (x > 0)
                {
                    if (white)
                    {
                        if (board[y + 2, x - 1] == WN)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 2, x - 1] == BN)
                        {
                            return true;
                        }
                    }
                }

                //right
                if (x < 7)
                {
                    if (white)
                    {
                        if (board[y + 2, x + 1] == WN)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 2, x + 1] == BN)
                        {
                            return true;
                        }
                    }
                }
            }

            //left
            if (x > 1)
            {
                //top
                if (y > 0)
                {
                    if (white)
                    {
                        if (board[y - 1, x - 2] == WN)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 1, x - 2] == BN)
                        {
                            return true;
                        }
                    }
                }

                //bottom
                if (y < 7)
                {
                    if (white)
                    {
                        if (board[y + 1, x - 2] == WN)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 1, x - 2] == BN)
                        {
                            return true;
                        }
                    }
                }
            }

            //right
            if (x < 6)
            {
                //top
                if (y > 0)
                {
                    if (white)
                    {
                        if (board[y - 1, x + 2] == WN)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 1, x + 2] == BN)
                        {
                            return true;
                        }
                    }
                }

                //bottom
                if (y < 7)
                {
                    if (white)
                    {
                        if (board[y + 1, x + 2] == WN)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 1, x + 2] == BN)
                        {
                            return true;
                        }
                    }
                }
            }

            //VERTICALLY
            int pieceFound = _;

            int currentX = x;
            int currentY = y;

            //top
            do
            {
                currentY--;
                if (currentY < 0)
                {
                    break;
                }

                pieceFound = board[currentY, currentX];
            } while (pieceFound == _);

            if (pieceFound != _)
            {
                if (white)
                {
                    if (pieceFound == WR || pieceFound == WQ)
                    {
                        return true;
                    }
                }
                else
                {
                    if (pieceFound == BR || pieceFound == BQ)
                    {
                        return true;
                    }
                }
            }

            //bottom
            pieceFound = _;

            currentX = x;
            currentY = y;

            do
            {
                currentY++;
                if (currentY >= 8)
                {
                    break;
                }

                pieceFound = board[currentY, currentX];
            } while (pieceFound == _);

            if (pieceFound != _)
            {
                if (white)
                {
                    if (pieceFound == WR || pieceFound == WQ)
                    {
                        return true;
                    }
                }
                else
                {
                    if (pieceFound == BR || pieceFound == BQ)
                    {
                        return true;
                    }
                }
            }

            //HORIZONTALLY

            //left
            pieceFound = _;

            currentX = x;
            currentY = y;

            do
            {
                currentX--;
                if (currentX < 0)
                {
                    break;
                }

                pieceFound = board[currentY, currentX];
            } while (pieceFound == _);

            if (pieceFound != _)
            {
                if (white)
                {
                    if (pieceFound == WR || pieceFound == WQ)
                    {
                        return true;
                    }
                }
                else
                {
                    if (pieceFound == BR || pieceFound == BQ)
                    {
                        return true;
                    }
                }
            }

            //right
            pieceFound = _;

            currentX = x;
            currentY = y;

            do
            {
                currentX++;
                if (currentX >= 8)
                {
                    break;
                }

                pieceFound = board[currentY, currentX];
            } while (pieceFound == _);

            if (pieceFound != _)
            {
                if (white)
                {
                    if (pieceFound == WR || pieceFound == WQ)
                    {
                        return true;
                    }
                }
                else
                {
                    if (pieceFound == BR || pieceFound == BQ)
                    {
                        return true;
                    }
                }
            }

            //DIAGONALLY

            //top left
            pieceFound = _;

            currentX = x;
            currentY = y;

            do
            {
                currentX--;
                currentY--;
                if (currentX < 0 || currentY < 0)
                {
                    break;
                }

                pieceFound = board[currentY, currentX];
            } while (pieceFound == _);

            if (pieceFound != _)
            {
                if (white)
                {
                    if (pieceFound == WB || pieceFound == WQ)
                    {
                        return true;
                    }
                }
                else
                {
                    if (pieceFound == BB || pieceFound == BQ)
                    {
                        return true;
                    }
                }
            }

            //top right
            pieceFound = _;

            currentX = x;
            currentY = y;

            do
            {
                currentX++;
                currentY--;
                if (currentX >= 8 || currentY < 0)
                {
                    break;
                }

                pieceFound = board[currentY, currentX];
            } while (pieceFound == _);

            if (pieceFound != _)
            {
                if (white)
                {
                    if (pieceFound == WB || pieceFound == WQ)
                    {
                        return true;
                    }
                }
                else
                {
                    if (pieceFound == BB || pieceFound == BQ)
                    {
                        return true;
                    }
                }
            }

            //bottom left
            pieceFound = _;

            currentX = x;
            currentY = y;

            do
            {
                currentX--;
                currentY++;
                if (currentX < 0 || currentY >= 8)
                {
                    break;
                }

                pieceFound = board[currentY, currentX];
            } while (pieceFound == _);

            if (pieceFound != _)
            {
                if (white)
                {
                    if (pieceFound == WB || pieceFound == WQ)
                    {
                        return true;
                    }
                }
                else
                {
                    if (pieceFound == BB || pieceFound == BQ)
                    {
                        return true;
                    }
                }
            }

            //bottom right
            pieceFound = _;

            currentX = x;
            currentY = y;

            do
            {
                currentX++;
                currentY++;
                if (currentX >= 8 || currentY >= 8)
                {
                    break;
                }

                pieceFound = board[currentY, currentX];
            } while (pieceFound == _);

            if (pieceFound != _)
            {
                if (white)
                {
                    if (pieceFound == WB || pieceFound == WQ)
                    {
                        return true;
                    }
                }
                else
                {
                    if (pieceFound == BB || pieceFound == BQ)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

}
