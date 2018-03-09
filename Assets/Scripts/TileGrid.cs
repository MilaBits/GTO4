using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

    public int gridWidth;
    public int gridHeight;
    public float spacing;
    
    public GameObject tile;

    public GameObject selectedTile;

    // Use this for initialization
    void Start() {
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                GridTile currentTile = Instantiate(tile, new Vector3(x * spacing, 0, y * spacing), Quaternion.identity).GetComponent<GridTile>();
                currentTile.transform.SetParent(gameObject.transform);
                currentTile.x = x;
                currentTile.y = y;
                currentTile.name = "Tile(" + currentTile.x + "," + currentTile.y + ")";
                
            }
        }
    }

    public GridTile GetTile(int x, int y)
    {
        return transform.GetChild(x * gridWidth + y).GetComponent<GridTile>();
    }

    // Update is called once per frame
    void Update() {

    }

    [ContextMenu("Test")] private void Test()
    {
        GridTile tile = GetTile(3, 10);
        Debug.Log(tile.name);
    }
}
