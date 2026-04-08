using System;
using System.Linq;


public static class EnumUtils
{
    // o_O
    // merci a l'IA ? j'ai aucun credit pour ce bout de code
    // j'ai un peu appris du coup, merci a elle... youhou !
    public static int MaxIntValue<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Max(value => Convert.ToInt32(value));
    }
}

