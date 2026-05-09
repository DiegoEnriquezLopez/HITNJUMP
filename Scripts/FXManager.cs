using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager obj;

    public GameObject Pop;

    void Awake()
    {
        obj = this;
    }

    public void ShowPop(Vector3 pos)
    {
        Pop.gameObject.GetComponent<Pop>().show(pos);
    }
    void OnDestroy()
    {
        obj = null;
    }
}

