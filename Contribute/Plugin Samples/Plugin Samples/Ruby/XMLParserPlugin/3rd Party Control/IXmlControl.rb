require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing.Printing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module XMLTreeView
	# <summary>
	# Summary description for IXmlControl.
	# </summary>
	class IXmlControl
		def Xml
		end

		def Xml=(value)
		end

		def Search(sender, e)
		end

		def StartSearch(criterion, caseSensitive)
		end

		def Next()
		end

		def Print(sender, e)
		end

		def PrintPage(sender, e)
		end

		def PrintDocument
		end

		def Edit(sender, e)
		end

		def Delete(sender, e)
		end

		def Copy(sender, e)
		end

		def Paste(sender, e)
		end

		def Save(sender, e)
		end
	end
end