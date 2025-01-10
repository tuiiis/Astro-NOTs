using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public WireSpawnSet currentSpawnSet; // Текущая конфигурация проводов
    public List<Transform> spawnPositions; // Координаты для спавна проводов
    public List<Transform> holes; // Список дырок (16 штук)
    public GameObject holePrefab; // Префаб дырки

    public static GameManager Instance { get; private set; }
    private List<GameObject> spawnedWires = new List<GameObject>();
    private List<GameObject> spawnedHoles = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
            // TODO: передать в wirestart ссылку на созданный объект и оттуда уже доставать данные для обработки (префаб содержит LineRenderer (у него важно направление), и конец провода)
            
            // Передаём информацию о правильной дырке

            Transform correctHole = holes[config.correctHoleIndex];

            WireStart wireStart = spawnedWire.GetComponentInChildren<WireStart>();

            wireStart.Setup(correctHole);

            spawnedWires.Add(spawnedWire);

            Debug.Log($"i:{i}, config.correctHoleIndex: {config.correctHoleIndex}");
        }
    }
}
