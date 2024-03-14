using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PlayerAttck : MonoBehaviour
{
    public Enemy_behaviour[] enemies;
    [SerializeField] float damage = 35;
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
        if (trig.gameObject.tag == "enemy")
        {
            GameObject parentObject = trig.gameObject.transform.parent.gameObject;
            string text = parentObject.name;
            string pattern = @"\d+$";
            Match match = Regex.Match(text, pattern);
            string numberString = match.Value;
            int idx = int.Parse(numberString) - 1;
            Debug.Log(idx);
            enemies[idx].deductHP(damage);
        }
    }
}
