using UnityEngine;
using UnityEngine.UI;

public class FixCanvasMode : MonoBehaviour
{
    private void Start()
    {
        // Находим активный Canvas в сцене
        CanvasScaler scaler = FindFirstObjectByType<CanvasScaler>();
        if (scaler != null)
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; // Принудительно задаем режим
            scaler.referenceResolution = new Vector2(1920, 1080); // Указываем правильное разрешение
            scaler.matchWidthOrHeight = 0.5f; // Подгоняем масштаб
        }
        else
        {
            Debug.LogWarning("CanvasScaler не найден! Убедись, что в сцене есть Canvas.");
        }
    }
}
