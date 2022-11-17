using System;
using FlaUI.UIA3;
using FlaUI.Core;
using FlaUI.Core.Conditions;
using FlaUI.Core.AutomationElements;    
using System.IO;
using FlaUI.Core.Tools;
using NUnit.Framework;

namespace exam_terminal.Test
{
    public class Program
    {
        static Application msApplication;
        [SetUp]
        public static void Main()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            msApplication = Application.Launch(@"C:\Program Files\Skole\Complete-exam\exam-terminal\exam-terminal\bin\Debug\net6.0-windows\exam-terminal.exe");

            //Program p = new Program();
            //p.TestProduktGrupperButton();

        }
        [Test, Order(1)]
        public void TestProduktGrupperButton()
        {
            var automation = new UIA3Automation();
            var mainWindow = msApplication.GetMainWindow(automation);
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            mainWindow.FindFirstChild(cf.ByName("1")).AsButton().Click();
            mainWindow.FindFirstChild(cf.ByName("ENT")).AsButton().Click();
            Retry.WhileNull(() => mainWindow.FindFirstChild(cf.ByName("Dessert")), TimeSpan.FromSeconds(30), null, true);
            var ProduktgruppeButton = mainWindow.FindFirstChild(cf.ByName("Dessert")).AsButton();
            ProduktgruppeButton.Click();
            var produkter = mainWindow.FindAll(FlaUI.Core.Definitions.TreeScope.Children, cf.ByHelpText("BTN_Produkt"));
            Assert.That(produkter.Length, Is.GreaterThan(0));
            mainWindow.FindFirstDescendant(cf.ByName("BTN_HentSetup"));
        }
        //[Test, Order(2)]
        //public void TestProduktButton()
        //{
        //    var automation = new UIA3Automation();
        //    var mainWindow = msApplication.GetMainWindow(automation);

        //    ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
        //    var ProduktgruppeButton = mainWindow.FindFirstChild(cf.ByName("Schaden Ltd")).AsButton();
        //    ProduktgruppeButton.Click();
        //    var produkter = mainWindow.FindAll(FlaUI.Core.Definitions.TreeScope.Children, cf.ByHelpText("BTN_Produkt"));
        //    Assert.That(produkter.Length, Is.GreaterThan(0));
        //    mainWindow.FindFirstDescendant(cf.ByName("BTN_HentSetup"));
        //}
    }
}
