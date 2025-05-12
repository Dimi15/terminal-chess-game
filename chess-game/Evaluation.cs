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
        public static double Evaluation()
        {
            int evaluation = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // PIECES VALUE
                    // NOTE: all pieces value are multiplied by 100 to avoid floating point precision errors
                    //
                    // Pawn:    1
                    // Knight:  3
                    // Bishop:  3
                    // Rook:    5
                    // Queen:   9
                    switch (board[i, j])
                    {
                        //white piece
                        case WP:
                            evaluation += 100; break;

                        case WN:
                            evaluation += 300; break;

                        case WB:
                            evaluation += 300; break;

                        case WR:
                            evaluation += 500; break;

                        case WQ:
                            evaluation += 900; break;

                        //black piece
                        case BP:
                            evaluation -= 100; break;

                        case BN:
                            evaluation -= 300; break;

                        case BB:
                            evaluation -= 300; break;

                        case BR:
                            evaluation -= 500; break;

                        case BQ:
                            evaluation -= 900; break;
                    }

                    int positionGain = 0;

                    if (board[i, j] == WK)  //white king
                    {
                        // for the white king
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
                        else if(i < 5 && (j == 3 || j == 4))
                        {
                            positionGain += -50;
                        }
                        else if (i == 5)
                        {
                            positionGain += -20;
                        }
                        else if(i == 6 && (j == 0 || j == 1 || j == 6 || j == 7))
                        {
                            positionGain += 20;
                        }
                        else if(i == 7 && (j == 0 || j == 7))
                        {
                            positionGain += 20;
                        }
                        else if(i == 7 && (j == 1 || j == 6))
                        {
                            positionGain += 30;
                        }
                        else if(i == 7 && (j == 2 || j == 5))
                        {
                            positionGain += 10;
                        }
                    }
                    else if (board[i, j] == BK) //black king
                    {
                        // for the black king
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
                        // for all other pieces
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
                        else if((i == 0 || i == 7) || (j == 0 || j == 7))
                        {
                            positionGain += -40;
                        }
                        else if((i == 1 || i == 6) && (j == 1 || j == 6))
                        {
                            positionGain += -20;
                        }
                        else if((i == 2 || i == 5) && (j == 2 || j == 5))
                        {
                            positionGain += 10;
                        }
                        else if((i == 2 || i == 5) || (j == 2 || j == 5))
                        {
                            positionGain += 20;
                        }
                        else if((i == 3 || i == 4) && (j == 3 || j == 4))
                        {
                            positionGain += 25;
                        }
                    }

                    if (board[i, j] < BP)   //true if white piece
                    {
                        evaluation += positionGain;
                    }
                    else
                    {
                        evaluation -= positionGain;
                    }
                }
            }

            return evaluation / 100.0;  //divide by 100 to correct for the multiplication of 100 of all value
        }

        /// <summary>
        /// Check if game ended
        /// </summary>
        public static void GameEnded(ref bool draw, ref bool whiteWon, ref bool blackWon)
        {

        }

        /// <summary>
        /// Check if the king is under check
        /// </summary>
        /// <param name="white">if it is the white king</param>
        /// <returns>if the king is under check</returns>
        public static bool UnderCheck(bool white)
        {
            //find king location
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (white)
                    {
                        if (board[i, j] == WK)
                        {
                            if (AttachedBy(j, i, false))
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
                            if (AttachedBy(j, i, true))
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

            return false;   //king not found
        }

        /// <summary>
        /// Check if a square is under attack
        /// </summary>
        /// <param name="white">The square is attacked by white</param>
        /// <returns>true if the square is attacked</returns>
        static bool AttachedBy(int x, int y, bool white)
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
