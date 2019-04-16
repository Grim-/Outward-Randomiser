using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;
using UnityEngine;

namespace RandomItemStats
{
    class JDBHelpers
    {

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
