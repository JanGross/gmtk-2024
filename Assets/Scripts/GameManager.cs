using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    [SerializeField]
    private Transform targetCameraPos;
    private MonumentManager monumentManager;
    [SerializeField]
    private float cameraSpeed = 5;
    
    [SerializeField]
    private GameObject quitPanel, transitionPanel;
    [SerializeField]
    private float transitionTime = 1f;
    private float transitionCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        transitionPanel.SetActive(true);
        Camera.main.transform.position = mainCameraPos.position;
        targetCameraPos = mainCameraPos;
        monumentManager = GameObject.Find("MonumentManager").GetComponent<MonumentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(!quitPanel.activeSelf);
        }

        if(transitionCounter <= transitionTime)
        {
            transitionCounter += Time.deltaTime;
            Color c = transitionPanel.GetComponent<Image>().color;
            c.a = Mathf.Lerp(1f, 0f, transitionCounter / transitionTime);
            transitionPanel.GetComponent<Image>().color = c;
            if(transitionCounter > transitionTime) { Destroy(transitionPanel); }
        }

        float effectiveCamSpeed = Vector3.Distance(Camera.main.transform.position, targetCameraPos.position) * cameraSpeed * Time.deltaTime;
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, targetCameraPos.position, effectiveCamSpeed);
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
                targetCameraPos = mainCameraPos;
                mainViewBtn.SetActive(false);
                mapViewBtn.SetActive(true);
                commitBtn.SetActive(false);
                break;
            case 1: //Map View
                targetCameraPos = worldMapCameraPos;
                mainViewBtn.SetActive(true);
                mapViewBtn.SetActive(false);
                commitBtn.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
