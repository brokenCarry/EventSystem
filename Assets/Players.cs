﻿using UnityEngine;
using System.Collections;

public class Players : Unit, ISub
{
    int MaxHealth; //Max health any player object can have at any time
    [SerializeField]
    int CurrentHealth;  //current health a playte robject has
    [SerializeField]
    int AttackPower; //how much damage a player object can do before arrmor calculations
    [SerializeField]
    int Potions; //number of potions a player object has
    [SerializeField]
    int Arrmor; //Arrmor rating of a player object how much dmg he can absorb

    bool blocking;

    [SerializeField]
    Enemy Target;


    void Awake()
    {
        Subscribe("GUI", "Attack", Attack);
        Subscribe("GUI", "Defend", Defend);
        Subscribe("GUI", "Heal", Heal);
        Subscribe("GUI", "Wait", Wait);
    }
    void Start()
    {
        MaxHealth = 100; //sets players max HP to 100
        CurrentHealth = MaxHealth; //sets the starting current hp to the max
        AttackPower = Random.Range(10,20); //generates a random number between 10 and 20 to be his attack power
        Potions = Random.Range(1, 5); //generate random number between 1 adn 5 to see how many potions the player has a start
        Arrmor = Random.Range(1, 10); //generate random number between 1,10 for how much dmg the player can block
        Target = FindObjectOfType<Enemy>();
    }

    public void Subscribe(string type, string msg, CallBacks func)
    {
        EventSystem.AddSub(type, msg, func, this);
    }

    void TakeDamage(int dmg)
    {
            CurrentHealth -= dmg;
        if(blocking)
        {
            CurrentHealth -= (dmg - Arrmor);
            blocking = false;
        }
    }

    void Attack()
    {
        Target.TakeDamage(AttackPower);
    }

    void Defend()
    {
        blocking = true;
    }

    void Heal()
    {
        Debug.Log("Heal");
    }

    void Wait()
    {
        Debug.Log("Wait");
    }
}
