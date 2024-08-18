using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignableWorker : MonoBehaviour
{
    private int workersAssigned = 0;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public int GetAssignedWorkers()
    {
        return workersAssigned;
    }

    public void AddWorker()
    {
        if(gameManager.GetResourceCount(Resource.WORKER) > 0)
        {
            workersAssigned++;
            gameManager.RemoveResource(Resource.WORKER, 1);
        }
    }

    public bool RemoveWorker()
    {
        if (workersAssigned == 0) return false;
        workersAssigned--;
        gameManager.AddResource(Resource.WORKER, 1);
        return true;
    }
}
