using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Метод для переключения сцены по имени
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    // Метод для выхода из игры
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been quit."); // Отображается в редакторе, но не в сборке
    }

    public void GoToTutor()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void GoToOptions()
    {
        SceneManager.LoadScene("Options");
    }
    private System.Collections.IEnumerator SetSceneActiveWhenLoaded(string sceneName)
    {
        // Ожидание, пока сцена загрузится
        while (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            yield return null;
        }

        // Устанавливаем сцену как активную
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        Debug.Log($"Сцена {sceneName} теперь активная.");
    }
}
