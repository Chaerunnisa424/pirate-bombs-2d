using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctionTask : MonoBehaviour
{
    // Public variables to be adjusted from Unity Inspector
    public int playerHealth = 100;  // Set initial value to 100
    public int playerArmor = 25;    // Set initial value to 25
    public int playerDamage = 50;   // Set initial value to 50

    // Start is called before the first frame update
    void Start()
    {
        // Calling the functions and displaying results using Debug.Log
        Debug.Log("Player Status: " + GetPlayerStatus());
        Debug.Log("Effective Damage: " + CalculateEffectiveDamage());
        Debug.Log("Is Player Alive: " + IsPlayerAlive());
    }

    // Update is called once per frame
    void Update()
    {
        // You can dynamically change or update the player status here if needed.
        Debug.Log("Updated Player Status: " + GetPlayerStatus());
        Debug.Log("Updated Effective Damage: " + CalculateEffectiveDamage());
        Debug.Log("Is Player Alive (Updated): " + IsPlayerAlive());
    }

    // Function to get player status based on health
    public string GetPlayerStatus()
    {
        if (playerHealth > 50)
        {
            return "Healthy";
        }
        else if (playerHealth > 20)
        {
            return "Injured";
        }
        else if (playerHealth > 0)
        {
            return "Critical";
        }
        else
        {
            return "Dead";
        }
    }

    // Function to calculate effective damage considering player armor
    public int CalculateEffectiveDamage()
    {
        int effectiveDamage = playerDamage - playerArmor;
        return effectiveDamage > 0 ? effectiveDamage : 0; // Ensure damage is not negative
    }

    // Function to check if the player is alive based on health
    public bool IsPlayerAlive()
    {
        return playerHealth > 0;
    }
}
