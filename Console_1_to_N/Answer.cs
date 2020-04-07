using System.Collections.Generic;

namespace Console_1_to_N
{
    public class Answer
    {
        public List<Cell> Path { get; set; }
        public int[,] Solution { get; set; }

        public Answer( List<Cell> path, int[,] solution )
        {
            Path = path;
            Solution = solution;
        }
    }
}
