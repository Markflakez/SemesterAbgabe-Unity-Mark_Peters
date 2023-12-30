using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event Action<float> OnHealthChanged;
    public float currentHealth;
    public float maxHealth = 100;

    #region Start Kommentare
    //Setzt den aktuellen Health Wert auf den maximalen Health Wert.
    #endregion
    private void Start()
    {
        currentHealth = maxHealth;
    }

    #region TakeDamage Kommentare
    //Reduziert den Health Wert um den gegebenen Schadenwert.
    //Stellt sicher, dass der Health Wert nicht unter 0 fällt.
    //Benachrichtigt alle Observer über die Veränderung des Health Wertes.
    #endregion
    public void DecreaseHealth(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        NotifyHealthChanged();
    }

    #region NotifyHealthChanged Kommentare
    //Benachrichtigt alle Observer über die Veränderung des Health Werts.
    #endregion
    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth);
    }
}
