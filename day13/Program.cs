using System.Text.RegularExpressions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Optimization;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day13/input.txt";
        string[] input = File.ReadAllLines(inputFile);
        List<List<double>> gameNumbers = ParseInput(input);
        double total = 0;
        
        foreach(List<double> game in gameNumbers)
        {
            total += PlayGame(game);
        }
        Console.WriteLine(total);
    }

    static double PlayGame(List<double> game)
    {
        var A = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            {game[0],game[2]},
            {game[1],game[3]}
        });
        var b = Vector<double>.Build.DenseOfArray(new double[] {game[4]+10000000000000,game[5]+10000000000000});
        var x = A.Solve(b);
        x[0] = double.Round(x[0], 2);
        x[1] = double.Round(x[1], 2);
        if (x[0] % 1 == 0 && x[1] % 1 == 0)
        {
            double tokens = x[0]*3 + x[1];
            return tokens;
        }
        else
            return 0;
    }

    static List<List<double>> ParseInput(string[] input)
    {
        List<string[]> games = [];
        string[] tempGame = ["0","0","0"];
        for (int i = 0; i < input.Length; i++)
        {
            if (i % 4 == 0)
            {
                tempGame[0] = input[i];
            }
            else if (i % 4 == 1)
            {
                tempGame[1] = input[i];
            }
            else if (i % 4 == 2)
            {
                tempGame[2] = input[i];
                games.Add(tempGame);
                tempGame = ["0","0","0"];
            }
        }
        
        List<List<double>> gameNumbers = [];
        foreach(string[] game in games)
        {
            List<double> numbers = [];
            foreach(string g in game)
            {
                numbers.AddRange(ParseGame(g));
            }
            gameNumbers.Add(numbers);
        }
        return gameNumbers;
    }
    static List<double> ParseGame(string game)
    {
        string pattern = @"\d+";
        Regex regex = new Regex(pattern);

        List<double> numbers = [];
        MatchCollection matches = regex.Matches(game);
        foreach (Match match in matches)
        {
            numbers.Add(int.Parse(match.Value));
        }

        return numbers;
    }
}
