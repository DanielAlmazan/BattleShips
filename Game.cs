using System.Globalization;

namespace BattleShips
{
	internal class Game
	{
		#region Variables
		
		public static readonly ConsoleColor DefaultColor = Console.ForegroundColor;
		public static readonly ConsoleColor DefaultBackgroundColor = Console.BackgroundColor;
		private Player? player;
		private Player? enemy;
		private Player? winner;
		public bool IsRunning;
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
		private readonly ResourceManager rm;
        #endregion

		public Game(Configuration config)
		{
            rm = new ResourceManager(config.Language);
		}

        public void Run()
		{
			IsRunning = true;
			ConsoleKey? key = ConsoleKey.NoName;

			while (key.Value != ConsoleKey.Enter && key.Value != ConsoleKey.Escape)
			{
				Console.Clear();
				Welcome();
				key = Console.ReadKey(true).Key;

				switch (key.Value)
				{
					case ConsoleKey.Enter:
						Console.Clear();
						Console.WriteLine(rm.GetString("lets_battleships"));
						log.Info("New game started");
						Thread.Sleep(1500);
						winner = NewGame();
						break;
					case ConsoleKey.Escape:
						Console.Clear();
						Console.WriteLine("Exit");
						IsRunning = false;
						break;
				}
			}

			if (winner != null)
			{
                const int center = 12;
				PrintMessageOnBoard(rm.GetString("you_win_message"), winner.offset + center, 5);
				PrintMessageOnBoard(rm.GetString("you_loose_message"), winner.enemyOffset + center - 2, 5);
				PrintMessageOnBoard("Press [ESC] to exit...", winner.offset + 8, 6);
				while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
				IsRunning = false;
			}
		}

		public void Update()
		{
			if (player != null && enemy != null)
			{
				const int arrowXPosition = 28;
				const int arrowYPosition = 4;
				PrintMessageOnBoard("==>", arrowXPosition, arrowYPosition);
				if (player.IsAlive())
				{
					enemy.GetShot(player.Shoot());
					enemy.Update();
				}

				PrintMessageOnBoard("<==", arrowXPosition, arrowYPosition);

				if (enemy.IsAlive())
				{
					player.GetShot(enemy.Shoot());
					player.Update();
				}
			}
		}

		public static void PrintMessageOnBoard(string message, int x, int y)
		{
			Console.SetCursorPosition(x, y);
			Console.Write(message);
		}
		
		private void Draw()
		{
			if (player != null && enemy != null)
			{
				player.Draw();
				enemy.Draw();
			}
		}
		
		private void Welcome()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(rm.GetString("welcome_message1"));
			Console.ForegroundColor = DefaultColor;
			Console.WriteLine(rm.GetString("welcome_message2"));
			Console.WriteLine(rm.GetString("welcome_message3"));
		}

		private Player NewGame()
		{
			player = new Player(Player.PlayerType.Human);
			enemy = new Player(Player.PlayerType.Computer);
			Console.Clear();

			while (player.IsAlive() && enemy.IsAlive())
			{
				Draw();
				Update();
			}

			return player.IsAlive() ? player : enemy;
		}
	}
}
