using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public Image tutorialImage;
    public Sprite[] tutorialSlides;
    private int currentSlideIndex = 0;



    void Start()
    {
        // Находим активный Canvas в сцене
        //// Находим первый Canvas в сцене
        //Canvas[] canvases = GameObject.FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        //if (canvases.Length > 0)
        //{
        //    Canvas canvas = canvases[0];
        //    CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();

        //    if (scaler != null)
        //    {
        //        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        //        scaler.referenceResolution = new Vector2(19, 1080);
        //        scaler.matchWidthOrHeight = 0.5f;

        //        // 🔹 Принудительно обновляем все элементы UI после смены настроек
        //        StartCoroutine(ForceUpdateUI(canvas));
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Canvas найден, но у него нет CanvasScaler!");
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("Canvas не найден в сцене!");
        //}

        ShowSlide(0);
    }

    public void ShowSlide(int index)
    {
        if (index >= 0 && index < tutorialSlides.Length)
        {
            currentSlideIndex = index;
            tutorialImage.sprite = tutorialSlides[currentSlideIndex];
        }
    }

    public void NextSlide()
    {
        if (currentSlideIndex < tutorialSlides.Length - 1)
        {
            ShowSlide(currentSlideIndex + 1);
        }
    }

    public void PreviousSlide()
    {
        if (currentSlideIndex > 0)
        {
            ShowSlide(currentSlideIndex - 1);
        }
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Panels");
    }
}
