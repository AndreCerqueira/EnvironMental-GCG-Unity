using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Sustainability total, endTurn;
    public int population, education, food, money;
    public int workersTotal, workersFree;

    // Start is called before the first frame update
    void Start()
    {
        // Set data
        total = new Sustainability(50, 50, 50, 50);
        endTurn = new Sustainability(0, 0, 0, 0);

        GameObject.Find("Sustainability Points/Human/Text").GetComponent<Text>().text = total.human.ToString();
        GameObject.Find("Sustainability Points/Environmental/Text").GetComponent<Text>().text = total.environmental.ToString();
        GameObject.Find("Sustainability Points/Social/Text").GetComponent<Text>().text = total.social.ToString();
        GameObject.Find("Sustainability Points/Economic/Text").GetComponent<Text>().text = total.economic.ToString();
        
        population = 0;
        workersTotal = population / 10;
        workersFree = workersTotal;
        education = 0;
        food = 0;
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendWorkers(Tile tile) 
    {
        workersFree -= 1;
        tile.workers += 1;
        
        // Change the button text
        GameObject.Find("Work status").GetComponent<Text>().text = tile.workers + " Workers here";
    }

    public void returnWorkers(Tile tile) 
    {
        workersFree += 1;
        tile.workers -= 1;
        
        // Change the button text
        GameObject.Find("Work status").GetComponent<Text>().text = tile.workers + " Workers here";
    }

    public struct Sustainability 
    {
        public float human, environmental, social, economic;

        public Sustainability(float h, float env, float s, float ec)
        {
            human = h;
            environmental = env;
            social = s;
            economic = ec;
        }
    }

}
