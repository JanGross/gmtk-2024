using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Facility : MonoBehaviour
{
    public Resource inputResource;
    public Resource outputResource;
    private GameManager gameManager;

    [SerializeField]
    private int materialConsumption = 5;
    [SerializeField]
    private float processingTime = 5.0f;
    [SerializeField]
    private int baseOutput = 2;
    [SerializeField]
    private TMP_Text currentProgressLabel;
    private bool processing;
    private float currentProcess = 0f;
    private int currentProductionBatch = 0;

    private AssignableWorker assignableWorker;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        assignableWorker = gameObject.GetComponent<AssignableWorker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!processing && assignableWorker.GetAssignedWorkers() <= 0) return; //No workers and no manual process running

        if (!processing) //Not processing but workers assigned, start new job if material is available
        {
            if (gameManager.GetResourceCount(inputResource) < materialConsumption) return; //Not enough material

            //Consumtion 5, 2
            int workerCount = (assignableWorker.GetAssignedWorkers() == 0 ? 1 : assignableWorker.GetAssignedWorkers());
            int maxPossibleBatch = gameManager.GetResourceCount(inputResource) / materialConsumption;
            currentProductionBatch = Mathf.Min(maxPossibleBatch, workerCount);
            
            int requiredRessources = currentProductionBatch * materialConsumption;

            gameManager.RemoveResource(inputResource,requiredRessources);
            currentProcess = processingTime;
            processing = true;
        }

        if(currentProcess > 0)
        {
            currentProcess -= Time.deltaTime;
            currentProgressLabel.text = currentProcess.ToString();
        }

        if (currentProcess <= 0){
            currentProcess = processingTime;
            processing = false;
            gameManager.AddResource(outputResource, currentProductionBatch * baseOutput);
        }
    }

    private void OnMouseUp()
    {
        if (processing) return; //Already processing

        if (gameManager.GetResourceCount(inputResource) < materialConsumption) return; //Not enough material

        currentProductionBatch = 1;
        gameManager.RemoveResource(inputResource, materialConsumption);
        currentProcess = processingTime;
        processing = true;
    }
}
