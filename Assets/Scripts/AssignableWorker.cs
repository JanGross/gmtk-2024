using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignableWorker : MonoBehaviour
{
    private int workersAssigned = 0;
    private TogglePurchaseAmount purchaseAmount;
    private GameManager gameManager;
    private int workerAmount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        purchaseAmount = GameObject.Find("PurchaseMultiplier").GetComponent<TogglePurchaseAmount>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public int GetAssignedWorkers()
    {
        return workersAssigned;
    }

    public void AddWorker()
    {
        workerAmount = purchaseAmount.GetPurchaseAmount();
        
        if(gameManager.GetResourceCount(Resource.WORKER) >= workerAmount)
        {
            workersAssigned += workerAmount;
            gameManager.RemoveResource(Resource.WORKER, workerAmount);
        }
        
        
    }

    public bool RemoveWorker()
    {
        workerAmount = purchaseAmount.GetPurchaseAmount();
        if (workersAssigned < workerAmount) return false;
        workersAssigned -= workerAmount;
        gameManager.AddResource(Resource.WORKER, workerAmount);
        return true;
    }
}
