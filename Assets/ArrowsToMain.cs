using UnityEngine;
using UnityEngine.SceneManagement; // Для работы со сценами

public class ArrowToMain : MonoBehaviour
{
    public string sceneName; // Имя сцены, которую нужно загрузить

    private void OnMouseDown()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is not assigned!");
            return;
        }

        // Загружаем указанную сцену
        LoadTargetScene();
    }

    private void LoadTargetScene()
    {
        // Проверяем, существует ли сцена с указанным именем
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Scene '{sceneName}' is loading...");
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' does not exist or is not added to Build Settings.");
        }
    }
}
