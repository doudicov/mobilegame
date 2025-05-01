using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text healthBarText;
    Damageable playerDamageable;
    // Start is called before the first frame update

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();

        if (player == null)
        {
            Debug.Log("look at tag make sure its player");
        } 
    }
    void Start()
    {
        healthBar.value= CalculateBarPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "Health" + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerhealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerhealthChanged);
    }
    private float CalculateBarPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Update is called once per frame
    private void OnPlayerhealthChanged(int newHealth, int maxHealth)
    {
        healthBar.value = CalculateBarPercentage(newHealth, maxHealth);
        healthBarText.text = "Health" + newHealth + "/" + maxHealth;
    }
}
