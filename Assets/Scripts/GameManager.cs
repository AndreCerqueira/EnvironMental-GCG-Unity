using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // Canvas Variables
    CanvasGroup tileDetailsWindow;
    Text tileDetailsTitle;
    Text tileDetailsDetails;

    // Game Variables
    public Biome[] biomes;
    TileBase[] tiles;
    Tilemap tileMap;
    WorldGenerator worldGenerator;
    
    // Use this for initialization
    void Start()
    {
        // Get data
        worldGenerator = GameObject.Find("Grid/Tilemap").GetComponent<WorldGenerator>();
        tileDetailsWindow = GameObject.Find("Tile Details Window").GetComponent<CanvasGroup>();
        tileDetailsTitle = GameObject.Find("Tile Details Window/Title").GetComponent<Text>();
        tileDetailsDetails = GameObject.Find("Tile Details Window/Details").GetComponent<Text>();

        // Set data
        tileMap = worldGenerator.GetComponent<Tilemap>();
        tiles = worldGenerator.tiles;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = tileMap.WorldToCell(mouseWorldPos);
        
        if (Input.GetMouseButtonDown(0))
        {
            //tileMap.SetTile(new Vector3Int(coordinate.x, coordinate.y, 0), tiles[3]);
            tileDetailsWindow.alpha = 1;
            tileDetailsWindow.interactable = true;

            // The tile type
            tileDetailsTitle.text = biomes[worldGenerator.map[coordinate.x, coordinate.y]].biomeName;
            tileDetailsDetails.text = biomes[worldGenerator.map[coordinate.x, coordinate.y]].biomeDetails;
        }
        /**/
    }
}
