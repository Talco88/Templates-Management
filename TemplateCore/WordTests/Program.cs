using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Word;
using TemplateCoreBusiness.Database;
using TemplateCoreBusiness.Engine;
using TemplateCoreBusiness.Models;

namespace WordTests
{
    class Program
    {
        const string birthday = "{\"Template\": \"<p  align=''left'' style=''font-size:20px; color:green;''><b>To: </b>$Name </p><p align=''left'' style=''font-size:16px; color:blueViolet;''><b>DATE: </b> $Date </p><p align=''left'' style=''font-size:12px; color:black; ''>So what if you are getting older</br>There are worse things to be,Like a goofy, pimply teenager</br> With zero self-esteem.</br>So what if you got a few wrinkles</br>There are worse things to have</br>Like a case of the twenty somethings</br>And a room at Mom and Dad</br>To me, you are simply wonderful.</br>Everything about you is just right</br>So have a happy birthday</br>And lets party all night</br></p><p align =''left'' style=''font-size:20px; color:red;''><b>Yours:</br></b>$From </p>\", \"numberOfChanges\": 3}";
        const string birthdayBoss = "{\"Template\": \"<p  align=''left'' style=''font-size:30px; color:pink;''><b>To: </b>$Name </p><p align=''left'' style=''font-size:20px; color:red;''><b>DATE: </b> $Date </p><p align=''left'' style=''font-size:18px; color:black; ''>Happy Birthday, to  <b> my boss!</b> </br>You are always professional towards your employees! </br> I hope your special day goes well!.</br>I am so blessed to have a kindhearted boss like you. Your invaluable contributions in my life are fully appreciated.</br></p><p align =''left'' style=''font-size:20px; color:red;''><b>from:</br></b>$From </p>\", \"numberOfChanges\": 3}";
        const string Bar_Mitzvah_Wishes = "{\"Template\": \"<p  align=''left'' style=''font-size:25px; color:blue;''><b>Congratulations </b>$Name </p><p align=''left'' style=''font-size:20px; color:purple;''></p><p align=''left'' style=''font-size:20px; color:black; ''>Mazel tov on your bar mitzvah!</br>What a milestone! Enjoy being celebrated today — you deserve it! </br>Wishing you lots of happiness as you enter adulthood. </br>Be proud of yourself today!</br>Thank you for inviting me to celebrate this important time with you. Congratulations! </br>Let the Torah inspire you and guide you through this beautiful life.</br>Wishing you blessings and joy as you step into adulthood</br></p><p align =''left'' style=''font-size:20px; color:red;''><b>love:</br></b>$From </p>\", \"numberOfChanges\": 2}";
        const string Name_Change_Announcement = "{\"Template\": \"<p  align=''left'' style=''font-size:25px; color:blue;''><b>Name Change Announcement </b> </p><p align=''left'' style=''font-size:20px; color:black;''></p><p align=''left'' style=''font-size:20px; color:black; ''>Dear all,</br>I hope you are all well. I am writing because I have updated my contact information to reflect my recent name change from <b>$Old Name </b> to <b>$New Name </b>  </br>I would like to make sure that we remain in touch, so please take a few minutes to update my information, as I will no longer be using this account after  <b>$Date </b> </br>Best regards,</br></p><p align =''left'' style=''font-size:20px; color:red;''><b>NAME:</br></b>$New Name </p><p align =''left'' style=''font-size:20px; color:red;''><b>Cell: </br></b>$Cell </p>\", \"numberOfChanges\": 4}";
        static void Main(string[] args)
        {
            //Test.BasicTest();

            try
            {
                //INSERT
                //CreateAdminUsers();
                //CreateTopics();
                string jsonValue = Name_Change_Announcement;
                AppEngineBuilder.GetAppEngine().CreateNewTemplate(jsonValue, "Name Change Announcement", "shani", "Buissness");
                //string jsonValue = "{\"Template\": \" this is a $firstName $lastName \n $FriendFirstName $FriendLastName\", \"numberOfChanges\": 4}";
                //AppEngineBuilder.GetAppEngine().CreateNewTemplate(jsonValue, "יום הולדת", "orho@gmail.com", "ברכות");
                /*
                AppEngineBuilder.GetAppEngine().CreateNewTemplate("טל", "שלטים", "talCo@gmail.com", "כללי");
                
                //Console.WriteLine();
                //Console.WriteLine(UserEngineBuilder.GetUserEngine().RegisterNewUser("Guy", "tvil", "0542021405", "guy@gmail.com"));
                */

                //SELECT
                //UserEntity userInformation = UserEngineBuilder.GetUserEngine().LogInUser("chen@gmail.com", "03021991");
                //Console.WriteLine();
                /*
                List<string> templateNames = AppEngineBuilder.GetAppEngine().GetTemplateFromSearch("שלט",false, "orchen@gmail.com");
                Console.WriteLine();
                
                List<TopicEntity> listEntities = AppEngineBuilder.GetAppEngine().GetAllTopics();
                Console.WriteLine();

                List<string> headersList = AppEngineBuilder.GetAppEngine().GetTopicsInCategory("מסמכים");
                Console.WriteLine();

                List<string> topicsList = AppEngineBuilder.GetAppEngine().GetTopicsNames();
                Console.WriteLine();
                 

                UserEntity userInformation = UserEngineBuilder.GetUserEngine().GetUserData("chen@gmail.com");
                Console.WriteLine();
                */

                //DELETE
                /*
                Console.WriteLine(AppEngineBuilder.GetAppEngine().DeleteTemplate("כללי", "שלט חוצות", "chen@gmail.com"));
                Console.WriteLine();
                
                Console.WriteLine(AppEngineBuilder.GetAppEngine().DeleteTopic("ברכות", "בר מצווה"));
                Console.WriteLine();
                

                Console.WriteLine(DataBaseFactory.GetDbInstance().DeleteAllTable("Topic"));
                Console.WriteLine();
                */

                //Update
                //Console.WriteLine(AppEngineBuilder.GetAppEngine().GenerateHTMLTemplateWithValues(CreateTemplateFormation()));
                //Console.WriteLine();

                //TemplateFormation templateFormation = AppEngineBuilder.GetAppEngine().GetTemplate("ברכות", "יום הולדת");
                //Console.WriteLine();
                /*
                //Console.WriteLine(AppEngineBuilder.GetAppEngine().RateTamplate("כללי", "טלוויזיה", 2));
                //Console.WriteLine();

                //Console.WriteLine(AppEngineBuilder.GetAppEngine().AddCommentToTemplate("כללי", "טלוויזיה", "chen@gmail.com", "ממליצה בחום"));
                //Console.WriteLine();
                
                Console.WriteLine(AppEngineBuilder.GetAppEngine().AddCommentToTemplate("כללי", "טלוויזיה", "orho@gmail.com", "אכן template מעולה"));
                Console.WriteLine();
                
                //Console.WriteLine(AppEngineBuilder.GetAppEngine().MarkTemplateAsFavorite("כללי", "טלוויזיה", "orho@gmail.com"));
                //Console.WriteLine();

                Console.WriteLine(AppEngineBuilder.GetAppEngine().RemoveMarkTemplateAsFavorite("כללי", "טלוויזיה", "orho@gmail.com"));
                Console.WriteLine();
                 
                Console.WriteLine(AppEngineBuilder.GetAppEngine().SetSharedTemplate("כללי", "שלט חוצות", "chen@gmail.com", true));
                Console.WriteLine();
                */

                //Office
                //Console.WriteLine(AppEngineBuilder.GetAppEngine().OpenTemplateInWord("<p  align=\"right\" style=\"font-size:20px; color:green;\">אני <b>ושני </b>גדולים.</p><p align=\"left\" style=\"font-size:16px; color:blueViolet;\"><b>This text</b> is <b>bold or.</b></p><p align=\"left\" style=\"font-size:12px; color:blue; \">YESSSSSSS\nYOOOOOOOOOHOOOOOOOO</p>", "shani"));
                //Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CreateTopics()
        {
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("Greetings", "Birthday"));
        }

        private static void CreateAdminUsers()
        {
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Or", "Horovitz", "orho@gmail.com", "25011991", true));
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Tal", "cohen", "talCo@gmail.com", "1234", true));
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Shani", "Somech", "Shani@gmail.com", "1245", true));
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Nati", "Lehrer", "Nati@gmail.com", "1246", true));
        }

        private static TemplateFormation CreateTemplateFormation()
        {
            TemplateFormation retVal = new TemplateFormation();
            retVal.CategoryName = "ברכות";
            retVal.HeaderName = "יום הולדת";
            retVal.Values = createWebDataContainerList();

            return retVal;
        }

        private static List<WebDataContainer> createWebDataContainerList()
        {
            List<WebDataContainer> retVal = new List<WebDataContainer>();
            WebDataContainer webDataContainer = new WebDataContainer();
            webDataContainer.Name = "firstName";
            webDataContainer.Value = "or";
            retVal.Add(webDataContainer);

            WebDataContainer webDataContainer1 = new WebDataContainer();
            webDataContainer1.Name = "lastName";
            webDataContainer1.Value = "horovitz";
            retVal.Add(webDataContainer1);

            WebDataContainer webDataContainer2 = new WebDataContainer();
            webDataContainer2.Name = "FriendFirstName";
            webDataContainer2.Value = "tal";
            retVal.Add(webDataContainer2);

            WebDataContainer webDataContainer3 = new WebDataContainer();
            webDataContainer3.Name = "FriendLastName";
            webDataContainer3.Value = "cohen";
            retVal.Add(webDataContainer3);

            return retVal;
        }
    }
}