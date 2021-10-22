using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sustainability total, endTurn;
    public int population, education, food, money;
    public int workersTotal, workersFree;

    // Start is called before the first frame update
    void Start()
    {
        // Set data
        total = new Sustainability(0, 0, 0, 0);
        endTurn = new Sustainability(0, 0, 0, 0);
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

    public void sendWorkers() 
    {
        workersFree -= 1;

    }


    public struct Sustainability 
    {
        public int human, environmental, social, economic;

        public Sustainability(int h, int env, int s, int ec)
        {
            human = h;
            environmental = env;
            social = s;
            economic = ec;
        }
    }

}
