using Task1;

namespace Task1Tests;

public class UnitTest1
{
    [Theory]
    [InlineData("aaabbcccdde", "a3b2c3d2e")]
    [InlineData("abc", "abc")]
    [InlineData("aaaa", "a4")]
    [InlineData("a", "a")]
    [InlineData("aabb", "a2b2")]
    [InlineData("aaaaaaaaaaaa", "a12")]
    public void Compression_ShouldReturnExpectedResult(string input, string expected)
    {
        string result = StringCompressor.Compression(input);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("a3b2c3d2e", "aaabbcccdde")]
    [InlineData("abc", "abc")]
    [InlineData("a4", "aaaa")]
    [InlineData("a", "a")]
    [InlineData("a2b2", "aabb")]
    [InlineData("a12", "aaaaaaaaaaaa")]
    public void Decompression_ShouldReturnExpectedResult(string input, string expected)
    {
        string result = StringCompressor.Decompression(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("aaabbcccdde")]
    [InlineData("abc")]
    [InlineData("aaaa")]
    [InlineData("a")]
    [InlineData("aabb")]
    [InlineData("aaaaaaaaaaaa")]
    [InlineData("abbbbbbbbbbbbccccc")]
    public void CompressionAndDecompression_ShouldRestoreOriginalString(string input)
    {
        string compressed = StringCompressor.Compression(input);
        string decompressed = StringCompressor.Decompression(compressed);

        Assert.Equal(input, decompressed);
    }

}