using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // Current health of the player
    float health;
    // Timer for smooth health bar transitions
    float lerpTimer;
    // Maximum health value
    public float maxHealth = 100f;
    // Speed of the health bar transition
    public float chipSpeed = 2f;
    // UI element for the front health bar
    public Image frontHealthBar;
    // UI element for the back health bar
    public Image backHealthBar;
    // UI element for the health text
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize health to maxHealth
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Clamp health between 0 and maxHealth
        health = Mathf.Clamp(health, 0, maxHealth);
        // Update the health UI elements
        UpdateHealthUI();

        // Simulate taking damage when Z key is pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(Random.Range(5, 10));
        }

        // Simulate restoring health when X key is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }

    // Update the health UI elements
    public void UpdateHealthUI()
    {
        Debug.Log(health);

        // Current fill amount of front and back health bars
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        // Fraction of current health relative to max health
        float hFraction = health / maxHealth;

        // Smooth transition when health is decreasing
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = Mathf.Pow(lerpTimer / chipSpeed, 2);
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        // Smooth transition when health is increasing
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = Mathf.Pow(lerpTimer / chipSpeed, 2);
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }

        // Update the health text display
        healthText.text = Mathf.Round(health) + "/" + Mathf.Round(maxHealth);
    }

    // Reduce health by a specified damage amount
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    // Restore health by a specified amount
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0;
    }

    // Increase max health based on player level
    public void IncreaseHealth(int level)
    {
        maxHealth += (health * 0.01f) * ((100 - level) * 0.1f);
        health = maxHealth;
    }
}
