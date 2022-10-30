using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING, WAITING, COUNTING
    };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Transform[] spawnPoints;

    public Wave[] wave;
    private int nextWave = 0;

    public float TimebetweenWaves = 5f;
    private float waveCountdown = 0;

    private SpawnState state = SpawnState.COUNTING;

    private float searchCountdown = 1f;

    private PlayerUI playerUI;

    private void Start()
    {
        playerUI = GetComponent<PlayerUI>();

        if (spawnPoints.Length == 0)
        {
            Debug.Log("No Spawnpoints");
        }

        waveCountdown = TimebetweenWaves;
    }

    private void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                //Begin a new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING) 
            {
                StartCoroutine(SpawnWave(wave[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            playerUI.UpdateWaveText("NEW WAVE IN: " + waveCountdown);
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completeted");

        state = SpawnState.COUNTING;
        waveCountdown = TimebetweenWaves;

        if(nextWave + 1 > wave.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE");
            playerUI.UpdateWaveText("WAVE COMPLETE");
        } 
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        playerUI.UpdateWaveText("KILL THEM ALL");

        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        _enemy.tag = "Enemy";
        Instantiate(_enemy, _sp.position, _sp.rotation);
        _enemy.tag = "Untagged";
    }
}
