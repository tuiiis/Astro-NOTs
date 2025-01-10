using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WireSpawnSet", menuName = "Game/Wire Spawn Set")]
public class WireSpawnSet : ScriptableObject
{
    public List<WireSpawnConfig> wireConfigs; // Список проводов для спавна
}

