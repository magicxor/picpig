using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.RealisticArt;

// GalaxyTimeMachine's "ForYou-Fantasy": https://civitai.com/models/25611/galaxytimemachines-foryou-fantasy-fantasyai
public class GalaxyTimeMachineFantasy : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"Man at edge of creation of the universe";

    public override string DefaultNegativePrompt => @"sketch, comic, (loli:1.2), (child:1.2), (aged down:1.2), disfigured, missing limbs, extra limbs, extra legs, extra arms, cartoon, 3d, deformed eyes, bad-hands-5";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "galaxytimemachines_v10.safetensors",
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
            Sampler_name = "DPM++ SDE Karras",
            Batch_size = 1,
            N_iter = 1,
            Steps = samplingSteps,
            Cfg_scale = 5.5,
            Width = 512,
            Height = 512,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = true,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.5,
            S_churn = 0.0,
            S_tmax = null,
            S_tmin = 0.0,
            S_noise = 1.0,
            Sampler_index = null,
            Save_images = false,

            Enable_hr = true,
            Hr_scale = 2,
            Hr_upscaler = "4x_Valar_v1",
            Hr_second_pass_steps = 7,
        };
    }
}
