using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public float Health { get; set; } = 5f;

    public void TakeDamage(float amount)
    {
        Health -= amount;
    }


    void Update()
    {

        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //die animation
        EnemyBehaviour behaviour = gameObject.GetComponent<EnemyBehaviour>();
        GameManager.Instance.score += behaviour.score;
        GameManager.Instance.enemies.Remove(gameObject);
        //play score exploding animation so for arcade feel
        GameManager.Instance.SpawnFromKill(this, this.transform.position);

        //explode in multiple seeds
        Object.Destroy(gameObject, 0);
    }
}
