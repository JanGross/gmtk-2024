using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HouseManager : MonoBehaviour
{
    [SerializeField]
    private float ttnw = 30; //Time to next worker
    private float currentTtnw = 0;
    private int workerPerHouse = 3;
    private int purchaseMultiplier;
    private TogglePurchaseAmount purchaseAmount;
    private GameManager gameManager;
    [SerializeField]
    private GameObject mouseNotificationLabel;
    //House cost: Brick Tile Plank
    // public int[] cost = new int[3] { 10, 5, 2 };
    public int[] cost = new int[3] { 2, 1, 1 };
    [SerializeField]
    private Transform notificationOrigin;
    [SerializeField] 
    private TMP_Text houseReqLabel;
    // Start is called before the first frame update
    void Start()
    {
        purchaseAmount = GameObject.Find("PurchaseMultiplier").GetComponent<TogglePurchaseAmount>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        purchaseMultiplier = purchaseAmount.GetPurchaseAmount();
        Debug.Log(purchaseMultiplier.ToString());
        houseReqLabel.text = $"{gameManager.GetResourceCount(Resource.BRICK)}/{cost[0] * purchaseMultiplier} Bricks\n" +
                             $"{gameManager.GetResourceCount(Resource.TILE)}/{cost[1] * purchaseMultiplier} Tiles\n" +
                             $"{gameManager.GetResourceCount(Resource.PLANKS)}/{cost[2] * purchaseMultiplier} Planks\n";
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

                GameObject infoLabel = Instantiate(mouseNotificationLabel, notificationOrigin.transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
                infoLabel.GetComponent<MousePosLabel>().tmp.text = $"{newWorkers} worker{(newWorkers > 1 ? "s" : "")} moved in";
                infoLabel.transform.position = notificationOrigin.transform.position;
                infoLabel.GetComponent <MousePosLabel>().direction = new Vector3(0, -2, 0);
            }
        }
    }

    public void BuildHouse()
    {
        if (gameManager.GetResourceCount(Resource.BRICK) < cost[0] * purchaseMultiplier ||
            gameManager.GetResourceCount(Resource.TILE) < cost[1] * purchaseMultiplier ||
            gameManager.GetResourceCount(Resource.PLANKS) < cost[2] * purchaseMultiplier)
        {
            Debug.Log("Not enough resources to build house!");
            GameObject infoLabel = Instantiate(mouseNotificationLabel, GameObject.Find("Canvas").transform);
            infoLabel.GetComponent<MousePosLabel>().tmp.text = "Not enough resources to build house!";
            infoLabel.GetComponent<MousePosLabel>().direction = new Vector3(0, 4, 0);
            return;
        }

        gameManager.RemoveResource(Resource.BRICK, cost[0] * purchaseMultiplier);
        gameManager.RemoveResource(Resource.TILE, cost[1] * purchaseMultiplier);
        gameManager.RemoveResource(Resource.PLANKS, cost[2] * purchaseMultiplier);
        gameManager.AddResource(Resource.HOUSE, 1 * purchaseMultiplier); 
    }
}
