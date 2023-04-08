using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets;

public class MantersBatchFurry : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"Masterpiece, best quality, Wolf_male, detailed_fluffy_fur, uploaded_to_e621, antro, furry_male, Masterpiece, Best_quality, solo, (Antro), kemono art style, sfw";

    public override string DefaultNegativePrompt => @"lowres, bad anatomy, bad hands, text, error, missing fingers, extra digit, fewerdigits, cropped, worst quality, low quality, normal quality, ipeg artifacts, signature, watermark,username, blurry, comic, panels, bad anatomy, disfigured, weird anatomy, deformed mutated disfigured hands, mutated, multiple balls, multiple tits, deformed penis, deformed balls,";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "mantersBatchFurry_mantersBatchV3.safetensors",
                eta_noise_seed_delta = 0,
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
            Sampler_name = "Euler a",
            Batch_size = 1,
            N_iter = 1,
            Steps = samplingSteps,
            Cfg_scale = 12,
            Width = 576,
            Height = 576,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = false,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.7,
            S_churn = 0.0,
            S_tmax = null,
            S_tmin = 0.0,
            S_noise = 1.0,
            Sampler_index = null,
            Save_images = true,

            Enable_hr = true,
            Hr_scale = 1.5,
            Hr_upscaler = "Latent",
            Hr_second_pass_steps = 7,
        };
    }
}
