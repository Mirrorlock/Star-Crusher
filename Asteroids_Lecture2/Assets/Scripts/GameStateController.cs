using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{

    public float GameOverScreenDelay = 2.0f;
    public string GameOverScene = "GameOver";
    public List<GameObject> spawnObjects;
    public int layerEnemy;
    public int layerEnemyProjectile;
    public Image damageFlash;

    private uint CurrentScore = 0;


    public static GameStateController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Physics.IgnoreLayerCollision(layerEnemy, layerEnemyProjectile);
    }

    public void OnPlayerSpawned()
    {
        spawnObjects.Clear();
        CurrentScore = 0;
    }

    public void OnPlayerDied()
    {
        Invoke("ShowGameOverScreen", GameOverScreenDelay);
    }

    public void IncrementScore(uint scoreToAdd)
    {
        CurrentScore += scoreToAdd;
    }

    public uint GetCurrentScore()
    {
        return CurrentScore;
    }


    public bool isNotClear()
    {
        return spawnObjects.Count != 0;
    }


    void ShowGameOverScreen()
    {
        SceneManager.LoadScene(GameOverScene);
    }

}   

