using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform FirePoint;
    [SerializeField] private ParticleSystem MuzzleFlash;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject AreaAttackPrefab;
    [SerializeField] private float BulletForce;
    [SerializeField] private float Range = 10f;
    [SerializeField] private int MagSize = 2;
    [SerializeField] private float ReloadTime = 2f;
    [SerializeField] private float Cooldown = 0.5f;

    private float _cooldownTimeDelta;
    private float _reloadTimeDelta;
    private int _mag;

    private void Start()
    {
        _reloadTimeDelta = ReloadTime;
        _mag = MagSize;
    }


    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Jump"))
        {
            AreaAttack();
        }

        
        if (_mag <= 0)
        {
            Reload();
        }

        if (_cooldownTimeDelta > 0)
        {
            _cooldownTimeDelta -= Time.deltaTime;
        }


        // Debug:
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        Debug.DrawRay(FirePoint.position, direction * Range);

    }

    void AreaAttack()
    {
        /*
        if (GameManager.Instance.mana >= 5)
        {
            if (GameManager.Instance.mana - 5 >= 0)
                GameManager.Instance.mana -= 5;
            else
                GameManager.Instance.mana = 0;

            MuzzleFlash.Play();

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

            GameObject bullet = Instantiate(AreaAttackPrefab, FirePoint.position, FirePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * BulletForce / 2, ForceMode2D.Impulse);
        }
        */
    }

    void Shoot()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        if (_mag > 0 && _cooldownTimeDelta <= 0)
        {
            _mag -= 1;
            _cooldownTimeDelta = Cooldown;

            MuzzleFlash.Play();

            GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * BulletForce, ForceMode2D.Impulse);



            /*
            RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, direction, Range);

            if (hit.collider != null)
            {


                if (hit.collider.TryGetComponent<IDamagable>(out var damagable))
                {
                    Debug.Log("Damage");
                    damagable.TakeDamage(Damage);
                }


            }*/
        }

    }

    void Reload()
    {
        if (_reloadTimeDelta > 0)
        {
            _reloadTimeDelta -= Time.deltaTime;
        }

        if (_reloadTimeDelta <= 0)
        {
            _mag = MagSize;
            _reloadTimeDelta = ReloadTime;

        }

    }
}
