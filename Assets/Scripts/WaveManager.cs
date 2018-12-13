using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy variables")]
    public GameObject[] enemyTypes;
    public List<GameObject> currentWaveEnemies;

    [Header("Game variables")]
    public int currentWave;


    // Start is called before the first frame update
    void Start()
    {
        currentWave = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }






}
