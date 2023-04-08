namespace PicPig.Enums;

public static class LocalizationKeys
{
    public static class Errors
    {
        public static class Configuration
        {
            public const string StableDiffusionApiAddressMissing = "Errors.Configuration.StableDiffusionApiAddressMissing";
            public const string TelegramBotApiKeyMissing = "Errors.Configuration.TelegramBotApiKeyMissing";
        }

        public static class Telegram
        {
            public const string ErrorSendingMessage = "Errors.Telegram.ErrorSendingMessage";
            public const string ErrorObtainingPhotoId = "Errors.Telegram.ErrorObtainingPhotoId";
            public const string LoadingProgressPictureIdIsNull = "Errors.Telegram.LoadingProgressPictureIdIsNull";
        }

        public static class Txt2Img
        {
            public const string PresetFactoryNotFound = "Errors.Txt2Img.PresetFactoryNotFound";
            public const string ErrorQueryingStableDiffusion = "Errors.Txt2Img.ErrorQueryingStableDiffusion";
            public const string NoImagesReturned = "Errors.Txt2Img.NoImagesReturned";
        }
    }
}
