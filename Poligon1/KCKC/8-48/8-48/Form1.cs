using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio.Wave;
using NAudio.Mixer;

using System.IO;


using CUETools.Codecs;
using CUETools.Codecs.FLAKE;

using Newtonsoft;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;
using System.Threading;



namespace _8_48
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartBtn.Enabled = false;
            StopBtn.Enabled = true;
            ExecuteBtn.Enabled = false;

            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveFile = new WaveFileWriter(@"lol.wav", waveSource.WaveFormat);

            try
            {
                waveSource.StartRecording();
            }
            catch (NAudio.MmException)
            {
                richTextBox1.Text = "\nPodlacz mikrofon!!!\nZrestartuj aplikacje.";
                StartBtn.Enabled = false;
                StopBtn.Enabled = false;
                ExecuteBtn.Enabled = false;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopBtn.Enabled = false;
           

            waveSource.StopRecording();
            Thread.Sleep(1500);
            ExecuteBtn.Enabled = true;
            StopBtn.Enabled = false;
        }


        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }

            StartBtn.Enabled = true;
        }

        public WaveIn waveSource { get; set; }

        public WaveFileWriter waveFile { get; set; }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (var stream = new FileStream(@"lol.wav", FileMode.Open))
            {
                ConvertToFlac(stream);
                GoogleSpeech();
            }

            JarExecute();
        }

        private void ConvertToFlac(Stream sourceStream)
        {
          
            var audioSource = new WAVReader(null, sourceStream);
            try
            {
                if (audioSource.PCM.SampleRate != 44100)
                {
                    throw new InvalidOperationException("Incorrect frequency - WAV file must be at 44.1 KHz.");
                }
                var buff = new AudioBuffer(audioSource, 0x10000);
                var flakeWriter = new FlakeWriter( @"lol.flac",audioSource.PCM);
                flakeWriter.CompressionLevel = 8;
                while (audioSource.Read(buff, -1) != 0)
                {
                    flakeWriter.Write(buff);
                }
                flakeWriter.Close();
            }
            finally
            {
                audioSource.Close();
                //richTextBox1.Text += "Koniec Konwersji\n";
            }
        }

         public void GoogleSpeech()
        {
            var result = "";
           // richTextBox1.Text += "Jestem w google\n";
            try
            {
                
                FileStream fileStream = File.OpenRead("lol.flac");
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.SetLength(fileStream.Length);
                fileStream.Read(memoryStream.GetBuffer(), 0, (int)fileStream.Length);
                byte[] BA_AudioFile = memoryStream.GetBuffer();
                HttpWebRequest _HWR_SpeechToText = null;
                _HWR_SpeechToText =
                            (HttpWebRequest)HttpWebRequest.Create(
                                "[TUTAJ KLUCZ]");
                _HWR_SpeechToText.Credentials = CredentialCache.DefaultCredentials;
                _HWR_SpeechToText.Method = "POST";
                _HWR_SpeechToText.ContentType = "audio/x-flac; rate=44100"; //"audio/x-flac; rate=44100";
                _HWR_SpeechToText.ContentLength = BA_AudioFile.Length;
                Stream stream = _HWR_SpeechToText.GetRequestStream();
                stream.Write(BA_AudioFile, 0, BA_AudioFile.Length);
                stream.Close();

                HttpWebResponse HWR_Response = (HttpWebResponse)_HWR_SpeechToText.GetResponse();
                if (HWR_Response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader SR_Response = new StreamReader(HWR_Response.GetResponseStream());
                    result = SR_Response.ReadToEnd();
                    //richTextBox1.Text += result.ToString();
                    Console.WriteLine("\n" + result);

                }
               
                // result =
                // "{\"result\":[]}\n{\"result\":[{\"alternative\":[{\"transcript\":\"good morning Google how are you feeling today\",\"confidence\":0.93832707},{\"transcript\":\"goodmorning Google how are you feeling today\"},{\"transcript\":\"Good Morning Google how are you feeling today\"},{\"transcript\":\"good mornin Google how are you feeling today\"},{\"transcript\":\"good mourning Google how are you feeling today\"}],\"final\":true}],\"result_index\":0}\n";

                var jsons = result.Split('\n');



                foreach (var j in jsons)
                {
                    dynamic jsonObject = JsonConvert.DeserializeObject(j);
                    if (jsonObject == null || jsonObject.result.Count <= 0) continue;

                    var text = jsonObject.result[0].alternative[0].transcript;
                    richTextBox1.Text += "\n" + text;
                    /////Wpisywanie do pliku txt

                    string lines = text;
                    lines = UsuwaniePolskichZnakow(lines);
                    System.IO.StreamWriter file = new System.IO.StreamWriter("komendy.txt");
                    file.WriteLine(lines);
                    file.Close();


                    ///////Koniec wpisywania
                    Console.WriteLine(text);
                   
                    
                }

                fileStream.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }

         public void JarExecute()
         {
             System.Diagnostics.Process.Start(@"C:\Users\aleks_000\Desktop\KCKC\8-48\8-48\bin\Debug\MovmentInterpreter[v.1.0].jar");
             Console.WriteLine("Jar Executed\n");
         }
        
        public static string UsuwaniePolskichZnakowWolne(string t)
         {
             return t.Replace("ą", "a")

                .Replace("Ą", "A")

                .Replace("ę", "e")

                .Replace("Ę", "E")

                .Replace("ś", "s")

                .Replace("Ś", "S")

                .Replace("ż", "z")

                .Replace("Ż", "Z")

                .Replace("ć", "c")

                .Replace("Ć", "C")

                .Replace("ź", "z")

                .Replace("Ź", "Z")

                .Replace("ń", "n")

                .Replace("Ń", "N")

                .Replace("ó", "o")

                .Replace("Ó", "O")

                .Replace("ł", "l")

                .Replace("Ł", "L");

         }
        /**
         *  Dla 100 000 operacji i ciągu 100 znakowego: StringBuilder.Replace - 00:00:00.4480256, 
         *  string.Replace - 00:00:00.5490314
         **/
        public static string UsuwaniePolskichZnakow(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace('ą', 'a')

              .Replace('ć', 'c')

              .Replace('ę', 'e')

              .Replace('ł', 'l')

              .Replace('ń', 'n')

              .Replace('ó', 'o')

              .Replace('ś', 's')

              .Replace('ż', 'z')

              .Replace('ź', 'z')

              .Replace('Ą', 'A')

              .Replace('Ć', 'C')

              .Replace('Ę', 'E')

              .Replace('Ł', 'L')

              .Replace('Ń', 'N')

              .Replace('Ó', 'O')

              .Replace('Ś', 'S')

              .Replace('Ż', 'Z')

              .Replace('Ź', 'Z');

            var wynik = sb.ToString();
            return wynik;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        


    }
}
