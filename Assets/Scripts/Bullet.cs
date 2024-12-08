using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Mermi hızı
    public int damage = 1;     // Merminin verdiği hasar
    private Transform target;  // Hedef transform
    private bool _isTargetNull;
    private Transform _myTransform;

    private void Start()
    {
        _isTargetNull = target == null;
        _myTransform = transform;
    }

    public void Initialize(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (_isTargetNull)
        {
            Destroy(gameObject); // Hedef yoksa mermiyi yok et
            return;
        }

        // Hedefe doğru hareket et
        var direction = (target.position - transform.position).normalized;
        _myTransform.position += direction * (speed * Time.deltaTime);

        // Hedefe ulaştığında düşmana hasar ver ve mermiyi yok et
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        // Hedefe zarar ver
        var enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Hasar ver
        }

        Destroy(gameObject); // Mermiyi yok et
    }
}