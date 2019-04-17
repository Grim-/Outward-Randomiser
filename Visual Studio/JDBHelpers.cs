using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;
using UnityEngine;
using System.IO;

namespace RandomItemStats
{
    public static class JDBHelper
    {
        private static string itemDB_location = "Mods/RandomItemStats/itemDB.json";

        private static JSONNode itemDB;


        private static JSONNode currentCharacterSave;


        public static void LoadItemDB()
        {
            string json = File.ReadAllText(itemDB_location);
            itemDB = JSON.Parse(json);
        }

        public static void SaveItemDB()
        {
            File.WriteAllText(itemDB_location, itemDB.ToString());
        }

        public static void AddItemToDB(JSONNode currentCharacter, ItemMod itemMod)
        {
            var newItemObject = new JSONObject();
            var newItemModArray = new JSONArray();

            Debug.Log("item mod item");
            Debug.Log(itemMod.item);

            newItemObject.Add("item_UID", itemMod.item.UID);
            newItemObject.Add("item_type", itemMod.itemType.ToString());
            currentCharacter["items"][-1] = newItemObject;
            var newModObj = new JSONObject();

            foreach (var mod in itemMod.mods)
            {

                newModObj.Add(mod.Key, mod.Value);

            }
            newItemModArray.Add(newModObj);
            newItemObject.Add("modifications", newItemModArray);
            SaveItemDB();
        }

        public static JSONNode LoadItemDBSetCharacter(string characterUID)
        {
            Debug.Log("Loading Item DB and Setting Current Character");
            string json = File.ReadAllText(itemDB_location);
            itemDB = JSON.Parse(json);
            var chara = SetCurrentCharacter(itemDB, characterUID);
            SaveItemDB();
            return chara;
        }

        public static JSONNode SetCurrentCharacter(JSONNode node, string playerUID)
        {
            Debug.Log("Setting Current Character UID " + playerUID);
            if (node["characters"] != null)
            {
                Debug.Log("Found Characters Array in Config File");

                if (DoesCharacterExist(node, playerUID))
                {
                    Debug.Log("Character Does Exist in Config File");
                    var charSaveIndex = GetCharacterSaveIndex(node, playerUID);
                    var charSaveObject = node["characters"][charSaveIndex];

                    Debug.Log("Setting as currentCharacter");
                    currentCharacterSave = charSaveObject;
                    return charSaveObject;
                }
                else
                {
                    Debug.Log("Character Does Not Exist in Config File");


                    Debug.Log("Setting as currentCharacter");
                    return CreateNewCharacterSave(node, playerUID);
                }


            }

            return null;
        }

        public static bool DoesCharacterExist(JSONNode node, string characterUID)
        {
            for (int i = 0; i < node["characters"].AsArray.Count; i++)
            {

                var eachCharID = node["characters"][i]["characterUID"];
                //if the current ID exists modify instead of adding
                if (eachCharID == characterUID)
                {
                    return true;
                }

            }

            return false;
        }

        public static JSONObject CreateNewCharacterSave(JSONNode node, string characterUID)
        {

            //Debug.Log("Adding a New Character");
            var newplayerObject = new JSONObject();
            node["characters"][-1] = newplayerObject;


            //values to add the new player object
            var newPlayerUID = new JSONString(characterUID);
            var newItemsArray = new JSONArray();

            //now actually add them to the object
            newplayerObject.Add("characterUID", newPlayerUID);
            newplayerObject.Add("items", newItemsArray);

            return newplayerObject;
        }

        public static int GetCharacterSaveIndex(JSONNode node, string characterUID)
        {
            for (int i = 0; i < node["characters"].AsArray.Count; i++)
            {

                var eachCharID = node["characters"][i]["characterUID"];
                //if the current ID exists modify instead of adding
                if (eachCharID == characterUID)
                {
                    return i;
                }

            }
            return 0;
        }
    }
}
