using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed = .5f;
    public Vector2 direction;

    public bool isActive;
    public string bulletName;

    void Update()
    {
        if (isActive)
        {
            this.transform.position += new Vector3(direction.normalized.x, direction.normalized.y, 0) * 0.01f * speed;
            if (this.transform.position.y < -6 || this.transform.position.y > 6)
                BulletManager.Instance.DeactivateBullet(this, this.bulletName);
            if (this.transform.position.x < -9 || this.transform.position.x > 9)
                BulletManager.Instance.DeactivateBullet(this, this.bulletName);
        }
    }

    public void OnHit()
    {
        //explode animation
        //set 0 to animation time
        BulletManager.Instance.DeactivateBullet(this, this.bulletName);
    }
}
