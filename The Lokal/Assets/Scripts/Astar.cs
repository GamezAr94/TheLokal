using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Astar : MonoBehaviour
{
    #region Unwalkables Tiles
    /*
    [SerializeField]
    private Tilemap unwalkableTileMap;
    */

    private List<Vector3Int> UnwalkablePositions;
    #endregion

    private Vector3Int startPos, goalPos;

    private Node current;

    private HashSet<Node> openList;

    private HashSet<Node> closedList;

    private Stack<Vector3> path;

    private Dictionary<Vector3, Node> allNodes = new Dictionary<Vector3, Node>();

    private List<Vector3Int> obstacleTiles = new List<Vector3Int>();

    public void Awake()
    {
        Tilemap unwalkableTileMap = GetingTileMaps.Unwalkable;

        GameObject[] obstacle = GameObject.FindGameObjectsWithTag("Obstacles");

        #region Initializing List with Unwalkable positions
        UnwalkablePositions = new List<Vector3Int>();

        foreach (Vector3Int position in unwalkableTileMap.cellBounds.allPositionsWithin)
        {
            Vector3 positionTemp = unwalkableTileMap.CellToWorld(position);
            if (unwalkableTileMap.GetTile(position))
            {
                UnwalkablePositions.Add(new Vector3Int((int)Math.Round(positionTemp.x), (int)Math.Round(positionTemp.y), 0));
            }
        }
        #endregion

        for (int i = 0; i < obstacle.Length; i++)
        {
            Vector3Int waterPosition = new Vector3Int((int)obstacle[i].transform.position.x, (int)obstacle[i].transform.position.y, 0);
            obstacleTiles.Add(waterPosition);
        }
    }
    public Stack<Vector3> Algorithm(Vector3 start, Vector3 goal)
    {
        /*
        startPos = tileMap.WorldToCell(start);
        goalPos = tileMap.WorldToCell(goal);
        
        Debug.Log(goal.x + " " + goal.y);
        Debug.Log(tileMap.WorldToLocal(new Vector3Int((int)goal.x, (int)goal.y, (int)0)));
        */
        
        goalPos = new Vector3Int((int)Math.Round(goal.x), (int)Math.Round(goal.y), 0);
        startPos = new Vector3Int((int)Math.Round(start.x),(int)Math.Round(start.y),0);

       // Debug.Log("Goal wordToLocal" + tileMap.WorldToLocal(new Vector3(goal.x, goal.y, 0)));
        // Debug.Log("Goal Normal" + Math.Round(goal.x) + " " + Math.Round(goal.y) + " " + 0);
        
        current = GetNode(startPos);
        openList = new HashSet<Node>();

        closedList = new HashSet<Node>();

        openList.Add(current);
        path = null;

        while (openList.Count > 0 && path == null)
        {
            List<Node> neighbours = findNeighbors(current.Position);
            ExamineNeighbors(neighbours, current);
            UpdateCurrentTile(ref current);
            path = GeneratePath(current);
        }
        if (path != null)
        {
            return path;
        }
        return null;
    }
    
    private List<Node> findNeighbors(Vector3Int parentPosition)
    {
        List<Node> neighbors = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector3Int neighborPos = new Vector3Int(parentPosition.x - x, parentPosition.y - y, parentPosition.z);
                if (y != 0 || x != 0)
                {
                    if (neighborPos != startPos && !obstacleTiles.Contains(neighborPos) && !UnwalkablePositions.Contains(neighborPos))
                    {
                        Node neighbor = GetNode(neighborPos);
                        neighbors.Add(neighbor);
                    }
                }
            }
        }
        return neighbors;
    }

    private void ExamineNeighbors(List<Node> neighbors, Node current)
    {
        for (int i = 0; i < neighbors.Count; i++)
        {
            Node neighbor = neighbors[i];
            
            if (!ConnectedDiagonally(current, neighbor))
            {
                continue;
            }
            int gScore = (int)DetermineGScore(neighbors[i].Position, current.Position);

            if (openList.Contains(neighbor))
            {
                if (current.G + gScore < neighbor.G)
                {
                    CalcValue(current, neighbor, gScore);
                }
            }
            else if (!closedList.Contains(neighbor))
            {
                CalcValue(current, neighbor, gScore);

                openList.Add(neighbor);
            }
        }
    }

    private void CalcValue(Node parent, Node neighbor, int cost)
    {
        neighbor.Parent = parent;

        neighbor.G = parent.G + cost;

        neighbor.H = ((Math.Abs((neighbor.Position.x - goalPos.x)) + Math.Abs((neighbor.Position.y - goalPos.y))) * 10);

        neighbor.F = neighbor.G + neighbor.H;

    }
    
    private float DetermineGScore(Vector3 neighbor, Vector3 current)
    {
        float gScore = 0;

        float x = (current.x - neighbor.x);
        float y = (current.y - neighbor.y);

        if (Math.Abs(x - y) % 2 == 1)
        {
            gScore = 10;
        }
        else
        {
            gScore = 10;
        }
        return gScore;
    }
    private void UpdateCurrentTile(ref Node current)
    {
        openList.Remove(current);

        closedList.Add(current);

        if (openList.Count > 0)
        {
            current = openList.OrderBy(x => x.F).First();
        }
    }
    private Node GetNode(Vector3Int position)
    {
        if (allNodes.ContainsKey(position))
        {
            return allNodes[position];
        }
        else
        {
            Node node = new Node(position);
            allNodes.Add(position, node);
            return node;
        }
    }
    


    
    //Avoid Walk in Diagonall
    private bool ConnectedDiagonally(Node currentNode, Node neighbor)
    {
        Vector3Int direct = currentNode.Position - neighbor.Position;

        Vector3Int first = new Vector3Int(current.Position.x + (direct.x * -1), current.Position.y, current.Position.z);
        Vector3Int second = new Vector3Int(current.Position.x, current.Position.y + (direct.y * -1), current.Position.z);

        if (obstacleTiles.Contains(first) || obstacleTiles.Contains(second))
        {
            return false;
        }
        return true; ;
    }

    private Stack<Vector3> GeneratePath(Node current)
    {
        if (current.Position == goalPos)
        {
            Stack<Vector3> finalPath = new Stack<Vector3>();

            while (current.Position != startPos)
            {
                finalPath.Push(current.Position);

                current = current.Parent;
            }
            return finalPath;
        }
        return null;
    }
}
