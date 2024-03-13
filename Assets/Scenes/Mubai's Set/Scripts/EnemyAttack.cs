using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public NewBehaviourScript player;
    public Enemy_behaviour enemy;
    public float weaponDamage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player" && enemy.checkHaveAttacked())
        {
            player.deductHP(weaponDamage);
        }
    }
}
