using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask groundLayer;
    public bool onGround;
    public bool onWall;
    public float collisionRadius = 0.25f;
    public Vector2 bottom, left, right;
    private Color circleColor = Color.blue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottom, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + right, collisionRadius, groundLayer)
         || Physics2D.OverlapCircle((Vector2)transform.position + left, collisionRadius, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        var positoins = new Vector2[] {bottom, left, right};

        Gizmos.DrawWireSphere((Vector2)transform.position + bottom, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + left, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + right, collisionRadius);
    }
}
