using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PDFDownloader : MonoBehaviour
{
    public Button downloadButton; // UI Button
    public string fileName = "Tutor_Astro.pdf"; // Имя вашего PDF файла

    void Start()
    {
        // Добавьте слушатель события нажатия на кнопку
        downloadButton.onClick.AddListener(OnDownloadButtonClick);
    }

    void OnDownloadButtonClick()
    {
        StartCoroutine(DownloadPDF());
    }

    IEnumerator DownloadPDF()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                byte[] fileData = www.downloadHandler.data;
                string pathToSave = Path.Combine(Application.persistentDataPath, fileName);
                File.WriteAllBytes(pathToSave, fileData);

                // Показать сообщение о том, что файл был сохранен
                Debug.Log("Файл успешно сохранен: " + pathToSave);
            }
            else
            {
                Debug.LogError("Ошибка загрузки файла: " + www.error);
            }
        }
    }
}
