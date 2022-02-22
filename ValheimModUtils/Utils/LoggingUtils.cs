namespace MilkWyzardStudios.Valheim.Utils
{
    public static class LoggingUtils
    {
        public static bool WarnIfNull(object obj, string objectName="")
        {
            bool isNull = obj == null;
            if (isNull)
                Jotunn.Logger.LogWarning(objectName + " Object is null.");
            return isNull;
        }
    }
}
