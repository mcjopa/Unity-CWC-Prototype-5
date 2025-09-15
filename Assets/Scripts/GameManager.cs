using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject volumeUI;
    public GameObject titleScreen;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI pauseText;

    public Button restartButton;

    public bool isGameActive;
    public bool isGamePaused;

    private float spawnRate = 1.0f;

    private int lives;
    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandlePause();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }

    public void HandlePause()
    {
        if (!isGameActive) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseText.gameObject.SetActive(true);
        isGamePaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseText.gameObject.SetActive(false);
        isGamePaused = false;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void UpdateLives(int lifeToAdd)
    {

        lives += lifeToAdd;
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }

        livesText.text = $"Lives: {lives}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        titleScreen.SetActive(false);
        volumeUI.SetActive(false);

        isGameActive = true;
        score = 0;
        lives = 3;
        isGameActive = true;

        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);
    }
}
