using System;
using System.Collections.Generic;
using System.Text;

namespace _8PuzzleGame.Solvers
{
    public abstract class Solver
    {
        protected int[,] GoalState { get; set; }
        protected int MaxFringeSize { get; set; }
        protected int MaxSearchDepth { get; set; }
        protected int NodesExpanded { get; set; }

        public Solver()
        {
            this.GoalState = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8, } };
        }

        public abstract void Solve(State state);

        protected List<State> GenerateChildrenStates(State currentState, int x, int y)
        {
            var children = new List<State>();

            var rightState = currentState.MoveZeroToTheRight(x, y);
            if (rightState != null)
            {
                rightState.LastMove = "right";
                rightState.Parent = currentState;
                rightState.SearchDepth++;
                children.Add(rightState);
            }

            var leftState = currentState.MoveZeroToTheLeft(x, y);
            if (leftState != null)
            {
                leftState.LastMove = "left";
                leftState.Parent = currentState;
                leftState.SearchDepth++;
                children.Add(leftState);
            }

            var downState = currentState.MoveZeroDown(x, y);
            if (downState != null)
            {
                downState.LastMove = "down";
                downState.Parent = currentState;
                downState.SearchDepth++;
                children.Add(downState);
            }

            var upState = currentState.MoveZeroUp(x, y);
            if (upState != null)
            {
                upState.LastMove = "up";
                upState.Parent = currentState;
                upState.SearchDepth++;
                children.Add(upState);
            }

            return children;
        }

        public void PrintResults(State finalState, int fringeSize)
        {
            var searchDepth = finalState.SearchDepth;
            var path = FindPath(finalState);
            var pathToGoal = this.GetPathAsString(path);
            var costOfPath = path.Count;

            Console.WriteLine($"Path to goal: {pathToGoal}");
            Console.WriteLine($"Cost of path: {costOfPath}");
            Console.WriteLine($"Nodes expanded: {this.NodesExpanded}");
            Console.WriteLine($"Fringe size: {fringeSize}");
            Console.WriteLine($"Max fringe size: {this.MaxFringeSize}");
            Console.WriteLine($"Search depth: {searchDepth}");
            Console.WriteLine($"Max search depth: {this.MaxSearchDepth}");
        }

        private List<string> FindPath(State state)
        {
            var path = new List<string>();
            while (state.Parent != null)
            {
                path.Add(state.LastMove);
                state = state.Parent;
            }

            return path;
        }

        private string GetPathAsString(List<string> path)
        {
            var sb = new StringBuilder();
            sb.Append("[");

            for (int i = path.Count - 1; i >= 0; i--)
            {
                sb.Append("'");
                sb.Append(path[i]);
                sb.Append("'");
                sb.Append(", ");
            }

            var pathToGoal = sb.ToString().TrimEnd(new[] { ',', ' ' });
            pathToGoal += "]";

            return pathToGoal;
        }
    }
}
