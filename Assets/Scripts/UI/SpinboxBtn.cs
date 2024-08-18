using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpinnerBtnType
{
    UP,
    DOWN
}
public class SpinboxBtn : MonoBehaviour
{
    [SerializeField]
    private SpinnerBtnType type;

    private void OnMouseUp()
    {
        if (type == SpinnerBtnType.UP)
        {
            transform.parent.gameObject.GetComponent<Spinbox>().target.AddWorker();

        }

        if (type == SpinnerBtnType.DOWN)
        {
            transform.parent.gameObject.GetComponent<Spinbox>().target.RemoveWorker();
        }
    }
}
