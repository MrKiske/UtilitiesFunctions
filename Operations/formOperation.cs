using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Operations.Class;

namespace Operations
{
    public partial class formOperation : Form
    {

        private const string JSONPATH = @"C:\Test";
        public formOperation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utils obj = new Utils();

            try
            {
                string[] files = Directory.GetFiles(JSONPATH, "*.json", SearchOption.AllDirectories);

                if (files.Length != 0)
                {
                    foreach (string file in files)
                    {
                        //Get information about file
                        FileInfo info = new FileInfo(file);

                        //Read json file
                        StreamReader json = new StreamReader(file);

                        // Parse json string into JObject.
                        var parsedObject = JObject.Parse(json.ReadToEnd());

                        // Sort properties of JObject.
                        var normalizedObject = obj.SortPropertiesAlphabetically(parsedObject);

                        //Create new Json
                        StreamWriter newJson = new StreamWriter(System.IO.Path.Combine(info.Directory.FullName, $"{info.Name}_Sort"));

                        //Write sort content alphabetical 
                        newJson.Write(normalizedObject);

                        //Approve write
                        newJson.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}. {ex.StackTrace}", $"{this.GetType().FullName}");
            }
        }
    }
}
