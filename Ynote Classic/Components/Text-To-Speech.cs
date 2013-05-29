
namespace SS.Ynote.Classic
{
    using System.Drawing;
    using System.Speech.Synthesis;
    using System.IO;

    public partial class Text_To_Speech : System.Windows.Forms.Form
    {
        SpeechSynthesizer Reader = new SpeechSynthesizer();
        string TextToSpeak = "No Reference Set";
        static SolidBrush b = new SolidBrush(Color.FromArgb(60,0,0,255));
        FastColoredTextBoxNS.TextStyle Highlightstyle = new FastColoredTextBoxNS.TextStyle(null, b, FontStyle.Regular);
        public Text_To_Speech()
        {
            InitializeComponent();
            Reader.SpeakProgress += new System.EventHandler<SpeakProgressEventArgs>(Reader_SpeakProgress);
            Reader.SpeakCompleted += new System.EventHandler<SpeakCompletedEventArgs>(Reader_SpeakCompleted);
            comboBox1.SelectedIndex = 0;
        }
         string GetText()
        {
            return textBox1.Text;
        }
        public string SpeakingText
        {
            get { return TextToSpeak; }
            set { TextToSpeak = value; }
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            Reader.Volume = trackBar1.Value;
            Reader.Rate = trackBar2.Value;
            comboBox1_SelectedIndexChanged(sender, e);
            Reader.SpeakAsync(textBox1.Text);
            btnstart.Enabled = false;
            btnpause.Enabled = true;
        }
        private void Reader_SpeakProgress(object sender, SpeakProgressEventArgs e)
        {
            this.textBox1.Range.ClearStyle(Highlightstyle);
            this.textBox1.Range.SetStyle(Highlightstyle, e.Text, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "wav files (*.wav)|*.wav";
            sfd.Title = "Save to a wave file";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
               FileStream fs = new FileStream(sfd.FileName,FileMode.Create,FileAccess.Write);
               Reader.SetOutputToWaveStream(fs);
               Reader.Speak(this.textBox1.Text);
               fs.Close();
                }
        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            Reader.Volume = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, System.EventArgs e)
        {
            Reader.Rate = trackBar2.Value;
        }
        private void Reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e){
            btnstart.Enabled = true;
            btnpause.Enabled = false;
            btnresume.Enabled = false;
            this.textBox1.Range.ClearStyle(Highlightstyle);
            }
        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
          
               if(comboBox1.SelectedIndex == 0)
                    Reader.SelectVoiceByHints(VoiceGender.Male);
               if(comboBox1.SelectedIndex == 1)
                    Reader.SelectVoiceByHints(VoiceGender.Female);
                if(comboBox1.SelectedIndex == 2)
                    Reader.SelectVoiceByHints(VoiceGender.Neutral);
                if(comboBox1.SelectedIndex == 3)
                    Reader.SelectVoiceByHints(VoiceGender.NotSet);
            }

        private void Text_To_Speech_Load(object sender, System.EventArgs e)
        {
            this.textBox1.Text = this.SpeakingText;
        }

        private void btnpause_Click(object sender, System.EventArgs e)
        {
            if (Reader != null)
            {
                if (Reader.State == SynthesizerState.Speaking)
                {
                    Reader.Pause();
                    btnresume.Enabled = true;
                    btnpause.Enabled = false;
                }
            }
            
        }

        private void btnresume_Click(object sender, System.EventArgs e)
        {
            if (Reader != null)
            {
                if (Reader.State == SynthesizerState.Paused)
                {
                    Reader.Resume();
                    btnresume.Enabled = false;
                    btnpause.Enabled = true;
                }
                
            }
        }

        private void btnStop_Click(object sender, System.EventArgs e)
        {
            Close();
        }

       }
    }

