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
        /// Evaluate the board
        /// </summary>
        /// <return>
        ///         == 0, draw position.
        ///         > 0, advantage for white.
        ///         < 0, advantage for black.
        /// </return>
        static double Evaluation()
        {
            int evaluation = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // PIECES VALUE
                    // NOTE: All pieces value are multiplied by 100 to avoid floating point precision errors
                    //
                    // Pawn:    1
                    // Knight:  3
                    // Bishop:  3
                    // Rook:    5
                    // Queen:   9
                    switch (board[i, j])
                    {
                        // White Piece
                        case WP: evaluation += 100; break;
                        case WN: evaluation += 300; break;
                        case WB: evaluation += 300; break;
                        case WR: evaluation += 500; break;
                        case WQ: evaluation += 900; break;

                        // Black Piece
                        case BP: evaluation -= 100; break;
                        case BN: evaluation -= 300; break;
                        case BB: evaluation -= 300; break;
                        case BR: evaluation -= 500; break;
                        case BQ: evaluation -= 900; break;
                    }

                    int positionGain = 0;

                    if (board[i, j] == WK) // White King
                    {
                        // For White King
                        //
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.20 |-0.20 |-0.20 |-0.20 |-0.20 |-0.20 |-0.20 |-0.20 |
                        // +------+------+------+------+------+------+------+------+
                        // | 0.20 | 0.20 | 0.00 | 0.00 | 0.00 | 0.00 | 0.20 | 0.20 |
                        // +------+------+------+------+------+------+------+------+
                        // | 0.20 | 0.30 | 0.10 | 0.00 | 0.00 | 0.10 | 0.30 | 0.20 |
                        // +------+------+------+------+------+------+------+------+

                        if (i < 5 && (j == 0 || j == 7))
                        {
                            positionGain += -30;
                        }
                        else if (i < 5 && (j == 1 || j == 2 || j == 5 || j == 6))
                        {
                            positionGain += -40;
                        }
                        else if (i < 5 && (j == 3 || j == 4))
                        {
                            positionGain += -50;
                        }
                        else if (i == 5)
                        {
                            positionGain += -20;
                        }
                        else if (i == 6 && (j == 0 || j == 1 || j == 6 || j == 7))
                        {
                            positionGain += 20;
                        }
                        else if (i == 7 && (j == 0 || j == 7))
                        {
                            positionGain += 20;
                        }
                        else if (i == 7 && (j == 1 || j == 6))
                        {
                            positionGain += 30;
                        }
                        else if (i == 7 && (j == 2 || j == 5))
                        {
                            positionGain += 10;
                        }
                    }
                    else if (board[i, j] == BK) // Black King
                    {
                        // For Black King
                        //
                        // +------+------+------+------+------+------+------+------+
                        // | 0.20 | 0.30 | 0.10 | 0.00 | 0.00 | 0.10 | 0.30 | 0.20 |
                        // +------+------+------+------+------+------+------+------+
                        // | 0.20 | 0.20 | 0.00 | 0.00 | 0.00 | 0.00 | 0.20 | 0.20 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.20 |-0.20 |-0.20 |-0.20 |-0.20 |-0.20 |-0.20 |-0.20 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.30 |-0.40 |-0.40 |-0.50 |-0.50 |-0.40 |-0.40 |-0.30 |
                        // +------+------+------+------+------+------+------+------+

                        if (i > 2 && (j == 0 || j == 7))
                        {
                            positionGain += 30;
                        }
                        else if (i > 2 && (j == 1 || j == 2 || j == 5 || j == 6))
                        {
                            positionGain += 40;
                        }
                        else if (i > 2 && (j == 3 || j == 4))
                        {
                            positionGain += 50;
                        }
                        else if (i == 2)
                        {
                            positionGain += 20;
                        }
                        else if (i == 1 && (j == 0 || j == 1 || j == 6 || j == 7))
                        {
                            positionGain += -20;
                        }
                        else if (i == 0 && (j == 0 || j == 7))
                        {
                            positionGain += -20;
                        }
                        else if (i == 0 && (j == 1 || j == 6))
                        {
                            positionGain += -30;
                        }
                        else if (i == 0 && (j == 2 || j == 5))
                        {
                            positionGain += -10;
                        }
                    }
                    else if (board[i, j] != _)
                    {
                        // For All Other Pieces
                        //
                        // +------+------+------+------+------+------+------+------+
                        // |-0.50 |-0.40 |-0.40 |-0.40 |-0.40 |-0.40 |-0.40 |-0.50 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.40 |-0.20 | 0.00 | 0.00 | 0.00 | 0.00 |-0.20 |-0.40 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.40 | 0.00 | 0.10 | 0.20 | 0.20 | 0.10 | 0.00 |-0.40 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.40 | 0.00 | 0.20 | 0.25 | 0.25 | 0.20 | 0.00 |-0.40 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.40 | 0.00 | 0.20 | 0.25 | 0.25 | 0.20 | 0.00 |-0.40 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.40 | 0.00 | 0.10 | 0.20 | 0.20 | 0.10 | 0.00 |-0.40 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.40 |-0.20 | 0.00 | 0.00 | 0.00 | 0.00 |-0.20 |-0.40 |
                        // +------+------+------+------+------+------+------+------+
                        // |-0.50 |-0.40 |-0.40 |-0.40 |-0.40 |-0.40 |-0.40 |-0.50 |
                        // +------+------+------+------+------+------+------+------+

                        if ((i == 0 || i == 7) && (j == 0 || j == 7))
                        {
                            positionGain += -50;
                        }
                        else if ((i == 0 || i == 7) || (j == 0 || j == 7))
                        {
                            positionGain += -40;
                        }
                        else if ((i == 1 || i == 6) && (j == 1 || j == 6))
                        {
                            positionGain += -20;
                        }
                        else if ((i == 2 || i == 5) && (j == 2 || j == 5))
                        {
                            positionGain += 10;
                        }
                        else if ((i == 2 || i == 5) || (j == 2 || j == 5))
                        {
                            positionGain += 20;
                        }
                        else if ((i == 3 || i == 4) && (j == 3 || j == 4))
                        {
                            positionGain += 25;
                        }
                    }

                    if (board[i, j] < BP) // True if White Piece
                    {
                        evaluation += positionGain;
                    }
                    else
                    {
                        evaluation -= positionGain;
                    }
                }
            }

            return evaluation / 100.0; // Divide by 100 to correct for the multiplication of 100 of all value
        }

        /// <summary>
        /// Check if the game has ended
        /// <param name="white">if it is white to be under checkmate</param>
        /// <param name="draw">game ended by draw</param>
        /// </summary>
        /// <returns>if the king is under checkmate</returns>
        static bool Checkmate(bool white, ref bool draw)
        {
            draw = false;
            bool correctPlayer = false;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != _)
                    {
                        correctPlayer = false;

                        if (white)
                        {
                            if (board[i, j] < BP)
                            {
                                correctPlayer = true;
                            }
                        }
                        else
                        {
                            if (board[i, j] >= BP)
                            {
                                correctPlayer = true;
                            }
                        }

                        if (correctPlayer)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (LegalMove(j, i, l, k, white))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (UnderCheck(white))
            {
                return true;
            }
            else
            {
                draw = true;
                return false;
            }
        }

        /// <summary>
        /// Check if the king is under check
        /// </summary>
        /// <param name="white">if it is the white king</param>
        /// <returns>if the king is under check</returns>
        static bool UnderCheck(bool white)
        {
            int kingX = 0, kingY = 0, pieceX = 0, pieceY = 0;
            return UnderCheck(white, ref kingX, ref kingY, ref pieceX, ref pieceY);
        }

        /// <summary>
        /// Check if the King is under check
        /// </summary>
        /// <param name="white">if it is the white king</param>
        /// <param name="kingX">The X of the king</param>
        /// <param name="kingY">The Y of the king</param>
        /// <param name="pieceX">The X of the first piece found that's giving the check</param>
        /// <param name="pieceY">The Y of the first piece found that's giving the check</param>
        /// <returns>if the king is under check</returns>
        static bool UnderCheck(bool white, ref int kingX, ref int kingY, ref int pieceX, ref int pieceY)
        {
            // Find Kings Location
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (white)
                    {
                        if (board[i, j] == WK)
                        {
                            kingX = j;
                            kingY = i;

                            if (AttackedBy(j, i, false, ref pieceX, ref pieceY))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (board[i, j] == BK)
                        {
                            kingX = j;
                            kingY = i;

                            if (AttackedBy(j, i, true, ref pieceX, ref pieceY))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return false; // King Not Found
        }

        static bool AttackedByPawn(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Left
            if (white)
            {
                if (x > 0 && y < 7)
                {
                    if (board[y + 1, x - 1] == WP)
                    {
                        fromX = x - 1;
                        fromY = y + 1;
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
                        fromX = x - 1;
                        fromY = y - 1;
                        return true;
                    }
                }
            }

            // Right
            if (white)
            {
                if (x < 7 && y < 7)
                {
                    if (board[y + 1, x + 1] == WP)
                    {
                        fromX = x + 1;
                        fromY = y + 1;
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
                        fromX = x + 1;
                        fromY = y - 1;
                        return true;
                    }
                }
            }

            return false;
        }

        static bool AttackedByKnight(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Top
            if (y > 1)
            {
                // Left
                if (x > 0)
                {
                    if (white)
                    {
                        if (board[y - 2, x - 1] == WN)
                        {
                            fromX = x - 1;
                            fromY = y - 2;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 2, x - 1] == BN)
                        {
                            fromX = x - 1;
                            fromY = y - 2;
                            return true;
                        }
                    }
                }

                // Right
                if (x < 7)
                {
                    if (white)
                    {
                        if (board[y - 2, x + 1] == WN)
                        {
                            fromX = x + 1;
                            fromY = y - 2;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 2, x + 1] == BN)
                        {
                            fromX = x + 1;
                            fromY = y - 2;
                            return true;
                        }
                    }
                }
            }

            // Bottom
            if (y < 6)
            {
                // Left
                if (x > 0)
                {
                    if (white)
                    {
                        if (board[y + 2, x - 1] == WN)
                        {
                            fromX = x - 1;
                            fromY = y + 2;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 2, x - 1] == BN)
                        {
                            fromX = x + 1;
                            fromY = y + 2;
                            return true;
                        }
                    }
                }

                // Right
                if (x < 7)
                {
                    if (white)
                    {
                        if (board[y + 2, x + 1] == WN)
                        {
                            fromX = x + 1;
                            fromY = y + 2;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 2, x + 1] == BN)
                        {
                            fromX = x + 1;
                            fromY = y + 2;
                            return true;
                        }
                    }
                }
            }

            // Left
            if (x > 1)
            {
                // Top
                if (y > 0)
                {
                    if (white)
                    {
                        if (board[y - 1, x - 2] == WN)
                        {
                            fromX = x - 2;
                            fromY = y - 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 1, x - 2] == BN)
                        {
                            fromX = x - 2;
                            fromY = y - 1;
                            return true;
                        }
                    }
                }

                // Bottom
                if (y < 7)
                {
                    if (white)
                    {
                        if (board[y + 1, x - 2] == WN)
                        {
                            fromX = x - 2;
                            fromY = y + 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 1, x - 2] == BN)
                        {
                            fromX = x - 2;
                            fromY = y + 1;
                            return true;
                        }
                    }
                }
            }

            // Right
            if (x < 6)
            {
                // Top
                if (y > 0)
                {
                    if (white)
                    {
                        if (board[y - 1, x + 2] == WN)
                        {
                            fromX = x + 2;
                            fromY = y - 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 1, x + 2] == BN)
                        {
                            fromX = x + 2;
                            fromY = y - 1;
                            return true;
                        }
                    }
                }

                // Bottom
                if (y < 7)
                {
                    if (white)
                    {
                        if (board[y + 1, x + 2] == WN)
                        {
                            fromX = x + 2;
                            fromY = y + 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 1, x + 2] == BN)
                        {
                            fromX = x + 2;
                            fromY = y + 1;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        static bool AttackedByKing(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Top
            if (y > 0)
            {
                // Left
                if (x > 0)
                {
                    if (white)
                    {
                        if (board[y - 1, x - 1] == WK)
                        {
                            fromX = x - 1;
                            fromY = y - 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 1, x - 1] == BK)
                        {
                            fromX = x - 1;
                            fromY = y - 1;
                            return true;
                        }
                    }
                }

                // Middle
                if (white)
                {
                    if (board[y - 1, x] == WK)
                    {
                        fromX = x;
                        fromY = y - 1;
                        return true;
                    }
                }
                else
                {
                    if (board[y - 1, x] == BK)
                    {
                        fromX = x;
                        fromY = y - 1;
                        return true;
                    }
                }

                // Right
                if (x < 7)
                {
                    if (white)
                    {
                        if (board[y - 1, x + 1] == WK)
                        {
                            fromX = x + 1;
                            fromY = y - 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y - 1, x + 1] == BK)
                        {
                            fromX = x + 1;
                            fromY = y - 1;
                            return true;
                        }
                    }
                }
            }

            // Left
            if (x > 0)
            {
                if (white)
                {
                    if (board[y, x - 1] == WK)
                    {
                        fromX = x - 1;
                        fromY = y;
                        return true;
                    }
                }
                else
                {
                    if (board[y, x - 1] == BK)
                    {
                        fromX = x - 1;
                        fromY = y;
                        return true;
                    }
                }
            }

            // Right
            if (x < 7)
            {
                if (white)
                {
                    if (board[y, x + 1] == WK)
                    {
                        fromX = x + 1;
                        fromY = y;
                        return true;
                    }
                }
                else
                {
                    if (board[y, x + 1] == BK)
                    {
                        fromX = x + 1;
                        fromY = y;
                        return true;
                    }
                }
            }

            // Bottom
            if (y < 7)
            {
                // Left
                if (x > 0)
                {
                    if (white)
                    {
                        if (board[y + 1, x - 1] == WK)
                        {
                            fromX = x - 1;
                            fromY = y + 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 1, x - 1] == BK)
                        {
                            fromX = x - 1;
                            fromY = y + 1;
                            return true;
                        }
                    }
                }

                //midle
                if (white)
                {
                    if (board[y + 1, x] == WK)
                    {
                        fromX = x;
                        fromY = y + 1;
                        return true;
                    }
                }
                else
                {
                    if (board[y + 1, x] == BK)
                    {
                        fromX = x;
                        fromY = y + 1;
                        return true;
                    }
                }

                // Right
                if (x < 7)
                {
                    if (white)
                    {
                        if (board[y + 1, x + 1] == WK)
                        {
                            fromX = x + 1;
                            fromY = y + 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (board[y + 1, x + 1] == BK)
                        {
                            fromX = x + 1;
                            fromY = y + 1;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        static bool AttackedVerticallyTop(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            int pieceFound = _;

            fromX = x;
            fromY = y;

            // Top
            do
            {
                fromY--;
                if (fromY < 0)
                {
                    break;
                }

                pieceFound = board[fromY, fromX];
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

            return false;
        }

        static bool AttackedVerticallyBottom(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Bottom
            int pieceFound = _;

            fromX = x;
            fromY = y;

            do
            {
                fromY++;
                if (fromY >= 8)
                {
                    break;
                }

                pieceFound = board[fromY, fromX];
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

            return false;
        }

        static bool AttackedVertically(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            if (AttackedVerticallyTop(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedVerticallyBottom(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }

        static bool AttackedHorizzontallyLeft(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Left
            int pieceFound = _;

            fromX = x;
            fromY = y;

            do
            {
                fromX--;
                if (fromX < 0)
                {
                    break;
                }

                pieceFound = board[fromY, fromX];
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

            return false;
        }

        static bool AttackedHorizzontallyRight(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Right
            int pieceFound = _;

            fromX = x;
            fromY = y;

            do
            {
                fromX++;
                if (fromX >= 8)
                {
                    break;
                }

                pieceFound = board[fromY, fromX];
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

            return false;
        }

        static bool AttackedHorizzontally(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            if (AttackedHorizzontallyLeft(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedHorizzontallyRight(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }

        static bool AttackedDiagonallyTopLeft(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Top left
            int pieceFound = _;

            fromX = x;
            fromY = y;

            do
            {
                fromX--;
                fromY--;
                if (fromX < 0 || fromY < 0)
                {
                    break;
                }

                pieceFound = board[fromY, fromX];
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

        static bool AttackedDiagonallyTopRight(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Top right
            int pieceFound = _;

            fromX = x;
            fromY = y;

            do
            {
                fromX++;
                fromY--;
                if (fromX >= 8 || fromY < 0)
                {
                    break;
                }

                pieceFound = board[fromY, fromX];
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

        static bool AttackedDiagonallyBottomLeft(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Bottom left
            int pieceFound = _;

            fromX = x;
            fromY = y;

            do
            {
                fromX--;
                fromY++;
                if (fromX < 0 || fromY >= 8)
                {
                    break;
                }

                pieceFound = board[fromY, fromX];
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

        static bool AttackedDiagonallyBottomRight(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // Bottom right
            int pieceFound = _;

            fromX = x;
            fromY = y;

            do
            {
                fromX++;
                fromY++;
                if (fromX >= 8 || fromY >= 8)
                {
                    break;
                }

                pieceFound = board[fromY, fromX];
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

        static bool AttackedDiagonally(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            if (AttackedDiagonallyTopLeft(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedDiagonallyTopRight(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedDiagonallyBottomLeft(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedDiagonallyBottomRight(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if a square is under attack
        /// </summary>
        /// <param name="white">The square is attacked by white</param>
        /// <returns>true if the square is attacked</returns>
        static bool AttackedBy(int x, int y, bool white)
        {
            int fromX = 0, fromY = 0;
            return AttackedBy(x, y, white, ref fromX, ref fromY);
        }

        /// <summary>
        /// Check if a square is under attack
        /// </summary>
        /// <param name="white">The square is attacked by white</param>
        /// <param name="fromX">The X of the first piece found able to attack the given square</param>
        /// <param name="fromY">The Y of the first piece found able to attack the given square</param>
        /// <returns>true if the square is attacked</returns>
        static bool AttackedBy(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            // BY PAWN
            if (AttackedByPawn(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            // BY KNIGHT
            if (AttackedByKnight(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            // BY KING
            if (AttackedByKing(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            // VERTICALLY
            if (AttackedVertically(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            // HORIZONTALLY
            if (AttackedHorizzontally(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            // DIAGONALLY
            if (AttackedDiagonally(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }
    }
}