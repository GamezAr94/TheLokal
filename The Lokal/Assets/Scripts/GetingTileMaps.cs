using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GetingTileMaps : MonoBehaviour
{
    private Tilemap[] tileMaps;
    [SerializeField]
    private static Tilemap unwalkable;

    private static Tilemap lineCashier;
    public Tilemap[] TileMaps { get => tileMaps; set => tileMaps = value; }
    public static Tilemap Unwalkable { get => unwalkable; set => unwalkable = value; }
    public static Tilemap LineCashier { get => lineCashier; set => lineCashier = value; }

    public static List<Vector3> positionInLine = new List<Vector3>();

    void Awake()
    {
        TileMaps = Tilemap.FindObjectsOfType<Tilemap>();
        foreach (Tilemap t in TileMaps)
        {
            if (t.CompareTag("UnwalkableTilemap"))
            {
                Unwalkable = t;
            }
        }
        foreach (Tilemap t in TileMaps)
        {
            if (t.CompareTag("Line"))
            {
                LineCashier = t;
            }
        }
        #region Testear la posicion de las linea
        foreach (Vector3Int position in LineCashier.cellBounds.allPositionsWithin)
        {
            Vector3 positionTemp = LineCashier.CellToWorld(position);
            if (LineCashier.GetTile(position))
            {
                positionInLine.Add(positionTemp);
                Debug.Log("1");
            }
        }
        #endregion
    }
}
