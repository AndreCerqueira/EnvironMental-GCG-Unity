using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    CanvasGroup warningWindow;
    CanvasGroup inventoryWindow;
    Text tileDetailsTitle;
    Text tileDetailsDescription;
    Text tileWorkStatus;

    // Game Variables
    CameraController cam;
    Player player;
    Tile selectedTile;
    Tilemap tileMap;
    WorldGenerator worldGenerator;
    int romanCont = 0;
    
    //public Dictionary<string, string> events = new Dictionary<string, string>();

    // Use this for initialization
    void Start()
    {
        //events.Add("WildFire", "");
        //events.Add("WildFire", "");
            
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
        warningWindow = GameObject.Find("Warning").GetComponent<CanvasGroup>();
        inventoryWindow = GameObject.Find("Inventory Window").GetComponent<CanvasGroup>();

        // Set data
        tileMap = worldGenerator.GetComponent<Tilemap>();
    }


    // Update is called once per frame
    void Update()
    {

        // Click on a tile
            if (Input.GetMouseButtonUp(0) && !cam.cameraMoving && !EventSystem.current.IsPointerOverGameObject())
            {
                // Get selected tile
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int coords = tileMap.WorldToCell(mouseWorldPos);
                selectedTile = worldGenerator.map[coords.x, coords.y];

                getTileInfo();
            }

            //if (Input.GetMouseButtonDown(0))
                //romanCont++;
            

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

    #region Button Events

    public IEnumerator romanEasterEgg()
    {
        
        yield return new WaitForSeconds(1);
        
        Debug.Log(romanCont);
        
        if (romanCont > 5)
            showingRomanFace();
    }

    public void showingRomanFace()
    {
        GameObject.Find("Character/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Roman");;
    }
    
    public void collect()
    {
        Text buttonName = GameObject.Find("Collect/Text").GetComponent<Text>();
        if (buttonName.text == "Collect")
        {
            // Send Workers
            player.sendWorkers(selectedTile);

            // Change tile apearance
            tileMap.SetTile(new Vector3Int(selectedTile.x, selectedTile.y, 0), worldGenerator.tileWorkers);

            // Change the button to return button
            GameObject.Find("Collect/Text").GetComponent<Text>().text = "Return";
        }
        else
        {
            // Return Workers
            player.returnWorkers(selectedTile);
            
            // Add to the inventory
            Inventario inventory = GameObject.Find("Inventory Window/Scroll View/Viewport/Content").GetComponent<Inventario>();
            inventory.adicionarNoInventario("Wood", 10);
            
            // Change tile apearance
            tileMap.SetTile(new Vector3Int(selectedTile.x, selectedTile.y, 0), 
                worldGenerator.tiles[worldGenerator.map[selectedTile.x, selectedTile.y].type]);
            
            // Change the button to collect button
            GameObject.Find("Collect/Text").GetComponent<Text>().text = "Collect";
        }
        
    }
    
    public void skipTurn() 
    {
        // hide the button
        
        // check the events
        
        // show warning
        Text text = warningWindow.GetComponentInChildren<Text>();
        StartCoroutine(DoFadeIn(warningWindow));
        StartCoroutine(CloseWarning());
        
        
        // check the events
        
        if (checkWildFire(getProbabilities(player.total).environmental)) // <-- put the bar value here
        {
            text.text = "A wildfire started!";
        }
        else
        {
            text.text = "Its your turn!";
        }
        
    }

    public void showWindow(int window)
    {
        CanvasGroup windowObject = getWindowByID(window);

        //romanCont = 0;
        if (window == 1)
            StartCoroutine(romanEasterEgg());
        
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
            case 5:
                return inventoryWindow;
            default:
                return null;
        }
    }
    #endregion
   
    //----------------------------------------------- Animations -----------------------------------------------\\

    #region Animations
    
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
    
    public IEnumerator CloseWarning()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(DoFadeOut(warningWindow));
    }
    #endregion
    
    //----------------------------------------------- Events -----------------------------------------------\\
    
    
    public Player.Sustainability getProbabilities(Player.Sustainability sustainability)
    {
        float economicProb = 1 - Mathf.Log(sustainability.economic * 1.7f / 100 + 1);
        float environmentalProb = 1 - Mathf.Log(sustainability.environmental * 1.7f / 100 + 1);
        float humanProb = 1 - Mathf.Log(sustainability.human * 1.7f / 100 + 1);
        float socialProb = 1 - Mathf.Log(sustainability.social * 1.7f / 100 + 1);

        Player.Sustainability probs = new Player.Sustainability(
            humanProb, 
            environmentalProb, 
            socialProb, 
            economicProb
            );
        
        return probs;
    }

    public bool checkWildFire(float environmentalProb)
    {
        return Random.Range(0f, 1f) < environmentalProb;
    }
}
