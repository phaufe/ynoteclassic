from System.IO import *
from System.Drawing import *
from FastColoredTextBoxNS import *
from System.Collections.Generic import *

class SpellChecker(object):
	def __init__(self, fctb, dictionarypath):
		self._wavyStyle = WavyLineStyle(255, Color.Red)
		List = File.ReadAllLines(dictionarypath)
		DictionaryPath = dictionarypath
		self._words = HashSet[str](List, System.StringComparer.InvariantCultureIgnoreCase)
		self._tb = fctb
		fctb = self._tb
		self._tb.TextChangedDelayed += self.tb_TextChangedDelayed

	def get_DictionaryPath(self):
		return self._Dictionary

	def set_DictionaryPath(self, value):
		self._Dictionary = value

	DictionaryPath = property(fget=get_DictionaryPath, fset=set_DictionaryPath)

	def SpellCheck(self, sender):
		self.tb_TextChangedDelayed(sender, TextChangedEventArgs(self._tb.Range))

	def tb_TextChangedDelayed(self, sender, e):
		System.Threading.ThreadPool.QueueUserWorkItem()

	#
	def Clear(self):
		self._tb.Range.ClearStyle(self._wavyStyle)