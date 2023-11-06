using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType {START,GOAL,WATER,GRASS,PATH}

public class Astar : MonoBehaviour
{
    private TileType tileType;

    [SerializeField]
    private Tilemap tileMap;

    [SerializeField]
    private Tile[] tiles;

    [SerializeField]
    private RuleTile water;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private LayerMask layerMask;

    private Vector3Int startPos, goalPos;
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity,layerMask);

            if(hit.collider != null)
            {
                Vector3 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickPos = tileMap.WorldToCell(mouseWorldPos);

                ChangeTile(clickPos);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Algorithm();
        }
    }

    private void Algorithm()
    {
        AStarDebugger.MyInstance.CreateTiles(startPos, goalPos);
    }

    public void ChangeTileType(TileButton button)
    {
        tileType = button.MyTileType;
        
    }

    private void ChangeTile(Vector3Int clickPos)
    {
        if(tileType == TileType.WATER)
        {
            tileMap.SetTile(clickPos, water);
        }
        else
        {
            if(tileType == TileType.START)
            {
                startPos = clickPos;
            }
            else if(tileType == TileType.GOAL)
            {
                goalPos = clickPos;
            }

            tileMap.SetTile(clickPos, tiles[(int)tileType]);
        }

        
    }
}
