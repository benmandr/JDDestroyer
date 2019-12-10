namespace GameServer
{
    public static class Config
    {
        //Server settings
        public const int SERVER_PORT = 8088;
        public const string SERVER_HOST = //"ws://192.168.43.110";
            "ws://localhost";
        //AZURE: "ws://23.101.139.207";

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
        public const int ENEMYSTATECHANGETIME = 2000;

        //Golden Tooth
        public const double GOLDENTOOTHSIZE = 2;

        //Bullet settings
        public const double BULLETWIDTH = 2;
        public const double BULLETSPEED = 2;
        public const double SHOOTINGRATE = 300;

        //Scores
        public const int ENEMYHITSCORE = 50;
        public const int PLAYERHITSCORE = 200;
        public const int SHOOTSCOST = 30;
        public const int HITCOST = 50;
        public const int GOLDENTOOTHSCORE = 400;

        //Map settings
        public const int FRAMESPEED = 40;
        public const double INNERSQUARESIZE = 50;
        public const double CORNERSIZE = (100 - INNERSQUARESIZE) / 2;
        public const double PLAYERBOUND = CORNERSIZE + PLAYERSIZE / 2;
        public const double PLAYERBOUND2 = 100 - PLAYERBOUND;
    }
}
