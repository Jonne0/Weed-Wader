using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    [SerializeField] float invincibilityDuration = 1;
    [SerializeField] Sprite invincibleSprite;
    [SerializeField] Sprite baseSprite;
    float invincibilityCounter;
    bool isInvincible;
    bool invincibleHit;
    // Start is called before the first frame update
    void Start()
    {
        invincibilityCounter = invincibilityDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleHit)
        {
            invincibilityCounter -= Time.deltaTime;
            if (invincibilityCounter <= 0)
            {
                isInvincible = false;
                //revert sprite to normal
                this.GetComponent<SpriteRenderer>().sprite = baseSprite;
                invincibilityCounter = invincibilityDuration;
            }
        }   

        //if shift is pressed, start dodgeroll, after animation, revert back to normal
    }

    void DodgeRoll()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bulletEnemy") && !isInvincible)
        {
            this.health -= 1;
            //update UI
            //cool effect
            if(health <= 0)
            {
                Debug.Log("skill issue");
                health = 3;
            }
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            bullet.OnHit();
            //start counting down and turn on invincibility
            invincibleHit = true;
            isInvincible = true;

            //change sprite to invincibilitywindow sprite
            this.GetComponent<SpriteRenderer>().sprite = invincibleSprite;


        }

        
    }
}
