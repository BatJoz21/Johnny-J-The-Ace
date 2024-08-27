using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyExplosion;
    [SerializeField] private Transform parent;

    private void OnParticleCollision(GameObject other)
    {
        GameObject vfx = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(this.gameObject);
    }
}
