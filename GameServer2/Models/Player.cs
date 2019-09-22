using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GameServer2.Models
{
    class Player
    {
        public string name { get; set; }
        public Position position { get; set; }


        public static Player createFromJson(string jsonString)
        {

            Player player = JsonConvert.DeserializeObject<Player>(jsonString);

            return player;
        }
    }
}
