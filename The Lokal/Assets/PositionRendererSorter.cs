using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script to change the value of the Layer depends of the position in Y and after destry the script
public class PositionRendererSorter : MonoBehaviour
{
    private Renderer myRenderer;
    private int sortingOrderBase = 85;
    [SerializeField]
    private bool runOnlyOnce = false;

    public void Awake()
    {
        myRenderer = this.gameObject.GetComponent<Renderer>();
    }
    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase - (transform.position.y*3));
        if (runOnlyOnce)
        {
            Destroy(this);
        }
    }
}
