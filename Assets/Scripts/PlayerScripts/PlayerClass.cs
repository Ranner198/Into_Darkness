using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass {

    private int health, ammo;
    private float speed, difficultyModifier;
    private float oxygenLevels;
    private bool dead;

    public PlayerClass(float speed, int health, int ammo) {
        SetSpeed(speed);
        SetHealth(health);
        SetAmmo(ammo);
    }

    //Get tha speed
    public float GetSpeed() {
        return this.speed;
    }

    //Set tha Speed
    public void SetSpeed(float speed) { 
        this.speed = speed;
    }

    //Get tha Health
    public int GetHealth() {
        return this.health;
    }

    //Set tha Health
    public void SetHealth(int health) {
        this.health = health;
    }

    //Take tha Damage
    public void TakeDamage(int damage) {
        this.health -= damage;
    }

    //✔ tha Health 
    public bool isDead() {
        if (health <= 0 || oxygenLevels <= 0)
        {
            dead = true;
        } else {
            dead = false;
        }
        return dead;
    }

    //Set tha Oxygen Level
    public void SetOxygenLevel(float num) {
        this.oxygenLevels = num;
    }

    //Get tha Oxygen Level
    public float GetOxygenLevel() {
        return this.oxygenLevels;
    }

    //Set tha Ammo
    public void SetAmmo(int num)
    {
        this.ammo = num;
    }

    //Add tha Ammo
    public void AddAmmo()
    {
        this.ammo++;
    }

    //Add tha Ammo Overloaded
    public void AddAmmo(int num) {
        this.ammo += num;
    }

    public int GetAmmo() {
        return this.ammo;
    }
}