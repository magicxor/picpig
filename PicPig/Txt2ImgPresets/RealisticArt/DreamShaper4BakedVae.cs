using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.RealisticArt;

// DreamShaper: https://civitai.com/models/4384/dreamshaper
public class DreamShaper4BakedVae : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"8k portrait, intricate, elegant, highly detailed, majestic, digital photography, art by artgerm and ruan jia and greg rutkowski surreal painting gold butterfly filigree, (masterpiece, sidelighting, finely detailed beautiful eyes: 1.2), hdr, rainy, rtx, octane, unreal, detailed background";

    public override string DefaultNegativePrompt => @"(worst quality, low quality:1.4), (((deformed))), blurry, bad anatomy, disfigured, poorly drawn face, mutation, mutated, (extra_limb), (ugly), (poorly drawn hands), fused fingers, canvas frame, cartoon, 3d, ((disfigured)), ((bad art)), ((deformed)),((extra limbs)),((close up)),((b&w)), wierd colors, blurry, (((duplicate))), ((morbid)), ((mutilated)), [out of frame], extra fingers, mutated hands, ((poorly drawn hands)), ((poorly drawn face)), (((mutation))), (((deformed))), ((ugly)), blurry, ((bad anatomy)), (((bad proportions))), ((extra limbs)), cloned face, (((disfigured))), out of frame, ugly, extra limbs, (bad anatomy), gross proportions, (malformed limbs), ((missing arms)), ((missing legs)), (((extra arms))), (((extra legs))), mutated hands, (fused fingers), (too many fingers), (((long neck))), Photoshop, video game, ugly, tiling, poorly drawn hands, poorly drawn feet, poorly drawn face, out of frame, mutation, mutated, extra limbs, extra legs, extra arms, disfigured, deformed, cross-eye, body out of frame, blurry, bad art, bad anatomy, 3d render, mutated eyes, cleavage, sfw, lowres, bad anatomy, bad hands, text, error, missing fingers, extra digit, fewer digits, cropped, worst quality, low quality, normal quality, jpeg artifacts, signature, watermark, username, blurry, crown, hat, multiple hands, bad hands, bad anatomy, ugly, gallant, blurry, floating, multiple body, duplicate, extra fingers, extra limbs, mutation, deformed, disfigured, morbid, long body, asymmetric, poorly drawn face, cloned face, cloned head, asymmetric ears, asymmetric ear rings, floating hair, (long neck), cross eyed, blinking, winking, makeup, lipstick, eye liner, eyeliner, kitsch, bad art, bad taste, unnatural, staged, missing limb, missing arm, missing leg, floating limb, disconnected limb, extra limb, too many fingers, extra fingers, malformed limbs, malformed hands, poorly drawn hands, (mutated hands), (mutated fingers), mutilated hands, extra tail, missing tail,";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "dreamshaper_4BakedVae.safetensors",
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
            Cfg_scale = 7,
            Width = 512,
            Height = 512,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = false,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.4,
            S_churn = 0.0,
            S_tmax = null,
            S_tmin = 0.0,
            S_noise = 1.0,
            Sampler_index = null,
            Save_images = true,
        };
    }
}
