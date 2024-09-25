using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 3;
    [SerializeField] private GameObject enemyExplosion;
    [SerializeField] private GameObject enemyDamaged;
    [SerializeField] private Transform parent;
    [SerializeField] private int enemyVal = 10;

    private ScoreBoard scoreBoard;

    void Awake()
    {
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        DamagedEnemy();
        ProcessScore();
    }

    private void DamagedEnemy()
    {
        if (enemyHealth == 0)
        {
            PlayParticleEffect(enemyExplosion);
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
        GameObject vfx = Instantiate(val, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
    }
}
