using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarDebugger : MonoBehaviour
{
    private static AStarDebugger instance;

    public static AStarDebugger MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AStarDebugger>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Color openColor, closedColor, pathColor, currentColor, startColor, goalColor;

    [SerializeField]
    private GameObject debugTextPrefab;

    private List<GameObject> debugObjects = new List<GameObject>();

    public void CreateTiles(Vector3Int start, Vector3Int goal)
    {
        ColorTile(start, startColor);
        ColorTile(goal, goalColor);
    }

    public void ColorTile(Vector3Int position, Color color)
    {
        tilemap.SetTile(position, tile);
        tilemap.SetTileFlags(position, TileFlags.None);
        tilemap.SetColor(position, color);

    }
}
