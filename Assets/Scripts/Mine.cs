using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mine : MonoBehaviour
{
    public Resource resource;
    private GameManager gameManager;

    private float nextTick = 0f;
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
        nextTick -= Time.deltaTime;
        if (nextTick <= 0f)
        {
            Tick();
            nextTick = 1f;
        }
    }

    private void Tick()
    {
        gameManager.AddResource(resource, 1 * assignableWorker.GetAssignedWorkers());
    }

    private void OnMouseDown()
    {
        gameManager.AddResource(resource, 1);
    }
}
