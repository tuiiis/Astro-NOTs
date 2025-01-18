using System.Collections.Generic;
using UnityEngine;

public class SliderLogic : MonoBehaviour
{
    public Transform[] slider0;    // Массив позиций для красного ползунка
    public Transform[] slider1;    // Массив позиций для оранжевого ползунка
    public Transform[] slider2;    // Массив позиций для зелёного ползунка
    public Transform redSlider;    // Красный ползунок
    public Transform orangeSlider; // Оранжевый ползунок
    public Transform greenSlider;  // Зелёный ползунок
    public GameObject[] arrows;    // Массив стрелок-индикаторов (6 стрелок)
    public int temperature;        // Текущая температура

    private Transform selectedSlider = null; // Ползунок, который в данный момент перемещается

    void Start()
    {
        // Устанавливаем случайное значение температуры
        temperature = Random.Range(10, 99);
        Debug.Log("Temperature: " + temperature);

        // Устанавливаем случайные начальные значения для ползунков
        InitializeSliderPosition(redSlider, slider0);
        InitializeSliderPosition(orangeSlider, slider1);
        InitializeSliderPosition(greenSlider, slider2);

        // Генерируем условия для головоломки
        CheckArrowConditions();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1.0f); // Визуализируем луч для отладки

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log("Raycast hit: " + hit.transform.name); // Проверяем, что именно попадает в Raycast
                    if (hit.transform == redSlider || hit.transform == orangeSlider || hit.transform == greenSlider)
                    {
                        selectedSlider = hit.transform;
                    }
                }
                else
                {
                    Debug.Log("Raycast missed");
                }
            }

            if (touch.phase == TouchPhase.Moved && selectedSlider != null)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                MoveSliderToClosestPosition(selectedSlider, touchPosition);

                // Выводим название объекта, на который был помещён ползунок
                if (selectedSlider == redSlider)
                    Debug.Log("Red Slider is now on position: " + GetSliderPositionName(redSlider, slider0));
                else if (selectedSlider == orangeSlider)
                    Debug.Log("Orange Slider is now on position: " + GetSliderPositionName(orangeSlider, slider1));
                else if (selectedSlider == greenSlider)
                    Debug.Log("Green Slider is now on position: " + GetSliderPositionName(greenSlider, slider2));
            }

            if (touch.phase == TouchPhase.Ended)
            {
                selectedSlider = null;
                Debug.Log("Touch ended. Checking puzzle condition...");
                CheckPuzzleCondition(); // Проверка условий после перемещения ползунков
            }
        }
    }

    void InitializeSliderPosition(Transform slider, Transform[] positions)
    {
        int randomIndex = Random.Range(0, positions.Length); // Устанавливаем случайное значение в пределах массива позиций
        slider.position = positions[randomIndex].position;
    }

    void MoveSliderToClosestPosition(Transform slider, Vector3 touchPosition)
    {
        Transform[] positions = null;

        // Определяем, какой массив позиций использовать
        if (slider == redSlider)
            positions = slider0;
        else if (slider == orangeSlider)
            positions = slider1;
        else if (slider == greenSlider)
            positions = slider2;

        // Находим ближайшую позицию
        if (positions != null)
        {
            Transform closestPosition = positions[0];
            float minDistance = Vector3.Distance(touchPosition, closestPosition.position);

            foreach (Transform pos in positions)
            {
                float distance = Vector3.Distance(touchPosition, pos.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPosition = pos;
                }
            }

            // Перемещаем ползунок к ближайшей позиции
            slider.position = closestPosition.position;
        }
    }

    string GetSliderPositionName(Transform slider, Transform[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (slider.position == positions[i].position)
            {
                return "slider " + i; // Возвращаем название позиции
            }
        }
        return "Unknown position"; // Если позиция не найдена
    }

    void CheckArrowConditions()
    {
        int numArrows = Random.Range(0, 4); // Случайное число горящих стрелок (0-3)
        Debug.Log("Number of arrows to activate: " + numArrows);

        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }

        for (int i = 0; i < numArrows; i++)
        {
            arrows[i].SetActive(true);
        }
    }

    public void CheckPuzzleCondition()
    {
        int numArrows = 0;

        foreach (GameObject arrow in arrows)
        {
            if (arrow.activeSelf)
            {
                numArrows++;
            }
        }
        Debug.Log("Number of arrows lit: " + numArrows);

        // Выводим температуру и ожидаемые значения
        Debug.Log("Temperature: " + temperature);

        if (numArrows == 2)
        {
            // Расчёт значений для ползунков
            float expectedRed = Mathf.Abs((temperature / 10) - (temperature % 10));
            float expectedOrange = SumOddDigits(temperature);
            float expectedGreen = numArrows;

            // Ограничиваем значения для ползунков максимальным значением 8
            float maxSliderValue = 8f;
            expectedRed = Mathf.Min(expectedRed, maxSliderValue);
            expectedOrange = Mathf.Min(expectedOrange, maxSliderValue);
            expectedGreen = Mathf.Min(expectedGreen, maxSliderValue);

            Debug.Log("Expected Red: " + expectedRed);
            Debug.Log("Expected Orange: " + expectedOrange);
            Debug.Log("Expected Green: " + expectedGreen);

            if (Mathf.Approximately(GetSliderIndex(redSlider, slider0), expectedRed) &&
                Mathf.Approximately(GetSliderIndex(orangeSlider, slider1), expectedOrange) &&
                Mathf.Approximately(GetSliderIndex(greenSlider, slider2), expectedGreen))
            {
                Debug.Log("Congrats! Puzzle solved correctly for 2 arrows.");
            }
            else
            {
                Debug.Log("INCORRECT");
            }
        }
        else if (numArrows == 1)
        {
            float expectedRed = 2f; // Начнем с 2, как наименьшей чётной отметки

            // Для ползунка оранжевого
            float expectedOrange = 4f; // Следующая чётная отметка

            // Для ползунка зелёного
            float expectedGreen = 6f; // И следующая чётная отметка

            Debug.Log("Expected Red: " + expectedRed);
            Debug.Log("Expected Orange: " + expectedOrange);
            Debug.Log("Expected Green: " + expectedGreen);

            if (Mathf.Approximately(GetSliderIndex(redSlider, slider0), expectedRed) &&
                Mathf.Approximately(GetSliderIndex(orangeSlider, slider1), expectedOrange) &&
                Mathf.Approximately(GetSliderIndex(greenSlider, slider2), expectedGreen))
            {
                Debug.Log("Congrats! Puzzle solved correctly.");
            }
            else
            {
                Debug.Log("INCORRECT");
            }
        }
        else if (numArrows == 0)
        {
            // Проверка на случай, если температура случайно равна 0
            if (temperature == 0)
            {
                Debug.Log("Temperature is 0, unable to calculate sum of digits.");
                return; // Выход из метода, если температура равна 0
            }

            int sumOfDigits = 0;
            int temp = temperature;

            // Вычисление суммы цифр
            while (temp != 0)
            {
                sumOfDigits += temp % 10;
                temp /= 10;
            }

            // Ограничиваем сумму цифр значением 8
            float maxSliderValue = 8f;
            float expectedRed = Mathf.Min(sumOfDigits, maxSliderValue);
            float expectedOrange = Mathf.Min(sumOfDigits, maxSliderValue);
            float expectedGreen = Mathf.Min(sumOfDigits, maxSliderValue);

            Debug.Log("Temperature: " + temperature);
            Debug.Log("Sum of Digits: " + sumOfDigits);
            Debug.Log("Expected Red: " + expectedRed);
            Debug.Log("Expected Orange: " + expectedOrange);
            Debug.Log("Expected Green: " + expectedGreen);

            if (Mathf.Approximately(GetSliderIndex(redSlider, slider0), expectedRed) &&
                Mathf.Approximately(GetSliderIndex(orangeSlider, slider1), expectedOrange) &&
                Mathf.Approximately(GetSliderIndex(greenSlider, slider2), expectedGreen))
            {
                Debug.Log("Congrats! Puzzle solved correctly for sum of digits.");
            }
        }
        else
        {
            // Подсчитываем количество единиц в числе
            int countOnes = 0;
            int temp = temperature;

            while (temp != 0)
            {
                if (temp % 10 == 1) // Если последняя цифра 1
                {
                    countOnes++;
                }
                temp /= 10;
            }

            // Если единиц нет, устанавливаем все ползунки на 1
            float expectedRed = countOnes > 0 ? countOnes : 1;
            float expectedOrange = countOnes > 0 ? countOnes : 1;
            float expectedGreen = countOnes > 0 ? countOnes : 1;

            Debug.Log("Temperature: " + temperature);
            Debug.Log("Number of ones in the temperature: " + countOnes);
            Debug.Log("Expected Red: " + expectedRed);
            Debug.Log("Expected Orange: " + expectedOrange);
            Debug.Log("Expected Green: " + expectedGreen);

            if (Mathf.Approximately(GetSliderIndex(redSlider, slider0), expectedRed) &&
                Mathf.Approximately(GetSliderIndex(orangeSlider, slider1), expectedOrange) &&
                Mathf.Approximately(GetSliderIndex(greenSlider, slider2), expectedGreen))
            {
                Debug.Log("Congrats! Puzzle solved correctly for number of ones.");
            }
        }
    }

    int GetSliderIndex(Transform slider, Transform[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (slider.position == positions[i].position)
                return i;
        }
        return -1; // Если позиция не найдена
    }

    int SumOddDigits(int num)
    {
        int sum = 0;
        while (num > 0)
        {
            int digit = num % 10;
            if (digit % 2 == 1) sum += digit;
            num /= 10;
        }
        return sum;
    }
}

