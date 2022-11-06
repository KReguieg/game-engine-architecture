using System.Collections.Generic;

using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterClass
    {
        None,
        Ranger,
        Mage,
        Savage,
        Paladin
    }
    protected CharacterClass characterClass = CharacterClass.None;

    protected int strength = 0;
    protected int agility = 0;
    protected int intelligence = 0;

    protected int health = 0;
    protected int mana = 0;

    protected float baseDamage = 0f;
    protected float baseMagicDamage = 0f;

    protected List<Ability> abilities = new List<Ability>();
}
