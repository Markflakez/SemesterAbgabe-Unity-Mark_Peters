using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TakeDamage : MonoBehaviour
{
    public TextMeshProUGUI damageAmountText;
    public HealthSystem healthSystem;
    private int damageAmount = 0;

    #region IncreaseDecreaseDamageAmount Kommentare
    //Erhöht oder verringert den Schadenbetrag um 5 und begrenzt ihn auf den Bereich [0, 100].
    //Aktualisiert die Textanzeige auf den neuen Schadenbetrag.
    #endregion
    public void IncreaseDecreaseDamageAmount(int amount)
    {

        damageAmount += amount;
        if(damageAmount > 100)
        {
            damageAmount = 100;
        }
        else if(damageAmount < 0)
        {
            damageAmount = 0;
        }
        damageAmountText.text = "-" + damageAmount.ToString();
    }

    #region PerformDamage Kommentare
    //Führt den aktuellen SchadenBetrag auf das HealthSystem aus.
    #endregion
    public void PerformDamage()
    {
        healthSystem.TakeDamage(damageAmount);
    }
}
