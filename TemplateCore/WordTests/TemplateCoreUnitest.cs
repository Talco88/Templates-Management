using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TemplateCoreBusiness.Engine;

namespace WordTests
{
    internal class TemplateCoreUnitest
    {
        private string _TestBirthdayBoss = "{\"Template\": \"<p  align=''left'' style=''font-size:30px; color:pink;''><b>To: </b>$Name </p><p align=''left'' style=''font-size:20px; color:red;''><b>DATE: </b> $Date </p><p align=''left'' style=''font-size:18px; color:black; ''>Happy Birthday, to  <b> my boss!</b> </br>You are always professional towards your employees! </br> I hope your special day goes well!.</br>I am so blessed to have a kindhearted boss like you. Your invaluable contributions in my life are fully appreciated.</br></p><p align =''left'' style=''font-size:20px; color:red;''><b>from:</br></b>$From </p>\", \"numberOfChanges\": 3}";
        private string _TestTemplateName = "BirthdayTest";
        private string _TestCategory = "Greetings";
        private string _TestTemplateNameWord = "BirthdayTestWord";
        private string _succeededUpdateToDB = "The update to DB succeeded";

        private IUserEngine userEngine { get; }
        private IAppEngine appEngine { get; }

        private string _TestUserEmail { get; set; }

        //const string birthday = "{\"Template\": \"<p  align=''left'' style=''font-size:20px; color:green;''><b>To: </b>$Name </p><p align=''left'' style=''font-size:16px; color:blueViolet;''><b>DATE: </b> $Date </p><p align=''left'' style=''font-size:12px; color:black; ''>So what if you are getting older</br>There are worse things to be,Like a goofy, pimply teenager</br> With zero self-esteem.</br>So what if you got a few wrinkles</br>There are worse things to have</br>Like a case of the twenty somethings</br>And a room at Mom and Dad</br>To me, you are simply wonderful.</br>Everything about you is just right</br>So have a happy birthday</br>And lets party all night</br></p><p align =''left'' style=''font-size:20px; color:red;''><b>Yours:</br></b>$From </p>\", \"numberOfChanges\": 3}";
        //private string birthdayBoss = "{\"Template\": \"<p  align=''left'' style=''font-size:30px; color:pink;''><b>To: </b>$Name </p><p align=''left'' style=''font-size:20px; color:red;''><b>DATE: </b> $Date </p><p align=''left'' style=''font-size:18px; color:black; ''>Happy Birthday, to  <b> my boss!</b> </br>You are always professional towards your employees! </br> I hope your special day goes well!.</br>I am so blessed to have a kindhearted boss like you. Your invaluable contributions in my life are fully appreciated.</br></p><p align =''left'' style=''font-size:20px; color:red;''><b>from:</br></b>$From </p>\", \"numberOfChanges\": 3}";
        //const string Bar_Mitzvah_Wishes = "{\"Template\": \"<p  align=''left'' style=''font-size:25px; color:blue;''><b>Congratulations </b>$Name </p><p align=''left'' style=''font-size:20px; color:purple;''></p><p align=''left'' style=''font-size:20px; color:black; ''>Mazel tov on your bar mitzvah!</br>What a milestone! Enjoy being celebrated today — you deserve it! </br>Wishing you lots of happiness as you enter adulthood. </br>Be proud of yourself today!</br>Thank you for inviting me to celebrate this important time with you. Congratulations! </br>Let the Torah inspire you and guide you through this beautiful life.</br>Wishing you blessings and joy as you step into adulthood</br></p><p align =''left'' style=''font-size:20px; color:red;''><b>love:</br></b>$From </p>\", \"numberOfChanges\": 2}";
        //const string Name_Change_Announcement = "{\"Template\": \"<p  align=''left'' style=''font-size:25px; color:blue;''><b>Name Change Announcement </b> </p><p align=''left'' style=''font-size:20px; color:black;''></p><p align=''left'' style=''font-size:20px; color:black; ''>Dear all,</br>I hope you are all well. I am writing because I have updated my contact information to reflect my recent name change from <b>$Old Name </b> to <b>$New Name </b>  </br>I would like to make sure that we remain in touch, so please take a few minutes to update my information, as I will no longer be using this account after  <b>$Date </b> </br>Best regards,</br></p><p align =''left'' style=''font-size:20px; color:red;''><b>NAME:</br></b>$New Name </p><p align =''left'' style=''font-size:20px; color:red;''><b>Cell: </br></b>$Cell </p>\", \"numberOfChanges\": 4}";
        //const string Board_Resignation = "{\"Template\": \"<p align=''left'' style=''font-size:25px; color:black;''><b> Board Resignation</b> </p><p align=''left'' style=''font-size:20px; color:black;''></p><p align=''left'' style=''font-size:20px; color:black;''><b>Date: </b>$Date </br> <b>Address: </b>$Address </br><b>Dear, </b>$To </br> It is with regret that I am writing to inform you of my decision to resign my position on the Board of The Art Foundation, effective immediately.</br>My other commitments have become too great for me to be able to fulfill the requirements of my position on the Board,</br> and I feel it is best for me to make room for someone with the time and energy to devote to the job. </br>It has been a pleasure being a part of the Art Foundation board. I am so proud of all we have accomplished in the past five years,</br> and I have no doubt the board will continue these successes in the future. </br>If I can be of any assistance during the time it will take to fill the position, please do not hesitate to ask. </br></p><p align =''left'' style=''font-size:20px; color:green;''><b>Best regards,</br></b>$From </p>\", \"numberOfChanges\": 4}";
        //const string Request_to_Work_Remotely = "{\"Template\": \"<p  align=''left'' style=''font-size:25px; color:orange;''><b>Request to Work Remotely </br> Dear: </b>$Name </p><p align=''left'' style=''font-size:20px; color:black; ''>As you know, I have been working some hours from home on an occasional basis.</br> I have found that my productivity has increased, and I am able to focus well on my work activities without the distractions in the office.</br> Would it be possible for me to work from home regularly, meeting in the office on an as-needed basis? </br> I have really enjoyed working with you and your team, and look forward to our continued collaboration.</br> Thank you for very much for your consideration,</br></p><p align =''left'' style=''font-size:20px; color:orange;''><b>from </br></b>$From </p>\", \"numberOfChanges\": 2}";
        //const string Appreciation_to_a_Team_Member = "{\"Template\": \"<p  align=''left'' style=''font-size:20px; color:green;''><b>Appreciation Note to a Team Member </br> <b> Date: </b>$Date <b> Dear: </b>$Name </p><p align=''left'' style=''font-size:20px; color:black; ''>I really appreciate your taking the time to help us get started on our year-end inventory.</br> It was so helpful to have someone who has been through the whole process before to guide the newer employees through the procedure.</br> Your experience made everything go so much smoother, and we are already noting improved accuracy in our numbers due to your input.</br> </p><p align =''left'' style=''font-size:20px; color:orange;''><b>regards,</br></b>$From </p>\", \"numberOfChanges\": 3}";
        //const string appeal_on_a_parking = "{\"Template\": \"<p align=''left'' style=''font-size:20px; color:black;''><b>appeal on a parking report </br> <b> Date: </b>$Date </p><p align=''left'' style=''font-size:20px; color:black; ''>Dear Sir/Madam,</br> Re: Parking ticket <b>$Number of report </b></br> I was issued with a parking ticket for parking at street <b>$Street </b>  on date <b>$date of the report </b></br>I believe that this ticket was issued unfairly. I am not liable for the amount payable because: </br> There was insufficient signage. There was no sign at the entrance of the car park. </br>I enclose a photo of the entrance to the car park. It was taken on the day the ticket was issued. </br>It shows clearly that there was no sign about the parking charges. </br> </p><p align =''left'' style=''font-size:20px; color:orange;''><b>Yours faithfully,</br></b>$From </p>\", \"numberOfChanges\": 5}";

