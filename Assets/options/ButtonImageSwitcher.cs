using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitcher : MonoBehaviour
{
    public Sprite image1; // Первое изображение
    public Sprite image2; // Второе изображение
    private Image buttonImage;
    private bool isImage1Active = true;

    void Start()
    {
        // Получаем компонент Image на кнопке
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = image1; // Устанавливаем начальное изображение
    }

    public void SwitchImage()
    {
        if (isImage1Active)
        {
            buttonImage.sprite = image2;
        }
        else
        {
            buttonImage.sprite = image1;
        }

        isImage1Active = !isImage1Active;
    }
}

