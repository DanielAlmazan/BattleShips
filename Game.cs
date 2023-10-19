namespace BattleShips
{
	internal class Game
	{
		#region Variables
		
		public static ConsoleColor defaultColor = Console.ForegroundColor;
		public static ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
		private Player? player;
		private Player? enemy;
		private Player? winner;
		public bool isRunning;

		#endregion

		public void Run()
		{
			isRunning = true;
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
						Console.WriteLine("Let's... BattleShipspsps");
						Thread.Sleep(1500);
						winner = NewGame();
						break;
					case ConsoleKey.Escape:
						Console.Clear();
						Console.WriteLine("Exit");
						isRunning = false;
						break;
				}
			}

			if (winner != null)
			{
				const int center = 12;
				PrintMessageOnBoard("YOU WIN!", winner.offset + center, 5);
				PrintMessageOnBoard("YOU LOOSE!", winner.enemyOffset + center - 2, 5);
				PrintMessageOnBoard("Press [ESC] to exit...", winner.offset + 8, 6);
				while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
				isRunning = false;
			}
		}

		public void Update()
		{
			if (player != null && enemy != null)
			{
				int arrowXPosition = 28;
				int arrowYPosition = 4;
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
			Console.WriteLine("Welcome to BattleShips!");
			Console.ForegroundColor = defaultColor;
			Console.WriteLine("Press ENTER to start...");
			Console.WriteLine("Press ESC to exit...");
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
