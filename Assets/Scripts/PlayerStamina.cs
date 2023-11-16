using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    /// <summary>
    /// Maximum number of Stamina points for each player.
    /// </summary>
    public int staminaPoints;
    public int StaminaPoints { get { return staminaPoints; } }

    public int maxWager;
    public int minWager = 1;

    private int currentStamina;
    public int CurrentStamina{ get { return currentStamina; } }

    private int currentWager;
    public int CurrentWager { get { return currentWager; } }
    private int previousWager;
    public int PreviousWager { set { previousWager = value; } }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = staminaPoints / 2;
        currentWager = minWager;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStamina < 0)
            currentStamina = 0;
        if (currentStamina > staminaPoints)
            currentStamina = staminaPoints;

        if (currentWager < minWager)
            currentWager = minWager;
        if (currentWager > maxWager)
            currentWager = maxWager;
    }

    public void LoseStamina(int staminaLost)
    {
        currentStamina -= staminaLost;
        if (currentStamina < 0)
            currentStamina = 0;
        //Testing
        print("Stamina Lost. Current Stamina: " + currentStamina);
    }

    public void GainStamina(int staminaGained)
    {
        currentStamina += staminaGained;
        //Testing
        if (currentStamina > staminaPoints)
            currentStamina = staminaPoints;
        print("Stamina Gained. Current Stamina: " + currentStamina);

    }
    public void resetStamina()
    {
        currentStamina = staminaPoints / 2;
        //Testing
        print("Stamina Reset. Current Stamina: " + currentStamina);
    }

    public void increaseWager()
    {
        if (previousWager == 2 && currentWager == 1)
        {
            currentWager += 2;
        }
        if (previousWager == 3 && currentWager == 2)
        {
            return;
        }
        if (currentWager < maxWager && currentWager < currentStamina)
        {
            currentWager += 1;
            //Testing
            print("Increased Wager. Current Wager: " + currentWager);
        }
    }
    public void decreaseWager()
    {
        if (previousWager == 2 && currentWager == 3)
        {
            currentWager -= 2;
        }
        if (previousWager == 1 && currentWager == 2)
        {
            return;
        }

        if (currentWager > minWager)
        {
            currentWager -= 1;

            //Testing
            print("Decreased Wager. Current Wager: " + currentWager);
        }
    }

    public void resetWager()
    {
        if (previousWager == 1)
        {
            currentWager = 2;
            return;
        }
        currentWager = minWager;

        //Testing
        print("Wager Reset. Current Wager: " + currentWager);
    }
}