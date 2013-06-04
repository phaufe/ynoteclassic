from System.Collections.Generic import *
from System.Text import *
from FastColoredTextBoxNS import *
from System.Drawing import *
from System.Text.RegularExpressions import *
from System import *

class CLSyntaxHighlighter(object):
	def __init__():
		self._TypePattern = @"void|bool|char|uchar|short|ushort|int|uint|long|ulong|float|double|half|size_t|char2|uchar2|short2|ushort2|int2|uint2|long2|ulong2|float2|double2|char4|uchar4|short4|ushort4|int4|uint4|long4|ulong4|float4|double4|char8|uchar8|short8|ushort8|int8|uint8|long8|ulong8|float8|double8|char16|uchar16|short16|ushort16|int16|uint16|long16|ulong16|float16|double16"
		self._FlowPattern = @"for|while|if|else|return"
		self._ExtensionsPattern = @"cl_khr_fp64|cl_khr_global_int32_base_atomics|cl_khr_global_int32_extended_atomics|cl_khr_local_int32_base_atomics|cl_khr_local_int32_extended_atomics|cl_khr_int64_base_atomics|cl_khr_int64_extended_atomics|cl_khr_3d_image_writes|cl_khr_byte_addressable_store|cl_khr_fp16"
		self._QualifiersPattern = @"kernel|read_only|write_only|global|local|constant|private|__kernel|__read_only|__write_only|__global|__local|__constant|__private|__attribute__|reqd_work_group_size|work_group_size_hint|vec_type_hint|const"
		self._WorkItemFuncPattern = @"get_work_dim|get_global_size|get_global_id|get_local_size|get_local_id|get_num_groups|get_group_id"
		self._ConstantsPattern = @"MAXFLOAT|HUGE_VALF|INFINITY|NAN|M_E|M_LOG2E|M_LOG10E|M_LN2|M_LN10|M_PI|M_PI_2|M_PI_4|M_1_PI|M_2_PI|M_2_SQRTPI|M_SQRT2|M_SQRT1_2"
		self._MathFuncPattern = @"acos|acosh|acospi|asin|asinh|asinpi|atan|atan2|atanh|atanpi|atan2pi|cbrt|ceil|copysign|cos|cosh|cospi|erfc|erf|axp|axp2|axp10|expm1|fabs|fdim|floor|fma|fmax|fmin|fmod|fract|frexp|hypot|ilogb|ldexp|lgamma|lgamma_r|log|log2|log10|log1p|logb|mad|modf|nan|nextafter|pow|pown|powr|remainder|remquo|rint|rootn|round|rsqrt|sin|sincos|sinh|sinpi|sqrt|tan|tanh|tanpi|tgamma|trunc"
		self._NativeMathFuncPattern = @"native_cos|native_divide|native_exp|native_exp2|native_exp10|native_log|native_log2|native_log10|native_powr|native_recip|native_rsqrt|native_sin|native_sqrt|native_tan"
		self._CommonFuncPattern = @"clamp|degress|max|min|mix|radians|step|smoothstep|sign"
		self._GeomFuncPattern = @"cross|dot|distance|length|normalize"
		self._FastGeomFuncPattern = @"fast_distance|fast_length|fast_normalize"
		self._ImageFuncPattern = @"sampler_t|image2d_t|read_imagef|read_imagei|read_imageui|write_imagef|write_imagei|write_imageui|CLK_FILTER_NEAREST|CLK_FILTER_LINEAR|CLK_NORMALIZED_COORDS_FALSE|CLK_NORMALIZED_COORDS_TRUE|CLK_ADDRESS_CLAMP_TO_EDGE|CLK_ADDRESS_CLAMP|CLK_ADDRESS_NONE"
		self._SynchFuncPattern = @"barrier|mem_fence|CLK_LOCAL_MEM_FENCE|CLK_GLOBAL_MEM_FENCE|read_mem_fence|write_mem_fence"
		self._OtherPattern = @"#pragma|#define"
		self._KhakiStyle = TextStyle(Brushes.Khaki, None, FontStyle.Regular)
		self._BrownStyle = TextStyle(Brushes.Brown, None, FontStyle.Regular)
		self._NavyStyle = TextStyle(Brushes.Blue, None, FontStyle.Regular)
		self._BlueStyle = TextStyle(Brushes.DodgerBlue, None, FontStyle.Regular)
		self._PurpleStyle = TextStyle(Brushes.Purple, None, FontStyle.Regular)
		self._PurpleBoldStyle = TextStyle(Brushes.Purple, None, FontStyle.Bold)
		self._GrayStyle = TextStyle(Brushes.DarkGray, None, FontStyle.Regular)
		self._GreenStyle = TextStyle(Brushes.Green, None, FontStyle.Regular)
		self._GoldStyle = TextStyle(Brushes.Gold, None, FontStyle.Regular)
		self._RedStyle = TextStyle(Brushes.Red, None, FontStyle.Regular)
		self._BoldStyle = TextStyle(Brushes.Black, None, FontStyle.Bold | FontStyle.Underline)
		self._platformType = PlatformType.GetOperationSystemPlatform()
		self.Init()

	def Init():
		self._TypeRegex = Regex(str.Format(@"\b({0})\b", self._TypePattern), RegexCompiledOption)
		self._CommentRegex1 = Regex(@"//.*$", RegexOptions.Multiline | RegexCompiledOption)
		self._CommentRegex2 = Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption)
		self._CommentRegex3 = Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption)
		self._NumberRegex = Regex(@"\b\d+[\.]?\d*([e]\-?\d+)?[hulf]?\b|\b0x[a-f\d]+\b", RegexCompiledOption | RegexOptions.IgnoreCase)
		self._FuncNameRegex = Regex(str.Format(@"\b({0})\s+(?<range>[\w\d_]+)\s*\(", self._TypePattern), RegexCompiledOption)
		self._FuncSignatureRegex = Regex(str.Format(@"\b({0})\s+[\w\d_]+\s*\([^\)]*?\)", self._TypePattern), RegexCompiledOption)
		self._FuncSignatureParserRegex = Regex(str.Format(@"^(?<type>\S+)\s+(?<name>[\w\d_]+)\s*\((?<args>[^\)]*?)\)$"), RegexCompiledOption)
		self._FlowRegex = Regex(str.Format(@"\b({0})\b", self._FlowPattern), RegexCompiledOption)
		self._WorkItemFuncRegex = Regex(str.Format(@"\b({0})\b", self._WorkItemFuncPattern), RegexCompiledOption)
		self._ConstantsRegex = Regex(str.Format(@"\b({0})\b", self._ConstantsPattern), RegexCompiledOption)
		self._ExtensionsRegex = Regex(str.Format(@"\b({0})\b", self._ExtensionsPattern), RegexCompiledOption)
		self._QualifiersRegex = Regex(str.Format(@"\b({0})\b", self._QualifiersPattern), RegexCompiledOption)
		self._FuncRegex = Regex(str.Format(@"\b({0}|{1}|{2}|{3}|{4})\b", self._MathFuncPattern, self._NativeMathFuncPattern, self._CommonFuncPattern, self._GeomFuncPattern, self._FastGeomFuncPattern), RegexCompiledOption)
		self._ImageRegex = Regex(str.Format(@"\b({0})\b", self._ImageFuncPattern), RegexCompiledOption)
		self._SynchFuncRegex = Regex(str.Format(@"\b({0})\b", self._SynchFuncPattern), RegexCompiledOption)
		self._OtherRegex = Regex(str.Format(@"({0})\b", self._OtherPattern), RegexCompiledOption)

	Init = staticmethod(Init)

	def GetAllKeywords():
		result = List[str]()
		result.AddRange(self._TypePattern.Split('|'))
		result.AddRange(self._FlowPattern.Split('|'))
		result.AddRange(self._ExtensionsPattern.Split('|'))
		result.AddRange(self._QualifiersPattern.Split('|'))
		result.AddRange(self._WorkItemFuncPattern.Split('|'))
		result.AddRange(self._ConstantsPattern.Split('|'))
		result.AddRange(self._MathFuncPattern.Split('|'))
		result.AddRange(self._NativeMathFuncPattern.Split('|'))
		result.AddRange(self._CommonFuncPattern.Split('|'))
		result.AddRange(self._GeomFuncPattern.Split('|'))
		result.AddRange(self._FastGeomFuncPattern.Split('|'))
		result.AddRange(self._ImageFuncPattern.Split('|'))
		result.AddRange(self._SynchFuncPattern.Split('|'))
		result.AddRange(self._OtherPattern.Split('|'))
		return result

	GetAllKeywords = staticmethod(GetAllKeywords)

	def Highlight(range, customFunctions):
		#build regex for custom functions
		sb = StringBuilder()
		enumerator = customFunctions.GetEnumerator()
		while enumerator.MoveNext():
			func = enumerator.Current
			sb.Append(func.funcName + "|")
		CustomFuncRegex = Regex(@"\b(" + sb.ToString().TrimEnd('|') + @")\b")
		#
		range.tb.LeftBracket = '('
		range.tb.RightBracket = ')'
		#clear
		range.ClearStyle(self._GreenStyle, self._RedStyle, self._BoldStyle, self._KhakiStyle, self._BrownStyle, self._NavyStyle, self._PurpleStyle, self._GrayStyle, self._BlueStyle, self._GoldStyle, self._RedStyle)
		#comment
		range.SetStyle(self._GreenStyle, self._CommentRegex1)
		range.SetStyle(self._GreenStyle, self._CommentRegex2)
		range.SetStyle(self._GreenStyle, self._CommentRegex3)
		#number
		range.SetStyle(self._RedStyle, self._NumberRegex)
		#func name
		range.SetStyle(self._BoldStyle, self._FuncNameRegex)
		#keywords
		range.SetStyle(self._KhakiStyle, self._ExtensionsRegex)
		range.SetStyle(self._BrownStyle, self._QualifiersRegex)
		range.SetStyle(self._NavyStyle, self._TypeRegex)
		range.SetStyle(self._NavyStyle, self._FlowRegex)
		range.SetStyle(self._PurpleStyle, self._WorkItemFuncRegex)
		range.SetStyle(self._RedStyle, self._ConstantsRegex)
		range.SetStyle(self._PurpleStyle, self._FuncRegex)
		range.SetStyle(self._PurpleStyle, self._OtherRegex)
		range.SetStyle(self._GoldStyle, self._ImageRegex)
		range.SetStyle(self._RedStyle, self._SynchFuncRegex)
		range.SetStyle(self._PurpleBoldStyle, CustomFuncRegex)
		#clear folding markers
		range.ClearFoldingMarkers()
		#set folding markers
		range.SetFoldingMarkers("{", "}") #allow to collapse brackets block
		range.SetFoldingMarkers(@"/\*", @"\*/")

	Highlight = staticmethod(Highlight)
 #allow to collapse comment block
	def get_RegexCompiledOption(self):
		if self._platformType == Platform.X86:
			return RegexOptions.Compiled
		else:
			return RegexOptions.None

	RegexCompiledOption = property(fget=get_RegexCompiledOption)

	def BuildExplorerItems(range):
		list = List[ExplorerItem]()
		enumerator = range.GetRanges(self._FuncSignatureRegex).GetEnumerator()
		while enumerator.MoveNext():
			r = enumerator.Current
			m = self._FuncSignatureParserRegex.Match(r.Text)
			if not m.Success:
				continue
			item = ExplorerItem(type = ExplorerItemType.Method, line = r.Start.iLine, funcName = m.Groups["name"].Value, funcType = CLType.Parse(m.Groups["type"].Value))
			item.args = CLSyntaxHighlighter.ParseFuncArgs(m.Groups["args"].Value)
			list.Add(item)
		list.Sort(ExplorerItemComparer())
		return list

	BuildExplorerItems = staticmethod(BuildExplorerItems)

	def ParseFuncArgs(text):
		res = List[FunctionArgument]()
		parts = text.Trim().Split(',')
		enumerator = parts.GetEnumerator()
		while enumerator.MoveNext():
			part = enumerator.Current
			parts2 = part.Split(Array[Char]((' ', '*')), StringSplitOptions.RemoveEmptyEntries)
			if parts2.Length < 2:
				continue
			isPointer = part.Contains("*")
			arg = FunctionArgument(name = parts2[parts2.Length - 1], type = CLType.Parse(parts2[parts2.Length - 2] + ("*" if isPointer else "")))
			res.Add(arg)
		return res

	ParseFuncArgs = staticmethod(ParseFuncArgs)

