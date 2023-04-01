using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Text;
using System.Xml;

namespace iTunesCore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_ClickAsync(object sender, EventArgs e)
        {
            label1.Visible = false;
            label1.Text += "Total tracks:";

            openFileDialog1.FileName = "Select an XML playlist";
            openFileDialog1.Filter = "XML Playlist files (*.xml)|*.xml";
            openFileDialog1.Title = "Open playlist";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    var output = new StringBuilder();
                    Stream? stream = null;
                    ProcessNodes nodes = new ProcessNodes();

                    if ((stream = openFileDialog1.OpenFile()) != null)
                    {
                        nodes.GetXmlDocument(stream, output);

                        var xmldoc = new XmlDocument();
                        xmldoc.LoadXml(output.ToString());
                        var counter = 1;
                        var model = new List<Track>();

                        foreach (XmlNode node in xmldoc.SelectNodes("//plist//dict/dict"))
                        {
                            var track = nodes.ParseTrack(node);
                            model.Add(track);

                            treeView1.Nodes.Add(counter++ + " " + track.Artist + " - " + track.Name);
                        }

                        label1.Text += " " + model.Count;
                        label1.Visible = true;
                    }
                }
                catch (XmlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}