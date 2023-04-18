using StableDiffusionClient;

namespace PicPig.Txt2ImgPresets.RealisticArt;

// The Ally's Mix III: Revolutions: https://civitai.com/models/10752/the-allys-mix-iii-revolutions
public class TheAllysMix3Revolutions : BasePresetFactory
{
    public override string DefaultPositivePrompt => @"masterpiece, highest quality, (perfect face:1.1), (high detail:1.1),dramatic,dynamic pose, 1girl, breasts, blonde hair, blue eyes, (finely detailed beautiful eyes: 1.2), solo, cleavage, long hair, curvy, large breasts,see-through, dress,thighs,covered navel, white dress, bare shoulders, (wide hips:0.8), (thick thighs:0.8), lips, covered nipples, robe, pelvic curtain,castle,night,moon, detailed background, art by artgerm and greg rutkowski, cinematic lighting,";

    public override string DefaultNegativePrompt => @"3d, cartoon, anime, sketches, (worst quality:2), (low quality:2), (normal quality:2), (muscular), low-res, normal quality, ((monochrome)), ((grayscale)), skin spots, acne, skin blemishes, bad anatomy, ((child)) ((loli)), tattoos, bad_prompt_version2, ng_deepnegative_v1_75t, (asian.1.2) bad-hands-5, handbag, Poorly drawn hands, ((too many fingers)), ((bad fingers)) bad-image-v2-39000, teeth, small lips, earrings, curly hair, asian, afroamerican, african, (worst quality, low quality:1.4), (((deformed))), blurry, bad anatomy, disfigured, poorly drawn face, mutation, mutated, (extra_limb), (ugly), (poorly drawn hands), fused fingers, canvas frame, cartoon, 3d, ((disfigured)), ((bad art)), ((deformed)),((extra limbs)),((close up)),((b&w)), wierd colors, blurry, (((duplicate))), ((morbid)), ((mutilated)), [out of frame], extra fingers, mutated hands, ((poorly drawn hands)), ((poorly drawn face)), (((mutation))), (((deformed))), ((ugly)), blurry, ((bad anatomy)), (((bad proportions))), ((extra limbs)), cloned face, (((disfigured))), out of frame, ugly, extra limbs, (bad anatomy), gross proportions, (malformed limbs), ((missing arms)), ((missing legs)), (((extra arms))), (((extra legs))), mutated hands, (fused fingers), (too many fingers), (((long neck))), Photoshop, video game, ugly, tiling, poorly drawn hands, poorly drawn feet, poorly drawn face, out of frame, mutation, mutated, extra limbs, extra legs, extra arms, disfigured, deformed, cross-eye, body out of frame, blurry, bad art, bad anatomy, 3d render, mutated eyes, cleavage, lowres, bad anatomy, bad hands, text, error, missing fingers, extra digit, fewer digits, cropped, worst quality, low quality, normal quality, jpeg artifacts, signature, watermark, username, blurry, crown, hat, multiple hands, bad hands, bad anatomy, ugly, gallant, blurry, floating, multiple body, duplicate, extra fingers, extra limbs, mutation, deformed, disfigured, morbid, long body, asymmetric, poorly drawn face, cloned face, cloned head, asymmetric ears, asymmetric ear rings, floating hair, (long neck), cross eyed, blinking, winking, makeup, lipstick, eye liner, eyeliner, kitsch, bad art, bad taste, unnatural, staged, missing limb, missing arm, missing leg, floating limb, disconnected limb, extra limb, too many fingers, extra fingers, malformed limbs, malformed hands, poorly drawn hands, (mutated hands), (mutated fingers), mutilated hands, extra tail, missing tail,";

    public override StableDiffusionProcessingTxt2Img GetRequestData(string? positivePrompt, int samplingSteps = DefaultSamplingSteps)
    {
        return new StableDiffusionProcessingTxt2Img
        {
            Override_settings = new
            {
                sd_model_checkpoint = "theAllysMixIII_v10.safetensors",
                eta_noise_seed_delta = 31337,
                CLIP_stop_at_last_layers = 2,
                sd_vae = "anything-v4.0.vae.pt",
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
            Sampler_name = "DPM++ 2S a Karras",
            Batch_size = 1,
            N_iter = 1,
            Steps = samplingSteps,
            Cfg_scale = 9,
            Width = 512,
            Height = 656,
            Restore_faces = false,
            Tiling = false,
            Do_not_save_samples = true,
            Do_not_save_grid = true,
            Eta = null,
            Denoising_strength = 0.55,
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
