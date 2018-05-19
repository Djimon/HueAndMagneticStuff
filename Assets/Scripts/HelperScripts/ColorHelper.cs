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
        else if (color == Color.yellow) return "yellow";
        else return "";
    }
}
