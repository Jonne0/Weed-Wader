using UnityEngine;

public class AreaAttackBullet : MonoBehaviour
{
    [SerializeField] private float LifeSpan;
    [SerializeField] private GameObject AreaAttack;
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

            Debug.Log("Dead bullet");
            Instantiate(AreaAttack, transform.position, transform.rotation);

            Destroy(gameObject);



        }
    }
}
