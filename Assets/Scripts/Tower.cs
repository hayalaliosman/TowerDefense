using System;
using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    public float maxHealth = 10;  // Kulenin maksimum canı
    
    public GameObject projectilePrefab; // Mermi prefabı
    public Transform firePoint;         // Merminin çıktığı nokta
    //public float fireRate = 1f;         // Ateş etme sıklığı
    public float range = 5f;            // Kulenin menzili
    public List<Enemy> enemiesInRange = new List<Enemy>(); // Menzildeki düşmanlar

    [SerializeField] private TowerSensor towerSensor;
    private float fireCooldown = 0f;
    private float currentHealth; // Mevcut canı
    private float damage = 10;
    private float attackSpeed = 1f;
    private float criticalChance = 0.1f;

    private void Start()
    {
        currentHealth = maxHealth;
        GameManager.Instance.tower = this;
        LevelManager.Instance.onWaveStarted.AddListener(OnWaveStarted);
    }
    
    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Menzilde düşman varsa ve ateş zamanı geldiyse
        if (enemiesInRange.Count > 0 && fireCooldown <= 0f)
        {
            FireAtEnemy(enemiesInRange[0]); // İlk düşmana ateş et
            fireCooldown = 1f / attackSpeed;  // Cooldown sıfırla
        }
    }
    
    private void FireAtEnemy(Enemy target)
    {
        if (target == null) return; // Hedef geçerli değilse işlem yapma

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.Initialize(target.transform); // Hedefi projeye at
    }

    public void UpdateUpgradeLevels(int healthUpgradeLevel, int damageUpgradeLevel, int attackSpeedUpgradeLevel, int criticalChanceUpgradeLevel, int rangeUpgradeLevel)
    {
        IncreaseMaxHealth(healthUpgradeLevel * 10);
        IncreaseDamage(damageUpgradeLevel * 0.2f);
        IncreaseAttackSpeed(attackSpeedUpgradeLevel * 0.1f);
        IncreaseCriticalChance(criticalChanceUpgradeLevel * 0.05f);
        IncreaseRange(rangeUpgradeLevel);
    }
    

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Tower health: {currentHealth}");

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }
    
    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
    }

    public void IncreaseDamage(float amount)
    {
        damage += amount;
    }

    public void IncreaseAttackSpeed(float amount)
    {
        attackSpeed += amount; // Daha hızlı saldırı için süreyi azaltıyoruz
        if (attackSpeed > 2f) attackSpeed = 2f; // Minimum sınır
    }

    public void IncreaseCriticalChance(float amount)
    {
        criticalChance += amount;
        if (criticalChance > 1f) criticalChance = 1f; // Kritik vuruş %100'ü geçmesin
    }

    public void IncreaseRange(float amount)
    {
        range += amount;
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Tower has been destroyed.");
        // Oyun sonlandırma işlemleri burada yapılabilir
        Time.timeScale = 0; // Oyun durdurulabilir
        LevelManager.Instance.ActivateFailPanel();
    }

    private void OnWaveStarted()
    {
        currentHealth = maxHealth;
        fireCooldown = 0f;
        enemiesInRange.Clear();
    }
}