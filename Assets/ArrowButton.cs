using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public Transform targetObject; // Assign the target object via Inspector
    public Camera mainCamera; // Assign the camera to move via Inspector
    public float moveSpeed = 5f; // Speed of the camera movement
    public bool vertical = false;

    private void OnMouseDown()
    {
        if (targetObject == null || mainCamera == null)
        {
            Debug.LogError("Target Object or Main Camera is not assigned!");
            return;
        }

        // Start moving the camera to the target
        StartCoroutine(MoveCameraToTarget());
    }

    private System.Collections.IEnumerator MoveCameraToTarget()
    {
        Vector3 targetPosition;

        if (vertical)
        {
            targetPosition = new Vector3(
            targetObject.position.x,
            targetObject.position.y,
            mainCamera.transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(
            targetObject.position.x,
            mainCamera.transform.position.y,
            mainCamera.transform.position.z);

        }
        // Preserve the camera's Z-axis

        // Smoothly move the camera to the target's position
        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                targetPosition,
                Time.deltaTime * moveSpeed
            );
            yield return null; // Wait for the next frame
        }

        // Snap the camera to the target's position
        mainCamera.transform.position = targetPosition;

        Debug.Log("Camera moved to target object.");
    }
}
