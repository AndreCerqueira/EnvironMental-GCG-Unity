using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Biome[] biomes = { 
        new Biome("Ocean", "null"),
        new Biome("Plains", "Good for crops"),
        new Biome("Forest", "null"),
        new Biome("Desert", "null")
    };

    // Map Sizes
    public static int width = 20, height = 20;
}
