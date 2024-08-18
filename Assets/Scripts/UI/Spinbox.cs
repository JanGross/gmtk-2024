using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spinbox : MonoBehaviour
{
    public AssignableWorker target;
    [SerializeField]
    private TMP_Text numAssignedLabel;

    private void Start()
    {
        target = transform.parent.GetComponent<AssignableWorker>();    
    }

    private void Update()
    {
        numAssignedLabel.text = target.GetAssignedWorkers().ToString();
    }
}
