using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float LifeSpan;
    [SerializeField] private GameObject Impact;
    [SerializeField] private float _lifeSpawnDelta;

    void Start()
    {
        _lifeSpawnDelta = LifeSpan;
    }

    void Update()
    {

        _lifeSpawnDelta -= Time.deltaTime;

        if (_lifeSpawnDelta <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Enemy Damage
            // Enemy impact effect?
            IDamagable damagable = collision.GetComponent<IDamagable>();

            if (damagable != null)
            {
                GameManager.Instance.mana += 1;
                damagable.TakeDamage(Damage);
            }

            Destroy(gameObject);
        }
        else
        {
            /*
            if (collision.GetComponent<EnemyBullet>() != null)
            {
                Destroy(gameObject);
            }
            */

        }
    }
}
