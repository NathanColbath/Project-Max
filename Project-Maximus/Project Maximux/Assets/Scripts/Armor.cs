using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public int armorValue;
    public int maxArmorValue;
    public ArmorType type;

    public void takeDamage(int damage)
    {
        int actualDamage = 0;

        switch (type)
        {
            case ArmorType.light:
                actualDamage = (damage - 1);
            break;

            case ArmorType.medium:
                actualDamage = (damage - 2);
                break;

            case ArmorType.heavy:
                actualDamage = (damage - 3);
                break;

            case ArmorType.superheavy:
                actualDamage = (damage - 4);
                break;
        }

        if(actualDamage <= 0)
        {
            actualDamage = 0;
        }

        armorValue -= actualDamage;
    }

    public void repairArmor(int value)
    {
        armorValue += value;
        if(armorValue >= maxArmorValue)
        {
            armorValue = maxArmorValue;
        }
    }

    public void improveArmor(ArmorType newType, int newMaxArmorValue)
    {
        type = newType;
        maxArmorValue = newMaxArmorValue;
    }

    public bool isDestroyed()
    {
        if(armorValue <= 0)
        {
            return true;
        }

        return false;
    }
}
