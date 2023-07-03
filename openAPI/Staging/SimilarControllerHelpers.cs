using System.Text.RegularExpressions;

internal static class SimilarControllerHelpers
{
    //正規化
    public static string[] ExtractValues(string input)
    {
        string pattern = @"[-+]?[0-9]*\.?[0-9]+";
        MatchCollection matches = Regex.Matches(input, pattern);
        string[] values = new string[matches.Count];

        for (int i = 0; i < matches.Count; i++)
        {
            values[i] = matches[i].Value;
        }

        return values;
    }
    //餘弦
    public static double Similarity(double[] vector_a, double[] vector_b)
    {
        if (vector_a.Length != vector_b.Length)
        {
            throw new ArgumentException("輸入的兩個向量長度要一樣!");
        }
        var sum_a = vector_a.Select(x => Math.Pow(x, 2)).Sum();
        var sum_b = vector_b.Select(x => Math.Pow(x, 2)).Sum();
        var root_a = Math.Round(Math.Sqrt(Math.Round(sum_a, 5)), 5);
        var root_b = Math.Round(Math.Sqrt(Math.Round(sum_b, 5)), 5);
        var sum = Math.Round(vector_a.Zip(vector_b, (x, y) => x * y).Sum(), 5);
        var norm = root_a * root_b;
        var sim = Math.Round(sum / norm, 5);
        return sim;
    }


}

