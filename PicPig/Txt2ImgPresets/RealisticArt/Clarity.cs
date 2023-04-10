using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.RealisticArt;

// Clarity: https://civitai.com/models/5062/clarity
public class Clarity : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"photo of (missmiafit:0.99), a woman, photorealistic painting, (full body) portrait, stunningly attractive, at a (music festival:1.2), ((highly detailed face)), (glitter freckles), glitter, wearing a floral dress, intricate, 8k, highly detailed, volumetric lighting, digital painting, intense, sharp focus, art by artgerm and rutkowski and alphonse mucha, cgsociety, ((detailed eyes)), (leather belt:1.1)";

    public override string DefaultNegativePrompt => @"cartoon, 3d, (disfigured), (bad art), (deformed), (poorly drawn), (extra limbs), (close up), blurry,";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "clarity_2.safetensors",
                eta_noise_seed_delta = 31337,
                CLIP_stop_at_last_layers = 2,
                sd_vae = "vae-ft-mse-840000-ema-pruned.vae.pt",
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
            Width = 384,
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
            Hr_upscaler = "Latent",
            Hr_second_pass_steps = 7,
        };
    }
}
