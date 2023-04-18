using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Furry;

// Lawlas's yiffymix (furry model): https://civitai.com/models/4698/lawlass-yiffymix-furry-model
public class LawlasYiffymix : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"uploaded on e621, by personalami, Michael & Inessa Garmash, Ruan Jia, Pino Daeni, garden, solo (bright red) ((female)) anthro, (high angle shot), detailed face, detailed eyes, detailed fluffy fur, fluffy tail, short hair, digitigrade, seductive pose, suit, fancy pants, digital painting, natural lighting, cleavage, photorealistic (pinup) (Victorian clothing) medals (smug face) smiling (cocky), eyewear, steampunk, golden jewelry, (furry hands)";

    public override string DefaultNegativePrompt => @"(worst quality, low quality:1.4), art by bad_artist, bad-artist, bad anatomy, ((mutated hands)), text, error, missing fingers, extra digit, fewer digits, cropped, jpeg artifacts, signature, watermark, username, blurry, pregnant, (light skinned hands)";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "lawlassYiffymixFurry_lawlasmixOGWithvae7Gb.safetensors",
                eta_noise_seed_delta = 1,
                CLIP_stop_at_last_layers = 1,
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
            Cfg_scale = 7.5,
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
