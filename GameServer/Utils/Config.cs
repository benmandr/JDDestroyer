namespace GameServer
{
    public static class Config
    {

        //Server settings
        public const int SERVER_PORT = 8088;
        public const string SERVER_HOST = "ws://localhost";//AZURE: "ws://23.101.139.207";
       // public const string SERVER_HOST = "ws://25.51.59.235";//AZURE: "ws://23.101.139.207";

        //Player settings
        public const double MOVESPEED = 5;
        public const double PLAYERSIZE = 10;


        //Enemies settings
        public const double MAXENEMIES = 5;
        public const int ENEMYMOVESPEED = 2;
        public const int ENEMYMOVERATE = 200;
        public const int ENEMYSPAWNSPEED = 500;
        public const double ENEMYSIZE = 5;

        //Enemy states
        public const int ENEMYANGRYTIME = 4000;
        public const int ENEMYFREEZETIME = 4000;

        //Golden Tooth
        public const double GOLDENTOOTHSIZE = 2;

        //Bullet settings
        public const double BULLETWIDTH = 2;
        public const double BULLETSPEED = 1;

        //Map settings
        public const int FRAMESPEED = 20;
        public const double INNERSQUARESIZE = 50;
        public const double CORNERSIZE = (100 - INNERSQUARESIZE) / 2;
        public const double PLAYERBOUND = CORNERSIZE + PLAYERSIZE / 2;
        public const double PLAYERBOUND2 = 100 - PLAYERBOUND;
    }
}
