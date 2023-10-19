using System;

namespace BattleShips
{
    internal class Program
    {
	    private static Game? _game;
		static void Main(string[] args)
		{
			Console.CursorVisible = false;
			_game = new Game();
			Console.Clear();
			_game.Run();
			while (_game.isRunning)
			{
				_game.Update();
			}
		}
	}
}
