using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem Instance { get; private set; }

    public int baseHealthUpgradeCost = 50;
    public int damageUpgradeCost = 75;
    public int attackSpeedUpgradeCost = 100;
    public int criticalChanceUpgradeCost = 125;
    public int rangeUpgradeCost = 150;

    public float criticalChanceIncrease = 0.05f;
    public float rangeIncrease = 1.0f;

    private Tower tower;

    private int _healthUpgradeLevel,
        _damageUpgradeLevel,
        _attackSpeedUpgradeLevel,
        _criticalChanceUpgradeLevel,
        _rangeUpgradeLevel;

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
        tower = FindObjectOfType<Tower>(); // Kuleyi bul
        LoadUpgradeLevels();
    }

    private void LoadUpgradeLevels()
    {
        _healthUpgradeLevel = PlayerPrefs.GetInt("_healthUpgradeLevel", 0);
        _damageUpgradeLevel = PlayerPrefs.GetInt("_damageUpgradeLevel", 0);
        _attackSpeedUpgradeLevel = PlayerPrefs.GetInt("_attackSpeedUpgradeLevel", 0);
        _criticalChanceUpgradeLevel = PlayerPrefs.GetInt("_criticalChanceUpgradeLevel", 0);
        _rangeUpgradeLevel = PlayerPrefs.GetInt("_rangeUpgradeLevel", 0);

        tower.UpdateUpgradeLevels(_healthUpgradeLevel, _damageUpgradeLevel, _attackSpeedUpgradeLevel,
            _criticalChanceUpgradeLevel, _rangeUpgradeLevel);
    }

    public void UpgradeBaseHealth()
    {
        if (CoinManager.Instance.SpendCoins(baseHealthUpgradeCost))
        {
            tower.IncreaseMaxHealth(10); // Maksimum sağlığı 10 artır
            _healthUpgradeLevel++;
            PlayerPrefs.SetInt("_healthUpgradeLevel", _healthUpgradeLevel);
            Debug.Log("Tower base health upgraded!");
        }
        else
        {
            Debug.Log("Coin is not enough to upgrade health!");
        }
    }

    public void UpgradeDamage()
    {
        if (CoinManager.Instance.SpendCoins(damageUpgradeCost))
        {
            tower.IncreaseDamage(0.2f); // Hasarı 1 artır
            PlayerPrefs.SetInt("_damageUpgradeLevel", _damageUpgradeLevel);
            Debug.Log("Tower damage upgraded!");
        }
        else
        {
            Debug.Log("Coin is not enough to upgrade damage!");
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (CoinManager.Instance.SpendCoins(attackSpeedUpgradeCost))
        {
            tower.IncreaseAttackSpeed(0.1f); // Saldırı hızını artır
            PlayerPrefs.SetInt("_attackSpeedUpgradeLevel", _attackSpeedUpgradeLevel);
            Debug.Log("Tower attack speed upgraded!");
        }
        else
        {
            Debug.Log("Coin is not enough to upgrade attack speed!");
        }
    }

    public void UpgradeCriticalChance()
    {
        if (CoinManager.Instance.SpendCoins(criticalChanceUpgradeCost))
        {
            tower.IncreaseCriticalChance(criticalChanceIncrease); // Kritik vuruş ihtimalini artır
            PlayerPrefs.SetInt("_attackSpeedUpgradeLevel", _criticalChanceUpgradeLevel);
            Debug.Log("Tower critical chance upgraded!");
        }
        else
        {
            Debug.Log("Coin is not enough to upgrade critical chance!");
        }
    }

    public void UpgradeRange()
    {
        if (CoinManager.Instance.SpendCoins(rangeUpgradeCost))
        {
            tower.IncreaseRange(rangeIncrease); // Menzili artır
            PlayerPrefs.SetInt("_rangeUpgradeLevel", _rangeUpgradeLevel);
            Debug.Log("Tower range upgraded!");
        }
        else
        {
            Debug.Log("Coin is not enough to upgrade range!");
        }
    }
}