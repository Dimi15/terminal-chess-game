using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game
{
    public partial class Program
    {
        // Global Variables Declaration
        public const int _ = 0; // Empty Square
        public const int WP = 1; // White Pawn
        public const int WN = 2; // White Knight
        public const int WB = 3; // White Bishop
        public const int WR = 4; // White Rook
        public const int WQ = 5; // White Queen
        public const int WK = 6; // White King
        public const int BP = 7; // Black Pawn
        public const int BN = 8; // Black Knight
        public const int BB = 9; // Black Bishop
        public const int BR = 10; // Black Rook
        public const int BQ = 11; // Black Queen
        public const int BK = 12; // Black King

        public static bool playerWhite = false;
        public static int[,] board = new int[8, 8];

        static void Main(string[] args)
        {
            Program.Minimax();
            Program.Evaluation();
            Program.LegalMove();

            // Tests display function
            setupStartPosition();
            Console.WriteLine(getPosition());
        }

        /// <summary>
        /// Assigns the variables to the matrix for the start position
        /// </summary>
        static void setupStartPosition()
        {
            // CASE FOR BLACK:
            // Assigning to each square the appropriate piece
            board[0, 0] = BR;
            board[0, 1] = BN;
            board[0, 2] = BB;
            board[0, 3] = BQ;
            board[0, 4] = BK;
            board[0, 5] = BB;
            board[0, 6] = BN;
            board[0, 7] = BR;

            // Loop to assign pawns
            for (int i = 0; i < 8; i++)
                board[1, i] = BP;

            // CASE FOR WHITE:
            // Assigning to each square the appropriate piece
            board[7, 0] = WR;
            board[7, 1] = WN;
            board[7, 2] = WB;
            board[7, 3] = WQ;
            board[7, 4] = WK;
            board[7, 5] = WB;
            board[7, 6] = WN;
            board[7, 7] = WR;

            // Loop to assign pawns
            for (int i = 0; i < 8; i++)
                board[6, i] = WP;
        }

        /// <summary>
        /// Gets the current board
        /// </summary>
        /// <returns>The current board position</returns>
        static string getPosition()
        {
            string stringBoard = "";

            // Cycles through rows
            for (int i = 0; i < 8; i++)
            {
                stringBoard += "---------------------------------\n";

                // Cycles through columns
                for (int j = 0; j < 8; j++)
                {
                    stringBoard += "| ";

                    // Matching the correct piece for each number in the matrix
                    switch (board[i, j])
                    {
                        // White pieces
                        case WP:
                            stringBoard += "♙"; break;

                        case WN:
                            stringBoard += "♘"; break;

                        case WB:
                            stringBoard += "♗"; break;

                        case WR:
                            stringBoard += "♖"; break;

                        case WQ:
                            stringBoard += "♕"; break;

                        case WK:
                            stringBoard += "♔"; break;

                        // Black pieces
                        case BP:
                            stringBoard += "♟"; break;

                        case BN:
                            stringBoard += "♞"; break;

                        case BB:
                            stringBoard += "♝"; break;

                        case BR:
                            stringBoard += "♜"; break;

                        case BQ:
                            stringBoard += "♛"; break;

                        case BK:
                            stringBoard += "♔"; break;

                        case _:
                            stringBoard += " "; break;
                    }
                    stringBoard += " ";
                }
                stringBoard += "|\n";
            }
            stringBoard += "---------------------------------";

            return stringBoard;
        }
    }
}
