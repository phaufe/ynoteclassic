require "mscorlib"
require "System.IO, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "FastColoredTextBoxNS, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Collections.Generic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module WavyStylePlugin
	class SpellChecker
		def initialize(fctb, dictionarypath)
			@wavyStyle = WavyLineStyle.new(255, Color.Red)
			List = File.ReadAllLines(dictionarypath)
			DictionaryPath = dictionarypath
			@words = HashSet[System::String].new(List, System.StringComparer.InvariantCultureIgnoreCase)
			self.@tb = fctb
			fctb = self.@tb
			self.@tb.TextChangedDelayed { |sender, e| self.tb_TextChangedDelayed(sender, e) }
		end

		def DictionaryPath
			return @Dictionary
		end

		def DictionaryPath=(value)
			@Dictionary = value
		end

		def SpellCheck(sender)
			self.tb_TextChangedDelayed(sender, TextChangedEventArgs.new(self.@tb.Range))
		end

		def tb_TextChangedDelayed(sender, e)
			System.Threading.ThreadPool.QueueUserWorkItem(			list = List[FastColoredTextBoxNS::Range].new()
			enumerator = e.ChangedRange.GetRanges(@"[\w']+").GetEnumerator()
			while enumerator.MoveNext()
				word = enumerator.Current
				if not @words.Contains(word.Text) then
					list.Add(word)
				end
			end
			#
			e.ChangedRange.ClearStyle(@wavyStyle)
			enumerator = list.GetEnumerator()
			while enumerator.MoveNext()
				word = enumerator.Current
				word.SetStyle(@wavyStyle)
			end
)
		end

		def Clear()
			self.@tb.Range.ClearStyle(@wavyStyle)
		end
	end
end