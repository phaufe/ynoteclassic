

using System.Collections.Generic;
using System.Text;
using FastColoredTextBoxNS;
using System.Drawing;
using System.Text.RegularExpressions;
using System;

namespace HighlightingPluginSample
{
    public static class CLSyntaxHighlighter
    {
        public const string TypePattern = @"void|bool|char|uchar|short|ushort|int|uint|long|ulong|float|double|half|size_t|char2|uchar2|short2|ushort2|int2|uint2|long2|ulong2|float2|double2|char4|uchar4|short4|ushort4|int4|uint4|long4|ulong4|float4|double4|char8|uchar8|short8|ushort8|int8|uint8|long8|ulong8|float8|double8|char16|uchar16|short16|ushort16|int16|uint16|long16|ulong16|float16|double16";
        public const string FlowPattern = @"for|while|if|else|return";
        public const string ExtensionsPattern = @"cl_khr_fp64|cl_khr_global_int32_base_atomics|cl_khr_global_int32_extended_atomics|cl_khr_local_int32_base_atomics|cl_khr_local_int32_extended_atomics|cl_khr_int64_base_atomics|cl_khr_int64_extended_atomics|cl_khr_3d_image_writes|cl_khr_byte_addressable_store|cl_khr_fp16";
        public const string QualifiersPattern = @"kernel|read_only|write_only|global|local|constant|private|__kernel|__read_only|__write_only|__global|__local|__constant|__private|__attribute__|reqd_work_group_size|work_group_size_hint|vec_type_hint|const";
        public const string WorkItemFuncPattern = @"get_work_dim|get_global_size|get_global_id|get_local_size|get_local_id|get_num_groups|get_group_id";
        public const string ConstantsPattern = @"MAXFLOAT|HUGE_VALF|INFINITY|NAN|M_E|M_LOG2E|M_LOG10E|M_LN2|M_LN10|M_PI|M_PI_2|M_PI_4|M_1_PI|M_2_PI|M_2_SQRTPI|M_SQRT2|M_SQRT1_2";
        public const string MathFuncPattern = @"acos|acosh|acospi|asin|asinh|asinpi|atan|atan2|atanh|atanpi|atan2pi|cbrt|ceil|copysign|cos|cosh|cospi|erfc|erf|axp|axp2|axp10|expm1|fabs|fdim|floor|fma|fmax|fmin|fmod|fract|frexp|hypot|ilogb|ldexp|lgamma|lgamma_r|log|log2|log10|log1p|logb|mad|modf|nan|nextafter|pow|pown|powr|remainder|remquo|rint|rootn|round|rsqrt|sin|sincos|sinh|sinpi|sqrt|tan|tanh|tanpi|tgamma|trunc";
        public const string NativeMathFuncPattern = @"native_cos|native_divide|native_exp|native_exp2|native_exp10|native_log|native_log2|native_log10|native_powr|native_recip|native_rsqrt|native_sin|native_sqrt|native_tan";
        public const string CommonFuncPattern = @"clamp|degress|max|min|mix|radians|step|smoothstep|sign";
        public const string GeomFuncPattern = @"cross|dot|distance|length|normalize";
        public const string FastGeomFuncPattern = @"fast_distance|fast_length|fast_normalize";
        public const string ImageFuncPattern = @"sampler_t|image2d_t|read_imagef|read_imagei|read_imageui|write_imagef|write_imagei|write_imageui|CLK_FILTER_NEAREST|CLK_FILTER_LINEAR|CLK_NORMALIZED_COORDS_FALSE|CLK_NORMALIZED_COORDS_TRUE|CLK_ADDRESS_CLAMP_TO_EDGE|CLK_ADDRESS_CLAMP|CLK_ADDRESS_NONE";
        public const string SynchFuncPattern = @"barrier|mem_fence|CLK_LOCAL_MEM_FENCE|CLK_GLOBAL_MEM_FENCE|read_mem_fence|write_mem_fence";
        public const string OtherPattern = @"#pragma|#define";

        static readonly Style KhakiStyle = new TextStyle(Brushes.Khaki, null, FontStyle.Regular);
        static readonly Style BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        static readonly Style NavyStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        static readonly Style BlueStyle = new TextStyle(Brushes.DodgerBlue, null, FontStyle.Regular);
        static readonly Style PurpleStyle = new TextStyle(Brushes.Purple, null, FontStyle.Regular);
        static readonly Style PurpleBoldStyle = new TextStyle(Brushes.Purple, null, FontStyle.Bold);
        static readonly Style GrayStyle = new TextStyle(Brushes.DarkGray, null, FontStyle.Regular);
        static readonly Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        static readonly Style GoldStyle = new TextStyle(Brushes.Gold, null, FontStyle.Regular);
        static readonly Style RedStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        static readonly Style BoldStyle = new TextStyle(Brushes.Black, null, FontStyle.Bold | FontStyle.Underline);


