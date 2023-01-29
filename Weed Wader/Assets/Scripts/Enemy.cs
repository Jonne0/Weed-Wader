using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 3;
    private float timeSinceShot;
    [SerializeField] private float fireRate = 0.5f;
    private float timeSinceMoved;
    private float moveTime = 4;

    [SerializeField] Bullet bulletPrefab;
    GameObject bulletContainer;
    GameObject player;

    Vector3 target;

    bool isMoving;

    public float speed;

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("PlayerCharacter");
        this.bulletContainer = GameObject.Find("BulletContainer");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bulletPlayer"))
        {
            health -= 1;
            if(health <= 0)
                this.Kill();
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            bullet.OnHit();
        }
    }

    void Kill()
    {
        //die animation
        GameManager.Instance.score += 1;
        Debug.Log(GameManager.Instance.score);
        GameManager.Instance.enemies.Remove(this);
        Object.Destroy(this.gameObject, 0);
    }

    void Update()
    {
        if(isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if(Vector3.Distance(transform.position, target) < 0.001f)
            {
                isMoving = false;
                timeSinceMoved = 0;
            }
        }
        else
        {
            float f = Random.Range(0.0f,1.0f);
            if(f < 0.01f && timeSinceMoved > moveTime)
            {
                Move();
            }
            else
            {
                if(timeSinceShot > fireRate)
                {
                    timeSinceShot = 0;
                    Shoot();
                }
                timeSinceShot += Time.deltaTime;    
            }
            timeSinceMoved += Time.deltaTime;
        }
    }

    void Shoot()
    {

        Vector2 direction = new Vector2(player.transform.position.x, player.transform.position.y)
         - new Vector2(this.transform.position.x, this.transform.position.y);
        Bullet bullet = BulletManager.Instance.GetBullet(bulletPrefab.name);
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;
        bullet.direction = direction;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        bullet.transform.rotation = Quaternion.Euler(0,0,angle);
    }

    void Move()
    {
        int rand_Y = Random.Range(-5,5);
        int rand_X = Random.Range(-7,7);

        //do some funky animation.
        target = new Vector3(rand_X, rand_Y, this.transform.position.z);
        isMoving = true;
    }
}
