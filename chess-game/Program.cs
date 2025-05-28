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

        public static int[,] board = new int[8, 8];

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // Needed to display the pieces

            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 0;
            int bestStartX = 0;
            int bestStartY = 0;
            int bestEndX = 0;
            int bestEndY = 0;

            bool blackWhite = false;
            bool whiteCheck = false;
            bool blackCheck = false;
            bool whiteCheckmate = false;
            bool blackCheckmate = false;
            bool playerWhite = false;
            bool legal = true;
            bool draw = false;

            char pickedColor = ' ';

            // Asks the color of the player
            do
            {
                Console.WriteLine("Do you want to play Black or White? (B/W)");
                pickedColor = Convert.ToChar(Console.ReadLine().ToLower());
            } while (pickedColor != 'w' && pickedColor != 'b');

            if (pickedColor == 'w')
            {
                playerWhite = true;
            }

            // Shows starting board depending on the color of the player
            SetupStartPosition();
            GetPosition();

            // Exits the loop when the game ends
            do
            {
                blackWhite = !blackWhite;

                // Iterates until the player hasn't chosen a legal move
                if (blackWhite)
                {
                    do
                    {
                        Console.Clear();
                        GetPosition();

                        Console.WriteLine("Piece to move:\nRow:");
                        startY = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Column:");
                        startX = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Square to move:\nRow:");
                        endY = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Column:");
                        endX = Convert.ToInt32(Console.ReadLine());

                    } while (!Program.Move(startX, startY, endX, endY, blackWhite));
                }

                // Check if the game has ended
                if (Program.Checkmate(!playerWhite, ref draw))
                {
                    break;
                }
                else if (draw)
                {
                    break;
                }

                /*
                Console.Clear();
                GetPosition();

                 Turn of the computer
                Program.Minimax(2, ref bestStartX, ref bestStartY, ref bestEndX, ref bestEndY);
                Program.Move(bestStartX, bestStartY, bestEndX, bestEndY, blackWhite);
                */

                // Check if the game has ended
                if (Program.Checkmate(playerWhite, ref draw))
                {
                    break;
                }
                else if (draw)
                {
                    break;
                }

                Console.Clear();
                GetPosition();
            } while (true);

            Console.ReadKey();
        }

        /// <summary>
        /// Assigns the variables to the matrix for the start position
        /// </summary>
        static void SetupStartPosition()
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
            {
                board[1, i] = BP;
            }

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
            {
                board[6, i] = WP;
            }
        }

        /// <summary>
        /// Prints the current board
        /// </summary>
        static void GetPosition()
        {
            ConsoleColor defaulBackground = Console.BackgroundColor;
            ConsoleColor defaulForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;

            // Iterates through rows
            for (int i = 0; i < 8; i++)
            {
                // Iterates through columns
                for (int j = 0; j < 8; j++)
                {
                    // Condition to alternate the background of the squares
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                    }

                    // Matching the correct piece for each number in the matrix
                    switch (board[i, j])
                    {
                        // White pieces
                        case WP: Console.Write("♙"); break;
                        case WN: Console.Write("♘"); break;
                        case WB: Console.Write("♗"); break;
                        case WR: Console.Write("♖"); break;
                        case WQ: Console.Write("♕"); break;
                        case WK: Console.Write("♔"); break;

                        // Black pieces
                        case BP: Console.Write("♟"); break;
                        case BN: Console.Write("♞"); break;
                        case BB: Console.Write("♝"); break;
                        case BR: Console.Write("♜"); break;
                        case BQ: Console.Write("♛"); break;
                        case BK: Console.Write("♚"); break;
                        case _: Console.Write(" "); break;
                    }

                    Console.Write(' ');
                }

                Console.BackgroundColor = defaulBackground;
                Console.Write("\n");
            }

            Console.ForegroundColor = defaulForeground;
        }
    }
}