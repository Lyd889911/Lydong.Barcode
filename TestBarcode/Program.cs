// See https://aka.ms/new-console-template for more information
using Lydong.Barcode;
using System.IO;

Console.WriteLine("Hello, World!");
BarcodeGenerator bg = new BarcodeGenerator();
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

bg.Create(opt).ToSavePng("Barcode/ceshi.png");

//string p = @"Barcode/36c489f33e7b0c07fa20665f4b55e99.jpg";
//string r = bg.Parse(new FileStream(p, FileMode.Open));
//Console.WriteLine($"结果是：{r}");

Console.WriteLine("完成");