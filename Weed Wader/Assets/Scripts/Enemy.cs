using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    public EnemyBehaviour behaviour;

    void Start()
    {
        this.behaviour = this.GetComponent<EnemyBehaviour>();
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
        
        GameManager.Instance.SpawnFromKill(this.transform.position);
        Object.Destroy(this.gameObject, 0);

    }  
}
