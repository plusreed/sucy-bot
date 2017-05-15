// System
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Discord
using Discord;
using Discord.API;
using Discord.Audio;
using Discord.Commands;
using Discord.Logging;
using Discord.Net;
using Discord.OAuth2;

// Newtonsoft
using Newtonsoft.Json;

// SucyDiscordBot
using SucyDiscordBot.src.scripts.modules;

namespace SucyDiscordBot.src.scripts {
    // MasterScript Class
    class MasterScript {
        // variables
        DiscordClient client;
        ConsoleScript conscr;
        CommandService commands;
        string json1;
        Target newTarget1;

        // constructor
        public MasterScript(ConsoleScript cosc) {
            conscr = cosc;
            try {
                // finding json file
                json1 = File.ReadAllText("bot_config.json");
                newTarget1 = JsonConvert.DeserializeObject<Target>(json1);

                // checking stuff
                conscr.Print("Normal","Checking configuration file...");
                conscr.Wait(3);
                configReader();
                checkAPIKeys();

                string output = JsonConvert.SerializeObject(newTarget1,Formatting.Indented);
                // Console.WriteLine(output); // this was mainly used as a debugger
                File.WriteAllText("bot_config.json", output);

                // initializing client functions
                client = new DiscordClient(input => {
                    input.LogLevel = LogSeverity.Info;
                    input.LogHandler = Log;
                });

                client.UsingCommands(input => {
                    input.PrefixChar = '>';
                    input.AllowMentionPrefix = false;
                });

                // initializing other compontents
                client.UserJoined += async (s, e) => {
                    conscr.Print("Warning", $"[Event] {e.User.Name} has joined the server.");
                    conscr.Print("Warning", $"{e.User.Mention}.");
                    var channel = e.Server.FindChannels("general", ChannelType.Text).FirstOrDefault();
                    await channel.SendMessage($"{e.User.Mention} has joined the server!");
                };

                client.UserLeft += async (s, e) => {
                    conscr.Print("Warning", $"[Event] {e.User.Name} has left the server.");
                    var channel = e.Server.FindChannels("general",ChannelType.Text).FirstOrDefault();
                    await channel.SendMessage($"{e.User.Name} has left the server... :c");
                };

                client.UserBanned += async (s, e) => {
                    conscr.Print("Error", $"[Event] {e.User.Name} has been banned from the server!");
                    var channel = e.Server.FindChannels("general", ChannelType.Text).FirstOrDefault();
                    await channel.SendMessage($"{e.User.Name} has been banned from the server! :O");
                };

                client.UserUnbanned += async (s, e) => {
                    conscr.Print("Error", $"[Event] {e.User.Name} has been unbanned from the server!");
                    var channel = e.Server.FindChannels("general", ChannelType.Text).FirstOrDefault();
                    await channel.SendMessage($"{e.User.Name} has been unbanned from the server! :D");
                };

                // installing the commands
                commands = client.GetService<CommandService>(true);
                BasicCommands b_commands = new BasicCommands(client,conscr,commands);
                Games g_Commands = new Games(client,conscr,commands);

                // printing out the final stuff for the console
                conscr.Print("Normal", "----------------------------------------------------------");
                conscr.Print("Normal", " ");
                conscr.Print("Normal", "Console Output:");
                Console.Beep();

                // executing the client
                client.ExecuteAndWait(async() => {
                    await client.Connect(newTarget1.Token, TokenType.Bot);
                });
            } catch (Exception e) { 
                conscr.Print("Error", $"[Error] {e.Message}");
                Console.Beep();
                conscr.Wait(3);
                Environment.Exit(0);
            }
        }

        // private functions
        private void Log (object sender, LogMessageEventArgs e) {
            conscr.Print("Cyan",$"{e.Message}");
        }

        private void configReader() {
            try {
                if (newTarget1.Configured == false || string.IsNullOrWhiteSpace(newTarget1.Token) || string.IsNullOrWhiteSpace(newTarget1.ClientId)) {
                    conscr.Print("Warning","Bot information has not been properly configured. Setting up initialization...");
                    conscr.Wait(3);
                    initializeChecker();
                } else {
                    conscr.Print("Normal", "Configuration file has been successfully initialized.");
                }
            } catch (Exception e) {
                conscr.Print("Error", "Error: Cannot find configuration file..");
                conscr.Wait(3);
                Environment.Exit(0);
            }
        }

        private void checkAPIKeys() {
            try {
                conscr.Print("Normal","Checking for Google API key...");
                conscr.Wait(2);

                if (string.IsNullOrWhiteSpace(newTarget1.GoogleAPIKey)) { 
                    conscr.Print("Warning", "No Google API key found within the configuration file. Music from Youtube is disabled.");
                } else {
                    conscr.Print("Hacker", "Google API key has been obtained. Music from Youtube is now enabled.");
                    conscr.Print("Hacker", $"Key ID: {newTarget1.GoogleAPIKey}");
                }
                conscr.Wait(2);
                
                conscr.Print("Normal", "Checking for SoundCloud Client ID...");
                conscr.Wait(2);

                if (string.IsNullOrWhiteSpace(newTarget1.SoundCloudClientID)) { 
                    conscr.Print("Warning", "No Soundcloud Client ID found within the configuration file. Soundcloud streaming is disabled.");
                } else {
                    conscr.Print("Hacker", "Soundcloud Client ID has been obtained. Soundcloud streaming enabled.");
                    conscr.Print("Hacker", $"Client ID: {newTarget1.SoundCloudClientID}");
                }
                conscr.Wait(2);

            } catch (Exception e) {
                conscr.Print("Error", "Error: Cannot find configuration file..");
                Console.Beep();
                conscr.Wait(3);
                Environment.Exit(0);
            }
        }
        
        private void initializeChecker() {
            Console.Beep();
            conscr.Print("Special", "Please Enter Token: ");
            newTarget1.Token = Console.ReadLine();

            Console.Beep();
            conscr.Print("Special", "Please Enter ClientId: ");
            newTarget1.ClientId = Console.ReadLine();

            Console.Beep();
            conscr.Print("Special", "Setting up configuration (please wait)...");
            conscr.Wait(3);

            Console.Beep();
            conscr.Print("Hacker", "Configuration completed!");
            conscr.Print("Special", $"Token: {newTarget1.Token}");
            conscr.Print("Special", $"ClientId: {newTarget1.ClientId}");
            newTarget1.Configured = true;
            conscr.Wait(3);
        }
    }
}