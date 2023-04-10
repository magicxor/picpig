using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.Furry;

// Furry Vixens: https://civitai.com/models/17333/furry-vixens
public class FurryVixens2BakedVae : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"1girl, nsfw, (furry:1.2), (fluffy:1.1), (anthro:1.2), (snout:1.2), short hair, (black hair | red hair), (wizard:1.4, magic:1.2, cleavage), looking at viewer, (fantasy:1.2, city)";

    public override string DefaultNegativePrompt => @"(solo:1.4), (worst quality:1.4), (low quality:1.4), (cropped head:1.4), (lipstick), pink lips, ((amputee)), artist signature, asymmetric, asymmetric ear rings, asymmetric ears, ((bad anatomy)), ((bad art)), bad hands, bad hands, (((bad proportions))), bad taste, blinking, ((blurry)), body out of frame, (border), canvas frame, cleavage, cloned face, cloned head, closed eyes, comic, cropped, cross-eye, (cross-eyed), (((deformed))), (deformed body parts), (deformed eyes), deformed face, deformed feet, deformed hands, dialogue, disconnected limb, (((disfigured))), disfigured hands, disgusting, disproportional, (((duplicate))), error, (((extra arms))), (extra body parts), extra digit, (extra ears), extra fingers, (((extra legs))), extra limb, ((extra limbs)), extra tail, (extra_limb), eye liner, eyeliner, face out of frame, fat, fewer digits, fewerdigits, floating, floating hair, floating limb, (fused fingers), gallant, gross, gross proportions, hat, horror, ipeg artifacts, kitsch, large eyes, (letters), lipstick, long body, (low contrast), low quality, lowres, lowres, makeup, malformed, (malformed genitals), ((malformed hands)), (malformed limbs), (merged body parts), (merged limbs), missing arm, ((missing arms)), (missing body parts), missing fingers, ((missing hands)), missing leg, ((missing legs)), missing limb, ((missing limbs)), (missing pupils), missing tail, monstrous, ((morbid)), multi balls, multi penis, multiple body, multiple hands, multiple tails, multiple tits, mutant, mutated, mutated eyes, (mutated fingers), (mutated genitals), (mutated hands), (((mutation))), ((mutilated)), mutilated hands, normal quality, out of focus, out of frame, oversized body parts, overweight, panels, Photoshop, (poorly drawn), ((poorly drawn face)), poorly drawn feet, ((poorly drawn hands)), signature, (split limbs), text, tiling, (too many fingers), ((ugly)), unnatural, username, watermark, weird anatomy, winking, worst quality";

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
            Hr_scale = 2,
            Hr_upscaler = "R-ESRGAN 4x+ Anime6B",
            Hr_second_pass_steps = 7,
        };
    }
}
