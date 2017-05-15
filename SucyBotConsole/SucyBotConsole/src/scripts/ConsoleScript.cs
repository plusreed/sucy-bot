// System
using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

// Newtonsoft
using Newtonsoft.Json;

namespace SucyDiscordBot.src.scripts {
    // Target Class
    class Target {
        public string Token { get; set; }
        public string ClientId { get; set; }
        public int BotId { get; set; }
        public Boolean Configured { get; set; }
        public string Prefix { get; set; }
        public string GoogleAPIKey { get; set; }
        public string SoundCloudClientID { get; set; }
    }

    // ConsoleScript Class
    class ConsoleScript {
        // constructor
        public ConsoleScript(Boolean beep) {
            if (beep == true) {
                Print("Hacker","ConsoleScript has successfully been loaded.");
                Console.Beep();
            } else {
                // do nothing lol
            }
        }

        // public functions
        public void Print(string type, string msg) {
            if (type == "Normal") {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(msg);
            } else if (type == "Error") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(msg);
                Console.Beep();
                Console.Beep();
            } else if (type == "Warning") {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(msg);
                Console.Beep();
                Console.Beep();
            } else if (type == "Special") {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(msg);
            } else if (type == "Neon") {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(msg);
            } else if (type == "Hacker") {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg);
            }
        }

        public void SetBackgroundColor(ConsoleColor color, Boolean clear) {
            Console.BackgroundColor = color;
            if (clear == true)
                Console.Clear();
        }

        public void Wait(int time) {
            Thread.Sleep(time * 1000);
        }
    }
}
