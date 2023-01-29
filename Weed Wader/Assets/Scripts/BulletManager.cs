using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;
    Dictionary<string, List<Bullet>> activeBullets = new Dictionary<string, List<Bullet>>();

    Dictionary<string, List<Bullet>> inactiveBullets = new Dictionary<string, List<Bullet>>();

    [SerializeField] List<Bullet> bulletPrefabs = new List<Bullet>();

    // Start is called before the first frame update

    void Awake()
    {
        Instance =this;
    }

    void Start()
    {
        foreach(Bullet bPrefab in bulletPrefabs)
        {
            
            inactiveBullets[bPrefab.bulletName] = new List<Bullet>();
            activeBullets[bPrefab.bulletName] = new List<Bullet>();
            
            for(int i = 0; i < 20; i++)
            {
                Bullet b = Instantiate(bPrefab, this.transform);
                inactiveBullets[bPrefab.bulletName].Add(b);
                b.gameObject.SetActive(false);

            }
        }
        
    }


    public Bullet GetBullet(string bulletName)
    {
        Bullet b;
        if(inactiveBullets[bulletName].Count == 0)
        {
            
            b = Instantiate(activeBullets[bulletName][0], this.transform);
        }
        else
        {
            b = inactiveBullets[bulletName][0];
            inactiveBullets[bulletName].Remove(b);
        }
        b.gameObject.SetActive(true);
        activeBullets[bulletName].Add(b);
        b.isActive = true;
        return b;
    }

    public void DeactivateBullet(Bullet bullet, string bulletName)
    {
        activeBullets[bulletName].Remove(bullet);
        inactiveBullets[bulletName].Add(bullet);
        bullet.isActive = false;
        bullet.gameObject.SetActive(false);
    }
}
