using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private float spawnPos = 0;
    private float tileLength = 100;
    private List<GameObject> activeTiles = new List<GameObject>();

    [SerializeField] private Transform playerTransform;
    private int startTiles = 6;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }

        // Set the initial position of the player
        playerTransform.position = new Vector3(0, 0, spawnPos - tileLength);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 60> spawnPos - (startTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
