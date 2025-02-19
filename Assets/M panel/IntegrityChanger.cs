using UnityEngine;

public class IntegrityChanger : MonoBehaviour
{
    public bool button = false;
    private Vector3 originalPosition; // Starting position
    private int healthPoints = 5;
    private float stepDistance; // Distance to move per step

    void Start()
    {
        // Store the original position
        originalPosition = transform.position;

        // Calculate the step distance (1/4th of object's length)
        stepDistance = transform.localScale.y / 5f;
    }

    private void Update()
    {
        if (button)
        {
            DecreaseHealth();
            button = false;
        }
    }

    public void DecreaseHealth()
    {
        if (healthPoints > 0)
        {
            // Move the object down by the step distance
            transform.position -= new Vector3(0, stepDistance, 0);
            healthPoints--;
            if (healthPoints == 0)
            {
                Debug.Log($"GAME END");
            }
            if (healthPoints == 1)
            {
                Debug.Log($"CRITICAL HEALTH");
            }
        }
    }


}
