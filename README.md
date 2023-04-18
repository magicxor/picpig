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
- [Furry Vixens](https://civitai.com/models/17333/furry-vixens)
- [The Ally's Mix III: Revolutions](https://civitai.com/models/10752/the-allys-mix-iii-revolutions)
- [NeverEnding Dream](https://civitai.com/models/10028/neverending-dream)
- [ReV Animated](https://civitai.com/models/7371/rev-animated)
- [Dark Sushi Mix](https://civitai.com/models/24779/dark-sushi-mix-mix)
- [Lyriel](https://civitai.com/models/22922/lyriel)
- [MeinaMix](https://civitai.com/models/7240/meinamix)
- [Counterfeit-V2.5](https://civitai.com/models/4468/counterfeit-v25)
- [GalaxyTimeMachine's "ForYou-Fantasy"](https://civitai.com/models/25611/galaxytimemachines-foryou-fantasy-fantasyai)
- [SXZ Luma](https://civitai.com/models/25831/sxz-luma)
- [Dungeons N Waifu's](https://civitai.com/models/11718/dungeons-n-waifus-new-version-22)

# [Upscalers](https://upscale.wiki/wiki/Model_Database)

- 4x_fatal_Anime_500000_G.pth
- 4x_foolhardy_Remacri.pth
- RealESRGAN_x4plus_anime_6B.pth

# VAE

- [anything-v4.0.vae.pt](https://huggingface.co/andite/anything-v4.0)
- [orangemix.vae.pt](https://huggingface.co/WarriorMama777/OrangeMixs/tree/main/VAEs)
- [kl-f8-anime2.vae.pt](https://huggingface.co/Norisuke193/kl-f8-anime2)
- [vae-ft-mse-840000-ema-pruned.vae.pt](https://huggingface.co/stabilityai/sd-vae-ft-mse-original)

# Lora

- [arcaneStyleLora_offset.safetensors](https://civitai.com/models/7094/arcane-style-lora)
- [openjourneyLora_v1.safetensors](https://civitai.com/models/86/openjourney-aka-midjourney-v4)
- [torinoAquaStyleLora_v1.safetensors](https://civitai.com/models/5126/torino-aqua-style-lora)
- [roundGlasses_v1a.safetensors](https://civitai.com/models/21285/round-glasses-or-accessory)
- [virtualgirlAim_v20.safetensors](https://huggingface.co/jomcs/NeverEnding_Dream-Feb19-2023/tree/main/Realistic%20LORA)
- [hannahOwo_v1.safetensors](https://civitai.com/models/14959/hannah-owo)

# Embeddings

- [EasyNegative.pt](https://huggingface.co/datasets/gsdf/EasyNegative)
- [bad-artist.pt](https://huggingface.co/nick-x-hacker/bad-artist)
- [bad_prompt_version2.pt](https://huggingface.co/datasets/Nerfgun3/bad_prompt)
- [boring_e621.pt](https://huggingface.co/FoodDesert/boring_e621)
- [ng_deepnegative_v1_75t.pt](https://civitai.com/models/4629/deep-negative-v1x)
- [badhandv4](https://civitai.com/models/16993/badhandv4-animeillustdiffusion)

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
