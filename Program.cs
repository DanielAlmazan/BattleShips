using log4net;
using log4net.Config;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace BattleShips
{
    internal class Program
    {
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	    
		private static Game? _game;
		static void Main(string[] args)
		{
			string json = File.ReadAllText("config.json");
			Configuration config = JsonConvert.DeserializeObject<Configuration>(json);

			if (config == null)
			{
				Log.Error("Failed to load config.json");
				throw new InvalidDataException();
			}

			var res_lang = new ResourceManager(config.Language);
			CultureInfo culture = CultureInfoFactory.CreateCultureInfo(config.Language);
			Thread.CurrentThread.CurrentUICulture = culture;

			Log.Info("Starting BattleShips");
			Console.CursorVisible = false;
			_game = new Game(config);
			Console.Clear();
			_game.Run();

			Log.Info("Exiting BattleShips");
			while (_game.IsRunning)
			{
				_game.Update();
			}
		}
	}
}
