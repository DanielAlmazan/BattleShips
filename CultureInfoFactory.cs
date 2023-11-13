using System.Globalization;

namespace BattleShips;

public static class CultureInfoFactory
{
    private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
    
    public static CultureInfo CreateCultureInfo(string? language)
    {
        try
        {
            return new CultureInfo(language);
        }
        catch (CultureNotFoundException)
        {
            log.Warn($"Culture {language} not found, using default culture");
            return new CultureInfo("en-US");
        }
    }
}