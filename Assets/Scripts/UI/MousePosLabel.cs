using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosLabel : MonoBehaviour
{
    public Vector3 direction = new Vector3(0,1,0);
    public float floatSpeed = 20f;
    public float lifetime = 5f;
    private float counter = 0;
    public TMPro.TMP_Text tmp;
    // Start is called before the first frame update
    void Awake()
    {
        tmp = GetComponent<TMPro.TMP_Text>();
        transform.position = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + direction * floatSpeed * Time.deltaTime;
        counter += Time.deltaTime;

        if( counter > lifetime/2)
        {
            Color32 c = new Color32((byte)tmp.color.r, (byte)tmp.color.g, (byte)tmp.color.b, (byte)tmp.color.a);
            c.a = (byte)Mathf.Lerp(255f, 0f, (counter-(lifetime/2)) / (lifetime/2));
            tmp.color = c;
            tmp.SetAllDirty();
        }

        if (counter >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
