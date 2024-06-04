using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentTpye;

    [Header("Unique effect")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;
    [TextArea]
    public string itemEffectDscription;

    [Header("Sword Skill Type(only weapon)")]
    public SwordType swordType;

    [Header("Major stats")]
    public int strength; // 1 point increase damage by 1 and crit.power by 1%
    public int agility;  // 1 point increase evasion by 1% and crit.chance by 1%
    public int intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public int vitality; // 1 point incredase health by 3 or 5 points

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critPower;              // default value 150%

    [Header("Defensive stats")]
    public int maxHealth;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;

    [Header("Craft requirements")]
    public List<InventoryItem> craftingMaterials;

    private int discriptionLength;
    public void Effect(Transform _enemyPosition)
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect(_enemyPosition);
        }
    }
    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);

        playerStats.onHealthChanged?.Invoke();

    }
    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);

        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);

        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);


        playerStats.IncreaseHealthBy(0);//reset health after removed the equipment 
        playerStats.onHealthChanged?.Invoke();

    }

    public override string GetDiscription()
    {
        sb.Length = 0;
        discriptionLength = 0;
        // Major stats
        AddItemDiscription(strength, "Strength");
        AddItemDiscription(agility, "Agility");
        AddItemDiscription(intelligence, "Intelligence");
        AddItemDiscription(vitality, "Vitality");

        // Offensive stats
        AddItemDiscription(damage, "Damage");
        AddItemDiscription(critChance, "Critical Chance");
        AddItemDiscription(critPower, "Critical Power");

        // Defensive stats
        AddItemDiscription(maxHealth, "Max Health");
        AddItemDiscription(armor, "Armor");
        AddItemDiscription(evasion, "Evasion");
        AddItemDiscription(magicResistance, "Magic Resistance");

        // Magic stats
        AddItemDiscription(fireDamage, "Fire Damage");
        AddItemDiscription(iceDamage, "Ice Damage");
        AddItemDiscription(lightingDamage, "Lightning Damage");

        if (discriptionLength < 5)
        {
            for (int i = 0; i < 5 - discriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");

            }
        }

        if (itemEffectDscription.Length > 1)
        {
            sb.AppendLine();
            sb.Append(itemEffectDscription);

        }

        return sb.ToString();
    }
    private void AddItemDiscription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (_value > 0)
                sb.Append("+ " + _value + " " + _name);

            discriptionLength++;
        }
    }

}
