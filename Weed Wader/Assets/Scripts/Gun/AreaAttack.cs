using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float LifeSpan;
    [SerializeField] private float _lifeSpawnDelta;
    [SerializeField] private float _damageTickRate;

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

        _damageTickRate += Time.deltaTime;

    }

    private void OnTriggerStay2D(Collider2D collision)
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

        }
    }
}
