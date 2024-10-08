using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 3;
    [SerializeField] private GameObject enemyExplosion;
    [SerializeField] private GameObject enemyDamaged;
    [SerializeField] private int enemyVal = 10;
    //[SerializeField] private AudioSource enemyExplodeSfx;

    private GameObject parent;
    private ScoreBoard scoreBoard;
    private Rigidbody rb;

    void Awake()
    {
        parent = GameObject.FindWithTag("SpawnAtRuntime");
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
        //AddRigidBody();
    }

    private void AddRigidBody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        DamagedEnemy();
    }

    private void DamagedEnemy()
    {
        if (enemyHealth == 0)
        {
            ProcessScore();
            PlayParticleEffect(enemyExplosion);
            //enemyExplodeSfx.Play();
            Destroy(this.gameObject);
        }
        else
        {
            PlayParticleEffect(enemyDamaged);
            enemyHealth--;
        }
    }

    private void ProcessScore()
    {
        scoreBoard.IncreaseScore(enemyVal);
    }

    private void PlayParticleEffect(GameObject val)
    {
        GameObject temp = Instantiate(val, transform.position, Quaternion.identity);
        temp.transform.parent = parent.transform;
    }
}
