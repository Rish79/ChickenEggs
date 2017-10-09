using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text collectedEggstext;
    public GameObject startPanel;

    
    public int eggsCollected;
    public float initialStartDelay = 1.5f;
    public float snakeSpawnDelay = 3.0f;
    public bool isGameRunning;
    public bool snakesCanSpawn;

    public Transform[] snakeSpawnPoints;
    public GameObject snakePrefab;

    private void Start()
    {
        isGameRunning = false;
        snakesCanSpawn = false;
        startPanel.SetActive(true);
    }

    void Update ()
    {
        collectedEggstext.text = eggsCollected.ToString();

        if(eggsCollected >= 7)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        isGameRunning = true;
        InvokeRepeating("SpawnSnakes", initialStartDelay, snakeSpawnDelay);
    }

    public void EndGame()
    {
        startPanel.SetActive(true);
        isGameRunning = false;
    }

    public void SpawnSnakes()
    {
        int randomSpawnPoint = Random.Range(0, snakeSpawnPoints.Length);
        Debug.Log(randomSpawnPoint);
        Instantiate(snakePrefab, snakeSpawnPoints[randomSpawnPoint].transform.position,
            snakePrefab.transform.rotation);
    }
}
