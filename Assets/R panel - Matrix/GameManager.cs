using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public WireSpawnSet currentSpawnSet; // Текущая конфигурация проводов
    public List<Transform> spawnPositions; // Координаты для спавна проводов
    public List<Transform> holes; // Список дырок (16 штук)
    public GameObject holePrefab; // Префаб дырки

    private List<GameObject> spawnedWires = new List<GameObject>();
    private List<GameObject> spawnedHoles = new List<GameObject>();

    void Start()
    {
        Debug.Log(holes.Count);
        for (int i = 0; i < holes.Count; i++)
        {
            Debug.Log(holes[i].transform.position.x);
        }
        SpawnHoles();
        SpawnWires();

    }

    void SpawnHoles()
    {
        // Очищаем старые дырки
        foreach (var hole in spawnedHoles)
        {
            Destroy(hole);
        }
        spawnedHoles.Clear();

        // Создаём дырки
        for (int i = 0; i < holes.Count; i++)
        {
            GameObject spawnedHole = Instantiate(holePrefab, holes[i].position, Quaternion.identity);
            spawnedHoles.Add(spawnedHole);
        }
    }

    void SpawnWires()
    {
        // Очищаем старые провода
        foreach (var wire in spawnedWires)
        {
            Destroy(wire);
        }
        spawnedWires.Clear();

        // Спавним провода
        for (int i = 0; i < currentSpawnSet.wireConfigs.Count; i++)
        {
            // Берём позицию для спавна
            Transform spawnPosition = spawnPositions[i];
            WireSpawnConfig config = currentSpawnSet.wireConfigs[i];

            // Создаём провод
            GameObject spawnedWire = Instantiate(config.wirePrefab, spawnPosition.position, Quaternion.identity);
            Debug.Log($"{spawnedWire == null} spawnedWire = NULL");
            // Передаём информацию о правильной дырке
            WireStart wireLogic = spawnedWire.GetComponent<WireStart>();
            Debug.Log($"{wireLogic == null} WireLogic = NULL" );
            Transform correctHole = holes[config.correctHoleIndex];
            Debug.Log($"{correctHole == null} correctHole = NULL");
            wireLogic.Setup(correctHole, holes);

            spawnedWires.Add(spawnedWire);
        }
    }
}
