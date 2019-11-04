namespace GameServer
{
    public static class Config
    {

        //Server settings
        public const int SERVER_PORT = 8088;
        public const string SERVER_HOST = "ws://localhost";//AZURE: "ws://23.101.139.207";

        //Player settings
        public const double MOVESPEED = 5;
        public const double PLAYERSIZE = 10;


        //Enemies settings
        public const double MAXENEMIES = 20;
        public const int ENEMYMOVESPEED = 5;
        public const int ENEMYMOVERATE = 1000;
        public const int ENEMYSPAWNSPEED = 1000;
        public const double ENEMYSIZE = 5;

        //Bullet settings
        public const int BULLETSPEED = 20;
        public const double BULLETWIDTH = 2;

        //Map settings
        public const double INNERSQUARESIZE = 60;
        public const double CORNERSIZE = (100 - INNERSQUARESIZE) / 2;
        public const double PLAYERBOUND = CORNERSIZE + PLAYERSIZE / 2;
        public const double PLAYERBOUND2 = 100 - PLAYERBOUND;
    }
}
