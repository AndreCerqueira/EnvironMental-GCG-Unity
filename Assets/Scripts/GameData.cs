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

    public static Biome city = new Biome("City", "Player 1 city");

    // Map Sizes
    public static int width = 20, height = 20;
}
