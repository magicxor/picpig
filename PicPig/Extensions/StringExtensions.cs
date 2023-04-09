namespace PicPig.Extensions;

public static class StringExtensions
{
    public static string Cut(this string src, int maxLength, string? defaultStr = null)
    {
        if (string.IsNullOrEmpty(src) && !string.IsNullOrEmpty(defaultStr))
            return defaultStr;
        return src.Length <= maxLength ? src : src[..(maxLength - 1)];
    }
}
