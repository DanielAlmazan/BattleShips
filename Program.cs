using System;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace BattleShips
{
    internal class Program
    {
		private static readonly log4net.ILog log = 
			log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	    
		private static Game? _game;
		static void Main(string[] args)
		{
			log.Info("Starting BattleShips");
			Console.CursorVisible = false;
			_game = new Game();
			Console.Clear();
			_game.Run();

			log.Info("Exiting BattleShips");
			while (_game.isRunning)
			{
				_game.Update();
			}
		}
	}
}
