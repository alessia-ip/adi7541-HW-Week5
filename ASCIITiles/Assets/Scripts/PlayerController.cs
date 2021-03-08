using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    
    public GameObject seeds;

    private bool plantable = true;

    private GameObject selectedSeed;

    private int waterAmt = 5;
    private int maxWater = 5;

    public Text waterText;
    public Text harvestedText;

    private int cropsHarvested = 0;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Today.Millisecond);
        rb = this.gameObject.GetComponent<Rigidbody2D>();
      
        waterAmt = maxWater;
        waterText.text = "Water amount: " + waterAmt;
        harvestedText.text = "Crops harvested: " + cropsHarvested;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D plantH = Physics2D.Raycast(
                transform.position, 
                Vector2.down);
            if (plantH.collider != null && plantH.distance < 0.15f && 
                plantH.collider.gameObject.name.Contains("Dirt"))
            {
                var newSeed = Instantiate<GameObject>(seeds);
                newSeed.transform.position = plantH.transform.position;
            }
            else if (waterAmt > 0  && 
                     plantH.collider.gameObject.name.Contains("Seed"))
            {
                if (selectedSeed.GetComponent<SeedGrow>().watered == false)
                {
                    selectedSeed.GetComponent<SeedGrow>().watered = true;
                    waterAmt--;
                    waterText.text = "Water amount: " + waterAmt;  
                } 
            }else if (selectedSeed.GetComponent<SeedGrow>().grown == true)
            {
                Destroy(selectedSeed.gameObject);
                var gains = Random.Range(1, 3);
                cropsHarvested += gains;
                harvestedText.text = "Crops harvested: " + cropsHarvested;
            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hitB = Physics2D.Raycast(
            transform.position, 
            Vector2.down);
        RaycastHit2D hitU = Physics2D.Raycast(
            transform.position, 
            Vector2.up);
        RaycastHit2D hitR = Physics2D.Raycast(
            transform.position, 
            Vector2.right);
        RaycastHit2D hitL = Physics2D.Raycast(
            transform.position, 
            Vector2.left);

        if (hitB.collider != null && hitB.distance < 0.15f && 
            hitB.collider.gameObject.tag == "NotWalkable")
        {
            Debug.Log(hitB.collider.gameObject.name);
            if (hitB.collider.gameObject.name.Contains("Pond") && 
                Input.GetKey(KeyCode.Space))
            {
                waterAmt = maxWater;
                waterText.text = "Water amount: " + waterAmt;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - TerrainParser.tileSize);
                rb.MovePosition(newPos);
            }
        }

        if (hitU.collider != null & hitU.distance < 0.15f  && hitU.collider.gameObject.tag == "NotWalkable")
        {
         
            if (hitU.collider.gameObject.name.Contains("Pond") && Input.GetKey(KeyCode.Space))
            {
                waterAmt = maxWater;
                waterText.text = "Water amount: " + waterAmt;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + TerrainParser.tileSize);
                rb.MovePosition(newPos);
            }
        }
        
        if (hitR.collider != null & hitR.distance < 0.15f  && hitR.collider.gameObject.tag == "NotWalkable")
        {

            if (hitB.collider.gameObject.name.Contains("Pond") && Input.GetKey(KeyCode.Space))
            {
                waterAmt = maxWater;
                waterText.text = "Water amount: " + waterAmt;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                Vector2 newPos = new Vector2(transform.position.x + TerrainParser.tileSize, transform.position.y);
                rb.MovePosition(newPos);
            }
        }
        
        if (hitL.collider != null & hitL.distance < 0.15f  && hitL.collider.gameObject.tag == "NotWalkable")
        {

            if (hitB.collider.gameObject.name.Contains("Pond") && Input.GetKey(KeyCode.Space))
            {
                waterAmt = maxWater;
                waterText.text = "Water amount: " + waterAmt;
            }
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Seed"))
        {
            plantable = false;
            selectedSeed = other.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Contains("Seed"))
        {
            plantable = false;
            selectedSeed = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Contains("Seed"))
        {
            plantable = true;
            selectedSeed = null;
        }
        
    }
}
