using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.RealisticArt;

// ReV Animated: https://civitai.com/models/7371/rev-animated
public class RevAnimated : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"((best quality)), ((masterpiece)), (detailed), ultra high quality CG, Scherazard, crop top, gauntlets, purple skirt, white shawl, standing, farmhouse, green eyes, rural setting, 4:3 aspect ratio, vibrant colors";

    public override string DefaultNegativePrompt => @"(worst quality, low quality:1.4), (bad-hands-5,bad-artist-anime, bad-artist bad_prompt, bad_prompt_version2, bad_quality:0.9), easynegative, NG_DeepNegative_V1_75T";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "revAnimated_v121.safetensors",
                eta_noise_seed_delta = 31337,
                CLIP_stop_at_last_layers = 2,
                sd_vae = "orangemix.vae.pt",
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
            Hr_scale = 2,
            Hr_upscaler = "R-ESRGAN 4x+ Anime6B",
            Hr_second_pass_steps = 7,
        };
    }
}
