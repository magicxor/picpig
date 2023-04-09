namespace PicPig.Models;

public record Txt2ImgResult(
    Stream ImageStream,
    Txt2ImgQuery Query
);
