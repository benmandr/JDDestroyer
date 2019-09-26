using GameServer.Models;
using System;
namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller.getInstance().initiateConnection();
        }
    }
}
