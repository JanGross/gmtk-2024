using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [SerializeField]
    private float ttnw = 30; //Time to next worker
    private float currentTtnw = 0;
    private int workerPerHouse = 3;
    private GameManager gameManager;

    //House cost: Brick Tile Glass
    public int[] cost = new int[3] { 10, 5, 2 };

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTtnw > 0)
        {
            currentTtnw -= Time.deltaTime;
        } else
        {
            currentTtnw = ttnw;
            if (gameManager.GetResourceCount(Resource.TOTAL_WORKER) < gameManager.GetResourceCount(Resource.HOUSE) * workerPerHouse)
            {
                int freeWorkerSlots = (gameManager.GetResourceCount(Resource.HOUSE) * workerPerHouse) - gameManager.GetResourceCount(Resource.TOTAL_WORKER);
                int newWorkers = Mathf.CeilToInt((float)freeWorkerSlots / workerPerHouse);
                gameManager.AddResource(Resource.WORKER, newWorkers);
                gameManager.AddResource(Resource.TOTAL_WORKER, newWorkers);
            }
        }
    }

    public void BuildHouse()
    {
        if (gameManager.GetResourceCount(Resource.BRICK) < cost[0] ||
            gameManager.GetResourceCount(Resource.TILE) < cost[1] ||
            gameManager.GetResourceCount(Resource.GLASS) < cost[2])
        {
            Debug.Log("Not enough resources to build house!");
            return;
        }

        gameManager.RemoveResource(Resource.BRICK, cost[0]);
        gameManager.RemoveResource(Resource.TILE, cost[1]);
        gameManager.RemoveResource(Resource.GLASS, cost[2]);
        gameManager.AddResource(Resource.HOUSE, 1); 
    }
}
