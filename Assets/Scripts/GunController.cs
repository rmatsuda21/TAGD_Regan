using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject gun;

    public bool isParent = true;

    private float lastShot = 0;
    public float shotCooldown = .1f;

    private bool canShoot = true;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && isParent)
        {
            canShoot = Time.time - shotCooldown > lastShot;

            if (Input.GetMouseButton(0) && canShoot)
            {
                shoot();
            }
        }
        else
        {
            if (GetComponent<ParticleSystem>().isStopped)
            {
                Destroy(this);
            }
        }
    }

    public void shoot()
    {
        lastShot = Time.time;

        GameObject g = Instantiate(gun, transform.position, transform.rotation);
        g.AddComponent<GunController>();
        g.GetComponent<GunController>().isParent = false;
        g.GetComponent<Rigidbody>().velocity = transform.forward * 10f;

    }

    public void OnCollisionEnter(Collision collision)
    {
        print("Hello");
        if(collision.gameObject.tag == "Floor")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<ParticleSystem>().Play();
            isDead = true;
        }
    }
}
