using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_game
{
    public partial class Program
    {
        // Returns the best move for the computer
        public static double Minimax(int depth, int bestStartX, int bestStartY, int bestEndX, int bestEndY)
        {
            double bestEvaluation = -1000;
            double currentEvaluation = 0;

            if (depth = 0)
                return Program.Evaluation();

            // Cycle through all moves for each piece in the matrix
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Skips empty squares and white pieces
                    if (board[i,j] != _ && board[i,j] < BP)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                if (Program.Move(j, i, l, k))
                                {
                                    currentEvaluation = Minimax(depth - 1);
                                    // Checks if the current evaluation is greater than the best one
                                    if (currentEvaluation > bestEvaluation)
                                    {
                                        bestEvaluation = currentEvaluation;
                                        bestStartX = j;
                                        bestStartY = i;
                                        bestEndX = l;
                                        bestEndY = k;
                                    }
                                }
                                    
                            }
                        }
                    }

                }
            }
        }
    }
}
