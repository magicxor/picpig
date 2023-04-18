using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Porn;

// Hassaku (hentai model): https://civitai.com/models/2583/hassaku-hentai-model
public class HassakuHentai : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"masterpiece, best quality, 1girl, yellow eyes, long hair, white hair, sitting, red kimono, large breasts, topless, light smile, arms behind back, nature, hair ornament, animal ears, bridge, nipples,";

    public override string DefaultNegativePrompt => @"ng_deepnegative_v1_75t, EasyNegative,fake, sketches, (cropped),(worst quality:2), (low quality:2), (normal quality:2), lowres, normal quality, ((monochrome)), ((grayscale)), skin spots, acnes, skin blemishes, age spot,(bad-artist:0.7), (ugly:1.331), (duplicate:1.331), (morbid:1.21), (mutilated:1.21), (tranny:1.331), mutated hands, (poorly drawn hands:1.5), blurry, (bad anatomy:1.21), (bad proportions:1.331), extra limbs, (disfigured:1.331), (more than 2 nipples:1.331), (missing arms:1.331), (extra legs:1.331), (fused fingers:1.61051), (too many fingers:1.61051), (unclear eyes:1.331), lowers, bad hands, missing fingers, extra digit, (futa:1.1),bad hands, missing fingers, (watermark:1.5), male:1.4, men:1.4 ,man:1.4 ,(kid:1.4)";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "hassakuHentaiModel_v11.safetensors",
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
            Sampler_name = "DDIM",
            Batch_size = 1,
            N_iter = 1,
            Steps = samplingSteps,
            Cfg_scale = 8,
            Width = 384,
            Height = 512,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = true,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.52,
            S_churn = 0.0,
            S_tmax = null,
            S_tmin = 0.0,
            S_noise = 1.0,
            Sampler_index = null,
            Save_images = false,

            Enable_hr = true,
            Hr_scale = 2,
            Hr_upscaler = "Latent",
            Hr_second_pass_steps = 7,
        };
    }
}
