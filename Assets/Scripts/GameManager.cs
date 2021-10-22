using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // Canvas Variables
    CanvasGroup tileDetailsWindow;
    CanvasGroup cityDetailsWindow;
    CanvasGroup characterWindow;
    CanvasGroup craftingWindow;
    CanvasGroup happinessWindow;
    Text tileDetailsTitle;
    Text tileDetailsDescription;
    Text tileWorkStatus;

    // Game Variables
    CameraController cam;
    Player player;
    Tile selectedTile;
    Tilemap tileMap;
    WorldGenerator worldGenerator;

    // Use this for initialization
    void Start()
    {
        // Get data
        cam = Camera.main.GetComponent<CameraController>();
        player = GetComponent<Player>();

        // Get canvas data
        worldGenerator = GameObject.Find("Grid/Tilemap").GetComponent<WorldGenerator>();
        tileDetailsWindow = GameObject.Find("Tile Details Window").GetComponent<CanvasGroup>();
        tileDetailsTitle = GameObject.Find("Tile Details Window/Title").GetComponent<Text>();
        tileDetailsDescription = GameObject.Find("Tile Details Window/Description").GetComponent<Text>();
        tileWorkStatus = GameObject.Find("Tile Details Window/Work status").GetComponent<Text>();
        cityDetailsWindow = GameObject.Find("City Details Window").GetComponent<CanvasGroup>();
        characterWindow = GameObject.Find("Character Window").GetComponent<CanvasGroup>();
        craftingWindow = GameObject.Find("Crafting Window").GetComponent<CanvasGroup>();
        happinessWindow = GameObject.Find("Happiness Window").GetComponent<CanvasGroup>();

        // Set data
        tileMap = worldGenerator.GetComponent<Tilemap>();
    }


    // Update is called once per frame
    void Update()
    {

        // Click on a tile
        if (Input.GetMouseButtonUp(0) && !cam.cameraMoving)
        {
            // Get selected tile
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coords = tileMap.WorldToCell(mouseWorldPos);
            selectedTile = worldGenerator.map[coords.x, coords.y];

            getTileInfo();
        }

    }

    public void getTileInfo() 
    {
        


        if (selectedTile.biome.name == "City") 
        {
            // Show the window
            if (!cityDetailsWindow.interactable)
            {
                cityDetailsWindow.alpha = 1;
                cityDetailsWindow.interactable = true;
                cityDetailsWindow.blocksRaycasts = true;
            }
        }
        else
        {
            // Show the window
            if (!tileDetailsWindow.interactable)
            {
                tileDetailsWindow.alpha = 1;
                tileDetailsWindow.interactable = true;
                tileDetailsWindow.blocksRaycasts = true;
            }

            tileDetailsTitle.text = selectedTile.biome.name;
            tileDetailsDescription.text = selectedTile.biome.description;
            tileWorkStatus.text = selectedTile.workers + " Workers";
        }
        
    }

    //----------------------------------------------- Button Events -----------------------------------------------\\

    public void collect() 
    {
        // Send Workers
        player.sendWorkers(selectedTile);

        // Change tile apearance
        tileMap.SetTile(new Vector3Int(selectedTile.x, selectedTile.y, 0), worldGenerator.tileWorkers);

        // Change the button to return button

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
            case 4:
                return cityDetailsWindow;
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
