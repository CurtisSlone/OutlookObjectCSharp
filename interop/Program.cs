using System;
using System.Collections.Generic;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;

namespace OutlookApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize Outlook Application
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.NameSpace outlookNamespace = outlookApp.GetNamespace("MAPI");
                
                // Get the Sent Items folder
                Outlook.MAPIFolder sentFolder = outlookNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
                Outlook.Items sentItems = sentFolder.Items;

                // Store mail items and list them
                List<Outlook.MailItem> mailList = new List<Outlook.MailItem>();
                Console.WriteLine("Listing all sent emails:");

                int index = 0;
                foreach (Outlook.MailItem item in sentItems)
                {
                    mailList.Add(item);
                    Console.WriteLine($"[{index}] Subject: {item.Subject}");
                    index++;
                }

                if (mailList.Count == 0)
                {
                    Console.WriteLine("No sent emails found.");
                    return;
                }

                // Allow user to navigate and select email with arrow keys
                int selectedIndex = 0;
                ConsoleKey key;

                do
                {
                    Console.Clear();
                    for (int i = 0; i < mailList.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"> Subject: {mailList[i].Subject}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"  Subject: {mailList[i].Subject}");
                        }
                    }

                    key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                        selectedIndex--;
                    else if (key == ConsoleKey.DownArrow && selectedIndex < mailList.Count - 1)
                        selectedIndex++;
                } while (key != ConsoleKey.Enter);

                // Display selected mail item attributes
                Console.Clear();
                Outlook.MailItem selectedMail = mailList[selectedIndex];
                DisplayMailAttributes(selectedMail);

                // Release COM objects
                Marshal.ReleaseComObject(sentItems);
                Marshal.ReleaseComObject(sentFolder);
                Marshal.ReleaseComObject(outlookNamespace);
                Marshal.ReleaseComObject(outlookApp);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        static void DisplayMailAttributes(Outlook.MailItem mail)
        {
            Console.WriteLine($"Subject: {mail.Subject}");
            Console.WriteLine($"To: {mail.To}");
            Console.WriteLine($"CC: {mail.CC}");
            Console.WriteLine($"Body: {mail.Body.Substring(0, Math.Min(mail.Body.Length, 100))}...");  // Display first 100 chars
            Console.WriteLine($"Sent On: {mail.SentOn}");
            Console.WriteLine($"Attachments Count: {mail.Attachments.Count}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