        static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();

        static CLSyntaxHighlighter()
        {
            Init();
        }

        static Regex TypeRegex, CommentRegex1, CommentRegex2, CommentRegex3, NumberRegex, FuncNameRegex, FlowRegex, WorkItemFuncRegex, ConstantsRegex, ExtensionsRegex, QualifiersRegex, FuncRegex, ImageRegex, SynchFuncRegex, OtherRegex, FuncSignatureRegex, FuncSignatureParserRegex;

        static void Init()
        {
            TypeRegex = new Regex(string.Format(@"\b({0})\b", TypePattern), RegexCompiledOption);
            CommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline | RegexCompiledOption);
            CommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            CommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
            NumberRegex = new Regex(@"\b\d+[\.]?\d*([e]\-?\d+)?[hulf]?\b|\b0x[a-f\d]+\b", RegexCompiledOption | RegexOptions.IgnoreCase);
            FuncNameRegex = new Regex(string.Format(@"\b({0})\s+(?<range>[\w\d_]+)\s*\(", TypePattern), RegexCompiledOption);
            FuncSignatureRegex = new Regex(string.Format(@"\b({0})\s+[\w\d_]+\s*\([^\)]*?\)", TypePattern), RegexCompiledOption);
            FuncSignatureParserRegex = new Regex(string.Format(@"^(?<type>\S+)\s+(?<name>[\w\d_]+)\s*\((?<args>[^\)]*?)\)$"), RegexCompiledOption);
            FlowRegex = new Regex(string.Format(@"\b({0})\b", FlowPattern), RegexCompiledOption);
            WorkItemFuncRegex = new Regex(string.Format(@"\b({0})\b", WorkItemFuncPattern), RegexCompiledOption);
            ConstantsRegex = new Regex(string.Format(@"\b({0})\b", ConstantsPattern), RegexCompiledOption);
            ExtensionsRegex = new Regex(string.Format(@"\b({0})\b", ExtensionsPattern), RegexCompiledOption);
            QualifiersRegex = new Regex(string.Format(@"\b({0})\b", QualifiersPattern), RegexCompiledOption);
            FuncRegex = new Regex(string.Format(@"\b({0}|{1}|{2}|{3}|{4})\b", MathFuncPattern, NativeMathFuncPattern, CommonFuncPattern, GeomFuncPattern, FastGeomFuncPattern), RegexCompiledOption);
            ImageRegex = new Regex(string.Format(@"\b({0})\b", ImageFuncPattern), RegexCompiledOption);
            SynchFuncRegex = new Regex(string.Format(@"\b({0})\b", SynchFuncPattern), RegexCompiledOption);
            OtherRegex = new Regex(string.Format(@"({0})\b", OtherPattern), RegexCompiledOption);
        }

        static public List<string> GetAllKeywords()
        {
            List<string> result = new List<string>();
            result.AddRange(TypePattern.Split('|'));
            result.AddRange(FlowPattern.Split('|'));
            result.AddRange(ExtensionsPattern.Split('|'));
            result.AddRange(QualifiersPattern.Split('|'));
            result.AddRange(WorkItemFuncPattern.Split('|'));
            result.AddRange(ConstantsPattern.Split('|'));
            result.AddRange(MathFuncPattern.Split('|'));
            result.AddRange(NativeMathFuncPattern.Split('|'));
            result.AddRange(CommonFuncPattern.Split('|'));
            result.AddRange(GeomFuncPattern.Split('|'));
            result.AddRange(FastGeomFuncPattern.Split('|'));
            result.AddRange(ImageFuncPattern.Split('|'));
            result.AddRange(SynchFuncPattern.Split('|'));
            result.AddRange(OtherPattern.Split('|'));

            return result;
        }

