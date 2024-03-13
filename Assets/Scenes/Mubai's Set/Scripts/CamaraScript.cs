using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform target;
    Vector3 diff;

    void Start()
    {
        diff = new Vector3(0, 2, 0);
        transform.position = target.position + diff;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f) + diff;
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed*Time.deltaTime);
    }
}
