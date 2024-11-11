using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;
    public float lifeTime = 2;


    private void Update() {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<EnemyScript>() != null) {
            other.GetComponent<EnemyScript>().OnHit(damage);
        }  
        Destroy(gameObject);
    } 
}
