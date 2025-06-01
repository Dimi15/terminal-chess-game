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

            int playerPromoteTo = _, computerPromoteTo = _;

            int bestStartX = 0;
            int bestStartY = 0;
            int bestEndX = 0;
            int bestEndY = 0;

            bool blackWhite = false; // If false it's black turn
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
                playerPromoteTo = WQ;
                computerPromoteTo = BQ;
            }
            else
            {
                playerPromoteTo = BQ;
                computerPromoteTo = WQ;
            }

            // Shows starting board depending on the color of the player
            SetupStartPosition();
            //GetPosition(playerWhite);

            Move(4, 6, 4, 4, true, _);
            Move(4, 4, 4, 3, true, _);
            Move(3, 1, 3, 3, false, _);

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
                        GetPosition(playerWhite);

                        Console.WriteLine("Piece to move:\nRow:");
                        startY = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Column:");
                        startX = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Square to move:\nRow:");
                        endY = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Column:");
                        endX = Convert.ToInt32(Console.ReadLine());

                    } while (!Program.Move(startX, startY, endX, endY, playerWhite, playerPromoteTo));
                }

                //Check if the game has ended
                if (Program.Checkmate(!playerWhite, ref draw))
                {
                    Console.WriteLine("You won!");
                    break;
                }
                else if (draw)
                {
                    Console.WriteLine("Tie!");
                    break;
                }

                //Console.Clear();
                //GetPosition(playerWhite);

                blackWhite = !blackWhite;

                // Turn of the computer
                Minimax(5, double.NegativeInfinity, double.PositiveInfinity, ref bestStartX, ref bestStartY, ref bestEndX, ref bestEndY, true, playerWhite);
                Move(bestStartX, bestStartY, bestEndX, bestEndY, !playerWhite, computerPromoteTo);

                // Checks if Pawn Promotion is available
                //CheckPromotion(endX, endY, playerWhite, blackWhite);

                // Checks if the game has ended
                if (Checkmate(playerWhite, ref draw))
                {
                    Console.WriteLine("You lost!");
                    break;
                }
                if (draw)
                {
                    Console.WriteLine("Tie!");
                    break;
                }

                //Console.Clear();
                //GetPosition(playerWhite);
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
        /// Prints one square of the current board
        /// </summary>
        static void WriteSquare(int x, int y)
        {
            // Condition to alternate the background of the squares
            if (y % 2 == 0)
            {
                if (x % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
            }
            else
            {
                if (x % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
            }

            // Matching the correct piece for each number in the matrix
            switch (board[y, x])
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

        /// <summary>
        /// Prints the current board
        /// </summary>
        static void GetPosition(bool white)
        {
            ConsoleColor defaulBackground = Console.BackgroundColor;
            ConsoleColor defaulForeground = Console.ForegroundColor;

            if (white)
            {
                // Iterates through rows
                for (int i = 0; i < 8; i++)
                {
                    Console.Write(i);

                    // Iterates through columns
                    for (int j = 0; j < 8; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;

                        WriteSquare(j, i);
                    }

                    Console.ForegroundColor = defaulForeground;
                    Console.BackgroundColor = defaulBackground;
                    Console.Write("\n");
                }

                Console.Write(' ');

                for (int i = 0; i < 8; i++)
                {
                    Console.Write(Convert.ToString(i) + ' ');
                }
                Console.Write("\n");
            }
            else
            {
                // Iterates through rows
                for (int i = 7; i >= 0; i--)
                {
                    Console.Write(i);

                    // Iterates through columns
                    for (int j = 7; j >= 0; j--)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;

                        WriteSquare(j, i);
                    }

                    Console.ForegroundColor = defaulForeground;
                    Console.BackgroundColor = defaulBackground;
                    Console.Write("\n");
                }

                Console.Write(' ');

                for (int i = 7; i >= 0; i--)
                {
                    Console.Write(Convert.ToString(i) + ' ');
                }
                Console.Write("\n");
            }
        }
    }
}