namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string textFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day4/input.txt";
            string[] lines = File.ReadAllLines(textFile);
            int answer = Part1(lines);
            Console.WriteLine(answer);
            int answer2 = Part2(lines);
            Console.WriteLine(answer2);
        }
        static int Part1(string[] lines)
        {
            //loop through each line 
            //loop through each letter looking for X
            //when found, check each options for M: j+1, j-1, i+1 j, i+1 j+1, i+1 j-1
            //continue for each letter of xmas 
            int xmasCounter = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'X')
                    {
                        //right
                        if (j+3 < lines[i].Length && lines[i][j+1] == 'M')
                        {
                            if (lines[i][j+2] == 'A' && lines[i][j+3] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //left
                        if (j >= 3 && lines[i][j-1] == 'M')
                        {
                            if (lines[i][j-2] == 'A' && lines[i][j-3] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //down
                        if (i+3 < lines.Length && lines[i+1][j] == 'M')
                        {
                            if (lines[i+2][j] == 'A' && lines[i+3][j] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //up
                        if (i >= 3 && lines[i-1][j] == 'M')
                        {
                            if (lines[i-2][j] == 'A' && lines[i-3][j] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //down right diagonal
                        if (i+3 < lines.Length && j+3 < lines[i].Length && lines[i+1][j+1] == 'M')
                        {
                            if (lines[i+2][j+2] == 'A' && lines[i+3][j+3] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //down left diagonal
                        if (j >= 3 && i+3 < lines.Length && lines[i+1][j-1] == 'M')
                        {
                            if (lines[i+2][j-2] == 'A' && lines[i+3][j-3] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //up right diagonal
                        if (i >= 3 && j+3 < lines[i].Length && lines[i-1][j+1] == 'M')
                        {
                            if (lines[i-2][j+2] == 'A' && lines[i-3][j+3] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //up left diagonal
                        if (j >= 3 && i >= 3 && lines[i-1][j-1] == 'M')
                        {
                            if (lines[i-2][j-2] == 'A' && lines[i-3][j-3] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                    }
                }
            }
            return xmasCounter;
        }
        static int Part2(string[] lines)
        {
            //loop through each line 
            //loop through each letter looking for M
            //check each of the 4 orientations of X
            int xmasCounter = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'M')
                    {
                        //M at top
                        if (j+2 < lines[i].Length && i+2 < lines.Length && lines[i][j+2] == 'M')
                        {
                            if (lines[i+1][j+1] == 'A' && lines[i+2][j] == 'S' && lines[i+2][j+2] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //M at left 
                        if (j+2 < lines[i].Length && i+2 < lines.Length && lines[i+2][j] == 'M')
                        {
                            if (lines[i+1][j+1] == 'A' && lines[i][j+2] == 'S' && lines[i+2][j+2] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //M at bottom
                        if (i >= 2 && j+2 < lines.Length && lines[i][j+2] == 'M')
                        {
                            if (lines[i-1][j+1] == 'A' && lines[i-2][j] == 'S'&& lines[i-2][j+2] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                        //M at left 
                        if (j >= 2 && i+2 < lines.Length && lines[i+2][j] == 'M')
                        {
                            if (lines[i+1][j-1] == 'A' && lines[i][j-2] == 'S' && lines[i+2][j-2] == 'S')
                            {
                                xmasCounter++;
                            }
                        }
                    }
                }
            }
            return xmasCounter;
        }
    }
}