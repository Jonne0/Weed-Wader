using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bulletContainer;
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private float timeToFire = .5f;

    private float timeSinceShot = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 gunPosition = gun.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - gunPosition;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        RotateGun(angle);

        if (Input.GetMouseButton(0) && timeSinceShot > timeToFire)
        {
            timeSinceShot = 0;
            Shoot(direction);
        }

        timeSinceShot += Time.deltaTime;
    }

    void RotateGun(float angle)
    {

        gun.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Shoot(Vector2 direction)
    {
        Bullet bullet = BulletManager.Instance.GetBullet(bulletPrefab.name);
        bullet.transform.position = gun.transform.position;
        bullet.transform.rotation = gun.transform.rotation;
        bullet.direction = direction;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

    }

}
