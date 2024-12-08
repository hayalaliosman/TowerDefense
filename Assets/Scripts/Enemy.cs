using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int coinValue = 10; // Bu düşmanı öldürmekten kazanılan coin
    
    private float health;    // Düşmanın canı
    private float speed;   // Düşmanın hareket hızı
    public int damageToTower = 1; // Kuleye vereceği hasar

    private Transform tower;
    private bool _destroyed = false;

    private void Start()
    {
        tower = GameObject.FindGameObjectWithTag("Tower").transform; // Kuleyi hedef al
        LevelManager.Instance.onLevelRestarted.AddListener((OnLevelRestarted));
    }

    private void Update()
    {
        MoveTowardsTower();
    }

    private void MoveTowardsTower()
    {
        if (tower != null)
        {
            Vector3 targetPos = new Vector3(tower.position.x, transform.position.y, tower.position.z);
            Vector3 direction = (targetPos - transform.position).normalized;
            transform.position += direction * (speed * Time.deltaTime);
        }
    }

    public void SetAttributes(int health, float speed)
    {
        this.health = health;
        this.speed = speed;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if(_destroyed)  return;

        _destroyed = true;
        CoinManager.Instance.AddCoins(coinValue);
        GameManager.Instance.tower.enemiesInRange.Remove(this);
        LevelManager.Instance.CheckSuccess();
        CreateDestroyEffect();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_destroyed)  return;
        
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Tower"))
        {
            Tower tower = other.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                tower.TakeDamage(damageToTower);
            }

            // Düşmanı yok et
            _destroyed = true;
            GameManager.Instance.tower.enemiesInRange.Remove(this);
            LevelManager.Instance.CheckSuccess();
            Destroy(gameObject);
        }
    }

    private void OnLevelRestarted()
    {
        CreateDestroyEffect();
        Destroy(gameObject);
    }

    private void CreateDestroyEffect()
    {
        var deathEffect = Instantiate(GameManager.Instance.enemyDestroyParticleEffect, transform.position, Quaternion.identity);
        deathEffect.GetComponent<ParticleSystem>().Play();
        Destroy(deathEffect, 2f);
    }
}