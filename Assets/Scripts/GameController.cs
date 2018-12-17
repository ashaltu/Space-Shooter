using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject hazard;
    public GameObject mediumHazard;
    public GameObject bigHazard;
    public Vector3 spawnValues;
    public float gameStartWait;

    //Difficulty Attributes
    private int hazardCount;
    private float spawnRate;
    private float waveWait;
    private float smallHazardChance;
    private float mediumHazardChance;

    public Dropdown difficultyList;
    public GUIText scoreText;
    public GUIText gameOverText;
    public GUIText restartText;
    public GUIText exitText;

    private int score;
    private bool gameOver;
    private bool restart;

    //Options
    private static int difficulty;


    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        exitText.text = "";
        score = 0;
        EasyWave();
        UpdateScore();
        SetDifficulty(difficulty);
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
                Debug.Log("Difficulty: " + difficulty);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(0);
                Debug.Log("Difficulty: " + difficulty);
            }
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Difficulty: " + difficulty);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetDifficulty(int value)
    {
        difficulty = value;
        switch (difficulty)
        {
            case 0:
                EasyWave();
                break;
            case 1:
                MediumWave();
                break;
            case 2:
                HardWave();
                break;
            case 3:
                LegendaryWave();
                break;
        }
    }

    public void RefreshList(){
        difficultyList.value = difficulty;
        difficultyList.Select();
        difficultyList.RefreshShownValue();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(gameStartWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 newPos = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                float randomNumber = Random.Range(1, 100);
                if (randomNumber <= smallHazardChance)
                {
                    Instantiate(hazard, newPos, Quaternion.identity);
                }
                else if (randomNumber <= (smallHazardChance + mediumHazardChance))
                {
                    Instantiate(mediumHazard, newPos, Quaternion.identity);
                }
                else
                {
                    Instantiate(bigHazard, newPos, Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnRate);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                break;
            }

        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        restartText.text = "Press 'R' for Restart";
        restart = true;
        exitText.text = "Press 'E' to Exit";
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void EasyWave()
    {
        hazardCount = 10;
        spawnRate = 0.75f;
        waveWait = 4.0f;
        smallHazardChance = 45.0f;
        mediumHazardChance = 30.0f;
    }

    void MediumWave()
    {
        hazardCount = 10;
        spawnRate = 0.5f;
        waveWait = 2.0f;
        smallHazardChance = 25.0f;
        mediumHazardChance = 40.0f;
    }

    void HardWave()
    {
        hazardCount = 15;
        spawnRate = 0.4f;
        waveWait = 0.5f;
        smallHazardChance = 10.0f;
        mediumHazardChance = 20.0f;
    }

    void LegendaryWave()
    {
        hazardCount = 20;
        spawnRate = 0.5f;
        waveWait = 0.0f;
        smallHazardChance = 0.0f;
        mediumHazardChance = 0.0f;
    }

}