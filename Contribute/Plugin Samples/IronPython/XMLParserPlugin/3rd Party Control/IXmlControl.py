from System import *
from System.Drawing.Printing import *

class IXmlControl(object):
	""" <summary>
	 Summary description for IXmlControl.
	 </summary>
	"""
	def get_Xml(self):

	def set_Xml(self, value):

	Xml = property(fget=get_Xml, fset=set_Xml)

	def Search(self, sender, e):
		pass

	def StartSearch(self, criterion, caseSensitive):
		pass

	def Next(self):
		pass

	def Print(self, sender, e):
		pass

	def PrintPage(self, sender, e):
		pass

	def get_PrintDocument(self):

	PrintDocument = property(fget=get_PrintDocument)

	def Edit(self, sender, e):
		pass

	def Delete(self, sender, e):
		pass

	def Copy(self, sender, e):
		pass

	def Paste(self, sender, e):
		pass

	def Save(self, sender, e):
		pass