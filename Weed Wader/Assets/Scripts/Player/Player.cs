using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public bool Invincible;
    public float InvincibleTime;
    private float _invincibleTimeDelta;

    public float Health { get; set; } = 3f;


    void Update()
    {
        if (Invincible)
        {
            if (_invincibleTimeDelta <= 0.1)
            {
                _invincibleTimeDelta = InvincibleTime;
            }
            else
            {
                _invincibleTimeDelta -= Time.deltaTime;
            }
        }
        else
        {
            _invincibleTimeDelta = 0;
        }

        if (_invincibleTimeDelta < 0.1)
        {
            Invincible = false;
        }


        if (Health <= 0)
        {
            Die();
            GameManager.Instance.EndGame();
        }
    }

    public void TakeDamage(float amount)
    {
        if (!Invincible)
        {
            Health -= amount;
            Invincible = true;
            GetComponent<Animator>().SetTrigger("Damage");
        }
    }

    public void AddHealth(float amount)
    {
        Health += amount;
    }

    public void SetHealth(float amount)
    {
        Health = amount;
    }

    public void Die()
    {
        GetComponent<Animator>().SetTrigger("Death");
        GetComponent<PlayerMovement>().CanMove = false;
    }
}
