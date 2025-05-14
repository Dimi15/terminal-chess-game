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
        public static void Move(int startX, int startY, int endX, int endY, ref bool l)
        {
            if (LegalMove(startX, startY, endX, endY) == false)
            {
                Console.WriteLine("La mossa non è valida\n");
                l = false;
            }
            else
            {
                board[endX, endY] = board[startX, startY];
                board[startX, startY] = _;
            }
        }

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <returns>If the move is legal</returns>
        static bool LegalMove(int startX, int startY, int endX, int endY)
        {
            bool White = false;


            // Checks if it is inside the matrix
            if (startX < 0 || startX > 7 || startY < 0 || startY > 7)
                return false;

            if (endX < 0 || endX > 7 || endY < 0 || endY > 7)
                return false;



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
                case @_:
                    return false;


                //PAWNS
                //white pawn
                case WP:

                    // Checks if the pawn is at the limit of the matrix
                    if (startY == 7 || startY == 0)
                    {
                        if (board[startX - 1, startY] != _ && (endX == startX - 1 && endY == startY - 1))
                        { }
                        else if (board[startX - 1, startY] != _ && (endX == startX - 1 && endY == startY - 1))
                        { }
                        else
                        {
                            //Checks if the pawn is on the starting square
                            if ((startX == 1 && board[2, startY] != _ && startX == 1 && board[3, startY] != _) && (endX == 3 && endY == startY))
                            { }
                            else
                            {
                                //Checks everything else
                                if (endY != startY || endX >= startX || endX > startX + 1)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        //Checks if the pawn can eat a piece
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
                                if (endY != startY || endX >= startX || endX < startX - 1)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    break;

                //black pawn
                case BP:
                    
                    // Checks if the pawn is at the limit of the matrix
                    if (startY == 7 || startY == 0)
                    {
                        if (board[startX - 1, startY] != _ && (endX == startX - 1 && endY == startY - 1))
                        { }
                        else if (board[startX - 1, startY] != _ && (endX == startX - 1 && endY == startY - 1))
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
                    }
                    else
                    {
                        //Checks if the pawn can eat a piece
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
                    }
                    break;

                //KNIGHTS
                case WN:
                case BN:
                    //Checks if the ending position of the knight is legal
                    if (endX == startX - 2)
                    {
                        if (endY == startY - 1 || endY == startY + 1)
                        { }
                    }
                    else if (endX == startX + 2)
                    {
                        if (endY == startY - 1 || endY == startY + 1)
                        { }
                    }
                    else if (endX == startX - 1)
                    {
                        if (endY == startY - 2 || endY == startY + 2)
                        { }
                    }
                    else if (endX == startX + 1)
                    {
                        if (endY == startY - 2 || endY == startY + 2)
                        { }
                    }
                    else
                    {
                        return false;
                    }
                    break;

                //BISHOPS
                case WB:
                case BB:
                    //Checks if the move is legal
                    if (startX + startY == endX + endY || startX - startY == endX - endY)
                    {
                        for (int i = 1; i <= Math.Sqrt(((endX - startX) * (endX - startX)) + ((endY - startY) * (endY - startY))); i++)
                        {
                            if (board[startX + i, startY + i] != 0)
                            {
                                return false;
                            }
                        }

                    }
                    else
                    {
                        return false;
                    }
                    break;

                //ROOKS
                case WR:
                case BR:
                    //Checks if the move is legal
                    if (startX == endX && startY != endY)
                    {
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
                    break;

                //QUEENS
                case WQ:
                case BQ:
                    if ((startX + startY == endX + endY || startX - startY == endX - endY) || (startX == endX && startY != endY) || (startX != endX && startY == endY))
                    { }
                    else
                    {
                        return false;
                    }
                    break;

                //KINGS
                //white king
                case WK:
                    if (attackedBy(endX, endY, true) == true)
                    {
                        return false;
                    }
                    break;
                //black king
                case BK:
                    if (attackedBy(endX, endY, false) == true)
                    {
                        return false;
                    }
                    break;


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
