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
        /// Move a piece from Start square to End square
        /// </summary>
        public static void Move(int startX, int startY, int endX, int endY)
        {
            board[endX, endY] = board[startX, startY];
            board[startX, startY] = _;
        }

        /// <summary>
        /// Given a move checks its legality
        /// </summary>
        /// <returns>If the move is legal</returns>
        static bool LegalMove(int startX, int startY, int endX, int endY)
        {
            // Checks if it is inside the matrix
            if (startX < 0 || startX > 7 || startY < 0 || startY > 7)
                return false;

            if (endX < 0 || endX > 7 || endY < 0 || endY > 7)
                return false;


            // Checks if the move is legal based on the type of piece

            // PAWN:
        }
    }
}
