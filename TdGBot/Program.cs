using TdGBot.commands.Prefix;
using TdGBot.config;
using DSharpPlus;
using DSharpPlus.AsyncEvents;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComponentType = DSharpPlus.ComponentType;
using InteractionResponseType = DSharpPlus.InteractionResponseType;
using Discord.WebSocket;

namespace TdGBot
{
    internal class Program
    {
        public static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands { get; set; }
        public static AsyncEventHandler<DiscordClient, ComponentInteractionCreateEventArgs> ButtonPressResponse { get; private set; }

        static async Task Main(string[] args)
        {
            // 1.Lendo o token e o prefixo
            var configJson = new JSONReader();
            await configJson.ReadJSON();

            //2. Configurando a configuração do bot
            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJson.discordToken,
                TokenType = DSharpPlus.TokenType.Bot,
                AutoReconnect = true,
            };

            //3. Aplique esta configuração ao nosso DiscordClient
            Client = new DiscordClient(config);

            //4. Defina o tempo limite padrão para comandos que usam interatividade
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            //5. Configure o evento Task Handler Ready
            Client.Ready += OnClientReady;
            Client.ComponentInteractionCreated += ButtonResponse;
            //   Client.ComponentInteractionCreated += InteractionEventHandler;
            Client.MessageCreated += MessageCreateHandler;
            //   Client.ModalSubmitted += ModalEventHandler;
            //   Client.VoiceStateUpdated += VoiceChannelHandler;
            Client.GuildMemberAdded += UserJoinHandler;
            //   Client.SelectMenuExecuted += MyMenuHandler;
            // Client.MessageReactionAdded += MessageReactionAdded;

            //6. Defina a configuração dos comandos
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJson.discordPrefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            //7. Registre seus comandos
            Commands.RegisterCommands<command>();
            // Commands.RegisterCommands<JoinCommands>();
            //   Commands.RegisterCommands<UserRequestedCommands>();
            //   Commands.RegisterCommands<DiscordComponentCommands>();
            //   Commands.RegisterCommands<MusicCommands>();

