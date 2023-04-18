using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Anime;

// MeinaMix: https://civitai.com/models/7240/meinamix
public class MeinaMix : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"realistic, 1girl, blue hair, horns, multicolored eyes, glowing, blue eye, purple eye, bare shoulders, demon eyes, magic circle, light particles, light rays, wallpaper,";

    public override string DefaultNegativePrompt => @"(worst quality:2, low quality:2), (zombie, sketch, interlocked fingers, comic), EasyNegative, monochrome, wrong_hand, wrong_feet, text, signature, watermark, username, extra digit, fewer digits, bad hands, bad anatomy, bad-artist, badhandv4";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "meinamix_meinaV9.safetensors",
                eta_noise_seed_delta = 31337,
                CLIP_stop_at_last_layers = 2,
                sd_vae = "auto",
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
            Sampler_name = "Euler a",
            Batch_size = 1,
            N_iter = 1,
            Steps = 40,
            Cfg_scale = 7,
            Width = 384,
            Height = 512,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = true,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.4,
            S_churn = 0.0,
            S_tmax = null,
            S_tmin = 0.0,
            S_noise = 1.0,
            Sampler_index = null,
            Save_images = false,

            Enable_hr = true,
            Hr_scale = 2,
            Hr_upscaler = "R-ESRGAN 4x+ Anime6B",
            Hr_second_pass_steps = 7,
        };
    }
}
