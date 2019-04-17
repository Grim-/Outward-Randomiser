using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RandomItemStats
{
    public static class WeaponHelper
    {
        #region Damage
        public static void AddWeaponDamage(Item item, DamageType.Types weaponDamageType, float damageAmount)
        {
            WeaponStats weaponStatComponent = item.GetComponent<WeaponStats>();
            DamageType damageType = new DamageType(weaponDamageType, damageAmount);
            weaponStatComponent.BaseDamage.Add(damageType);
            AddAttackStepDamageType(weaponStatComponent.Attacks, damageAmount, weaponDamageType);
        }

        public static bool CheckWeaponHasDamageType(Item item, WeaponModDamageType weaponDamageType)
        {
            DamageType damageType = null;
            // I know this is really dumb
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

        public static void AddAttackStepDamageType(WeaponStats.AttackData[] attackData, float damageValue, DamageType.Types damageType)
        {
            Debug.Log("Setting Attack Step Damage for each step.");
            Debug.Log("To " + damageValue);

            var damageTypeAsInt = (int)damageType;

            //iterate each attack step in attack data
            for (int i = 0; i < attackData.Length; i++)
            {
                //Only update the damage for the given type
                var currentAttackStep = attackData[i];
                currentAttackStep.Damage.Add(damageValue);

            }
        }

        public static void UpdateAttackStepDamage(WeaponStats.AttackData[] attackData, float damageValue, DamageType.Types damageType)
        {
            Debug.Log("Udapting Attack Step Damage for each step. (" + damageType.ToString() + ")");
            var damageTypeAsInt = (int)damageType;

            //iterate each attack step in attack data
            for (int i = 0; i < attackData.Length; i++)
            {
                //Only update the damage for the given type
                var currentAttackStep = attackData[i];
                var oldValue = currentAttackStep.Damage[damageTypeAsInt];
                var newValue = oldValue + damageValue;
                Debug.Log("From " + oldValue + " To " + newValue);
                currentAttackStep.Damage[damageTypeAsInt] = newValue;
            }
        }
        #endregion

        #region Durability
        public static void UpdateDurability(Item item, float newDurabilityValue)
        {
            
            ItemStats itemStatsComp = item.GetComponent<WeaponStats>();
            var oldDuraValue = itemStatsComp.MaxDurability;
            var newValue = (int)oldDuraValue + (int)newDurabilityValue;
            Debug.Log("Setting Durability from " + oldDuraValue + " to " + newValue);
            itemStatsComp.MaxDurability = newValue;
        }
        #endregion

        #region Reach
        public static void UpdateAttackReach(Item item, float newReachValue)
        {
            WeaponStats weaponStatsComp = item.GetComponent<WeaponStats>();
            var oldReachValue = weaponStatsComp.Reach;
            Debug.Log("Setting REach from " + oldReachValue + " to " + newReachValue);
            weaponStatsComp.Reach = oldReachValue + newReachValue;
        }
        #endregion
       
        //I think setting a variable on the WeaponStatsComp 
        //Setting certain weapon stats Such as Impact, Speed, Stamina cost
        //Is done two fold 
        //1 Update the WeaponStatComp fir the Display
        //2 Update the AttackData for each "Step"/combo

        #region Attack Speed
        public static void UpdateAttackSpeed(Item item, float newAttackSpeed)
        {
            WeaponStats weaponStatsComp = item.GetComponent<WeaponStats>();
            var oldValue = weaponStatsComp.AttackSpeed;
            var newValue = oldValue + newAttackSpeed;
            Debug.Log("Setting Attack Speed from " + oldValue + " to " + newValue);
            weaponStatsComp.AttackSpeed = newValue;
            UpdateAttackStepSpeed(weaponStatsComp.Attacks, newAttackSpeed);
        }

        public static void UpdateAttackStepSpeed(WeaponStats.AttackData[] attackData, float speedValue)
        {
            Debug.Log("Setting Attack Step Speed for each step.");
            //iterate each attack step in attack data
            for (int i = 0; i < attackData.Length; i++)
            {
                var currentAttackStep = attackData[i];
                var oldValue = currentAttackStep.AttackSpeed;
                var newValue = oldValue + speedValue;
                Debug.Log("From " + oldValue + " To " + newValue);
                currentAttackStep.AttackSpeed = newValue;
            }
        }
        #endregion


        #region Impact
        public static void UpdateImpact(Item item, float newImpact)
        {
            WeaponStats weaponStatsComp = item.GetComponent<WeaponStats>();
            var oldReachValue = weaponStatsComp.Impact;
            Debug.Log("Setting Impact from " + oldReachValue + " to " + newImpact);
            weaponStatsComp.Impact = oldReachValue + newImpact;
            UpdateAttackStepImpact(weaponStatsComp.Attacks, newImpact);
        }

        public static void UpdateAttackStepImpact(WeaponStats.AttackData[] attackData, float knockbackAmount)
        {
            Debug.Log("Setting Attack Step Knockback for each step.");
            //iterate each attack step in attack data
            for (int i = 0; i < attackData.Length; i++)
            {
                var currentAttackStep = attackData[i];
                var oldValue = currentAttackStep.Knockback;
                var newValue = oldValue + knockbackAmount;
                Debug.Log("From " + oldValue + " To " + newValue);
                currentAttackStep.Knockback = newValue;
            }
        }
        #endregion

        #region Stamina
        public static void UpdateStaminaCost(Item item, float newStaminaCost)
        {
            WeaponStats weaponStatsComp = item.GetComponent<WeaponStats>();
            var oldValue = weaponStatsComp.StamCost;
            Debug.Log("Setting Stamina Cost from " + oldValue + " to " + newStaminaCost);
            weaponStatsComp.Impact = oldValue + newStaminaCost;
            SetAttackStepStaminaCost(weaponStatsComp.Attacks, newStaminaCost);
        }

        public static void SetAttackStepStaminaCost(WeaponStats.AttackData[] attackData, float staminaCost)
        {
            Debug.Log("Setting Attack Step Stamina Cost for each step.");
            //iterate each attack step in attack data
            for (int i = 0; i < attackData.Length; i++)
            {
                var currentAttackStep = attackData[i];
                var oldValue = currentAttackStep.StamCost;
                var newValue = oldValue + staminaCost;
                Debug.Log("From " + oldValue + " To " + newValue);
                currentAttackStep.StamCost = newValue;
            }
        }
        #endregion
    }
}
