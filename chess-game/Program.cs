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

            string startSquare = "";
            int startX = 0;
            int startY = 0;

            string endSquare = "";
            int endX = 0;
            int endY = 0;

            bool validInput = true;

            int playerPromoteTo = _;
            int computerPromoteTo = _;

            int bestStartX = 0;
            int bestStartY = 0;
            int bestEndX = 0;
            int bestEndY = 0;
            int computerDepth = 5;

            bool isWhiteTurn = true;
            bool playerWhite = false;
            bool draw = false;

            char pickedColor = ' ';

            ConsoleColor defaulBackground = Console.BackgroundColor;
            ConsoleColor defaulForeground = Console.ForegroundColor;

            // Asks the color of the player
            do
            {
                Console.WriteLine("Do You Want To Play Black or White? (B/W)");
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
                playerWhite = false;
                playerPromoteTo = BQ;
                computerPromoteTo = WQ;
            }
            
            // Shows starting board depending on the color of the player
            SetupStartPosition();
            
            // Exits the loop when the game ends
            do
            {
                // Iterates until the player hasn't chosen a legal move
                if (isWhiteTurn == playerWhite)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\n");
                        GetPosition(playerWhite);

                        if (validInput)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;

                            Console.WriteLine("\nMake a Move:\n");

                            Console.ForegroundColor = defaulForeground;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Red;

                            Console.Write("\n\x1b[1mInvalid Move!\x1b[0m\n\n"); //"\x1b[1m" + ... + "\x1b[0m\n" == bold text

                            Console.ForegroundColor = defaulForeground;
                            Console.BackgroundColor = defaulBackground;
                        }

                        validInput = true;

                        //start square
                        Console.WriteLine("Start Square: (Es: e2)");
                        startSquare = Console.ReadLine();

                        if(!GetCoordinates(startSquare, ref startX, ref startY))
                        {
                            validInput = false;
                        }

                        //end square
                        Console.WriteLine("End Square: (Es: e4)");
                        endSquare = Console.ReadLine();

                        if(!GetCoordinates(endSquare, ref endX, ref endY))
                        {
                            validInput = false;
                        }

                        //check move
                        if(!Program.Move(startX, startY, endX, endY, playerWhite, playerPromoteTo))
                        {
                            validInput = false;
                        }
                    } while (!validInput);
                    
                    //Check if the game has ended
                    if (Program.Checkmate(!playerWhite, ref draw))
                    {
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;

                        Console.Write("\x1b[1mYOU WON\u001b[0m\n");

                        Console.ForegroundColor = defaulForeground;
                        Console.BackgroundColor = defaulBackground;

                        Console.WriteLine();

                        break;
                    }
                    else if (draw)
                    {
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Gray;

                        Console.Write("\x1b[1mDRAW!\u001b[0m\n");

                        Console.ForegroundColor = defaulForeground;
                        Console.BackgroundColor = defaulBackground;

                        Console.WriteLine();

                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    GetPosition(playerWhite);

                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine("\nThe Compure is Thinking...\n");

                    Console.ForegroundColor = defaulForeground;

                    // Turn of the computer
                    if (!playerWhite)
                    {
                        Minimax(computerDepth, double.NegativeInfinity, double.PositiveInfinity, ref bestStartX,
                            ref bestStartY, ref bestEndX, ref bestEndY, true, playerWhite);
                    }
                    else
                    {
                        Minimax(computerDepth, double.NegativeInfinity, double.PositiveInfinity, ref bestStartX,
                            ref bestStartY, ref bestEndX, ref bestEndY, false, playerWhite);
                    }
                    
                    Move(bestStartX, bestStartY, bestEndX, bestEndY, !playerWhite, computerPromoteTo);
                    
                    // Checks if the game has ended
                    if (Checkmate(playerWhite, ref draw))
                    {
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Red;

                        Console.Write("\x1b[1mYOU LOST!\u001b[0m\n");

                        Console.ForegroundColor = defaulForeground;
                        Console.BackgroundColor = defaulBackground;

                        Console.WriteLine();

                        break;
                    }
                     else if (draw)
                    {
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Gray;

                        Console.Write("\x1b[1mDRAW!\u001b[0m\n");

                        Console.ForegroundColor = defaulForeground;
                        Console.BackgroundColor = defaulBackground;

                        Console.WriteLine();

                        break;
                    }
                }
                
                isWhiteTurn = !isWhiteTurn;
                
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
        /// Moves a piece from the start square to the end square, including handling en passant.
        /// </summary>
        /// <param name="input">String that contains the square in the format letter-number (es: "e4")</param>
        /// <param name="startY">Start column</param>
        /// <param name="endX">End row</param>
        /// <param name="endY">End column</param>
        /// <param name="isWhiteTurn">True if white's turn, false if black's</param>
        /// <returns>Valid input</returns>
        static bool GetCoordinates(string input, ref int x, ref int y)
        {
            //NOTE: sparare un elicottero non è una buaona idea
            if(input.Length != 2)
            {
                return false;
            }

            y = '8' - input[1];
            x = input[0] - 'a';

            if(x < 0 || x > 7)
            {
                return false;
            }

            if (y < 0 || y > 7)
            {
                return false;
            }

            return true;
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
                    Console.Write(8 - i);

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
                    Console.Write(Convert.ToChar(i + 'a') + " ");
                }

                Console.Write("\n");
            }
            else
            {
                // Iterates through rows
                for (int i = 7; i >= 0; i--)
                {
                    Console.Write(8 - i);

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
                    Console.Write(Convert.ToChar(i + 'a') + " ");
                }

                Console.Write("\n");
            }
        }
    }
}
