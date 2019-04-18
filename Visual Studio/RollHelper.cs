using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RandomItemStats
{
    public static class RollHelper
    {
        public static WeaponModDamageType RollForDamageType()
        {
            int damageTypes = (int)WeaponModDamageType.COUNT - 1;

            int damageTypeRoll = UnityEngine.Random.Range(0, damageTypes);

            return (WeaponModDamageType)damageTypeRoll;
        }

        public static int RollForQuality()
        {
            Debug.Log("Rolling for modification Quality");
            int rollTypes = (int)RollQuality.COUNT - 1;

            int rollRarity = UnityEngine.Random.Range(0, rollTypes);
            RollQuality rollQualityEnum = (RollQuality)rollRarity;


            var weights = new Dictionary<RollQuality, int>();
            weights.Add(RollQuality.UNCOMMON, 60); // 60% spawn chance;
            weights.Add(RollQuality.RARE, 20); // 20% spawn chance;
            weights.Add(RollQuality.EPIC, 10); // 10% spawn chance;
            weights.Add(RollQuality.LEGENDARY, 7); // 7% spawn chance;
            weights.Add(RollQuality.CURSED, 3); // 3% spawn chance;

            RollQuality selected = WeightedRandomizer.From(weights).TakeOne();

            Debug.Log(selected.ToString() + " Was Chosen");
            var valueToModBy = 0;

            switch (selected)
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
                case RollQuality.CURSED:
                    valueToModBy = -15;
                    break;
            }
            return valueToModBy;
        }
    }
}
