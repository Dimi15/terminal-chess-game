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
        /// Evaluates the board position.
        /// </summary>
        /// <returns>
        /// 0 indicates a draw position.
        /// A positive value indicates an advantage for White.
        /// A negative value indicates an advantage for Black.
        /// </returns>
        static double Evaluation()
        {
            int evaluation = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // PIECES VALUE
                    // NOTE: All pieces values are multiplied by 100 to avoid floating point precision errors
                    // Pawn: 1
                    // Knight: 3
                    // Bishop: 3
                    // Rook: 5
                    // Queen: 9
                    switch (board[i, j])
                    {
                        // White Pieces
                        case WP: evaluation += 100; break;
                        case WN: evaluation += 300; break;
                        case WB: evaluation += 300; break;
                        case WR: evaluation += 500; break;
                        case WQ: evaluation += 900; break;

                        // Black Pieces
                        case BP: evaluation -= 100; break;
                        case BN: evaluation -= 300; break;
                        case BB: evaluation -= 300; break;
                        case BR: evaluation -= 500; break;
                        case BQ: evaluation -= 900; break;
                    }

                    int positionGain = 0;

                    if (board[i, j] == WK) // White King
                    {
                        // For White King:
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
                        // For Black King:
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
                        // For All The Other Pieces:
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
        /// Checks if the game has ended
        /// <param name="white">If it is white to be under checkmate</param>
        /// <param name="draw">Game ended by draw</param>
        /// </summary>
        /// <returns>If the king is under checkmate</returns>
        static bool Checkmate(bool white, ref bool draw)
        {
            draw = false;
            bool correctPlayer = false, canMakeMove = false;

            // Count the pieces found:

            // WHITE
            int WPsFound = 0;
            int WNsFound = 0;
            int WBsFoundLightSquares = 0;
            int WBsFoundDarkSquares = 0;
            int WRsFound = 0;
            int WQsFound = 0;
            int WKsFound = 0;

            // BLACK
            int BPsFound = 0;
            int BNsFound = 0;
            int BBsFoundLightSquares = 0;
            int BBsFoundDarkSquares = 0;
            int BRsFound = 0;
            int BQsFound = 0;
            int BKsFound = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != _)
                    {
                        switch(board[i, j])
                        {
                            // White pieces
                            case WP: WPsFound++; break;
                            case WN: WNsFound++; break;
                            case WB:
                                if (i % 2 == 0)
                                {
                                    if (j % 2 == 0)
                                    {
                                        WBsFoundLightSquares++;
                                    }
                                    else
                                    {
                                        WBsFoundDarkSquares++;
                                    }
                                }
                                else
                                {
                                    if (j % 2 == 0)
                                    {
                                        WBsFoundDarkSquares++;
                                    }
                                    else
                                    {
                                        WBsFoundLightSquares++;
                                    }
                                }
                                break;

                            case WR: WRsFound++; break;
                            case WQ: WQsFound++; break;
                            case WK: WKsFound++; break;

                            // Black pieces
                            case BP: BPsFound++; break;
                            case BN: BNsFound++; break;
                            case BB:
                                if (i % 2 == 0)
                                {
                                    if (j % 2 == 0)
                                    {
                                        BBsFoundLightSquares++;
                                    }
                                    else
                                    {
                                        BBsFoundDarkSquares++;
                                    }
                                }
                                else
                                {
                                    if (j % 2 == 0)
                                    {
                                        BBsFoundDarkSquares++;
                                    }
                                    else
                                    {
                                        BBsFoundLightSquares++;
                                    }
                                }
                                break;
                            case BR: BRsFound++; break;
                            case BQ: BQsFound++; break;
                            case BK: BKsFound++; break;
                        }

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

                        if (!canMakeMove && correctPlayer)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (LegalMove(j, i, l, k, white))
                                    {
                                        canMakeMove = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!canMakeMove)
            {
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
            else
            {
                // Check for 50-move rule
                if(movesDone >= 50)
                {
                    draw = true;
                    return false;
                }

                // CHECK FOR INSUFFICIENT MATERIAL DRAW:

                // White King VS Black King
                if(WPsFound == 0 && WNsFound == 0 && WBsFoundLightSquares == 0 && WBsFoundDarkSquares == 0 && WRsFound == 0 && WQsFound == 0 && WKsFound == 1 && BPsFound == 0 && BNsFound == 0 && BBsFoundLightSquares == 0 && BBsFoundDarkSquares == 0 && BRsFound == 0 && BQsFound == 0 && BKsFound == 1)
                {
                    draw = true;
                }
                // White King And Knight VS Black King
                else if(WPsFound == 0 && WNsFound == 1 && WBsFoundLightSquares == 0 && WBsFoundDarkSquares == 0 && WRsFound == 0 && WQsFound == 0 && WKsFound == 1 && BPsFound == 0 && BNsFound == 0 && BBsFoundLightSquares == 0 && BBsFoundDarkSquares == 0 && BRsFound == 0 && BQsFound == 0 && BKsFound == 1)
                {
                    draw = true;
                }
                // White King VS Black King And Knight
                else if (WPsFound == 0 && WNsFound == 0 && WBsFoundLightSquares == 0 && WBsFoundDarkSquares == 0 && WRsFound == 0 && WQsFound == 0 && WKsFound == 1 && BPsFound == 0 && BNsFound == 1 && BBsFoundLightSquares == 0 && BBsFoundDarkSquares == 0 && BRsFound == 0 && BQsFound == 0 && BKsFound == 1)
                {
                    draw = true;
                }
                // White King And Bishops VS Black King
                else if (WPsFound == 0 && WNsFound == 0 && ((WBsFoundLightSquares == 1 && WBsFoundDarkSquares == 0) || (WBsFoundLightSquares == 0 && WBsFoundDarkSquares == 1)) && WRsFound == 0 && WQsFound == 0 && WKsFound == 1 && BPsFound == 0 && BNsFound == 0 && BBsFoundLightSquares == 0 && BBsFoundDarkSquares == 0 && BRsFound == 0 && BQsFound == 0 && BKsFound == 1)
                {
                    draw = true;
                }
                // White King VS Black King And Bishops
                else if (WPsFound == 0 && WNsFound == 0 && WBsFoundLightSquares == 0 && WBsFoundDarkSquares == 0 && WRsFound == 0 && WQsFound == 0 && WKsFound == 1 && BPsFound == 0 && BNsFound == 0 && ((BBsFoundLightSquares == 1 && BBsFoundDarkSquares == 0) || (BBsFoundLightSquares == 0 && BBsFoundDarkSquares == 1)) && BRsFound == 0 && BQsFound == 0 && BKsFound == 1)
                {
                    draw = true;
                }
                // White King And Light Squares Bishops VS Black King And Light Squares Bishops
                else if (WPsFound == 0 && WNsFound == 0 && WBsFoundLightSquares == 1 && WBsFoundDarkSquares == 0 && WRsFound == 0 && WQsFound == 0 && WKsFound == 1 && BPsFound == 0 && BNsFound == 0 && BBsFoundLightSquares == 1 && BBsFoundDarkSquares == 0 && BRsFound == 0 && BQsFound == 0 && BKsFound == 1)
                {
                    draw = true;
                }
                // White King And Dark Squares Bishops VS Black King And Dark Squares Bishops
                else if (WPsFound == 0 && WNsFound == 0 && WBsFoundLightSquares == 0 && WBsFoundDarkSquares == 1 && WRsFound == 0 && WQsFound == 0 && WKsFound == 1 && BPsFound == 0 && BNsFound == 0 && BBsFoundLightSquares == 0 && BBsFoundDarkSquares == 1 && BRsFound == 0 && BQsFound == 0 && BKsFound == 1)
                {
                    draw = true;
                }

                return false;
            }
        }

        /// <summary>
        /// Checks if the king is under check
        /// </summary>
        /// <param name="byWhite">The square is attacked by white</param>
        /// <returns>If the king is under check</returns>
        static bool UnderCheck(bool byWhite)
        {
            int kingX = 0, kingY = 0, pieceX = 0, pieceY = 0;
            return UnderCheck(byWhite, ref kingX, ref kingY, ref pieceX, ref pieceY);
        }

        /// <summary>
        /// Checks if the King is under check
        /// </summary>
        /// <param name="byWhite">The square is attacked by white</param>
        /// <param name="kingX">Column of the king</param>
        /// <param name="kingY">Row of the king</param>
        /// <param name="pieceX">Column of the first piece found that's giving the check</param>
        /// <param name="pieceY">Row of the first piece found that's giving the check</param>
        /// <returns>If the king is under check</returns>
        static bool UnderCheck(bool byWhite, ref int kingX, ref int kingY, ref int pieceX, ref int pieceY)
        {
            // Find Kings Location
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (byWhite)
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

        /// <summary>
        /// Check whether an opposing pawn is attacking the indicated square by checking the two immediately adjacent diagonal squares
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="byWhite">The square is attacked by white</param>
        /// <param name="fromX">The Column from which square the square is attacked</param>
        /// <param name="fromY">The Column from which square the square is attacked</param>
        /// <returns>If the square is attacked by a pawn</returns>
        static bool AttackedByPawn(int x, int y, bool byWhite, ref int fromX, ref int fromY)
        {
            // Left
            if (byWhite)
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
            if (byWhite)
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
        
        static bool AttackedByKnight(int x, int y, bool byWhite, ref int fromX, ref int fromY)
        {
            // Top
            if (y > 1)
            {
                // Left
                if (x > 0)
                {
                    if (byWhite)
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
                    if (byWhite)
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
                    if (byWhite)
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
                    if (byWhite)
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
                    if (byWhite)
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
                    if (byWhite)
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
                    if (byWhite)
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
                    if (byWhite)
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
        
        static bool AttackedByKing(int x, int y, bool byWhite, ref int fromX, ref int fromY)
        {
            // Top
            if (y > 0)
            {
                // Left
                if (x > 0)
                {
                    if (byWhite)
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
                if (byWhite)
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
                    if (byWhite)
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
                if (byWhite)
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
                if (byWhite)
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
                    if (byWhite)
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
                if (byWhite)
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
                    if (byWhite)
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
        
        static bool AttackedVerticallyTop(int x, int y, bool byWhite, ref int fromX, ref int fromY)
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
                if (byWhite)
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
        
        static bool AttackedVerticallyBottom(int x, int y, bool byWhite, ref int fromX, ref int fromY)
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
                if (byWhite)
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
        
        static bool AttackedVertically(int x, int y, bool byWhite, ref int fromX, ref int fromY)
        {
            if (AttackedVerticallyTop(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedVerticallyBottom(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }

        static bool AttackedHorizzontallyLeft(int x, int y, bool byWhite, ref int fromX, ref int fromY)
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
                if (byWhite)
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

        static bool AttackedHorizzontallyRight(int x, int y, bool byWhite, ref int fromX, ref int fromY)
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
                if (byWhite)
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
        
        static bool AttackedHorizzontally(int x, int y, bool byWhite, ref int fromX, ref int fromY)
        {
            if (AttackedHorizzontallyLeft(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedHorizzontallyRight(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }

        static bool AttackedDiagonallyTopLeft(int x, int y, bool byWhite, ref int fromX, ref int fromY)
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
                if (byWhite)
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

        static bool AttackedDiagonallyTopRight(int x, int y, bool byWhite, ref int fromX, ref int fromY)
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
                if (byWhite)
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

        static bool AttackedDiagonallyBottomLeft(int x, int y, bool byWhite, ref int fromX, ref int fromY)
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
                if (byWhite)
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

        static bool AttackedDiagonallyBottomRight(int x, int y, bool byWhite, ref int fromX, ref int fromY)
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
                if (byWhite)
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
        
        static bool AttackedDiagonally(int x, int y, bool byWhite, ref int fromX, ref int fromY)
        {
            if (AttackedDiagonallyTopLeft(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedDiagonallyTopRight(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedDiagonallyBottomLeft(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            if (AttackedDiagonallyBottomRight(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if a square is under attack
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="byWhite">The square is attacked by white</param>
        /// <returns>True if the square is attacked</returns>
        static bool AttackedBy(int x, int y, bool byWhite)
        {
            int fromX = 0, fromY = 0;
            return AttackedBy(x, y, byWhite, ref fromX, ref fromY);
        }

        /// <summary>
        /// Check if a square is under attack
        /// </summary>
        /// <param name="x">Column</param>
        /// <param name="y">Row</param>
        /// <param name="byWhite">The square is attacked by white</param>
        /// <returns>True if the square is attacked</returns>
        static bool AttackedBy(int x, int y, bool byWhite, ref int fromX, ref int fromY)
        {
            // BY PAWN
            if (AttackedByPawn(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            // BY KNIGHT
            if (AttackedByKnight(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            // BY KING
            if (AttackedByKing(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            // VERTICALLY
            if (AttackedVertically(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            // HORIZONTALLY
            if (AttackedHorizzontally(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            // DIAGONALLY
            if (AttackedDiagonally(x, y, byWhite, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }
    }
}
