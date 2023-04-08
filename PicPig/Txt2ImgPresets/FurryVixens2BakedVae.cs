using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets;

// https://civitai.com/models/17333/furry-vixens
public class FurryVixens2BakedVae : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"1girl, nsfw, (furry:1.2), (fluffy:1.1), (anthro:1.2), (snout:1.2), short hair, (black hair | red hair), (wizard:1.4, magic:1.2, cleavage), looking at viewer, (fantasy:1.2, city)";

    public override string DefaultNegativePrompt => @"(solo:1.4), (worst quality:1.4), (low quality:1.4), (cropped head:1.4), (blurry), (lipstick), pink lips";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "furryVixens_v20BakedVAE.safetensors",
                eta_noise_seed_delta = 0,
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
            Sampler_name = "DPM++ 2M Karras",
            Batch_size = 1,
            N_iter = 1,
            Steps = samplingSteps,
            Cfg_scale = 6,
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
            Hr_scale = 1.5,
            Hr_upscaler = "R-ESRGAN 4x+ Anime6B",
            Hr_second_pass_steps = 7,
        };
    }
}
