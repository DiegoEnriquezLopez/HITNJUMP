using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    public static Pop obj;

    private void Awake()
    {
        obj = this;
    }

    public void show(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void disappear() 
    { 
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
