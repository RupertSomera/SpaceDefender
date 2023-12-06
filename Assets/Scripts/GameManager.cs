using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public TextMeshProUGUI scoreText; // Reference to the score TextMeshProUGUI

    private int score = 0; // Current score
    public GameObject PlayerPrefab;

    [SerializeField] private Transform spawnpoint;

    private void Awake()
    {
        // Ensure only one instance of the GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnPlayer();  
        // Initialize the score to 0 at the start
        score = 0;
        UpdateScoreText();

    }

    // Method to add score
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    // Method to update the score TextMeshProUGUI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void SpawnPlayer()
    {
        if (spawnpoint != null && PlayerPrefab != null)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, spawnpoint.position, spawnpoint.rotation, 0);
            Debug.Log("Spawned player: " + PlayerPrefab.name);
        }
        else
        {
            Debug.Log("SpawnPoint or PlayerPrefab not assigned");
        }
    }
}