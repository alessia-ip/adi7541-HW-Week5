using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrow : MonoBehaviour
{
    //time elapsed
    public float time = 0;

    //sprite for a grown plant
    public Sprite plant;

    //sprite for a watered seed
    public Sprite wateredSeed; 
    
    //is it watered?
    public bool watered = false;

    //is it grown?
    public bool grown = false;

    // Update is called once per frame
    void Update()
    {
        //start the timer after the plant is watered
        if (watered == true)
        {
            //change the sprite to the new seed
            this.gameObject.GetComponent<SpriteRenderer>().sprite = wateredSeed;
            time += Time.deltaTime; //then start to increment the time
            Debug.Log(this.gameObject.name + time);
        }

        if (time >= 10)
        {
            //when ten seconds have elapsed, switch it to a full grown plant
            this.gameObject.GetComponent<SpriteRenderer>().sprite = plant;
            grown = true;
        }
    }
}
