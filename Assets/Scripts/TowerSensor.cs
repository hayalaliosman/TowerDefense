using UnityEngine;

public class TowerSensor : MonoBehaviour
{
    [SerializeField] private Tower Tower;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !Tower.enemiesInRange.Contains(enemy))
            {
                Tower.enemiesInRange.Add(enemy); // Menzile giren düşmanı listeye ekle
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && Tower.enemiesInRange.Contains(enemy))
            {
                Tower.enemiesInRange.Remove(enemy); // Menzilden çıkan düşmanı listeden çıkar
            }
        }
    }
}
