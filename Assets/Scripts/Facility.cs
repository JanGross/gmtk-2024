using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    public Resource inputResource;
    public Resource outputResource;
    private int worker = 0;
    private GameManager gameManager;

    [SerializeField]
    private int materialConsumption = 5;
    [SerializeField]
    private float processingTime = 5.0f;
    private bool processing;
    private float currentProcess = 0f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!processing) return;

        if(currentProcess > 0)
        {
            currentProcess -= Time.deltaTime;
        }

        if (currentProcess <= 0){
            currentProcess = processingTime;
            processing = false;
            gameManager.AddResource(outputResource, 1 + worker);
        }
    }

    private void OnMouseUp()
    {
        if (processing) return; //Already processing

        if (gameManager.GetResource(inputResource) < materialConsumption) return; //Not enough material

        gameManager.RemoveResource(inputResource, materialConsumption);
        currentProcess = processingTime;
        processing = true;
    }
}
