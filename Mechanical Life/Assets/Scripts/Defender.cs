using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    public Level0 level;
    public bool EnemyInLane = false;
    public float Health;
    public int Cost;
    void Awake(){
        level = GameObject.FindObjectOfType<Level0>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckForEnemy();
    }

    public bool TakeDamage(float damage)
    {
        bool wasDestroyed = false;
        Health characterHealth = gameObject.GetComponent<Health>();
        characterHealth.DealDamage(damage);
        return wasDestroyed;
    }

    public void CheckForEnemy()
    {
        List<Attacker> atackers = ScreenManager._instance.Attackers;
        foreach (Attacker attacker in atackers)
        {
            if (attacker != null && level.CharactersInSameLane(gameObject, attacker.gameObject))
            {
                EnemyInLane = true;
                break;
            }
            else
            {
                EnemyInLane = false;
            }
        }
    }
}
