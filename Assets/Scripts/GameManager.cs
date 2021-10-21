using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // Enum
    public enum Window { Character, TileDetails, Crafting, Happiness, Options };

    // Canvas Variables
    CanvasGroup tileDetailsWindow;
    CanvasGroup characterWindow;
    CanvasGroup craftingWindow;
    CanvasGroup happinessWindow;
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

        characterWindow = GameObject.Find("Character Window").GetComponent<CanvasGroup>();
        craftingWindow = GameObject.Find("Crafting Window").GetComponent<CanvasGroup>();
        happinessWindow = GameObject.Find("Happiness Window").GetComponent<CanvasGroup>();

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


    public void showWindow(int window)
    {
        CanvasGroup windowObject;

        switch (window)
        {
            case 0:
                windowObject = tileDetailsWindow;
                break;
            case 1:
                windowObject = characterWindow;
                break;
            case 2:
                windowObject = craftingWindow;
                break;
            case 3:
                windowObject = happinessWindow;
                break;
            default:
                windowObject = null;
                break;
        }

        StartCoroutine(DoFadeIn(windowObject));
    }


    public void closeWindow(int window)
    {
        CanvasGroup windowObject;

        switch (window)
        {
            case 0:
                windowObject = tileDetailsWindow;
                break;
            case 1:
                windowObject = characterWindow;
                break;
            case 2:
                windowObject = craftingWindow;
                break;
            case 3:
                windowObject = happinessWindow;
                break;
            default:
                windowObject = null;
                break;
        }

        StartCoroutine(DoFadeOut(windowObject));
    }


    static public IEnumerator DoFadeOut(CanvasGroup canvasG)
    {
        while (canvasG.alpha > 0)
        {
            canvasG.alpha -= Time.deltaTime * 4;
            yield return null;
        }

        canvasG.interactable = false;
        canvasG.blocksRaycasts = false;
    }

    static public IEnumerator DoFadeIn(CanvasGroup canvasG)
    {
        while (canvasG.alpha < 1)
        {
            canvasG.alpha += Time.deltaTime * 4;
            yield return null;
        }

        canvasG.interactable = true;
        canvasG.blocksRaycasts = true;
    }
}
