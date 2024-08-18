using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Resource
{
    ROCK,
    WOOD,
    CLAY,
    BRICK,
    TILE,
    PLANKS,
    HOUSE,
    WORKER,
    TOTAL_WORKER
}
public class GameManager : MonoBehaviour
{
    private int currentView = 0;

    private Dictionary<Resource, int> committedResources = new Dictionary<Resource, int>()
    {
        { Resource.BRICK, 0 },
        { Resource.TILE, 0 },
        { Resource.PLANKS, 0 }
    };

    private Dictionary<Resource, int> resources = new Dictionary<Resource, int>()
    {
        { Resource.ROCK, 0 },
        { Resource.WOOD, 0 },
        { Resource.CLAY, 0 },
        { Resource.BRICK, 0 },
        { Resource.TILE, 0 },
        { Resource.PLANKS, 0 },
        { Resource.HOUSE, 0 },
        { Resource.WORKER, 0 },
        { Resource.TOTAL_WORKER, 0 }
    };


    public TMPro.TMP_Text debugText;
    [SerializeField]
    private TMPro.TMP_Text resourceLabel;
    [SerializeField]
    private TMPro.TMP_Text housingLabel;

    [Header("Map View")]
    [SerializeField]
    private Transform worldMapCameraPos;
    [SerializeField]
    private GameObject mainViewBtn, commitBtn;

    [Header("Main View")]
    [SerializeField]
    private Transform mainCameraPos;
    [SerializeField]
    private GameObject mapViewBtn;

    private MonumentManager monumentManager;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = mainCameraPos.position;
        monumentManager = GameObject.Find("MonumentManager").GetComponent<MonumentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = "";
        foreach (var item in resources)
        {
            debugText.text += item.ToString();
        }

        housingLabel.text = $"{resources[Resource.HOUSE]} Houses\n" +
                            $"{resources[Resource.WORKER]}/{resources[Resource.TOTAL_WORKER]} Workers";
        resourceLabel.text = $"Resources:\n" +
            $"Storage: Bricks {resources[Resource.BRICK]} | Tiles {resources[Resource.TILE]} | Planks {resources[Resource.PLANKS]}\n" +
            $"Comitted: Bricks {committedResources[Resource.BRICK]} | Tiles {committedResources[Resource.TILE]} | Planks {committedResources[Resource.PLANKS]}";
    }

    public void CommitResources()
    {
        List<Resource> resourceList = new List<Resource>(committedResources.Keys);
        foreach (Resource resource in resourceList)
        {
            committedResources[resource] += GetResourceCount(resource);
            RemoveResource(resource, GetResourceCount(resource));
        }

        monumentManager.UpdateUnlocks();
    }

    public void AddResource(Resource res, int amt)
    {
        resources[res] += amt;
    }

    public void RemoveResource(Resource res, int amt)
    {
        resources[res] -= amt;
    }

    public int GetResourceCount(Resource res)
    {
        return resources[res];
    }

    public Dictionary<Resource, int> GetCommitedResources()
    {
        return new Dictionary<Resource, int>(committedResources);
    }

    public void SetView(int view)
    {
        switch (view)
        {
            case 0: //Main view
                Camera.main.transform.position = mainCameraPos.position;
                mainViewBtn.SetActive(false);
                mapViewBtn.SetActive(true);
                commitBtn.SetActive(false);
                break;
            case 1: //Map View
                Camera.main.transform.position = worldMapCameraPos.position;
                mainViewBtn.SetActive(true);
                mapViewBtn.SetActive(false);
                commitBtn.SetActive(true);
                break;
            default:
                break;
        }
    }
}
