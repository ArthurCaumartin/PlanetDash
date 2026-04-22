using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData_", menuName = "Mwa/WaveData")]
public class ScriptableWaveData : ScriptableObject
{


    [SerializeField] float _delayBetweenSpawn;
    [SerializeField] private List<WaveSpawn> _waveSpawnList = new List<WaveSpawn>();

    public List<WaveSpawn> WaveSpawnList => _waveSpawnList;
    public float DelayBetweenSpawn => _delayBetweenSpawn;
}