using System.Text;

public class Prgram
{
    public static char[] sides = new char[] { 'F', 'B', 'L', 'R', 'U', 'D' };
    public static void Main()
    {
        List<CubeAssemblyResult> results = new();

        while (true)
        {
            Console.Clear();

            ShowResults(results);

            string shuffleAlg = GenerateShuffleAlg();
            Console.WriteLine($"\n{shuffleAlg}\nPress any key to start");
            Console.ReadKey(true);

            DateTime timeStart = DateTime.Now;
            Console.WriteLine("\nTimer is ON, press any key to stop");
            Console.ReadKey();

            TimeSpan timeDifference = DateTime.Now - timeStart;
            Console.WriteLine(timeDifference.TotalSeconds);

            CubeAssemblyResult assemblyResult =
                new CubeAssemblyResult(shuffleAlg, timeDifference.TotalSeconds);

            results.Add(assemblyResult);
        }
    }

    private static void ShowResults(List<CubeAssemblyResult> results)
    {
        var resultsSorted = results.OrderBy(r => r.Time).ToList();

        var worst = resultsSorted.LastOrDefault();
        var best = resultsSorted.FirstOrDefault();
        foreach (CubeAssemblyResult result in results)
        {
            if (result == worst)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else if (result == best)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            Console.Write($"{result.ShuffleAlg}");
            Console.CursorLeft = 35;
            Console.WriteLine($"{result.Time:0.00}");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        if (results.Any())
        {
            double avg = results.Select(r => r.Time).Average();
            Console.WriteLine($"Average: {avg:0.00}");
        }
    }

    public static string GenerateShuffleAlg()
    {
        StringBuilder sb = new();

        char prevChar = 'U';
        for (int i = 0; i < 10; i++)
        {
            Random random = new Random();
            do
            {
                char side = sides[random.Next(sides.Length)];
                if (side != prevChar)
                {
                    prevChar = side;
                    sb.Append(side);
                    break;
                }
            } while (true);

            bool singleTurn = RandomBool(random);
            bool clockWise = RandomBool(random);

            if (singleTurn && clockWise)
            {
                sb.Append('\'');
            }
            else if (singleTurn == false)
            {
                sb.Append('2');
            }
            sb.Append(' ');
        }

        return sb.ToString();
    }

    public static bool RandomBool(Random random)
    {
        return random.Next(0, 2) == 0;
    }
}

public class CubeAssemblyResult
{
    public readonly string ShuffleAlg;
    public readonly double Time;

    public CubeAssemblyResult(string shuffleAlg, double time)
    {
        ShuffleAlg = shuffleAlg;
        Time = time;
    }
}