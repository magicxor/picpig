using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Furry;

// BB95 Furry Mix v2: https://civitai.com/models/17649/bb95-furry-mix-v2
// todo: Hires Fix ?
public class Bb95FurryMixV2 : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"anthro white wolf, white fur, adult, solo, pose, fit body, shorts, topless, tails, looking at viewer, realistic eyes, realistic fur, realistic hands, pubic hair, veiny muscles, (((cinematic lighting, outside background))), photorealistic, detailed fur";

    public override string DefaultNegativePrompt => @"bad-artist boring_e621 easynegative";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "bb95FurryMixV2_v20.safetensors",
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
            Sampler_name = "UniPC",
            Batch_size = 1,
            N_iter = 1,
            Steps = samplingSteps,
            Cfg_scale = 12,
            Width = 384,
            Height = 512,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = false,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.6,
            S_churn = 0.0,
            S_tmax = null,
            S_tmin = 0.0,
            S_noise = 1.0,
            Sampler_index = null,
            Save_images = true,

            Enable_hr = true,
            Hr_scale = 2,
            Hr_upscaler = "Latent",
            Hr_second_pass_steps = 7,
        };
    }
}
