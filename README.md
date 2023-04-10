# picpig
A telegram [bot](https://github.com/TelegramBots/Telegram.Bot) working as a frontend for [stable-diffusion-webui](https://github.com/AUTOMATIC1111/stable-diffusion-webui)

# Demo

https://user-images.githubusercontent.com/8275793/230755520-da1b77a6-e566-465e-9c66-631fdd8a39c9.mp4

# Syntax

```
[model number][!][prompt]
```

- `model number` - a number of model (from 0 to 4: DreamShaper4BakedVae, MantersBatchFurry, FurryVixens2BakedVae, TheAllysMix3Revolutions, NeverEndingDream)
- `!` - specify if you want to ignore the standard prompt

# Models

- [DreamShaper](https://civitai.com/models/4384/dreamshaper)
- [Manter's Batch](https://civitai.com/models/20957/manters-batch-furry-model)
- [Furry Vixens](https://civitai.com/models/17333/furry-vixens)
- [The Ally's Mix III: Revolutions](https://civitai.com/models/10752/the-allys-mix-iii-revolutions)
- [NeverEnding Dream](https://civitai.com/models/10028/neverending-dream)

# Upscalers

- 4x_fatal_Anime_500000_G.pth
- 4x_foolhardy_Remacri.pth
- RealESRGAN_x4plus_anime_6B.pth

# VAE

- anything-v4.0.vae.pt
- orangemix.vae.pt
- Crosskemono.vae.pt
- kl-f8-anime2.vae.pt
- vae-ft-mse-840000-ema-pruned.vae.pt

# Lora

- arcaneStyleLora_offset.safetensors
- openjourneyLora_v1.safetensors
- torinoAquaStyleLora_v1.safetensors
- roundGlasses_v1a.safetensors
- virtualgirlAim_v20.safetensors

# Embeddings

- [EasyNegative.pt](https://huggingface.co/datasets/gsdf/EasyNegative)
- [bad-artist.pt](https://huggingface.co/nick-x-hacker/bad-artist)
- [bad_prompt_version2.pt](https://huggingface.co/datasets/Nerfgun3/bad_prompt)
- [boring_e621.pt](https://huggingface.co/FoodDesert/boring_e621)
- [ng_deepnegative_v1_75t.pt](https://civitai.com/models/4629/deep-negative-v1x)

# WebUI launch script

```bat
@echo off

set SAFETENSORS_FAST_GPU=1
set PYTHON=
set GIT=
set VENV_DIR=
set COMMANDLINE_ARGS=--api --api-log --xformers --no-half-vae

call webui.bat
```
