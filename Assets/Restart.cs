using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void RestartMap()
    {
        SceneManager.LoadScene(0);
    }
    
}
