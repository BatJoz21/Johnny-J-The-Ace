using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f;
    private int currentScene;
    PlayerControls plc;

    void Start()
    {
        plc = GetComponent<PlayerControls>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(this.name + " just bumped into " + col.gameObject.name);
        Invoke("PlayerCrash", loadDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} triggered by {other.gameObject.name}");
        Invoke("PlayerCrash", loadDelay);
    }

    private void PlayerCrash()
    {
        plc.enabled = false;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
