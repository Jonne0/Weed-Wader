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
            Destroy(gameObject);
        }
    }
}
