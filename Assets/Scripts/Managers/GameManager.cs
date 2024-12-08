using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Tower tower;
    public GameObject enemyDestroyParticleEffect;
    
    public static GameManager Instance { get; private set; }

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

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0; // Oyun durdurulur
        // Diğer oyun sonlandırma işlemleri (örneğin, bir Game Over UI'ı gösterme) burada yapılabilir.
    }
}