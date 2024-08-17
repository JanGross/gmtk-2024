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
    WORKER
}
public class GameManager : MonoBehaviour
{

    private Dictionary<Resource, int> resources = new Dictionary<Resource, int>()
    {
        { Resource.ROCK, 0 },
        { Resource.SAND, 0 },
        { Resource.CLAY, 0 },
        { Resource.BRICK, 0 },
        { Resource.TILE, 0 },
        { Resource.GLASS, 0 },
        { Resource.HOUSE, 0 },
        { Resource.WORKER, 0 }
    };


    public TMPro.TMP_Text debugText;
    // Start is called before the first frame update
    void Start()
    {
        
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

    public int GetResource(Resource res)
    {
        return resources[res];
    }

}
