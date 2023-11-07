namespace BattleShips
{
	internal class Board
	{
		#region Variables
		private static readonly ConsoleColor wavesColor = ConsoleColor.White;
		private static readonly ConsoleColor shipPreviewColor = ConsoleColor.Gray;
		private static readonly ConsoleColor boardBackgroundColor = ConsoleColor.Blue;
		private readonly Ship[] ships = new Ship[4];
		private readonly int xOffset;
		private readonly Player player;
		private List<Coordinate?> shots;
		private List<Coordinate?> enemyShots;
		bool playing = false;
		#endregion

		public Board(Player player)
		{
			this.player = player;
			this.xOffset = player.IsHuman() ? 3 : 33;
			InitializeShips();
			Console.Clear();
			playing = true;
			shots = new List<Coordinate?>();
			enemyShots = new List<Coordinate?>();
		}

		/// <summary>
		/// Initializes the player's ships.
		/// </summary>
		public void InitializeShips()
		{
			Console.Clear();
			ships[0] = new Ship(this.player, 3);
			ships[1] = new Ship(this.player, 3);
			ships[2] = new Ship(this.player, 4);
			ships[3] = new Ship(this.player, 5);

			int xCursorPosition = this.xOffset + 1;
			int yCursorPosition = 1;

			byte shipsPlaced = 0;
			while (shipsPlaced < 4)
			{
				int boardWidth = 23 - ships[shipsPlaced].GetWidth() * 2;
				HandleBorderCollisions(ref xCursorPosition, ref yCursorPosition,
					shipsPlaced, boardWidth);

				DrawBoard();
				PrintInstructions();

				Console.SetCursorPosition(xCursorPosition, yCursorPosition);
				DrawShipPreview(ships[shipsPlaced]);

				Console.SetCursorPosition(xCursorPosition, yCursorPosition);

				ConsoleKey action;

				if (this.player.IsHuman())
				{
					action = Console.ReadKey(true).Key;
				}
				else
				{
					action = RandomAction();
				}

				HandleUserInput(action, ref xCursorPosition,
					ref yCursorPosition, ref shipsPlaced);
			}
			Console.SetCursorPosition(this.xOffset, 15);
			Console.WriteLine("Press [ENTER] to continue!");
			while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
		}

		/// <summary>
		/// Method for getting a random action for the computer.
		/// </summary>
		/// <returns>ConsoleKey</returns>
		private ConsoleKey RandomAction()
		{
			Random random = new Random();
			ConsoleKey action = new ConsoleKey();
			switch (random.Next(1, 7))
			{
				case 1:
					action = ConsoleKey.Spacebar;
					break;
				case 2:
					action = ConsoleKey.LeftArrow;
					break;
				case 3:
					action = ConsoleKey.RightArrow;
					break;
				case 4:
					action = ConsoleKey.UpArrow;
					break;
				case 5:
					action = ConsoleKey.DownArrow;
					break;
				case 6:
					action = ConsoleKey.Enter;
					break;
			}
			Thread.Sleep(200);
			return action;
		}

		/// <summary>
		/// Handles the border collisions of the boat preview when user is placing boats.
		/// </summary>
		/// <param name="xCursorPosition"></param>
		/// <param name="yCursorPosition"></param>
		/// <param name="shipsPlaced"></param>
		/// <param name="boardWidth"></param>
		private void HandleBorderCollisions(
			ref int xCursorPosition, ref int yCursorPosition,
			byte shipsPlaced, int boardWidth)
		{
			while (xCursorPosition >= this.xOffset + boardWidth + 2)
			{
				xCursorPosition -= 2;
			}

			while (xCursorPosition < this.xOffset + 2)
			{
				xCursorPosition += 2;
			}

			while (yCursorPosition > 11 - ships[shipsPlaced].GetHeight())
			{
				yCursorPosition--;
			}

			while (yCursorPosition < 1)
			{
				yCursorPosition++;
			}
		}

		/// <summary>
		/// Handles the user input when placing boats.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="xCursorPosition"></param>
		/// <param name="yCursorPosition"></param>
		/// <param name="shipsPlaced"></param>
		private void HandleUserInput(
			ConsoleKey action, ref int xCursorPosition,
			ref int yCursorPosition, ref byte shipsPlaced)
		{

			switch (action)
			{
				case ConsoleKey.LeftArrow:
					xCursorPosition -= 2;
					break;

				case ConsoleKey.RightArrow:
					xCursorPosition += 2;
					break;

				case ConsoleKey.UpArrow:
					yCursorPosition--;
					break;

				case ConsoleKey.DownArrow:
					yCursorPosition++;
					break;

				// Rotate Ship
				case ConsoleKey.Spacebar:
					ships[shipsPlaced].SetHorizontal(!ships[shipsPlaced].IsHorizontal());
					break;

				// Place Ship
				case ConsoleKey.Enter:
					if (IsValidPlacement(shipsPlaced))
					{
						ships[shipsPlaced].SetPlaced(true);
						shipsPlaced++;
					}
					else
					{
						Console.Clear();
						Game.PrintMessageOnBoard("You can't place a Ship there!", this.player.offset, 4);
						Game.PrintMessageOnBoard("Press [ENTER] to continue!", this.player.offset, 5);
						if (this.player.IsHuman())
						{
							while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
						}
						else
						{
							Thread.Sleep(500);
						}

						Console.Clear();
					}
					break;

				case ConsoleKey.Escape:
					break;
			}
			Console.SetCursorPosition(xCursorPosition, yCursorPosition);
		}

		/// <summary>
		/// Processes the enter key when placing boats.
		/// </summary>
		/// <param name="shipsPlaced"></param>
		private bool IsValidPlacement(byte shipsPlaced)
		{
			bool success = true;

			foreach (Ship ship in ships)
			{
				if (ship.IsPlaced())
				{
					if (ships[shipsPlaced].IsColliding(ship))
					{
						success = false;
					}
				}
			}
			return success;
		}

		/// <summary>
		/// Prints the instructions for placing boats.
		/// </summary>
		private void PrintInstructions()
		{
			int line = 11;
			Game.PrintMessageOnBoard("Place your Ships!", this.player.offset, line++);
			Game.PrintMessageOnBoard("Use the arrow keys to move the cursor.", this.player.offset, line++);
			Game.PrintMessageOnBoard("Press [SPACE] to rotate the Ship.", this.player.offset, line++);
			Game.PrintMessageOnBoard("Press [ENTER] to place a Ship.", this.player.offset, line++);
		}

		/// <summary>
		/// Updates the board.
		/// </summary>
		public void Update()
		{
			foreach (Ship ship in ships)
			{
				foreach (Coordinate coordinate in enemyShots)
				{
					ship.Update(coordinate);
				}
			}
		}

		/// <summary>
		/// Draws the board.
		/// </summary>
		public void DrawBoard()
		{
			DrawWaves();
			
			if (enemyShots != null)
			{
				DrawShots();
			}
			
			foreach (Ship ship in ships)
			{
				ship.Draw(playing);
			}
		}

		/// <summary>
		/// Draws the waves.
		/// </summary>
		public void DrawWaves()
		{
			char wave = '~';

			Console.SetCursorPosition(this.xOffset, 0);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("   ");
			for (int i = 0; i < 10; i++)
			{
				Console.Write(i + 1);
				if (i < 9)
				{
					Console.Write(" ");
				}
			}

			Console.WriteLine();			
			for (int i = 0; i < 10; i++)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.SetCursorPosition(this.xOffset, Console.CursorTop);
				Console.Write($"{(char)('A' + i)} |");
				for (int j = 0; j < 10; j++)
				{
					Console.ForegroundColor = wavesColor;
					Console.BackgroundColor = boardBackgroundColor;
					Console.Write($"{wave} ");
					Console.ForegroundColor = Game.DefaultColor;
					Console.BackgroundColor = Game.DefaultBackgroundColor;
				}
				Console.WriteLine();
			}
		}

		/// <summary>
		/// Draws the ship preview when placing the ships.
		/// </summary>
		/// <param name="ship"></param>
		public void DrawShipPreview(Ship ship)
		{
			(int xCursorPosition, int yCursorPosition) = Console.GetCursorPosition();

			Console.ForegroundColor = shipPreviewColor;
			Console.BackgroundColor = boardBackgroundColor;
			if (ship.IsHorizontal())
			{
				Console.SetCursorPosition(xCursorPosition, yCursorPosition);

				for (int i = 0; i < ship.GetLength(); i++)
				{
					ship.SetShipParts(new Coordinate(xCursorPosition, yCursorPosition), i);
					Console.Write($"{Ship.SHIP_PART_CHAR} ");
					xCursorPosition += 2;
				}
			}
			else
			{
				for (int i = 0; i < ship.GetLength(); i++)
				{
					Console.SetCursorPosition(xCursorPosition, yCursorPosition + i);
					ship.SetShipParts(new Coordinate(xCursorPosition, yCursorPosition + i), i);
					Console.Write(Ship.SHIP_PART_CHAR);
				}
			}
			Console.ForegroundColor = Game.DefaultColor;
			Console.BackgroundColor = Game.DefaultBackgroundColor;
		}

		/// <summary>
		/// Draws the failed shots.
		/// </summary>
		public void DrawShots()
		{
			Console.BackgroundColor = boardBackgroundColor;
			foreach (Coordinate coordinate in enemyShots)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Game.PrintMessageOnBoard("X", coordinate.row, coordinate.column);
				Console.ForegroundColor = Game.DefaultColor;
			}
			Console.BackgroundColor = Game.DefaultBackgroundColor;
		}

		/// <summary>
		/// Gets the shot. It is used for checking if the shot is valid.
		/// </summary>
		/// <param name="position"></param>
		/// <returns>bool</returns>
		public bool GetShot(Coordinate? position)
		{
			bool success = false;
			if (position is not null && !shots.Contains(position))
			{
				success = true;
				shots.Add(position);
			}
			return success;
		}

		/// <summary>
		/// Gets the shot from the player.
		/// </summary>
		/// <param name="position"></param>
		public void GetEnemyShot(Coordinate? position)
		{
			enemyShots.Add(position);
			foreach (Ship ship in ships)
			{
				ship.Update(position);
			}
		}

		/// <summary>
		/// Checks if all boats are sunk.
		/// </summary>
		/// <returns>bool</returns>
		public bool AllBoatsSunk()
		{
			return ships
				.ToList()
				.TrueForAll(ship => ship.IsSunk());
		}
	}
}
