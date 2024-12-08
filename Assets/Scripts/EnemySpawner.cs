using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    public GameObject enemyPrefab;  // Düşman prefabı
    public Transform tower;        // Kule referansı
    public float spawnRadius = 10f; // Kuleden uzaklık

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

    public GameObject SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        return Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Kuleyi merkez alarak bir rastgele yön oluştur
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = tower.position + new Vector3(randomDirection.x, 0f, randomDirection.y) * spawnRadius;

        // Yerden yüksekliği ayarlayın (ör. zemin yüksekliği 0 olarak ayarlanmış)
        spawnPosition.y = -0.05f;

        return spawnPosition;
    }
}