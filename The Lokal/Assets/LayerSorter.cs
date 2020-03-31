using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    private SpriteRenderer parentRenderer;
    private List<FurnitureObstacles> furnitureO = new List<FurnitureObstacles>();
    // Start is called before the first frame update
    
    void Start()
    {
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    //detects any trigger with objects with tag and change the layer to display at the back ob the sprite
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Chair" || collision.tag == "Walls" || collision.tag == "Tables")
        {

            FurnitureObstacles fO = collision.GetComponent<FurnitureObstacles>();
            if(furnitureO.Count == 0 || fO.MySpriteRender.sortingOrder-1 < parentRenderer.sortingOrder)
            {
                parentRenderer.sortingOrder = fO.MySpriteRender.sortingOrder - 1;
            }
            furnitureO.Add(fO);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Chair" || collision.tag == "Walls" || collision.tag == "Tables")
        {
            FurnitureObstacles fO = collision.GetComponent<FurnitureObstacles>();
            furnitureO.Remove(fO);
            if(furnitureO.Count == 0)
            {
                parentRenderer.sortingOrder = 300;
            }
            else
            {
                furnitureO.Sort();
                parentRenderer.sortingOrder = furnitureO[0].MySpriteRender.sortingOrder - 1;
            }
        }
    }
}

