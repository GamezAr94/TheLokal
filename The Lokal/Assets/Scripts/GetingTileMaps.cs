using UnityEngine;
using UnityEngine.Tilemaps;

public class GetingTileMaps : MonoBehaviour
{
    private Tilemap[] tileMaps;
    [SerializeField]
    private static Tilemap unwalkable;
    public Tilemap[] TileMaps { get => tileMaps; set => tileMaps = value; }
    public static Tilemap Unwalkable { get => unwalkable; set => unwalkable = value; }

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
    }
}