        public TemplateCoreUnitest()
        {
            userEngine = UserEngineBuilder.GetUserEngine();
            appEngine = AppEngineBuilder.GetAppEngine();
        }
        public void Start()
        {
            loginTest();
            createTemplateTest();
            getServerTemplateDetailsAfterCreation();
            generateTemplate();
            templateCommentsTest();
            addRateTest();
            favoriteTest();
            testDeleteTemplate();
            Console.ReadKey();
        }

        private void loginTest()
        {
            string nameFormat = DateTime.Now.ToString("yyMMddhhmm");
            string unitestPass = "112233";
            _TestUserEmail = $"unitestUser{nameFormat}@gmail.com";

            createNewUser(_TestUserEmail, unitestPass);
            loginWithUser(_TestUserEmail, unitestPass);
        }

        private void createNewUser(string iUnitestMail, string iUnitestPass)
        {
            string name = "uni";
            string lastName = "test";
            var userEntity = userEngine.RegisterNewUser(name, lastName, iUnitestMail, iUnitestPass, false);
            var result =
                (userEntity.Email == iUnitestMail && userEntity.Password == iUnitestPass &&
                 userEntity.FirstName == name && userEntity.LastName == lastName &&
                 string.IsNullOrEmpty(userEntity.FavoriteTemplates) && !userEntity.IsAdmin);

            string status = resultString(result);
            Console.WriteLine($"[Create New User] : {status}");
        }

        private void loginWithUser(string iUnitestMail, string iUnitestPass)
        {
            bool result;
            try
            {
                var userEntity = userEngine.LogInUser(iUnitestMail, iUnitestPass);
                result = userEntity.Email == iUnitestMail;
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Login User] : {status}");
        }

        private void createTemplateTest()
        {
            bool result;
            try
            {
                string jsonValue = _TestBirthdayBoss;
                var resultString = appEngine.CreateNewTemplate(jsonValue, _TestTemplateName, _TestUserEmail, _TestCategory);
                result = (resultString == "");
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Create New Template] : {status}");
        }

