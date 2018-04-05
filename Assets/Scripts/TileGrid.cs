using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGrid : MonoBehaviour {
    public int GridWidth;
    public int GridHeight;
    public float Spacing;

    public GridTile Tile;
    public List<GridTile> tiles;

    // Use this for initialization
    void Start() {
        for (int x = 0; x < GridWidth; x++) {
            for (int y = 0; y < GridHeight; y++) {
                GridTile currentTile = Instantiate(Tile, new Vector3(x * Spacing, 0, y * Spacing), Quaternion.identity)
                    .GetComponent<GridTile>();
                currentTile.transform.SetParent(gameObject.transform);
                currentTile.x = x;
                currentTile.y = y;
                currentTile.name = "Tile(" + currentTile.x + "," + currentTile.y + ")";
                tiles.Add(currentTile);
            }
        }
    }

    public GridTile GetTile(int x, int y) {
        return transform.GetChild(x * GridHeight + y).GetComponent<GridTile>();
    }

    public GridTile GetRandomTile(bool empty) {
        int count = 1;
        if (empty) count = 2;

        List<GridTile> emptyTiles = tiles.Where(t => t.transform.GetChild(0).childCount < count).ToList();
        return emptyTiles[Random.Range(0, emptyTiles.Count - 1)];
    }

    public GridTile GetRandomTile() {
        return GetTile(Random.Range(0, GridWidth - 1), Random.Range(0, GridHeight - 1));
    }
}