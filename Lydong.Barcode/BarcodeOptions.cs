using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace Lydong.Barcode
{
    public class BarcodeOptions
    {
        /// <summary>
        /// 数据文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 数据文本配置
        /// </summary>
        public BarcodeTextConfigs TextConfig { get; set; } = new() { Size = 36, Family = "Arial", IsBold = true, Position = BarcodeTextPosition.Bottom };
        /// <summary>
        /// 条码标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 标题配置
        /// </summary>
        public BarcodeTextConfigs TitleConfig { get; set; } = new() { Size = 50, Family = "宋体", IsBold = true, Position = BarcodeTextPosition.Top };
        /// <summary>
        /// logo文件
        /// </summary>
        public Stream Logo { get; set; }
        /// <summary>
        /// logo的配置
        /// </summary>
        public BarcodeLogoConfigs LogoConfig { get; set; } = new() { Width = 500, Height = 140, Transparency = 0.4 };


        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; } = 700;
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; } = 250;
        /// <summary>
        /// 上边距
        /// </summary>
        public int MarginTop { get; set; }
        /// <summary>
        /// 下边距
        /// </summary>
        public int MarginBottom { get; set; }
        /// <summary>
        /// 左边距
        /// </summary>
        public int MarginLeft { get; set; }
        /// <summary>
        /// 右边距
        /// </summary>
        public int MarginRight { get; set; }
        /// <summary>
        /// 背景颜色
        /// </summary>
        public uint BackgroundColor { get; set; } = 0xFFFFFFFF;
        /// <summary>
        /// 前景颜色
        /// </summary>
        public uint ForegroundColor { get; set; } = 0xFF000000;

        /// <summary>
        /// 条码格式，常用一维码CODE_128，二维码QR_CODE
        /// </summary>
        public BarcodeFormat BarcodeFormat { get; set; } = BarcodeFormat.CODE_128;
        /// <summary>
        /// 二维码用到的字符集
        /// </summary>
        public string CharacterSet { get; set; } = "UTF-8";
        /// <summary>
        /// 二维码QR_CODE类型的配置
        /// </summary>
        public Barcode2DQRCodeConfigs QRCodeConfig { get; set; } = new();
        /// <summary>
        /// 二维码Aztec类型的配置
        /// </summary>
        public Barcode2DAztecConfigs AztecConfig { get; set; } = new();
        /// <summary>
        /// 二维码PDF417类型的配置
        /// </summary>
        public Barcode2DPDF417Configs PDF417Config { get; set; } = new();
    }


    /// <summary>
    /// 条码文本配置
    /// </summary>
    public class BarcodeTextConfigs
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public int Size { get; set; } = 32;
        /// <summary>
        /// 字体
        /// </summary>
        public string Family { get; set; } = "黑体";
        /// <summary>
        /// 水平显示左中右
        /// </summary>
        public BarcodeTextAlign Align { get; set; }= BarcodeTextAlign.Center;
        /// <summary>
        /// 是否粗体
        /// </summary>
        public bool IsBold { get; set; }
        /// <summary>
        /// 顶端边距
        /// </summary>
        public int MarginTop { get; set; }
        /// <summary>
        /// 底端边距
        /// </summary>
        public int MarginBottom { get; set; }
        /// <summary>
        /// 文本位置，上下
        /// </summary>
        public BarcodeTextPosition Position { get; set; }
    }
    /// <summary>
    /// logo的配置
    /// </summary>
    public class BarcodeLogoConfigs
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 绝对定位Y点
        /// </summary>
        public int AbsoluteY {  get; set; }
        /// <summary>
        /// 绝对定位X点
        /// </summary>
        public int AbsoluteX {  get; set; }
        /// <summary>
        /// 透明度0-1
        /// </summary>
        public double Transparency { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
    }

    /// <summary>
    /// 二维码的QRCode的配置
    /// </summary>
    public class Barcode2DQRCodeConfigs
    {
        /// <summary>
        /// 是否禁用ECI
        /// </summary>
        public bool DisableECI { get; set; } = true;
        /// <summary>
        /// 纠错等级(0-3)
        /// </summary>
        public int ECILevel { get; set; } = 3;
        /// <summary>
        /// 版本（1-40）
        /// </summary>
        public int Version { get; set; } = 10;
    }
    /// <summary>
    /// 二维码的Aztec的配置
    /// </summary>
    public class Barcode2DAztecConfigs
    {
        /// <summary>
        /// 纠错率（不应小于25%，默认30%）
        /// </summary>
        public int ECIRatio { get; set; } = 30;
    }
    /// <summary>
    /// 二维码的PDF417的配置
    /// </summary>
    public class Barcode2DPDF417Configs
    {
        /// <summary>
        /// 是否禁用ECI
        /// </summary>
        public bool DisableECI { get; set; } = true;
        /// <summary>
        /// 纠错等级(0-8)
        /// </summary>
        public int ECILevel { get; set; } = 9;
        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount { get; set; } = 100;
        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { get; set; } = 100;
    }

    public enum BarcodeTextAlign
    {
        Center,
        Right,
        Left,
    }
    public enum BarcodeTextPosition
    {
        Top,
        Bottom
    }
    public enum BarcodeFormat
    {
        /*
         * 1.Code128码是一种高密度的一维条码，可表示从 ASCII 0 到ASCII 127 共128个字符（其中包含数字，字母，符号），所以称128码。
         * 2.Code128 A码，Code128 B码，Code128 C码都是Code128码的子集。
         * 2.1.Code128 A码可表示：大写英文字母、数字、控制字符组成的字符串，比如：ABC、ABC123。
         * 2.2.Code128 B码可表示：大小写英文字母、数字、字符组成的字符串，比如：Abc123、A-123(B)。
         * 2.3.Code128 C码可表示：仅可表示100个“两位”数字编码（00-99），比如：123456、00225869。
         */
        CODE_128,
        /*
         * 1.Code39码由5条线和分开它们的4条缝隙共9个元素构成。线和缝隙有宽窄之分，而且无论线还是缝隙仅有3个比其他的元素要宽一定比例。39码因此得名。
         * 2.编码规则
         * 2.1.每五条线表示一个字符。
         * 2.2.粗线表示1，细线表示0。
         * 2.3.线条间的间隙宽的表示1，窄的表示0。
         * 2.4.五条线加上它们之间的四条间隙就是九位二进制编码，而且这九位中必定有三位是1，所以称为39码。
         * 2.5.条形码的首尾各一个 * 标识开始和结束。
         * 3.有效字符43个
         * 3.1.26个大写字母（A - Z）
         * 3.2.十个数字（0 - 9）
         * 3.3.连接号(-),句号（.）,空格,美圆符号($),斜扛(/),加号(+)以及百分号(%)
         */
        CODE_39,
        /*
         * 1.Code93码是在Code39码的基础上诞生的，改善了Code39码缺点，安全性比Code39码更高，采用双校验符，也就是说条码里有两个检查码，
         *   降低条码扫描器读取条码时出现的错误率，Code93码列印长度（占9位）比Code39码（占12位）短，相同的字符集下，比Code39码更窄。
         * 2.有效字符43个
         * 2.1.26个大写字母（A - Z）
         * 2.2.十个数字（0 - 9）
         * 2.3.连接号(-),句号（.）,空格,美圆符号($),斜扛(/),加号(+)以及百分号(%)
         */
        CODE_93,
        /*
         * 1.Codabar是由Monarch Marking Systems在1972年研制的条码。它是在“2 of 5”后早期阶段引入额条码。广泛用于序列号的领域，如血库、门到门交货服务以及会员卡片管理。
         * 2.基本构成
         * 2.1.7个条和空代表一个字符
         * 2.2.在条码的开始和结束（起始/终止符）都有A、B、C、D或（a、b、c、d）中的任何一个。
         * 3.有效字符19个
         * 3.1.字母（A - D）
         * 3.2.十个数字（0 - 9）
         * 3.3.连接号(-),美圆符号($),斜扛(/),句号（.）,加号(+)
         */
        CODABAR,
        /*
         * 1.ITF条码，又称交叉二五条码，采用5个条（空）来代表一个字符。由于5个中的2个是宽的，因此被叫做"2 of 5",多用于物流管理领域。
         * 2.有效字符10个
         * 2.1.十个数字（0 - 9）
         */
        ITF,
        /*
         * 1.EAN码（European Article Number）是国际物品编码协会制定的一种商品用条码，通用于全世界。EAN码符号有标准版（EAN-13）和缩短版（EAN-8）两种。缩短版表示8位数字，又称EAN8。
         * 2.有效字符10个
         * 2.1.十个数字（0 - 9）
         * 3.基本结构
         * 3.1.EAN码由前缀码、厂商识别码、商品项目代码和校验码组成。
         * 3.2.前3位数字叫“前缀码”，是用于标识EAN成员的代码，由EAN统一管理和分配，不同的国家或地区有不同的前缀码，我国为690-699。
         * 3.3.厂商代码是EAN编码组织在EAN分配的前缀码的基础上分配给厂商的代码。
         * 3.4.商品项目代码由厂商自行编码。
         * 3.5.校验码为了校验代码的正确性。
         */
        EAN_8,//作者电脑无法使用
        /*
         * 1.EAN码（European Article Number）是国际物品编码协会制定的一种商品用条码，通用于全世界。EAN码符号有标准版（EAN-13）和缩短版（EAN-8）两种。标准版表示13位数字，又称为EAN13码。
         * 2.有效字符10个
         * 2.1.十个数字（0 - 9）
         * 3.基本结构
         * 3.1.EAN码由前缀码、厂商识别码、商品项目代码和校验码组成。
         * 3.2.前3位数字叫“前缀码”，是用于标识EAN成员的代码，由EAN统一管理和分配，不同的国家或地区有不同的前缀码，我国为690-699。
         * 3.3.厂商代码是EAN编码组织在EAN分配的前缀码的基础上分配给厂商的代码。
         * 3.4.商品项目代码由厂商自行编码。
         * 3.5.校验码为了校验代码的正确性。
         */
        EAN_13,//作者电脑无法使用
        /*
         * 1.通用产品代码（Universal Product Code），通常简称UPC码，是美国均匀码理事会（Uniform Code Council, UCC）制定的一种商品条码，主要在美国及加拿大使用。在其基础之上发展起来的EAN码则已发展成为适用范围最广的通用条码。
         * 2.有效字符10个
         * 2.1.十个数字（0 - 9）
         * 3.基本结构
         * 3.1.每7个模组表达一个字符，每个模组有空（白色）与条（黑色）两种状态。 UPC码又分为UPC-A、B、C、D、E五种版本。UPC-B\C\D码与UPC-A码基本相同。
         *      B码用于医药卫生；
         *      C码用于产业部门；
         *      D码用于仓库批发；
         *      E码表示商品短码；
         * 3.2.UPC-A码是定长码，只能表示12位数字。
         * 
         */
        UPC_A,//作者电脑无法使用
        /*
         * 1.通用产品代码（Universal Product Code），通常简称UPC码，是美国均匀码理事会（Uniform Code Council, UCC）制定的一种商品条码，主要在美国及加拿大使用。在其基础之上发展起来的EAN码则已发展成为适用范围最广的通用条码。
         * 2.有效字符10个
         * 2.1.十个数字（0 - 9）
         * 3.基本结构
         * 3.1.每7个模组表达一个字符，每个模组有空（白色）与条（黑色）两种状态。 UPC码又分为UPC-A、B、C、D、E五种版本。UPC-B\C\D码与UPC-A码基本相同。
         *      B码用于医药卫生；
         *      C码用于产业部门；
         *      D码用于仓库批发；
         *      E码表示商品短码；
         * 3.2.UPC-E码是定长码，只能表示8位数字。
         * 
         */
        UPC_E,//作者电脑无法使用
        /*
         * 1.rss码的诞生是由于市场对条码有了更高的要求，使得ucc和ean两个标准化组织意识到现有的条码无法满足某些特殊任务需要。
         *      通过对现有条码的进一步研究发现，必须发展一种新型的条码以满足以上的需求。于1999年10月颁布了rss码aim国际标准。
         * 2.基本类型
         * 2.1.rss码是线性条码，它有三种基本类型：rss-14码、rss缩微码和rss扩展码。
         * 2.2.rss-14码是一维条码，由14位ucc/ean数字组成，可包含产品信息、追踪数据、日期、数量、地点等信息。
         * 2.3.rss缩微码也是14位一维条码，与rss－14码包含同样的信息。rss缩微码比rss－14码小，其宽70x/高10x，用于非零售业小商品，如医用及电子产品。
         * 2.4.rss扩展码
         * 
         */
        RSS_14,//作者电脑无法使用
        /*
         * 1.rss码的诞生是由于市场对条码有了更高的要求，使得ucc和ean两个标准化组织意识到现有的条码无法满足某些特殊任务需要。
         *      通过对现有条码的进一步研究发现，必须发展一种新型的条码以满足以上的需求。于1999年10月颁布了rss码aim国际标准。
         * 2.基本类型
         * 2.1.rss码是线性条码，它有三种基本类型：rss-14码、rss缩微码和rss扩展码。
         * 2.2.rss-14码是一维条码，由14位ucc/ean数字组成，可包含产品信息、追踪数据、日期、数量、地点等信息。
         * 2.3.rss缩微码也是14位一维条码，与rss－14码包含同样的信息。rss缩微码比rss－14码小，其宽70x/高10x，用于非零售业小商品，如医用及电子产品。
         * 2.4.rss扩展码
         * 
         */
        RSS_EXPANDED,//作者电脑无法使用
        /*
         * 1.MSI Plessey条形码是由英国plesser Company所设计，主要使用在图书馆和零售应用中，MSI Plessey是一款数字条码，多用于超市、存储用的仓库和其他贮藏室的货架，
         *      货架上的条码可以告知货架上的产品、应放数量和其他相关信息。条码可以为任意长度，但是通常固定为用于特定应用的长度。
         * 2.仅支持数组
         * 
         */
        MSI,
        IMB,//作者电脑无法使用
        PLESSEY,
        /*
         * 1.Pharmacode是由laetus开发，适用于制药学领域的药品包装，小标签，对于控制色彩符合打印的随意彩色条码服务，Pharmacode可被用于无可读性文本；
         *      Pharmacode条码仅使用条形承载数据，而不使用空白。打印容限较大，并且可以选择以多种颜色打印条码，这使得Pharmacode格式非常使用。
         *      Pharmacode格式的代码必须为数字。 Pharmacode条码有两种版本，Pharmacode one Track和Pharmac Two Track。
         * 
         */
        PHARMA_CODE,//作者电脑无法使用
        /*
         * 1.PDF417条码是一种高密度、高信息含量的便携式数据文件，是实现证件及卡片等大容量、高可靠性信息自动存储、携带并可用机器自动识读的理想手段。
         * 2.条码特征
         * 2.1.信息容量大：根据不同的条空比例每平方英寸可以容纳250到1100个字符。在国际标准的证卡有效面积上(相当于信用卡面积的2/3，约为76mm*25mm), 
         *      PDF417条码可以容纳1848个字母字符或2729个数字字符，约500个汉字信息。这种二维条码比普通条码信息容量高几十倍。
         * 2.2.编码范围广：PDF417条码可以将照片、指纹、掌纹、签字、声音、文字等凡可数字化的信息进行编码。
         * 2.3.保密、防伪性能好：具有多重防伪特性，它可以采用密码防伪、软件加密及利用所包含的信息如指纹、照片等进行防伪，因此具有极强的保密防伪性能。
         * 2.4.译码可靠性高：普通条码的译码错误率约为百万分之二左右，而PDF417条码的误码率不超过千万分之一，译码可靠性极高。
         * 2.5.修正错误能力强：采用了世界上最先进的数学纠错理论，如果破损面积不超过50%，条码由于沾污、破损等所丢失的信息，可以照常破译出丢失的信息。
         * 2.6.容易制作且成本很低：利用现有的点阵、激光、喷墨、热敏/热转印、制卡机等打印技术，即可在纸张、卡片、PVC、甚至金属表面上印出PDF417二维条码。
         *      由此所增加的费用仅是油墨的成本，因此人们又称PDF417是“零成本”技术。
         * 2.7.条码符号的形状可变：同样的信息量，PDF417条码的形状可以根据载体面积及美工设计等进行自我调整。
         * 
         */
        PDF_417,
        /*
         * 1.Aztec码是1995年，由Hand HeldProducts公司的Dr.Andrew Longacre设计。它是二维矩阵代码，它的命名主要是因为条码中央的定位器有点像从高空鸟瞰Aztec金字塔，因此得名。
         *      通常用于机票和其他旅行文档以及汽车登记文档，还可以用于医院的患者识别、药物识别、样本以及特定患者相关的其他物品。
         * 2.它不支持东亚字符。
         * 
         */
        AZTEC,
        /*
         * 1.datamatrix是一种矩阵式二维条码，于1989年由美国国际资料公司发明，广泛用于商品的防伪、统筹标识。其发展的构想是希望在较小的条码标签上存入更多的资料量。
         *      Datamatrix的最小尺寸是所有条码中最小的，尤其特别适用于小零件的标识，以及直接印刷在实体上。
         * 2.特征
         * 2.1.可编码字元集包括全部的ASCII字元及扩充ASCII字元，共256个字元。
         * 2.2.条码大小(不包括空白区)：10×10 ~ 144×144
         * 2.3.资料容量：235个文数字资料，1556个8位元资料，3116个数字资料。
         * 2.4.错误纠正：透过Reed-Solomon演算法产生多项式计算获得错误纠正码。不同尺寸宜采用不同数量的错误纠正码。
         * 3.尺寸
         * 3.1.因其是矩阵式二维码，又分为ECC000-140与ECC200两种类型，所以组成被分为30组，其中正方形分为24组，长方形分为6组
         * 3.2.正方形
         *      10×10   12×12   14×14   16×16   18×18   20×20
         *      22×22   24×24   26×26   32×32   36×36   40×40
         *      44×44   48×48   52×52   64×64   72×72   80×80
         *      88×88   96×96   104×104   120×120   132×132   144×144
         * 3.3.长方形
         *      8×18   8×32   12×26   12×36   16×36   16×48
         * 4.它不支持东亚字符。
         */
        DATA_MATRIX,
        /*
         * 1.QR Code码，是由日本Denso公司于1994年9月研制的一种矩阵二维码符号，它具有一维条码及其它二维条码所具有的信息容量大、可靠性高、可表示汉字及图象多种文字信息、保密防伪性强等优点。
         * 2.特点
         * 2.1.符号规格从版本1（21×21模块）到版本40（177×177 模块），每提高一个版本，每边增加4个模块。
         * 2.2.数据类型与容量（参照最大规格符号版本40-L级）：
         *          数字数据：7,089个字符
         *          字母数据: 4,296个字符
         *          8位字节数据: 2,953个字符
         *          汉字数据：1,817个字符
         * 2.3.数据表示方法：深色模块表示二进制"1"，浅色模块表示二进制"0"。
         * 2.4.纠错能力：
         *      L级：约可纠错7%的数据码字
         *      M级：约可纠错15%的数据码字
         *      Q级：约可纠错25%的数据码字
         *      H级：约可纠错30%的数据码字
         * 
         */
        QR_CODE,
        /*
         * 1.1980年代晚期，美国知名的UPS（United Parcel Service）快递公司认知到利用机器辨读资讯可有效改善作业效率、提高服务品质而研发的条码。
         * 2.基本特征
         * 2.1.外形近乎正方形，由位于符号中央的同心圆(或称公牛眼)定位图形 (Finder Pattern)，及其周围六边形蜂巢式结构的资料位元所组成，这种排列方式使得Maxicode可从任意方向快速扫瞄。
         * 2.2.符号大小固定。为了方便定位，使解码更容易，以加快扫描速度，Maxicode的图形大小与资料容量大小都是固定的，图形固定约1平方英寸，资料容量最多93个字元。
         * 2.3.定位图形：Maxicode具有一个大小固定且唯一的中央定位图形，为叁个黑色的同心圆，用于扫瞄定位。此定位图形位在资料模组所围成的虚拟六边形的正中央，在此虚拟六边形的六个顶点上各有3个黑白色不同组合式所构成的模组，称为「方位丛」(Orientation Cluster)，其提供扫瞄器重要的方位资讯。
         * 3.组成结构
         *      Maxicode允许对256个国际字符编码，包括值0~127的ASCII字元和128~255的扩展ASCII字元。
         * 
         */
        MAXICODE//作者电脑无法使用
    }
}
