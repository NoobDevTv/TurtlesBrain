﻿using System;

namespace turtleAPI
{
    /// <summary>
    /// a Turtle are a essentially robot, were added in ComputerCraft.
    /// This class represents your Turtle.
    /// </summary>
    public class Turtle : Computer
    {
        /// <summary>
        /// This class creates a new connection to your turtle in the Minecraft world
        /// </summary>
        /// <param name="label">The Name of your Turtle</param>
        public Turtle(string label) : base(label, Server.Instance)
        {
            if (Server.Instance[label] == null)
                throw new InvalidOperationException("turtle not found");
            this.wait = Server.Instance[label].wait;

        }

        internal Turtle(string label, Server server) : base(label, server)
        {
        }

        /// <summary>
        /// Try to move the turtle down
        /// </summary>
        /// <returns>boolean success</returns>
        public virtual bool down()
        {
            string reason = "";
            return down(out reason);
        }

        /// <summary>
        /// Try to move the turtle down
        /// </summary>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean of success and string of reason by faiure</returns>
        public virtual bool down(out string Reason)
        {
            string result = Send("turtle.down()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Try to move the turtle up
        /// </summary>
        /// <returns>boolean success</returns>
        public virtual bool up()
        {
            string reason = "";
            return up(out reason);
        }

        /// <summary>
        /// Try to move the turtle up
        /// </summary>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean of success and string of reason by faiure</returns>
        public virtual bool up(out string Reason)
        {
            string result = Send("turtle.up()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Try to move the turtle forward 
        /// </summary>
        /// <returns>boolean success</returns>
        public virtual bool forward()
        {
            string reason = "";
            return forward(out reason);
        }

        /// <summary>
        /// Try to move the turtle forward
        /// </summary>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean of success and string of reason by faiure</returns>
        public virtual bool forward(out string Reason)
        {
            string result = Send("turtle.forward()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Try to move the turtle backward 
        /// </summary>
        /// <returns>boolean success </returns>
        public virtual bool back()
        {
            string reason = "";
            return back(out reason);
        }

        /// <summary>
        /// Try to move the turtle backward
        /// </summary>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean of success and string of reason by faiure</returns>
        public virtual bool back(out string Reason)
        {
            string result = Send("turtle.back()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        
        /// <summary>
        /// Turn the turtle left
        /// </summary>
        /// <returns>boolean success</returns>
        public virtual bool turnLeft()
        {
            string result = Send("turtle.turnLeft()");
            return GetBool(result);
        }

        /// <summary>
        /// Turn the turtle right
        /// </summary>
        /// <returns>boolean success </returns>
        public virtual bool turnRight()
        {
            string result = Send("turtle.turnRight()");
            return GetBool(result);
        }

        /// <summary>
        /// Make the turtle select slot slotNum (1 is top left, 16 (9 in 1.33 and earlier) is bottom right)
        /// </summary>
        /// <param name="slot">slot number, upper left 1 and lower right 16</param>
        /// <returns>boolean success</returns>
        public virtual bool select(byte slot)
        {
            string result = Send("turtle.select(" + slot + ")");
            return GetBool(result);
        }

        /// <summary>
        /// Indicates the currently selected inventory slot	
        /// </summary>
        /// <returns>byte slotNumber</returns>
        public virtual byte getSelectedSlot()
        {
            return GetByte(Send("turtle.getSelectedSlot()"));
        }


        /// <summary>
        /// Counts how many items are in the slotNum slot 
        /// </summary>
        /// <param name="slotNum">slot number, upper left 1 and lower right 16</param>
        /// <returns>slotNum the number of items found in the specified slot.</returns>
        public virtual byte getItemCount(byte slotNum)
        {
            string result = Send("turtle.getItemCount(" + slotNum + ")");
            return GetByte(result);
        }

        /// <summary>
        /// Counts how many items are in the currently selected slot
        /// </summary>
        /// <returns></returns>
        public virtual byte getItemCount()
        {
            string result = Send("turtle.getItemCount()");
            return GetByte(result);
        }

        /// <summary>
        /// get the number of remaining space in the current slot
        /// </summary>
        /// <returns>byte space</returns>
        public virtual byte getItemSpace()
        {
            string result = Send("turtle.getItemSpace()");
            return GetByte(result);
        }

        /// <summary>
        /// get the number of remaining space in the specified slot
        /// </summary>
        /// <param name="slotNum">slot number, upper left 1 and lower right 16</param>
        /// <returns>byte space</returns>
        public virtual byte getItemSpace(byte slotNum)
        {
            string result = Send("turtle.getItemSpace(" + slotNum + ")");
            return GetByte(result);
        }

        /// <summary>
        /// Equip an item to the left side of the turtle. <para />
        /// If an item was already present there, it'll be placed back to the inventory of the turtle
        /// in the selected slot and the new item will be equiped.<para />
        /// • Works only with ComputerCraft 1.6 or greater!
        /// </summary>
        /// <returns>boolean was the item equipped successfully</returns>
        public virtual bool equipLeft()
        {
            string reason = "";
            return equipLeft(out reason);
        }
        /// <summary>
        /// Equip an item to the left side of the turtle. <para />
        /// If an item was already present there, it'll be placed back to the inventory of the turtle
        /// in the selected slot and the new item will be equiped.<para />
        /// • Works only with ComputerCraft 1.6 or greater!
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean was the item equipped successfully</returns>
        public virtual bool equipLeft(out string Reason)
        {
            string result = Send("turtle.equipLeft()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Equip an item to the right side of the turtle. <para />
        /// If an item was already present there, it'll be placed back to the inventory of the turtle
        /// in the selected slot and the new item will be equiped.<para />
        /// • Works only with ComputerCraft 1.6 or greater! 
        /// </summary>
        /// <returns>boolean was the item equipped successfully</returns>
        public virtual bool equipRight()
        {
            string reason = "";
            return equipRight(out reason);
        }

        /// <summary>
        /// Equip an item to the right side of the turtle. <para />
        /// If an item was already present there, it'll be placed back to the inventory of the turtle
        /// in the selected slot and the new item will be equiped.
        /// • Works only with ComputerCraft 1.6 or greater!
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean was the item equipped successfully</returns>
        public virtual bool equipRight(out string Reason)
        {
            string result = Send("turtle.equipRight()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Attempts to attack in front of the turtle.
        /// </summary>
        /// <returns>boolean whether the turtle succeeded in attacking forward</returns>
        public virtual bool attack()
        {
            string reason = "";
            return attack(out reason);
        }

        /// <summary>
        /// Attempts to attack in front of the turtle.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean whether the turtle succeeded in attacking forward</returns>
        public virtual bool attack(out string Reason)
        {
            string result = Send("turtle.attack()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Attempts to attack above the turtle.
        /// </summary>
        /// <returns>boolean whether the turtle succeeded in attacking upwards</returns>
        public virtual bool attackUp()
        {
            string reason = "";
            return attackUp(out reason);
        }

        /// <summary>
        /// Attempts to attack above the turtle.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean whether the turtle succeeded in attacking upwards</returns>
        public virtual bool attackUp(out string Reason)
        {
            string result = Send("turtle.attackUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Attempts to attack below the turtle.
        /// </summary>
        /// <returns>boolean whether the turtle succeeded in attacking downwards</returns>
        public virtual bool attackDown()
        {
            string reason = "";
            return attackDown(out reason);
        }

        /// <summary>
        /// Attempts to attack below the turtle.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean whether the turtle succeeded in attacking downwards</returns>
        public virtual bool attackDown(out string Reason)
        {
            string result = Send("turtle.attackDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Attempts to dig the block in front of the turtle. <para />
        /// If successful, suck() is automatically called, placing the item in turtle
        /// inventory in the selected slot if possible (block type matches and the slot
        /// is not a full stack yet), or in the next available slot.
        /// </summary>
        /// <returns>boolean whether the turtle succeeded in digging</returns>
        public virtual bool dig()
        {
            string reason = "";
            return dig(out reason);
        }

        /// <summary>
        /// Attempts to dig the block in front of the turtle. <para />
        /// If successful, suck() is automatically called, placing the item in turtle
        /// inventory in the selected slot if possible (block type matches and the slot
        /// is not a full stack yet), or in the next available slot.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean whether the turtle succeeded in digging</returns>
        public virtual bool dig(out string Reason)
        {
            string result = Send("turtle.dig()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Attempts to dig the block above of the turtle. <para />
        /// If successful, suck() is automatically called, placing the item in turtle
        /// inventory in the selected slot if possible (block type matches and the slot
        /// is not a full stack yet), or in the next available slot.
        /// </summary>
        /// <returns>boolean whether the turtle succeeded in digging</returns>
        public virtual bool digUp()
        {
            string reason = "";
            return digUp(out reason);
        }

        /// <summary>
        /// Attempts to dig the block above of the turtle. <para />
        /// If successful, suck() is automatically called, placing the item in turtle
        /// inventory in the selected slot if possible (block type matches and the slot
        /// is not a full stack yet), or in the next available slot.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean whether the turtle succeeded in digging</returns>
        public virtual bool digUp(out string Reason)
        {
            string result = Send("turtle.digUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Attempts to dig the block below of the turtle. <para />
        /// If successful, suck() is automatically called, placing the item in turtle
        /// inventory in the selected slot if possible (block type matches and the slot
        /// is not a full stack yet), or in the next available slot.
        /// </summary>
        /// <returns>boolean whether the turtle succeeded in digging</returns>
        public virtual bool digDown()
        {
            string reason = "";
            return digDown(out reason);
        }

        /// <summary>
        /// Attempts to dig the block below of the turtle. <para />
        /// If successful, suck() is automatically called, placing the item in turtle 
        /// inventory in the selected slot if possible (block type matches and the slot 
        /// is not a full stack yet), or in the next available slot.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean whether the turtle succeeded in digging</returns>
        public virtual bool digDown(out string Reason)
        {
            string result = Send("turtle.digDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Places the selected block in front of the Turtle.
        /// </summary>
        /// <returns>boolean if the blockplacement was succesful</returns>
        public virtual bool place()
        {
            string reason = "";
            return place(out reason);
        }

        /// <summary>
        /// Places the selected block in front of the Turtle.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean if the blockplacement was succesful</returns>
        public virtual bool place(out string Reason)
        {
            string result = Send("turtle.place()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Places the selected block in front of the Turtle. 
        /// This method is only needed, if you place a sign.
        /// </summary>
        /// <param name="signText">Text of sign. New line can be achived by using \n character</param>
        /// <returns>boolean if the blockplacement was succesful</returns>
        public virtual bool place(string signText)
        {
            string reason = "";
            return place(signText, out reason);
        }

        /// <summary>
        /// Places the selected block in front of the Turtle. 
        /// This method is only needed, if you place a sign.
        /// </summary>
        /// <param name="signText">Text of sign. New line can be achived by using \n character</param>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean if the blockplacement was succesful</returns>
        public virtual bool place(string signText, out string Reason)
        {
            string result = Send("turtle.place(" + signText + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }


        /// <summary>
        /// Places the selected block above of the Turtle.
        /// </summary>
        /// <returns>boolean if the blockplacement was succesful</returns>
        public virtual bool placeUp()
        {
            string reason = "";
            return placeUp(out reason);
        }
        /// <summary>
        /// Places the selected block above of the Turtle.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean if the blockplacement was succesful</returns>
        public virtual bool placeUp(out string Reason)
        {
            string result = Send("turtle.placeUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Places the selected block below of the Turtle.
        /// </summary>
        /// <returns>boolean if the blockplacement was succesful</returns>
        public virtual bool placeDown()
        {
            string reason = "";
            return placeDown(out reason);
        }
        /// <summary>
        /// Places the selected block below of the Turtle.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean if the blockplacement was succesful</returns>
        public virtual bool placeDown(out string Reason)
        {
            string result = Send("turtle.placeDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Detects if there is a Block in front. Does not detect mobs or liquids or floating items.
        /// </summary>
        /// <returns>boolean if the turtle has detected a block</returns>
        public virtual bool detect()
        {
            string reason = "";
            return detect(out reason);
        }

        /// <summary>
        /// Detects if there is a Block in front. Does not detect mobs or liquids or floating items.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean if the turtle has detected a block</returns>
        public virtual bool detect(out string Reason)
        {
            string result = Send("turtle.detect()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Detects if there is a Block above. Does not detect mobs or liquids or floating items.
        /// </summary>
        /// <returns>boolean if the turtle has detected a block</returns>
        public virtual bool detectUp()
        {
            string reason = "";
            return detectUp(out reason);
        }

        /// <summary>
        /// Detects if there is a Block above. Does not detect mobs or liquids or floating items.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean if the turtle has detected a block</returns>
        public virtual bool detectUp(out string Reason)
        {
            string result = Send("turtle.detectUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Detects if there is a Block below. Does not detect mobs or liquids or floating items.
        /// </summary>
        /// <returns>boolean if the turtle has detected a block</returns>
        public virtual bool detectDown()
        {
            string reason = "";
            return detectDown(out reason);
        }

        /// <summary>
        /// Detects if there is a Block below. Does not detect mobs or liquids or floating items.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean if the turtle has detected a block</returns>
        public virtual bool detectDown(out string Reason)
        {
            string result = Send("turtle.detectDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }


        /// <summary>
        /// Detects if the block in front is the same as the one in the selected Slot.
        /// </summary>
        /// <returns>boolean if the block in front is the same as the one in the selected Slot.</returns>
        public virtual bool compare()
        {
            string result = Send("turtle.compare()");
            return GetBool(result);
        }

        /// <summary>
        /// Detects if the block above is the same as the one in the selected Slot.
        /// </summary>
        /// <returns>boolean if the block above is the same as the one in the selected Slot.</returns>
        public virtual bool compareUp()
        {
            string result = Send("turtle.compareUp()");
            return GetBool(result);
        }

        /// <summary>
        /// Detects if the block below is the same as the one in the selected Slot.
        /// </summary>
        /// <returns>boolean if the block below is the same as the one in the selected Slot.</returns>
        public virtual bool compareDown()
        {
            string result = Send("turtle.compareDown()");
            return GetBool(result);
        }

        /// <summary>
        /// Detects if the block in the specified slot is the same as the one in the selected Slot.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns>boolean true if the selected item matches the one in the specified slot.</returns>
        public virtual bool compareTo(byte slot)
        {
            string result = Send("turtle.compareTo(" + slot + ")");
            return GetBool(result);
        }

        /// <summary>
        /// Drops all items off the selected slot in front of the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that block, 
        /// the items go to that inventory instead. Then the items will be placed in the first available 
        /// slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool drop()
        {
            string reason = "";
            return drop(out reason);
        }

        /// <summary>
        /// Drops all items off the selected slot in front of the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that block, 
        /// the items go to that inventory instead. Then the items will be placed in the first available 
        /// slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool drop(out string Reason)
        {
            string result = Send("turtle.drop()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Drops the given amount of items from the current slot in front of the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that block, 
        /// the items go to that inventory instead. Then the items will be placed in the first 
        /// available slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <param name="amount">the amount to drop</param>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool drop(byte amount)
        {
            string reason = "";
            return drop(amount, out reason);
        }

        /// <summary>
        /// Drops the given amount of items from the current slot in front of the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that 
        /// block, the items go to that inventory instead. Then the items will be placed in the first 
        /// available slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <param name="amount">the amount to drop</param>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool drop(byte amount, out string Reason)
        {
            string result = Send("turtle.drop(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Drops all items off the selected slot above the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that block, 
        /// the items go to that inventory instead. Then the items will be placed in the first available 
        /// slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool dropUp()
        {
            string reason = "";
            return dropUp(out reason);
        }
        /// <summary>
        /// Drops all items off the selected slot above the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that block, 
        /// the items go to that inventory instead. Then the items will be placed in the first available 
        /// slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool dropUp(out string Reason)
        {
            string result = Send("turtle.dropUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Drops the given amount of items from the current slot above the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that block, 
        /// the items go to that inventory instead. Then the items will be placed in the first 
        /// available slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <param name="amount">the amount to drop</param>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool dropUp(byte amount)
        {
            string reason = "";
            return dropUp(amount, out reason);
        }
        /// <summary>
        /// Drops the given amount of items from the current slot above the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that 
        /// block, the items go to that inventory instead. Then the items will be placed in the first 
        /// available slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <param name="amount">the amount to drop</param>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool dropUp(byte amount, out string Reason)
        {
            string result = Send("turtle.dropUp(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Drops all items off the selected slot below the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that block, 
        /// the items go to that inventory instead. Then the items will be placed in the first available 
        /// slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool dropDown()
        {
            string reason = "";
            return dropDown(out reason);
        }
        /// <summary>
        /// Drops all items off the selected slot below the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that block, 
        /// the items go to that inventory instead. Then the items will be placed in the first available 
        /// slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool dropDown(out string Reason)
        {
            string result = Send("turtle.dropDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        /// <summary>
        /// Drops the given amount of items from the current slot below the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that block, 
        /// the items go to that inventory instead. Then the items will be placed in the first 
        /// available slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <param name="amount">the amount to drop</param>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool dropDown(byte amount)
        {
            string reason = "";
            return dropDown(amount, out reason);
        }
        /// <summary>
        /// Drops the given amount of items from the current slot below the turtle. <para />
        /// The items are dropped on the ground by default, but if there is an inventory in that 
        /// block, the items go to that inventory instead. Then the items will be placed in the first 
        /// available slot of the inventory, starting at the top left, moving right and then down.
        /// </summary>
        /// <param name="amount">the amount to drop</param>
        /// <param name="Reason">reason of failure</param>
        /// <returns>boolean true if an item was dropped; false otherwise.</returns>
        public virtual bool dropDown(byte amount, out string Reason)
        {
            string result = Send("turtle.dropDown(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Moves one or more items from either the ground in front of the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly in front of the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly in front of the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// </summary>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suck()
        {
            string reason = "";
            return suck(out reason);
        }

        /// <summary>
        /// Moves one or more items from either the ground in front of the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly in front of the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly in front of the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// </summary>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suck(out string Reason)
        {
            string result = Send("turtle.suck()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Moves one or more items from either the ground in front of the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly in front of the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly in front of the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// • As of ComputerCraft 1.6 the turtle will attempt to pick up at most the specified number of items. Earlier builds always attempt to pick up a full slot.
        /// </summary>
        /// <param name="amount">the amount of items to suck up</param>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suck(byte amount)
        {
            string reason = "";
            return suck(amount, out reason);
        }

        /// <summary>
        /// Moves one or more items from either the ground in front of the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly in front of the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly in front of the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// • As of ComputerCraft 1.6 the turtle will attempt to pick up at most the specified number of items. Earlier builds always attempt to pick up a full slot.
        /// </summary>
        /// <param name="amount">the amount of items to suck up</param>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suck(byte amount, out string Reason)
        {
            string result = Send("turtle.suck(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Moves one or more items from either the ground above the turtle, or, from an inventory-enabled block (such as a chest) above the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly above the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly above the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// </summary>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suckUp()
        {
            string reason = "";
            return suckUp(out reason);
        }

        /// <summary>
        /// Moves one or more items from either the ground above the turtle, or, from an inventory-enabled block (such as a chest) above the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly above the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly above the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// </summary>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suckUp(out string Reason)
        {
            string result = Send("turtle.suckUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Moves one or more items from either the ground above the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly above the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly above the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// • As of ComputerCraft 1.6 the turtle will attempt to pick up at most the specified number of items. Earlier builds always attempt to pick up a full slot.
        /// </summary>
        /// <param name="amount">the amount of items to suck up</param>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suckUp(byte amount)
        {
            string reason = "";
            return suckUp(amount, out reason);
        }

        /// <summary>
        /// Moves one or more items from either the ground above the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly above the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly above the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// • As of ComputerCraft 1.6 the turtle will attempt to pick up at most the specified number of items. Earlier builds always attempt to pick up a full slot.
        /// </summary>
        /// <param name="amount">the amount of items to suck up</param>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suckUp(byte amount, out string Reason)
        {
            string result = Send("turtle.suckUp(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Moves one or more items from either the ground below the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly below the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly below the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// </summary>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suckDown()
        {
            string reason = "";
            return suckDown(out reason);
        }

        /// <summary>
        /// Moves one or more items from either the ground below the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly below the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly below the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// </summary>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suckDown(out string Reason)
        {
            string result = Send("turtle.suckDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Moves one or more items from either the ground below the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly below the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly below the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// • As of ComputerCraft 1.6 the turtle will attempt to pick up at most the specified number of items. Earlier builds always attempt to pick up a full slot.
        /// </summary>
        /// <param name="amount">the amount of items to suck up</param>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suckDown(byte amount)
        {
            string reason = "";
            return suckDown(amount, out reason);
        }

        /// <summary>
        /// Moves one or more items from either the ground below the turtle, or, from an inventory-enabled block (such as a chest) in front of the turtle to the turtles inventory.<para />
        /// • If an item is in the square directly below the turtle, it picks up one of those items.<para />
        /// • If a chest is in the square directly below the turtle, it picks up the items from the first non-empty chest slot, moving from top left to bottom right.The items are moved into the currently selected turtle slot if there is room.<para />
        /// • If the currently selected turtle slot is filled up before all of the items are picked up, the remaining picked up items are put in the next available turtle slot.<para />
        /// • If the currently selected turtle slot is 16 and the next slot is required, it will loop around and try turtle slot 1, and so on.<para />
        /// • As of ComputerCraft 1.6 the turtle will attempt to pick up at most the specified number of items. Earlier builds always attempt to pick up a full slot.
        /// </summary>
        /// <param name="amount">the amount of items to suck up</param>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean true if at least one item was moved into the turtle's inventory; false otherwise.</returns>
        public virtual bool suckDown(byte amount, out string Reason)
        {
            string result = Send("turtle.suckDown(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// If the currently selected slot contains fuel items, it will consume all of them to give the turtle the ability to move. If the current slot doesn't contain fuel items, it returns false.<para />
        /// • With ComputerCraft 1.6 or greater the turtles have a limited amount of storable full and trying to get over the limit will destroy the fuel. There will be no error returned.
        /// </summary>
        /// <returns>boolean true if fueled, else false.</returns>
        public virtual bool refuel()
        {
            string reason = "";
            return suckDown(out reason);
        }

        /// <summary>
        /// If the currently selected slot contains fuel items, it will consume all of them to give the turtle the ability to move. If the current slot doesn't contain fuel items, it returns false.<para />
        /// • With ComputerCraft 1.6 or greater the turtles have a limited amount of storable full and trying to get over the limit will destroy the fuel. There will be no error returned.
        /// </summary>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean true if fueled, else false.</returns>
        public virtual bool refuel(out string Reason)
        {
            string result = Send("turtle.refuel()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// If the currently selected slot contains fuel items, it will consume the given amount of them to give the turtle the ability to move. If the current slot doesn't contain fuel items, it returns false.<para />
        /// • With ComputerCraft 1.6 or greater the turtles have a limited amount of storable full and trying to get over the limit will destroy the fuel. There will be no error returned.
        /// </summary>
        /// <param name="quantity">the amount of items to be consumed as fuel</param>
        /// <returns>boolean true if fueled, else false.</returns>
        public virtual bool refuel(byte quantity)
        {
            string reason = "";
            return suckDown(quantity, out reason);
        }

        /// <summary>
        /// If the currently selected slot contains fuel items, it will consume the given amount of them to give the turtle the ability to move. If the current slot doesn't contain fuel items, it returns false.<para />
        /// • With ComputerCraft 1.6 or greater the turtles have a limited amount of storable full and trying to get over the limit will destroy the fuel. There will be no error returned.
        /// </summary>
        /// <param name="quantity">the amount of items to be consumed as fuel</param>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean true if fueled, else false.</returns>
        public virtual bool refuel(byte quantity, out string Reason)
        {
            string result = Send("turtle.refuel(" + quantity + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Returns the amount of fuel inside the turtle.
        /// </summary>
        /// <returns>amount of fuel inside the turtle</returns>
        public virtual int getFuelLevel()
        {
            string result = Send("turtle.getFuelLevel()");
            return GetInt(result);
        }

        /// <summary>
        /// This command returns the maximum amount of fuel a turtle may store. By default, a regular turtle may hold 20,000 units, and an advanced model 100,000 units. <para />
        /// • Requires ComputerCraft 1.6 or greater, otherwise the turtles store unlimited amount of fuel!
        /// </summary>
        /// <returns></returns>
        public virtual int getFuelLimit()
        {
            string result = Send("turtle.getFuelLimit()");
            return GetInt(result);
        }

        /// <summary>
        /// Transfers all the items from the current selected slot to the given slot. 
        /// If the targeted slot already has an item of a different type in it, it will return false.
        /// If only a few items can be transfered, the method will returns true and fills the slot as
        /// far as it goes.
        /// </summary>
        /// <param name="slot">the slot number, to which the items should be transfered</param>
        /// <returns>boolean success if any item could be transfered, otherwise false</returns>
        public virtual bool transferTo(byte slot)
        {
            string reason = "";
            return transferTo(slot, out reason);
        }

        /// <summary>
        /// Transfers all the items from the current selected slot to the given slot. 
        /// If the targeted slot already has an item of a different type in it, it will return false.
        /// If only a few items can be transfered, the method will returns true and fills the slot as
        /// far as it goes.
        /// </summary>
        /// <param name="slot">the slot number, to which the items should be transfered</param>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean success if any item could be transfered, otherwise false</returns>
        public virtual bool transferTo(byte slot, out string Reason)
        {
            string result = Send("turtle.transferTo(" + slot + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Transfers the given amount the items from the current selected slot to the given slot. 
        /// If the targeted slot already has an item of a different type in it, it will return false.
        /// If only a few items can be transfered, the method will returns true and fills the slot as
        /// far as it goes or the quantity is reached.
        /// </summary>
        /// <param name="slot">the slot number, to which the items should be transfered</param>
        /// <param name="quantity">the amount to be transfered</param>
        /// <returns>boolean success if any item could be transfered, otherwise false</returns>
        public virtual bool transferTo(byte slot, byte quantity)
        {
            string reason = "";
            return transferTo(slot, quantity, out reason);
        }

        /// <summary>
        /// Transfers the given amount the items from the current selected slot to the given slot. 
        /// If the targeted slot already has an item of a different type in it, it will return false.
        /// If only a few items can be transfered, the method will returns true and fills the slot as
        /// far as it goes or the quantity is reached.
        /// </summary>
        /// <param name="slot">the slot number, to which the items should be transfered</param>
        /// <param name="quantity">the amount to be transfered</param>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean success if any item could be transfered, otherwise false</returns>
        public virtual bool transferTo(byte slot, byte quantity, out string Reason)
        {
            string result = Send("turtle.transferTo(" + slot + "," + quantity + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Returns the ID string and metadata of the block in front of the Turtle in an array format: { name = "modname:blockname", metadata, state}.<para />
        /// • The "state" string requires at least Minecraft 1.8+, and isn't implemented yet.
        /// </summary>
        /// <returns>boolean if there was a inspectable block present</returns>
        public virtual bool inspect()
        {
            string[] reason;
            return inspect(out reason);
        }
        /// <summary>
        /// Returns the ID string and metadata of the block in front of the Turtle in an array format: { name = "modname:blockname", metadata, state}.<para />
        /// • The "state" string requires at least Minecraft 1.8+, and isn't implemented yet.
        /// </summary>
        /// <param name="Reason">the reason of failur</param>
        /// <returns>boolean if there was a inspectable block present</returns>
        public virtual bool inspect(out string[] Reason)
        {
            string result = Send("turtle.inspect()");
            Reason = GetArray(result, 1);
            return GetBool(result);
        }

        /// <summary>
        /// Returns the ID string and metadata of the block below the Turtle in an array format: { name = "modname:blockname", metadata, state}.<para />
        /// • The "state" string requires at least Minecraft 1.8+, and isn't implemented yet.
        /// </summary>
        /// <returns>boolean if there was a inspectable block present</returns>
        public virtual bool inspectDown()
        {
            string[] reason;
            return inspectDown(out reason);
        }
        /// <summary>
        /// Returns the ID string and metadata of the block below the Turtle in an array format: { name = "modname:blockname", metadata, state}.<para />
        /// • The "state" string requires at least Minecraft 1.8+, and isn't implemented yet.
        /// </summary>
        /// <param name="Reason">the reason of failure</param>
        /// <returns>boolean if there was a inspectable block present</returns>
        public virtual bool inspectDown(out string[] Reason)
        {
            string result = Send("turtle.inspectDown()");
            Reason = GetArray(result, 1);
            return GetBool(result);
        }

        /// <summary>
        /// Returns the ID string and metadata of the block above the Turtle in an array format: { name = "modname:blockname", metadata, state}.<para />
        /// • The "state" string requires at least Minecraft 1.8+, and isn't implemented yet.
        /// </summary>
        /// <returns>boolean if there was a inspectable block present</returns>
        public virtual bool inspectUp()
        {
            string[] reason;
            return inspectUp(out reason);
        }
        /// <summary>
        /// Returns the ID string and metadata of the block above the Turtle in an array format: { name = "modname:blockname", metadata, state}.<para />
        /// • The "state" string requires at least Minecraft 1.8+, and isn't implemented yet.
        /// </summary>
        /// <param name="Reason">the reason of failur</param>
        /// <returns>boolean if there was a inspectable block present</returns>
        public virtual bool inspectUp(out string[] Reason)
        {
            string result = Send("turtle.inspectUp()");
            Reason = GetArray(result, 1);
            return GetBool(result);
        }

        /// <summary>
        /// Returns the ID string, count and damage values of the currently selected slot in an array format: { name = "modname:itemname", damage, count}. Returns nil if there is no item in the specified or currently selected slot.
        /// </summary>
        /// <returns>array of strings with name, damage and amount of item in the current slot</returns>

        public virtual string[] getItemDetail()
        {
            string result = Send("turtle.getItemDetail()");
            return GetArray(result, 0);
        }

        /// <summary>
        /// Returns the ID string, count and damage values of the given slot number in an array format: { name = "modname:itemname", damage, count}. Returns nil if there is no item in the specified or currently selected slot.
        /// </summary>
        /// <returns>array of strings with name, damage and amount of the item in the given slot</returns>

        public virtual  string[] getItemDetail(byte slotNum)
        {
            string result = Send("turtle.getItemDetail(" + slotNum + ")");
            return GetArray(result, 0);
        }
    }
}
