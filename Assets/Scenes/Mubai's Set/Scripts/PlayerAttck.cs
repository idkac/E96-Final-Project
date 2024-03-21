using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PlayerAttck : MonoBehaviour
{
    public Enemy_behaviour[] enemies;
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
            enemies[idx].deductHP(50);
        }
    }
}
