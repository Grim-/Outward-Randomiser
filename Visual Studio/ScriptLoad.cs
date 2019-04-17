using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using On;
using UnityEngine;
using System.Reflection;

namespace RandomItemStats
{
    public class ScriptLoad : MonoBehaviour
    {
        private List<ItemMod> modifiedStuff = new List<ItemMod>();

        public RandomItemStats ris;

        private SimpleJSON.JSONNode currentCharSave;

        private bool inited = false;

        public void Initialise()
        {
            Patch();
        }

        public void Patch()
        {
            On.InteractionOpenChest.OnActivate += new On.InteractionOpenChest.hook_OnActivate(chestActivateHook);
            On.PlayerSystem.StartInit += new On.PlayerSystem.hook_StartInit(playerSystemStartHook);

            On.Bag.OnItemAddedToBag += new On.Bag.hook_OnItemAddedToBag(onItemAddedHook);
            On.PlayerSystem.Update += new On.PlayerSystem.hook_Update(playerSystemUpdateHook);
        }

        private void playerSystemUpdateHook(On.PlayerSystem.orig_Update orig, PlayerSystem self)
        {
            orig(self);

            if (!inited)
            {
                if (ItemManager.Instance.IsAllItemSynced)
                {
                    inited = true;
                    ris.ReInitaliseRandomisedItems(currentCharSave, ItemManager.Instance);               
                }
            }
        }

        private void onItemAddedHook(On.Bag.orig_OnItemAddedToBag orig, Bag self, Item _item)
        {
            orig(self, _item);
            var itemToAdd = modifiedStuff.Find(x => x.item.UID == _item.UID);
            if (itemToAdd != null)
            {
                Debug.Log(itemToAdd.item.DisplayName);
                Debug.Log("Notifying Adding Item " + itemToAdd.item.DisplayName);
                Debug.Log("Item to add is not null");
                Debug.Log("Adding modified weapon to inventory and adding to json");
                JDBHelper.AddItemToDB(currentCharSave, itemToAdd);
            }
        }

        //Used for LoadingDB originally and Setting the Character Save Object
        private void playerSystemStartHook(On.PlayerSystem.orig_StartInit orig, PlayerSystem self)
        {
            orig(self);
            currentCharSave = JDBHelper.LoadItemDBSetCharacter(self.CharUID);
        }

        //TODO: A method to hook into to Modify each item changed by the mod DONE
        //flow > Hook > Load UIDs of Modded Items > Mod those items back to what they were


        //Used for potentially modifying an item then saving that UID and Modification to the Item DB
        private void chestActivateHook(On.InteractionOpenChest.orig_OnActivate orig, InteractionOpenChest self)
        {
            orig(self);
            Debug.Log("this should fire when a chest is opened");

            ItemContainer chestContainer = outwardUTILS.ReflectionGetValue<ItemContainer>(typeof(InteractionOpenChest), self, "m_chest");         
            Debug.Log(chestContainer.IsInWorld);
            Debug.Log(chestContainer.ItemCount);
            List<Weapon> weapons = chestContainer.GetItemOfType<Weapon>();

            Debug.Log("Weapon in chest count");
            Debug.Log(weapons.Count);

            List<Armor> armour = chestContainer.GetItemOfType<Armor>();

            Debug.Log("Armor in chest count");
            Debug.Log(armour.Count);


            if (weapons.Count > 0)
            {
                var itemMod = Itemreroller.ReRollWeapon(weapons[0]);
                if (itemMod != null)
                {
                    modifiedStuff.Add(itemMod);
                }
                else
                {
                    Debug.Log("A Null ItemMod can't be added to a list");
                }
            }

            if (armour.Count > 0)
            {
                var itemMod = Itemreroller.ReRollArmour(armour[0]);

                if (itemMod != null)
                {
                    modifiedStuff.Add(itemMod);
                }
                else
                {
                    Debug.Log("A Null ItemMod can't be added to a list");
                }
                
            }

        }

    }
}
