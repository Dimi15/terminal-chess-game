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
        /// <returns>If the move is legal</returns>
        public static bool Move(int startX, int startY, int endX, int endY, bool blackWhite)
        {
            if (LegalMove(startY, startX, endX, endY, blackWhite))
            {
                board[endX, endY] = board[startY, startX];
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
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
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
                if (AttackedBy(kingX, kingY, true))
                {
                    return false;
                }
            }
            else
            {
                if (AttackedBy(kingX, kingY, false))
                {
                    return false;
                }
            }

            board[startY, startX] = board[endX, endY];
            board[endX, endY] = reversMove;

            // Checks if the piece moved is the right color
            if (blackWhite == false && (board[startY, startX] != BP && board[startY, startX] != BN &&
                                        board[startY, startX] != BB && board[startY, startX] != BR &&
                                        board[startY, startX] != BQ && board[startY, startX] != BK))
            {
                return false;
            }
            else if (blackWhite && (board[startY, startX] != WP && board[startY, startX] != WN &&
                                    board[startY, startX] != WB && board[startY, startX] != WR &&
                                    board[startY, startX] != WQ && board[startY, startX] != BK))
            {
                return false;
            }

            // Checks if either the starting or the ending position are inside the matrix
            if (startY < 0 || startY > 7 || startX < 0 || startX > 7)
            {
                return false;
            }

            if (endX < 0 || endX > 7 || endY < 0 || endY > 7)
            {
                return false;
            }

            // Checks if the moved piece is capturing a piece of the same color
            // Checks for white pieces if the ending square contains a white piece
            if (board[startY, startY] == WP || board[startY, startY] == WN || board[startY, startY] == WB ||
                board[startY, startY] == WR || board[startY, startY] == WQ || board[startY, startY] == WK)
            {
                if (board[endX, endY] == WP && board[endX, endY] == WN && board[endX, endY] == WB &&
                    board[endX, endY] == WR && board[endX, endY] == WQ && board[endX, endY] == WK)
                {
                    return false;
                }
            }
            // Checks for black pieces if the ending square contains a black piece
            else if (board[startY, startY] == BP || board[startY, startY] == BN || board[startY, startY] == BB ||
                     board[startY, startY] == BR || board[startY, startY] == BQ || board[startY, startY] == BK)
            {
                if (board[endX, endY] == BP && board[endX, endY] == BN && board[endX, endY] == BB &&
                    board[endX, endY] == BR && board[endX, endY] == BQ && board[endX, endY] == BK)
                {
                    return false;
                }
            }

            // Checks if the piece has been moved
            if (startY == endX && startX == endY)
            {
                return false;
            }

            // Checks if the move is legal based on the type of piece
            switch (board[startY, startX])
            {
                // EMPTY
                case _: return false;

                // PAWNS
                case WP: return WhitePawn(startY, startX, endX, endY);
                case BP: return BlackPawn(startY, startX, endX, endY);

                // KNIGHTS
                case WN:
                case BN: return Knights(startY, startX, endX, endY);

                // BISHOPS
                case WB:
                case BB: return Bishops(startY, startX, endX, endY);

                // ROOKS
                case WR:
                case BR: return Rooks(startY, startX, endX, endY);

                // QUEENS
                case WQ:
                case BQ: return Queens(startY, startX, endX, endY);

                // KINGS
                case WK:
                case BK: return Kings(startY, startX, endX, endY);

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
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool WhitePawn(int startX, int startY, int endX, int endY)
        {
            // Checks if the pawn is on the limits of the matrix
            if (startX == 0)
            {
                if (board[startY - 1, startX + 1] != _ && (endX == startY - 1 && endY == startX + 1))
                {
                    return true;
                }
            }
            else if (startX == 7)
            {
                if (board[startY - 1, startX - 1] != _ && (endX == startY - 1 && endY == startX - 1))
                {
                    return true;
                }
            }
            else
            {
                // Checks if the pawn can eat a piece and if the piece is a white one
                if (board[startY - 1, startX - 1] != _ && (endX == startY - 1 && endY == startX - 1))
                {
                    return true;
                }
                else if (board[startY - 1, startX + 1] != _ && (endX == startY - 1 && endY == startX + 1))
                {
                    // Checks if the pawn is on the starting square
                    if ((startY == 6 && board[5, startX] == _ && board[4, startX] == _) &&
                        (endX == 4 && endY == startX))
                    {
                        return true;
                    }
                    // Checks if te piece in front is occupied
                    else if ((endX == startY - 1 && endY == startX) && board[startY - 1, endY] != _)
                    {
                        return false;
                    }
                    else
                    {
                        // Checks everything else
                        if (endY != startX || endX >= startY || endX < startY - 1)
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
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool BlackPawn(int startX, int startY, int endX, int endY)
        {
            // Checks if the pawn is on the limits of the matrix
            if (startX == 0)
            {
                if (board[startY + 1, startX + 1] != _ && (endX == startY + 1 && endY == startX + 1))
                {
                    return true;
                }
            }
            else if (startX == 7)
            {
                if (board[startY + 1, startX - 1] != _ && (endX == startY + 1 && endY == startX - 1))
                {
                    return true;
                }
            }
            else
            {
                // Checks if the pawn can capture a piece and if the piece is a white one
                if (board[startY + 1, startX - 1] != _ && (endX == startY + 1 && endY == startX - 1))
                {
                    return true;
                }
                else if (board[startY + 1, startX + 1] != _ && (endX == startY + 1 && endY == startX + 1))
                {
                    return true;
                }
            }

            // Checks if the pawn is on the starting square
            if ((startY == 1 && board[2, startX] == _ && board[3, startX] == _) && (endX == 3 && endY == startX))
            {
                return true;
            }
            // Checks if the square in front is occupied
            else if ((endX == startY + 1 && endY == startX) && board[startY + 1, endY] != _)
            {
                return false;
            }
            else
            {
                // Checks everything else
                if (endY != startX || endX <= startY || endX > startY + 1)
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
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
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
                    if ((endX == startY + 2 && endY == startX + 1) || (endX == startY + 1 && endY == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the last column
                else if (startX == 7)
                {
                    if ((endX == startY + 2 && endY == startX - 1) || (endX == startY + 1 && endY == startX - 2))
                    {
                        return true;
                    }
                }
                // Checks the second column
                else if (startX == 1)
                {
                    if ((endX == startY + 2 && (endY == startX + 1 || endY == startX - 1)) ||
                        (endX == startY + 1 && endY == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the penultimate column
                else if (startX == 6)
                {
                    if ((endX == startY + 2 && (endY == startX - 1 || endY == startX + 1)) ||
                        (endX == startY + 1 && endY == startX - 2))
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
                    if ((endX == startY - 2 && endY == startX + 1) || (endX == startY - 1 && endY == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the last column
                else if (startX == 7)
                {
                    if ((endX == startY - 2 && endY == startX - 1) || (endX == startY - 1 && endY == startX - 2))
                    {
                        return true;
                    }
                }
                // Checks the second column
                else if (startX == 1)
                {
                    if ((endX == startY - 2 && (endY == startX + 1 || endY == startX - 1)) ||
                        (endX == startY - 1 && endY == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the penultimate column
                else if (startX == 6)
                {
                    if ((endX == startY - 2 && (endY == startX - 1 || endY == startX + 1)) ||
                        (endX == startY - 1 && endY == startX - 2))
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
                    if ((endX == startY + 2 && endY == startX + 1) || (endX == startY + 1 && endY == startX + 2) ||
                        (endX == startY - 1 && endY == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the last column
                else if (startX == 7)
                {
                    if ((endX == startY + 2 && endY == startX - 1) || (endX == startY + 1 && endY == startX - 2) ||
                        (endX == startY - 1 && endY == startX - 2))
                    {
                        return true;
                    }
                }
                // Checks the second column
                else if (startX == 1)
                {
                    if ((endX == startY + 2 && (endY == startX + 1 || endY == startX - 1)) ||
                        ((endX == startY + 1 || endX == startY - 1) && endY == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the penultimate column
                else if (startX == 6)
                {
                    if ((endX == startY + 2 && (endY == startX - 1 || endY == startX + 1)) ||
                        ((endX == startY + 1 || endX == startY - 1) && endY == startX - 2))
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
                    if ((endX == startY - 2 && endY == startX + 1) || (endX == startY - 1 && endY == startX + 2) ||
                        (endX == startY + 1 && endY == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the last column
                else if (startX == 7)
                {
                    if ((endX == startY - 2 && endY == startX - 1) || (endX == startY - 1 && endY == startX - 2) ||
                        (endX == startY + 1 && endY == startX - 2))
                    {
                        return true;
                    }
                }
                // Checks the second column
                else if (startX == 1)
                {
                    if ((endX == startY - 2 && (endY == startX + 1 || endY == startX - 1)) ||
                        ((endX == startY - 1 || endX == startY + 1) && endY == startX + 2))
                    {
                        return true;
                    }
                }
                // Checks the penultimate column
                else if (startX == 6)
                {
                    if ((endX == startY - 2 && (endY == startX - 1 || endY == startX + 1)) ||
                        ((endX == startY - 1 || endX == startY + 1) && endY == startX - 2))
                    {
                        return true;
                    }
                }
            }
            // Checks for knights that aren't at the limits
            else
            {
                if (endX == startY - 2 && (endY == startX - 1 || endY == startX + 1))
                {
                    return true;
                }
                else if (endX == startY + 2 && (endY == startX - 1 || endY == startX + 1))
                {
                    return true;
                }
                else if (endX == startY - 1 && (endY == startX - 2 || endY == startX + 2))
                {
                    return true;
                }
                else if (endX == startY + 1 && (endY == startX - 2 || endY == startX + 2))
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
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Bishops(int startX, int startY, int endX, int endY)
        {
            // Checks if the move is legal
            if (startY + startX == endX + endY || startY - startX == endX - endY)
            {
                // Checks if there is a piece between 
                for (int i = 1;
                     i <= Math.Sqrt(((endX - startY) * (endX - startY)) + ((endY - startX) * (endY - startX)));
                     i++)
                {
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
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Rooks(int startX, int startY, int endX, int endY)
        {
            //Checks if the move is legal
            if (startY == endX && startX != endY)
            {
                //Checks if there is a piece between 
                for (int i = 1; i <= endY - startX; i++)
                {
                    if (board[endX, i] != 0)
                    {
                        return false;
                    }
                }
            }
            else if (startY != endX && startX == endY)
            {
                //Checks if there is a piece between 
                for (int i = 1; i <= endX - startY; i++)
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
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Queens(int startX, int startY, int endX, int endY)
        {
            if ((startY + startX == endX + endY || startY - startX == endX - endY) ||
                (startY == endX && startX != endY) || (startY != endX && startX == endY))
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
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <returns>if the move is legal</returns>
        static bool Kings(int startX, int startY, int endX, int endY)
        {
            if (Math.Abs(endX - startY) > 1 || Math.Abs(endY - startX) > 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if a Pawn promotion is available and plays it
        /// </summary>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <param name="playerWhite">true if the player has white pieces</param>
        /// <param name="blackWhite">if the turn is of white or black</param>
        static void CheckPromotion(int endX, int endY, bool playerWhite, bool blackWhite)
        {
            // Checks both if the piece is a pawn and if it is on the last square
            if (((endY == 0) && board[endX, endY] == WP) || ((endY == 7) && board[endX, endY] == BP))
            {
                bool isPlayerMove = (playerWhite == blackWhite);
                int promotionPiece = 0;

                if (isPlayerMove)
                {
                    char choice;
                    do
                    {
                        Console.Write("Promote pawn to (R/N/B/Q): ");
                        // Converts the value of a Unicode character to its lowercase equivalent
                        choice = char.ToLowerInvariant(Console.ReadKey().KeyChar);
                        Console.WriteLine();
                    } while (choice != 'r' && choice != 'n' && choice != 'b' && choice != 'q');
                    switch (choice)
                    {
                        case 'r':
                            if (blackWhite)
                                promotionPiece = WR;
                            else
                                promotionPiece = BR;
                            break;
                        case 'n':
                            if (blackWhite)
                                promotionPiece = WN;
                            else
                                promotionPiece = BN;
                            break;
                        case 'b':
                            if (blackWhite)
                                promotionPiece = WB;
                            else
                                promotionPiece = BB;
                            break;
                        case 'q':
                            if (blackWhite)
                                promotionPiece = WQ;
                            else
                                promotionPiece = BQ;
                            break;
                    }
                }
                else
                {
                    // The computer always picks the queen
                    if (blackWhite)
                        promotionPiece = BQ;
                    else
                        promotionPiece = WQ;
                }

                board[endX, endY] = promotionPiece; // Plays the move
            }
        }
    }
}