using System.Text.RegularExpressions;
using PicPig.Models;

namespace PicPig.Services;

public class Txt2ImgQueryParser
{
    private readonly PresetFactoryProvider _presetFactoryProvider;

    private const string RandomMarker = "🎲";
    private static readonly Regex QueryRegex = new(@"^(?<preset>\d*)\s*(?<override>!{0,1})\s*(?<prompt>.*)$", RegexOptions.Compiled);

    public Txt2ImgQueryParser(PresetFactoryProvider presetFactoryProvider)
    {
        _presetFactoryProvider = presetFactoryProvider;
    }

    public Txt2ImgQuery Parse(string userQuery)
    {
        var formattedQuery = userQuery.Trim();
        if (string.IsNullOrWhiteSpace(formattedQuery) || formattedQuery == RandomMarker)
        {
            var presetFactory = _presetFactoryProvider.GetRandomPresetFactory();
            return new Txt2ImgQuery(presetFactory, string.Empty, false);
        }
        else
        {
            var groups = QueryRegex.Match(formattedQuery).Groups;

            var presetCapture = groups["preset"].Captures.FirstOrDefault();
            var overrideFlagCapture = groups["override"].Captures.FirstOrDefault();
            var promptCapture = groups["prompt"].Captures.FirstOrDefault();

            var presetFactory = int.TryParse(presetCapture?.Value, out var presetFactoryIndex)
                ? _presetFactoryProvider.GetPresetFactory(presetFactoryIndex)
                : _presetFactoryProvider.GetDefaultPresetFactory();
            var userPositivePrompt = promptCapture?.Value ?? string.Empty;
            var ignoreDefaultPrompt = overrideFlagCapture?.Value == "!" && !string.IsNullOrWhiteSpace(userPositivePrompt);

            return new Txt2ImgQuery(presetFactory, userPositivePrompt, ignoreDefaultPrompt);
        }
    }
}
