namespace BattleShips
{
	readonly struct Coordinate
	{
		public readonly int row;
		public readonly int column;

		public Coordinate(int row, int column)
		{
			this.row = row;
			this.column = column;
		}

		// Casi todo esto lo generó el IntelliSense, aunque algún cambio hice

		public static bool operator ==(Coordinate a, Coordinate b)
		{
			// Aquí concreté, no es el código que generó el IntelliSense
			return a.row == b.row && a.column == b.column;
		}

		public static bool operator !=(Coordinate a, Coordinate b)
		{
			// Y aquí también
			return !(a.row == b.row && a.column == b.column);
		}

		public override bool Equals(object? obj)
		{
			if (obj is Coordinate otherCoord)
			{
				return this.row == otherCoord.row && this.column == otherCoord.column;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return row.GetHashCode() ^ column.GetHashCode();
		}

		// Hasta aquí lo generó el IntelliSense. #sinceridad

		public override string ToString()
		{
			return $"Row: {row} | Column: {column}";
		}
	}

	struct ShipPart
		{
			public bool isPartSunk;
			public Coordinate position;

			public ShipPart(Coordinate coordinates)
			{
				isPartSunk = false;
				position = coordinates;
			}
		}
	internal class Ship
	{

		#region Variables
		private const ConsoleColor shipPartColor = ConsoleColor.Black;
		private const ConsoleColor hitShipPartColor = ConsoleColor.DarkCyan;
		private bool isPlaced;
		private bool isHorizontal = true;
		private int width;
		private int height;
		private readonly ShipPart[] shipParts;
		private readonly Player player;
		public const char SHIP_PART_CHAR = '∎';
		#endregion

		public Ship(Player player, int length)
		{
			this.player = player;
			this.shipParts = new ShipPart[length];
			this.width = length;
			this.height = 1;
			for (int i = 0; i < shipParts.Length; i++)
			{
				shipParts[i] = new ShipPart(new Coordinate(i, 0));
			}
		}

		public void Update(Coordinate? coordinates)
		{
			for (int i = 0; i < shipParts.Length; i++)
			{
				if (coordinates == shipParts[i].position)
				{
					shipParts[i].isPartSunk = true;
				}
			}
		}

		public void Draw(bool playing)
		{
			PrintShip(playing);
		}

		public void PrintShip(bool playing)
		{
			foreach (ShipPart shipPart in shipParts)
			{
				Console.BackgroundColor = ConsoleColor.Blue;
				if (isPlaced)
				{
					Console.SetCursorPosition
						(shipPart.position.row, shipPart.position.column);
					
					Console.ForegroundColor =
						!shipPart.isPartSunk ? 
						shipPartColor :
						hitShipPartColor;
						
					bool drawShipPart = 
						player.IsHuman() || 
						(!player.IsHuman() && shipPart.isPartSunk) ||
						(!player.IsHuman() && !playing);
						
					if (drawShipPart)
					{
						Console.Write(SHIP_PART_CHAR);
					}

					Console.ForegroundColor = Game.defaultColor;
				}
			}
			Console.BackgroundColor = Game.defaultBackgroundColor;
		}

		public bool IsColliding(Ship otherShip)
		{
			bool isColliding = false;

			foreach (ShipPart ShipPart in this.shipParts)
			{
				foreach (ShipPart otherShipPart in otherShip.shipParts)
				{
					if (ShipPart.position == otherShipPart.position)
					{
						isColliding = true;
					}
				}
			}

			return isColliding;
		}

		public void SetShipParts(Coordinate position, int shipPart)
		{
			shipParts[shipPart].position = position;
		}

		public bool IsPlaced() { return this.isPlaced; }
		public void SetPlaced(bool isPlaced) { this.isPlaced = isPlaced; }
		public bool IsHorizontal() { return this.isHorizontal; }
		public void SetHorizontal(bool isHorizontal)
		{
			if (isHorizontal)
			{
				this.width = shipParts.Length;
				this.height = 1;
			}
			else
			{
				this.width = 1;
				this.height = shipParts.Length;
			}
			this.isHorizontal = isHorizontal;
		}

		public int GetWidth() { return width; }

		public int GetHeight() { return height; }

		public int GetLength() { return shipParts.Length; }

		public bool IsSunk()
		{
			return shipParts
				.ToList()
				.TrueForAll(shipPart => shipPart.isPartSunk);
		}
	}
}
