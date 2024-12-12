using UnityEngine;

public class ResizeScrollViewContent : MonoBehaviour
{
    public RectTransform canvasRectTransform; // ������ �� Canvas
    public RectTransform contentRectTransform; // ������ �� Content Scroll View

    void Start()
    {
        if (canvasRectTransform == null || contentRectTransform == null)
        {
            Debug.LogError("����������, ��������� Canvas � Content � ����������.");
            return;
        }

        ResizeContent();
        ResizeAndExpandChildren();
    }

    void ResizeContent()
    {
        // ����� ������� Canvas
        float canvasWidth = canvasRectTransform.rect.width;
        float canvasHeight = canvasRectTransform.rect.height;

        // ������������� ������� Content
        contentRectTransform.sizeDelta = new Vector2(canvasWidth * 3, canvasHeight);
    }

    void ResizeAndExpandChildren()
    {
        // ����� ������� Canvas
        float canvasWidth = canvasRectTransform.rect.width;
        float canvasHeight = canvasRectTransform.rect.height;

        // ���������� �� ���� �������� ��������� Content
        for (int i = 0; i < contentRectTransform.childCount; i++)
        {
            RectTransform child = contentRectTransform.GetChild(i) as RectTransform;

            if (child != null)
            {
                // ������������ ������� �����������
                float originalWidth = child.rect.width;
                float originalHeight = child.rect.height;

                // ������������ �����������
                float widthRatio = canvasWidth / originalWidth;
                float heightRatio = canvasHeight / originalHeight;

                // �������� ����������� �����������, ����� �� ����� �� �������
                float scaleFactor = Mathf.Min(widthRatio, heightRatio);

                // ����� ������� � ������ ���������
                float newWidth = originalWidth * scaleFactor;
                float newHeight = originalHeight * scaleFactor;

                // ������������� ������� �������
                child.sizeDelta = new Vector2(newWidth, newHeight);
            }
        }
    }
}