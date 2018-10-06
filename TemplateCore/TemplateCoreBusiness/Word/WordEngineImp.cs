using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using TemplateCoreBusiness.Properties;
using Xceed.Words.NET;
using Font = Xceed.Words.NET.Font;

namespace TemplateCoreBusiness.Word
{
    public class WordEngineImp : IWordEngine
    {
        private const string UNNECESSARY_PATH = "C:\\inetpub\\wwwroot";

        //TODO: implement Docx from real db template and not from default
        public string createTemplateInWord(string iTamplateName, string iTemlateContent)
        {
            return getDefaultDocxLink(iTamplateName);
        }

        [Obsolete]
        //This function is for tests only
        private string getDefaultDocxLink(string iTamplateName)
        {
            string fielsDirectory = Settings.Default.FIELS_DIRECTORY;
            bool exists = Directory.Exists(@fielsDirectory);
            if (!exists)
            {
                Directory.CreateDirectory(@fielsDirectory);
            }

            string fileName = @fielsDirectory + $"/{iTamplateName}.docx";
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
            string serverIp = getLocalIPAddress();
            string newValue = Path.GetFullPath(fileName).Replace(UNNECESSARY_PATH, serverIp);
            return newValue;
        }

        private string getLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
