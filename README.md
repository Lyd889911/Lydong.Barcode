# 介绍
Lydong.Barcode底层是基于ZXing.NET进行条码生成，解析。图像使用SkiaSharp。
二次封装，更加方便配置生成的条码。

1. 自定义条码图片的整体（包含文字）的前景和背景色。
2. 支持自定义显示数据文本，条码标题（位置，水平居中，文字边距，字体 ，大小等）。
3. 支持自定义边距留白
4. 支持logo图片（绝对定位，logo图层位于条码图层之下，可调节透明度，logo大小，缩放）
5. 多种一维码二维码生成格式。
6. 只支持.png格式的图片生成。
# 使用
```csharp
BarcodeGenerator bg = new BarcodeGenerator();

//所有配置,数据都在opt对象里面，需要设置。详细看源码
BarcodeOptions opt = new()
{
    Text = "1234567890",
    Title = "XXX实验小学",
    MarginTop = 30,
    MarginLeft = 60,
    MarginRight = 60,
    MarginBottom = 30,
    Height = 500,
    Width = 500,
    BarcodeFormat = BarcodeFormat.QR_CODE
};
opt.TextConfig.IsShow = true;
opt.TextConfig.Align = BarcodeTextAlign.Right;
opt.TextConfig.MarginBottom = 20;
opt.TextConfig.MarginTop = 20;

opt.TitleConfig.IsShow = true;
opt.TitleConfig.Align = BarcodeTextAlign.Left;
opt.TitleConfig.MarginBottom = 20;
opt.TitleConfig.MarginTop = 20;

opt.Logo = File.OpenRead("C:/2.png");
opt.LogoConfig.IsShow = true;
opt.LogoConfig.Width = 800;
opt.LogoConfig.Height = 300;
opt.LogoConfig.AbsoluteY = 0;
opt.LogoConfig.Transparency = 0.4;

//生成一维码或者二维码，保存图片
bg.Create(opt).ToSaveBarcodeImage("Barcode/ceshi.png");

//解析一维码或者二维码，返回解析文本
string r = bg.Parse(new FileStream(path, FileMode.Open));
```
# 
