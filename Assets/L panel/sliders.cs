using System.Collections.Generic;
using UnityEngine;

public class SliderLogic : MonoBehaviour
{
    public Transform[] slider0;
    public Transform[] slider1;
    public Transform[] slider2;
    public Transform redSlider;
    public Transform orangeSlider;
    public Transform greenSlider;
    public GameObject[] arrows;
    public int temperature;
    public Transform circleButton;
    public GameObject intergrity;

    private IntegrityChanger integrityHealth;
    private Transform selectedSlider = null; // Ползунок, который в данный момент перемещается

    public void Start()
    {
        temperature = Random.Range(10, 99);
        Debug.Log("Temperature: " + temperature);

        integrityHealth = intergrity.GetComponent<IntegrityChanger>();

        InitializeSliderPosition(redSlider, slider0);
        InitializeSliderPosition(orangeSlider, slider1);
        InitializeSliderPosition(greenSlider, slider2);

        CheckArrowConditions();
    }

    public void Update()
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

                    if (hit.transform == circleButton)
                    {
                        Debug.Log("Circle button touched. Checking puzzle condition...");
                        CheckPuzzleCondition();
                    }
                    else if (hit.transform == redSlider || hit.transform == orangeSlider || hit.transform == greenSlider)
                    {
                        selectedSlider = hit.transform;
                        Debug.Log(hit.transform.name + " touched. Moving slider...");
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
            }

            if (touch.phase == TouchPhase.Ended)
            {
                selectedSlider = null;
            }
        }
    }

    //рандом слайдеров
    public void InitializeSliderPosition(Transform slider, Transform[] positions)
    {
        int randomIndex = Random.Range(0, positions.Length);
        slider.position = positions[randomIndex].position;
    }

    //физика перемещения ползунков (к ближ)
    public void MoveSliderToClosestPosition(Transform slider, Vector3 touchPosition)
    {
        Transform[] positions = null;

        if (slider == redSlider)
            positions = slider0;
        else if (slider == orangeSlider)
            positions = slider1;
        else if (slider == greenSlider)
            positions = slider2;

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

            slider.position = closestPosition.position;
        }
    }

    //public string GetSliderPositionName(Transform slider, Transform[] positions)
    //{
    //    for (int i = 0; i < positions.Length; i++)
    //    {
    //        if (slider.position == positions[i].position)
    //        {
    //            return "slider " + i; // Возвращаем название позиции
    //        }
    //    }
    //    return "Unknown position"; // Если позиция не найдена
    //}

    //генерация стрелок
    public void CheckArrowConditions()
    {
        int numArrows = Random.Range(0, 4);
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

    //основная логика загадки
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

        Debug.Log("Temperature: " + temperature);

        SpriteRenderer spriteRenderer = circleButton.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on circleButton!");
            return;
        }

        if (numArrows == 2)
        {
            float expectedRed = Mathf.Abs((temperature / 10) - (temperature % 10));
            float expectedOrange = SumOddDigits(temperature);
            float expectedGreen = numArrows;

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
                Debug.Log("Congrats!");
                spriteRenderer.color = new Color(145f / 255f, 199f / 255f, 91f / 255f); // #91c75b
            }
            else
            {
                integrityHealth.DecreaseHealth();
            }
        }
        else if (numArrows == 1)
        {
            float expectedRed = 2f;
            float expectedOrange = 4f;
            float expectedGreen = 6f;

            Debug.Log("Expected Red: " + expectedRed);
            Debug.Log("Expected Orange: " + expectedOrange);
            Debug.Log("Expected Green: " + expectedGreen);

            if (Mathf.Approximately(GetSliderIndex(redSlider, slider0), expectedRed) &&
                Mathf.Approximately(GetSliderIndex(orangeSlider, slider1), expectedOrange) &&
                Mathf.Approximately(GetSliderIndex(greenSlider, slider2), expectedGreen))
            {
                Debug.Log("Congrats!");
                spriteRenderer.color = new Color(145f / 255f, 199f / 255f, 91f / 255f); // #91c75b
            }
            else
            {
                Debug.Log("INCORRECT");
            }
        }
        else if (numArrows == 0)
        {
            int sumOfDigits = 0;
            int temp = temperature;

            while (temp != 0)
            {
                sumOfDigits += temp % 10;
                temp /= 10;
            }

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
                Debug.Log("Congrats!");
                spriteRenderer.color = new Color(145f / 255f, 199f / 255f, 91f / 255f); // #91c75b
            }
            else
            {
                integrityHealth.DecreaseHealth();
            }
        }
        else
        {
            int countOnes = 0;
            int temp = temperature;

            while (temp != 0)
            {
                if (temp % 10 == 1)
                {
                    countOnes++;
                }
                temp /= 10;
            }

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
                Debug.Log("Congrats!");
                spriteRenderer.color = new Color(145f / 255f, 199f / 255f, 91f / 255f); // #91c75b
            }
            else
            {
                integrityHealth.DecreaseHealth();
            }
        }
    }

    public int GetSliderIndex(Transform slider, Transform[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (slider.position == positions[i].position)
                return i;
        }
        return -1;
    }

    public int SumOddDigits(int num)
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