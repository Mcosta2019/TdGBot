using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TdGBot.commands.Prefix
{
    public class command : BaseCommandModule
    {
        [Command("reg")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task JoinVeref(CommandContext ctx)
        {

            var interactivity = ctx.Client.GetInteractivity();
            var menuBuilder = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("Menu de funções: entrada")
                .WithColor(DiscordColor.Orange)
                .WithDescription("Reaja para se tornar Membro\n\n" +
                ":green_square: : ``Membro do Servidor``"));

            await ctx.Channel.SendMessageAsync(menuBuilder);

        }

        [Command("menu-D")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task MenuDownload(CommandContext ctx)
        {
            // Verificar se o comando está sendo executado no canal permitido
            ulong canalPermitidoId = 851960770143977532; // Substitua pelo ID do canal permitido  851960770143977532

            if (ctx.Channel.Id != canalPermitidoId)
            {
                // Se o comando não estiver no canal permitido, você pode enviar uma mensagem ou simplesmente ignorar
                await ctx.RespondAsync("Este comando só pode ser usado no canal permitido.");
                return;
            }

            //Declara a lista de opções no menu suspenso
            List<DiscordSelectComponentOption> optionList = new List<DiscordSelectComponentOption>();
            optionList.Add(new DiscordSelectComponentOption("Ancient Cities", "Ancient", "Versão do jogo **2.02**"));
            optionList.Add(new DiscordSelectComponentOption("Anno 1800", "Anno1800", "Versão do jogo **10**"));
            optionList.Add(new DiscordSelectComponentOption("Baldurs Gate 3", "BaldursGate3", "Versão do jogo **???**"));
            optionList.Add(new DiscordSelectComponentOption("Casttle And Croops", "CasttleAndCroops", "Versão do jogo **???**"));
            optionList.Add(new DiscordSelectComponentOption("Empire Of Sin", "EmpireOfSin", "Versão do jogo **1.06**"));
            optionList.Add(new DiscordSelectComponentOption("Expeditions Rome", "ExpeditionsRome", "Versão do jogo **1.5.0.113**"));
            optionList.Add(new DiscordSelectComponentOption("Fire Commander", "FireCommander", "Versão do jogo **???**"));
            optionList.Add(new DiscordSelectComponentOption("Frozenheim", "Frozenheim", "Versão do jogo **1.0.0.30**"));
            optionList.Add(new DiscordSelectComponentOption("Imperator Rome", "ImperatorRome", "Versão do jogo **2.01**"));
            optionList.Add(new DiscordSelectComponentOption("Lords and Villeins", "LordsAndVilleins", "Versão do jogo **???**"));
            optionList.Add(new DiscordSelectComponentOption("Police Simulator Patrol Duty", "PoliceSimulatorPatrol", "Versão do jogo **???**"));
            optionList.Add(new DiscordSelectComponentOption("Ready Or Not", "ReadyOrNot", "Versão do jogo **???**"));
            optionList.Add(new DiscordSelectComponentOption("SOS FoMt pc", "SOS-FoMt-pc", "Versão do jogo **1.04**"));
            optionList.Add(new DiscordSelectComponentOption("SOS FoMt switch", "SOS-FoMt-witch", "Versão do jogo **???**"));
            optionList.Add(new DiscordSelectComponentOption("The Guild 3", "TheGuild3", "Versão do jogo **???**"));
            optionList.Add(new DiscordSelectComponentOption("This Land Is MyLand", "ThisLandMyLand", "Versão do jogo **v0.4**"));
            optionList.Add(new DiscordSelectComponentOption("Thunder Tier One", "ThunderTierOne", "Versão do jogo **???**"));
            optionList.Add(new DiscordSelectComponentOption("Tropico 6", "Tropico6", "Versão do jogo **???**"));
            // optionList.Add(new DiscordSelectComponentOption("12", "12", "Versão do jogo **???**"));

            //Transforma a lista em um IEnumerable para o Componente
            var options = optionList.AsEnumerable();

            //Faça o componente suspenso
            var dropDown = new DiscordSelectComponent("dropDownList", "Selecionar....", options);

            //Faça e envie a mensagem com o componente
            var menuDown = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTimestamp(DateTimeOffset.UtcNow)
                .WithColor(DiscordColor.White)
                .WithFooter("M.Costa [PT]#8428™", ctx.User.AvatarUrl)
                .WithTitle("MENU DOWNLOADS")
                .WithDescription("Este é o novo ** MENU DOWNLOADS**, A Barra de Menu contém nomes de jogos com tradução, e a versão do jogo quanto foi traduzido.\n**Por favor, siga as instruções abaixo...**\n" +
                    "\n**INSTRUÇÃOES**\n Abra o menu de seleção para ver os nomes dos jogos já traduzidos.\n Seleciona a tradução do jogo na lista abaixo \n Após a seleção, Aparecerá uma Mensagem do Bot com link para download da tradução selecionado.\n\n**AVISO** \n A Mensagem do Bot Sera apagada Ao Fim de 1 minuto."))
                .AddComponents(dropDown);


            await ctx.Channel.SendMessageAsync(menuDown);
        }

        [Command("test")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task MyFirstCommand(CommandContext ctx, string option1, string option2, string option3, [RemainingText] string pollTitle)
        {
            var interactivity = Program.Client.GetInteractivity();

            DiscordEmoji[] emojiOptions = { DiscordEmoji.FromName(Program.Client, ":one:"),
                                            DiscordEmoji.FromName(Program.Client, ":two:"),
                                            DiscordEmoji.FromName(Program.Client, ":question:"),};

            string optionsDescription = $"{emojiOptions[0]} | {option1} \n" +
                                        $"{emojiOptions[0]} | {option2} \n" +
                                        $"{emojiOptions[0]} | {option3} \n";

            var pollMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                Title = pollTitle,
                Description = optionsDescription
            };

            var sendPoll = await ctx.Channel.SendMessageAsync(embed: pollMessage);
            foreach (var emoji in emojiOptions)
            {
                await sendPoll.CreateReactionAsync(emoji);
            }
        }

        // deletar menssagens com o comando /deletar nome da mensagem

        [Command("deletar")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task Deletar(CommandContext ctx, string searchText)
        {
            // Obtém as mensagens no canal
            var messages = await ctx.Channel.GetMessagesAsync();

            // Filtra as mensagens pelo texto específico
            var messageToDelete = messages.FirstOrDefault(msg => msg.Content.Contains(searchText));

            if (messageToDelete != null)
            {
                // Deleta a mensagem
                await messageToDelete.DeleteAsync();
                await ctx.RespondAsync($"Mensagem contendo '{searchText}' deletada com sucesso.");
            }
            else
            {
                await ctx.RespondAsync($"Nenhuma mensagem contendo '{searchText}' encontrada.");
            }
        }

        [Command("download")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task EmbedMenssage(CommandContext ctx)
        {
            var userDisplayName = ctx.Member.DisplayName;

            // Criar a mensagem com o menu de seleção
            var embed = new DiscordEmbedBuilder
            {
                Title = "DOWNLOADS",
                Timestamp = DateTimeOffset.UtcNow,
                Description = $"Este é o novo menu de **DOWNLOADS**, A Barra de Menu contém nomes de jogos com tradução, e a versão do jogo quanto foi traduzido.\nPor favor, siga as instruções abaixo..." +
                "\n **INSTRUÇÃO** \n _Abra o menu de seleção para ver os nomes dos jogos já traduzidos._\n_Escolha um deles para adicionar._\n_Após a seleção, será enviado um DM para baixar a tradução do respetivo jogo selecionado._",
                Color = new DiscordColor(0xffffff),
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"M.Costa [PT]#8428™ ⚙", // M.Costa [PT]#8428™ ⚙",
                    IconUrl = "https://www.imagemhost.com.br/images/2022/04/23/Pokerface07.th.png"
                }

            };

            await ctx.Channel.SendMessageAsync(embed: embed);
        }

        [Command("help")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task HelpComand(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();


            // Emoji que você deseja usar (pode ser um emoji personalizado ou um emoji padrão do Discord)
            //  var customEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");

            //      var funButao = new DiscordButtonComponent(ButtonStyle.Success, "funBotao", "Fun");
            //      var gameButao = new DiscordButtonComponent(ButtonStyle.Success, "gameBotao", "Games");

            var customEmoji = DiscordEmoji.FromName(ctx.Client, ":thumbsup:");

            var username = ctx.User.Username;

            var helpMassage = new DiscordMessageBuilder()
                   //  .AddComponents(gameButao)
                   .AddEmbed(new DiscordEmbedBuilder()
                   .WithColor(DiscordColor.Azure)
                   .WithTimestamp(DateTimeOffset.UtcNow)
                   .WithTitle(username + "\n Bem-Vindo ao Servidor TDG")
                   .WithThumbnail(ctx.User.AvatarUrl));


            var message = await ctx.Channel.SendMessageAsync(helpMassage);

            // Reagir à mensagem com o emoji
            await message.CreateReactionAsync(customEmoji);


        }

        [Command("dropdown-list")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task DropdownList(CommandContext ctx)
        {
            //Declare the list of options in the drop-down
            List<DiscordSelectComponentOption> optionList = new List<DiscordSelectComponentOption>();
            optionList.Add(new DiscordSelectComponentOption("Option 1", "option1"));
            optionList.Add(new DiscordSelectComponentOption("Option 2", "option2"));
            optionList.Add(new DiscordSelectComponentOption("Option 3", "option3"));

            //Turn the list into an IEnumerable for the Component
            var options = optionList.AsEnumerable();

            //Make the drop-down component
            var dropDown = new DiscordSelectComponent("menuList", "Select...", options);

            //Make and send off the message with the component
            var dropDownMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Gold)
                .WithTitle("This embed has a drop-down list on it"))
                .AddComponents(dropDown);

            await ctx.Channel.SendMessageAsync(dropDownMessage);
        }
        /*-------------------------------------------------------*/
        /*-------------------------------------------------------*/

        [Command("tdg")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task Spawn(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var menuBuilder = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("Bot Teste Bownload")
                .WithColor(DiscordColor.DarkBlue)
                .WithDescription("paguina de  Download")
                .AddField("01", "Name", true)
                .AddField("02", "Age"));

            await ctx.Channel.SendMessageAsync(menuBuilder);
            //     var channel = await interactivity.WaitForMessageAsync(message => message.Content == "ola");

            var channel = await ctx.Member.CreateDmChannelAsync();
            var novaMsg = await interactivity.WaitForMessageAsync(x => x.Author == ctx.User && x.Channel == channel);

            if (novaMsg.Result.Content == "01")
            {
                await channel.SendMessageAsync("Miguel");
            }

        }

        [Command("rool")]
        [RequirePermissions(DSharpPlus.Permissions.Administrator)]
        public async Task RoolCommand(CommandContext ctx)
        {
            var emojis = new List<string> { ":+1:" /* adicione outros emojis, se necessário */ };

            var mensagem = await ctx.RespondAsync($"Reaja a esta mensagem com os emojis: {string.Join(" ", emojis)}");

            foreach (var emoji in emojis)
            {
                await mensagem.CreateReactionAsync(DiscordEmoji.FromName(ctx.Client, emoji));
            }
        }

    }
}
