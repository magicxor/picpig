using PicPig.Txt2ImgPresets;

namespace PicPig.Models;

public record Txt2ImgQuery(
    BasePresetFactory PresetFactory,
    string UserPositivePrompt,
    bool IgnoreDefaultPrompt
);
