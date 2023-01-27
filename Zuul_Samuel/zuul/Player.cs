using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zuul;
using Zuul;

namespace Zuul
{
    internal class Player
    {
        //field
        private int health;
        private Inventory inventory;
        private Dictionary<string, Item> items;


        //property
        public int Health
        { 
         get { return health; }
        }
        public Inventory playerInventory
        {
            get { return inventory; }
        }
       
        public Room CurrentRoom { get; set; }
       
        

        public Player()
        {
            CurrentRoom = null ;
            health = 100 ;
            inventory = new Inventory(30);
            
        }

        public bool TakeFromChest(string itemName)
        {
            Item item = CurrentRoom.chest.Get(itemName);
            if(item == null)
            {
                Console.WriteLine(" There is no " + itemName + " in this room");
                return false;
            }

            if(inventory.Put(itemName, item))
            {
                return true;
            }

            Console.WriteLine( itemName + " is to heavy for you to carry ");
            CurrentRoom.Chest.Put(itemName, item);
            return true;

        }
        public bool DropToChest(string itemName)
        {
            Item item = inventory.Get(itemName);
            if (item == null)
            {
                Console.WriteLine(" There is no " + itemName + " in your inventory");
                return false;
            }
            Console.WriteLine(" you have droped" + itemName );
            CurrentRoom.Chest.Put(itemName, item);
            return true;
        }


        public void Damage( int amount)
        {
            health = health - amount ;
        }
        public void Heal( int amount)
        {
            health = health + amount ;
        }
        public bool IsAlive()
        {
           return(health > 0); 
        }
      
    }
}
