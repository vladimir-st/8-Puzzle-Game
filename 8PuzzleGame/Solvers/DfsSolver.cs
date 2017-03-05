using System.Collections.Generic;

namespace _8PuzzleGame.Solvers
{
    public class DfsSolver : Solver
    {
        public override void Solve(State state)
        {
            var visited = new HashSet<Board>();
            var stack = new Stack<State>();

            stack.Push(state);
            visited.Add(state.CurrentBoard);

            while (stack.Count > 0)
            {
                if (stack.Count > this.MaxFringeSize)
                {
                    this.MaxFringeSize = stack.Count;
                }

                state = stack.Pop();

                if (state.SearchDepth > this.MaxSearchDepth)
                {
                    this.MaxSearchDepth = state.SearchDepth;
                }


                if (state.CurrentBoard.IsEqual(this.GoalState))
                {
                    this.PrintResults(state, stack.Count);
                    break;
                }

                this.NodesExpanded++;

                var zeroXAndY = state.CurrentBoard.IndexOfZero();
                var zeroX = zeroXAndY.Item1;
                var zeroY = zeroXAndY.Item2;
                var children = this.GenerateChildrenStates(state, zeroX, zeroY);

                for (var i = 0; i < children.Count; i++)
                {
                    var currentChild = children[i];
                    if (!visited.Contains(currentChild.CurrentBoard))
                    {
                        stack.Push(currentChild);
                        visited.Add(currentChild.CurrentBoard);

                        if (currentChild.SearchDepth > this.MaxSearchDepth)
                        {
                            this.MaxSearchDepth = currentChild.SearchDepth;
                        }
                    }
                }
            }
        }
    }
}
