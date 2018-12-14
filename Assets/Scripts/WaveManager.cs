using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy variables")]
    public GameObject[] enemyTypes;
    public List<GameObject> currentWaveEnemies;

    [Header("Environment variables")]
    public GameObject doorGameObject;

    [Header("Game variables")]
    public int currentWave;
    public static WaveManager currentInstance;

    // Start is called before the first frame update
    void Start()
    {
        currentInstance = this;
        currentWave = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceLifeFromObjective(GameObject objective)
    {
        if (objective.tag == "Enemy")
        {
            objective.GetComponent<EnemyBehaviour>().ReceiveDamage(20f);
        }
        print("Ha chocado Paco!!!");
    }




}
