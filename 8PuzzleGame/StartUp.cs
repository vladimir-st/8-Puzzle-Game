using _8PuzzleGame.Solvers;

namespace _8PuzzleGame
{
    public class Startup
    {
        public static void Main()
        {
            // 1,2,5,3,4,0,6,7,8
            var arr = new int[3, 3] { { 1, 2, 5 }, { 3, 4, 0 }, { 6, 7, 8 } };

            var board = new Board(arr);
            var startingState = new State(board, null, null, 0);

            var idaStar = new IdaStarSolver();
            var measurer = new PerformanceMeasurer();
            measurer.StartMeasuring();
            idaStar.Solve(startingState);
            measurer.StopMeasuring();
            measurer.PrintResults();
        }
    }
}
