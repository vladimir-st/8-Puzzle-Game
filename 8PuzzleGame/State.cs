using System;

namespace _8PuzzleGame
{
    public class State : IComparable<State>
    {
        private int manhatanDistance;

        public State(Board currentBoard, State parent, string lastMove, int searchDepth)
        {
            this.CurrentBoard = currentBoard;
            this.Parent = parent;
            this.LastMove = lastMove;
            this.SearchDepth = searchDepth;
        }

        public Board CurrentBoard { get; set; }
        public State Parent { get; set; }
        public string LastMove { get; set; }
        public int SearchDepth { get; set; }

        public int ManhatanDistance()
        {
            var matrix = this.CurrentBoard.Matrix;
            this.manhatanDistance = 0; 

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var num = matrix[i, j];

                    switch (num)
                    {
                        case 1: this.manhatanDistance += Math.Abs(0 - i) + Math.Abs(1 - j); break;
                        case 2: this.manhatanDistance += Math.Abs(0 - i) + Math.Abs(2 - j); break;
                        case 3: this.manhatanDistance += Math.Abs(1 - i) + Math.Abs(0 - j); break;
                        case 4: this.manhatanDistance += Math.Abs(1 - i) + Math.Abs(1 - j); break;
                        case 5: this.manhatanDistance += Math.Abs(1 - i) + Math.Abs(2 - j); break;
                        case 6: this.manhatanDistance += Math.Abs(2 - i) + Math.Abs(0 - j); break;
                        case 7: this.manhatanDistance += Math.Abs(2 - i) + Math.Abs(1 - j); break;
                        case 8: this.manhatanDistance += Math.Abs(2 - i) + Math.Abs(2 - j); break;

                        default:
                            break;
                    }
                }
            }

            return this.manhatanDistance;
        }

        public State MoveZeroToTheLeft(int x, int y)
        {
            if (y == 0)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x, y - 1];
            clonedState.CurrentBoard.Matrix[x, y - 1] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State MoveZeroToTheRight(int x, int y)
        {
            if (y == (this.CurrentBoard.Matrix.Length / 3) - 1)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x, y + 1];
            clonedState.CurrentBoard.Matrix[x, y + 1] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State MoveZeroUp(int x, int y)
        {
            if (x == 0)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x - 1, y];
            clonedState.CurrentBoard.Matrix[x - 1, y] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State MoveZeroDown(int x, int y)
        {
            if (x == (this.CurrentBoard.Matrix.Length / 3) - 1)
            {
                return null;
            }

            var clonedState = this.Clone();

            var temp = clonedState.CurrentBoard.Matrix[x + 1, y];
            clonedState.CurrentBoard.Matrix[x + 1, y] = 0;
            clonedState.CurrentBoard.Matrix[x, y] = temp;
            return clonedState;
        }

        public State Clone()
        {
            var newMatrix = new int[3, 3];

            for (int i = 0; i < this.CurrentBoard.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.CurrentBoard.Matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = this.CurrentBoard.Matrix[i, j];
                }
            }

            var clonedBoard = new Board(newMatrix);

            return new State(clonedBoard, this.Parent, this.LastMove, this.SearchDepth);
        }

        public override int GetHashCode()
        {
            return this.CurrentBoard.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var otherState = (State)obj;

            return this.CurrentBoard.Equals(otherState.CurrentBoard);
        }

        public int CompareTo(State other)
        {
            // Heuristic value
            var thisValue = this.ManhatanDistance();
            var otherValue = other.ManhatanDistance();

            if (thisValue < otherValue)
            {
                return 1;
            }
            else if (thisValue > otherValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
