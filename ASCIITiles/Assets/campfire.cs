using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campfire : MonoBehaviour
{
    private float time = 0;
    public Sprite camp1;
    public Sprite camp2;

    private void Update()
    {
        time += Time.deltaTime;
        if (time < 0.5)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = camp1;
        }
        else if (time > 1)
        {
            time = 0;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = camp2;
        }
    }
}
