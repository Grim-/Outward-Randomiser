using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomItemStats
{
    public class ItemMod
    {
        public Item item;
        public Type itemType;
        public Dictionary<string, string> mods;

        public ItemMod(Item _item)
        {
            item = _item;
            mods = new Dictionary<string, string>();
        }

        public void AddMod(string modKey, string modValue)
        {
            if (!mods.ContainsKey(modKey))
            {
                mods.Add(modKey, modValue);
            }
        }
    }
}
