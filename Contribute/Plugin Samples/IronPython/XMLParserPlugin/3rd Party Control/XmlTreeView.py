from System import *
from System.IO import *
from System.Xml import *
from System.Data import *
from System.Drawing import *
from System.Collections import *
from System.Windows.Forms import *
from System.ComponentModel import *
from System.Drawing.Printing import *

class XmlTreeView(System.Windows.Forms.UserControl):
    """ <summary>
          Summary description for XmlTreeView.
        </summary>
    """
    # <summary> 
    # Required designer variable.
    # </summary>
    def __init__(self):
        self._content = str.Empty
        self._fileXml = str.Empty
        self._filePath = str.Empty
        self._MessageBoxTitle = "XmlTreeView"
        self._components = None
        # This call is required by the Windows.Forms Form Designer.
        self.InitializeComponent()

    def Dispose(self, disposing):
        """ <summary> 
         Clean up any resources being used.
         </summary>
        """
        if disposing:
            if self._components != None:
                self._components.Dispose()
        self.Dispose(disposing)

    def InitializeComponent(self):
        """ <summary> 
         Required method for Designer support - do not modify 
         the contents of this method with the code editor.
         </summary>
        """
        self._popUpMenu = System.Windows.Forms.ContextMenu()
        self._printMenuItem = System.Windows.Forms.MenuItem()
        self._searchMenuItem = System.Windows.Forms.MenuItem()
        self._deleteMenuItem = System.Windows.Forms.MenuItem()
        self._saveMenuItem = System.Windows.Forms.MenuItem()
        self._pasteMenuItem = System.Windows.Forms.MenuItem()
        self._copyMenuItem = System.Windows.Forms.MenuItem()
        self._editMenuItem = System.Windows.Forms.MenuItem()
        self._printDialog1 = System.Windows.Forms.PrintDialog()
        self._saveFileDialog1 = System.Windows.Forms.SaveFileDialog()
        self.SuspendLayout()
        # 
        # popUpMenu
        # 
        self._popUpMenu.MenuItems.AddRange(Array[System.Windows.Forms.MenuItem]((self._printMenuItem, self._searchMenuItem, self._deleteMenuItem, self._saveMenuItem, self._pasteMenuItem, self._copyMenuItem, self._editMenuItem)))
        # 
        # printMenuItem
        # 
        self._printMenuItem.Index = 0
        self._printMenuItem.Text = "&Print"
        # 
        # searchMenuItem
        # 
        self._searchMenuItem.Index = 1
        self._searchMenuItem.Text = "&Search"
        # 
        # deleteMenuItem
        # 
        self._deleteMenuItem.Index = 2
        self._deleteMenuItem.Text = "&Delete"
        # 
        # saveMenuItem
        # 
        self._saveMenuItem.Index = 3
        self._saveMenuItem.Text = "S&ave As..."
        # 
        # pasteMenuItem
        # 
        self._pasteMenuItem.Index = 4
        self._pasteMenuItem.Text = "Past&e"
        # 
        # copyMenuItem
        # 
        self._copyMenuItem.Index = 5
        self._copyMenuItem.Text = "&Copy"
        # 
        # editMenuItem
        # 
        self._editMenuItem.Index = 6
        self._editMenuItem.Text = "Ed&it"
        # 
        # saveFileDialog1
        # 
        self._saveFileDialog1.Filter = "XML-Files|*.xml"
        # 
        # XmlTreeView
        # 
        self._BackColor = System.Drawing.Color.White
        self._Name = "XmlTreeView"
        self._Size = System.Drawing.Size(232, 321)
        self.ResumeLayout(False)

    def DeregisterEvents(self, control):
        if None != control:
            self._printMenuItem.Click -= control.Print
            self._searchMenuItem.Click -= control.Search
            self._deleteMenuItem.Click -= control.Delete
            self._saveMenuItem.Click -= control.Save
            self._pasteMenuItem.Click -= control.Paste
            self._copyMenuItem.Click -= control.Copy
            self._editMenuItem.Click -= control.Edit
            self._printDialog1.Document.PrintPage -= control.PrintPage

    def RegisterEvents(self, control):
        if None != control:
            self._printMenuItem.Click += control.Print
            self._searchMenuItem.Click += control.Search
            self._deleteMenuItem.Click += control.Delete
            self._saveMenuItem.Click += control.Save
            self._pasteMenuItem.Click += control.Paste
            self._copyMenuItem.Click += control.Copy
            self._editMenuItem.Click += control.Edit
            self._printDialog1.Document = control.PrintDocument
            self._printDialog1.Document.PrintPage += control.PrintPage

    def form_Closing(self, sender, e):
        if not self._content.Equals(Xml):
            if MessageBox.Show("Save changes?", XmlTreeView.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes:
                if self._saveFileDialog1.ShowDialog() == DialogResult.OK:
 # <summary>
    # Sets or gets XML-data in a string form.
    # </summary>
    def get_Xml(self):
        result = str.Empty
        if Controls.Count > 0:
            result = (Controls[0]).Xml
        return result

    def set_Xml(self, value):
        Controls.Clear()
        try:
            self.DeregisterEvents(self._xmlControl)
            self._xmlControl = InnerXmlTreeView(self)
            self._xmlControl.Xml = value
            self._content = self._xmlControl.Xml
            self.RegisterEvents(self._xmlControl)
        except , :
            Controls.Clear()
            self.DeregisterEvents(self._xmlControl)
            self._xmlControl = InnerTextBox(self)
            self._xmlControl.Xml = value
            self.RegisterEvents(self._xmlControl)
        finally:
        (self._xmlControl).ContextMenu = self._popUpMenu
        Controls.Add(self._xmlControl)

    Xml = property(fget=get_Xml, fset=set_Xml)

    # <summary>
    # Sets or gets the fully qualified path to a file containing XML-data.
    # </summary>
    def get_XmlFile(self):
        if self._fileXml != self.Xml:
            self._filePath = str.Empty
        return self._filePath

    def set_XmlFile(self, value):
        if None != value and value.Length > 0:

    XmlFile = property(fget=get_XmlFile, fset=set_XmlFile)

    def get_PrintDialog(self):
        return self._printDialog1

    PrintDialog = property(fget=get_PrintDialog)

    def get_SaveDialog(self):
        return self._saveFileDialog1

    SaveDialog = property(fget=get_SaveDialog)

    def get_LabelEdit(self):
        # <summary>
        # 
        # </summary>
        return self._deleteMenuItem.Enabled and self._editMenuItem.Enabled

    def set_LabelEdit(self, value):
        self._editMenuItem.Enabled = self._deleteMenuItem.Enabled = value

    LabelEdit = property(fget=get_LabelEdit, fset=set_LabelEdit)