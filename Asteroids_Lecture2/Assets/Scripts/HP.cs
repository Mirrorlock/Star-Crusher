using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {
    public float maxHealth = 100;
    public float currentHealth = 100;
    public Slider healthSlider;
    public Image damageFlash;
    public float flashSpeed = 5f;

    private bool playerHit = false;
    private void Awake()
    {
        currentHealth = maxHealth;
        if(healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public bool isAlive() {
        return currentHealth > 0;
	}

    private void Update()
    {
        if(damageFlash != null)
        {
            if (playerHit)
            {
                damageFlash.color = new Color(1f, 0f, 0f, 0.3f);
            }
            else
            {
                damageFlash.color = Color.Lerp(damageFlash.color, Color.clear, flashSpeed * Time.deltaTime);
            }
        }

        playerHit = false;
    }

    public void receiveDamage(float damage)
    {
        currentHealth -= damage;
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        playerHit = true;
    }

}
