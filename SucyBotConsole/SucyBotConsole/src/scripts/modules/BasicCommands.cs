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
    class BasicCommands {
        // variables
        DiscordClient client;
        CommandService commands;
        ConsoleScript conscr;

        // constructor
        public BasicCommands(DiscordClient c, ConsoleScript cosc, CommandService cmds)  {
            client = c;
            conscr = cosc;
            commands = cmds;
            install();
            conscr.Print("Hacker", "[Modules] Successfully loaded Commands Module.");
        }
        
        // installation function
        private void install() {
                commands.CreateCommand("hello").Do(async e => {
                    conscr.Print("Special", "[Event] Sending hello response.");
                    await e.Channel.SendMessage($"Hello {e.User.Mention}!");
                });

                commands.CreateCommand("help")
                    .Parameter("type", ParameterType.Unparsed)
                    .Do(async e => {
                        var question = e.GetArg("type");
                        conscr.Print("Special", "[Event] Sending help response.");
                        if (string.IsNullOrWhiteSpace(question)) {
                            try {
                                conscr.Print("Special", "[Event] Sending list of help commands.");
                                string message = $"```css\n=== HELP ===\n>help info - Gives out information of the bot.\n>help games - Gives out all the game commands.\n>help basic - Gives out all the basic commands.```";
                                await e.Channel.SendMessage(message).ConfigureAwait(false);
                            }
                            catch { }
                        } else { 
                            try {
                                string message = "";
                                if (question == "info") {
                                    conscr.Print("Special", "[Event] Sending important information about SucyBot.");
                                    message = $"```css\n=== INFORMATION ===\nHello there! My name is SucyBot. I am a multi-purpose Discord bot created by TeraArray (also known as Chris). With me in this server, you can do many wonderful things such as:\n- Playing music and games.\n- Creating customized color ranks.\n- Moderating the server.\n- And making this server a fun place to be at.\n \nI hope I can be able to assist you in all your needs. <3\n \nGithub Page: https://github.com/TeraArray/sucybot ```";
                                } else if (question == "games") {
                                    conscr.Print("Special", "[Event] Sending a list of game commands");
                                    message = $"```css\n=== GAMES ===\n>8ball [question] - Gives a randomly selected response to said question.\n```";
                                } else if (question == "basic") {
                                    conscr.Print("Special", "[Event] Sending a list of game commands");
                                    message = $"```css\n=== GAMES ===\n>help [type] - Gives important and helpful information about SucyBot. Current types: info, basic and games.\n>hello - Gives an automatic hello response.```";
                                } else {
                                    conscr.Print("Special", "[Event] Sending list of help commands.");
                                    message = $"```css\n=== HELP ===\n>help info - Gives out information of the bot.\n>help games - Gives out all the game commands.\n>help basic - Gives out all the basic commands.```";
                                }
                                await e.Channel.SendMessage(message).ConfigureAwait(false);
                            }
                            catch { }
                        }
                    });
        }
    }
}
