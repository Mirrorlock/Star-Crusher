using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed = 0.2f;
    public string GameOverScene = "GameOver";
    public float attackDamage = 20;
 

    void Start()
    {
        GameStateController.Instance.OnPlayerSpawned();
    }

    void OnDestroy()
    {
        GameStateController.Instance.OnPlayerDied();
    }

    void FixedUpdate()
    {
        MoveShipWithPhysics();
    }

    void Update()
    {
        UpdateShootInputs();
    }

    private void UpdateShootInputs()
    {
        if (Input.GetButton("Fire1"))
        {
            GetComponent<Weapon>().Shoot(attackDamage);
        }
    }

    private void MoveShipWithPhysics()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float playerCameraOffset = Camera.main.transform.position.y - transform.position.y;
        Vector3 mousePositionScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerCameraOffset);
        Vector3 mousePositionWorldSpace = Camera.main.ScreenToWorldPoint(mousePositionScreenSpace);

        Quaternion newRotation = Quaternion.LookRotation(mousePositionWorldSpace - transform.position);
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction = transform.rotation * direction;
        direction = direction * PlayerSpeed * Time.deltaTime;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.MovePosition(transform.position + direction);
        rb.MoveRotation(newRotation);
    }
}