using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bullet;
    public Rigidbody2D bulletRb;
    public float speed = 15f;
    //public float range

    // Start is called before the first frame update
    void Start()
    {
        bullet = this.gameObject;
        bulletRb = gameObject.GetComponent<Rigidbody2D>();

        if(PlayerMovement.instance.isWatchingRight == true)
        {
            bulletRb.velocity = new Vector2(speed, 0);
        }
        else
        {
            bulletRb.velocity = new Vector2(-speed, 0);
        }      
    }
}