        static public void Highlight(Range range, List<ExplorerItem> customFunctions)
        {
            //build regex for custom functions
            StringBuilder sb = new StringBuilder();
            foreach (var func in customFunctions)
                sb.Append(func.funcName + "|");
            Regex CustomFuncRegex = new Regex(@"\b(" + sb.ToString().TrimEnd('|') + @")\b");
            //
            range.tb.LeftBracket = '(';
            range.tb.RightBracket = ')';
            //clear
            range.ClearStyle(GreenStyle, RedStyle, BoldStyle, KhakiStyle, BrownStyle, NavyStyle, PurpleStyle, GrayStyle, BlueStyle, GoldStyle, RedStyle);
            //comment
            range.SetStyle(GreenStyle, CommentRegex1);
            range.SetStyle(GreenStyle, CommentRegex2);
            range.SetStyle(GreenStyle, CommentRegex3);
            //number
            range.SetStyle(RedStyle, NumberRegex);
            //func name
            range.SetStyle(BoldStyle, FuncNameRegex);
            //keywords
            range.SetStyle(KhakiStyle, ExtensionsRegex);
            range.SetStyle(BrownStyle, QualifiersRegex);
            range.SetStyle(NavyStyle, TypeRegex);
            range.SetStyle(NavyStyle, FlowRegex);
            range.SetStyle(PurpleStyle, WorkItemFuncRegex);
            range.SetStyle(RedStyle, ConstantsRegex);
            range.SetStyle(PurpleStyle, FuncRegex);
            range.SetStyle(PurpleStyle, OtherRegex);
            range.SetStyle(GoldStyle, ImageRegex);
            range.SetStyle(RedStyle, SynchFuncRegex);
            range.SetStyle(PurpleBoldStyle, CustomFuncRegex);

            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            range.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        static RegexOptions RegexCompiledOption
        {
            get
            {
                if (platformType == Platform.X86)
                    return RegexOptions.Compiled;
                else
                    return RegexOptions.None;
            }
        }

        public static List<ExplorerItem> BuildExplorerItems(Range range)
        {
            List<ExplorerItem> list = new List<ExplorerItem>();

            foreach (var r in range.GetRanges(FuncSignatureRegex))
            {
                var m = FuncSignatureParserRegex.Match(r.Text);
                if (!m.Success)
                    continue;
                var item = new ExplorerItem() { type = ExplorerItemType.Method, line = r.Start.iLine, funcName = m.Groups["name"].Value, funcType = CLType.Parse(m.Groups["type"].Value) };
                item.args = ParseFuncArgs(m.Groups["args"].Value);
                list.Add(item);
            }

            list.Sort(new ExplorerItemComparer());

            return list;
        }

        private static List<FunctionArgument> ParseFuncArgs(string text)
        {
            List<FunctionArgument> res = new List<FunctionArgument>();
            var parts = text.Trim().Split(',');
            foreach (var part in parts)
            {
                var parts2 = part.Split(new char[] { ' ', '*' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts2.Length < 2)
                    continue;
                bool isPointer = part.Contains("*");
                var arg = new FunctionArgument() { name = parts2[parts2.Length - 1], type = CLType.Parse(parts2[parts2.Length - 2] + (isPointer ? "*" : "")) };
                res.Add(arg);
            }

            return res;
        }
    }

    public enum ExplorerItemType
    {
        Class, Method, Property, Event
    }

    public class ExplorerItem
    {
        public ExplorerItemType type;
        public string funcName;
        public CLType funcType;
        public List<FunctionArgument> args = new List<FunctionArgument>();
        public int line;
    }

    public class FunctionArgument
    {
        public CLType type;
        public string name;
    }

    public class CLType
    {
        public string BaseType { get; private set; }
        public int Dim { get; private set; }
        public bool IsPointer { get; private set; }

        public static CLType Parse(string s)
        {
            var m = Regex.Match(s.Trim(), @"^(?<type>[a-z]+)(?<dim>\d*)(?<isPointer>\*?)$");
            if (!m.Success)
                return null;
            var result = new CLType();
            result.IsPointer = m.Groups["isPointer"].Value != "";
            result.Dim = 1;
            if (m.Groups["dim"].Value != "")
                result.Dim = int.Parse(m.Groups["dim"].Value);
            result.BaseType = m.Groups["type"].Value;

            return result;
        }

        public override string ToString()
        {
            return BaseType + (Dim > 1 ? Dim.ToString() : "") + (IsPointer ? "*" : "");
        }
    }

    class ExplorerItemComparer : IComparer<ExplorerItem>
    {
        public int Compare(ExplorerItem x, ExplorerItem y)
        {
            return x.funcName.CompareTo(y.funcName);
        }
    }
}
