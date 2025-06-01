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
        //white can castle
        static bool whiteCastleKing = true;
        static bool whiteCastleQueen = true;

        //black can castle
        static bool blackCastleKing = true;
        static bool blackCastleQueen = true;

        static int enPassantX = -1; //where can a pawn capture with en passant
        static int enPassantY = -1;

        /// <summary>
        /// Moves a piece from the start square to the end square, including handling en passant.
        /// </summary>
        /// <param name="startX">Start row</param>
        /// <param name="startY">Start column</param>
        /// <param name="endX">End row</param>
        /// <param name="endY">End column</param>
        /// <param name="white">True if white's turn, false if black's</param>
        /// <returns>True if the move is legal and performed</returns>
        public static bool Move(int startX, int startY, int endX, int endY, bool white, int promoteTo)
        {
            if (LegalMove(startX, startY, endX, endY, white))
            {
                int startPiece = board[startY, startX];
                int endPiece = board[endY, endX];

                bool changedEnPassant = false;

                int diffX = startX - endX;
                int diffY = startY - endY;

                switch (startPiece)
                {
                    case WP:
                        if(diffY == 2)
                        {
                            enPassantX = endX;
                            enPassantY = endY + 1;

                            changedEnPassant = true;
                        }
                        else if(startX != endX && endPiece == _)    //captured with en passant
                        {
                            board[endY + 1, endX] = _;
                        }
                        else if(endY == 0)  //promotion
                        {
                            if(promoteTo >= BP)
                            {
                                return false;
                            }

                            startPiece = promoteTo;
                        }
                        break;

                    case BP:
                        if (diffY == -2)
                        {
                            enPassantX = endX;
                            enPassantY = endY - 1;

                            changedEnPassant = true;
                        }
                        else if (startX != endX && endPiece == _)    //captured with en passant
                        {
                            board[endY - 1, endX] = _;
                        }
                        else if(endY == 7)
                        {
                            if(promoteTo < BP)
                            {
                                return false;
                            }

                            startPiece = promoteTo;
                        }
                        break;

                    case WK:
                        if (diffX == 2)  //queen side castle
                        {
                            //move rook
                            board[endY, endX - 2] = _;
                            board[startY, startX - 1] = WR;
                        }
                        else if (diffX == -2)    //king side castle
                        {
                            //move rook
                            board[endY, endX + 1] = _;
                            board[startY, startX + 1] = WR;
                        }

                        whiteCastleKing = false;
                        whiteCastleQueen = false;
                        break;

                    case BK:
                        if (diffX == 2)  //queen side castle
                        {
                            //move rook
                            board[endY, endX - 2] = _;
                            board[startY, startX - 1] = BR;
                        }
                        else if (diffX == -2)    //king side castel
                        {
                            //move rook
                            board[endY, endX + 1] = _;
                            board[startY, startX + 1] = BR;
                        }

                        blackCastleKing = false;
                        blackCastleQueen = false;
                        break;

                    case WR:
                        if (startY == 7)    //rook on start row
                        {
                            if (startX == 0)  //queen side rook
                            {
                                whiteCastleQueen = false;
                            }
                            else if (startX == 7)   //king side rook
                            {
                                whiteCastleKing = false;
                            }
                        }
                        break;

                    case BR:
                        if (startY == 0)    //rook on start row
                        {
                            if (startX == 0)  //queen or king side rook
                            {
                                blackCastleQueen = false;
                            }
                            else if (startX == 7)   //king side rook
                            {
                                blackCastleKing = false;
                            }
                        }
                        break;
                }

                switch (endPiece)
                {
                    case WR:
                        if (endY == 7) //rook on start row
                        {
                            if (endX == 0)  //queen side rook
                            {
                                whiteCastleQueen = false;
                            }
                            else if (endX == 7) //king side rook
                            {
                                whiteCastleKing = false;
                            }
                        }
                        break;

                    case BR:
                        if (endY == 0) //rook on start row
                        {
                            if (endX == 0)  //queen side rook
                            {
                                blackCastleQueen = false;
                            }
                            else if (endX == 7)  //king side rook
                            {
                                blackCastleKing = false;
                            }
                        }
                        break;
                }

                if(!changedEnPassant)
                {
                    enPassantX = -1;
                    enPassantY = -1;
                }

                board[endY, endX] = startPiece;
                board[startY, startX] = _;

                return true;
            }

            return false;
        }

        static void UndoMove(int startX, int startY, int endX, int endY, int startPiece, int endPiece, bool oldWhiteCastleKing, bool oldWhiteCastleQueen, bool oldBlackCastleKing, bool oldBlackCastleQueen, int oldEnPassantX, int oldEnPassantY)
        {
            int diffX = startX - endX;

            switch (startPiece)
            {
                case WP:
                    if(endX == oldEnPassantX && endY == oldEnPassantY) //captured with en passant
                    {
                        board[endY + 1, endX] = BP;
                    }
                    break;

                case BP:
                    if (endX == oldEnPassantX && endY == oldEnPassantY) //captured with en passant
                    {
                        board[endY - 1, endX] = WP;
                    }
                    break;

                case WK:
                    if (diffX == 2)  //queen side castle
                    {
                        board[endY, endX - 2] = WR;
                        board[startY, startX - 1] = _;
                    }
                    else if (diffX == -2)    //king side castel
                    {
                        board[endY, endX + 1] = WR;
                        board[startY, startX + 1] = _;
                    }
                    break;

                case BK:
                    if (diffX == 2)  //queen side castle
                    {
                        board[endY, endX - 2] = BR;
                        board[startY, startX - 1] = _;
                    }
                    else if (diffX == -2)    //king side castel
                    {
                        board[endY, endX + 1] = BR;
                        board[startY, startX + 1] = _;
                    }
                    break;
            }

            whiteCastleKing = oldWhiteCastleKing;
            whiteCastleQueen = oldWhiteCastleQueen;

            blackCastleKing = oldBlackCastleKing;
            blackCastleQueen = oldBlackCastleQueen;

            enPassantX = oldEnPassantX;
            enPassantY = oldEnPassantY;

            board[startY, startX] = startPiece;
            board[endY, endX] = endPiece;
        }

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startY">starting x position</param>
        /// <param name="startX">starting y position</param>
        /// <param name="endX">ending x position</param>
        /// <param name="endY">ending y position</param>
        /// <param name="white">False if the current move is made by black and True if it is made by white</param>
        /// <returns>if the move is legal</returns>
        static bool LegalMove(int startX, int startY, int endX, int endY, bool white)
        {
            if (startX < 0 || startY < 0 || startX > 7 || startY > 7)    //start square is not inside the board
            {
                return false;
            }
            if (endX < 0 || endY < 0 || endX > 7 || endY > 7)   //end square is not inside the 
            {
                return false;
            }

            if (startX == endX && startY == endY)    //the piece remeins on the same square
            {
                return false;
            }

            int startPiece = board[startY, startX];
            int endPiece = board[endY, endX];

            bool oldWhiteCastleKing = whiteCastleKing, oldWhiteCastleQueen = whiteCastleQueen;
            bool oldBlackCastleKing = blackCastleKing, oldBlackCastleQueen = blackCastleQueen;

            int oldEnPassantX = enPassantX, oldEnPassantY = enPassantY;

            if (startPiece == _) //a piece is beeing moved
            {
                return false;
            }

            if (white)   //the piece is of the color that needs to play
            {
                if (startPiece >= BP)
                {
                    return false;
                }
            }
            else
            {
                if (startPiece < BP)
                {
                    return false;
                }
            }

            if (startPiece < _ && startPiece > BK)  //piece to move doesn't exist
            {
                return false;
            }
            if (endPiece < _ && endPiece > BK)  //piece to move into doesn't exist
            {
                return false;
            }

            if (endPiece != _)  //end piece can be captured
            {
                if (white)
                {
                    if (endPiece < BP)
                    {
                        return false;
                    }
                }
                else
                {
                    if (endPiece >= BP)
                    {
                        return false;
                    }
                }
            }

            //check if it is a valid move for the type of piece
            if (startPiece == WP || startPiece == BP)
            {
                if (!LegalPawnMove(startX, startY, endX, endY, white))
                {
                    return false;
                }
            }
            else if (startPiece == WN || startPiece == BN)
            {
                if (!LegalKnightMove(startX, startY, endX, endY, white))
                {
                    return false;
                }
            }
            else if (startPiece == WK || startPiece == BK)
            {
                if (!LegalKingMove(startX, startY, endX, endY, white))
                {
                    return false;
                }
            }
            else
            {
                if (startX == endX || startY == endY)    //sliding move
                {
                    if (!LegalSlidingMove(startX, startY, endX, endY, white))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!LegalDiagonalMove(startX, startY, endX, endY, white))
                    {
                        return false;
                    }
                }
            }

            //check if the move causes check to the king of the current playing color
            board[startY, startX] = _;
            board[endY, endX] = startPiece;

            bool causesCheck = UnderCheck(white);

            UndoMove(startX, startY, endX, endY, startPiece, endPiece, oldWhiteCastleKing, oldWhiteCastleQueen, oldBlackCastleKing, oldBlackCastleQueen, oldEnPassantX, oldEnPassantY);

            if (!causesCheck)
            {
                return true;
            }

            return false;
        }

        static bool LegalPawnMove(int startX, int startY, int endX, int endY, bool white)
        {
            if (white)
            {
                if (startX == endX)
                {
                    if (startY == 6 && endY == 4)
                    {
                        if (board[startY - 1, startX] == _ && board[endY, endX] == _)
                        {
                            enPassantX = startX;
                            enPassantY = startY - 1;

                            return true;
                        }
                    }
                    else if (startY - endY == 1)
                    {
                        return true;
                    }
                }
                else
                {
                    if (board[endY, endX] != _)
                    {
                        if (startX == endX - 1 || startX == endX + 1)
                        {
                            if (startY - endY == 1)
                            {
                                return true;
                            }
                        }
                    }
                    else if(endX == enPassantX && endY == enPassantY && (startX == endX - 1 || startX == endX + 1))
                    {
                        enPassantX = -1;
                        enPassantY = -1;

                        board[endY + 1, endX] = _;

                        return true;
                    }
                }
            }
            else
            {
                if (startX == endX)
                {
                    if (startY == 1 && endY == 3)
                    {
                        if (board[startY + 1, startX] == _ && board[endY, endX] == _)
                        {
                            enPassantX = startX;
                            enPassantY = startY + 1;

                            return true;
                        }
                    }
                    else if (startY - endY == -1)
                    {
                        return true;
                    }
                }
                else
                {
                    if (board[endY, endX] != _)
                    {
                        if (startX == endX - 1 || startX == endX + 1)
                        {
                            if (startY - endY == -1)
                            {
                                return true;
                            }
                        }
                    }
                    else if (endX == enPassantX && endY == enPassantY && (startX == endX - 1 || startX == endX + 1))
                    {
                        enPassantX = -1;
                        enPassantY = -1;

                        board[endY - 1, endX] = _;

                        return true;
                    }
                }
            }

            return false;
        }

        static bool LegalKnightMove(int startX, int startY, int endX, int endY, bool white)
        {
            int diffX = startX - endX;
            int diffY = startY - endY;

            if (diffX < 0)
            {
                diffX = -diffX;
            }
            if (diffY < 0)
            {
                diffY = -diffY;
            }

            if (diffX == 2 && diffY == 1)
            {
                return true;
            }
            else if (diffX == 1 && diffY == 2)
            {
                return true;
            }

            return false;
        }

        static bool LegalCastle(int startX, int startY, int endX, int endY, bool white)
        {
            int x = startX, y = startY;

            if (AttackedBy(startX, startY, !white))
            {
                return false;
            }

            if (white)
            {
                if (board[startY, startX] != WK)
                {
                    return false;
                }

                int diffX = startX - endX;

                if (diffX > 0)   //queen side castle
                {
                    if (!whiteCastleQueen)
                    {
                        return false;
                    }

                    do
                    {
                        x--;

                        if (board[y, x] != _ || AttackedBy(x, y, !white))
                        {
                            return false;
                        }

                    } while (x != endX);

                    //move rook
                    board[endY, endX - 2] = _;
                    board[startY, startX - 1] = WR;
                }
                else    //king side castle
                {
                    if (!whiteCastleKing)
                    {
                        return false;
                    }

                    do
                    {
                        x++;

                        if (board[y, x] != _ || AttackedBy(x, y, !white))
                        {
                            return false;
                        }

                    } while (x != endX);

                    //move rook
                    board[endY, endX + 1] = _;
                    board[startY, startX + 1] = WR;
                }
            }
            else
            {
                if (board[startY, startX] != BK)
                {
                    return false;
                }

                int diffX = startX - endX;

                if (diffX > 0)   //queen side castle
                {
                    if (!blackCastleQueen)
                    {
                        return false;
                    }

                    do
                    {
                        x--;

                        if (board[y, x] != _ || AttackedBy(x, y, !white))
                        {
                            return false;
                        }

                    } while (x != endX);

                    //move rook
                    board[endY, endX - 2] = _;
                    board[startY, startX - 1] = BR;
                }
                else    //king side castle
                {
                    if (!blackCastleKing)
                    {
                        return false;
                    }

                    do
                    {
                        x++;

                        if (board[y, x] != _ || AttackedBy(x, y, !white))
                        {
                            return false;
                        }

                    } while (x != endX);

                    //move rook
                    board[endY, endX + 1] = _;
                    board[startY, startX + 1] = BR;
                }
            }

            return true;
        }

        static bool LegalKingMove(int startX, int startY, int endX, int endY, bool white)
        {

            int diffX = startX - endX;
            int diffY = startY - endY;

            if (diffX < 0)
            {
                diffX = -diffX;
            }
            if (diffY < 0)
            {
                diffY = -diffY;
            }

            if (diffX == 2 && diffY == 0)    //castle move
            {
                return LegalCastle(startX, startY, endX, endY, white);
            }

            if ((diffX == 0 || diffX == 1) && (diffY == 0 || diffY == 1))
            {
                if (white)
                {
                    whiteCastleKing = false;
                    whiteCastleQueen = false;
                }
                else
                {
                    blackCastleKing = false;
                    blackCastleQueen = false;
                }

                return true;
            }

            return false;
        }

        static bool LegalSlidingMove(int startX, int startY, int endX, int endY, bool white)
        {
            int x = startX, y = startY;

            if (white)
            {
                if (board[startY, startX] != WR && board[startY, startX] != WQ)
                {
                    return false;
                }
            }
            else
            {
                if (board[startY, startX] != BR && board[startY, startX] != BQ)
                {
                    return false;
                }
            }

            if (startX == endX)
            {
                if (startY < endY)
                {
                    do
                    {
                        y++;

                        if (y == endY)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
                else
                {
                    do
                    {
                        y--;

                        if (y == endY)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
            }
            else
            {
                if (startX < endX)
                {
                    do
                    {
                        x++;

                        if (x == endX)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
                else
                {
                    do
                    {
                        x--;

                        if (x == endX)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
            }

            return false;
        }

        static bool LegalDiagonalMove(int startX, int startY, int endX, int endY, bool white)
        {
            int x = startX, y = startY;

            int diffX = startX - endX;
            int diffY = startY - endY;

            if (diffX < 0)
            {
                diffX = -diffX;
            }
            if (diffY < 0)
            {
                diffY = -diffY;
            }

            if (diffX != diffY)  //not a diagonal move
            {
                return false;
            }

            if (white)
            {
                if (board[startY, startX] != WB && board[startY, startX] != WQ)
                {
                    return false;
                }
            }
            else
            {
                if (board[startY, startX] != BB && board[startY, startX] != BQ)
                {
                    return false;
                }
            }

            if (startY < endY)
            {
                if (startX < endX)
                {
                    do
                    {
                        y++;
                        x++;

                        if (x == endX && y == endY)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
                else
                {
                    do
                    {
                        y++;
                        x--;

                        if (x == endX && y == endY)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
            }
            else
            {
                if (startX < endX)
                {
                    do
                    {
                        y--;
                        x++;

                        if (x == endX && y == endY)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
                else
                {
                    do
                    {
                        y--;
                        x--;

                        if (x == endX && y == endY)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
            }

            return false;
        }
    }
}
