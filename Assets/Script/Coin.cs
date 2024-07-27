using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinManager coinManager;

    [Header("Rotation Settings")]
    public float rotationSpeed = 100f;
    public Vector3 rotationAxis = Vector3.up;

    private void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
    }

    private void Update()
    {
        // Rotate the coin
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            coinManager.CoinCollected();
        }
    }
}