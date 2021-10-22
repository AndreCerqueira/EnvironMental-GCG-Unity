using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // Variables
    int width, height;
    public TileBase[] tiles;
    public TileBase tileCity;
    public Tilemap tileMap;
    Vector2Int cityPos;
    public Tile[,] map;

    void Start()
    {
        // Set data
        width = GameData.width;
        height = GameData.height;

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
        generateArray(width, height, true);
        placeWater();
        terrainGeneration();
        renderMap();
    }

    public void placeWater() 
    {
        for (int x = -17; x < width+17; x++)
        {
            for (int y = -17; y < height+17; y++)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), tiles[0]);
            }
        }
        
    }

    public void generateArray(int width, int height, bool empty)
    {
        map = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = new Tile();
            }
        }

    }


    public void terrainGeneration() 
    {
        // Get seed
        float seed = Random.Range(0f, 100000f); // 0.2f

        // Generate the world
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                Vector3 pos = new Vector3(x, y, 0);        

                float AB = Mathf.PerlinNoise((x + seed) * 0.1f, (y + seed) * 0.2f);
                float BA = Mathf.PerlinNoise((x + seed) * 0.1f, (y + seed) * 0.2f);

                float perlin = (AB + BA) / 2f;

                if (perlin < 0.30f) // Forest
                    map[x, y].type = 2;
                else if (perlin < 0.55f) // Plains
                    map[x, y].type = 1;
                else if (perlin < 0.6f) // River
                    map[x, y].type = 0;
                else if (perlin < 0.8f) // Plains
                    map[x, y].type = 1;
                else
                    map[x, y].type = 3; // Desert

            }
        }

        // Place the city
        cityPos = new Vector2Int(
                    Random.Range(5, width - 5), 
                    Random.Range(5, width - 5));
        map[cityPos.x, cityPos.y].type = 100;

    }

    public void renderMap() 
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y].type < tiles.Length) 
                {
                    tileMap.SetTile(new Vector3Int(x, y, 0), tiles[map[x, y].type]);
                }
                else
                {
                    tileMap.SetTile(new Vector3Int(x, y, 0), tileCity);
                }
            }
        }
    }


    // Return the position of the city in world space scale
    public Vector3 getCityPosition() 
    {
        return tileMap.CellToWorld(
            new Vector3Int(cityPos.x, cityPos.y, -10));
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
