using System.Reflection;

namespace BattleShips;

public class EnglishResourceManagerFactory : IResourceManagerFactory
{
	public System.Resources.ResourceManager CreateResourceManager()
	{
		return new System.Resources.ResourceManager("BattleShips.Resources.Strings_en", Assembly.GetExecutingAssembly());
	}
}