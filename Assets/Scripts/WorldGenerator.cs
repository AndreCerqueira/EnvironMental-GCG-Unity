using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // Variables
    public int width, height;
    public TileBase[] tiles;
    public Tilemap tileMap;
    public int[,] map;

    void Start()
    {
        Generation();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Generation();
        }
    }

    void Generation()
    {
        tileMap.ClearAllTiles();
        map = GenerateArray(width, height, true);
        map = TerrainGeneration(map);
        RenderMap(map, tileMap);
    }

    public int[,] GenerateArray(int width, int height, bool empty) 
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = (empty) ? 0 : 1;
            }
        }

        return map;
    }

    public int[,] TerrainGeneration(int[,] map) 
    {
        float seed = Random.Range(0f, 100000f);
        // seed = 0.2f

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);        

                float AB = Mathf.PerlinNoise((x + seed) * 0.1f, (y + seed) * 0.2f);
                float BA = Mathf.PerlinNoise((x + seed) * 0.1f, (y + seed) * 0.2f);

                float perlin = (AB + BA) / 2f;

                if (perlin < 0.30f) // Forest
                    map[x, y] = 2;
                else if (perlin < 0.55f) // Plains
                    map[x, y] = 1;
                else if (perlin < 0.6f) // River
                    map[x, y] = 0;
                else if (perlin < 0.8f) // Plains
                    map[x, y] = 1;
                else
                    map[x, y] = 3; // Desert

            }
        }

        return map;
    }

    public void RenderMap(int[,] map, Tilemap groundTileMap) 
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] <= 4) 
                {
                    groundTileMap.SetTile(new Vector3Int(x, y, 0), tiles[map[x, y]]);
                }
            }
        }
    }
























    /*
    public int analyzeAdjacents(int x, int y) 
    {
        // Get the current tile
        int tileID = map[x, y];

        // Get the adjacents tiles

        int[] adjacents = { (x     >= 0 && x     < width && y + 1 >= 0 && y + 1 < height) ? map[x    , y + 1] : 0,
                            (x - 1 >= 0 && x - 1 < width && y + 1 >= 0 && y + 1 < height) ? map[x - 1, y + 1] : 0,
                            (x - 1 >= 0 && x - 1 < width && y     >= 0 && y     < height) ? map[x - 1, y    ] : 0,
                            (x + 1 >= 0 && x + 1 < width && y     >= 0 && y     < height) ? map[x + 1, y    ] : 0,
                            (x - 1 >= 0 && x - 1 < width && y - 1 >= 0 && y - 1 < height) ? map[x - 1, y - 1] : 0,
                            (x     >= 0 && x     < width && y - 1 >= 0 && y - 1 < height) ? map[x    , y - 1] : 0
        };

        
    map[x    , y + 1],
                              map[x - 1, y + 1],
                              map[x - 1, y    ],
                              map[x + 1, y    ],
                              map[x - 1, y - 1],
                              map[x    , y - 1]
         

        // Check if every tile is different from the current
        bool change = true;
        for (int i = 0; i < adjacents.Length && change; i++)
        {
            if (tileID == adjacents[i])
                change = false;
        }

        // Change in order to never have a loose tile
        return (change) ? adjacents[Random.Range(0, 5)] : tileID;

    }*/

}
