using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitcher : MonoBehaviour
{
    public Sprite image1; // Первое изображение (звук включен)
    public Sprite image2; // Второе изображение (звук выключен)
    private Image buttonImage;
    private bool isImage1Active = true;

    void Start()
    {
        // Получаем компонент Image на кнопке
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = image1; // Устанавливаем начальное изображение

        // Убедимся, что звук включен по умолчанию
        AudioListener.volume = 1f;
    }

    public void SwitchImage()
    {
        if (isImage1Active)
        {
            buttonImage.sprite = image2; // Меняем изображение на image2
            AudioListener.volume = 0f;  // Выключаем звук
        }
        else
        {
            buttonImage.sprite = image1; // Меняем изображение на image1
            AudioListener.volume = 1f;  // Включаем звук
        }

        isImage1Active = !isImage1Active; // Переключаем состояние
    }
}
