using System.Text.RegularExpressions;

namespace BattleShips
{
	internal class Player
	{
		// Enum for player type
		public enum PlayerType { Human, Computer }
		#region Variables
		private readonly PlayerType playerType;
		private readonly Board board;
		private bool isAlive = true;
		public readonly int offset;
		public readonly int enemyOffset;
		#endregion
		
		/// <summary>
		/// Constructor for Player
		/// </summary>
		/// <param name="playerType"></param>
		public Player(PlayerType playerType)
		{
			this.playerType = playerType;
			if (this.IsHuman())
			{
				offset = 0;
				enemyOffset = 30;
			}
			else
			{
				offset = 30;
				enemyOffset = 0;
			}

			board = new Board(this);
		}

		public void Update()
		{
			isAlive = !board.AllBoatsSunk();
			Draw();
			board.Update();
		}

		public void Draw()
		{
			board.DrawBoard();
		}

		public Coordinate? Shoot()
		{
			ConsoleKey? key = null;
			string input = "";
			Coordinate? shotCoordinates = null;

			while (shotCoordinates is null || key != ConsoleKey.Enter)
			{
				Console.CursorVisible = true;
				Game.PrintMessageOnBoard("Enter coordinates: " + input, 0, 12);
				if (this.IsHuman())
				{
					key = Console.ReadKey(true).Key;
				}
				else
				{
					key = RandomKey(input);
				}

				
				shotCoordinates = ProcessKey(ref key, ref input, shotCoordinates);
				if (!this.board.GetShot(shotCoordinates))
				{
					shotCoordinates = null;
				}
			}
			Console.CursorVisible = false;
			this.board.GetShot(shotCoordinates);
			return shotCoordinates;
		}

		private ConsoleKey RandomKey(string input)
		{
			Random compu = new Random();
			int key = 0;

			switch (input.Length)
			{
				case 0:
					key = compu.Next(65, 74); // A-J
					break;
				case 1:
					key = compu.Next(49, 58); // 1-9
					break;
				case 2:
					key = compu.Next(48, 50) == 0 ? 48 : 13; // 0 or Enter
					Thread.Sleep(500);
					break;
			}

			Thread.Sleep(250);
			return (ConsoleKey)key;
		}

		private Coordinate? ProcessKey(ref ConsoleKey? key, ref string input, Coordinate? shotCoordinates)
		{
			if (key == ConsoleKey.Backspace && input.Length > 0)
			{
				input = RemoveInput(1, input);
			}
			else if (
				key == ConsoleKey.Enter)
			{
				Regex regex = new Regex(@"^[a-j]([0-9]|10)$");
				if (regex.IsMatch(input))
				{
					int column = Convert.ToInt32(input[0]) - 96;

					int row = Convert.ToInt32(input.Substring(1));
					row = HumanCoordToComputerCoord(row) +
						this.enemyOffset + 3;

					shotCoordinates = new Coordinate(row, column);

					input = RemoveInput(input.Length, input);
				}
				else
				{
					key = null;
				}
			}
			else
			{
				SumKeyToInput(ref key, ref input);
			}

			return shotCoordinates;
		}
		
		private void SumKeyToInput(ref ConsoleKey? key, ref string input)
		{
			if (key >= ConsoleKey.A && key <= ConsoleKey.J && input.Length < 1)
			{
				input += key.ToString()!.ToLower();
			}
			else if (key >= ConsoleKey.D1 && key <= ConsoleKey.D9 && input.Length == 1)
			{
				input += key.ToString()!.ToLower()[1];
			}
			else if (key == ConsoleKey.D0 && input.Length == 2 && input[1] == '1')
			{
				input += key.ToString()!.ToLower()[1];
			}
		}

		private static string RemoveInput(int length, string input)
		{
			int currentLeft = Console.CursorLeft;
			Console.CursorLeft = currentLeft - length;
			Console.Write(new string(' ', length));
			Console.CursorLeft = currentLeft - length;
			input = input.Substring(0, input.Length - length);

			return input;
		}

		private static int HumanCoordToComputerCoord(int humanCoord)
		{
			return humanCoord * 2 + 1;
		}
		
		public bool IsHuman() { return playerType == PlayerType.Human; }
		
		public bool IsAlive() { return isAlive; }

		public void GetShot(Coordinate? coordinates)
		{
			board.GetEnemyShot(coordinates);
		}
	}
}
