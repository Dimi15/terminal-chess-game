using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game
{
    public partial class Program
    {
        // Declaration of Pieces
        const int _ = 0; // Empty Square
        const int WP = 1; // White Pawn
        const int WN = 2; // White Knight
        const int WB = 3; // White Bishop
        const int WR = 4; // White Rook
        const int WQ = 5; // White Queen
        const int WK = 6; // White King
        const int BP = 7; // Black Pawn
        const int BN = 8; // Black Knight
        const int BB = 9; // Black Bishop
        const int BR = 10; // Black Rook
        const int BQ = 11; // Black Queen
        const int BK = 12; // Black King

        bool playerWhite = false;

        int[,] board = new int[8, 8];

        static void Main(string[] args)
        {

            

            Program.Minimax();
            Program.Evaluation();
            Program.LegalMove();
        }

        /// <summary>
        /// Assigns the variables to the matrix for the start position
        /// </summary>
        static void setupStartPosition()
        {
            // CASE FOR BLACK:
            // Assigning to each square the respective piece
            board[0, 0] = BR;
            board[0, 1] = BN;
            board[0, 2] = BB;
            board[0, 3] = BQ;
            board[0, 4] = BK;
            board[0, 5] = BB;
            board[0, 6] = BN;
            board[0, 7] = BR;

            // Loop for pawns
            for (int i = 0; i < 8; i++)
                board[1, i] = BP;

            // CASE FOR WHITE:
            // Assigning to each square the respective piece
            board[7, 0] = WR;
            board[7, 1] = WN;
            board[7, 2] = WB;
            board[7, 3] = WQ;
            board[7, 4] = WK;
            board[7, 5] = WB;
            board[7, 6] = WN;
            board[7, 7] = WR;

            // Loop for pawns
            for (int i = 0; i < 8; i++)
                board[6, i] = WP;
        }

        /// <summary>
        /// Gets the current board
        /// </summary>
        /// <returns>The current board position</returns>
        static string getPosition()
        {
            stringBoard = "";


            for (int i = 0; i < 8; i++) // Cycles through rows
            {
                stringBoard += "-----------------------------------";

                for(int j = 0; j < 8; j++) // Cycles through columns
                {
                    stringBoard += "| ";

                    // TODO: Finish the switch case with all the cases both black and white
                    switch(board[i, j])
                    {
                        case WP:
                            stringBoard += "♟";
                            break;

                        case WN:
                            stringBoard += "♞";
                            break;
                    }

                    stringBoard += " ";
                }
            }
        }
    }
}