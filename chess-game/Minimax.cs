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
        /// Minimax algorithm with alpha-beta pruning to find the best move for the computer.
        /// </summary>
        /// <param name="depth">Maximum depth of the search tree.</param>
        /// <param name="alpha">Alpha value for pruning.</param>
        /// <param name="beta">Beta value for pruning.</param>
        /// <param name="bestStartX">Outputs the starting row of the best move found.</param>
        /// <param name="bestStartY">Outputs the starting column of the best move found.</param>
        /// <param name="bestEndX">Outputs the ending row of the best move found.</param>
        /// <param name="bestEndY">Outputs the ending column of the best move found.</param>
        /// <param name="isMaximizing">
        /// True if it's the maximizing player's turn (AI), who tries to maximize the evaluation score; 
        /// false if it's the minimizing player's turn (opponent), who tries to minimize the score. 
        /// The condition alternates each recursion to simulate both players' moves.
        /// </param>
        /// <param name="playerWhite">True if the player is playing white. The computer will play the opposite color.</param>
        /// <returns>The evaluation score of the best move found.</returns>
        public static double Minimax(int depth, double alpha, double beta,
            ref int bestStartX, ref int bestStartY, ref int bestEndX, ref int bestEndY,
            bool isMaximizing, bool playerWhite)
        {
            bool draw = false;

            // Terminal condition: if depth is 0, evaluate current board state
            if (depth == 0)
            {
                return Evaluation();
            }

            // The computer's color is the opposite of the player's
            bool isComputerWhite = !playerWhite;

            // Determine the current player's color in this recursion step
            bool currentPlayerIsWhite;
            if (isMaximizing)
            {
                currentPlayerIsWhite = isComputerWhite;
            }
            else
            {
                currentPlayerIsWhite = playerWhite;
            }

            // Check if the game is over for the current player
            if (Checkmate(currentPlayerIsWhite, ref draw))
            {
                if (draw)
                {
                    return 0.0; // Game ended in draw
                }
                else
                {
                    // Game ended in checkmate
                    if (isMaximizing)
                    {
                        return -10000 - depth; // Loss for the computer
                    }
                    else
                    {
                        return 10000 + depth; // Win for the computer
                    }
                }
            }

            // Initialize the best evaluation value based on whether we are maximizing or minimizing
            double bestEvaluation;
            if (isMaximizing)
            {
                // For maximizing player, start with the lowest possible value
                bestEvaluation = double.NegativeInfinity;
            }
            else
            {
                // For minimizing player, start with the highest possible value
                bestEvaluation = double.PositiveInfinity;
            }

            // Loop through all squares on the board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int piece = board[i, j];

                    bool skipSquare = false;

                    // Skip if square is empty
                    if (piece == _)
                    {
                        skipSquare = true;
                    }

                    // Determine if the piece belongs to the current player
                    bool pieceIsWhite = piece < BP;
                    if (!skipSquare && pieceIsWhite != currentPlayerIsWhite)
                    {
                        skipSquare = true;
                    }

                    // Only process valid pieces
                    if (!skipSquare)
                    {
                        for (int k = 0; k < 8; k++) // destination row
                        {
                            for (int l = 0; l < 8; l++) // destination column
                            {
                                int capturedPiece = board[k, l];

                                // Try the move; proceed if it's valid
                                if (Move(i, j, k, l, currentPlayerIsWhite))
                                {
                                    int tempStartX = 0, tempStartY = 0, tempEndX = 0, tempEndY = 0;

                                    // Recurse to evaluate this move
                                    double currentEvaluation = Minimax(depth - 1, alpha, beta,
                                        ref tempStartX, ref tempStartY, ref tempEndX, ref tempEndY,
                                        !isMaximizing, playerWhite);

                                    // Undo the move to restore original board state
                                    UndoMove(i, j, k, l, capturedPiece);

                                    // If this is a maximizing step, choose the max score
                                    if (isMaximizing)
                                    {
                                        if (currentEvaluation > bestEvaluation)
                                        {
                                            bestEvaluation = currentEvaluation;
                                            bestStartX = i;
                                            bestStartY = j;
                                            bestEndX = k;
                                            bestEndY = l;
                                        }

                                        if (bestEvaluation > alpha)
                                        {
                                            alpha = bestEvaluation;
                                        }
                                    }
                                    else // Minimizing step
                                    {
                                        if (currentEvaluation < bestEvaluation)
                                        {
                                            bestEvaluation = currentEvaluation;
                                            bestStartX = i;
                                            bestStartY = j;
                                            bestEndX = k;
                                            bestEndY = l;
                                        }

                                        if (bestEvaluation < beta)
                                        {
                                            beta = bestEvaluation;
                                        }
                                    }

                                    // Alpha-beta pruning: cut off unnecessary branches
                                    if (beta <= alpha)
                                    {
                                        k = 8; // Exit inner loop
                                        l = 8; // Exit outer loop
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return bestEvaluation;
        }
    }
}