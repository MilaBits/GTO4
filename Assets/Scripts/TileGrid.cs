using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

    public int GridWidth;
    public int GridHeight;
    public float Spacing;
    
    public GridTile Tile;

    // Use this for initialization
    void Start() {
        for (int x = 0; x < GridWidth; x++) {
            for (int y = 0; y < GridHeight; y++) {
                GridTile currentTile = Instantiate(Tile, new Vector3(x * Spacing, 0, y * Spacing), Quaternion.identity).GetComponent<GridTile>();
                currentTile.transform.SetParent(gameObject.transform);
                currentTile.x = x;
                currentTile.y = y;
                currentTile.name = "Tile(" + currentTile.x + "," + currentTile.y + ")";
                
            }
        }
    }

    public GridTile GetTile(int x, int y)
    {
        return transform.GetChild(x * GridHeight + y).GetComponent<GridTile>();
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
