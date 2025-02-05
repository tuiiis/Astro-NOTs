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
