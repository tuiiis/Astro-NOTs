using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WireSpawnSet", menuName = "Game/Wire Spawn Set")]
public class WireSpawnSet : ScriptableObject
{
    public List<WireSpawnConfig> wireConfigs; // Список проводов для спавна
    public List<WireSpawnConfig> wireConfigs1;
    public List<WireSpawnConfig> wireConfigs2;
    public List<WireSpawnConfig> wireConfigs3;
    public List<WireSpawnConfig> wireConfigs4;
    public List<WireSpawnConfig> wireConfigs5;

    public List<WireSpawnConfig> RandomSet()
    {
        List<List<WireSpawnConfig>> list = new List<List<WireSpawnConfig>>() { wireConfigs, wireConfigs1, wireConfigs2, wireConfigs3, wireConfigs4, wireConfigs5 };

        int randomIndex = Random.Range(0, list.Count);

        Debug.Log(randomIndex);

        return list[randomIndex];
    }
}

