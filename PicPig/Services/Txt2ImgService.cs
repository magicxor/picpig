using OneOf;
using OneOf.Types;
using PicPig.Enums;
using PicPig.Exceptions;
using PicPig.Models;
using SixLabors.ImageSharp.Formats.Jpeg;
using StableDiffusionClient;

namespace PicPig.Services;

public class Txt2ImgService
{
    private readonly ILogger<Txt2ImgService> _logger;
    private readonly Txt2ImgQueryParser _txt2ImgQueryParser;
    private readonly Client _stableDiffusionClient;

    public Txt2ImgService(ILogger<Txt2ImgService> logger,
        Txt2ImgQueryParser txt2ImgQueryParser,
        Client stableDiffusionClient)
    {
        _logger = logger;
        _txt2ImgQueryParser = txt2ImgQueryParser;
        _stableDiffusionClient = stableDiffusionClient;
    }

    public async Task<OneOf<Txt2ImgResult, ServiceException>> GenerateImageAsync(string userQuery,
        CancellationToken cancellationToken)
    {
        var queryParseResult = _txt2ImgQueryParser.Parse(userQuery);
        return await queryParseResult.Match<Task<OneOf<Txt2ImgResult, ServiceException>>>(
            async txt2ImgQuery =>
            {
                var txt2ImgResult = await GenerateImageAsync(txt2ImgQuery, cancellationToken);
                return txt2ImgResult.Match<OneOf<Txt2ImgResult, ServiceException>>(
                    stream => new Txt2ImgResult(stream, txt2ImgQuery),
                    exception => exception);
            },
            exception => Task.FromResult<OneOf<Txt2ImgResult, ServiceException>>(exception));
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

    public async Task<OneOf<Success, ServiceException>> StableDiffusionResponseToStreamAsync(TextToImageResponse textToImageResponse,
        bool compress,
        Stream outputStream,
        CancellationToken cancellationToken)
    {
        if (textToImageResponse.Images == null || textToImageResponse.Images.Count == 0)
        {
            return new ServiceException(LocalizationKeys.Errors.Txt2Img.NoImagesReturned);
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

        return new Success();
    }

    public async Task<OneOf<Stream, ServiceException>> GenerateImageAsync(Txt2ImgQuery txt2ImgQuery,
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
            var result = await StableDiffusionResponseToStreamAsync(response, true, outputStream, cancellationToken);

            return await result.Match<Task<OneOf<Stream, ServiceException>>>(_ => Task.FromResult<OneOf<Stream, ServiceException>>(outputStream),
                serviceException => Task.FromResult<OneOf<Stream, ServiceException>>(serviceException));
        }
        catch (Exception e)
        {
            return new ServiceException(LocalizationKeys.Errors.Txt2Img.ErrorQueryingStableDiffusion,
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
