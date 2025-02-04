using UnityEngine;
using UnityEngine.SceneManagement; // Для работы со сценами

public class ArrowToMain : MonoBehaviour
{
    public string sceneName; // Имя сцены, которую нужно загрузить

    private void OnMouseDown()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
