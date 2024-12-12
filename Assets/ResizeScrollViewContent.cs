using UnityEngine;

public class ResizeScrollViewContent : MonoBehaviour
{
    public RectTransform canvasRectTransform; // Ссылка на Canvas
    public RectTransform contentRectTransform; // Ссылка на Content Scroll View

    void Start()
    {
        if (canvasRectTransform == null || contentRectTransform == null)
        {
            Debug.LogError("Пожалуйста, назначьте Canvas и Content в инспекторе.");
            return;
        }

        ResizeContent();
        ResizeAndExpandChildren();
    }

    void ResizeContent()
    {
        // Берем размеры Canvas
        float canvasWidth = canvasRectTransform.rect.width;
        float canvasHeight = canvasRectTransform.rect.height;

        // Устанавливаем размеры Content
        contentRectTransform.sizeDelta = new Vector2(canvasWidth * 3, canvasHeight);
    }

    void ResizeAndExpandChildren()
    {
        // Берем размеры Canvas
        float canvasWidth = canvasRectTransform.rect.width;
        float canvasHeight = canvasRectTransform.rect.height;

        // Проходимся по всем дочерним элементам Content
        for (int i = 0; i < contentRectTransform.childCount; i++)
        {
            RectTransform child = contentRectTransform.GetChild(i) as RectTransform;

            if (child != null)
            {
                // Оригинальные размеры изображения
                float originalWidth = child.rect.width;
                float originalHeight = child.rect.height;

                // Рассчитываем соотношения
                float widthRatio = canvasWidth / originalWidth;
                float heightRatio = canvasHeight / originalHeight;

                // Выбираем минимальное соотношение, чтобы не выйти за границы
                float scaleFactor = Mathf.Min(widthRatio, heightRatio);

                // Новые размеры с учетом пропорций
                float newWidth = originalWidth * scaleFactor;
                float newHeight = originalHeight * scaleFactor;

                // Устанавливаем размеры ребенка
                child.sizeDelta = new Vector2(newWidth, newHeight);
            }
        }
    }
}