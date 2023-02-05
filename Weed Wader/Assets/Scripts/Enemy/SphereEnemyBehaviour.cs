using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] int bulletsInSpread;
    [SerializeField] int angleSpread;

    public override void Shoot()
    { 
        //-1 since the for loop technically counts the intervals instead of the bullets
        for (float i = 0; i <= (bulletsInSpread - 1); i++)
        {
            Vector2 direction = new Vector2(player.transform.position.x, player.transform.position.y)
         - new Vector2(transform.position.x, transform.position.y);

            //0 => bulletsInspred relates to leftAngle => rightAngle
            //so first find i/bulletsInSpread

            float angleExtra = i/(float)bulletsInSpread * (float)angleSpread;
            //Vector2 angleVector = DirFromAngle(angleExtra);
            GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            direction = Quaternion.Euler(0f,0f, angleExtra) * direction;

            direction = direction.normalized;
            
            rb.AddForce(direction * BulletForce, ForceMode2D.Impulse);
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }

    public Vector2 DirFromAngle(float angleInDegrees) {
		
		return new Vector2(-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), -Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
	}
}
