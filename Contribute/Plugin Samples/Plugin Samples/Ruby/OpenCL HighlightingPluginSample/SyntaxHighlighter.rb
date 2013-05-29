require "mscorlib"
require "System.Collections.Generic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Text, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "FastColoredTextBoxNS, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Text.RegularExpressions, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module HighlightingPluginSample
	class CLSyntaxHighlighter
		def initialize()
			@TypePattern = @"void|bool|char|uchar|short|ushort|int|uint|long|ulong|float|double|half|size_t|char2|uchar2|short2|ushort2|int2|uint2|long2|ulong2|float2|double2|char4|uchar4|short4|ushort4|int4|uint4|long4|ulong4|float4|double4|char8|uchar8|short8|ushort8|int8|uint8|long8|ulong8|float8|double8|char16|uchar16|short16|ushort16|int16|uint16|long16|ulong16|float16|double16"
			@FlowPattern = @"for|while|if|else|return"
			@ExtensionsPattern = @"cl_khr_fp64|cl_khr_global_int32_base_atomics|cl_khr_global_int32_extended_atomics|cl_khr_local_int32_base_atomics|cl_khr_local_int32_extended_atomics|cl_khr_int64_base_atomics|cl_khr_int64_extended_atomics|cl_khr_3d_image_writes|cl_khr_byte_addressable_store|cl_khr_fp16"
			@QualifiersPattern = @"kernel|read_only|write_only|global|local|constant|private|__kernel|__read_only|__write_only|__global|__local|__constant|__private|__attribute__|reqd_work_group_size|work_group_size_hint|vec_type_hint|const"
			@WorkItemFuncPattern = @"get_work_dim|get_global_size|get_global_id|get_local_size|get_local_id|get_num_groups|get_group_id"
			@ConstantsPattern = @"MAXFLOAT|HUGE_VALF|INFINITY|NAN|M_E|M_LOG2E|M_LOG10E|M_LN2|M_LN10|M_PI|M_PI_2|M_PI_4|M_1_PI|M_2_PI|M_2_SQRTPI|M_SQRT2|M_SQRT1_2"
			@MathFuncPattern = @"acos|acosh|acospi|asin|asinh|asinpi|atan|atan2|atanh|atanpi|atan2pi|cbrt|ceil|copysign|cos|cosh|cospi|erfc|erf|axp|axp2|axp10|expm1|fabs|fdim|floor|fma|fmax|fmin|fmod|fract|frexp|hypot|ilogb|ldexp|lgamma|lgamma_r|log|log2|log10|log1p|logb|mad|modf|nan|nextafter|pow|pown|powr|remainder|remquo|rint|rootn|round|rsqrt|sin|sincos|sinh|sinpi|sqrt|tan|tanh|tanpi|tgamma|trunc"
			@NativeMathFuncPattern = @"native_cos|native_divide|native_exp|native_exp2|native_exp10|native_log|native_log2|native_log10|native_powr|native_recip|native_rsqrt|native_sin|native_sqrt|native_tan"
			@CommonFuncPattern = @"clamp|degress|max|min|mix|radians|step|smoothstep|sign"
			@GeomFuncPattern = @"cross|dot|distance|length|normalize"
			@FastGeomFuncPattern = @"fast_distance|fast_length|fast_normalize"
			@ImageFuncPattern = @"sampler_t|image2d_t|read_imagef|read_imagei|read_imageui|write_imagef|write_imagei|write_imageui|CLK_FILTER_NEAREST|CLK_FILTER_LINEAR|CLK_NORMALIZED_COORDS_FALSE|CLK_NORMALIZED_COORDS_TRUE|CLK_ADDRESS_CLAMP_TO_EDGE|CLK_ADDRESS_CLAMP|CLK_ADDRESS_NONE"
			@SynchFuncPattern = @"barrier|mem_fence|CLK_LOCAL_MEM_FENCE|CLK_GLOBAL_MEM_FENCE|read_mem_fence|write_mem_fence"
			@OtherPattern = @"#pragma|#define"
			@KhakiStyle = TextStyle.new(Brushes.Khaki, nil, FontStyle.Regular)
			@BrownStyle = TextStyle.new(Brushes.Brown, nil, FontStyle.Regular)
			@NavyStyle = TextStyle.new(Brushes.Blue, nil, FontStyle.Regular)
			@BlueStyle = TextStyle.new(Brushes.DodgerBlue, nil, FontStyle.Regular)
			@PurpleStyle = TextStyle.new(Brushes.Purple, nil, FontStyle.Regular)
			@PurpleBoldStyle = TextStyle.new(Brushes.Purple, nil, FontStyle.Bold)
			@GrayStyle = TextStyle.new(Brushes.DarkGray, nil, FontStyle.Regular)
			@GreenStyle = TextStyle.new(Brushes.Green, nil, FontStyle.Regular)
			@GoldStyle = TextStyle.new(Brushes.Gold, nil, FontStyle.Regular)
			@RedStyle = TextStyle.new(Brushes.Red, nil, FontStyle.Regular)
			@BoldStyle = TextStyle.new(Brushes.Black, nil, FontStyle.Bold | FontStyle.Underline)
			@platformType = PlatformType.GetOperationSystemPlatform()
			self.Init()
		end

		def CLSyntaxHighlighter.Init()
			@TypeRegex = Regex.new(System::String.Format(@"\b({0})\b", @TypePattern), RegexCompiledOption)
			@CommentRegex1 = Regex.new(@"//.*$", RegexOptions.Multiline | RegexCompiledOption)
			@CommentRegex2 = Regex.new(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption)
			@CommentRegex3 = Regex.new(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption)
			@NumberRegex = Regex.new(@"\b\d+[\.]?\d*([e]\-?\d+)?[hulf]?\b|\b0x[a-f\d]+\b", RegexCompiledOption | RegexOptions.IgnoreCase)
			@FuncNameRegex = Regex.new(System::String.Format(@"\b({0})\s+(?<range>[\w\d_]+)\s*\(", @TypePattern), RegexCompiledOption)
			@FuncSignatureRegex = Regex.new(System::String.Format(@"\b({0})\s+[\w\d_]+\s*\([^\)]*?\)", @TypePattern), RegexCompiledOption)
			@FuncSignatureParserRegex = Regex.new(System::String.Format(@"^(?<type>\S+)\s+(?<name>[\w\d_]+)\s*\((?<args>[^\)]*?)\)$"), RegexCompiledOption)
			@FlowRegex = Regex.new(System::String.Format(@"\b({0})\b", @FlowPattern), RegexCompiledOption)
			@WorkItemFuncRegex = Regex.new(System::String.Format(@"\b({0})\b", @WorkItemFuncPattern), RegexCompiledOption)
			@ConstantsRegex = Regex.new(System::String.Format(@"\b({0})\b", @ConstantsPattern), RegexCompiledOption)
			@ExtensionsRegex = Regex.new(System::String.Format(@"\b({0})\b", @ExtensionsPattern), RegexCompiledOption)
			@QualifiersRegex = Regex.new(System::String.Format(@"\b({0})\b", @QualifiersPattern), RegexCompiledOption)
			@FuncRegex = Regex.new(System::String.Format(@"\b({0}|{1}|{2}|{3}|{4})\b", @MathFuncPattern, @NativeMathFuncPattern, @CommonFuncPattern, @GeomFuncPattern, @FastGeomFuncPattern), RegexCompiledOption)
			@ImageRegex = Regex.new(System::String.Format(@"\b({0})\b", @ImageFuncPattern), RegexCompiledOption)
			@SynchFuncRegex = Regex.new(System::String.Format(@"\b({0})\b", @SynchFuncPattern), RegexCompiledOption)
			@OtherRegex = Regex.new(System::String.Format(@"({0})\b", @OtherPattern), RegexCompiledOption)
		end

		def CLSyntaxHighlighter.GetAllKeywords()
			result = List[System::String].new()
			result.AddRange(@TypePattern.Split('|'))
			result.AddRange(@FlowPattern.Split('|'))
			result.AddRange(@ExtensionsPattern.Split('|'))
			result.AddRange(@QualifiersPattern.Split('|'))
			result.AddRange(@WorkItemFuncPattern.Split('|'))
			result.AddRange(@ConstantsPattern.Split('|'))
			result.AddRange(@MathFuncPattern.Split('|'))
			result.AddRange(@NativeMathFuncPattern.Split('|'))
			result.AddRange(@CommonFuncPattern.Split('|'))
			result.AddRange(@GeomFuncPattern.Split('|'))
			result.AddRange(@FastGeomFuncPattern.Split('|'))
			result.AddRange(@ImageFuncPattern.Split('|'))
			result.AddRange(@SynchFuncPattern.Split('|'))
			result.AddRange(@OtherPattern.Split('|'))
			return result
		end

		def CLSyntaxHighlighter.Highlight(range, customFunctions)
			#build regex for custom functions
			sb = StringBuilder.new()
			enumerator = customFunctions.GetEnumerator()
			while enumerator.MoveNext()
				func = enumerator.Current
				sb.Append(func.funcName + "|")
			end
			CustomFuncRegex = Regex.new(@"\b(" + sb.ToString().TrimEnd('|') + @")\b")
			#
			range.tb.LeftBracket = '('
			range.tb.RightBracket = ')'
			#clear
			range.ClearStyle(@GreenStyle, @RedStyle, @BoldStyle, @KhakiStyle, @BrownStyle, @NavyStyle, @PurpleStyle, @GrayStyle, @BlueStyle, @GoldStyle, @RedStyle)
			#comment
			range.SetStyle(@GreenStyle, @CommentRegex1)
			range.SetStyle(@GreenStyle, @CommentRegex2)
			range.SetStyle(@GreenStyle, @CommentRegex3)
			#number
			range.SetStyle(@RedStyle, @NumberRegex)
			#func name
			range.SetStyle(@BoldStyle, @FuncNameRegex)
			#keywords
			range.SetStyle(@KhakiStyle, @ExtensionsRegex)
			range.SetStyle(@BrownStyle, @QualifiersRegex)
			range.SetStyle(@NavyStyle, @TypeRegex)
			range.SetStyle(@NavyStyle, @FlowRegex)
			range.SetStyle(@PurpleStyle, @WorkItemFuncRegex)
			range.SetStyle(@RedStyle, @ConstantsRegex)
			range.SetStyle(@PurpleStyle, @FuncRegex)
			range.SetStyle(@PurpleStyle, @OtherRegex)
			range.SetStyle(@GoldStyle, @ImageRegex)
			range.SetStyle(@RedStyle, @SynchFuncRegex)
			range.SetStyle(@PurpleBoldStyle, CustomFuncRegex)
			#clear folding markers
			range.ClearFoldingMarkers()
			#set folding markers
			range.SetFoldingMarkers("{", "}") #allow to collapse brackets block
			range.SetFoldingMarkers(@"/\*", @"\*/")
		end
 #allow to collapse comment block
		def RegexCompiledOption
			if @platformType == Platform.X86 then
				return RegexOptions.Compiled
			else
				return RegexOptions.None
			end
		end

		def CLSyntaxHighlighter.BuildExplorerItems(range)
			list = List[ExplorerItem].new()
			enumerator = range.GetRanges(@FuncSignatureRegex).GetEnumerator()
			while enumerator.MoveNext()
				r = enumerator.Current
				m = @FuncSignatureParserRegex.Match(r.Text)
				if not m.Success then
					next
				end
				item = ExplorerItem.new(type = ExplorerItemType.Method, line = r.Start.iLine, funcName = m.Groups["name"].Value, funcType = CLType.Parse(m.Groups["type"].Value))
				item.args = CLSyntaxHighlighter.ParseFuncArgs(m.Groups["args"].Value)
				list.Add(item)
			end
			list.Sort(ExplorerItemComparer.new())
			return list
		end

		def CLSyntaxHighlighter.ParseFuncArgs(text)
			res = List[FunctionArgument].new()
			parts = text.Trim().Split(',')
			enumerator = parts.GetEnumerator()
			while enumerator.MoveNext()
				part = enumerator.Current
				parts2 = part.Split(Array[System::Char].new([' ', '*']), StringSplitOptions.RemoveEmptyEntries)
				if parts2.Length < 2 then
					next
				end
				isPointer = part.Contains("*")
				arg = FunctionArgument.new(name = parts2[parts2.Length - 1], type = CLType.Parse(parts2[parts2.Length - 2] + (isPointer ? "*" : "")))
				res.Add(arg)
			end
			return res
		end
	end
	class ExplorerItemType
		def initialize()
		end
	end
	class ExplorerItem
		def initialize()
			@args = List[FunctionArgument].new()
		end
	end
	class FunctionArgument
		def initialize()
		end
	end
	class CLType
		def BaseType
		end

		def BaseType=(value)
		end

		def Dim
		end

		def Dim=(value)
		end

		def IsPointer
		end

		def IsPointer=(value)
		end

		def CLType.Parse(s)
			m = Regex.Match(s.Trim(), @"^(?<type>[a-z]+)(?<dim>\d*)(?<isPointer>\*?)$")
			if not m.Success then
				return nil
			end
			result = CLType.new()
			result.IsPointer = m.Groups["isPointer"].Value != ""
			result.Dim = 1
			if m.Groups["dim"].Value != "" then
				result.Dim = System::Int32.Parse(m.Groups["dim"].Value)
			end
			result.BaseType = m.Groups["type"].Value
			return result
		end

		def ToString()
			return self.BaseType + (self.Dim > 1 ? self.Dim.ToString() : "") + (self.IsPointer ? "*" : "")
		end
	end
	class ExplorerItemComparer < IComparer
		def Compare(x, y)
			return x.funcName.CompareTo(y.funcName)
		end
	end
end