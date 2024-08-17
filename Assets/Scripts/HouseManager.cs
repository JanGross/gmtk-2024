using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    private float ttnw = 30; //Time to next worker
    private float currentTtnw = 0;
    private int workerPerHouse = 3;
    private GameManager gameManager;
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
            if (gameManager.GetResource(Resource.WORKER) < gameManager.GetResource(Resource.HOUSE) * workerPerHouse)
            {
                gameManager.AddResource(Resource.WORKER, 1);
            }
        }
    }

    public void BuildHouse()
    {
        if (gameManager.GetResource(Resource.BRICK) < 10 ||
            gameManager.GetResource(Resource.TILE) < 5 ||
            gameManager.GetResource(Resource.GLASS) < 2)
        {
            Debug.Log("Not enough resources to build house!");
            return;
        }

        gameManager.RemoveResource(Resource.BRICK, 10);
        gameManager.RemoveResource(Resource.TILE, 5);
        gameManager.RemoveResource(Resource.GLASS, 2);
        gameManager.AddResource(Resource.HOUSE, 1); 
    }
}
