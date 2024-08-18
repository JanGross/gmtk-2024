using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TutorialElement
{
    public TutorialElement(string _title) { title = _title; }
    public string title;
    public GameObject worldObject;
    public bool complete = false;
} 

public enum Tutorials:int
{
    Mining = 0,
    Production = 1,
    HouseBuilding = 2,
    MovingIn = 3,
    Assigning = 4,
    WorldMap = 5,
    Committing = 6,
    Unlocking = 7
}
public class Tutorial : MonoBehaviour
{

    [SerializeField]
    private TutorialElement[] tutorialElements;
    private GameManager gameManager;
    private HouseManager houseManager;
    [SerializeField]
    private float genericTimeout = 15; //Timeout for generic tutorials with no conditions
    private float genericCountdown = 0f;
    [SerializeField]
    private GameObject commitBtn;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        houseManager = GameObject.Find("HouseManager").GetComponent<HouseManager>();
        
        for (int i = 0; i < tutorialElements.Length; i++)
        {
            TutorialElement tutorial = tutorialElements[i];
            tutorial.worldObject = GameObject.Find($"{tutorial.title}Tutorial");
            if (i > 0) tutorial.worldObject.SetActive(false);
            Debug.Log($"{tutorial.title}Tutorial");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorialElements[(int)Tutorials.Mining].complete)
        {
            if (gameManager.GetResourceCount(Resource.ROCK) >= 5)
            {
                tutorialElements[(int)Tutorials.Mining].complete = true;
                tutorialElements[(int)Tutorials.Mining].worldObject.SetActive(false);
                tutorialElements[(int)Tutorials.Production].worldObject.SetActive(true);
            }
            return;
        }

        if (!tutorialElements[(int)Tutorials.Production].complete)
        {
            if (gameManager.GetResourceCount(Resource.BRICK) > 0)
            {
                tutorialElements[(int)Tutorials.Production].complete = true;
                tutorialElements[(int)Tutorials.Production].worldObject.SetActive(false);
            }
            return;
        }

        if (!tutorialElements[(int)Tutorials.HouseBuilding].complete)
        {
            if (gameManager.GetResourceCount(Resource.BRICK) >= houseManager.cost[0] &&
                gameManager.GetResourceCount(Resource.TILE) >= houseManager.cost[1] &&
                gameManager.GetResourceCount(Resource.GLASS) >= houseManager.cost[2])
            {

                tutorialElements[(int)Tutorials.HouseBuilding].worldObject.SetActive(true);
            }

            if (gameManager.GetResourceCount(Resource.HOUSE) > 0)
            {
                tutorialElements[(int)Tutorials.HouseBuilding].worldObject.SetActive(false);
                tutorialElements[(int)Tutorials.HouseBuilding].complete = true;
                tutorialElements[(int)Tutorials.MovingIn].worldObject.SetActive(true);
            }
        }

        if (!tutorialElements[(int)Tutorials.MovingIn].complete)
        {
            if (gameManager.GetResourceCount(Resource.TOTAL_WORKER) >= 3)
            {
                tutorialElements[(int)Tutorials.MovingIn].complete = true;
                tutorialElements[(int)Tutorials.MovingIn].worldObject.SetActive(false);
            }
        }

        if (!tutorialElements[(int)Tutorials.Assigning].complete)
        {
            if (gameManager.GetResourceCount(Resource.TOTAL_WORKER) >= 1)
            {
                tutorialElements[(int)Tutorials.Assigning].worldObject.SetActive(true);
            }
            if (gameManager.GetResourceCount(Resource.WORKER) < gameManager.GetResourceCount(Resource.TOTAL_WORKER))
            {
                tutorialElements[(int)Tutorials.Assigning].complete = true;
                tutorialElements[(int)Tutorials.Assigning].worldObject.SetActive(false);
            }
            return;
        }

        // Wait for 100 bricks to continue past this point
        if (!tutorialElements[(int)Tutorials.WorldMap].complete && !(gameManager.GetResourceCount(Resource.BRICK) > 50)) { return; }

        if (!tutorialElements[(int)Tutorials.WorldMap].complete)
        {
            tutorialElements[(int)Tutorials.WorldMap].worldObject.SetActive(true);
            if (commitBtn.activeSelf == true)
            {
                tutorialElements[(int)Tutorials.WorldMap].complete = true;
                tutorialElements[(int)Tutorials.WorldMap].worldObject.SetActive(false);
                tutorialElements[(int)Tutorials.Committing].worldObject.SetActive(true);
            }
            return;
        }

        if (!tutorialElements[(int)Tutorials.Committing].complete)
        {
            if (gameManager.GetCommitedResources()[Resource.BRICK] > 0)
            {
                tutorialElements[(int)Tutorials.Committing].complete = true;
                tutorialElements[(int)Tutorials.Committing].worldObject.SetActive(false);
                tutorialElements[(int)Tutorials.Unlocking].worldObject.SetActive(true);
                genericCountdown = genericTimeout;
            }
            return;
        }

        if (!tutorialElements[(int)Tutorials.Unlocking].complete)
        {
            genericCountdown -= Time.deltaTime;
            if(genericCountdown <= 0)
            {
                tutorialElements[(int)Tutorials.Unlocking].complete = true;
                tutorialElements[(int)Tutorials.Unlocking].worldObject.SetActive(false);
            }
        }
    }
}
