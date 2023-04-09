using System.Diagnostics;
using PicPig.Enums;
using PicPig.Exceptions;
using PicPig.Models;
using SixLabors.ImageSharp.Formats.Jpeg;
using StableDiffusionClient;

namespace PicPig.Services;

public class Txt2ImgService
{
    private readonly Txt2ImgQueryParser _txt2ImgQueryParser;
    private readonly Client _stableDiffusionClient;

    public Txt2ImgService(Txt2ImgQueryParser txt2ImgQueryParser,
        Client stableDiffusionClient)
    {
        _txt2ImgQueryParser = txt2ImgQueryParser;
        _stableDiffusionClient = stableDiffusionClient;
    }

    public async Task<Txt2ImgResult> GenerateImageAsync(string userQuery,
        CancellationToken cancellationToken)
    {
        var txt2ImgQuery = _txt2ImgQueryParser.Parse(userQuery);

        var stopwatch = Stopwatch.StartNew();
        var imageStream = await GenerateImageAsync(txt2ImgQuery, cancellationToken);
        stopwatch.Stop();

        return new Txt2ImgResult(imageStream, txt2ImgQuery, stopwatch.Elapsed);
    }

    public async Task<TextToImageResponse> GetStableDiffusionResponseAsync(StableDiffusionProcessingTxt2Img request,
        CancellationToken cancellationToken)
    {
        return await _stableDiffusionClient.Text2imgapi_sdapi_v1_txt2img_postAsync(request, cancellationToken);
    }

    public void ConvertToJpg(byte[] file, Stream outputStream)
    {
        using var image = Image.Load(file);
        image.SaveAsJpeg(outputStream, new JpegEncoder { Quality = 85 });
        outputStream.Position = 0;
    }

    public async Task StableDiffusionResponseToStreamAsync(TextToImageResponse textToImageResponse,
        bool compress,
        Stream outputStream,
        CancellationToken cancellationToken)
    {
        if (textToImageResponse.Images == null || textToImageResponse.Images.Count == 0)
        {
            throw new ServiceException(LocalizationKeys.Errors.Txt2Img.NoImagesReturned);
        }

        var imageBase64 = textToImageResponse.Images.First();
        var imageBytes = Convert.FromBase64String(imageBase64);

        if (compress)
        {
            ConvertToJpg(imageBytes, outputStream);
        }
        else
        {
            await outputStream.WriteAsync(imageBytes, cancellationToken);
        }
    }

    public async Task<Stream> GenerateImageAsync(Txt2ImgQuery txt2ImgQuery,
        CancellationToken cancellationToken)
    {
        try
        {
            var positivePrompt = txt2ImgQuery.IgnoreDefaultPrompt
                ? txt2ImgQuery.UserPositivePrompt
                : string.Join(", ", txt2ImgQuery.UserPositivePrompt, txt2ImgQuery.PresetFactory.DefaultPositivePrompt);

            var requestData = txt2ImgQuery.PresetFactory.GetRequestData(positivePrompt);

            var response = await GetStableDiffusionResponseAsync(requestData, cancellationToken);

            var outputStream = new MemoryStream();
            await StableDiffusionResponseToStreamAsync(response, true, outputStream, cancellationToken);

            return outputStream;
        }
        catch (Exception e)
        {
            throw new ServiceException(LocalizationKeys.Errors.Txt2Img.ErrorQueryingStableDiffusion,
                e,
                new Dictionary<string, string>
                {
                    { nameof(Txt2ImgQuery.PresetFactory), txt2ImgQuery.PresetFactory.GetType().Name },
                    { nameof(Txt2ImgQuery.UserPositivePrompt), txt2ImgQuery.UserPositivePrompt },
                    { nameof(Txt2ImgQuery.IgnoreDefaultPrompt), txt2ImgQuery.IgnoreDefaultPrompt.ToString() },
                });
        }
    }
}
