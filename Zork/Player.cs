using System;
using System.Collections.Generic;

namespace Zork
{
    public class Player
    {
        public Room CurrentRoom
        {
            get => _currentRoom;
            set => _currentRoom = value;
        }
        public List<Item> Inventory { get; }

        public Player(World world, string startingLocation)
        {
            _world = world;

            if (_world.RoomsByName.TryGetValue(startingLocation, out _currentRoom) == false)
            {
                throw new Exception($"Invalid starting location: {startingLocation}");
            }

            Inventory = new List<Item>();
        }

        public bool Move(Directions direction)
        {
            bool didMove = _currentRoom.Neighbors.TryGetValue(direction, out Room neighbor);
            if (didMove)
            {
                CurrentRoom = neighbor;
            }
                        
            return didMove;
        }

        //public bool CanTakeItem(Item itemToTake)
        //{
        //    bool TakeItem = _currentRoom.RoomInventory.Find(itemToTake);
        //}

        //May need to add an itemsbyname dictionary for the player's inventory

        private World _world;
        private Room _currentRoom;


    }
}
