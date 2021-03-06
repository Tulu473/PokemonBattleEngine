﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Kermalis.PokemonBattleEngineDiscord
{
    class DiscordBot
    {
        DiscordSocketClient client;
        IServiceProvider services;
        CommandService commands;
        readonly char commandPrefix = '!';

        public static void Main(string[] args)
            => new DiscordBot().MainAsync(args).GetAwaiter().GetResult();
        public async Task MainAsync(string[] args)
        {
            client = new DiscordSocketClient(new DiscordSocketConfig { WebSocketProvider = Discord.Net.Providers.WS4Net.WS4NetProvider.Instance });

            commands = new CommandService(new CommandServiceConfig { DefaultRunMode = RunMode.Async });
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
            services = new ServiceCollection().AddSingleton(client).AddSingleton(commands).BuildServiceProvider();

            client.Log += LogMessage;
            client.MessageReceived += CommandMessageReceived;
            client.ReactionAdded += ReactionListener.Client_ReactionAdded;

            await client.LoginAsync(TokenType.Bot, args[0]); // Token is passed as args[0]
            await client.StartAsync();

            await Task.Delay(-1);
        }

        async Task CommandMessageReceived(SocketMessage arg)
        {
            int argPos = 0;
            if (!(arg is SocketUserMessage message)
                || message.Author.Id == client.CurrentUser.Id
                || !message.HasCharPrefix(commandPrefix, ref argPos))
            {
                return;
            }
            var context = new SocketCommandContext(client, message);
            IResult result = await commands.ExecuteAsync(context, argPos);
            if (!result.IsSuccess)
            {
                Console.WriteLine(result.ErrorReason);
            }
        }
        Task LogMessage(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            return Task.CompletedTask;
        }
    }
}
