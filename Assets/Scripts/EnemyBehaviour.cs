using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    //Animation and objective variables
    Animator enemyAnimator;
    float distanceToObjective;
    public GameObject objective;

    //Mechanics variables
    public GameObject projectile;
    public GameObject launchProjectileZone;
    public float currentEnemyLife;
    bool targetInRange;
    float shootTimer;

    NavMeshAgent enemyNavAgent;

    bool melee = false;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyLife = 150f;
        shootTimer = 0f;
        enemyAnimator = GetComponent<Animator>();
        enemyNavAgent = GetComponent<NavMeshAgent>();
        targetInRange = false;
        enemyNavAgent.updateUpAxis = false;

        if (launchProjectileZone == null) //SOMOS MELEE
        {
            enemyNavAgent.speed = enemyNavAgent.speed * 2.5f;
            currentEnemyLife = 80f;
            melee = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (targetInRange == false)
        {  enemyNavAgent.SetDestination(objective.transform.position);        }

        if (melee == false)
        {
            Vector3 desiredObjectivePosition = new Vector3(objective.transform.position.x, 0f,
                                                       objective.transform.position.z);
            float distance = Vector3.Distance(transform.position, desiredObjectivePosition);
            enemyAnimator.SetFloat("DistanceToTarget", distance);
            if (distance < 10f)
            {
                targetInRange = true;
                //enemyNavAgent.isStopped = true;
            }
            else
            {
                targetInRange = false;
                //enemyNavAgent.isStopped = false;
            }
            //if (ObjectiveInSight())
            //{
            ShootingBehaviour();
            //}
        }
    }

    void ShootAtObjective()
    {
        GameObject proj = Instantiate(projectile, launchProjectileZone.transform.position,
                                      launchProjectileZone.transform.rotation);

        proj.name = gameObject.name + " Missile";

        Vector3 playerDirection = (objective.transform.position - transform.position);
        playerDirection.y = 0f;
        playerDirection.Normalize();

        proj.GetComponent<Rigidbody>().AddForce(playerDirection * 1000f);
        //Por si acaso, para eitar cosas injustas haremos que desaparezca en 5 seg si no choca con nada.
        Destroy(proj, 5f);
    }
    
    bool ObjectiveInSight()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, transform.forward, out hit) &&
            hit.transform.gameObject.name == objective.name;
    }


    void ShootingBehaviour()
    {
        shootTimer += Time.deltaTime;
        if (targetInRange)
        {
            if (shootTimer > 1.0f)
            {
                ShootAtObjective();
                shootTimer = 0.0f;
            }
        }
        else
        {
            if (shootTimer > 2.0f)
            {
                ShootAtObjective();
                shootTimer = 0.0f;
            }
        }
    }


    public void ReceiveDamage(float hit)
    {
        currentEnemyLife -= hit;
        print("current " + gameObject.name + " life: " + currentEnemyLife);
        if (currentEnemyLife <= 0f)
        {
            Destroy(this.gameObject);
        }


    }


}
