using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;

    public Vector2 spawnRangeX = new Vector2(-10f, 10f);
    public Vector2 spawnRangeZ = new Vector2(-10f, 10f);
    public float spawnY = 0f;

    private GameObject currentCoin;

    void Start()
    {
        SpawnNewCoin();
    }

    public void SpawnNewCoin()
    {
        if (currentCoin != null) return;

        float x = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float z = Random.Range(spawnRangeZ.x, spawnRangeZ.y);
        Vector3 spawnPos = new Vector3(x, spawnY, z);

        currentCoin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);

        currentCoin.GetComponent<Coin>().spawner = this;
    }

    public void CoinCollected()
    {
        currentCoin = null;
        SpawnNewCoin();
    }
}
