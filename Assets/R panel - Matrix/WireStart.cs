using UnityEngine;
using System.Collections.Generic;

public class WireStart : MonoBehaviour
{
    public LineRenderer lineRenderer; // Reference to the existing LineRenderer
    public int oppositeWay = 1; // Направление смещения провода
    public Transform correctHole; // "Правильная" дырка для подключения

    private float magnitude = 0.3f; // Радиус подключения
    private List<Transform> holes; // Список дырок
    private bool isDragging = false;
    private bool isConnected = false;
    private Vector3 originalPosition;
    private Transform connectedHole;

    private void Start()
    {
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            SetPosition(worldPosition);
        }
    }

    public void Setup(Transform targetHole, List<Transform> holes)
    {
        // Передача правильной дырки
        correctHole = targetHole;
        this.holes = holes;
    }

    void SetPosition(Vector3 position)
    {
        transform.position = position;

        Vector3 positionDifference = position - lineRenderer.transform.position;
        lineRenderer.SetPosition(1, positionDifference - new Vector3(oppositeWay * 0.3f, 0, 0));
        lineRenderer.SetPosition(2, positionDifference - new Vector3(oppositeWay * 0.1f, 0, 0));
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
            Transform closestHole = FindClosestHole(transform.position);
            if (closestHole != null)
            {
                if (closestHole == correctHole)
                {
                    SetPosition(correctHole.position);
                    isConnected = true;
                    connectedHole = correctHole;
                    Debug.Log("Правильное подключение!");
                }
                else
                {
                    Debug.Log("Неправильное подключение!");
                    ResetPosition();
                }
            }
            else
            {
                ResetPosition();
            }
        }
    }

    private void ResetPosition()
    {
        SetPosition(originalPosition);
    }
}