class ExplorerItemType(object):
	def __init__(self):

class ExplorerItem(object):
	def __init__(self):
		self._args = List[FunctionArgument]()

class FunctionArgument(object):
	def __init__(self):

class CLType(object):
	def get_BaseType(self):

	def set_BaseType(self, value):

	BaseType = property(fget=get_BaseType, fset=set_BaseType)

	def get_Dim(self):

	def set_Dim(self, value):

	Dim = property(fget=get_Dim, fset=set_Dim)

	def get_IsPointer(self):

	def set_IsPointer(self, value):

	IsPointer = property(fget=get_IsPointer, fset=set_IsPointer)

	def Parse(s):
		m = Regex.Match(s.Trim(), @"^(?<type>[a-z]+)(?<dim>\d*)(?<isPointer>\*?)$")
		if not m.Success:
			return None
		result = CLType()
		result.IsPointer = m.Groups["isPointer"].Value != ""
		result.Dim = 1
		if m.Groups["dim"].Value != "":
			result.Dim = int.Parse(m.Groups["dim"].Value)
		result.BaseType = m.Groups["type"].Value
		return result

	Parse = staticmethod(Parse)

	def ToString(self):
		return self.BaseType + (self.Dim.ToString() if self.Dim > 1 else "") + ("*" if self.IsPointer else "")

class ExplorerItemComparer(IComparer):
	def Compare(self, x, y):
		return x.funcName.CompareTo(y.funcName)