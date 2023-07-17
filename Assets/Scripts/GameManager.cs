using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public bool isPaused;
    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private int score;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        isPaused = false;
    }

    public int GetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore",0);
        return maxScore;
    }

    
}
