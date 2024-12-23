using UnityEngine;
using System.Collections.Generic;

public class WireStart : MonoBehaviour
{
    public LineRenderer lineRenderer; // Reference to the existing LineRenderer
    public float magnitude = 0.5f;
    public int opposite = 1;

    private List<Transform> holes; // Dynamically retrieved list of holes
    private bool isDragging = false;
    private bool isConnected = false;
    private Vector3 originalPosition;
    private Transform connectedHole;

    private void Start()
    {
        originalPosition = transform.position;

        // Get the list of holes from the HolesManager
        HolesManager manager = FindObjectOfType<HolesManager>();
        if (manager != null)
        {
            holes = manager.holes;
        }
        else
        {
            Debug.LogError("HolesManager not found in the scene!");
        }
    }

    void Update()
    {
        if (isDragging)
        {
            // Follow the mouse while dragging
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            SetPosition(worldPosition);
        }
    }

    void SetPosition(Vector3 position)
    {
        // Move the wire start point
        transform.position = position;

        // Update the wire's line positions
        Vector3 positionDifference = position - lineRenderer.transform.position;
        lineRenderer.SetPosition(1, positionDifference - new Vector3(opposite * 0.3f, 0, 0));
        lineRenderer.SetPosition(2, positionDifference - new Vector3(opposite * 0.1f, 0, 0));
    }

    private Transform FindClosestHole(Vector3 position)
    {
        Transform closestHole = null;
        float closestDistance = magnitude;

        foreach (Transform hole in holes)
        {
            float distance = Vector3.Distance(position, hole.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHole = hole;
            }
        }

        return closestHole;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        isConnected = false;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (!isConnected)
        {
            // Find the closest hole on mouse release
            Transform closestHole = FindClosestHole(transform.position);
            if (closestHole != null)
            {
                // Snap to the hole and finalize connection
                SetPosition(closestHole.position);
                isConnected = true;
                connectedHole = closestHole;
            }
            else
            {
                // Return to the original position if no valid connection
                SetPosition(originalPosition);
            }
        }
    }
}
