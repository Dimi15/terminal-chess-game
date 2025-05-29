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
        static bool hasWhiteCastled = false;
        static bool hasBlackCastled = false;
        static int enPassantTargetX = -1; // The row that can be captured en passant
        static int enPassantTargetY = -1; // The column that can be captured

        /// <summary>
        /// Moves a piece from the start square to the end square, including handling en passant.
        /// </summary>
        /// <param name="startX">Start row</param>
        /// <param name="startY">Start column</param>
        /// <param name="endX">End row</param>
        /// <param name="endY">End column</param>
        /// <param name="blackWhite">True if white's turn, false if black's</param>
        /// <returns>True if the move is legal and performed</returns>
        public static bool Move(int startX, int startY, int endX, int endY, bool blackWhite)
        {
            int movingPiece = board[startX, startY];

            // Handle Castling
            if (IsCastlingMove(startX, startY, endX, endY, blackWhite))
            {
                PerformCastling(startX, startY, endX, endY, blackWhite);
                return true;
            }

            // Handle En Passant
            if (IsEnPassantMove(startX, startY, endX, endY, blackWhite))
            {
                PerformEnPassant(startX, startY, endX, endY, blackWhite);
                return true;
            }

            // Handle Normal move
            if (LegalMove(startX, startY, endX, endY, blackWhite))
            {
                board[endY, endX] = board[startY, startX];
                board[startY, startX] = _;
                return true;
            }

            return false;
        }
        
        
        /// <summary>
        /// Undoes a move on the board by restoring the pieces to their original positions.
        /// </summary>
        /// <param name="startX">Start row</param>
        /// <param name="startY">Start column</param>
        /// <param name="endX">End row</param>
        /// <param name="endY">End column</param>
        /// <param name="capturedPiece">The piece that was captured (or empty) to restore</param>
        static void UndoMove(int startX, int startY, int endX, int endY, int capturedPiece)
        {
            board[startX, startY] = board[endX, endY];
            board[endX, endY] = capturedPiece;
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
            reversMove = board[endY, endX];
            board[endY, endX] = board[startY, startX];
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

            board[startY, startX] = board[endY, endX];
            board[endY, endX] = reversMove;

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
            if (board[startY, startX] == WP || board[startY, startX] == WN || board[startY, startX] == WB ||
                board[startY, startX] == WR || board[startY, startX] == WQ || board[startY, startX] == WK)
            {
                if (board[endY, endX] == WP && board[endY, endX] == WN && board[endY, endX] == WB &&
                    board[endY, endX] == WR && board[endY, endX] == WQ && board[endY, endX] == WK)
                {
                    return false;
                }
            }
            // Checks for black pieces if the ending square contains a black piece
            else if (board[startY, startX] == BP || board[startY, startX] == BN || board[startY, startX] == BB ||
                     board[startY, startX] == BR || board[startY, startX] == BQ || board[startY, startX] == BK)
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
                if (endX > startX && endY > startY)
                {
                    // Checks if there is a piece between bottom right
                    for (int i = 1; i < Math.Sqrt(((endY - startY) * (endY - startY)) + ((endX - startX) * (endX - startX))); i++)
                    {
                        if (board[startY + i, startX + i] != _)
                        {
                            return false;
                        }
                    }
                }
                else if (endX < startX && endY > startY)
                {
                    // Checks if there is a piece between bottom left
                    for (int i = 1; i < Math.Sqrt(((endY - startY) * (endY - startY)) + ((endX - startX) * (endX - startX))); i++)
                    {
                        if (board[startY + i, startX - i] != _)
                        {
                            return false;
                        }
                    }
                }
                else if (endX > startX && endY < startY)
                {
                    // Checks if there is a piece between top right
                    for (int i = 1; i < Math.Sqrt(((endY - startY) * (endY - startY)) + ((endX - startX) * (endX - startX))); i++)
                    {
                        if (board[startY - i, startX + i] != _)
                        {
                            return false;
                        }
                    }
                }
                else if (endX < startX && endY < startY)
                {
                    // Checks if there is a piece between top left
                    for (int i = 1; i < Math.Sqrt(((endY - startY) * (endY - startY)) + ((endX - startX) * (endX - startX))); i++)
                    {
                        if (board[startY - i, startX - i] != _)
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
                    for (int i = endX; i < startX; i++)
                    {
                        if (board[endY, i] != 0)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //Checks if there is a piece between (forward)
                    for (int i = startX; i < endX; i++)
                    {
                        if (board[endY, i] != 0)
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
                    //Checks if there's a piece in between (backward)
                    for (int i = endY; i < startY; i++)
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
                    for (int i = startY; i < endY; i++)
                    {
                        if (board[i, endX] != 0)
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
            if(Bishops(startX,startY,endX,endY) || Rooks(startX,startY,endX,endY))
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
                        Console.WriteLine("Promote pawn to (R/N/B/Q): ");
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

        /// <summary>
        /// Checks and performs castling if a valid castling move is made.
        /// </summary>
        /// <param name="startX">Start row</param>
        /// <param name="startY">Start column</param>
        /// <param name="endX">End row</param>
        /// <param name="endY">End column</param>
        /// <param name="blackWhite">True if white's turn, false if black's</param>
        /// <param name="hasWhiteCastled">Indicates if white has already castled</param>
        /// <param name="hasBlackCastled">Indicates if black has already castled</param>
        static bool IsCastlingMove(int startX, int startY, int endX, int endY, bool blackWhite)
        {
            // WHITE CASTLING
            if (blackWhite && board[startX, startY] == WK)
            {
                if (startX == 7 && startY == 4)
                {
                    // Kingside
                    if (endX == 7 && endY == 6 &&
                        board[7, 5] == _ && board[7, 6] == _ &&
                        board[7, 7] == WR && !hasWhiteCastled)
                        return true;

                    // Queenside
                    if (endX == 7 && endY == 2 &&
                        board[7, 1] == _ && board[7, 2] == _ && board[7, 3] == _ &&
                        board[7, 0] == WR && !hasWhiteCastled)
                        return true;
                }
            }

            // BLACK CASTLING
            if (!blackWhite && board[startX, startY] == BK)
            {
                if (startX == 0 && startY == 3)
                {
                    // Kingside
                    if (endX == 0 && endY == 1 &&
                        board[0, 1] == _ && board[0, 2] == _ &&
                        board[0, 0] == BR && !hasBlackCastled)
                        return true;

                    // Queenside
                    if (endX == 0 && endY == 5 &&
                        board[0, 4] == _ && board[0, 5] == _ && board[0, 6] == _ &&
                        board[0, 7] == BR && !hasBlackCastled)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Executes the castling move by moving the king and rook to their castled positions on the board.
        /// Assumes all legality checks (such as clear path, no checks, and castling rights) have been performed prior.
        /// Updates the board state accordingly and sets the castling flag for the respective color.
        /// </summary>
        /// <param name="kingStartX">The starting row of the king.</param>
        /// <param name="kingStartY">The starting column of the king.</param>
        /// <param name="kingEndX">The ending row of the king (castled position).</param>
        /// <param name="kingEndY">The ending column of the king (castled position).</param>
        /// <param name="blackWhite">True if it is white's turn; false if black's.</param>
        /// <param name="hasWhiteCastled">Reference to the white castling flag to update.</param>
        /// <param name="hasBlackCastled">Reference to the black castling flag to update.</param>
        static void PerformCastling(int startX, int startY, int endX, int endY, bool blackWhite)
        {
            // WHITE
            if (blackWhite && board[startX, startY] == WK)
            {
                // Kingside
                if (endX == 7 && endY == 6)
                {
                    board[7, 4] = _; // Clear King
                    board[7, 7] = _; // Clear Rook
                    board[7, 6] = WK;
                    board[7, 5] = WR;
                    hasWhiteCastled = true;
                }
                // Queenside
                else if (endX == 7 && endY == 2)
                {
                    board[7, 4] = _;
                    board[7, 0] = _;
                    board[7, 2] = WK;
                    board[7, 3] = WR;
                    hasWhiteCastled = true;
                }
            }

            // BLACK
            else if (!blackWhite && board[startX, startY] == BK)
            {
                // Kingside
                if (endX == 0 && endY == 1)
                {
                    board[0, 3] = _;
                    board[0, 0] = _;
                    board[0, 1] = BK;
                    board[0, 2] = BR;
                    hasBlackCastled = true;
                }
                // Queenside
                else if (endX == 0 && endY == 5)
                {
                    board[0, 3] = _;
                    board[0, 7] = _;
                    board[0, 5] = BK;
                    board[0, 4] = BR;
                    hasBlackCastled = true;
                }
            }
        }

        /// <summary>
        /// Checks if the current move is a valid en passant capture.
        /// </summary>
        /// <param name="startX">Start row</param>
        /// <param name="startY">Start column</param>
        /// <param name="endX">End row</param>
        /// <param name="endY">End column</param>
        /// <param name="blackWhite">True if white's turn, false if black's</param>
        /// <returns>True if the move is a valid en passant</returns>
        static bool IsEnPassantMove(int startX, int startY, int endX, int endY, bool blackWhite)
        {
            if (blackWhite)
            {
                // White pawn must move diagonally to en passant target square
                if (board[startX, startY] == WP && startX == 3 && endX == 2 && Math.Abs(endY - startY) == 1)
                {
                    return endX == enPassantTargetX && endY == enPassantTargetY;
                }
            }
            else
            {
                // Black pawn must move diagonally to en passant target square
                if (board[startX, startY] == BP && startX == 4 && endX == 5 && Math.Abs(endY - startY) == 1)
                {
                    return endX == enPassantTargetX && endY == enPassantTargetY;
                }
            }

            return false;
        }

        /// <summary>
        /// Performs the en passant capture.
        /// </summary>
        /// <param name="startX">Start row</param>
        /// <param name="startY">Start column</param>
        /// <param name="endX">End row (target pawn square)</param>
        /// <param name="endY">End column</param>
        /// <param name="blackWhite">True if white's turn, false if black's</param>
        static void PerformEnPassant(int startX, int startY, int endX, int endY, bool blackWhite)
        {
            if (blackWhite)
            {
                // Capture black pawn
                board[endX + 1, endY] = _;
                board[endX, endY] = WP;
            }
            else
            {
                // Capture white pawn
                board[endX - 1, endY] = _;
                board[endX, endY] = BP;
            }

            board[startX, startY] = _;
            enPassantTargetX = -1;
            enPassantTargetY = -1;
        }
    }
}