            //8. Conecte-se para colocar o Bot on-line
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task ButtonResponse(DiscordClient sender, ComponentInteractionCreateEventArgs e)
        {


            //Drop-Down Events
            if (e.Id == "dropDownList" && e.Interaction.Data.ComponentType == ComponentType.StringSelect)
            {

                var options = e.Values;
                foreach (var option in options)
                {

                    switch (option)
                    {
                        case "Ancient":

                            var AncientMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v0.2.1.2 \n\n**Intalação**: \n**1.** extraia a pasta Ancient Cities Tradução\n**2.** copiar e colar a pasta mod em ...\\**Documents\\Uncasual Games\\Ancient Cities\\Mod**")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Ancient Cities")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/667610/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                     new DiscordLinkButtonComponent("https://servidor-tdg.ezconnect.to/share/bChmmWEXj", "TdG", true),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/Y0xkgAJB#poNHZSssA17UOUfoUQeDM2si1uWxgAh52lmzTwXSm-w", "Mega"),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/Y0xkgAJB#poNHZSssA17UOUfoUQeDM2si1uWxgAh52lmzTwXSm-w", "Drive", true)
                                 });


                            var sendAncient = await e.Channel.SendMessageAsync(AncientMessage);
                            var AncientDelete = await e.Channel.GetMessageAsync(sendAncient.Id);

                            await Task.Delay(60000);
                            if (AncientDelete != null)
                            {
                                await AncientDelete.DeleteAsync();
                            }

                            break;

                        case "Anno1800":
                            var Anno1800Message = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v10\n\n**Intalação**:\n**1.** Remova a pasta Commuter-pier-south de\nAnno 1800\\mods (antiga tradução)\n\n**1.**extraia a pasta Anno 1800 tradução\n**2.** copiar e colar a pasta MODS no diretório do jogo, \\Anno 1800\\\n**3.** copiar a pasta BIN no diretório do jogo,\\Anno 1800 e Substitua os arquivos")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Anno 1800")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/916440/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                     new DiscordLinkButtonComponent("http://tdg.ezconnect.to/share/JJO4J9nYZ", "TdG", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1Rkn920ZYTtj2fn7CFODg8BJD9P5q5bWq/view?usp=sharing", "Drive", false),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1Rkn920ZYTtj2fn7CFODg8BJD9P5q5bWq/view?usp=sharing", "Mega", true),
                                 });


                            var sendAnno1800 = await e.Channel.SendMessageAsync(Anno1800Message);
                            var Anno1800Delete = await e.Channel.GetMessageAsync(sendAnno1800.Id);

                            await Task.Delay(60000);
                            if (Anno1800Delete != null)
                            {
                                await Anno1800Delete.DeleteAsync();
                            }
                            break;

                        case "BaldursGate3":
                            var BaldursGate3Message = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v4.1.90 \n\n**Intalação**:\n**1.** Entre na pasta Launcher\n**2.** Faça uma copia da pasta ES ao fazer terá outra pasta com o nome  es-copia\n**3** Renomeie a pasta es-copia por pt-PT\n**4** Inicie o Launcher e mude o idioma para o portugues de portugal")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Baldurs Gate 3")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/1086940/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/g1hE1DDa#xvfnDv8pQt6VUsqZvpsM-7EGPwClwEB1Rv-XJkXsVXA", "Mega", false),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1e4moAVGL8Fpr8gKs9jSvwX4VlDs5SYOd/view", "Drive", false)
                                 });


                            var sendBaldursGate3 = await e.Channel.SendMessageAsync(BaldursGate3Message);
                            var BaldursGate3Delete = await e.Channel.GetMessageAsync(sendBaldursGate3.Id);

                            await Task.Delay(60000);
                            if (BaldursGate3Delete != null)
                            {
                                await BaldursGate3Delete.DeleteAsync();
                            }
                            break;

                        case "CasttleAndCroops":
                            var CasttleMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: ???? \n\n**Intalação**:\n**1**: Extraia o conteúdo do arquivo para a pasta **(...\\Documents\\My Games\\Cattle and Crops)**\n**2**: Renomei todos os arquivos com a estençao pt_Pt.str por es_Es.str que estão dento das pastas **campaigns, machines, strings, tutoriales****3**: Ex: dentro da pasta tutoriales tens um arquivo com o nome **tutorials.pt_PT.str**  renomeia para **tutorials.es_Es.str** **3**: Em propriedades do jogo na steam mude o idioma para Español")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Casttle and Croops")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/704030/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                  //   new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/hgInhIRQ#o6512ITo-WERX5-JlfY-7OxwSNiVniX2Rg6XP-q2amo", "Mega", false),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/hgInhIRQ#o6512ITo-WERX5-JlfY-7OxwSNiVniX2Rg6XP-q2amo", "Drive", true)
                                 });


                            var sendCasttle = await e.Channel.SendMessageAsync(CasttleMessage);
                            var CasttleDelete = await e.Channel.GetMessageAsync(sendCasttle.Id);

                            await Task.Delay(60000);
                            if (CasttleDelete != null)
                            {
                                await CasttleDelete.DeleteAsync();
                            }
                            break;

                        case "EmpireOfSin":
                            var EmpireOfSinMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v1.06\n\n**Intalação**:\n**1.** extraia a pasta Empire Of Sin\n**2.**copiar e colar a pasta mod em...\\Empire of Sin\\EmpireOfSin_Data\\StreamingAssets\\Raw~\\Localization**")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Empire Of Sin")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/604540/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1VafG3CGIOXiagDPIYle5sD0ISgYJGwOl/view?usp=sharing", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1VafG3CGIOXiagDPIYle5sD0ISgYJGwOl/view?usp=sharing", "Drive", false)
                                 });


                            var sendEmpireOfSin = await e.Channel.SendMessageAsync(EmpireOfSinMessage);
                            var EmpireOfSinDelete = await e.Channel.GetMessageAsync(sendEmpireOfSin.Id);

                            await Task.Delay(60000);
                            if (EmpireOfSinDelete != null)
                            {
                                await EmpireOfSinDelete.DeleteAsync();
                            }
                            break;

                        case "ExpeditionsRome":
                            var ExpeditionsRomeMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v1.5.0.113.64976 \n**Intalação**:\n\n**1**: Extrair o arquivo.rar\n**2**: Copiar e cola o arquivo **.pak**  em ...\\**Expeditions Rome\\ExpeditionsRome\\Content\\Paks**")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Expeditions: Rome")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/987840/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1B1UD0m2xw4DGmfVw3Ff_16cUakhb0MA6/view?usp=sharing", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1aI2D7LgNxksLApKAnxT_1n2wdjv8Out2/view?usp=sharing", "Drive", false)
                                 });


                            var sendExpeditionsRome = await e.Channel.SendMessageAsync(ExpeditionsRomeMessage);
                            var ExpeditionsRomeDelete = await e.Channel.GetMessageAsync(sendExpeditionsRome.Id);

                            await Task.Delay(60000);
                            if (ExpeditionsRomeDelete != null)
                            {
                                await ExpeditionsRomeDelete.DeleteAsync();
                            }
                            break;

                        case "FireCommander":
                            var FireMessage = new DiscordMessageBuilder()
                             .AddEmbed(new DiscordEmbedBuilder()
                             .WithTimestamp(DateTimeOffset.UtcNow)
                             .WithFooter(e.User.Username, e.User.AvatarUrl)
                             .WithDescription("**Versão**:  v??? \n**Intalação**:\n\n**1**: igual ao video https://youtu.be/8UkAmoTfnpA")
                             .WithColor(new DiscordColor(0x56ff08))
                             .WithTitle("Fire Commander")
                             .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/1362560/header.jpg?"))
                             .AddComponents(new DiscordComponent[]
                             {
                               //  new DiscordLinkButtonComponent("", "TdG", true),
                                 new DiscordLinkButtonComponent("https://drive.google.com/file/d/1Cqjs9ei1m5Qq9nsObrSyKr8J046ZHnSE/view?usp=sharing", "Mega", true),
                                 new DiscordLinkButtonComponent("https://drive.google.com/file/d/1Cqjs9ei1m5Qq9nsObrSyKr8J046ZHnSE/view?usp=sharing", "Drive", false)
                             });


                            var sendFire = await e.Channel.SendMessageAsync(FireMessage);
                            var FireDelete = await e.Channel.GetMessageAsync(sendFire.Id);

                            await Task.Delay(60000);
                            if (FireDelete != null)
                            {
                                await FireDelete.DeleteAsync();
                            }
                            break;

                        case "Frozenheim":
                            var FrozMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v1.0.0.30 \n**Intalação**:\n\n**1**: Extrair o arquivo Frozenheim_PT-BR.rar\n**2**: Copiar e colar o pasta Frozenheim no diretório onde jogo foi instalado")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Frozenheim")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/1134100/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/19A8sIE2lWWZ61teVoAyznPxhFLxPOYEb/view?usp=sharing", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1DQgswGENMVHuSbG_Rw7aPXKLytes0IcS/view?usp=sharing", "Drive", false)
                                 });


                            var sendFroz = await e.Channel.SendMessageAsync(FrozMessage);
                            var FrozDelete = await e.Channel.GetMessageAsync(sendFroz.Id);

                            await Task.Delay(60000);
                            if (FrozDelete != null)
                            {
                                await FrozDelete.DeleteAsync();
                            }
                            break;

                        case "ImperatorRome":
                            var ImperatorMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v2.01 \n\n**Intalação**: \n**1.** ===========\n**2.** ============")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Imperator Rome")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/859580/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/dlYzDaRb#9hpLX97_APVSDSvpyuEgDkq6_hr3FWucMcVCTOaBlwk", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/17KJj09cL8PPk6LAIkjDtqvyzgGP6bjfN/view?usp=drive_link", "Drive", false)
                                 });


                            var sendImperator = await e.Channel.SendMessageAsync(ImperatorMessage);
                            var ImperatorDelete = await e.Channel.GetMessageAsync(sendImperator.Id);

                            await Task.Delay(60000);
                            if (ImperatorDelete != null)
                            {
                                await ImperatorDelete.DeleteAsync();
                            }
                            break;

                        case "LordsAndVilleins":
                            var lordsMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**:  v??? \n**Intalação**:\n\n**1**: veja o video https://youtu.be/8UkAmoTfnpA")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("lords and Villeins")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/1287530/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1a5KC27Qjv2-1gd7cMhgmFNx9Bg960IUW/view?usp=sharing", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1a5KC27Qjv2-1gd7cMhgmFNx9Bg960IUW/view?usp=sharing", "Drive", false)
                                 });


                            var sendlords = await e.Channel.SendMessageAsync(lordsMessage);
                            var lordsDelete = await e.Channel.GetMessageAsync(sendlords.Id);

                            await Task.Delay(60000);
                            if (lordsDelete != null)
                            {
                                await lordsDelete.DeleteAsync();
                            }
                            break;

                        case "PoliceSimulatorPatrol":
                            var PoliceMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v??? \n**Intalação**:\n\n**1**: Extrair o arquivo.rar\n**2**: Copiar e cola o arquivo **z_MCosta-Portuguese.pak** ...\\Police Simulator Patrol Duty\\Police\\Content\\Paks")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Police Simulator Patrol Duty")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/461870/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                  //   new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/A143DQDI#6iT0Bo7bV_sldsh_59cgrena0OOmMrCULdUN-4o-NJQ", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1bWt7jUP-3pIr-d6TR3bcFX7dbx9HZD8g/view?usp=drive_link", "Drive", false)
                                 });


                            var sendPolice = await e.Channel.SendMessageAsync(PoliceMessage);
                            var PoliceDelete = await e.Channel.GetMessageAsync(sendPolice.Id);

                            await Task.Delay(60000);
                            if (PoliceDelete != null)
                            {
                                await PoliceDelete.DeleteAsync();
                            }
                            break;

                        case "ReadyOrNot":
                            var ReadyMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**:  v??? \n**Intalação**:\n\n**1**: Extrair o arquivo.rar\n**2**: Copiar e cola arquivo.pak  em ...**\\Ready Or Not\\ReadyOrNot\\Content\\Paks**")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Ready Or Not")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/1144200/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1oRMgsGyCA1LcKFpC213t7AL3srzjRG9U/view?usp=sharing", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1oRMgsGyCA1LcKFpC213t7AL3srzjRG9U/view?usp=sharing", "Drive", false)
                                 });


                            var sendReady = await e.Channel.SendMessageAsync(ReadyMessage);
                            var ReadyDelete = await e.Channel.GetMessageAsync(sendReady.Id);

                            await Task.Delay(60000);
                            if (ReadyDelete != null)
                            {
                                await ReadyDelete.DeleteAsync();
                            }
                            break;

                        case "SOS-FoMt-pc":
                            var FoMtMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**:  v1.04 \n**Intalação**:\n\n**1**: Estrai o arquivo .rar \n**2** Copiar e substituir os arquivos extraídos no diretorios do jogo: 😉")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("SOS FoMt pc")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/978780/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                  //   new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1rCXwbWZuWl0upuGTpioRqEqdUw0oF1B8/view?usp=sharing", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1rCXwbWZuWl0upuGTpioRqEqdUw0oF1B8/view?usp=sharing", "Drive", false)
                                 });


                            var sendFoMt = await e.Channel.SendMessageAsync(FoMtMessage);
                            var FoMtDelete = await e.Channel.GetMessageAsync(sendFoMt.Id);

                            await Task.Delay(60000);
                            if (FoMtDelete != null)
                            {
                                await FoMtDelete.DeleteAsync();
                            }
                            break;

                        case "SOS-FoMt-witch":
                            var witchMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("Versão**:  v??? \n**Intalação**:\n\n**1**: nao tenho switch para ver como instala usei um emolador \n**2**: 😉")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("SOS FoMt switch")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/978780/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1C7zOanhC0rbD3IC6AaTeyfovd93J-Ihf/view?usp=sharing", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1C7zOanhC0rbD3IC6AaTeyfovd93J-Ihf/view?usp=sharing", "Drive", false)
                                 });


                            var sendwitch = await e.Channel.SendMessageAsync(witchMessage);
                            var witchDelete = await e.Channel.GetMessageAsync(sendwitch.Id);

                            await Task.Delay(60000);
                            if (witchDelete != null)
                            {
                                await witchDelete.DeleteAsync();
                            }
                            break;

                        case "TheGuild3":
                            var TheGuildMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v0.9.16_636414 \\n**Intalação**:\\n\\n**1**: Extrair o arquivo .rar\\n**2**: Copiar e cola o arquivo **locdirect_portugese.loo** na pasta **...\\The Guild 3-0.9\\media\\localization**")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("The Guild 3")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/311260/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/BtBQzTZK#cyaQDT833KGUm1D7a81Z9Hj0tzzsEiBnkUxWKzTIsZY", "Mega", false),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1GutCmc41KyzMX6eGgWiFIg2mr-Q5KkKd/view?usp=sharing", "Drive", false)
                                 });


                            var sendTheGuild = await e.Channel.SendMessageAsync(TheGuildMessage);
                            var TheGuildDelete = await e.Channel.GetMessageAsync(sendTheGuild.Id);

                            await Task.Delay(60000);
                            if (TheGuildDelete != null)
                            {
                                await TheGuildDelete.DeleteAsync();
                            }
                            break;

                        case "ThisLandMyLand":
                            var MyLandMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v0.4.16483 \n**Intalação**:\n\n**1**: Extrair o arquivo.rar\n**2**: Copiar e cola o arquivo **.lng** .../StreamingAssets/Languages")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("This Land is My Land")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/1181530/capsule_616x353.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                  //   new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/AloVnCpB#ty2yl_W4IQD0EUGpUBdhnDVHVnj3dpqgfzx1kroGtck", "Mega", false),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/1YsP29ggGKQoW9CzfdXbkBnx1HSouitxz/view", "Drive", false)
                                 });


                            var sendMyLand = await e.Channel.SendMessageAsync(MyLandMessage);
                            var MyLandDelete = await e.Channel.GetMessageAsync(sendMyLand.Id);

                            await Task.Delay(60000);
                            if (MyLandDelete != null)
                            {
                                await MyLandDelete.DeleteAsync();
                            }
                            break;

                        case "ThunderTierOne":
                            var ThunderMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**:  v??? \n**Intalação**:\n\n**1**: Extrair o arquivo.rar\n**2**: Copiar e cola a pasta **Localization**  em ...\\**Thunder Tier One\\Thunder2305\\Content**")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Thunder Tier One")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/377300/header.jpg"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/19P28uY3JrKjIydBz6jCkX8bvvLnEqkZ4/view?usp=sharing", "Mega", true),
                                     new DiscordLinkButtonComponent("https://drive.google.com/file/d/19P28uY3JrKjIydBz6jCkX8bvvLnEqkZ4/view?usp=sharing", "Drive", false)
                                 });


                            var sendThunder = await e.Channel.SendMessageAsync(ThunderMessage);
                            var ThunderDelete = await e.Channel.GetMessageAsync(sendThunder.Id);

                            await Task.Delay(60000);
                            if (ThunderDelete != null)
                            {
                                await ThunderDelete.DeleteAsync();
                            }
                            break;

                        case "Tropico6":
                            var TropicoMessage = new DiscordMessageBuilder()
                                 .AddEmbed(new DiscordEmbedBuilder()
                                 .WithTimestamp(DateTimeOffset.UtcNow)
                                 .WithFooter(e.User.Username, e.User.AvatarUrl)
                                 .WithDescription("**Versão**: v.18(825)  \n**Intalação**:\n\n**1**: Extrair o arquivo.rar\n**2**: Copiar e cola a pasta **Localization** em ...\\Tropico 6\\Tropico6\\Content")
                                 .WithColor(new DiscordColor(0x56ff08))
                                 .WithTitle("Tropico 6")
                                 .WithImageUrl("https://cdn.cloudflare.steamstatic.com/steam/apps/492720/header.jpg?t=1651485270"))
                                 .AddComponents(new DiscordComponent[]
                                 {
                                   //  new DiscordLinkButtonComponent("", "TdG", true),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/lpQETKDR#pXBO6XPKp2CPcfNuuyq5n3x3VmNCNovzrreb87u5KIw", "Mega", false),
                                     new DiscordLinkButtonComponent("https://mega.nz/file/k8RQgAYB#fmtYK_IQ9VNQSH2YAsTek20S-X_sK4Qy6PHd4d77Y0Q", "Drive", true)
                                 });


                            var sendTropico = await e.Channel.SendMessageAsync(TropicoMessage);
                            var TropicoDelete = await e.Channel.GetMessageAsync(sendTropico.Id);

                            await Task.Delay(60000);
                            if (TropicoDelete != null)
                            {
                                await TropicoDelete.DeleteAsync();
                            }
                            break;
                    }
                }
            }

            if (e.Id == "menuList" && e.Interaction.Data.ComponentType == ComponentType.StringSelect)
            {
                var options = e.Values;
                foreach (var option in options)
                {
                    switch (option)
                    {
                        case "option1":

                            // Enviar aviso ao usuário
                            var warningMessage = await e.Channel.SendMessageAsync("Pressioou o botao 1");

                            await Task.Delay(10000);
                            // Deletar a mensagem de aviso imediatamente
                            await warningMessage.DeleteAsync();

                            break;

                    }
                }
            }

            // Botao evento
            if (e.Interaction.Data.CustomId == "1")
            {

                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent("presionaste o botao 1"));
            }
            else if (e.Interaction.Data.CustomId == "2")
            {
                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent("presionaste o botao 2"));
            }
            else if (e.Interaction.Data.CustomId == "funBotao")
            {
                //  await e.Channel.SendMessageAsync("Mensagem de teste");
                //  await e.Interaction.CreateResponseAsync(InteractionResponseType.Modal);

                string funComandsList = "!ajuda -> teste \n" +
                                        "!test -> ola canal <#967754666735710228>";
                await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{funComandsList}"));
            }

        }

        private static Task OnClientReady(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private static async Task UserJoinHandler(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            var defaultChannel = e.Guild.GetDefaultChannel();

            var welcomeEmbed = new DiscordEmbedBuilder()
            {

                Title = "Bem-Vindo(a)\n\nServidor TDG",
                Timestamp = DateTimeOffset.UtcNow, // DateTime.Now,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = e.Member.AvatarUrl,
                },
                Color = DiscordColor.Green,
            };

            await defaultChannel.SendMessageAsync(embed: welcomeEmbed);

        }

        private static async Task MessageCreateHandler(DiscordClient sender, MessageCreateEventArgs e)
        {
            var swearFilter = new SwearFilter();
            foreach (var word in swearFilter.swearWords)
            {
                if (!e.Author.IsBot && Regex.IsMatch(e.Message.Content, $@"\b{Regex.Escape(word)}\b", RegexOptions.IgnoreCase))
                {
                    // Verificar se o canal da mensagem está na lista de canais permitidos
                    if (CanaisPermitidos.ListaDeCanais.Contains(e.Channel.Id))
                    {
                        // Deletar a mensagem do usuário
                        await e.Message.DeleteAsync();

                        // Enviar aviso ao usuário
                        var warningMessage = await e.Channel.SendMessageAsync("Mensagem não é permitida, você foi avisado");

                        // Deletar a mensagem de aviso imediatamente
                        await warningMessage.DeleteAsync();

                    }


                }

            }
        }

        public async Task MyMenuHandler(SocketMessageComponent arg)
        {
            var text = string.Join(", ", arg.Data.Values);
            await arg.RespondAsync($"You have selected {text}");
        }

    }
    
}
