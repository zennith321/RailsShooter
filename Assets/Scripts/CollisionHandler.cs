using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In Seconds")][SerializeField] float loadLevelDelay = 1f;
    [Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;
    public Rigidbody rb = null;
    void OnTriggerEnter(Collider other) 
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathFX.SetActive(true);
        Invoke("StartGameOver", loadLevelDelay);
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }

    private void StartGameOver() // string reference
    {
        SceneManager.LoadScene(1);
    }
}
