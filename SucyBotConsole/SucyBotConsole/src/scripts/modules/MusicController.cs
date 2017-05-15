// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Discord
using Discord;
using Discord.Commands;

namespace SucyDiscordBot.src.scripts.modules {
    class MusicController {
        // variables
        DiscordClient client;
        CommandService commands;
        ConsoleScript conscr;

        // constructor
        public MusicController(DiscordClient c, ConsoleScript cosc, CommandService cmds)  {
            client = c;
            conscr = cosc;
            commands = cmds;
            install();
            conscr.Print("Hacker", "[Modules] Successfully loaded Music Module.");
        }
        
        // installation function
        private void install() {
            // coming soon lol
        }
    }
}
