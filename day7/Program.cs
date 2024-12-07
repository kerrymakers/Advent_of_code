class Program
{
    static void Main(string[] args)
    {
        string inputFile = "/Users/kerryfinch/Documents/Projects/Advent_of_code/day7/input.txt";
        string[] input = File.ReadAllLines(inputFile);
        string[] splitInput;
        decimal answer;
        string[] numbers;
        List<string> allOperations;
        decimal runningTotal = 0;
        decimal finalAnswer = 0;

        for (int i = 0; i < input.Length; i++)
        {
            splitInput = input[i].Split(": ");
            answer = decimal.Parse(splitInput[0]);
            numbers = splitInput[1].Split(' ');
            allOperations = FindOperations(numbers.Length-1);

            foreach(string operation in allOperations)
            {
                runningTotal = decimal.Parse(numbers[0]);
                for (int j = 0; j < operation.Length; j++)
                {
                    if (operation[j] == '+')
                    {
                        runningTotal += decimal.Parse(numbers[j+1]);
                    }
                    else if (operation[j] == '*')
                    {
                        runningTotal *= decimal.Parse(numbers[j+1]);
                    }
                    else if (operation[j] == '|')
                    {
                        runningTotal = decimal.Parse(runningTotal.ToString() + numbers[j+1]);
                    }
                }
                if (runningTotal == answer)
                {
                    finalAnswer += answer;
                    break;
                }
            }
        }
        Console.WriteLine("Part 2: " + finalAnswer.ToString());
    }

    static List<string> FindOperations(int numbersLength)
    {
        List<string> opList = [];
        Dive("+*|", numbersLength, opList);
        return opList;
    }

    static void Dive(string validChars, int maxLength, List<string> opList, string prefix = "", int level = 0)
    {
        level += 1;
        foreach (char c in validChars)
        {
            if (level == maxLength)
            {
                opList.Add(prefix + c);
            }
            if (level < maxLength)
            {
                Dive(validChars, maxLength, opList, prefix + c, level);
            }
        }
    }
}