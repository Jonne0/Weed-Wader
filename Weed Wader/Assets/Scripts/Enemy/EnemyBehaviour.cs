using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    //movement speed of enemy
    public float speed = 2;

    public Vector3 target;
    [SerializeField] internal GameObject BulletPrefab;
    [SerializeField] internal float BulletForce = 10f;
    [SerializeField] private float fireRate = 0.5f;
    private float timeSinceShot;
    [SerializeField] private float growthTime = 5;
    private float currentGrowth;
    private int bulletsToShoot;
    private float waitCounter;

    [SerializeField] Animator animator;
    public int score;

    public EnemyState currentState;
    //player is kept track of to know where to aim
    internal GameObject player;

    void Awake()
    {
        this.player = GameObject.FindWithTag("Player");
    }

    void Start()
    {

    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Moving:
                {
                    MoveTowards();
                    if (Vector3.Distance(transform.position, target) < 0.001f)
                        ChangeState(EnemyState.WaitForNext);
                    break;
                }
            case EnemyState.Shooting:
                {
                    if (timeSinceShot > fireRate)
                    {
                        timeSinceShot = 0;
                        Shoot();
                        bulletsToShoot -= 1;
                    }
                    timeSinceShot += Time.deltaTime;
                    if (bulletsToShoot <= 0)
                        ChangeState(EnemyState.WaitForNext);
                    break;
                }
            case EnemyState.Growing:
                {
                    //update growing sprite
                    //if growing is done, change to shooting
                    currentGrowth += Time.deltaTime;

                    if (currentGrowth >= growthTime)
                        ChangeState(EnemyState.WaitForNext);

                    break;
                }
            case EnemyState.Seed:
                {
                    MoveTowards();
                    if (Vector3.Distance(transform.position, target) < 0.001f)
                        ChangeState(EnemyState.Growing);
                    break;
                }

            case EnemyState.WaitForNext:
                {
                    waitCounter += Time.deltaTime;
                    if (waitCounter > 1)
                    {
                        //70/30 for shoot or move
                        float f = Random.Range(0.0f, 1.0f);
                        //CHANGE THIS
                        if (f > 0.6f)
                            ChangeState(EnemyState.Moving);
                        else
                            ChangeState(EnemyState.Shooting);
                    }
                    break;
                }
        }
    }
    IEnumerator ClearParticlesInSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        ParticleSystem ps = this.GetComponent<ParticleSystem>();
        ps.Clear();

    }

    public void ChangeState(EnemyState newState)
    {
        //first manage behaviour for exiting old state
        switch (currentState)
        {
            case EnemyState.WaitForNext:
            {
                waitCounter = 0;
                break;
            }
            case EnemyState.Moving:
            {
                /*
                SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
                sr.enabled = true;
                //stop playing effect
                */
                gameObject.GetComponent<Collider2D>().enabled = true;
                ParticleSystem ps = this.GetComponent<ParticleSystem>();
                ps.Stop();
                StartCoroutine(ClearParticlesInSeconds(1));
                break;
            }
            case EnemyState.Seed:
            {
                gameObject.GetComponent<Collider2D>().enabled = true;
                break;
            }
            default:
            {
                break;
            }
        }

        //then manage behaviour for entering new state
        currentState = newState;
        switch (newState)
        {
            case EnemyState.Moving:
                {
                    float rand_Y = Random.Range(-3.5f, 2.5f);
                    float rand_X = Random.Range(-7.0f, 7.0f);
                    /*
                    target = new Vector3(rand_X, rand_Y, this.transform.position.z);
                    SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
                    sr.enabled = false;
                    */
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    //start playing effect
                    ParticleSystem ps = this.GetComponent<ParticleSystem>();
                    ps.Play();
                    break;
                }
            case EnemyState.Shooting:
                {
                    bulletsToShoot = Random.Range(2, 5);
                    break;
                }
            case EnemyState.Seed:
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
                break;

            }
            default:
                {
                    break;
                }
        }

        animator.SetInteger("currentState", (int)currentState);
    }

    public virtual void Shoot()
    {

        Vector2 direction = new Vector2(player.transform.position.x, player.transform.position.y)
         - new Vector2(transform.position.x, transform.position.y);

        GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        direction = direction.normalized;
        rb.AddForce(direction * BulletForce, ForceMode2D.Impulse);
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
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