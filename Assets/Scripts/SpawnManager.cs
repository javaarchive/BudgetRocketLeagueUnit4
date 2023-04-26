using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject powerupPrefab;
    private int waveCount = 1;
    private int enemyCount;

    void Start()
    {
        
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length; 
        if(enemyCount == 0){
            Debug.Log("gg you dodged " + waveCount + " balls. let's add one more :) ");
            waveCount ++;
            SpawnEnemy(waveCount);
            SpawnPowerups(waveCount);
        }
    }

    private void SpawnEnemy(int wave){
        for(int i = 0; i < wave; i ++){
            Instantiate(enemyPrefab, NewPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerups(int wave){
        for(int i = 0; i < wave; i ++){
            Instantiate(powerupPrefab, NewPowerupPosition(), powerupPrefab.transform.rotation);
        }
    }

    private Vector3 NewPowerupPosition(){
        float spawnX = Random.Range(-9,9);
        float spawnZ = Random.Range(-6,6); 
        Vector3 randomPosition = new Vector3(spawnX, 1f, spawnZ);
        return randomPosition;
    }

    private Vector3 NewPosition(){
        float spawnX = Random.Range(-9,9);
        float spawnZ = Random.Range(-6,6); 
        Vector3 randomPosition = new Vector3(spawnX, 3f, spawnZ);
        return randomPosition;
    }
}
