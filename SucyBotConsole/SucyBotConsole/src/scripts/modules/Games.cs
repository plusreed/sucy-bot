// System
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Discord
using Discord;
using Discord.Commands;

// Newtonsoft
using Newtonsoft.Json;

namespace SucyDiscordBot.src.scripts.modules {
    class CommandCfg {
        public string[] EightBall { get; set; }
        public string[] Settings { get; set; }
    }

    class UserInfo {
        public int Level { get; set; }
    }

    class Games {
        DiscordClient client;
        CommandService commands;
        ConsoleScript conscr;
        string json1;
        CommandCfg cmd_cfg;

        public Games (DiscordClient c, ConsoleScript cosc, CommandService cmds) {
            client = c;
            conscr = cosc;
            commands = cmds;
            try {
                json1 = File.ReadAllText("command_config.json");
                cmd_cfg = JsonConvert.DeserializeObject<CommandCfg>(json1);
                install();
                conscr.Print("Hacker", "[Modules] Successfully loaded Games Module.");
            } catch (Exception e) {
                conscr.Print("Error", $"Error: {e.Message}");
                conscr.Wait(3);
            }
        }

        private void install() {
                commands.CreateCommand("8ball")
                    .Alias("eightball")
                    .Parameter("question", ParameterType.Unparsed)
                    .Description($"A simple game of 8ball.")
                    .Do(async e => {
                        conscr.Print("Special", "[Event] Sending 8ball response.");
                        var question = e.GetArg("question");
                        if (string.IsNullOrWhiteSpace(question))
                            return;
                        try {
                            Random rdm = new Random();
                            string message = $":question: - *{question}*\n:8ball: - **{cmd_cfg.EightBall[rdm.Next(0, cmd_cfg.EightBall.Length)]}**";
                            await e.Channel.SendMessage(message).ConfigureAwait(false);
                        }
                        catch { }
                    });
        }
    }
}
