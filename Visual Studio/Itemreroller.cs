using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RandomItemStats
{
    static class Itemreroller
    {

        //TODO SPLIT BY ARMOUR / WEAPON, Each has stats the other cant use

        //public static ItemMod ReRollItem(Item item)
        //{
        //    //should be 4
       
        public static ItemMod ReRollArmour(Item item)
        {
            Debug.Log("Rerolling armor");
            int statTypes = (int)ArmourModType.COUNT - 1;
            int rollStatType = UnityEngine.Random.Range(0, statTypes);


            ArmourModType statTypeEnum = (ArmourModType)rollStatType;

            int rollQuality = RollForQuality();


            Debug.Log("Armor Stat Chosen is " + statTypeEnum.ToString() + " value is " + rollQuality);
            EquipmentStats itemEquipStats = item.GetComponent<EquipmentStats>();

            switch (statTypeEnum)
            {
                case ArmourModType.HEALTH_BONUS:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_maxHealthBonus", rollQuality);
                    ItemMod hpMod = new ItemMod(item);
                    hpMod.itemType = typeof(Armor);
                    hpMod.AddMod("mod_var", "m_maxHealthBonus");
                    hpMod.AddMod("mod_value", rollQuality.ToString());
                    hpMod.AddMod("mod_value_type", "HEALTH_BONUS");
                    return hpMod;

                case ArmourModType.POUCH_BONUS:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_pouchCapacityBonus", rollQuality);
                    ItemMod pouchMod = new ItemMod(item);
                    pouchMod.itemType = typeof(Armor);
                    pouchMod.AddMod("mod_var", "m_pouchCapacityBonus");
                    pouchMod.AddMod("mod_value", rollQuality.ToString());
                    pouchMod.AddMod("mod_value_type", "POUCH_BONUS");
                    return pouchMod;

                case ArmourModType.HEAT_PROTECTION:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_heatProtection", rollQuality);
                    ItemMod heatProtMod = new ItemMod(item);
                    heatProtMod.itemType = typeof(Armor);
                    heatProtMod.AddMod("mod_var", "m_heatProtection");
                    heatProtMod.AddMod("mod_value", rollQuality.ToString());
                    heatProtMod.AddMod("mod_value_type", "HEAT_PROTECTION");
                    return heatProtMod;

                case ArmourModType.COLD_PROTECTION:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_coldProtection", rollQuality);
                    ItemMod coldProtMod = new ItemMod(item);
                    coldProtMod.itemType = typeof(Armor);
                    coldProtMod.AddMod("mod_var", "m_coldProtection");
                    coldProtMod.AddMod("mod_value", rollQuality.ToString());
                    coldProtMod.AddMod("mod_value_type", "COLD_PROTECTION");
                    return coldProtMod;

                case ArmourModType.IMPACT_PROTECTION:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_impactResistance", rollQuality);
                    ItemMod impactProtMod = new ItemMod(item);
                    impactProtMod.itemType = typeof(Armor);
                    impactProtMod.AddMod("mod_var", "m_impactResistance");
                    impactProtMod.AddMod("mod_value", rollQuality.ToString());
                    impactProtMod.AddMod("mod_value_type", "IMPACT_PROTECTION");
                    return impactProtMod;

                case ArmourModType.CORRUPTION_PROTECTION:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_corruptionProtection", rollQuality);
                    ItemMod corruptionProtMod = new ItemMod(item);
                    corruptionProtMod.itemType = typeof(Armor);
                    corruptionProtMod.AddMod("mod_var", "m_corruptionProtection");
                    corruptionProtMod.AddMod("mod_value", rollQuality.ToString());
                    corruptionProtMod.AddMod("mod_value_type", "CORRUPTION_PROTECTION");
                    return corruptionProtMod;

                case ArmourModType.WATER_PROOF:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_waterproof", rollQuality);
                    ItemMod waterProofMod = new ItemMod(item);
                    waterProofMod.itemType = typeof(Armor);
                    waterProofMod.AddMod("mod_var", "m_waterproof");
                    waterProofMod.AddMod("mod_value", rollQuality.ToString());
                    waterProofMod.AddMod("mod_value_type", "WATER_PROOF");
                    return waterProofMod;

                case ArmourModType.MANA_USE_MODIFIER:
                    //var currentManaMod = outwardUTILS.ReflectionGetValue<float>(typeof(EquipmentStats), itemEquipStats, "m_manaUseModifier");
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_manaUseModifier", rollQuality * 3);
                    ItemMod manaMod = new ItemMod(item);
                    manaMod.itemType = typeof(Armor);
                    manaMod.AddMod("mod_var", "m_waterproof");
                    manaMod.AddMod("mod_value", (rollQuality * 3).ToString());
                    manaMod.AddMod("mod_value_type", "MANA_USE_MODIFIER");
                    return manaMod;

                case ArmourModType.SPEED:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_movementPenalty", rollQuality);
                    ItemMod speedMod = new ItemMod(item);
                    speedMod.itemType = typeof(Armor);
                    speedMod.AddMod("mod_var", "m_movementPenalty");
                    speedMod.AddMod("mod_value", rollQuality.ToString());
                    speedMod.AddMod("mod_value_type", "SPEED");
                    return speedMod;
            }

            return null;
        }

        //Roll for MOd Type 
        //Roll For Quality 
        //if Damage 
        //Roll For Damage Type 
        //Apply Mod
        public static ItemMod ReRollWeapon(Item item)
        {
            Debug.Log("Rerolling weapon");
            int statTypes = (int)WeaponModType.COUNT - 1;
            int rollStatType = UnityEngine.Random.Range(0, statTypes);


            WeaponModType statTypeEnum = (WeaponModType)rollStatType;

            int rollQuality = RollForQuality();
            Debug.Log("Weapon Stat Chosen is " + statTypeEnum.ToString() + " value is " + rollQuality);
            EquipmentStats itemEquipStats = item.GetComponent<EquipmentStats>();
            WeaponStats weaponEquipStats = item.GetComponent<WeaponStats>();

            Debug.Log("Weapon stats");
            Debug.Log(weaponEquipStats);

            switch (statTypeEnum)
            {
                //check the weapon damage type exists first
                case WeaponModType.DAMAGE:
                    int rollForDamageTypeCount = (int)WeaponModDamageType.COUNT - 1;
                    int rollForDamageType = UnityEngine.Random.Range(0, rollForDamageTypeCount);
                    
                    WeaponModDamageType type = (WeaponModDamageType)rollForDamageType;
                    Debug.Log("Damage type chosen is " + type.ToString());
                    switch (type)
                    {
                        case WeaponModDamageType.Physical:
                            if (CheckWeaponHasDamageType(item, type))
                            {
                                //add to the current damage 
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Physical damage");
                                AddWeaponDamage(item, DamageType.Types.Physical, rollQuality);
                            }                                                     
                            break;
                        case WeaponModDamageType.Ethereal:
                            if (CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Ethereal damage");
                                AddWeaponDamage(item, DamageType.Types.Ethereal, rollQuality);
                            }
                            break;
                        case WeaponModDamageType.Decay:
                            if (CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Decay damage");
                                AddWeaponDamage(item, DamageType.Types.Decay, rollQuality);
                            }
                            break;
                        case WeaponModDamageType.Electric:
                            if (CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Electric damage");
                                AddWeaponDamage(item, DamageType.Types.Electric, rollQuality);
                            }
                            break;
                        case WeaponModDamageType.Frost:
                            if (CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Frost damage");
                                AddWeaponDamage(item, DamageType.Types.Frost, rollQuality);
                            }
                            break;
                        case WeaponModDamageType.Fire:
                            if (CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Fire damage");
                                AddWeaponDamage(item, DamageType.Types.Fire, rollQuality);
                            }
                            break;
                    }                 
                break;
                case WeaponModType.SPEED:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(WeaponStats), weaponEquipStats, "AttackSpeed", rollQuality);
                break;
                case WeaponModType.REACH:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(WeaponStats), weaponEquipStats, "Reach", rollQuality);
                    break;
            }
            return null;
        }

        public static int RollForQuality()
        {
            int rollTypes = (int)RollQuality.COUNT - 1;

            int rollRarity = UnityEngine.Random.Range(0, rollTypes);
            RollQuality rollQualityEnum = (RollQuality)rollRarity;
            var valueToModBy = 0;

            switch (rollQualityEnum)
            {
                case RollQuality.UNCOMMON:
                    valueToModBy = 10;
                    break;
                case RollQuality.RARE:
                    valueToModBy = 15;
                    break;
                case RollQuality.EPIC:
                    valueToModBy = 27;
                    break;
                case RollQuality.LEGENDARY:
                    valueToModBy = 35;
                    break;
            }
            return valueToModBy;
        }

        public static void AddWeaponDamage(Item item, DamageType.Types weaponDamageType, float damageAmount)
        {
            WeaponStats weaponStatComponent = item.GetComponent<WeaponStats>();
            DamageType damageType = new DamageType(weaponDamageType, damageAmount);
            weaponStatComponent.BaseDamage.Add(damageType);
            SetAttackStepDamage(weaponStatComponent.Attacks, damageAmount);
        }

        public static void UpdateWeaponDamage(Item item, DamageType.Types weaponDamageType, float damageAmount)
        {
            WeaponStats weaponStatComponent = item.GetComponent<WeaponStats>();
            DamageType damageType = weaponStatComponent.BaseDamage.List.Find(x => x.Type == weaponDamageType);
            SetAttackStepDamage(weaponStatComponent.Attacks, damageAmount);
        }

        public static bool CheckWeaponHasDamageType(Item item, WeaponModDamageType weaponDamageType)
        {
            DamageType damageType = null;

            switch (weaponDamageType)
            {
                case WeaponModDamageType.Physical:
                    damageType = item.GetComponent<WeaponStats>().BaseDamage.List.Find(x => x.Type == DamageType.Types.Physical);
                    break;
                case WeaponModDamageType.Ethereal:
                    damageType = item.GetComponent<WeaponStats>().BaseDamage.List.Find(x => x.Type == DamageType.Types.Ethereal);
                    break;
                case WeaponModDamageType.Decay:
                    damageType = item.GetComponent<WeaponStats>().BaseDamage.List.Find(x => x.Type == DamageType.Types.Decay);
                    break;
                case WeaponModDamageType.Electric:
                    damageType = item.GetComponent<WeaponStats>().BaseDamage.List.Find(x => x.Type == DamageType.Types.Electric);
                    break;
                case WeaponModDamageType.Frost:
                    damageType = item.GetComponent<WeaponStats>().BaseDamage.List.Find(x => x.Type == DamageType.Types.Frost);
                    break;
                case WeaponModDamageType.Fire:
                    damageType = item.GetComponent<WeaponStats>().BaseDamage.List.Find(x => x.Type == DamageType.Types.Fire);
                    break;
            }
            return damageType != null ? true : false;
        }

        public static void SetAttackStepDamage(WeaponStats.AttackData[] attackData, float damageValue)
        {
            Debug.Log("Setting Attack Step Damage for each step.");
            Debug.Log("  To " + damageValue);
            //iterate each attack step in attack data
            for (int i = 0; i < attackData.Length; i++)
            {
                var currentAttackStep = attackData[i];
                currentAttackStep.Damage.Add(damageValue);
            }
        }
    }

    public enum RollQuality
    {
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY,
        COUNT
    }
}
