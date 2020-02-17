using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 4);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
