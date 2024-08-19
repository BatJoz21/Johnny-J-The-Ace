using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(this.name + " just bumped into " + col.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} triggered by {other.gameObject.name}");
    }
}
