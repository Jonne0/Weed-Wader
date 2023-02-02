using UnityEngine;

public class NormalBullet : MonoBehaviour
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
        if (collision.CompareTag("Enemy"))
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
            if (collision.GetComponent<NormalBullet>() != null)
            {
                Destroy(gameObject);
            }

        }
    }
}
