using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Node
{
    public float G { get; set; }
    public float H { get; set; }
    public float F { get; set; }
    public Node Parent { get; set; }
    public Vector3Int Position { get; set; }

    public Node(Vector3Int position)
    {
        this.Position = position;
    }
}
