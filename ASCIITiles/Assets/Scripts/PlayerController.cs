using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    //player rigidbody
    private Rigidbody2D rb;
    
    //these are 
    public GameObject seeds;

    //is the current space a plantable space
    public bool plantable = true;

    //the currently selected seed object, if there is one
    private GameObject selectedSeed;

    //the water the player is carrying, and the maximum water a player is carrying
    private int waterAmt = 5;
    private int maxWater = 5;

    //the UI objects for the amount of water and the amount of crops harvested 
    public Text waterText;
    public Text harvestedText;

    //the number of crops harvested
    private int cropsHarvested = 0;

    // Start is called before the first frame update
    void Start()
    {
        //I want every random number to be as random as possible, so the seed is based on the current second
        Random.InitState(System.DateTime.Today.Second);
        
        //rigidbody is the player's rigidbody
        rb = this.gameObject.GetComponent<Rigidbody2D>();
      
        //set the water amount to the max water amount
        waterAmt = maxWater;
        
        //set the OG canvas text
        waterText.text = "Water amount: " + waterAmt;
        harvestedText.text = "Crops harvested: " + cropsHarvested;
    }


    private void Update()
    {
        //this is the context decisions for all of the space bar presses, EXCEPT for collecting water
        //collecting water is handled in Fixed Update
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //cast a ray down from the player
            RaycastHit2D plantH = Physics2D.Raycast(
                transform.position, 
                Vector2.down);
            
            if (plantH.collider != null && //if the player does hit a collider
                plantH.distance < 0.01f &&  //AND it is within the distance of a tile
                plantH.collider.gameObject.name.Contains("Dirt")) //AND the object is dirt
            {
                var newSeed = Instantiate<GameObject>(seeds); //plant a seed
                newSeed.transform.position = plantH.transform.position; //put that seed on the tile that was detected
            }
            else if (waterAmt > 0  && //otherwise, if you water is above zero
                     plantH.collider.gameObject.name.Contains("Seed") &&  //AND the hit object is a Seed
                     selectedSeed.GetComponent<SeedGrow>().grown == false)  //AND the seed has not yet grown
            {
                //if the seed is neither watered NOR grown
                if (selectedSeed.GetComponent<SeedGrow>().watered == false && 
                    selectedSeed.GetComponent<SeedGrow>().grown == false)
                {
                    selectedSeed.GetComponent<SeedGrow>().watered = true; //water the seed!
                    waterAmt--; //subtract one unit of water from the player
                    waterText.text = "Water amount: " + waterAmt;  //re-set the Ui to the new amount
                } 
            }
            else if (selectedSeed.GetComponent<SeedGrow>().grown == true) //otherwise, is the seed is grown
            {
                Destroy(selectedSeed.gameObject); //destroy the seed
                var gains = Random.Range(1, 5); //generate a new 'Gains' into to represent the crops
                cropsHarvested += gains; //add gains to current crops gathered
                harvestedText.text = "Crops harvested: " + cropsHarvested; //update the UI with the new crops
            }

        }
    }

    // Update is called once per frame
    // I've lowered the rate of fixed update, so the player will distinctly move one tile at a time
    void FixedUpdate()
    {
        //four raycasts
        //one in each cardinal direction
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

        //check first if you hit a 'not walkable' collider WITHIN 0.15f of the player (which is one tile)
        if (hitB.collider != null && hitB.distance < TerrainParser.tileSize && 
            hitB.collider.gameObject.tag == "NotWalkable")
        {
            Debug.Log(hitB.collider.gameObject.name);
            //if the collider is a pond, AND the player hits the space bar, collect water!
            if (hitB.collider.gameObject.name.Contains("Pond") && 
                Input.GetKey(KeyCode.Space))
            {
                //set the water amount back to the max water amount you can carry
                waterAmt = maxWater;
                
                //re-print the water amount to the UI
                waterText.text = "Water amount: " + waterAmt;
            }
        }
        else //if the player does NOT have a not walkable tile below themself
        {
            if (Input.GetKey(KeyCode.S)) //allow the player to walk downwards!
            {
                Vector2 newPos = new Vector2(
                    transform.position.x, 
                    transform.position.y - TerrainParser.tileSize); //the new position is one tile away
                rb.MovePosition(newPos); //set the position
            }
        }

        //repeat the above for the 'up' direction
        if (hitU.collider != null & hitU.distance < TerrainParser.tileSize  && 
            hitU.collider.gameObject.tag == "NotWalkable")
        {
         
            if (hitU.collider.gameObject.name.Contains("Pond") && 
                Input.GetKey(KeyCode.Space))
            {
                waterAmt = maxWater;
                waterText.text = "Water amount: " + waterAmt;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector2 newPos = new Vector2(
                    transform.position.x, 
                    transform.position.y + TerrainParser.tileSize);
                rb.MovePosition(newPos);
            }
        }
        
        //repeat the above for the 'right' direction
        if (hitR.collider != null & hitR.distance < TerrainParser.tileSize  && 
            hitR.collider.gameObject.tag == "NotWalkable")
        {

            if (hitR.collider.gameObject.name.Contains("Pond") && 
                Input.GetKey(KeyCode.Space))
            {
                waterAmt = maxWater;
                waterText.text = "Water amount: " + waterAmt;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                Vector2 newPos = new Vector2(
                    transform.position.x + TerrainParser.tileSize, 
                    transform.position.y);
                rb.MovePosition(newPos);
            }
        }
        
        //repeat the above for the left direction
        if (hitL.collider != null & hitL.distance < TerrainParser.tileSize && 
            hitL.collider.gameObject.tag == "NotWalkable")
        {

            if (hitL.collider.gameObject.name.Contains("Pond") && 
                Input.GetKey(KeyCode.Space))
            {
                waterAmt = maxWater;
                waterText.text = "Water amount: " + waterAmt;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                Vector2 newPos = new Vector2(
                    transform.position.x - TerrainParser.tileSize, 
                    transform.position.y);
                rb.MovePosition(newPos);
            }
        }

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Seed"))
        {
            plantable = false; //if  a spot has a seed, don't plant new stuff there
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
            plantable = true; //if no seed, do plant new stuff there :) 
            selectedSeed = null;
        }
        
    }
}
