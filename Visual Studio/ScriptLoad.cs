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
            //On.SceneInteractionManager.DoneLoadingLevel += new On.SceneInteractionManager.hook_DoneLoadingLevel(simDoneLoadingHook);

            //On.InteractionOpenContainer.OnActivate += new On.InteractionOpenContainer.hook_OnActivate(containerActivateHook);
            On.InteractionOpenChest.OnActivate += new On.InteractionOpenChest.hook_OnActivate(chestActivateHook);
            On.PlayerSystem.StartInit += new On.PlayerSystem.hook_StartInit(playerSystemStartHook);

            On.Bag.OnItemAddedToBag += new On.Bag.hook_OnItemAddedToBag(onItemAddedHook);
            On.PlayerSystem.Update += new On.PlayerSystem.hook_Update(dddd);
        }

        private void dddd(On.PlayerSystem.orig_Update orig, PlayerSystem self)
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
                Debug.Log("Notifying Adding Item " + _item.DisplayName);
                Debug.Log("Item to add is not null");
                Debug.Log("Adding modified weapon to inventory and adding to json");
                ris.AddItemToDB(currentCharSave, itemToAdd);
            }
        }

        //Used for LoadingDB originally and Setting the Character Save Object
        private void playerSystemStartHook(On.PlayerSystem.orig_StartInit orig, PlayerSystem self)
        {
            orig(self);
            currentCharSave = ris.LoadItemDBSetCharacter(self.CharUID);
        }

        //TODO: A method to hook into to Modify each item changed by the mod DONE
        //flow > Hook > Load UIDs of Modded Items > Mod those items back to what they were


        //Used for potentially modifying an item then saving that UID and Modification to the Item DB
        private void chestActivateHook(On.InteractionOpenChest.orig_OnActivate orig, InteractionOpenChest self)
        {
            orig(self);

            Debug.Log("this should fire when a chest is opened");


            //self.LastCharacter == null;

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
                modifiedStuff.Add(itemMod);
            }

            if (armour.Count > 0)
            {
                var itemMod = Itemreroller.ReRollArmour(armour[0]);
                modifiedStuff.Add(itemMod);
            }

        }

        private void containerActivateHook(On.InteractionOpenContainer.orig_OnActivate orig, InteractionOpenContainer self)
        {
            orig(self);

            Debug.Log("This should fire when a container activate hook is fired");
        }

        private void simDoneLoadingHook(On.SceneInteractionManager.orig_DoneLoadingLevel orig, SceneInteractionManager self)
        {
            orig(self);

            //Type myType = typeof(SceneInteractionManager);
            //FieldInfo myField = myType.GetField("m_dropTables", BindingFlags.Instance | BindingFlags.NonPublic);
            //dropTables = (Dictionary<string, DropTable>) myField.GetValue(self);

            //dropTables = outwardUTILS.ReflectionGetValue<Dictionary<string, DropTable>>(typeof(SceneInteractionManager), self, "m_dropTables");


            //foreach (var item in dropTables)
            //{

            //    Debug.Log("DT :" + item.Key);
            //    Debug.Log("Drop possibilities count " + item.Value.DropPossibilitiesCount);
            //    Type dtType = typeof(DropTable);
            //    FieldInfo dtFieldInfo = dtType.GetField("m_itemDrops", BindingFlags.Instance | BindingFlags.NonPublic);
            //    List<ItemDropChance> itemDropChances = (List <ItemDropChance>) dtFieldInfo.GetValue(item.Value);


            //    Debug.Log(itemDropChances[0].DroppedItem.DisplayName);
            //}

        }

    }
}
