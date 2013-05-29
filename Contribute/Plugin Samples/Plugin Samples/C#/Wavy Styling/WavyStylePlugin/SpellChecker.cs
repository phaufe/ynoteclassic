using System.IO;
using System.Drawing;
using FastColoredTextBoxNS;
using System.Collections.Generic;

namespace WavyStylePlugin
{
    public class SpellChecker
    {
        private Style wavyStyle = new WavyLineStyle(255, Color.Red);
        private HashSet<string> words;
        private FastColoredTextBox tb;
        private string Dictionary;
        public SpellChecker(FastColoredTextBox fctb, string dictionarypath)
        {
            var List = File.ReadAllLines(dictionarypath);
            DictionaryPath = dictionarypath;
            words = new HashSet<string>(List, System.StringComparer.InvariantCultureIgnoreCase);
            this.tb = fctb;
            fctb = this.tb;
            this.tb.TextChangedDelayed += new System.EventHandler<TextChangedEventArgs>(tb_TextChangedDelayed);
        }
        public string DictionaryPath
        {
            get { return Dictionary;}
            set { Dictionary = value; }
        }
        public void SpellCheck(object sender)
        {
            tb_TextChangedDelayed(sender, new TextChangedEventArgs(this.tb.Range));
        }
        private void tb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(
                (s) =>
                {
                    var list = new List<FastColoredTextBoxNS.Range>();
                    foreach (var word in e.ChangedRange.GetRanges(@"[\w']+"))
                        if (!words.Contains(word.Text))
                            list.Add(word);
                    //
                    e.ChangedRange.ClearStyle(wavyStyle);
                    foreach (var word in list)
                        word.SetStyle(wavyStyle);
                });
        }
        public void Clear()
        {
            this.tb.Range.ClearStyle(wavyStyle);
        }
    }
}
