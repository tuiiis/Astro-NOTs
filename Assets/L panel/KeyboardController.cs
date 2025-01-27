using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public SpriteRenderer[] buttons; // Кнопки (KB_Button1 - KB_Button6)
    public SpriteRenderer LED1_TMP; // Индикатор TMP
    public SpriteRenderer LED2_GLZ; // Индикатор GLZ
    public SpriteRenderer LED3_RND; // Индикатор RND
    public SpriteRenderer KB_Score; // Индикатор результата (зеленый или красный)

    private int currentIndex = 0; // Текущий индекс правильного нажатия
    private int[] currentOrder; // Текущая последовательность для проверки

    private int[] correctOrderTMP = { 0, 1, 2, 3, 4, 5 }; // Правильная последовательность для TMP
    private int[] correctOrderGLZ = { 5, 4, 3, 2, 1, 0 }; // Правильная последовательность для GLZ
    private int[] correctOrderRND = { 0, 2, 4, 1, 3, 5 }; // Правильная последовательность для RND

    private bool gameActive = true; // Флаг активности игры

    void Start()
    {
        // Устанавливаем KB_Score в белый цвет по умолчанию
        KB_Score.color = new Color(1, 1, 1, 1);

        // Добавляем коллайдеры для кнопок
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.AddComponent<BoxCollider2D>(); // Добавляем коллайдер для каждой кнопки
        }

        RandomizeMode(); // Рандомный выбор режима при старте
    }


    void Update()
    {
        if (!gameActive) return; // Если игра завершена, игнорируем действия

        // Проверка нажатий мыши на кнопки
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Mouse Position: " + mousePos); // Проверяем позицию мыши

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name); // Проверяем, что клики регистрируются

                for (int i = 0; i < buttons.Length; i++)
                {
                    if (hit.collider.gameObject == buttons[i].gameObject)
                    {
                        OnButtonPress(i);
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Raycast missed"); // Если клики не попадают ни на одну кнопку
            }
        }
    }
    //void Update()
    
    // {
        
    //     if (!gameActive) return; // Если игра завершена, игнорируем действия

    //     // Проверка нажатий мыши на кнопки
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    //         if (hit.collider != null)
    //         {
    //             for (int i = 0; i < buttons.Length; i++)
    //             {
    //                 if (hit.collider.gameObject == buttons[i].gameObject)
    //                 {
    //                     OnButtonPress(i);
    //                     break;
    //                 }
    //             }
    //         }
    //     }
    // }

    public void OnButtonPress(int buttonIndex)
    {
        Debug.Log("Button pressed: " + buttonIndex);
        if (!gameActive) return; // Если игра завершена, игнорируем действия

        if (buttonIndex == currentOrder[currentIndex])
        {
            currentIndex++;
            if (currentIndex >= currentOrder.Length)
            {
                // Если последовательность завершена правильно
                ShowResult(true);
            }
        }
        else
        {
            // Если последовательность неправильная
            ShowResult(false);
        }
    }

    void RandomizeMode()
    {
        // Отключаем все индикаторы
        SetIndicator(LED1_TMP, false);
        SetIndicator(LED2_GLZ, false);
        SetIndicator(LED3_RND, false);

        // Случайный выбор режима
        int randomMode = Random.Range(0, 3);
        switch (randomMode)
        {
            case 0:
                SetIndicator(LED1_TMP, true);
                currentOrder = correctOrderTMP;
                break;
            case 1:
                SetIndicator(LED2_GLZ, true);
                currentOrder = correctOrderGLZ;
                break;
            case 2:
                SetIndicator(LED3_RND, true);
                currentOrder = correctOrderRND;
                break;
        }

        currentIndex = 0; // Сбрасываем текущий индекс
    }

    void SetIndicator(SpriteRenderer led, bool isActive)
    {
        if (isActive)
        {
            led.color = new Color(1, 0, 0, 1); // Полностью видимый (красный)
        }
        else
        {
            led.color = new Color(0, 1, 0, 0.2f); // Полупрозрачный (зеленый)
        }
    }

    void ShowResult(bool isCorrect)
    {
        gameActive = false; // Завершаем игру

        if (isCorrect)
        {
            KB_Score.color = new Color(0, 1, 0, 1); // Зеленый цвет
            Debug.Log("Игрок победил!");
        }
        else
        {
            KB_Score.color = new Color(1, 0, 0, 1); // Красный цвет
            Debug.Log("Игрок проиграл!");
        }

  
    }
}
