using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy_behaviour enemy;
    [SerializeField] float bulletDamage = 25;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "bullet")
        {
            enemy.deductHP(bulletDamage);
        }
    }
}
