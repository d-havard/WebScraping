﻿using Class_Webscrap;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;
using OpenQA.Selenium.DevTools.V123.Audits;
using Microsoft.Playwright;

namespace Console_WebScraping
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            LocateFile locateFile = new LocateFile(@"D:\Stage\Virtual_game\Webcsrap\dataFormatts.xlsx", @"D:\Stage\Virtual_game\Webcsrap\WebScrappingVirtualGame\dataFormatts.xlsx");

            LocateCell locateCell = new LocateCell();

            FillCell fillCell = new FillCell();

            TakeInformation takeInformation = new TakeInformation();

            Navigator navigator = new Navigator();
            
            VerificationUrl verificationUrl = new VerificationUrl();

            string nomSheet = verificationUrl.NameUrl("https://www.eva.gg/fr-FR/booking?locationId=24&gameId=1&currentDate=2024-06-17");

            var sheet = locateFile.VerificationIfFileExist(nomSheet);

            List<string> JsonResponses = await navigator.LaunchNavigatorProcess("https://www.eva.gg/fr-FR/booking?locationId=24&gameId=1&currentDate=2024-06-17");

            //IPage page = await navigator.interceptWebRequest(browser);
            
            //await navigator.goToUrl("https://www.eva.gg/fr-FR/booking?locationId=24&gameId=1&currentDate=2024-06-11", page);

            //await navigator.SaveJsonFiles(browser);

            //List<string> JsonResponses = navigator.getJsonResponses();

            string JsonFilePath = await takeInformation.GetJsonFile(JsonResponses);

            string DateFormat = await takeInformation.RearrangeDate(JsonFilePath);

            await takeInformation.GetInformation(JsonFilePath);

            List<string> StartedHours = takeInformation.GetStartedHour();

            List<int> MaximumPlayers = takeInformation.GetMaximumPlayer();

            List<int> NumberPlayers = takeInformation.GetNumberPlayer();

            List<bool> BattlepassPlayers = takeInformation.GetBattlePassPlayer();
            
            List<bool> PeakHours = takeInformation.GetPeakHour();
            
            int positionX = locateCell.locateCellXPosition(DateFormat, sheet);
            Console.WriteLine("paf");
            int [] positionY = locateCell.LocateCellYPosition(StartedHours, sheet);
            Console.WriteLine("pif");
            fillCell.FillSelectedCell(MaximumPlayers, NumberPlayers, sheet ,positionX, positionY);
            Console.WriteLine("pouf");
            fillCell.colorCell(MaximumPlayers, NumberPlayers, sheet, positionX, positionY, BattlepassPlayers, PeakHours);
            Console.WriteLine("pef");
            locateFile.SaveFile(sheet);
            
        }
    }
}
