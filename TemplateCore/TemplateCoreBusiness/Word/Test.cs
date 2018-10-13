using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TemplateCoreBusiness.Properties;
using Xceed.Words.NET;
using Font = Xceed.Words.NET.Font;

namespace TemplateCoreBusiness.Word
{
    public class Test
    {
        public static void BasicTest()
        {
            string jsonValue = "{\"values\": [2,3,4,5], \"Template\": \" this is a big \nstring <value0> contains <value1> some data\", \"numberOfChanges\": 2}";

            var obj = JsonConvert.DeserializeObject<MyObject>(jsonValue);

            Console.WriteLine("template: " + obj.Template);
            Console.WriteLine("the list is: ");
            foreach (var item in obj.values)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            for (int i = 0; i < obj.numberOfChanges; i++)
            {
                string newVal = obj.Template.Replace("<value" + i + ">", obj.values[i].ToString());
                obj.Template = newVal;
            }

            Console.WriteLine();

            obj.values.Add(10);

            string ser = JsonConvert.SerializeObject(obj);
            Console.WriteLine("this is the serilized obj: \n" + ser);

            Console.WriteLine("template: " + obj.Template);

            string fielsDirectory = Settings.Default.FIELS_DIRECTORY_NAME;
            bool exists = Directory.Exists(@fielsDirectory);
            if (!exists)
            {
                Directory.CreateDirectory(@fielsDirectory);
            }

            string fileName = @fielsDirectory + "/DocXExample.docx";
            DocX doc = DocX.Create(fileName);
            CultureInfo english = new CultureInfo("en-US");
            CultureInfo hebrew = new CultureInfo("he-IL");
            string headlineText = "Example";

            
            // A formatting object for our headline:
            var headLineFormat = new Xceed.Words.NET.Formatting();
            headLineFormat.FontFamily = new Font("Arial Black");
            headLineFormat.FontColor = Color.Red;
            headLineFormat.Size = 18D;
            headLineFormat.Position = 12;
            headLineFormat.Language = english;

            doc.InsertParagraph(headlineText, false, headLineFormat);

            // A formatting object for our normal paragraph text:
            Paragraph p1 = doc.InsertParagraph();
            p1.Alignment = Alignment.right;
            p1.Append("אור הורוביץ\n");
            p1.Append(" וטל");
            p1.FontSize(18);
            p1.Culture(hebrew);
            p1.Bold();
            p1.Color(Color.Blue);
            p1.Append(" כהן.");

            Paragraph p2 = doc.InsertParagraph();
            p2.Alignment = Alignment.left;
            p2.AppendLine("Can you help \nme figure it out?");
            p2.Culture(english);

            Console.WriteLine();
            
            var paraFormat = new Xceed.Words.NET.Formatting();
            paraFormat.FontFamily = new Font("Calibri");
            paraFormat.Size = 10D;
            paraFormat.Bold = true;
            paraFormat.UnderlineStyle = UnderlineStyle.singleLine;
            

            // Insert the now text obejcts;
            //doc.InsertParagraph(obj.Template, false, paraFormat);
            //doc.InsertParagraph("or and tal", false, paraFormat);

            // Save to the output directory:
            doc.Save();
            Byte[] docxInByteArray = File.ReadAllBytes(fileName);

            // Open in Word:
            Process.Start("WINWORD.EXE", fileName);
            
            Console.ReadKey();
        }
    }

    class MyObject
    {
        public List<int> values { get; set; }
        public string Template { get; set; }
        public int numberOfChanges { get; set; }
    }
}
