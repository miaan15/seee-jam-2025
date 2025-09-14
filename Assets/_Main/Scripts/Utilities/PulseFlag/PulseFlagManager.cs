using System;
using System.Reflection;

public static class PulseFlagManager
{
    public const int MaxDepth = 16;

    public static void Update(object obj, int depth = 0)
    {
        if (obj == null) return;

        var type = obj.GetType();

        if (!type.IsClass || type == typeof(string)) return;

        foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            var val = field.GetValue(obj);
            if (val == null) continue;

            if (val is PulseFlag pulseFlagField)
            {
                pulseFlagField.Deactivate();
            }
            else
            {
                Update(val, depth + 1);
            }
        }
    }
}
