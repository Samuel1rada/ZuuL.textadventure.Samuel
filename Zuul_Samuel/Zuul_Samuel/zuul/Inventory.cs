using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Zuul;

namespace zuul
{


    public class Inventory
    {
        private int maxWeight;
        private Dictionary<string, Item> items; 
        public Inventory(int m)
        {
            this.maxWeight = m;
            this.items = new Dictionary<string, Item>();

            
        }
       

        public bool Put(string itemName, Item item)
        { 
            if(Totalwieght() + item.Weight > maxWeight)
            {
                return false;
            }
            items.Add(itemName, item);  
            return true;
           
        }
        public Item Get(string itemName)
        {
            if(items.ContainsKey(itemName))
            {
                Item item = items[itemName];
                items.Remove(itemName);
                return item;
            }
            return null;
        }
       
        public string show()
        {
            string str = "";
            if(!IsEmpty())
            {
                foreach (string itemName in items.Keys)
                {
                    Item item = items[itemName];
                    str += " { " + itemName + " } " + item.Description + "(" + item.Weight + " kg)\n";
                }
            }
            return str;
        }

        private int Totalwieght()
        {
            int total = 0;
            foreach (string itemName in items.Keys)
            {
                total += items[itemName].Weight;

            }
            return total;
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }
        public int Freewieght()
        {
            return maxWeight - Totalwieght();
        }

      


    }

}   
