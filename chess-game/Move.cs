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
        // Castling variables
        static bool whiteCastleKing = false;
        static bool whiteCastleQueen = false;
        static bool blackCastleKing = false;
        static bool blackCastleQueen = false;

        // Coordinates where a pawn can capture with en passant
        static int enPassantX = -1;
        static int enPassantY = -1;

        static int movesDone = 0;   // Used to track the 50-move rule

        /// <summary>
        /// Moves a piece from the start square to the end square, handles en passant too.
        /// </summary>
        /// <param name="startX">Start row</param>
        /// <param name="startY">Start column</param>
        /// <param name="endX">End row</param>
        /// <param name="endY">End column</param>
        /// <param name="isWhiteTurn">True if it is white's turn</param>
        /// <param name="promoteTo">Piece to promote to</param>
        /// <returns>True if the move is legal and performed</returns>
        public static bool Move(int startX, int startY, int endX, int endY, bool isWhiteTurn, int promoteTo)
        {
            if (LegalMove(startX, startY, endX, endY, isWhiteTurn))
            {
                int startPiece = board[startY, startX];
                int endPiece = board[endY, endX];

                bool changedEnPassant = false;

                if (endPiece == _)
                {
                    movesDone++;
                }

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
                        else if(startX != endX && endPiece == _) // Capture with en passant
                        {
                            board[endY + 1, endX] = _;
                        }
                        else if(endY == 0) // Promotion
                        {
                            if(promoteTo >= BP)
                            {
                                return false;
                            }

                            startPiece = promoteTo;
                        }

                        movesDone = 0; // Reset after pawn move

                        break;

                    case BP:
                        if (diffY == -2)
                        {
                            enPassantX = endX;
                            enPassantY = endY - 1;

                            changedEnPassant = true;
                        }
                        else if (startX != endX && endPiece == _) // Capture with en passant
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

                        movesDone = 0; // Reset after pawn move

                        break;

                    case WK:
                        if (diffX == 2) // Queen side castle
                        {
                            // Moves the rook
                            board[endY, endX - 2] = _;
                            board[startY, startX - 1] = WR;
                        }
                        else if (diffX == -2) // King side castle
                        {
                            // Moves the rook
                            board[endY, endX + 1] = _;
                            board[startY, startX + 1] = WR;
                        }

                        whiteCastleKing = false;
                        whiteCastleQueen = false;
                        break;

                    case BK:
                        if (diffX == 2) // Queen side castle
                        {
                            // Moves the rook
                            board[endY, endX - 2] = _;
                            board[startY, startX - 1] = BR;
                        }
                        else if (diffX == -2) // King side castle
                        {
                            // Moves the rook
                            board[endY, endX + 1] = _;
                            board[startY, startX + 1] = BR;
                        }

                        blackCastleKing = false;
                        blackCastleQueen = false;
                        break;

                    case WR:
                        if (startY == 7) // If the rook is on starting row
                        {
                            if (startX == 0) // Queen side rook
                            {
                                whiteCastleQueen = false;
                            }
                            else if (startX == 7) // King side rook
                            {
                                whiteCastleKing = false;
                            }
                        }
                        break;

                    case BR:
                        if (startY == 0) // If the rook is on starting row
                        {
                            if (startX == 0) // Queen side rook
                            {
                                blackCastleQueen = false;
                            }
                            else if (startX == 7) // King side rook
                            {
                                blackCastleKing = false;
                            }
                        }
                        break;
                }

                switch (endPiece)
                {
                    case WR:
                        if (endY == 7) // If the rook is on starting row
                        {
                            if (endX == 0) // Queen side rook
                            {
                                whiteCastleQueen = false;
                            }
                            else if (endX == 7) // King side rook
                            {
                                whiteCastleKing = false;
                            }
                        }
                        break;

                    case BR:
                        if (endY == 0) // If the rook is on starting row
                        {
                            if (endX == 0) // Queen side rook
                            {
                                blackCastleQueen = false;
                            }
                            else if (endX == 7) // King side rook
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

        /// <summary>
        /// Undoes a move given the coordinates
        /// </summary>
        /// <param name="startX">Start row</param>
        /// <param name="startY">Start column</param>
        /// <param name="endX">End row</param>
        /// <param name="endY">End column</param>
        /// <param name="startPiece">Piece that was on coordinate startX</param>
        /// <param name="endPiece">Piece that was on coordinate endX</param>
        /// <param name="oldWhiteCastleKing">State of white castle king side before the move</param>
        /// <param name="oldWhiteCastleQueen">State of white castle queen side before the move</param>
        /// <param name="oldBlackCastleKing">State of white castle king side before the move</param>
        /// <param name="oldBlackCastleQueen">State of white castle queen side before the move</param>
        /// <param name="oldEnPassantX">State of en passant coordinate X before the move</param>
        /// <param name="oldEnPassantY">State of en passant coordinate Y before the move</param>
        /// <param name="oldMovesDone">Needed for 50-move rule</param>
        static void UndoMove(int startX, int startY, int endX, int endY, int startPiece, int endPiece, bool oldWhiteCastleKing, bool oldWhiteCastleQueen, bool oldBlackCastleKing, bool oldBlackCastleQueen, int oldEnPassantX, int oldEnPassantY, int oldMovesDone)
        {
            int diffX = startX - endX;

            switch (startPiece)
            {
                case WP:
                    if(endX == oldEnPassantX && endY == oldEnPassantY) // Captured with en passant
                    {
                        board[endY + 1, endX] = BP;
                    }
                    break;
                
                case BP:
                    if (endX == oldEnPassantX && endY == oldEnPassantY) // Captured with en passant
                    {
                        board[endY - 1, endX] = WP;
                    }
                    break;

                case WK:
                    if (diffX == 2) // Castle queen side
                    {
                        board[endY, endX - 2] = WR;
                        board[startY, startX - 1] = _;
                    }
                    else if (diffX == -2) // Castle king side
                    {
                        board[endY, endX + 1] = WR;
                        board[startY, startX + 1] = _;
                    }
                    break;

                case BK:
                    if (diffX == 2) // Castle queen side
                    {
                        board[endY, endX - 2] = BR;
                        board[startY, startX - 1] = _;
                    }
                    else if (diffX == -2) // Castle king side
                    {
                        board[endY, endX + 1] = BR;
                        board[startY, startX + 1] = _;
                    }
                    break;
            }

            // Updating variables and board state
            whiteCastleKing = oldWhiteCastleKing;
            whiteCastleQueen = oldWhiteCastleQueen;

            blackCastleKing = oldBlackCastleKing;
            blackCastleQueen = oldBlackCastleQueen;

            enPassantX = oldEnPassantX;
            enPassantY = oldEnPassantY;

            movesDone = oldMovesDone;

            board[startY, startX] = startPiece;
            board[endY, endX] = endPiece;
        }

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startX">Start column</param>
        /// <param name="startY">Start row</param>
        /// <param name="endX">End column</param>
        /// <param name="endY">End row</param>
        /// <param name="isWhiteTurn">True if white is making the move</param>
        /// <returns>If the move is legal</returns>
        static bool LegalMove(int startX, int startY, int endX, int endY, bool isWhiteTurn)
        {
            if (startX < 0 || startY < 0 || startX > 7 || startY > 7) // Checks if the start square is not inside the board
            {
                return false;
            }
            if (endX < 0 || endY < 0 || endX > 7 || endY > 7) // Checks if the end square is not inside the matrix
            {
                return false;
            }
            
            if (startX == endX && startY == endY) // Checks if the piece remains on the same square
            {
                return false;
            }

            int startPiece = board[startY, startX];
            int endPiece = board[endY, endX];

            bool oldWhiteCastleKing = whiteCastleKing, oldWhiteCastleQueen = whiteCastleQueen;
            bool oldBlackCastleKing = blackCastleKing, oldBlackCastleQueen = blackCastleQueen;

            int oldEnPassantX = enPassantX, oldEnPassantY = enPassantY;

            int oldMovesDone = movesDone;

            if (startPiece == _) // Checks if a piece is being moved
            {
                return false;
            }

            if (isWhiteTurn) // Checks if the piece is of the color that needs to play
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

            if (startPiece < _ && startPiece > BK) // Checks if the piece to move doesn't exist
            {
                return false;
            }
            if (endPiece < _ && endPiece > BK) // Checks if the piece to move to doesn't exist
            {
                return false;
            }

            if (endPiece != _) // Checks if the end piece can be captured
            {
                if (isWhiteTurn)
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

            // Checks if it is a valid move for the type of piece
            if (startPiece == WP || startPiece == BP)
            {
                if (!LegalPawnMove(startX, startY, endX, endY, isWhiteTurn))
                {
                    return false;
                }
            }
            else if (startPiece == WN || startPiece == BN)
            {
                if (!LegalKnightMove(startX, startY, endX, endY))
                {
                    return false;
                }
            }
            else if (startPiece == WK || startPiece == BK)
            {
                if (!LegalKingMove(startX, startY, endX, endY, isWhiteTurn))
                {
                    return false;
                }
            }
            else
            {
                if (startX == endX || startY == endY) // Checks if it is a sliding move
                {
                    if (!LegalSlidingMove(startX, startY, endX, endY, isWhiteTurn))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!LegalDiagonalMove(startX, startY, endX, endY, isWhiteTurn))
                    {
                        return false;
                    }
                }
            }

            // Checks if the move checks the king of the current playing color
            board[startY, startX] = _;
            board[endY, endX] = startPiece;

            bool causesCheck = UnderCheck(isWhiteTurn);

            UndoMove(startX, startY, endX, endY, startPiece, endPiece, oldWhiteCastleKing, oldWhiteCastleQueen, oldBlackCastleKing, oldBlackCastleQueen, oldEnPassantX, oldEnPassantY, oldMovesDone);

            if (!causesCheck)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startX">Start column</param>
        /// <param name="startY">Start row</param>
        /// <param name="endX">End column</param>
        /// <param name="endY">End row</param>
        /// <param name="isWhiteTurn">True if white is making the move</param>
        /// <returns>If the move is legal</returns>
        static bool LegalPawnMove(int startX, int startY, int endX, int endY, bool isWhiteTurn)
        {
            if (isWhiteTurn)
            {
                if (startX == endX) // Checks if it is moving vertically
                {
                    if (startY == 6 && endY == 4) // Checks if it is the first row and if the pawn moves of 2 squares
                    {
                        if (board[startY - 1, startX] == _ && board[endY, endX] == _) // Checks if 2 squares in front of the pawn are empty
                        {
                            return true;
                        }
                    }
                    else if (startY - endY == 1 && board[endY, endX] == _) // Checks if the pawn moves of at least 1 square and the ending square is empty
                    {
                        return true;
                    }
                }
                else
                {
                    if (board[endY, endX] != _) // Checks if the ending square is not empty
                    {
                        if (startX == endX - 1 || startX == endX + 1) // Checks if the pawn is moving right or left
                        {
                            if (startY - endY == 1) // Checks if the pawn is moving just of 1 square
                            {
                                return true;
                            }
                        }
                    }
                    else if(endX == enPassantX && endY == enPassantY && (startX == endX - 1 || startX == endX + 1) && startY - endY == 1) // Checks if the pawn is moving to an en passant square
                    {
                        board[endY + 1, endX] = _;
                        return true;
                    }
                }
            }
            else
            {
                if (startX == endX) // Checks if it is moving vertically
                {
                    if (startY == 1 && endY == 3) // Checks if it is the first row and if the pawn moves of 2 squares
                    {
                        if (board[startY + 1, startX] == _ && board[endY, endX] == _) // Checks if 2 squares in front of the pawn are empty
                        {
                            return true;
                        }
                    }
                    else if (startY - endY == -1 && board[endY, endX] == _) // Checks if the pawn moves of at least 1 square and the ending square is empty
                    {
                        return true;
                    }
                }
                else
                {
                    if (board[endY, endX] != _) // Checks if the ending square is not empty
                    {
                        if (startX == endX - 1 || startX == endX + 1) // Checks if the pawn is moving right or left
                        {
                            if (startY - endY == -1) // Checks if the pawn is moving just of 1 square
                            {
                                return true;
                            }
                        }
                    }
                    else if (endX == enPassantX && endY == enPassantY && (startX == endX - 1 || startX == endX + 1) && startY - endY == -1) // Checks if the pawn is moving to an en passant square
                    {
                        board[endY - 1, endX] = _;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startX">Start column</param>
        /// <param name="startY">Start row</param>
        /// <param name="endX">End column</param>
        /// <param name="endY">End row</param>
        /// <returns>If the move is legal</returns>
        static bool LegalKnightMove(int startX, int startY, int endX, int endY)
        {
            /* Calculates the difference between the coordinates X and Y, and does the absolute value of that,
               and then checks if it is moving in a direction of 2 squares and in the other of 1 square */
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

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startX">Start column</param>
        /// <param name="startY">Start row</param>
        /// <param name="endX">End column</param>
        /// <param name="endY">End row</param>
        /// <param name="isWhiteTurn">True if white is making the move</param>
        /// <returns>If the move is legal</returns>
        static bool LegalCastle(int startX, int startY, int endX, int endY, bool isWhiteTurn)
        {
            int x = startX, y = startY;
            
            if (AttackedBy(startX, startY, !isWhiteTurn))
            {
                return false;
            }

            if (isWhiteTurn)
            {
                if (startX != 4 || startY != 7) // Checks if it is not in its starting position
                {
                    return false;
                }
                
                if (board[startY, startX] != WK) // Checks if it is not a king
                {
                    return false;
                }

                int diffX = startX - endX;

                if (diffX > 0) // Castle queen side
                {
                    if (!whiteCastleQueen)
                    {
                        return false;
                    }

                    do
                    {
                        x--;

                        if (board[y, x] != _ || AttackedBy(x, y, !isWhiteTurn))
                        {
                            return false;
                        }

                    } while (x != endX);

                    // Moves the rook
                    board[endY, endX - 2] = _;
                    board[startY, startX - 1] = WR;
                }
                else // Castle king side
                {
                    if (!whiteCastleKing)
                    {
                        return false;
                    }

                    do
                    {
                        x++;

                        if (board[y, x] != _ || AttackedBy(x, y, !isWhiteTurn))
                        {
                            return false;
                        }

                    } while (x != endX);

                    // Moves the rook
                    board[endY, endX + 1] = _;
                    board[startY, startX + 1] = WR;
                }
            }
            else
            {
                if (startX != 4 || startY != 0) // Checks if it is not in its starting position
                {
                    return false;
                }
                
                if (board[startY, startX] != BK) // Checks if it is not a king
                {
                    return false;
                }

                int diffX = startX - endX;

                if (diffX > 0) // Castle queen side
                {
                    if (!blackCastleQueen)
                    {
                        return false;
                    }

                    do
                    {
                        x--;

                        if (board[y, x] != _ || AttackedBy(x, y, !isWhiteTurn))
                        {
                            return false;
                        }

                    } while (x != endX);

                    // Moves the rook
                    board[endY, endX - 2] = _;
                    board[startY, startX - 1] = BR;
                }
                else // Castle king side
                {
                    if (!blackCastleKing)
                    {
                        return false;
                    }

                    do
                    {
                        x++;

                        if (board[y, x] != _ || AttackedBy(x, y, !isWhiteTurn))
                        {
                            return false;
                        }

                    } while (x != endX);

                    // Moves the rook
                    board[endY, endX + 1] = _;
                    board[startY, startX + 1] = BR;
                }
            }

            return true;
        }

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startX">Start column</param>
        /// <param name="startY">Start row</param>
        /// <param name="endX">End column</param>
        /// <param name="endY">End row</param>
        /// <param name="isWhiteTurn">True if white is making the move</param>
        /// <returns>If the move is legal</returns>
        static bool LegalKingMove(int startX, int startY, int endX, int endY, bool isWhiteTurn)
        {
            /* Calculates the difference between the coordinates X and Y, and does the absolute value of that,
               and then checks if it is moving in a direction of 2 squares and in the other of 0 squares (in case of castling),
               or if it moves in any direction of 1 square the move is legal */
            
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

            if (diffX == 2 && diffY == 0)
            {
                return LegalCastle(startX, startY, endX, endY, isWhiteTurn);
            }

            if ((diffX == 0 || diffX == 1) && (diffY == 0 || diffY == 1))
            {
                if (isWhiteTurn)
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

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startX">Start column</param>
        /// <param name="startY">Start row</param>
        /// <param name="endX">End column</param>
        /// <param name="endY">End row</param>
        /// <param name="isWhiteTurn">True if white is making the move</param>
        /// <returns>If the move is legal</returns>
        static bool LegalSlidingMove(int startX, int startY, int endX, int endY, bool isWhiteTurn)
        {
            int x = startX;
            int y = startY;

            if (isWhiteTurn)
            {
                if (board[startY, startX] != WR && board[startY, startX] != WQ) // Checks if the white piece can move both vertically and horizontally
                {
                    return false;
                }
            }
            else
            {
                if (board[startY, startX] != BR && board[startY, startX] != BQ) // Checks if the black piece can move both vertically and horizontally
                {
                    return false;
                }
            }

            if (startX == endX) // Checks if the piece is moving vertically
            {
                if (startY < endY) // Checks if the piece is moving downwards
                {
                    // Continues until the piece arrives to the destination square or until it finds a piece
                    do
                    {
                        y++;

                        if (y == endY)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
                else // Checks if the piece is moving upwards
                {
                    // Continues until the piece arrives to the destination square or until it finds a piece
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
                if (startX < endX) // Checks if the piece is moving towards the right
                {
                    // Continues until the piece arrives to the destination square or until it finds a piece
                    do
                    {
                        x++;

                        if (x == endX)
                        {
                            return true;
                        }

                    } while (board[y, x] == _);
                }
                else  // Checks if the piece is moving towards the left
                {
                    // Continues until the piece arrives to the destination square or until it finds a piece
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

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <param name="startX">Start column</param>
        /// <param name="startY">Start row</param>
        /// <param name="endX">End column</param>
        /// <param name="endY">End row</param>
        /// <param name="isWhiteTurn">True if white is making the move</param>
        /// <returns>If the move is legal</returns>
        static bool LegalDiagonalMove(int startX, int startY, int endX, int endY, bool isWhiteTurn)
        {
            int x = startX, y = startY;

            // Calculates the differene and does the absolute value
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

            if (diffX != diffY) // Not a diagonal move
            {
                return false;
            }

            if (isWhiteTurn)
            {
                if (board[startY, startX] != WB && board[startY, startX] != WQ) // Checks if the white piece can move diagonally
                {
                    return false;
                }
            }
            else
            {
                if (board[startY, startX] != BB && board[startY, startX] != BQ) // Checks if the black piece can move diagonally
                {
                    return false;
                }
            }

            if (startY < endY)  // Checks if the piece is moving downwards
            {
                if (startX < endX) // Checks if the piece is moving towards the right
                {
                    // Continues until the piece arrives to the destination square or until it finds a piece
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
                else // Checks if the piece is moving towards the left
                {
                    // Continues until the piece arrives to the destination square or until it finds a piece
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
            else // Checks if the piece is moving upwards
            {
                if (startX < endX) // Checks if the piece is moving towards the right
                {
                    // Continues until the piece arrives to the destination square or until it finds a piece
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
                else // Checks if the piece is moving towards the left
                {
                    // Continues until the piece arrives to the destination square or until it finds a piece
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
