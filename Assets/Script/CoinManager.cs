using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class CoinManager : MonoBehaviour
{
    public List<Coin> coins; // List to hold references to all coins in the scene
    public int collectedCoins = 0; // Counter for collected coins

    
    [SerializeField]
    private TextMeshProUGUI coinCountText; // Reference to the TextMeshProUGUI component
    
    void Start()
    {
        // Initialize the list of coins
        coins = new List<Coin>(FindObjectsOfType<Coin>());
    }

    public void CoinCollected()
    {
        collectedCoins++;
        CalculateStars();
        UpdateCoinCountUI();
    }

    private void UpdateCoinCountUI()
    {
        if (coinCountText != null)
        {
            coinCountText.text = "Coins: " + collectedCoins.ToString();
        }
        else
        {
            Debug.LogWarning("Coin Count Text UI element is not assigned!");
        }
    }
    
    private void CalculateStars()
    {
        int totalCoins = coins.Count;
        int stars = 0;

        if (collectedCoins == totalCoins)
        {
            stars = 3; // All coins collected
        }
        else if (collectedCoins >= totalCoins * 0.66f)
        {
            stars = 2; // Two-thirds of the coins collected
        }
        else if (collectedCoins >= totalCoins * 0.33f)
        {
            stars = 1; // One-third of the coins collected
        }

        // Display or use the stars count as needed
        Debug.Log("Stars: " + stars);
    }
}