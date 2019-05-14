using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        score = 0;
        Debug.Log(score);
        SceneManager.LoadScene(0);
    }

    public void UpdateScore()
    { 
        score++;
    }

    
}
