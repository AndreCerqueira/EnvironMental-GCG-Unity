using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // Canvas Variables
    CanvasGroup tileDetailsWindow;
    CanvasGroup characterWindow;
    CanvasGroup craftingWindow;
    CanvasGroup happinessWindow;
    Text tileDetailsTitle;
    Text tileDetailsDetails;

    // Game Variables
    CameraController cam;
    TileBase[] tiles;
    Tilemap tileMap;
    WorldGenerator worldGenerator;
    Vector3 lastMouseCoordinate = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        // Get data
        cam = Camera.main.GetComponent<CameraController>();
        worldGenerator = GameObject.Find("Grid/Tilemap").GetComponent<WorldGenerator>();
        tileDetailsWindow = GameObject.Find("Tile Details Window").GetComponent<CanvasGroup>();
        tileDetailsTitle = GameObject.Find("Tile Details Window/Title").GetComponent<Text>();
        tileDetailsDetails = GameObject.Find("Tile Details Window/Description").GetComponent<Text>();
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

        if (Input.GetMouseButtonUp(0) && !cam.cameraMoving)
        {
            // Show the window
            if (!tileDetailsWindow.interactable) 
            { 
                tileDetailsWindow.alpha = 1;
                tileDetailsWindow.interactable = true;
                tileDetailsWindow.blocksRaycasts = true;
            }

            // Get the tile info
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coords = tileMap.WorldToCell(mouseWorldPos);
            
            tileDetailsTitle.text = GameData.biomes[worldGenerator.map[coords.x, coords.y].type].name;
            tileDetailsDetails.text = GameData.biomes[worldGenerator.map[coords.x, coords.y].type].details;
        }


    }

    //----------------------------------------------- Button Events -----------------------------------------------\\

    public void collect() 
    {
        // lose total workers

        // place workers in the tile

        // the tile change apearance

        // change the button to return button

        // 
    }

    public void showWindow(int window)
    {
        CanvasGroup windowObject = getWindowByID(window);

        StartCoroutine(DoFadeIn(windowObject));
    }


    public void closeWindow(int window)
    {
        CanvasGroup windowObject = getWindowByID(window);

        StartCoroutine(DoFadeOut(windowObject));
    }


    CanvasGroup getWindowByID(int id) 
    {
        switch(id)
        {
            case 0:
                return tileDetailsWindow;
            case 1:
                return characterWindow;
            case 2:
                return craftingWindow;
            case 3:
                return happinessWindow;
            default:
                return null;
        }
    }

   
    //----------------------------------------------- Animations -----------------------------------------------\\
    

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
