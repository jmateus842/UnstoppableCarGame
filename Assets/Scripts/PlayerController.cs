using UnityEngine;
using UnityEngine.SceneManagement; // deberia funcionar


public class PlayerController : MonoBehaviour
{
    public float maxFallTime = 5.0f; 
    private float fallStartTime;
    private bool isFalling = false; 
    public bool enabledFlag = true;
    public float speed = 10f; 
    public float rotationSpeed = 100f; 

    public float boostSpeed = 20f; // Speed during the boost
    public float boostDuration = 1.0f; // How long the boost lasts

    private float currentSpeed; 
    private float boostEndTime; 

    void Start()
    {
        currentSpeed = speed; // Initialize with the normal speed
    }

    void Update()
    {
        if (Time.time < boostEndTime) 
        {
            currentSpeed = boostSpeed; // Apply boost speed if active
        } 
        else
        {
            currentSpeed = speed;  // Revert to normal speed
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // Check for boost input
        if (Input.GetKeyDown(KeyCode.E))
        {
            boostEndTime = Time.time + boostDuration;
        }
        // Check for falling condition
        if (transform.position.y < 0)  // Or any other threshold you want
        {
            if (!isFalling)
            {
                isFalling = true;
                fallStartTime = Time.time;
            }

            if (Time.time - fallStartTime > maxFallTime)
            {
                ResetScene(); // Player has fallen too long
            }
        }
        else
        {
            isFalling = false; 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ResetScene();
        }
    }

    void ResetScene()
    {
        enabledFlag = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }
}
