using PicPig.Enums;
using PicPig.Exceptions;
using PicPig.Txt2ImgPresets;

namespace PicPig.Services;

public class PresetFactoryProvider
{
    private readonly IReadOnlyDictionary<string, BasePresetFactory> _presetFactories = new Dictionary<string, BasePresetFactory>
    {
        { nameof(DreamShaper4BakedVae), new DreamShaper4BakedVae() },
        { nameof(MantersBatchFurry), new MantersBatchFurry() },
        { nameof(FurryVixens2BakedVae), new FurryVixens2BakedVae() },
        { nameof(TheAllysMix3Revolutions), new TheAllysMix3Revolutions() },
        { nameof(NeverEndingDream), new NeverEndingDream() },
    }.AsReadOnly();

    private readonly Random _random = new();

    public PresetFactoryProvider()
    {
    }

    public PresetFactoryProvider(IReadOnlyDictionary<string, BasePresetFactory> presetFactories, Random random)
    {
        _presetFactories = presetFactories;
        _random = random;
    }

    public BasePresetFactory GetPresetFactory(string name)
    {
        return _presetFactories.TryGetValue(name, out var presetFactory)
            ? presetFactory
            : throw new ServiceException(LocalizationKeys.Errors.Txt2Img.PresetFactoryNotFound,
                new Dictionary<string, string>{ { "method", nameof(GetPresetFactory) }, { "key", nameof(name) }, { nameof(name), name } });
    }

    public BasePresetFactory GetPresetFactory(int index)
    {
        return index < _presetFactories.Count && index >= 0
            ? _presetFactories.ElementAt(index).Value
            : throw new ServiceException(LocalizationKeys.Errors.Txt2Img.PresetFactoryNotFound,
                new Dictionary<string, string>{ { "method", nameof(GetPresetFactory) }, { "key", nameof(index) }, { nameof(index), index.ToString() } });
    }

    public BasePresetFactory GetRandomPresetFactory()
    {
        return _presetFactories.Count != 0
            ? _presetFactories.ElementAt(_random.Next(0, _presetFactories.Count)).Value
            : throw new ServiceException(LocalizationKeys.Errors.Txt2Img.PresetFactoryNotFound,
                new Dictionary<string, string>{ { "method", nameof(GetRandomPresetFactory) } });
    }

    public BasePresetFactory GetDefaultPresetFactory()
    {
        return _presetFactories.Count != 0
            ? _presetFactories.First().Value
            : throw new ServiceException(LocalizationKeys.Errors.Txt2Img.PresetFactoryNotFound,
                new Dictionary<string, string>{ { "method", nameof(GetDefaultPresetFactory) } });
    }
}
