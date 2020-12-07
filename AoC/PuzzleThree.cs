namespace AoC
{
    internal class PuzzleThree : Puzzle
    {
        public PuzzleThree() : base("03")
        {
        }

        public override object GetAnswerOne()
        {
            int y = 0;
            int x = 0;

            int jumpX = 3;
            int jumpY = 1;
            return CountTrees(x,y, jumpX, jumpY);
        }

        public override object GetAnswerTwo()
        {
            int y = 0;
            int x = 0;

            long answer2 = CountTrees(x, y, 1, 1);
            answer2 = answer2 * CountTrees(x, y, 3, 1);
            answer2 = answer2 * CountTrees(x, y, 5, 1);
            answer2 = answer2 * CountTrees(x, y, 7, 1);
            answer2 = answer2 * CountTrees(x, y, 1, 2);

            return answer2;
        }


        private int CountTrees(int startX, int startY, int jumpX, int jumpY)
        {
            int y = startX + jumpY;
            int x = startY + jumpX;

            int treeCount = 0;

            while (y < Lines.Length)
            {
                if (HasTree(x, y))
                {
                    treeCount++;
                }

                y = y + jumpY;
                x = x + jumpX;

            }
            return treeCount;
        }

        private bool HasTree(int x, in int y)
        {
            x = x % Lines[0].Length;

            return (Lines[y][x] == '#');
        }
    }
}