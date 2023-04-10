using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Anime;

// AbyssOrangeMix3 (AOM3): https://civitai.com/models/9942/abyssorangemix3-aom3
public class AbyssOrangeMix3 : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"masterpiece, best quality, ultra-detailed, illustration, 1girl, solo, fantasy, flying, broom, night sky, outdoors, magic, spells, moon, stars, clouds, wind, hair, cape, hat, boots, broomstick, glowing, mysterious, enchanting, whimsical, playful, adventurous, freedom, wonder, imagination, determination, skill, speed, movement, energy, realism, naturalistic, figurative, representational, beauty, fantasy culture, mythology, fairy tales, folklore, legends, witches, wizards, magical creatures, fantasy worlds, composition, scale, foreground, middle ground, background, perspective, light, color, texture, detail, beauty, wonder";

    public override string DefaultNegativePrompt => @"(worst quality, low quality:1.4), (lip, nose, tooth, rouge, lipstick, eyeshadow:1.4), (jpeg artifacts:1.4), (depth of field, bokeh, blurry, film grain, chromatic aberration, lens flare:1.0), greyscale, monochrome, dusty sunbeams, trembling, motion lines, motion blur, emphasis lines, text, title, logo, signature";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "abyssorangemix3AOM3_aom3a1b.safetensors",
                eta_noise_seed_delta = 31337,
                CLIP_stop_at_last_layers = 2,
                // maybe orangemix.vae.pt is better?
                sd_vae = "kl-f8-anime2.vae.pt",
            },
            Override_settings_restore_afterwards = false,
            Prompt = positivePrompt,
            Negative_prompt = DefaultNegativePrompt,
            Styles = new List<string>(),
            Seed = -1,
            Subseed = -1,
            Subseed_strength = 0.0,
            Seed_resize_from_h = 0,
            Seed_resize_from_w = 0,
            Sampler_name = "DPM++ SDE Karras",
            Batch_size = 1,
            N_iter = 1,
            Steps = samplingSteps,
            Cfg_scale = 7,
            Width = 512,
            Height = 832,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = false,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.5,
            S_churn = 0.0,
            S_tmax = null,
            S_tmin = 0.0,
            S_noise = 1.0,
            Sampler_index = null,
            Save_images = true,
        };
    }
}
