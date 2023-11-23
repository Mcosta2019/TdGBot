using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TdGBot.config
{
    internal class JSONReader
    {
        //Declara nossas propriedades Token e Prefix desta classe
        public string discordToken { get; set; }
        public string discordPrefix { get; set; }


        public async Task ReadJSON() //Este método deve ser executado de forma assíncrona
        {
            using (StreamReader sr = new StreamReader("config.json", new UTF8Encoding(false)))
            {
                //Leia e desserealize o arquivo config.json
                string json = await sr.ReadToEndAsync();
                JSONStruct obj = JsonConvert.DeserializeObject<JSONStruct>(json);

                //Definimos nossas propriedades
                this.discordToken = obj.token;
                this.discordPrefix = obj.prefix;
            }
        }

        internal sealed class JSONStruct
        {
            public string token { get; set; }
            public string prefix { get; set; }
        }
    }
}
