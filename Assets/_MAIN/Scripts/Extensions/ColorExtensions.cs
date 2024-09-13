using UnityEngine;

public static class ColorExtensions
{
    public static Color SetAlpha(this Color original, float alpha)
    {
        return new Color(original.r, original.g, original.b, alpha);
    }

    public static Color GetColorFromName(this Color original, string colorName)
    {
        switch (colorName.ToLower())
        {
            case "red": return Color.red;
            case "orange": return new Color(1f, 0.5f, 0.5f);
            //TODO add more colors as needed
            default:
                Debug.LogWarning("Unrecognized color name: " + colorName);
                return Color.clear;
        }
    }
}
