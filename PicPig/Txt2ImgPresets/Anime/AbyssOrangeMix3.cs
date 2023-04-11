using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Anime;

// AbyssOrangeMix3 (AOM3): https://civitai.com/models/9942/abyssorangemix3-aom3
public class AbyssOrangeMix3 : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"masterpiece, best quality, ultra-detailed, illustration, 1girl, solo, outdoors, camping, night, mountains, nature, stars, moon, bonfire, tent, twin ponytails, green eyes, cheerful, happy, backpack, sleeping bag, camping stove, water bottle, mountain boots, gloves, sweater, hat, flashlight, forest, rocks, river, wood, smoke, shadows, contrast, clear sky, constellations, Milky Way, peaceful, serene, quiet, tranquil, remote, secluded, adventurous, exploration, escape, independence, survival, resourcefulness, challenge, perseverance, stamina, endurance, observation, intuition, adaptability, creativity, imagination, artistry, inspiration, beauty, awe, wonder, gratitude, appreciation, relaxation, enjoyment, rejuvenation, mindfulness, awareness, connection, harmony, balance, texture, detail, realism, depth, perspective, composition, color, light, shadow, reflection, refraction, tone, contrast, foreground, middle ground, background, naturalistic, figurative, representational, impressionistic, expressionistic, abstract, innovative, experimental, unique";

    public override string DefaultNegativePrompt => @"(worst quality, low quality:1.4), (realistic, lip, nose, tooth, rouge, lipstick, eyeshadow:1.0), (dusty sunbeams:1.0),, (abs, muscular, rib:1.0), (depth of field, bokeh, blurry:1.4), (greyscale, monochrome:1.0), text, title, logo, signature";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "abyssorangemix3AOM3_aom3a3.safetensors",
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
            Width = 768,
            Height = 512,
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

            Enable_hr = true,
            Hr_scale = 1.5,
            Hr_upscaler = "SwinIR_4x",
            Hr_second_pass_steps = 7,
        };
    }
}
