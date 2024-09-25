using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    void Update()
    {
        ClearChildren();
    }

    private void ClearChildren()
    {
        int i = transform.childCount;
        for (int j = 0; j < i; j++)
        {
            Transform child = transform.GetChild(j);
            Destroy(child.gameObject, 2.0f);
        }
    }
}
