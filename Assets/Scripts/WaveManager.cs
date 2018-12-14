using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy variables")]
    public GameObject[] enemyTypes;
    public List<GameObject> currentWaveEnemies;

    float spawnTimer = 0f;
    float spawnInterval = 0f;
    int activeEnemyIndex = 0;
    bool whileSpawn = false;

    [Header("Environment & player variables")]
    public GameObject doorGameObject;
    float doorLife = 300f;
    float playerLife = 150f;

    [Header("Game variables")]
    public int currentWave;
    public static WaveManager currentInstance;
    public GameObject[] spawners;
    public Transform enemyPool;

    [Header("UI variables")]
    public Slider doorUI;
    public Image doorUIFill;
    public Slider playerUI;
    public Image playerUIFill;

    // Start is called before the first frame update
    void Start()
    {
        currentInstance = this;
        currentWave = 0;
        ProceedToNextWave();
        SetUIProperties();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIProperties();

        //Si borramos algun enemigo, lo quitamos de aqui.
        for (int i = 0; i < currentWaveEnemies.Count; i++)
        {
            if (currentWaveEnemies[i] == null)
            { currentWaveEnemies.RemoveAt(i); }
            else if (whileSpawn == false)
            {    currentWaveEnemies[i].SetActive(true);     }
        }
        //Si no quedan enemigos, pasamos a la siguiente ronda.
        if (currentWaveEnemies.Count == 0)
        { ProceedToNextWave(); }

        //Spawneamos los enemigos.
        if (whileSpawn)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnInterval)
            {
                //pr si el jugador elimina a alguien cuando todos no están aun spawneados.
                if (activeEnemyIndex >= currentWaveEnemies.Count)
                { whileSpawn = false; }
                else
                {
                    currentWaveEnemies[activeEnemyIndex].SetActive(true);
                    activeEnemyIndex++;
                    spawnTimer = 0f;
                }
            }
        }
    }

    public void ReduceLifeFromObjective(GameObject objective, GameObject sender)
    {
        if (objective.tag == "Enemy" && sender.tag != "Enemy")
        {
            objective.GetComponent<EnemyBehaviour>().ReceiveDamage(20f, sender);
        }
        else if (objective.name == doorGameObject.name && sender.tag != "MainCamera")
        {
            if (sender.name.Contains("Missile"))
            { doorLife -= 5f; }
            else { doorLife -= 10f; }
            
        }
        if (objective.tag == "MainCamera")
        {
            if (sender.name.Contains("Missile"))
            { playerLife -= 5f; }
            else { playerLife -= 10f; }
            print(playerLife);
        }
        
    }

    public void ProceedToNextWave()
    {
        currentWave++;
        float numberOfEnemies = currentWave * 10;
        activeEnemyIndex = 0;
        spawnInterval = 1 / (currentWave - (currentWave / 2));
        whileSpawn = true;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            int spawnerIndex = Random.Range(0, spawners.Length);
            if (i % 2 == 0)
            {
                GameObject newEnemy = Instantiate(enemyTypes[0], spawners[spawnerIndex].transform.position, 
                                                                 Quaternion.identity, enemyPool);
                newEnemy.name = "Melee_" + i / 2;
                currentWaveEnemies.Add(newEnemy);
                newEnemy.SetActive(false);
                //newEnemy.GetComponent<EnemyBehaviour>().SetNewTarget(doorGameObject);
            }
            else
            {
                GameObject newEnemy = Instantiate(enemyTypes[1], spawners[spawnerIndex].transform.position,
                                                                Quaternion.identity, enemyPool);
                newEnemy.name = "Ranged_" + i / 2;
                currentWaveEnemies.Add(newEnemy);
                newEnemy.SetActive(false);
                //newEnemy.GetComponent<EnemyBehaviour>().SetNewTarget(doorGameObject);
            }
        }

    }

    public void SetUIProperties()
    {
        doorUI.maxValue = doorLife;
        doorUI.value = doorLife;
        doorUIFill.color = Color.green;

        playerUI.maxValue = playerLife;
        playerUI.value = playerLife;
        playerUIFill.color = Color.green;

    }

    void UpdateUIProperties()
    {
        doorUI.value = doorLife;
        if (doorLife / doorUI.maxValue > 0.7)
        {
            doorUIFill.color = Color.green;
        }
        else if (doorLife / doorUI.maxValue > 0.3)
        {
            doorUIFill.color = Color.yellow;
        }
        else { doorUIFill.color = Color.red; }

        playerUI.value = playerLife;
        if (playerLife / playerUI.maxValue > 0.7)
        {
            playerUIFill.color = Color.green;
        }
        else if (playerLife / playerUI.maxValue > 0.3)
        {
            playerUIFill.color = Color.yellow;
        }
        else { playerUIFill.color = Color.red; }



    }






}
