using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float ShotsPerSecond = 10f;
    public GameObject ProjectileToSpawn;
    public Transform SpawnPosition;
    public AudioSource shootAudio;
    private float NextShotTime = 0.0f;
    
    public bool Shoot(float attackDmg)
    {
        float cooldown = 1 / ShotsPerSecond;

        if (Time.time > NextShotTime)
        {
            var newProjectile = Instantiate(ProjectileToSpawn, SpawnPosition.position, SpawnPosition.rotation);
            newProjectile.GetComponent<DamageDealer>().damage = attackDmg;
            if(shootAudio != null)
            {
                shootAudio.Play();
            }
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), newProjectile.GetComponent<Collider>());
            NextShotTime = Time.time + cooldown;
            return true;
        }
        return false;
    }

}