        private void getServerTemplateDetailsAfterCreation()
        {
            bool result;
            try
            {
                var templateResult = appEngine.GetTemplate(_TestCategory, _TestTemplateName);
                result = (
                    templateResult.HeaderName == _TestTemplateName && templateResult.CategoryName == _TestCategory &&
                    templateResult.UserIdentity == _TestUserEmail && string.IsNullOrEmpty(templateResult.Comments));
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Get Template Details] : {status}");
        }

        private void generateTemplate()
        {
            bool result;
            try
            {
                string templateContent = appEngine.GenerateHTMLTemplateWithValues(CreateTemplateFormation());
                var templateResult = appEngine.GetTemplate(_TestCategory, _TestTemplateName);
                result = (
                    templateResult.HeaderName == _TestTemplateName && templateResult.CategoryName == _TestCategory &&
                    templateResult.UserIdentity == _TestUserEmail && string.IsNullOrEmpty(templateResult.Comments));
                if (result)
                {
                    string filePath = appEngine.OpenTemplateInWord(templateContent, _TestTemplateNameWord);
                    result = File.Exists(filePath);
                }
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Generate Template] : {status}");
        }

        private void templateCommentsTest()
        {
            bool result;
            try
            {
                var commentResult = appEngine.AddCommentToTemplate(_TestCategory, _TestTemplateName, "SomeTest Comment");
                result = (commentResult == _succeededUpdateToDB);
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Template Comment] : {status}");
        }

        private void addRateTest()
        {
            bool result = false;
            try
            {
                var commentResult = appEngine.RateTamplate(_TestCategory, _TestTemplateName, 4);
                if (commentResult == _succeededUpdateToDB)
                {
                    if (appEngine.GetTemplateDetails(_TestCategory, _TestTemplateName).Rate == 4)
                    {
                        commentResult = appEngine.RateTamplate(_TestCategory, _TestTemplateName, 2);
                        if (commentResult == _succeededUpdateToDB)
                        {
                            result = appEngine.GetTemplateDetails(_TestCategory, _TestTemplateName).Rate == 3;
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Rate Template] : {status}");
        }

        private void favoriteTest()
        {
            markAsFavorite();
            testExistingFavorite();
            unmarkFavorite();
            testNoFavorites();
        }

        private void markAsFavorite()
        {
            bool result;
            try
            {
                var commentResult = appEngine.MarkTemplateAsFavorite(_TestCategory, _TestTemplateName, _TestUserEmail);
                result = (commentResult == _succeededUpdateToDB);

            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Mark Favorit] : {status}");
        }

        private void testExistingFavorite()
        {
            bool result;
            try
            {
                var userResult = userEngine.GetUserData(_TestUserEmail);
                result = userResult.FavoriteTemplates.Contains(_TestTemplateName);
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Check Existing Favorit] : {status}");
        }

        private void unmarkFavorite()
        {
            bool result;
            try
            {
                var commentResult = appEngine.RemoveMarkTemplateAsFavorite(_TestCategory, _TestTemplateName, _TestUserEmail);
                result = (commentResult == _succeededUpdateToDB);
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Unmark Favorit] : {status}");
        }

        private void testNoFavorites()
        {
            bool result;
            try
            {
                var userResult = userEngine.GetUserData(_TestUserEmail);
                result = string.IsNullOrEmpty(userResult.FavoriteTemplates);
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Check No Favorit] : {status}");
        }

        private void testDeleteTemplate()
        {
            bool result;
            try
            {
                var userResult = appEngine.DeleteTemplate(_TestCategory, _TestTemplateName, _TestUserEmail);
                result = (userResult == "Delete succeeded");
            }
            catch
            {
                result = false;
            }

            string status = resultString(result);
            Console.WriteLine($"[Delete Template] : {status}");
        }

        private TemplateFormation CreateTemplateFormation()
        {
            TemplateFormation retVal = new TemplateFormation();
            retVal.CategoryName = "Greetings";
            retVal.HeaderName = _TestTemplateName;
            retVal.Values = createWebDataContainerList();

            return retVal;
        }

        private List<WebDataContainer> createWebDataContainerList()
        {
            List<WebDataContainer> retVal = new List<WebDataContainer>();
            WebDataContainer webDataContainer = new WebDataContainer();
            webDataContainer.Name = "Name";
            webDataContainer.Value = "uni2";
            retVal.Add(webDataContainer);

            WebDataContainer webDataContainer1 = new WebDataContainer();
            webDataContainer1.Name = "DATE";
            webDataContainer1.Value = "3/11/18";
            retVal.Add(webDataContainer1);

            WebDataContainer webDataContainer2 = new WebDataContainer();
            webDataContainer2.Name = "FROM";
            webDataContainer2.Value = "test2";
            retVal.Add(webDataContainer2);

            return retVal;
        }

        private string resultString(bool iResult)
        {
            Console.ForegroundColor = (iResult) ? ConsoleColor.Green : ConsoleColor.Red;
            return (iResult) ? "Pass" : "Faild";
        }
    }
}
