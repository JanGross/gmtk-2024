using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Resource
{
    ROCK,
    SAND,
    CLAY,
    BRICK,
    TILE,
    GLASS,
    HOUSE,
    WORKER,
    TOTAL_WORKER
}
public class GameManager : MonoBehaviour
{
    private int currentView = 0;

    private Dictionary<Resource, int> resources = new Dictionary<Resource, int>()
    {
        { Resource.ROCK, 0 },
        { Resource.SAND, 0 },
        { Resource.CLAY, 0 },
        { Resource.BRICK, 0 },
        { Resource.TILE, 0 },
        { Resource.GLASS, 0 },
        { Resource.HOUSE, 1 },
        { Resource.WORKER, 0 },
        { Resource.TOTAL_WORKER, 0 },
    };


    public TMPro.TMP_Text debugText;

    [SerializeField]
    GameObject mainViewBtn, mapViewBtn;

    [SerializeField]
    private Transform mainCameraPos, worldMapCameraPos;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = mainCameraPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = "";
        foreach (var item in resources)
        {
            debugText.text += item.ToString();
        }
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

    public void SetView(int view)
    {
        switch (view)
        {
            case 0: //Main view
                Camera.main.transform.position = mainCameraPos.position;
                mainViewBtn.SetActive(true);
                mapViewBtn.SetActive(false);
                break;
            case 1: //Map View
                Camera.main.transform.position = worldMapCameraPos.position;
                mainViewBtn.SetActive(false);
                mapViewBtn.SetActive(true);
                break;
            default:
                break;
        }
    }
}
