// System
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// SucyDiscordBot
using SucyDiscordBot;
using SucyDiscordBot.src;
using SucyDiscordBot.src.scripts;

namespace SucyDiscordBot {
    class Program {
        static void Main(string [] args) {
            // initialize consolescript
            ConsoleScript conscr = new ConsoleScript(false);
            conscr.SetBackgroundColor(ConsoleColor.Black, true);
            Console.Title = "SUCYBOT | Console";
            Console.OutputEncoding = Encoding.Unicode;

            // "printing" out text
            conscr.Print("Normal", "==== SUCYBOT ====");
            conscr.Print("Normal", "Created by: Chris F. (aka TeraArray)");
            conscr.Print("Normal", "Version: 1.0");
            conscr.Print("Normal", "Programming Language Used: C#");
            conscr.Print("Normal", "Github Page: https://github.com/TeraArray/sucybot");
            conscr.Print("Normal", "VISUAL FRACTION ENTERTAINMENT");
            conscr.Print("Normal", " ");
            Console.Beep();
            conscr.Wait(3);

            // loading MasterScript.cs
            conscr.Print("Normal", "----------------------------------------------------------");
            conscr = new ConsoleScript(true);
            conscr.Print("Normal", "Loading master script...");
            conscr.Print("Hacker", "Master script has been successfully loaded! Initializing script...");
            conscr.Wait(3);
            MasterScript master = new MasterScript(conscr);
        }
    }
}