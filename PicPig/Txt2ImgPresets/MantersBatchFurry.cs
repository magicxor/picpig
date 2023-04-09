using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets;

// Manter's Batch: https://civitai.com/models/20957/manters-batch-furry-model
public class MantersBatchFurry : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"Masterpiece, best quality, Wolf_male, detailed_fluffy_fur, uploaded_to_e621, antro, furry_male, Masterpiece, Best_quality, solo, (Antro), kemono art style, sfw";

    public override string DefaultNegativePrompt => @"((amputee)), artist signature, asymmetric, asymmetric ear rings, asymmetric ears, ((bad anatomy)), ((bad art)), bad hands, bad hands, (((bad proportions))), bad taste, blinking, ((blurry)), body out of frame, (border), canvas frame, cleavage, cloned face, cloned head, closed eyes, comic, cropped, cross-eye, (cross-eyed), (((deformed))), (deformed body parts), (deformed eyes), deformed face, deformed feet, deformed hands, dialogue, disconnected limb, (((disfigured))), disfigured hands, disgusting, disproportional, (((duplicate))), error, (((extra arms))), (extra body parts), extra digit, (extra ears), extra fingers, (((extra legs))), extra limb, ((extra limbs)), extra tail, (extra_limb), eye liner, eyeliner, face out of frame, fat, fewer digits, fewerdigits, floating, floating hair, floating limb, (fused fingers), gallant, gross, gross proportions, hat, horror, ipeg artifacts, kitsch, large eyes, (letters), lipstick, long body, (low contrast), low quality, lowres, lowres, makeup, malformed, (malformed genitals), ((malformed hands)), (malformed limbs), (merged body parts), (merged limbs), missing arm, ((missing arms)), (missing body parts), missing fingers, ((missing hands)), missing leg, ((missing legs)), missing limb, ((missing limbs)), (missing pupils), missing tail, monstrous, ((morbid)), multi balls, multi penis, multiple body, multiple hands, multiple tails, multiple tits, mutant, mutated, mutated eyes, (mutated fingers), (mutated genitals), (mutated hands), (((mutation))), ((mutilated)), mutilated hands, normal quality, out of focus, out of frame, oversized body parts, overweight, panels, Photoshop, (poorly drawn), ((poorly drawn face)), poorly drawn feet, ((poorly drawn hands)), signature, (split limbs), text, tiling, (too many fingers), ((ugly)), unnatural, username, watermark, weird anatomy, winking, worst quality";

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
