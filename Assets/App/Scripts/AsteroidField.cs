using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour {
    public GameObject asteroidPrefab;

    public float fieldSize;
    public float cellSize;
    public int density;
    public float minScale;
    public float maxScale;

    private List<GameObject> asteroids = new List<GameObject>();

    public void Awake() {
        if((int)fieldSize % 2 != 0) {
            fieldSize += 1;
        }

        float fieldMin = -(fieldSize / 2) * cellSize;
        for(int z = 0; z < fieldSize; ++z) {
            for(int y = 0; y < fieldSize; ++y) {
                for(int x = 0; x < fieldSize; ++x) {
                    int chance = UnityEngine.Random.Range(0, 100);
                    if(chance > density) {
                        continue;
                    }

                    Vector3 position = new Vector3(fieldMin + (cellSize * x), fieldMin + (cellSize * y), fieldMin + (cellSize * z));
                    Vector3 offset = new Vector3(UnityEngine.Random.Range(-cellSize * 0.8f, cellSize * 0.8f), UnityEngine.Random.Range(-cellSize * 0.8f, cellSize * 0.8f), UnityEngine.Random.Range(-cellSize * 0.8f, cellSize * 0.8f));
                    GameObject asteroid = GameObject.Instantiate(asteroidPrefab);
                    asteroid.transform.SetParent(transform, false);
                    asteroid.transform.position = position + offset;
                    asteroid.transform.localScale = Vector3.one * UnityEngine.Random.Range(minScale, maxScale);
                    asteroid.transform.localRotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                    asteroids.Add(asteroid);
                }
            }
        }
    }
}
