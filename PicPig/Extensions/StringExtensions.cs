namespace PicPig.Extensions;

public static class StringExtensions
{
    public static string Cut(this string src, int maxLength)
    {
        if (maxLength <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLength), $"{nameof(maxLength)} must be greater than 0");
        }

        return src.Length <= maxLength
            ? src
            : src[..maxLength];
    }
}
