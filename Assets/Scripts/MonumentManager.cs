using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Monument
{
    public string name;
    public string description;
    public int brickCost, tileCost, plankCost;
    public GameObject worldObject;
    public GameObject cover;
}
public class MonumentManager : MonoBehaviour
{
    [SerializeField]
    private List<Monument> monuments = new List<Monument>();
    [SerializeField]
    private TMP_Text nextUnlockLabel;

    private GameManager gameManager;
    private Dictionary<Resource, int> resourcesForNextUnlock;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateUnlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUnlocks()
    {
        Dictionary<Resource, int> availableResources = gameManager.GetCommitedResources();
        int lastMonumentIndex = -1;

        for (int i = 0; i < monuments.Count; i++)
        {
            Monument monument = monuments[i];
            if(availableResources[Resource.BRICK] >= monument.brickCost &&
               availableResources[Resource.TILE] >= monument.tileCost &&
               availableResources[Resource.PLANKS] >= monument.plankCost )
            {
                availableResources[Resource.BRICK] -= monument.brickCost;
                availableResources[Resource.TILE] -= monument.tileCost;
                availableResources[Resource.PLANKS] -= monument.plankCost;
                monument.worldObject.GetComponent<Renderer>().material.color = Color.green;
                Destroy(monument.cover);
                lastMonumentIndex = i;
            } else
            {
                break;
            }
        }

        resourcesForNextUnlock = availableResources;

        if (lastMonumentIndex == monuments.Count-1)
        {
            nextUnlockLabel.text = "Everything has been unlocked!";
            return;
        }

        nextUnlockLabel.text = $"Next Unlock: {monuments[lastMonumentIndex + 1].name} (" +
            $" B: {resourcesForNextUnlock[Resource.BRICK]}/{monuments[lastMonumentIndex + 1].brickCost}" +
            $" T: {resourcesForNextUnlock[Resource.TILE]}/{monuments[lastMonumentIndex + 1].tileCost}" +
            $" P: {resourcesForNextUnlock[Resource.PLANKS]}/{monuments[lastMonumentIndex + 1].plankCost})";

    }
}
