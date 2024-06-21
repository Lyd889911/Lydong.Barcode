using SkiaSharp;
using ZXing.OneD;
using ZXing;
using ZXing.SkiaSharp;
using ZXing.SkiaSharp.Rendering;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Aztec;
using ZXing.PDF417;
using ZXing.PDF417.Internal;
using ZXing.Datamatrix;
using ZXing.QrCode.Internal;
using System.Runtime.CompilerServices;

namespace Lydong.Barcode
{
    public class BarcodeGenerator
    {
        /// <summary>
        /// 创造一维码或者二维码
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public byte[] Create(BarcodeOptions option)
        {
            var writer = new BarcodeWriter()
            {
                Renderer = new SKBitmapRenderer() { Background = new SKColor(option.BackgroundColor), Foreground = new SKColor(option.ForegroundColor) },
            };


            writer.Format = Enum.Parse<ZXing.BarcodeFormat>(option.BarcodeFormat.ToString());
            writer.Options = GetWriterOptions(option);
            SKBitmap result = writer.Write(option.Text);

            //如果要加logo，条码先透明
            if (option.LogoConfig.IsShow)
                result = ConvertWhiteToTransparent(result, option.BackgroundColor);

            //是否显示数据文本
            if (option.TextConfig.IsShow)
                result = SetText(option.Text, result, option.TextConfig, option.ForegroundColor);

            //时候显示标题
            if (option.TitleConfig.IsShow)
                result = SetText(option.Title, result, option.TitleConfig, option.ForegroundColor);

            //设置上下左右留白
            result = SetMargin(result, option);

            //设置logo
            if (option.LogoConfig.IsShow)
                result = SetLogo(option.Logo, result, option.LogoConfig);

            result = SetBackgroudColor(result, option.BackgroundColor);

            return result.Encode(SKEncodedImageFormat.Png, 100).ToArray();
        }
        /// <summary>
        /// 解析一维码或者二维码
        /// </summary>
        public string Parse(Stream stream,string characterSet="UTF-8")
        {
            byte[] barcode = new byte[stream.Length];
            stream.Read(barcode, 0, barcode.Length);
            stream.Seek(0, SeekOrigin.Begin);
            stream.Close();


            var reader = new BarcodeReader();
            reader.AutoRotate = true;
            reader.Options = new DecodingOptions()
            {
                CharacterSet = characterSet,
                PureBarcode = false,
                ReturnCodabarStartEnd = false
            };

            var bmp = SKBitmap.Decode(barcode);
            var rst = reader.Decode(bmp);
            return rst == null ? "" : rst.Text;
        }


        /// <summary>
        /// 设置条码的边距
        /// </summary>
        private SKBitmap SetMargin(SKBitmap originalBitmap, BarcodeOptions opt)
        {
            // 新图像的尺寸
            int newWidth = originalBitmap.Width + opt.MarginLeft + opt.MarginRight;
            int newHeight = originalBitmap.Height + opt.MarginTop+opt.MarginBottom;

            SKBitmap newBitmap = new SKBitmap(newWidth, newHeight);

            // 使用 SKCanvas 绘制新图像
            using SKCanvas canvas = new SKCanvas(newBitmap);
            canvas.Clear(SKColors.Transparent);
            canvas.DrawBitmap(originalBitmap, new SKPoint(opt.MarginLeft, opt.MarginTop));
            return newBitmap;
        }

        /// <summary>
        /// 设置文本
        /// </summary>
        private SKBitmap SetText(string text,SKBitmap originalBitmap, BarcodeTextConfigs txtConfig,uint colorCode)
        {
            // 设置文本绘制属性
            using SKPaint paint = new SKPaint();
            paint.Color = new SKColor(colorCode);
            paint.IsAntialias = true;
            paint.TextSize = txtConfig.Size;
            paint.TextAlign = Enum.Parse<SKTextAlign>(txtConfig.Align.ToString());

            paint.Typeface = SKTypeface.FromFamilyName(txtConfig.Family, txtConfig.IsBold ? SKFontStyle.Bold: SKFontStyle.Normal);
            //文本
            string txt = txtConfig.Prefix + text + txtConfig.Suffix;
            // 计算文本位置，使其居中显示
            SKRect textRect = new SKRect();
            paint.MeasureText(txt, ref textRect);
            int raiseHeight = (int)textRect.Height + txtConfig.MarginTop + txtConfig.MarginBottom;


            // 新图像的尺寸
            int newWidth = originalBitmap.Width;
            int newHeight = originalBitmap.Height + raiseHeight; 
            SKBitmap newBitmap = new SKBitmap(newWidth, newHeight);

            using SKCanvas canvas = new SKCanvas(newBitmap);
            // 填充白色背景
            canvas.Clear(SKColors.Transparent);

            // 绘制文本
            float x = paint.TextAlign== SKTextAlign.Center?newBitmap.Width / 2: paint.TextAlign == SKTextAlign.Left?0: newBitmap.Width;
            float y = textRect.Height / 2 - textRect.MidY+ txtConfig.MarginTop;
            if (txtConfig.Position == BarcodeTextPosition.Bottom)
                y = originalBitmap.Height + y;
            canvas.DrawText(txt, x, y, paint);

            // 在新图像上绘制条码图像
            canvas.DrawBitmap(originalBitmap, new SKPoint(0, txtConfig.Position == BarcodeTextPosition.Bottom?0: raiseHeight));


            return newBitmap;
        }

