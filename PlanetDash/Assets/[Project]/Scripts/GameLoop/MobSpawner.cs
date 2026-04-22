using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;



public class MobSpawner : MonoBehaviour
{
    [SerializeField] private ScriptableWaveData[] _waveData;
    [Space]
    [SerializeField] private PlayerMovement playerMovement;
    private Coroutine _currentSpawnCoroutine = null;

    private void Start()
    {
        _currentSpawnCoroutine = StartCoroutine(SpawnSequence(_waveData, 0));
    }

    public IEnumerator SpawnSequence(ScriptableWaveData[] waveData, int index)
    {
        print("Start Spawn Sequence");

        List<WaveSpawn> spawns = waveData[index].WaveSpawnList;
        List<MobBehavior> mobSpawned = new List<MobBehavior>();
        for (int i = 0; i < spawns.Count; i++)
        {
            print("Spawn !");
            for (int j = 0; j < spawns[i].count; j++)
            {
                MobBehavior newMob = Instantiate(spawns[i].mobPrefab, transform);
                MobMovement mobMovement = newMob.GetComponent<MobMovement>();
                float angle = Random.Range(0, Mathf.PI * 2);
                mobMovement.SetSurface(playerMovement.CurrentSurface, angle, spawns[i].spawnHeight);
                mobSpawned.Add(newMob);

            }
            yield return new WaitForSeconds(waveData[index].DelayBetweenSpawn);
        }
        yield return new WaitUntil(() => mobSpawned.TrueForAll(m => m == null));
        print("All ded GG !");

        if (index >= waveData.Length - 1)
        {
            print("End Spawn Sequence");
            _currentSpawnCoroutine = null;
            yield break;
        }

        _currentSpawnCoroutine = StartCoroutine(SpawnSequence(_waveData, index + 1));
    }
}
