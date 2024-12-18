using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager Instance { get; private set; }

    [HideInInspector] public UnityEvent onLevelRestarted;
    [HideInInspector] public UnityEvent onWaveStarted;
    public TextMeshProUGUI textLevel;
    public Button startLevelButton;
    public GameObject bottomPanel;
    public int currentLevel = 1;    // Şu anki level
    public int baseEnemyCount = 10; // Başlangıç düşman sayısı
    public int baseEnemyHealth = 3; // Başlangıç düşman canı
    public float baseEnemySpeed = 2f; // Başlangıç düşman hızı
    public int remainingEnemyCount;

    private const string LevelDataKey = "currentLevel";
    private LevelEndUI _levelEndUI;

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
        _levelEndUI = GetComponent<LevelEndUI>();
        LoadData();
        startLevelButton.onClick.AddListener(StartNextLevel);
    }

    private void StartNextLevel()
    {
        Time.timeScale = 1;
        bottomPanel.SetActive(false);
        remainingEnemyCount = 10;
        onWaveStarted.Invoke();
        StartCoroutine(SpawnEnemies());
    }

    private void CompleteLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt(LevelDataKey, currentLevel);
        textLevel.text = "Level " + currentLevel.ToString();
        ActivateSuccessPanel();
    }

    private IEnumerator SpawnEnemies()
    {
        int enemyCount = baseEnemyCount;
        remainingEnemyCount = enemyCount;
        int enemyHealth = baseEnemyHealth + ((currentLevel-1)/3);   // Düşman canını artır
        float enemySpeed = baseEnemySpeed + currentLevel * 0.1f; // Düşman hızını artır
        
        Debug.Log($"Wave {currentLevel} started with {enemyCount} enemies!");

        WaitForSeconds wfs = new WaitForSeconds(1f);
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = EnemySpawner.Instance.SpawnEnemy(); // Düşman oluştur
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.SetAttributes(enemyHealth, enemySpeed);
            yield return wfs;
        }
    }

    private void LoadData()
    {
        currentLevel = PlayerPrefs.GetInt(LevelDataKey, 1);
        textLevel.text = "Level " + currentLevel.ToString();
    }

    public void OnClick_StartLevel()
    {
        Debug.Log("OnClick_StartLevel");
    }

    public void CheckSuccess()
    {
        remainingEnemyCount--;
        if (remainingEnemyCount <= 0)
        {
            CompleteLevel();
            ActivateSuccessPanel();
        }
    }

    public void ActivateFailPanel()
    {
        _levelEndUI.ActivateFailPanel();
    }

    private void ActivateSuccessPanel()
    {
        _levelEndUI.ActivateSuccessPanel();
    }
}