using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f;
    [SerializeField] private ParticleSystem explosionFx;
    [SerializeField] private GameObject aircraftBody;
    private int currentScene;
    private PlayerControls plc;
    private BoxCollider coll;

    void Start()
    {
        plc = GetComponent<PlayerControls>();
        coll = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(this.name + " just bumped into " + col.gameObject.name);
        PlayerCrash();
        Invoke("ReloadScene", loadDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} triggered by {other.gameObject.name}");
        PlayerCrash();
        Invoke("ReloadScene", loadDelay);
    }

    private void PlayerCrash()
    {
        explosionFx.Play();
        aircraftBody.SetActive(false);
        plc.enabled = false;
        coll.enabled = false;

    }

    private void ReloadScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
