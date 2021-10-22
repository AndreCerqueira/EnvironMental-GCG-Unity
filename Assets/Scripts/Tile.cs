using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    // Basic Variables
    public int x, y;
    int _type;
    public Biome biome;

    public int type
    {
        get { 
            return _type; 
        }
        set {
            _type = value;
            if (_type < 100) // <- because of the city
                biome = GameData.biomes[_type];
            else if (_type == 100)
                biome = GameData.city;
        }
    }

    // Workers
    //public int player; <- player who sent the workers
    public int workers;


    public Tile(int _x, int _y)
    {
        x = _x;
        y = _y;
        _type = 0;
    }



}
