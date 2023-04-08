using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets;

public abstract class BasePresetFactory
{
    protected const int DefaultSamplingSteps = 30;

    public abstract string DefaultPositivePrompt { get; }

    public abstract  string DefaultNegativePrompt { get; }

    public abstract StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps);
}
