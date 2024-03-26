using UnityEngine;
using UnityEngine.UI; // Add this line if you want to display the score on UI
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign this in the Inspector if using UI

    private int score = 0;
    private float timeAlive = 0f;

    public PlayerController PlayerController; // Assign your PlayerController in the Inspector


    void Start()
    {
        // Start the score counting coroutine
        StartCoroutine(UpdateScore()); 
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
    }

    System.Collections.IEnumerator UpdateScore()
    {
        while (true) // Keep updating the score while the player is alive
        {
            yield return new WaitForSeconds(5f); // Wait for 5 seconds 

            if (PlayerController.enabled) // Check if the car is still active
            {
                score++;
                if (scoreText != null)
                {
                    scoreText.text = "Score: " + score;
                }
            }   
        }
    }
}
