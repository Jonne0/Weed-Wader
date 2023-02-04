using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] List<GameObject> health;

    [SerializeField] Sprite fullSprite;
    [SerializeField] Sprite emptySprite;

    void Update()
    {
        for (int i = 0; i < health.Count; i++)
        {
            if (player.Health - 1 >= i)
            {
                health[i].GetComponent<Image>().sprite = fullSprite;
            }
            else
            {
                health[i].GetComponent<Image>().sprite = emptySprite;
            }
        }
    }
}
