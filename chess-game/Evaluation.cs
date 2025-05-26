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
        static bool Checkmate(ref bool draw, bool white)
        {
            int kingX = 0, kingY = 0;
            int attackingPieceX = 0, attackingPieceY = 0;

            if (!UnderCheck(white, ref kingX, ref kingY, ref attackingPieceX, ref attackingPieceY))
            {
                return false;
            }

            int king = board[kingY, kingX];
            int attackingPiece = board[attackingPieceY, attackingPieceX];

            bool checkmate = true;

            int startPiece = _, endPiece = _;
            int endX = 0, endY = 0;

            //king can move out of the check
            startPiece = king;

            //top
            if (kingY > 0)
            {
                //left
                if (kingX > 0)
                {
                    endPiece = board[kingY - 1, kingX - 1];

                    if (Move(kingX, kingY, kingX - 1, kingY - 1))
                    {
                        checkmate = UnderCheck(white);

                        board[kingY, kingX] = startPiece;
                        board[kingY - 1, kingX - 1] = endPiece;

                        if (!checkmate)
                        {
                            return false;
                        }
                    }
                }

                endPiece = board[kingY - 1, kingX];

                if (Move(kingX, kingY, kingX, kingY - 1))
                {
                    checkmate = UnderCheck(white);

                    board[kingY, kingX] = startPiece;
                    board[kingY - 1, kingX] = endPiece;

                    if (!checkmate)
                    {
                        return false;
                    }
                }

                //right
                if (kingX > 0)
                {
                    endPiece = board[kingY - 1, kingX + 1];

                    if (Move(kingX, kingY, kingX + 1, kingY - 1))
                    {
                        checkmate = UnderCheck(white);

                        board[kingY, kingX] = startPiece;
                        board[kingY - 1, kingX + 1] = endPiece;

                        if (!checkmate)
                        {
                            return false;
                        }
                    }
                }
            }

            //left
            if (kingX > 0)
            {
                endPiece = board[kingY, kingX - 1];

                if (Move(kingX, kingY, kingX - 1, kingY))
                {
                    checkmate = UnderCheck(white);

                    board[kingY, kingX] = startPiece;
                    board[kingY, kingX - 1] = endPiece;

                    if (!checkmate)
                    {
                        return false;
                    }
                }
            }

            //right
            if (kingX < 7)
            {
                endPiece = board[kingY, kingX + 1];

                if (Move(kingX, kingY, kingX + 1, kingY))
                {
                    checkmate = UnderCheck(white);

                    board[kingY, kingX] = startPiece;
                    board[kingY, kingX + 1] = endPiece;

                    if (!checkmate)
                    {
                        return false;
                    }
                }
            }

            //bottom
            if (kingY < 7)
            {
                //left
                if (kingX > 0)
                {
                    endPiece = board[kingY + 1, kingX - 1];

                    if (Move(kingX, kingY, kingX - 1, kingY + 1))
                    {
                        checkmate = UnderCheck(white);

                        board[kingY, kingX] = startPiece;
                        board[kingY + 1, kingX - 1] = endPiece;

                        if (!checkmate)
                        {
                            return false;
                        }
                    }
                }

                endPiece = board[kingY + 1, kingX];

                if (Move(kingX, kingY, kingX, kingY + 1))
                {
                    checkmate = UnderCheck(white);

                    board[kingY, kingX] = startPiece;
                    board[kingY + 1, kingX] = endPiece;

                    if (!checkmate)
                    {
                        return false;
                    }
                }

                //right
                if (kingX > 0)
                {
                    endPiece = board[kingY + 1, kingX + 1];

                    if (Move(kingX, kingY, kingX + 1, kingY + 1))
                    {
                        checkmate = UnderCheck(white);

                        board[kingY, kingX] = startPiece;
                        board[kingY + 1, kingX + 1] = endPiece;

                        if (!checkmate)
                        {
                            return false;
                        }
                    }
                }
            }

            int incrementX = 0, incrementY = 0;

            if (attackingPiece == WN || attackingPiece == BN)
            {
                incrementX = kingX - attackingPieceX;
                incrementY = kingY - attackingPieceY;
            }
            else
            {
                if (attackingPieceX != kingX)
                {
                    if (attackingPieceX < kingX)
                    {
                        incrementX = 1;
                    }
                    else
                    {
                        incrementY = -1;
                    }
                }

                if (attackingPieceY != kingY)
                {
                    if (attackingPieceY < kingY)
                    {
                        incrementY = 1;
                    }
                    else
                    {
                        incrementY = -1;
                    }
                }
            }

            int currentX = attackingPieceX, currentY = attackingPieceY;

            do
            {
                //TODO: move pieces to current square

                

                checkmate = UnderCheck(white);

                if(!checkmate)
                {
                    return false;
                }

                currentX += incrementX;
                currentY += incrementY;
            } while (currentX != kingX || currentY != kingY);

            return checkmate;
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
        /// Check if the king is under check
        /// </summary>
        /// <param name="white">if it is the white king</param>
        /// <param name="kingX">The X of the king</param>
        /// <param name="kingY">The Y of the king</param>
        /// <param name="pieceX">The X of the first piece found that's giving the check</param>
        /// <param name="pieceY">The Y of the first piece found that's giving the check</param>
        /// <returns>if the king is under check</returns>
        static bool UnderCheck(bool white, ref int kingX, ref int kingY, ref int pieceX, ref int pieceY)
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
                            kingX = j;
                            kingY = i;

                            if (AttachedBy(j, i, false, ref pieceX, ref pieceY))
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

                            if (AttachedBy(j, i, true, ref pieceX, ref pieceY))
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

        static bool AttackedByPawn(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            //left
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

            //right
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

                //right
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

                //right
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

                //bottom
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

                //bottom
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
            //top
            if (y > 0)
            {
                //left
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

                //midle
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

                //right
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

            //left
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

            //right
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

            //bottom
            if (y < 7)
            {
                //left
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

                //right
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

            //top
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
            //bottom
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
            if(AttackedVerticallyTop(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            if(AttackedVerticallyBottom(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }

        static bool AttackedHorizzontallyLeft(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            //left
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
            //right
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
            if(AttackedHorizzontallyLeft(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            if(AttackedHorizzontallyRight(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }

        static bool AttackedDiagonallyTopLeft(int x, int y, bool white, ref int fromX, ref int fromY)
        {
            //top left
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
            //top right
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
            //bottom left
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
            //bottom right
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
            if(AttackedDiagonallyTopLeft(x, y, white, ref fromX, ref fromY))
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
        static bool AttachedBy(int x, int y, bool white)
        {
            int fromX = 0, fromY = 0;

            return AttachedBy(x, y, white, ref fromX, ref fromY);
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
            //BY PAWN
            if(AttackedByPawn(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            //BY KNIGHT
            if (AttackedByKnight(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            //BY KING
            if(AttackedByKing(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            //VERTICALLY
            if(AttackedVertically(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            //HORIZONTALLY
            if(AttackedHorizzontally(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }                     

            //DIAGONALLY
            if(AttackedDiagonally(x, y, white, ref fromX, ref fromY))
            {
                return true;
            }

            return false;
        }
    }
}
