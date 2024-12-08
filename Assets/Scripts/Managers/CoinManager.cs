using System;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public TextMeshProUGUI coinText;
    
    private int coins = 0;
    private const string coinDataKey = "coinAmount";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadData();
    }

    public int GetCoins()
    {
        return coins;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt(coinDataKey, coins);
        coinText.text = coins.ToString();
        Debug.Log($"Coins: {coins}");
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            PlayerPrefs.SetInt(coinDataKey, coins);
            coinText.text = coins.ToString();
            Debug.Log($"Spent {amount} coins. Remaining: {coins}");
            return true;
        }
        else
        {
            Debug.Log("Not enough coins.");
            return false;
        }
    }

    private void LoadData()
    {
        coins = PlayerPrefs.GetInt(coinDataKey, 0);
        coinText.text = coins.ToString();
    }
}