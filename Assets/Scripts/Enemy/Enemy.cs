using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Token
{
    public Sprite enemySprite;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    public void OnMouseDown()
    {
        if (canBeTargeted)
        {
            AbilityLoader.Instance.UseAbility(this);
            return;
        }
    }

}
