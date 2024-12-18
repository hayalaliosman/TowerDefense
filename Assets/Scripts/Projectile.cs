using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;  // Mermi hızı
    public int damage = 1;     // Merminin verdiği hasar
    private Transform target;  // Hedef transform

    public void Initialize(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Hedef yoksa mermiyi yok et
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * (speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        Debug.Log("HitTarget");
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Düşmana hasar ver
        }

        Destroy(gameObject); // Mermiyi yok et
    }
}