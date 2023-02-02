using UnityEngine;

public class EnemyTest : MonoBehaviour, IDamagable
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
        GameManager.Instance.score += 1;
        Debug.Log(GameManager.Instance.score);
        GameManager.Instance.enemies.Remove(gameObject);
        Object.Destroy(gameObject, 0);
    }
}
