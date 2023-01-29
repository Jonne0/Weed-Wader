using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] List<GameObject> health;

    void Update()
    {
        for(int i = 0; i < health.Count; i++)
        {
            if(player.health - 1 >= i)
            {
                health[i].GetComponent<Image>().color = Color.red;
            }
            else
            {
                health[i].GetComponent<Image>().color = Color.grey;
            }
        }
    }
}
