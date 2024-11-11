using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [Header("Bullet Variables")]
    public float bulletSpeed;
    public float bulletDamage = 5;
    public bool isAuto;

    [Header("Initial Setup")]
    public Transform bulletSpawnTrasnform;
    public GameObject bulletPrefeb;


    private void Update()
    {
        if (isAuto) {
            if(Input.GetButtonDown("Fire1")){ // holding the button
                Shoot();
            }
        }
        else
        {
            if(Input.GetButtonDown("Fire1")){ // clicking the button
                Shoot();
            }
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefeb, bulletSpawnTrasnform.position, Quaternion.identity, GameObject.FindWithTag("WorldObjectHolder").transform);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnTrasnform.forward * bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<BulletController>().damage = bulletDamage;
    }
}
