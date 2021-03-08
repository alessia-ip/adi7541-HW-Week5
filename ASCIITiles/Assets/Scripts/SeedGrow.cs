using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrow : MonoBehaviour
{
    public float time = 0;

    public Sprite plant;

    public Sprite wateredSeed; 
    
    public bool watered = false;

    public bool grown = false;

    // Update is called once per frame
    void Update()
    {
        if (watered == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = wateredSeed;
            time += Time.deltaTime;
            Debug.Log(this.gameObject.name + time);
        }

        if (time >= 10)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = plant;
            grown = true;
        }
    }
}
