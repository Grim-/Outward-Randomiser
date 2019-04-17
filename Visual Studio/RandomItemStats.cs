using Partiality.Modloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SimpleJSON;
using System.IO;
using System.Reflection;

namespace RandomItemStats
{
    public class RandomItemStats : PartialityMod
    {
        private ScriptLoad script;
        private static string itemDB_location = "Mods/RandomItemStats/itemDB.json";
        public JSONNode itemDB;

        public RandomItemStats()
        {
            this.ModID = "Random Stats Roller";
            this.Version = "0.1";
            this.author = "Emo";
        }

        public override void OnEnable()
        {
            base.OnEnable();

            GameObject go = new GameObject();
            script = go.AddComponent<ScriptLoad>();
            script.ris = this;
            script.Initialise();

            LoadItemDB();
        }


        public void SaveItemDB()
        {
            File.WriteAllText(itemDB_location, itemDB.ToString());
        }

        public void LoadItemDB()
        {
            JDBHelper.LoadItemDB();
        }

        public void ReInitaliseRandomisedItems(JSONNode currentCharacter, ItemManager itemMan)
        {
            Debug.Log("Reinit Modded Items");

            foreach (var item in currentCharacter["items"].AsArray)
            {
                var itemToUpdate = itemMan.GetItem(item.Value["item_UID"]);
                var itemType = (ItemModType) Enum.Parse(typeof(ItemModType), item.Value["item_type"], true);
                var itemMods = item.Value["modifications"].AsArray;
                Debug.Log("item");
                Debug.Log(itemToUpdate);
                Debug.Log("item type");
                Debug.Log(itemType);
                //for each item type 

                switch (itemType)
                {
                    case ItemModType.WEAPON:
                        break;
                    case ItemModType.ARMOR:

                        ReInitaliseArmours(itemToUpdate, itemMods);
                        break;
                }

            }
        }


        public void ReInitaliseArmours(Item item, JSONArray itemMods)
        {
            Debug.Log("Reinit Armour Item " + item.Name);
            if (item != null)
            {
                Debug.Log("Reinit Armour Item " + item.Name);

                EquipmentStats itemEquipStats = item.GetComponent<EquipmentStats>();

                foreach (var thisModification in itemMods)
                {
                    var modifyVar = thisModification.Value["mod_var"];
                    var modifyValue = thisModification.Value["mod_value"];
                    var modifyVarType = (ArmourModType)Enum.Parse(typeof(ArmourModType), thisModification.Value["mod_value_type"], true);


                    Debug.Log(modifyVarType);
                    switch (modifyVarType)
                    {
                        case ArmourModType.SPEED:
                            outwardUTILS.ReflectionSetValue(typeof(EquipmentStats), itemEquipStats, modifyVar, modifyValue.AsFloat);
                            break;
                        case ArmourModType.HEALTH_BONUS:
                            outwardUTILS.ReflectionSetValue(typeof(EquipmentStats), itemEquipStats, modifyVar, modifyValue.AsFloat);
                            break;
                        case ArmourModType.POUCH_BONUS:
                            outwardUTILS.ReflectionSetValue(typeof(EquipmentStats), itemEquipStats, modifyVar, modifyValue.AsFloat);
                            break;
                        case ArmourModType.HEAT_PROTECTION:
                            outwardUTILS.ReflectionSetValue(typeof(EquipmentStats), itemEquipStats, modifyVar, modifyValue.AsFloat);
                            break;
                        case ArmourModType.COLD_PROTECTION:
                            outwardUTILS.ReflectionSetValue(typeof(EquipmentStats), itemEquipStats, modifyVar, modifyValue.AsFloat);
                            break;
                        case ArmourModType.DAMAGE_RESISTANCE:                           
                           // outwardUTILS.ReflectionSetValue(typeof(EquipmentStats), itemEquipStats, modifyVar, modifyValue.AsFloat);
                            break;
                        case ArmourModType.DAMAGE_PROTECTION:
                            //outwardUTILS.ReflectionSetValue(typeof(EquipmentStats), itemEquipStats, modifyVar, modifyValue.AsFloat);
                            break;
                        case ArmourModType.WATER_PROOF:
                            outwardUTILS.ReflectionSetValue(typeof(EquipmentStats), itemEquipStats, modifyVar, modifyValue.AsFloat);
                            break;
                        case ArmourModType.MANA_USE_MODIFIER:
                            outwardUTILS.ReflectionSetValue(typeof(EquipmentStats), itemEquipStats, modifyVar, modifyValue.AsFloat);
                            break;
                    }

                }

            }
        }

        public void ReInitaliseWeapons()
        {

        }
    }
}
