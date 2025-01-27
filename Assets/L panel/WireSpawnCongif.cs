using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "WireSpawnConfig", menuName = "Game/Wire Spawn Configuration")]
public class WireSpawnConfig : ScriptableObject
{
    public GameObject wirePrefab; // Префаб провода
    public int correctHoleIndex; // Индекс дырки, куда провод должен подключиться
}

