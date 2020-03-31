using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureObstacles : MonoBehaviour, IComparable<FurnitureObstacles>
{

    public SpriteRenderer MySpriteRender { get; set; }


    public int CompareTo(FurnitureObstacles other)
    {
        if(MySpriteRender.sortingOrder > other.MySpriteRender.sortingOrder)
        {
            return 1;
        }else if (MySpriteRender.sortingOrder < other.MySpriteRender.sortingOrder)
        {
            return -1;
        }
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        MySpriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
