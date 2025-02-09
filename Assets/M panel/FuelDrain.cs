using UnityEngine;
using UnityEngine.SceneManagement;

public class FuelDrain : MonoBehaviour
{
    public float duration = 90f; // Time in seconds to complete the movement
    private Vector3 targetPosition; // Final position
    private Vector3 originalPosition; // Starting position
    private float timer; // Internal timer
    private bool isMoving = true; // To control the movement
    private IntegrityChanger integrity;

    void Start()
    {
        var integrityObject = GameObject.Find("Integrity");
        integrity = integrityObject.GetComponent<IntegrityChanger>();
        // Store the original position
        originalPosition = transform.position;

        // Calculate the target position (half the object's scale down)
        float halfScaleY = transform.localScale.y;
        targetPosition = originalPosition - new Vector3(0, halfScaleY, 0);

        // Initialize the timer
        timer = 0f;
    }

    void Update()
    {
        if (isMoving)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Calculate the progress (0 to 1)
            float progress = Mathf.Clamp01(timer / duration);

            // Smoothly move the object
            transform.position = Vector3.Lerp(originalPosition, targetPosition, progress);

            // Check if the movement is complete
            if (progress >= 1f)
            {
                isMoving = false; // Stop updating
                SceneManager.LoadScene("Game Over");
            }
        }
    }
}