using System;

using C5;

namespace _8PuzzleGame.Solvers
{
    public class IdaStarSolver : Solver
    {
        public override void Solve(State state)
        {
            var limit = state.ManhatanDistance();
            
            while (true)
            {
                var stateAndLimit = Ids(state, limit);
                state = stateAndLimit.Item1;
                limit = stateAndLimit.Item2;

                if (state.CurrentBoard.IsEqual(this.GoalState))
                {
                    return;
                }
            }
        }

        // Iterative deepening search with heuristic as limit
        public Tuple<State, int> Ids(State root, int limit)
        {
            var queue = new IntervalHeap<State>();
            var minNotAccepted = int.MaxValue;
            var state = root.Clone();

            queue.Add(state);

            while (queue.Count > 0)
            {
                if (queue.Count > this.MaxFringeSize)
                {
                    this.MaxFringeSize = queue.Count;
                }

                state = queue.DeleteMax();

                if (state.SearchDepth > this.MaxSearchDepth)
                {
                    this.MaxSearchDepth = state.SearchDepth;
                }

                this.NodesExpanded++;

                var zeroXAndY = state.CurrentBoard.IndexOfZero();
                var zeroX = zeroXAndY.Item1;
                var zeroY = zeroXAndY.Item2;
                var children = this.GenerateChildrenStates(state, zeroX, zeroY);

                for (var i = 0; i < children.Count; i++)
                {
                    var currentChild = children[i];
                    var currentChildEval = currentChild.ManhatanDistance();

                    if (currentChild.CurrentBoard.IsEqual(this.GoalState))
                    {
                        this.PrintResults(currentChild, queue.Count);
                        return new Tuple<State, int>(currentChild, 0);
                    }
                   
                    if (currentChildEval < limit)
                    {
                        queue.Add(currentChild);
                    }
                    else
                    {
                        if (currentChildEval < minNotAccepted)    
                        {
                            minNotAccepted = currentChildEval;
                        }
                    }

                    if (currentChild.SearchDepth > this.MaxSearchDepth)
                    {
                        this.MaxSearchDepth = currentChild.SearchDepth;
                    }
                }
            }

            return new Tuple<State, int>(root, minNotAccepted);
        }
    }
}
