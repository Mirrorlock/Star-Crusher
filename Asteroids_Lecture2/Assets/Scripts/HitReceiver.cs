using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReceiver : MonoBehaviour {
	public GameObject ObjectToSpawnOnDeath;
	public GameObject DestructionFX;
	public float SpawnDistance = 2;
	public float DeflectionAngle = 45;
	public float DestructionFXDuration = 0.5f;
	public uint ScoreOnDeath = 0;
	public bool DebugDraw = false;

    public void ReceiveHit(GameObject damageDealer, float damage, bool spawnNew=false)
	{
		if (ObjectToSpawnOnDeath != null && spawnNew)
		{
			Vector3 hitDirection = transform.position - damageDealer.transform.position;
			hitDirection.Normalize();
			if (DebugDraw) {
				Debug.DrawLine (damageDealer.transform.position, transform.position, Color.red, 20.0f);
			}

			SpawnDeathObject (hitDirection, -DeflectionAngle);
			SpawnDeathObject (hitDirection, DeflectionAngle);
		}


        var hp = GetComponent<HP>();
        bool isAlive = false;
        if(hp)
        {
            hp.receiveDamage(damage);
            isAlive = hp.isAlive();
        }

        if (!isAlive)
        {
		    if (DestructionFX != null) {
			    GameObject spawnedFX = Instantiate (DestructionFX, transform.position, Random.rotation);
			    Destroy (spawnedFX, DestructionFXDuration);
		    }
            GameStateController.Instance.spawnObjects.Remove(gameObject);
		    GameStateController.Instance.IncrementScore (ScoreOnDeath);
		    Destroy (gameObject);
        }
	}

	private void SpawnDeathObject(Vector3 hitDirection, float angle)
	{
		Vector3 spawnDirection = Quaternion.AngleAxis (angle, Vector3.up) * hitDirection;
		Vector3 spawnPosition = transform.position + spawnDirection * SpawnDistance;
		if (DebugDraw) {
			Debug.DrawLine (transform.position, spawnPosition, Color.green, 2.0f);
		}


        GameObject spawnedObject = Instantiate(ObjectToSpawnOnDeath, spawnPosition, Random.rotation);
		var asteroidMovementController = spawnedObject.GetComponent<AsteroidMovementController> ();
		if (asteroidMovementController) {
			asteroidMovementController.InitialDirection = spawnDirection;
		}
        GameStateController.Instance.spawnObjects.Add(spawnedObject);
	}
}
