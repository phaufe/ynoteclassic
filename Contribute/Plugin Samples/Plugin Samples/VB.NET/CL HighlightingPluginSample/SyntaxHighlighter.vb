

Imports System.Collections.Generic
Imports System.Text
Imports FastColoredTextBoxNS
Imports System.Drawing
Imports System.Text.RegularExpressions

Public NotInheritable Class CLSyntaxHighlighter
	Private Sub New()
	End Sub
	Public Const TypePattern As String = "void|bool|char|uchar|short|ushort|int|uint|long|ulong|float|double|half|size_t|char2|uchar2|short2|ushort2|int2|uint2|long2|ulong2|float2|double2|char4|uchar4|short4|ushort4|int4|uint4|long4|ulong4|float4|double4|char8|uchar8|short8|ushort8|int8|uint8|long8|ulong8|float8|double8|char16|uchar16|short16|ushort16|int16|uint16|long16|ulong16|float16|double16"
	Public Const FlowPattern As String = "for|while|if|else|return"
	Public Const ExtensionsPattern As String = "cl_khr_fp64|cl_khr_global_int32_base_atomics|cl_khr_global_int32_extended_atomics|cl_khr_local_int32_base_atomics|cl_khr_local_int32_extended_atomics|cl_khr_int64_base_atomics|cl_khr_int64_extended_atomics|cl_khr_3d_image_writes|cl_khr_byte_addressable_store|cl_khr_fp16"
	Public Const QualifiersPattern As String = "kernel|read_only|write_only|global|local|constant|private|__kernel|__read_only|__write_only|__global|__local|__constant|__private|__attribute__|reqd_work_group_size|work_group_size_hint|vec_type_hint|const"
	Public Const WorkItemFuncPattern As String = "get_work_dim|get_global_size|get_global_id|get_local_size|get_local_id|get_num_groups|get_group_id"
	Public Const ConstantsPattern As String = "MAXFLOAT|HUGE_VALF|INFINITY|NAN|M_E|M_LOG2E|M_LOG10E|M_LN2|M_LN10|M_PI|M_PI_2|M_PI_4|M_1_PI|M_2_PI|M_2_SQRTPI|M_SQRT2|M_SQRT1_2"
	Public Const MathFuncPattern As String = "acos|acosh|acospi|asin|asinh|asinpi|atan|atan2|atanh|atanpi|atan2pi|cbrt|ceil|copysign|cos|cosh|cospi|erfc|erf|axp|axp2|axp10|expm1|fabs|fdim|floor|fma|fmax|fmin|fmod|fract|frexp|hypot|ilogb|ldexp|lgamma|lgamma_r|log|log2|log10|log1p|logb|mad|modf|nan|nextafter|pow|pown|powr|remainder|remquo|rint|rootn|round|rsqrt|sin|sincos|sinh|sinpi|sqrt|tan|tanh|tanpi|tgamma|trunc"
	Public Const NativeMathFuncPattern As String = "native_cos|native_divide|native_exp|native_exp2|native_exp10|native_log|native_log2|native_log10|native_powr|native_recip|native_rsqrt|native_sin|native_sqrt|native_tan"
	Public Const CommonFuncPattern As String = "clamp|degress|max|min|mix|radians|step|smoothstep|sign"
	Public Const GeomFuncPattern As String = "cross|dot|distance|length|normalize"
	Public Const FastGeomFuncPattern As String = "fast_distance|fast_length|fast_normalize"
	Public Const ImageFuncPattern As String = "sampler_t|image2d_t|read_imagef|read_imagei|read_imageui|write_imagef|write_imagei|write_imageui|CLK_FILTER_NEAREST|CLK_FILTER_LINEAR|CLK_NORMALIZED_COORDS_FALSE|CLK_NORMALIZED_COORDS_TRUE|CLK_ADDRESS_CLAMP_TO_EDGE|CLK_ADDRESS_CLAMP|CLK_ADDRESS_NONE"
	Public Const SynchFuncPattern As String = "barrier|mem_fence|CLK_LOCAL_MEM_FENCE|CLK_GLOBAL_MEM_FENCE|read_mem_fence|write_mem_fence"
	Public Const OtherPattern As String = "#pragma|#define"

	Shared ReadOnly KhakiStyle As Style = New TextStyle(Brushes.Khaki, Nothing, FontStyle.Regular)
	Shared ReadOnly BrownStyle As Style = New TextStyle(Brushes.Brown, Nothing, FontStyle.Regular)
	Shared ReadOnly NavyStyle As Style = New TextStyle(Brushes.Blue, Nothing, FontStyle.Regular)
	Shared ReadOnly BlueStyle As Style = New TextStyle(Brushes.DodgerBlue, Nothing, FontStyle.Regular)
	Shared ReadOnly PurpleStyle As Style = New TextStyle(Brushes.Purple, Nothing, FontStyle.Regular)
	Shared ReadOnly PurpleBoldStyle As Style = New TextStyle(Brushes.Purple, Nothing, FontStyle.Bold)
	Shared ReadOnly GrayStyle As Style = New TextStyle(Brushes.DarkGray, Nothing, FontStyle.Regular)
	Shared ReadOnly GreenStyle As Style = New TextStyle(Brushes.Green, Nothing, FontStyle.Regular)
	Shared ReadOnly GoldStyle As Style = New TextStyle(Brushes.Gold, Nothing, FontStyle.Regular)
	Shared ReadOnly RedStyle As Style = New TextStyle(Brushes.Red, Nothing, FontStyle.Regular)
	Shared ReadOnly BoldStyle As Style = New TextStyle(Brushes.Black, Nothing, FontStyle.Bold Or FontStyle.Underline)


	Shared ReadOnly platformType As Platform = PlatformType.GetOperationSystemPlatform()

	Shared Sub New()
		Init()
	End Sub

	Shared TypeRegex As Regex, CommentRegex1 As Regex, CommentRegex2 As Regex, CommentRegex3 As Regex, NumberRegex As Regex, FuncNameRegex As Regex, _
		FlowRegex As Regex, WorkItemFuncRegex As Regex, ConstantsRegex As Regex, ExtensionsRegex As Regex, QualifiersRegex As Regex, FuncRegex As Regex, _
		ImageRegex As Regex, SynchFuncRegex As Regex, OtherRegex As Regex, FuncSignatureRegex As Regex, FuncSignatureParserRegex As Regex

	Private Shared Sub Init()
		TypeRegex = New Regex(String.Format("\b({0})\b", TypePattern), RegexCompiledOption)
		CommentRegex1 = New Regex("//.*$", RegexOptions.Multiline Or RegexCompiledOption)
		CommentRegex2 = New Regex("(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline Or RegexCompiledOption)
		CommentRegex3 = New Regex("(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline Or RegexOptions.RightToLeft Or RegexCompiledOption)
		NumberRegex = New Regex("\b\d+[\.]?\d*([e]\-?\d+)?[hulf]?\b|\b0x[a-f\d]+\b", RegexCompiledOption Or RegexOptions.IgnoreCase)
		FuncNameRegex = New Regex(String.Format("\b({0})\s+(?<range>[\w\d_]+)\s*\(", TypePattern), RegexCompiledOption)
		FuncSignatureRegex = New Regex(String.Format("\b({0})\s+[\w\d_]+\s*\([^\)]*?\)", TypePattern), RegexCompiledOption)
		FuncSignatureParserRegex = New Regex(String.Format("^(?<type>\S+)\s+(?<name>[\w\d_]+)\s*\((?<args>[^\)]*?)\)$"), RegexCompiledOption)
		FlowRegex = New Regex(String.Format("\b({0})\b", FlowPattern), RegexCompiledOption)
		WorkItemFuncRegex = New Regex(String.Format("\b({0})\b", WorkItemFuncPattern), RegexCompiledOption)
		ConstantsRegex = New Regex(String.Format("\b({0})\b", ConstantsPattern), RegexCompiledOption)
		ExtensionsRegex = New Regex(String.Format("\b({0})\b", ExtensionsPattern), RegexCompiledOption)
		QualifiersRegex = New Regex(String.Format("\b({0})\b", QualifiersPattern), RegexCompiledOption)
		FuncRegex = New Regex(String.Format("\b({0}|{1}|{2}|{3}|{4})\b", MathFuncPattern, NativeMathFuncPattern, CommonFuncPattern, GeomFuncPattern, FastGeomFuncPattern), RegexCompiledOption)
		ImageRegex = New Regex(String.Format("\b({0})\b", ImageFuncPattern), RegexCompiledOption)
		SynchFuncRegex = New Regex(String.Format("\b({0})\b", SynchFuncPattern), RegexCompiledOption)
		OtherRegex = New Regex(String.Format("({0})\b", OtherPattern), RegexCompiledOption)
	End Sub

	Public Shared Function GetAllKeywords() As List(Of String)
		Dim result As New List(Of String)()
		result.AddRange(TypePattern.Split("|"C))
		result.AddRange(FlowPattern.Split("|"C))
		result.AddRange(ExtensionsPattern.Split("|"C))
		result.AddRange(QualifiersPattern.Split("|"C))
		result.AddRange(WorkItemFuncPattern.Split("|"C))
		result.AddRange(ConstantsPattern.Split("|"C))
		result.AddRange(MathFuncPattern.Split("|"C))
		result.AddRange(NativeMathFuncPattern.Split("|"C))
		result.AddRange(CommonFuncPattern.Split("|"C))
		result.AddRange(GeomFuncPattern.Split("|"C))
		result.AddRange(FastGeomFuncPattern.Split("|"C))
		result.AddRange(ImageFuncPattern.Split("|"C))
		result.AddRange(SynchFuncPattern.Split("|"C))
		result.AddRange(OtherPattern.Split("|"C))

		Return result
	End Function

	Public Shared Sub Highlight(range As Range, customFunctions As List(Of ExplorerItem))
		'build regex for custom functions
		Dim sb As New StringBuilder()
		For Each func As var In customFunctions
			sb.Append(func.funcName & "|")
		Next
		Dim CustomFuncRegex As New Regex("\b(" & sb.ToString().TrimEnd("|"C) & ")\b")
		'
		range.tb.LeftBracket = "("C
		range.tb.RightBracket = ")"C
		'clear
		range.ClearStyle(GreenStyle, RedStyle, BoldStyle, KhakiStyle, BrownStyle, NavyStyle, _
			PurpleStyle, GrayStyle, BlueStyle, GoldStyle, RedStyle)
		'comment
		range.SetStyle(GreenStyle, CommentRegex1)
		range.SetStyle(GreenStyle, CommentRegex2)
		range.SetStyle(GreenStyle, CommentRegex3)
		'number
		range.SetStyle(RedStyle, NumberRegex)
		'func name
		range.SetStyle(BoldStyle, FuncNameRegex)
		'keywords
		range.SetStyle(KhakiStyle, ExtensionsRegex)
		range.SetStyle(BrownStyle, QualifiersRegex)
		range.SetStyle(NavyStyle, TypeRegex)
		range.SetStyle(NavyStyle, FlowRegex)
		range.SetStyle(PurpleStyle, WorkItemFuncRegex)
		range.SetStyle(RedStyle, ConstantsRegex)
		range.SetStyle(PurpleStyle, FuncRegex)
		range.SetStyle(PurpleStyle, OtherRegex)
		range.SetStyle(GoldStyle, ImageRegex)
		range.SetStyle(RedStyle, SynchFuncRegex)
		range.SetStyle(PurpleBoldStyle, CustomFuncRegex)

		'clear folding markers
		range.ClearFoldingMarkers()
		'set folding markers
		range.SetFoldingMarkers("{", "}")
		'allow to collapse brackets block
		range.SetFoldingMarkers("/\*", "\*/")
		'allow to collapse comment block
	End Sub

	Private Shared ReadOnly Property RegexCompiledOption() As RegexOptions
		Get
			If platformType = Platform.X86 Then
				Return RegexOptions.Compiled
			Else
				Return RegexOptions.None
			End If
		End Get
	End Property

	Public Shared Function BuildExplorerItems(range As Range) As List(Of ExplorerItem)
		Dim list As New List(Of ExplorerItem)()

		For Each r As var In range.GetRanges(FuncSignatureRegex)
			Dim m = FuncSignatureParserRegex.Match(r.Text)
			If Not m.Success Then
				Continue For
			End If
			Dim item = New ExplorerItem() With { _
				Key .type = ExplorerItemType.Method, _
				Key .line = r.Start.iLine, _
				Key .funcName = m.Groups("name").Value, _
				Key .funcType = CLType.Parse(m.Groups("type").Value) _
			}
			item.args = ParseFuncArgs(m.Groups("args").Value)
			list.Add(item)
		Next

		list.Sort(New ExplorerItemComparer())

		Return list
	End Function

	Private Shared Function ParseFuncArgs(text As String) As List(Of FunctionArgument)
		Dim res As New List(Of FunctionArgument)()
		Dim parts = text.Trim().Split(","C)
		For Each part As var In parts
			Dim parts2 = part.Split(New Char() {" "C, "*"C}, StringSplitOptions.RemoveEmptyEntries)
			If parts2.Length < 2 Then
				Continue For
			End If
			Dim isPointer As Boolean = part.Contains("*")
			Dim arg = New FunctionArgument() With { _
				Key .name = parts2(parts2.Length - 1), _
				Key .type = CLType.Parse(parts2(parts2.Length - 2) & (If(isPointer, "*", ""))) _
			}
			res.Add(arg)
		Next

		Return res
	End Function
End Class

Public Enum ExplorerItemType
	[Class]
	Method
	[Property]
	[Event]
End Enum

Public Class ExplorerItem
	Public type As ExplorerItemType
	Public funcName As String
	Public funcType As CLType
	Public args As New List(Of FunctionArgument)()
	Public line As Integer
End Class

Public Class FunctionArgument
	Public type As CLType
	Public name As String
End Class

Public Class CLType
	Public Property BaseType() As String
		Get
			Return m_BaseType
		End Get
		Private Set
			m_BaseType = Value
		End Set
	End Property
	Private m_BaseType As String
	Public Property [Dim]() As Integer
		Get
			Return m_Dim
		End Get
		Private Set
			m_Dim = Value
		End Set
	End Property
	Private m_Dim As Integer
	Public Property IsPointer() As Boolean
		Get
			Return m_IsPointer
		End Get
		Private Set
			m_IsPointer = Value
		End Set
	End Property
	Private m_IsPointer As Boolean

	Public Shared Function Parse(s As String) As CLType
		Dim m = Regex.Match(s.Trim(), "^(?<type>[a-z]+)(?<dim>\d*)(?<isPointer>\*?)$")
		If Not m.Success Then
			Return Nothing
		End If
		Dim result = New CLType()
		result.IsPointer = m.Groups("isPointer").Value <> ""
		result.[Dim] = 1
		If m.Groups("dim").Value <> "" Then
			result.[Dim] = Integer.Parse(m.Groups("dim").Value)
		End If
		result.BaseType = m.Groups("type").Value

		Return result
	End Function

	Public Overrides Function ToString() As String
		Return BaseType & (If([Dim] > 1, [Dim].ToString(), "")) & (If(IsPointer, "*", ""))
	End Function
End Class

Class ExplorerItemComparer
	Implements IComparer(Of ExplorerItem)
	Public Function Compare(x As ExplorerItem, y As ExplorerItem) As Integer
		Return x.funcName.CompareTo(y.funcName)
	End Function
End Class
