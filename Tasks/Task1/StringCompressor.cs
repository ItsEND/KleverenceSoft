using System.Text;

namespace Task1;

public static class StringCompressor
{
    public static string Compression(string inputString)
    {
        var sb = new StringBuilder();

        int counter = 1;

        for (int i = 1; i < inputString.Length; i++)
        {
            if (inputString[i] == inputString[i - 1])
            {
                counter++;
            }
            else
            {
                sb.Append(inputString[i - 1]);
                if (counter > 1)
                {
                    sb.Append(counter);
                }
                counter = 1;
            }
        }

        sb.Append(inputString[^1]);
        if (counter > 1)
        {
            sb.Append(counter);
        }

        return sb.ToString();
    }

    public static string Decompression(string compressed)
    {
        var sb = new StringBuilder();
        for (int j = 0; j < compressed.Length; j++)
        {
            if (!char.IsDigit(compressed[j]))
            {
                sb.Append(compressed[j]);
            }
            else
            {
                int num = 0;
                while (j < compressed.Length && char.IsDigit(compressed[j]))
                {
                    num = num * 10 + (compressed[j] - '0');
                    j++;
                }
                j--;

                sb.Append(sb[^1], num - 1);
            }
        }
        return sb.ToString();
    }
}