        /// <summary>
        /// 设置logo
        /// </summary>
        private SKBitmap SetLogo(Stream logoStream,SKBitmap originalBitmap,BarcodeLogoConfigs logoConfig)
        {
            SKBitmap logo = SKBitmap.Decode(logoStream);
            logo = ResizeSKBitmap(logo, logoConfig.Width, logoConfig.Height, logoConfig.Transparency);

            SKBitmap newBitmap = new SKBitmap(originalBitmap.Width, originalBitmap.Height);
            using SKCanvas canvas = new SKCanvas(newBitmap);
            canvas.Clear(SKColors.Transparent);

            canvas.DrawBitmap(logo, new SKPoint(logoConfig.AbsoluteX, logoConfig.AbsoluteY));
            canvas.DrawBitmap(originalBitmap, new SKPoint(0, 0));

            return newBitmap;
        }

        /// <summary>
        /// 设置背景颜色
        /// </summary>
        private SKBitmap SetBackgroudColor(SKBitmap originalBitmap, uint colorCode)
        {
            SKBitmap newBitmap = new SKBitmap(originalBitmap.Width, originalBitmap.Height);
            using SKCanvas canvas = new SKCanvas(newBitmap);
            canvas.Clear(new SKColor(colorCode));
            canvas.DrawBitmap(originalBitmap, new SKPoint(0,0));
            return newBitmap;
        }
        // 缩小图像
        private SKBitmap ResizeSKBitmap(SKBitmap originalBitmap, int targetWidth, int targetHeight,double transparency)
        {
            // 创建一个新的 SKBitmap 以存储缩小后的图像
            SKBitmap resizedBitmap = new SKBitmap(targetWidth, targetHeight);

            // 使用 SKCanvas 绘制缩小后的图像
            using SKCanvas canvas = new SKCanvas(resizedBitmap);
            canvas.Clear(SKColors.Transparent);

            // 创建一个 SKPaint 对象并设置透明度
            SKPaint paint = new SKPaint
            {
                Color = new SKColor(255, 255, 255, (byte)(transparency*255)) // 设置 alpha 值 (0-255)
            };

            SKRect destRect = new SKRect(0, 0, targetWidth, targetHeight);
            canvas.DrawBitmap(originalBitmap, destRect, paint);
            return resizedBitmap;
        }

        // 将白色转换为透明
        private SKBitmap ConvertWhiteToTransparent(SKBitmap originalBitmap, uint colorCode)
        {
            SKBitmap transparentBitmap = new SKBitmap(originalBitmap.Width, originalBitmap.Height);

            for (int y = 0; y < originalBitmap.Height; y++)
            {
                for (int x = 0; x < originalBitmap.Width; x++)
                {
                    SKColor color = originalBitmap.GetPixel(x, y);
                    if (color == new SKColor(colorCode))
                        transparentBitmap.SetPixel(x, y, SKColors.Transparent);
                    else
                        transparentBitmap.SetPixel(x, y, color);
                }
            }
            return transparentBitmap;
        }

        //得到条码写入的设置
        private EncodingOptions GetWriterOptions(BarcodeOptions option)
        {
            EncodingOptions options = option.BarcodeFormat switch
            {
                BarcodeFormat.QR_CODE => new QrCodeEncodingOptions
                {
                    CharacterSet = option.CharacterSet,
                    DisableECI = option.QRCodeConfig.DisableECI,
                    QrVersion = option.QRCodeConfig.Version,
                    QrCompact = false,
                    ErrorCorrection = option.QRCodeConfig.ECILevel switch
                    {
                        0 => ZXing.QrCode.Internal.ErrorCorrectionLevel.L,
                        1 => ZXing.QrCode.Internal.ErrorCorrectionLevel.M,
                        2 => ZXing.QrCode.Internal.ErrorCorrectionLevel.Q,
                        _ => ZXing.QrCode.Internal.ErrorCorrectionLevel.H
                    }
                },
                BarcodeFormat.PDF_417 => new PDF417EncodingOptions
                {
                    CharacterSet = option.CharacterSet,
                    DisableECI = option.PDF417Config.DisableECI,
                    Dimensions = new ZXing.PDF417.Internal.Dimensions(1, option.PDF417Config.ColumnCount, 1, option.PDF417Config.RowCount),
                    ErrorCorrection = option.PDF417Config.ECILevel >= 0 && option.PDF417Config.ECILevel <= 8 ? Enum.Parse<PDF417ErrorCorrectionLevel>("L" + option.PDF417Config.ECILevel) : PDF417ErrorCorrectionLevel.AUTO
                },
                BarcodeFormat.AZTEC => new AztecEncodingOptions
                {
                    CharacterSet = option.CharacterSet,
                    ErrorCorrection = option.AztecConfig.ECIRatio
                },
                BarcodeFormat.DATA_MATRIX => new DatamatrixEncodingOptions
                {
                    CharacterSet = option.CharacterSet,
                },
                _ => new EncodingOptions()
            } ;

            options.GS1Format = false;
            options.Height = option.Height;
            options.Width = option.Width;
            options.PureBarcode = true;
            options.Margin = 0;
            options.NoPadding = true;
            return options;
        }

    }
    public static class BarcodeExpansion
    {
        /// <summary>
        /// 保存条码图片
        /// </summary>
        public static void ToSaveBarcodeImage(this byte[] data,string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("文件名不能为空");

            if (!fileName.ToLower().EndsWith(".png"))
                throw new ArgumentException("只支持保存.png格式");

            using var stream = new FileStream(fileName, FileMode.OpenOrCreate);
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Close();
        }
    }
}
