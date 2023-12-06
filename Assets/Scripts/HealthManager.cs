using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public Image healthbar;
    public float healthAmount = 100f;
    public TextMeshProUGUI gameOverText;

    private bool isGameOver = false;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (healthAmount <= 0)
            {
                GameOver();
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        healthAmount -= dmg;
        healthbar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float health)
    {
        health += healthAmount;
        healthAmount = Mathf.Clamp(health, 0, 100);

        healthbar.fillAmount = healthAmount / 100f;
    }

    // Use OnCollisionEnter2D for 2D collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Adjust the tag as needed
        {
            Debug.Log("Collision Detected");
            TakeDamage(20); // Adjust the damage amount as needed
            collision.gameObject.SetActive(false);
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;

        gameOverText.gameObject.SetActive(true);
    }
}