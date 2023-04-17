using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Furry;

// Lawlas's Yiffymix 2.0 (furry model): https://civitai.com/models/12979/lawlass-yiffymix-20-furry-model
public class LawlasYiffymix2 : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"(furry art, uploaded on e621:1.4), (ruins, vines, flowers:1.2), 1girl, solo_focus, (blue:1.3) female (anthro furry:1.4), (blue fur:1.4), (high angle shot:1.2), detailed face, detailed eyes, (furry blue colored breasts:1.4), (detailed fur texture:1.3), (unique haircut:1.5), big fluffy tail, short hair, (seductive pose:1.2), suit, fancy pants, digital painting, perfect hands, natural lighting, cleavage, photorealistic (pinup) (Victorian clothing:1.3), medals, smiling, steampunk, golden jewelry";

    public override string DefaultNegativePrompt => @"(worst quality, low quality:1.4), boring_e621, bad anatomy, (human, smooth skin:1.3), (mutated body:1.3), blurry, text, error, missing fingers, extra digit, fewer digits, cropped, jpeg artifacts, signature, watermark, username, blurry, pregnant, explicit content, nude";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "lawlassYiffymix20Furry_lawlasmixWithBakedIn.safetensors",
                eta_noise_seed_delta = 1,
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
            Cfg_scale = 7,
            Width = 384,
            Height = 512,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = true,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.2,
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
