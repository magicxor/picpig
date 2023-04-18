using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.RealisticArt;

// SXZ Luma: https://civitai.com/models/25831/sxz-luma
public class SxzLuma : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"best quality, intricate details, 1girl,ahri, ahri \(league of legends\), red and white kimino, cleavage, marks on face, yellow eyes, fox ears, fox tails, long black hair, messy hair, (flaming fox tails:1.3), space, stars, clouds, shiny, celestial, woman, goddess, wavy, purple cosmic dust, serenity, distinct, outerspace, earth,milky_way,starry";

    public override string DefaultNegativePrompt => @"(worst quality:1.4), (low quality:1.4), (monochrome:1.1), easynegative, bad-artist-anime, bad-image-v2-39000, bad_prompt_version2, bad_quality, ng_deepnegative_v1_75t, verybadimagenegative_v1.1-6400, vile_prompt3, bad-hands-5";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "sxzLuma_092.safetensors",
                eta_noise_seed_delta = 31337,
                CLIP_stop_at_last_layers = 2,
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
            Sampler_name = "DPM++ 2M Karras",
            Batch_size = 1,
            N_iter = 1,
            Steps = samplingSteps,
            Cfg_scale = 7,
            Width = 512,
            Height = 768,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = true,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.45,
            S_churn = 0.0,
            S_tmax = null,
            S_tmin = 0.0,
            S_noise = 1.0,
            Sampler_index = null,
            Save_images = false,

            Enable_hr = true,
            Hr_scale = 2,
            Hr_upscaler = "4x-UltraSharp",
            Hr_second_pass_steps = 7,
        };
    }
}
