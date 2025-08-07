using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 50;
    public float rotationSpeed = 100f;

    [HideInInspector] public CoinSpawner spawner;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            int currentCoins = PlayerPrefs.GetInt("coins", 0);
            currentCoins += coinValue;
            PlayerPrefs.SetInt("coins", currentCoins);
            PlayerPrefs.Save();

            CoinsDisplay display = FindObjectOfType<CoinsDisplay>();
            if (display != null)
            {
                display.RefreshCoinsUI();
            }

            if (spawner != null)
            {
                spawner.SpawnNewCoin();
            }

            Destroy(gameObject);
        }
    }
}
