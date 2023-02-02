using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //movement speed of enemy
    public float speed = 2;
    
    public Vector3 target;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] private float fireRate = 0.5f;
    private float timeSinceShot;
    [SerializeField] private float growthTime = 5;
    private float currentGrowth;
    private int bulletsToShoot;
    private float waitCounter;

    [SerializeField] Animator animator;

    public EnemyState currentState;
    //player is kept track of to know where to aim
    GameObject player;

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("PlayerCharacter");
    }

    void Start()
    {

    }

    void Update()
    {
        switch(currentState)
        {
            case EnemyState.Moving:
            {
                MoveTowards();
                if(Vector3.Distance(transform.position, target) < 0.001f)
                    ChangeState(EnemyState.WaitForNext);
                break;
            }
            case EnemyState.Shooting:
            {
                if(timeSinceShot > fireRate)
                {
                    timeSinceShot = 0;
                    Shoot();
                    bulletsToShoot -= 1;
                }
                timeSinceShot += Time.deltaTime;
                if(bulletsToShoot <= 0)
                ChangeState(EnemyState.WaitForNext);
                break;
            }
            case EnemyState.Growing:
            {
                //update growing sprite
                //if growing is done, change to shooting
                currentGrowth += Time.deltaTime;

                if(currentGrowth >= growthTime)
                    ChangeState(EnemyState.WaitForNext);

                break;
            }
            case EnemyState.Seed:
            {
                MoveTowards();
                if(Vector3.Distance(transform.position, target) < 0.001f)
                    ChangeState(EnemyState.Growing);
                break;
            }

            case EnemyState.WaitForNext:
            {
                waitCounter += Time.deltaTime;
                if(waitCounter > 1)
                {
                    //70/30 for shoot or move
                    float f = Random.Range(0.0f,1.0f);
                    if(f > 0.7f)
                        ChangeState(EnemyState.Moving);
                    else
                        ChangeState(EnemyState.Shooting);
                }
                break;
            }
        }
    }

    public void ChangeState(EnemyState newState)
    {
        //first manage behaviour for exiting old state
        switch(currentState)
        {
            case EnemyState.WaitForNext:
            {
                waitCounter = 0;
                break;
            }
            default:
            {
                break;
            }
        }

        //then manage behaviour for entering new state
        currentState = newState;
        switch(newState)
        {
            case EnemyState.Moving:
            {
                float rand_Y = Random.Range(-5,5);
                float rand_X = Random.Range(-7,7);
                target = new Vector3(rand_X, rand_Y, this.transform.position.z);
                break;
            }
            case EnemyState.Shooting:
            {
                bulletsToShoot = Random.Range(2, 5);
                break;
            }
            default:
            {
                break;
            }
        }

        animator.SetInteger("CurrentState", (int) currentState);
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

    void MoveTowards()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }


}

public enum EnemyState
{
    Shooting,
    Moving,
    Growing,
    Seed,
    WaitForNext
}