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
        private string FILES_DIRECTORY = Settings.Default.FIELS_DIRECTORY;
        private CultureInfo english = new CultureInfo("en-US");
        private CultureInfo hebrew = new CultureInfo("he-IL");
        private ParagraphProperties m_ParagraphProperties = new ParagraphProperties();
        private const string OPEN_BOLD = "b";
        private const string CLOSE_BOLD = "/b";
        private const string OPEN_PARAGRAPH = "p";
        private const string CLOSE_PARAGRAPH = "/p";
        private const string FONT_SIZE = "font-size";
        private const string COLOR = "color";
        private const string ALIGNMENT = "align";
        private const string PIXELS = "px;";
        private const char POINTS = ':';
        private const char SEMICOLON = ';';
        private const char QUOTATION_MARKS = '\"';
        private const string RIGHT_ALIGNMENT = "right";
        private const char OPEN_COMPLEX_PARAGRAPH = 'p';


        public string CreateTemplateInWord(string iTamplateName, string iTemlateContent)
        {
            try
            {
                createDirectory();
                return createDocumentFromTemplate(iTamplateName, iTemlateContent);
            }
            catch (Exception e)
            {
                throw new Exception($"Error during CreateTemplateInWord: {e.Message}");
            }
            
            //return getDefaultDocxLink(iTamplateName);
        }

        private void createDirectory()
        {
            bool exists = Directory.Exists(@FILES_DIRECTORY);
            if (!exists)
            {
                Directory.CreateDirectory(@FILES_DIRECTORY);
            }
        }

        private string createDocumentFromTemplate(string iTamplateName, string iTemlateContent)
        {
            string fileName = @FILES_DIRECTORY + $"/{iTamplateName}.docx";
            DocX doc = DocX.Create(fileName);

            Paragraph paragraph = doc.InsertParagraph();
            bool isFirst = true;
            string appendContent = "";
            string templateCopy = iTemlateContent;
            int index = 0;
            int remainingSize = iTemlateContent.Length;
            while (remainingSize != 0)
            {
                int indexOfStart = templateCopy.IndexOf('<');
                if (indexOfStart == 0)
                {
                    int indexOfClose = templateCopy.IndexOf('>');
                    string properties = templateCopy.Substring(indexOfStart + 1, indexOfClose - indexOfStart - 1);
                    switch (properties)
                    {
                        case OPEN_BOLD:
                        {
                            m_ParagraphProperties.IsBold = true;
                            break;
                        }
                        case CLOSE_BOLD:
                        {
                            m_ParagraphProperties.IsBold = false;
                            break;
                        }
                        case OPEN_PARAGRAPH:
                        {
                            createNewParagrapg(ref doc, ref paragraph, ref isFirst);
                            break;
                        }
                        case CLOSE_PARAGRAPH:
                        {
                            createNewParagrapg(ref doc, ref paragraph, ref isFirst);
                            break;
                        }
                        default:
                        {
                            updateParagraphProperties(properties, ref isFirst);
                            break;
                        }
                    }

                    index = indexOfClose + 1;
                }
                else
                {
                    appendContent = templateCopy.Substring(0, indexOfStart);
                    finishAppend(ref paragraph, appendContent);
                    index = indexOfStart;
                }

                remainingSize = remainingSize - index;
                templateCopy = templateCopy.Substring(index, remainingSize);
            }

            // Save to the output directory:
            doc.Save();
            string serverIp = getLocalIPAddress();
            string newValue = Path.GetFullPath(fileName).Replace(UNNECESSARY_PATH, serverIp);
            return newValue;
        }

        private void updateParagraphProperties(string i_StringProperties, ref bool i_IsFirst)
        {
            if (i_StringProperties[0].Equals(OPEN_COMPLEX_PARAGRAPH))
            {
                i_IsFirst = false;
                updateAlignment(i_StringProperties);
                updateColor(i_StringProperties);
                updateFontSize(i_StringProperties);
            }
        }

        private void updateFontSize(string i_StringProperties)
        {
            int indexOfFontSize = i_StringProperties.IndexOf(FONT_SIZE);
            if (indexOfFontSize != -1)
            {
                string afterFontSize = i_StringProperties.Substring(indexOfFontSize);
                int indexFirst = afterFontSize.IndexOf(POINTS);
                string cutOne = afterFontSize.Substring(indexFirst + 1);
                int indexLast = cutOne.IndexOf(PIXELS);
                string fontSizeValue = afterFontSize.Substring(indexFirst + 1, indexLast);
                try
                {
                    m_ParagraphProperties.FontSize = int.Parse(fontSizeValue);
                }
                catch (Exception e)
                {
                    throw new Exception($"Error during parse font size {e.Message}");
                }
            }
        }

        private void updateColor(string i_StringProperties)
        {
            int indexOfColor = i_StringProperties.IndexOf(COLOR);
            if (indexOfColor != -1)
            {
                string afterColor = i_StringProperties.Substring(indexOfColor);
                int indexFirst = afterColor.IndexOf(POINTS);
                string cutOne = afterColor.Substring(indexFirst + 1);
                int indexLast = cutOne.IndexOf(SEMICOLON);
                string colorValue = afterColor.Substring(indexFirst + 1, indexLast);
                m_ParagraphProperties.TextColor = Color.FromName(colorValue);
            }
        }

        private void updateAlignment(string i_StringProperties)
        {
            int indexOfAlign = i_StringProperties.IndexOf(ALIGNMENT);
            if (indexOfAlign != -1)
            {
                string afterAlign = i_StringProperties.Substring(indexOfAlign);
                int indexFirst = afterAlign.IndexOf(QUOTATION_MARKS);
                string cutOne = afterAlign.Substring(indexFirst + 1);
                int indexLast = cutOne.IndexOf(QUOTATION_MARKS);
                string alignValue = afterAlign.Substring(indexFirst + 1, indexLast);
                if (alignValue.Equals(RIGHT_ALIGNMENT))
                {
                    m_ParagraphProperties.IsRight = true;
                }
                else
                {
                    m_ParagraphProperties.IsRight = false;
                }
            }
        }

        private void createNewParagrapg(ref DocX i_Doc, ref Paragraph i_Paragraph, ref bool i_IsFirst)
        {
            if (!i_IsFirst)
            {
                i_Paragraph = i_Doc.InsertParagraph();
            }
            else
            {
                i_IsFirst = false;
            }
        }

        private void finishAppend(ref Paragraph i_Paragraph, string i_Content)
        {
            i_Paragraph.Append(i_Content);
            i_Paragraph.Color(m_ParagraphProperties.TextColor);
            i_Paragraph.FontSize(m_ParagraphProperties.FontSize);

            if (m_ParagraphProperties.IsBold)
            {
                i_Paragraph.Bold();
            }

            if (m_ParagraphProperties.IsRight)
            {
                i_Paragraph.Culture(hebrew);
                i_Paragraph.Alignment = Alignment.right;
            }

            else
            {
                i_Paragraph.Culture(english);
                i_Paragraph.Alignment = Alignment.left;
            }
        }

        [Obsolete]
        //This function is for tests only
        private string getDefaultDocxLink(string iTamplateName)
        {
            string fileName = @FILES_DIRECTORY + $"/{iTamplateName}.docx";
            DocX doc = DocX.Create(fileName);

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
            p1.Append(" כהן.");
            p1.FontSize(18);
            p1.Culture(hebrew);
            p1.Bold();
            p1.Color(Color.Blue);


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

        private class ParagraphProperties
        {
            public bool IsBold { get; set; } = false;
            public bool IsRight { get; set; } = false;
            public Color TextColor { get; set; } = Color.Black;
            public int FontSize { get; set; } = 12;
        }
    }
}