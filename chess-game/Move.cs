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
        public static bool Move(int startX, int startY, int endX, int endY, bool blackWhite)
        {
            if (LegalMove(startX, startY, endX, endY, blackWhite))
            {
                board[endY, endX] = board[startY, startX];
                board[startY, startX] = _;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endY">ending x position</param>
        /// <param name="endX">ending y position</param>
        /// <param name="blackWhite">False if the current move is made by black and True if it is made by white</param>
        /// <returns>if the move is legal</returns>
        static bool LegalMove(int startX, int startY, int endX, int endY, bool blackWhite)
        {
            int kingX = 0, kingY = 0;
            int reversMove = 0;

            // GENERAL CONTROLS
            // Finds the king
            FindKing(ref kingX, ref kingY, blackWhite);

            // Simulates the move to check if the king would be in check after the move
            reversMove = board[endX, endY];
            board[endX, endY] = board[startY, startX];
            board[startY, startX] = _;

            if (blackWhite == false)
            {
                if (Program.AttackedBy(kingX, kingY, true))
                {
                    return false;
                }
            }
            else
            {
                if (Program.AttackedBy(kingX, kingY, false))
                {
                    return false;
                }
            }

            board[startY, startX] = board[endX, endY];
            board[endX, endY] = reversMove;

            // Checks if the piece moved is the right color
            if (!blackWhite && (board[startY, startX] == WP || board[startY, startX] == WN ||
                                        board[startY, startX] == WB || board[startY, startX] == WR ||
                                        board[startY, startX] == WQ || board[startY, startX] == WK))
            {
                return false;
            }
            else if (blackWhite && (board[startY, startX] == BP || board[startY, startX] == BN ||
                                    board[startY, startX] == BB || board[startY, startX] == BR ||
                                    board[startY, startX] == BQ || board[startY, startX] == BK))           
            {
                return false;
            }

            // Checks if either the starting or the ending position are inside the matrix
            if (startY < 0 || startY > 7 || startX < 0 || startX > 7)
            {
                return false;
            }

            if (endY < 0 || endY > 7 || endX < 0 || endX > 7)
            {
                return false;
            }

                

            // Checks if the moved piece is capturing a piece of the same color
            // Checks for white pieces if the ending square contains a white piece
            if (board[startY, startY] == WP || board[startY, startY] == WN || board[startY, startY] == WB ||
                board[startY, startY] == WR || board[startY, startY] == WQ || board[startY, startY] == WK)
            {
                if (board[endY, endX] == WP && board[endY, endX] == WN && board[endY, endX] == WB &&
                    board[endY, endX] == WR && board[endY, endX] == WQ && board[endY, endX] == WK)
                {
                    return false;
                }
            }
            // Checks for black pieces if the ending square contains a black piece
            else if (board[startY, startY] == BP || board[startY, startY] == BN || board[startY, startY] == BB ||
                     board[startY, startY] == BR || board[startY, startY] == BQ || board[startY, startY] == BK)
            {
                if (board[endY, endX] == BP && board[endY, endX] == BN && board[endY, endX] == BB &&
                    board[endY, endX] == BR && board[endY, endX] == BQ && board[endY, endX] == BK)
                {
                    return false;
                }
            }

            // Checks if the piece has been moved
            if (startY == endY && startX == endX)
            {
                return false;
            }

            // Checks if the move is legal based on the type of piece
            switch (board[startY, startX])
            {
                // EMPTY
                case _: return false;

                // PAWNS
                case WP: return WhitePawn(startX, startY, endX, endY);
                case BP: return BlackPawn(startX, startY, endX, endY);

                // KNIGHTS
                case WN:
                case BN: return Knights(startX, startY, endX, endY);

                // BISHOPS
                case WB:
                case BB: return Bishops(startX, startY, endX, endY);

                // ROOKS
                case WR:
                case BR: return Rooks(startX, startY, endX, endY);

                // QUEENS
                case WQ:
                case BQ: return Queens(startX, startY, endX, endY);

                // KINGS
                case WK:
                case BK: return Kings(startX, startY, endX, endY);

                // NOTHING
                default: return false;
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
            // Finds the playing king's position
            if (blackWhite == false)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i, j] == BK)
                        {
                            X = j;
                            Y = i;
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
                            X = j;
                            Y = i;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks the legality of a move made by a white pawn
        /// </summary>
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endY">ending x position</param>
        /// <param name="endX">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool WhitePawn(int startX, int startY, int endX, int endY)
        {
            // Checks if the pawn is on the limits of the matrix
            if (startX == 0)
            {
                if (board[startY - 1, startX + 1] != _ && (endY == startY - 1 && endX == startX + 1))
                {
                    return true;
                }
            }
            else if (startX == 7)
            {
                if (board[startY - 1, startX - 1] != _ && (endY == startY - 1 && endX == startX - 1))
                {
                    return true;
                }
            }
            else
            {
                // Checks if the pawn can eat a piece and if the piece is a white one
                if (board[startY, startX - 1] != _ && (endY == startY - 1 && endX == startX - 1))
                {
                    return true;
                }
                else if (board[startY, startX + 1] != _ && (endY == startY - 1 && endX == startX + 1))
                {
                    return true;
                }
            }

            // Checks if the pawn is on the starting square
            if ((startY == 6 && board[5, startX] == _ && board[4, startX] == _) && (endY == 4 && endX == startX))
            {
                return true;
            }
            // Checks if the piece in front is occupied
            else if ((endY == startY - 1 && endX == startX) && board[startY - 1, endX] != _)
            {
                return false;
            }
            else
            {
                //
                if (endX != startX)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the legality of a move made by a black pawn
        /// </summary>
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endY">ending x position</param>
        /// <param name="endX">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool BlackPawn(int startX, int startY, int endX, int endY)
        {
            // Checks if the pawn is on the limits of the matrix
            if (startX == 0)
            {
                if (board[startY + 1, startX + 1] != _ && (endY == startY + 1 && endX == startX + 1))
                {
                    return true;
                }
            }
            else if (startX == 7)
            {
                if (board[startY + 1, startX - 1] != _ && (endY == startY + 1 && endX == startX - 1))
                {
                    return true;
                }
            }
            else
            {
                // Checks if the pawn can capture a piece and if the piece is a white one
                if (board[startY + 1, startX - 1] != _ && (endY == startY + 1 && endX == startX - 1))
                {
                    return true;
                }
                else if (board[startY + 1, startX + 1] != _ && (endY == startY + 1 && endX == startX + 1))
                {
                    return true;
                }
            }

            // Checks if the pawn is on the starting square
            if ((startY == 1 && board[2, startX] == _ && board[3, startX] == _) && (endY == 3 && endX == startX))
            {
                return true;
            }
            // Checks if the square in front is occupied
            else if ((endY == startY + 1 && endX == startX) && board[startY + 1, endX] != _)
            {
                return false;
            }
            else
            {
                // Checks everything else
                if (endX != startX || endY <= startY || endY > startY + 1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks the legality of a move made by a knight
        /// </summary>
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endY">ending x position</param>
        /// <param name="endX">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Knights(int startX, int startY, int endX, int endY)
        {
            // Checks if the knight is at the limits of the matrix
            // Checks the first row
            if (startY == 0)
            {
                // Checks the first column
                if (startX == 0)
                {
                    if ((endY == startY + 2 && endX == startX + 1) || (endY == startY + 1 && endX == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the last column
                else if (startX == 7)
                {
                    if ((endY == startY + 2 && endX == startX - 1) || (endY == startY + 1 && endX == startX - 2))
                    {
                        return true;
                    }
                }
                // Checks the second column
                else if (startX == 1)
                {
                    if ((endY == startY + 2 && (endX == startX + 1 || endX == startX - 1)) ||
                        (endY == startY + 1 && endX == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the penultimate column
                else if (startX == 6)
                {
                    if ((endY == startY + 2 && (endX == startX - 1 || endX == startX + 1)) ||
                        (endY == startY + 1 && endX == startX - 2))
                    {
                        return true;
                    }
                }
            }
            // Checks the bottom row
            else if (startY == 7)
            {
                // Checks the first column
                if (startX == 0)
                {
                    if ((endY == startY - 2 && endX == startX + 1) || (endY == startY - 1 && endX == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the last column
                else if (startX == 7)
                {
                    if ((endY == startY - 2 && endX == startX - 1) || (endY == startY - 1 && endX == startX - 2))
                    {
                        return true;
                    }
                }
                // Checks the second column
                else if (startX == 1)
                {
                    if ((endY == startY - 2 && (endX == startX + 1 || endX == startX - 1)) ||
                        (endY == startY - 1 && endX == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the penultimate column
                else if (startX == 6)
                {
                    if ((endY == startY - 2 && (endX == startX - 1 || endX == startX + 1)) ||
                        (endY == startY - 1 && endX == startX - 2))
                    {
                        return true;
                    }
                }
            }
            // Checks the second row
            else if (startY == 1)
            {
                // Checks the first column
                if (startX == 0)
                {
                    if ((endY == startY + 2 && endX == startX + 1) || (endY == startY + 1 && endX == startX + 2) ||
                        (endY == startY - 1 && endX == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the last column
                else if (startX == 7)
                {
                    if ((endY == startY + 2 && endX == startX - 1) || (endY == startY + 1 && endX == startX - 2) ||
                        (endY == startY - 1 && endX == startX - 2))
                    {
                        return true;
                    }
                }
                // Checks the second column
                else if (startX == 1)
                {
                    if ((endY == startY + 2 && (endX == startX + 1 || endX == startX - 1)) ||
                        ((endY == startY + 1 || endY == startY - 1) && endX == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the penultimate column
                else if (startX == 6)
                {
                    if ((endY == startY + 2 && (endX == startX - 1 || endX == startX + 1)) ||
                        ((endY == startY + 1 || endY == startY - 1) && endX == startX - 2))
                    {
                        return true;
                    }
                }
            }
            // Checks the penultimate row
            else if (startY == 6)
            {
                // Checks the first column
                if (startX == 0)
                {
                    if ((endY == startY - 2 && endX == startX + 1) || (endY == startY - 1 && endX == startX + 2) ||
                        (endY == startY + 1 && endX == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the last column
                else if (startX == 7)
                {
                    if ((endY == startY - 2 && endX == startX - 1) || (endY == startY - 1 && endX == startX - 2) ||
                        (endY == startY + 1 && endX == startX - 2))
                    {
                        return true;
                    }
                }
                // Checks the second column
                else if (startX == 1)
                {
                    if ((endY == startY - 2 && (endX == startX + 1 || endX == startX - 1)) ||
                        ((endY == startY - 1 || endY == startY + 1) && endX == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the penultimate column
                else if (startX == 6)
                {
                    if ((endY == startY - 2 && (endX == startX - 1 || endX == startX + 1)) ||
                        ((endY == startY - 1 || endY == startY + 1) && endX == startX - 2))
                    {
                        return true;
                    }
                }
            }
            // Checks for knights that aren't at the limits
            else
            {
                if (endY == startY - 2 && (endX == startX - 1 || endX == startX + 1))
                {
                    return true;
                }
                else if (endY == startY + 2 && (endX == startX - 1 || endX == startX + 1))
                {
                    return true;
                }
                else if (endY == startY - 1 && (endX == startX - 2 || endX == startX + 2))
                {
                    return true;
                }
                else if (endY == startY + 1 && (endX == startX - 2 || endX == startX + 2))
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
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endY">ending x position</param>
        /// <param name="endX">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Bishops(int startX, int startY, int endX, int endY)
        {
            // Checks if the move is legal
            if (startY + startX == endY + endX || startY - startX == endY - endX)
            {
                // Checks if there is a piece between 
                for (int i = 1; i <= Math.Sqrt(((endY - startY) * (endY - startY)) + ((endX - startX) * (endX - startX))); i++)
                {
                    if (startY + i == 7 || startX + 1 == 7 || startY + i == 0 || startX + 1 == 0)
                    {
                        break;
                    }
                    if (board[startY + i, startX + i] != _)
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
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endY">ending x position</param>
        /// <param name="endX">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Rooks(int startX, int startY, int endX, int endY)
        {
            //Checks if the move is legal horizzontally
            if (startY == endY && startX != endX)
            {
                if (endX < startX)
                {
                    //Checks if there's a piece in between (backward)
                    for(int i = startX - endX; i > 0; i--)
                    {
                        if (board[i, endX] != 0)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //Checks if there is a piece between (forward)
                    for (int i = 1; i <= endX - startX; i++)
                    {
                        if (board[i, endX] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            //Vertically
            else if (startY != endY && startX == endX)
            {
                if (endY < startY)
                {
                    for(int i = startY; i > startY - endY; i--)
                    {
                        if (board[endX,i] != 0)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //Checks if there is a piece between 
                    for (int i = 1; i <= endY - startY; i++)
                    {
                        if (board[endY, i] != 0)
                        {
                            return false;
                        }
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
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endY">ending x position</param>
        /// <param name="endX">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Queens(int startX, int startY, int endX, int endY)
        {
            if ((startY + startX == endY + endX || startY - startX == endY - endX) ||
                (startY == endY && startX != endX) || (startY != endY && startX == endX))
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
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endY">ending x position</param>
        /// <param name="endX">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Kings(int startX, int startY, int endX, int endY)
        {
            if (Math.Abs(endY - startY) > 1 || Math.Abs(endX - startX) > 1)
            {
                return false;
            }

            return true;
        }
    }
}