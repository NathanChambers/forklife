using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flooring : MonoBehaviour {
    public GameObject floorTile;
    public int width;
    public int height;
    
    public float tileWidth;
    public float tileHeight;

    public void Start() {
        for(int y = 0; y < height; ++y) {
            for(int x = 0; x < width; ++x) {
                Vector3 pos = new Vector3((-(width*tileWidth) / 2) + (x*tileWidth) + (tileWidth / 2), 0.0f, (-(height*tileHeight) / 2) + (y*tileHeight) + (tileHeight / 2));
                GameObject tile = GameObject.Instantiate(floorTile);
                tile.transform.SetParent(transform);
                tile.transform.localPosition = pos;
                tile.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
