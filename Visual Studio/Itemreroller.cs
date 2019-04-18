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
        public static ItemMod ReRollArmour(Item item)
        {
            Debug.Log("Rerolling armor");
            int statTypes = (int)ArmourModType.COUNT - 1;
            int rollStatType = UnityEngine.Random.Range(0, statTypes);
            ArmourModType statTypeEnum = (ArmourModType)rollStatType;
            int rollQuality = RollHelper.RollForQuality();
            Debug.Log("Armor Stat Chosen is " + statTypeEnum.ToString() + " value is " + rollQuality);

            //Component References
            EquipmentStats itemEquipStats = item.GetComponent<EquipmentStats>();
            ArmorBaseData armorBaseData = item.GetComponent<ArmorBaseData>();
            ItemStats itemStats = item.GetComponent<ItemStats>();

            switch (statTypeEnum)
            {
                case ArmourModType.HEALTH_BONUS:
                outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_maxHealthBonus", rollQuality);
                return CreateItemModFor<Armor>(item, rollQuality, "m_maxHealthBonus", "HEALTH_BONUS");

                case ArmourModType.POUCH_BONUS:
                outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_pouchCapacityBonus", rollQuality);
                return CreateItemModFor<Armor>(item, rollQuality, "m_pouchCapacityBonus", "POUCH_BONUS");

                case ArmourModType.HEAT_PROTECTION:
                outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_heatProtection", rollQuality);
                return CreateItemModFor<Armor>(item, rollQuality, "m_heatProtection", "HEAT_PROTECTION");

                case ArmourModType.COLD_PROTECTION:
                outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_coldProtection", rollQuality);
                return CreateItemModFor<Armor>(item, rollQuality, "m_coldProtection", "COLD_PROTECTION");

                case ArmourModType.WATER_PROOF:
                outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_waterproof", rollQuality);
                return CreateItemModFor<Armor>(item, rollQuality, "m_waterproof", "WATER_PROOF");

                case ArmourModType.MANA_USE_MODIFIER:
                outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_manaUseModifier", rollQuality * 3);
                return CreateItemModFor<Armor>(item, rollQuality, "m_manaUseModifier", "MANA_USE_MODIFIER");

                case ArmourModType.SPEED:
                outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_movementPenalty", rollQuality);
                return CreateItemModFor<Armor>(item, rollQuality, "m_movementPenalty", "SPEED");

                case ArmourModType.WEIGHT:
                outwardUTILS.ReflectionUpdateOrSetFloat(typeof(ItemStats), itemStats, "m_rawWeight", -rollQuality);
                return CreateItemModFor<Armor>(item, rollQuality, "m_rawWeight", "WEIGHT");

                case ArmourModType.STAMINA_USE_MODIFIER:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(EquipmentStats), itemEquipStats, "m_staminaUsePenalty", -rollQuality);
                return CreateItemModFor<Armor>(item, rollQuality, "m_staminaUsePenalty", "STAMINA_USE_MODIFIER");

                case ArmourModType.DAMAGE_PROTECTION:
                    WeaponModDamageType chosenType = RollHelper.RollForDamageType();


                break;

                case ArmourModType.DAMAGE_RESISTANCE:

                break;

                case ArmourModType.DURABILITY:
                    outwardUTILS.ReflectionUpdateOrSetFloat(typeof(ItemStats), itemStats, "MaxDurability", -rollQuality);
                return CreateItemModFor<Armor>(item, rollQuality, "MaxDurability", "DURABILITY");

                default:
                    Debug.Log("Unimplemented Type");
                break;
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

            int rollQuality = RollHelper.RollForQuality();
            Debug.Log("Weapon Stat Chosen is " + statTypeEnum.ToString() + " value is " + rollQuality);


            EquipmentStats itemEquipStats = item.GetComponent<EquipmentStats>();
            WeaponStats itemWeaponStats = item.GetComponent<WeaponStats>();
            WeaponBaseData weaponBaseData = item.GetComponent<WeaponBaseData>();


            Debug.Log("Weapon stats");
            Debug.Log(itemWeaponStats);

            switch (statTypeEnum)
            {
                //check the weapon damage type exists first
                case WeaponModType.DAMAGE:


                    WeaponModDamageType type = RollHelper.RollForDamageType();
                    Debug.Log("Damage type chosen is " + type.ToString());

                    switch (type)
                    {
                        case WeaponModDamageType.Physical:
                            if (WeaponHelper.CheckWeaponHasDamageType(item, type))
                            {
                                //add to the current damage 
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Physical damage");
                                WeaponHelper.AddWeaponDamage(item, DamageType.Types.Physical, rollQuality);
                                return CreateItemModFor<Weapon>(item, rollQuality, "physical", "damage");
                            }
                        break;

                        case WeaponModDamageType.Ethereal:
                            if (WeaponHelper.CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Ethereal damage");
                                WeaponHelper.AddWeaponDamage(item, DamageType.Types.Ethereal, rollQuality);
                                return CreateItemModFor<Weapon>(item, rollQuality, "ethereal", "damage");
                            }
                        break;

                        case WeaponModDamageType.Decay:
                            if (WeaponHelper.CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Decay damage");
                                WeaponHelper.AddWeaponDamage(item, DamageType.Types.Decay, rollQuality);
                                return CreateItemModFor<Weapon>(item, rollQuality, "decay", "damage");
                            }
                        break;

                        case WeaponModDamageType.Electric:
                            if (WeaponHelper.CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Electric damage");
                                WeaponHelper.AddWeaponDamage(item, DamageType.Types.Electric, rollQuality);
                                return CreateItemModFor<Weapon>(item, rollQuality, "electric", "damage");
                            }
                        break;

                        case WeaponModDamageType.Frost:
                            if (WeaponHelper.CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Frost damage");
                                WeaponHelper.AddWeaponDamage(item, DamageType.Types.Frost, rollQuality);
                                return CreateItemModFor<Weapon>(item, rollQuality, "frost", "damage");
                            }
                        break;

                        case WeaponModDamageType.Fire:
                            if (WeaponHelper.CheckWeaponHasDamageType(item, type))
                            {
                                Debug.Log("Weapon already has this damage type NotYetImplemented");
                            }
                            else
                            {
                                Debug.Log("Adding Fire damage");
                                WeaponHelper.AddWeaponDamage(item, DamageType.Types.Fire, rollQuality);
                                return CreateItemModFor<Weapon>(item, rollQuality, "fire", "damage");
                            }
                        break;
                    }
                break;

                case WeaponModType.SPEED:
                    WeaponHelper.UpdateAttackSpeed(item, rollQuality);
                return CreateItemModFor<Weapon>(item, rollQuality, "speed", "SPEED");

                case WeaponModType.REACH:
                    WeaponHelper.UpdateAttackReach(item, rollQuality);
                return CreateItemModFor<Weapon>(item, rollQuality, "reach", "REACH");

                case WeaponModType.DURABILITY:
                    WeaponHelper.UpdateDurability(item, rollQuality);
                    //outwardUTILS.ReflectionUpdateOrSetFloat(typeof(WeaponBaseData), weaponBaseData, "Durability", rollQuality);
                return CreateItemModFor<Weapon>(item, rollQuality, "Durability", "DURABILITY");

                case WeaponModType.IMPACT:
                    WeaponHelper.UpdateImpact(item, rollQuality);
                    //WeaponHelper.SetAttackStepKnockback(itemWeaponStats.Attacks, rollQuality);                
                return CreateItemModFor<Weapon>(item, rollQuality, "Impact", "IMPACT");
            }
            return null;
        }

        public static ItemMod CreateItemModFor<T>(Item item, float rollQuality, string mod_var, string mod_value_type)
        {
            ItemMod itemMod = new ItemMod(item);
            itemMod.itemType = typeof(T);
            itemMod.AddMod("mod_var", mod_var);
            itemMod.AddMod("mod_value", rollQuality.ToString());
            itemMod.AddMod("mod_value_type", mod_value_type);
            return itemMod;
        }


    }

}
