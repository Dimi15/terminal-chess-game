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
        /// Sets the variables for the best move that the computer can do
        /// </summary>
        /// <param name="depth">the depth of the tree of moves to analyze</param>
        /// <returns>The best evaluation of the best move the computer can make</returns>
        public static double Minimax(int depth, ref int bestStartX, ref int bestStartY, ref int bestEndX, ref int bestEndY)
        {
            // TODO: Checks if the position is in checkmate, if it is returns a so low value

            double bestEvaluation = -1000;
            double currentEvaluation = 0;
            int lastPiece = _;
            int currentPiece = _;

            if (depth = 0)
                return Program.Evaluation();

            // Cycle through all moves for each piece in the matrix
            for (int i = 0; i < 8; i++) // Rows of the matrix
            {
                for (int j = 0; j < 8; j++) // Columns of the matrix
                {
                    // Skips empty squares and white pieces
                    if (board[i,j] != _ && board[i,j] < BP)
                    {
                        for (int k = 0; k < 8; k++) // Rows of the single piece
                        {
                            for (int l = 0; l < 8; l++) // Columns of the single piece
                            {
                                // Plays the move if it is legal
                                if (Program.Move(j, i, l, k))
                                {
                                    currentPiece = board[k,l]; // Saves the end position of the piece
                                    lastPiece = board[i, j]; // Saves the start position of the piece

                                    currentEvaluation = Minimax(depth - 1, ref bestStartX, ref bestStartY, ref bestEndX, ref bestEndY);

                                    // Checks if the current evaluation is greater than the current best one
                                    if (currentEvaluation > bestEvaluation)
                                    {
                                        bestEvaluation = currentEvaluation;
                                        bestStartX = j;
                                        bestStartY = i;
                                        bestEndX = l;
                                        bestEndY = k;
                                    }
       
                                    board[k, l] = currentPiece; // Sets back the end position of the piece
                                    board[i, j] = lastPiece; // Sets back the start position of the piece
                                }
                                    
                            }
                        }
                    }

                }

                return bestEvaluation;
            }
        }
    }
}
