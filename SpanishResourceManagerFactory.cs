using System.Reflection;

namespace BattleShips
{
    internal class SpanishResourceManagerFactory : IResourceManagerFactory
    {
        public System.Resources.ResourceManager CreateResourceManager()
        {
            return new System.Resources.ResourceManager("BattleShips.Resources.Strings_es", Assembly.GetExecutingAssembly());
        }
    }
}
