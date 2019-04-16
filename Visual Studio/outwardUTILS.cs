using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RandomItemStats
{
    public static class outwardUTILS
    {

        public static T ReflectionGetValue <T>(Type objType, object obj, string value)
        {
            FieldInfo fieldInfo = objType.GetField(value, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T) fieldInfo.GetValue(obj);
        }

        public static void ReflectionSetValue<T>(Type objType, object obj, string value, T newValue)
        {
            FieldInfo fieldInfo = objType.GetField(value, BindingFlags.NonPublic | BindingFlags.Instance);

            fieldInfo.SetValue(obj, newValue);
        }

        public static void ReflectionUpdateOrSetFloat(Type objType, object obj, string value, float newValue)
        {
            FieldInfo fieldInfo = objType.GetField(value, BindingFlags.NonPublic | BindingFlags.Instance);
            var currentValue = (float) fieldInfo.GetValue(obj);

            if (currentValue  > 0)
            {
                var tempValue = currentValue + newValue;
                Debug.Log("Stat Value is " + currentValue + " updating to  "  + tempValue);
                fieldInfo.SetValue(obj, tempValue);
            }
            else
            {
                Debug.Log("Stat is zero setting ");
                fieldInfo.SetValue(obj, newValue);
            }           
        }
    }
}
