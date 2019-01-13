using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
	public string[] TagToHit = { "Asteroid" , "Enemy"};
    public float damage = 10;
	void OnCollisionEnter(Collision collision)
	{

        foreach (string tag in TagToHit)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                HitObject(gameObject, collision.gameObject, true);
                HitObject(collision.gameObject, gameObject);
            }
        }
	}


    public void HitObject(GameObject hitObject, GameObject hittingObject, bool spawnNew = false)
    {
        HitReceiver hitReceiver = hitObject.GetComponent<HitReceiver>();
        if (hitReceiver)
        {
            hitReceiver.ReceiveHit(hittingObject, damage, spawnNew);
        }
        else
        {
            Destroy(hitObject);
        }
    }

}

