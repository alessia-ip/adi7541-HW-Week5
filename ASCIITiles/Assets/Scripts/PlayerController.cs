using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hitB = Physics2D.Raycast(transform.position, Vector2.down);
        RaycastHit2D hitU = Physics2D.Raycast(transform.position, Vector2.up);
        RaycastHit2D hitR = Physics2D.Raycast(transform.position, Vector2.right);
        RaycastHit2D hitL = Physics2D.Raycast(transform.position, Vector2.left);

        if (hitB.collider != null & hitB.distance < 0.15f)
        {
            Debug.Log(hitB.collider.gameObject.name);
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - TerrainParser.tileSize);
                rb.MovePosition(newPos);
            }
        }

        if (hitU.collider != null & hitU.distance < 0.15f)
        {
            
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + TerrainParser.tileSize);
                rb.MovePosition(newPos);
            }
        }
        
        if (hitR.collider != null & hitR.distance < 0.15f)
        {
            
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                Vector2 newPos = new Vector2(transform.position.x + TerrainParser.tileSize, transform.position.y);
                rb.MovePosition(newPos);
            }
        }
        
        if (hitL.collider != null & hitL.distance < 0.15f)
        {
            
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                Vector2 newPos = new Vector2(transform.position.x - TerrainParser.tileSize, transform.position.y);
                rb.MovePosition(newPos);
            }
        }

    }
}
