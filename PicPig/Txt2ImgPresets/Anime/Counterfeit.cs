using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Anime;

// Counterfeit: https://civitai.com/models/4468/counterfeit-v25
public class Counterfeit : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"masterpiece, best quality, girl, cute face, brown eyes, mole on breast, sailor shirt, white kneehighs, uwabaki, hair ribbon, hair_bow, arm at side , long hair, platinum blonde hair, pointy ears,";

    public override string DefaultNegativePrompt => @"EasyNegative, extra fingers,fewer fingers,(watermark),sketch, duplicate, ugly, huge eyes, text, logo, monochrome, worst face, (bad and mutated hands:1.3), (worst quality:2.0), (low quality:2.0), (blurry:2.0), horror, geometry, (bad hands), (missing fingers), multiple limbs, bad anatomy, (interlocked fingers:1.2), Ugly Fingers, (extra digit and hands and fingers and legs and arms:1.4), crown braid, ((2girl)), (deformed fingers:1.2), (long fingers:1.2),(bad-artist-anime),";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "CounterfeitV25_25.safetensors",
                eta_noise_seed_delta = 31337,
                CLIP_stop_at_last_layers = 2,
                sd_vae = "Counterfeit-V2.5.vae.pt",
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
            Steps = 40,
            Cfg_scale = 7,
            Width = 384,
            Height = 512,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = true,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.6,
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
