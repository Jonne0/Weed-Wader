using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    public int scoreValue;

    public EnemyBehaviour behaviour;

    void Start()
    {
        this.behaviour = this.GetComponent<EnemyBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // only can be hit by player bullets, can't be hit while it's a seed
        if(collision.gameObject.CompareTag("bulletPlayer") && behaviour.currentState != EnemyState.Seed)
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
        GameManager.Instance.score += this.scoreValue;
        Debug.Log(GameManager.Instance.score);
        GameManager.Instance.enemies.Remove(this);
        
        //have a 10 appear above enemies head
        //only spawn new enemies from fully grown enemies
        if(this.behaviour.currentState != EnemyState.Growing)
            GameManager.Instance.SpawnFromKill(this.transform.position);
        Object.Destroy(this.gameObject, 0);
        

    }  
}
