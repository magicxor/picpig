using System.Text.RegularExpressions;
using OneOf;
using PicPig.Exceptions;
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

    public OneOf<Txt2ImgQuery, ServiceException> Parse(string userQuery)
    {
        var formattedQuery = userQuery.Trim();
        if (string.IsNullOrWhiteSpace(formattedQuery) || formattedQuery == RandomMarker)
        {
            var presetFactoryResult = _presetFactoryProvider.GetRandomPresetFactory();
            return presetFactoryResult
                .Match<OneOf<Txt2ImgQuery, ServiceException>>(presetFactory => new Txt2ImgQuery(presetFactory, string.Empty, false),
                    exception => exception
                );
        }
        else
        {
            var groups = QueryRegex.Match(formattedQuery).Groups;

            var presetCapture = groups["preset"].Captures.FirstOrDefault();
            var overrideFlagCapture = groups["override"].Captures.FirstOrDefault();
            var promptCapture = groups["prompt"].Captures.FirstOrDefault();

            var presetFactoryResult = int.TryParse(presetCapture?.Value, out var presetFactoryIndex)
                ? _presetFactoryProvider.GetPresetFactory(presetFactoryIndex)
                : _presetFactoryProvider.GetDefaultPresetFactory();
            var userPositivePrompt = promptCapture?.Value ?? string.Empty;
            var ignoreDefaultPrompt = overrideFlagCapture?.Value == "!" && !string.IsNullOrWhiteSpace(userPositivePrompt);

            return presetFactoryResult
                .Match<OneOf<Txt2ImgQuery, ServiceException>>(presetFactory => new Txt2ImgQuery(presetFactory, userPositivePrompt, ignoreDefaultPrompt),
                    exception => exception
                );
        }
    }
}
