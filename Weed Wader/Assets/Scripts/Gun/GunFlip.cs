using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlip : MonoBehaviour
{
    Vector3 scale;

    private void Start()
    {
        scale = transform.localScale;    
    }

    private void FixedUpdate()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (rotationZ > 90 || rotationZ < -90)
        {
            transform.localScale = new Vector3(transform.localScale.x, -scale.y, transform.localScale.z);

            
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, scale.y, transform.localScale.z);
        }



        

    }
}
