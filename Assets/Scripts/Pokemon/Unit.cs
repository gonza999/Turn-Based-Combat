using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int unitLevel;

    public float damage;

    public float maxHp;

    public float currentHp;

    public List<Atack> atacks;

    public bool TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp<=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
