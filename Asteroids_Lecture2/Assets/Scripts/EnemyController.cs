using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float timeBeforeFirstShot = 10;
    public float attackDamage = 10;
    Vector3 positionToFollow;
    private GameObject player;

	void Start () {
        timeBeforeFirstShot += Time.time;
        player = GameObject.FindWithTag("Player");
        positionToFollow = player.transform.position;
	}

    void Update()
    {
        if (player != null && Time.time >= timeBeforeFirstShot)
        {

            if (GetComponent<Weapon>().Shoot(attackDamage))
            {
                positionToFollow = player.transform.position;
                Debug.DrawLine(transform.position, positionToFollow, Color.green, 5f);
            }
        }

        GetComponent<EnemyMover>().Move(positionToFollow);
        
    }
}
