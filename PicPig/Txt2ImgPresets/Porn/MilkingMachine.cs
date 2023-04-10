using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Porn;

// Milking machine: https://civitai.com/models/8500/milking-machine
// todo: highres.fix ?
public class MilkingMachine : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"(milking machine:1.2), nipples, lactation, medium breasts, cowbell, long hair, elbow gloves, (cow print:1.2), thighhighs, leotard, steam, leaning forward, cowboy shot, indoors, arms behind back,";

    public override string DefaultNegativePrompt => @"(EasyNegative:1.0), (monochrome:1.1), (greyscale)";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "AbyssOrangeMix2_hard.safetensors",
                eta_noise_seed_delta = 31337,
                CLIP_stop_at_last_layers = 2,
                sd_vae = "kl-f8-anime2.vae.pt",
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
            Cfg_scale = 7,
            Width = 384,
            Height = 512,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = false,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.23,
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
