using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorHelper
{
    public static string ToColorName(this Color color)
    {
        if(color == Color.black) return "black";
        else if (color == Color.blue) return "blue";
        else if (color == Color.clear) return "clear";
        else if (color == Color.cyan) return "cyan";
        else if (color == Color.gray) return "gray";
        else if (color == Color.green) return "green";
        else if (color == Color.magenta) return "magenta";
        else if (color == Color.red) return "red";
        else if (color == Color.white) return "white";
        else if (color == new Color(1F, 1F, 0F, 1F)) return "yellow";
        else return "";
    }

    public static Color NextColor(this Color color)
    {
        if (color == Color.red) return Color.green;
        else if (color == Color.green) return Color.blue;
        else if (color == Color.blue) return Color.magenta;
        else if (color == Color.magenta) return new Color(1F, 1F, 0F, 1F);
        else if (color == new Color(1F, 1F, 0F, 1F)) return Color.cyan;
        else if (color == Color.cyan) return Color.red;
        else return Color.white;
    }

    public static Color PreviousColor(this Color color)
    {
        if (color == Color.green) return Color.red;
        else if (color == Color.blue) return Color.green;
        else if (color == Color.magenta) return Color.blue;
        else if (color == new Color(1F, 1F, 0F, 1F)) return Color.magenta;
        else if (color == Color.cyan) return new Color(1F, 1F, 0F, 1F);
        else if (color == Color.red) return Color.cyan;
        else return Color.white;
    }
}
