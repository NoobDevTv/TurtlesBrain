using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace turtleAPI
{
    public class Turtle : Computer
    {
        public Turtle(string label) : base(label)
        {
        }

        public bool down()
        {
            string reason = "";
            return down(out reason);
        }
        public bool down(out string Reason)
        {
            string result = Send("turtle.down()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool up()
        {
            string reason = "";
            return up(out reason);
        }
        public bool up(out string Reason)
        {
            string result = Send("turtle.up()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Try to move the turtle forward 
        /// </summary>
        /// <returns>boolean success</returns>
        public bool forward()
        {
            string reason = "";
            return forward(out reason);
        }
        public bool forward(out string Reason)
        {
            string result = Send("turtle.forward()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Try to move the turtle backward 
        /// </summary>
        /// <returns>boolean success </returns>
        public bool back()
        {
            string reason = "";
            return back(out reason);
        }
        public bool back(out string Reason)
        {
            string result = Send("turtle.back()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        /// <summary>
        /// Try to move the turtle up 
        /// </summary>
        /// <returns>boolean success </returns>
        public bool turnLeft()
        {
            string reason = "";
            return turnLeft(out reason);
        }
        public bool turnLeft(out string Reason)
        {
            string result = Send("turtle.turnLeft()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool turnRight()
        {
            string reason = "";
            return turnRight(out reason);
        }
        public bool turnRight(out string Reason)
        {
            string result = Send("turtle.turnRight()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool select(byte slot)
        {
            string reason = "";
            return select(slot, out reason);
        }
        public bool select(byte slot, out string Reason)
        {
            string result = Send("turtle.select(" + slot + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool select()
        {
            string reason = "";
            return select(out reason);
        }
        public bool select(out string Reason)
        {
            string result = Send("turtle.select()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public byte getSelectedSlot()
        {
            return GetByte(Send("turtle.getSelectedSlot()"));
        }

        /// <summary>
        /// Counts how many items are in the currently selected slot or, if specified, slotNum slot 
        /// </summary>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public bool getItemCount(byte slotNum)
        {
            string reason = "";
            return select(slotNum, out reason);
        }
        public byte getItemCount(byte slotNum, out string Reason)
        {
            string result = Send("turtle.getItemCount(" + slotNum + ")");
            Reason = GetReason(result);
            return GetByte(result);
        }
        public bool getItemCount()
        {
            string reason = "";
            return select(out reason);
        }
        public byte getItemCount(out string Reason)
        {
            string result = Send("turtle.getItemCount()");
            Reason = GetReason(result);
            return GetByte(result);
        }

        public bool getItemSpace()
        {
            string reason = "";
            return getItemSpace(out reason);
        }
        public bool getItemSpace(out string Reason)
        {
            string result = Send("turtle.getItemSpace()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool getItemSpace(byte slotNum)
        {
            string reason = "";
            return getItemSpace(slotNum, out reason);
        }
        public bool getItemSpace(byte slotNum, out string Reason)
        {
            string result = Send("turtle.getItemSpace(" + slotNum + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool equipLeft()
        {
            string reason = "";
            return equipLeft(out reason);
        }
        public bool equipLeft(out string Reason)
        {
            string result = Send("turtle.equipLeft()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool equipRight()
        {
            string reason = "";
            return equipRight(out reason);
        }
        public bool equipRight(out string Reason)
        {
            string result = Send("turtle.equipRight()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool attack()
        {
            string reason = "";
            return attack(out reason);
        }
        public bool attack(out string Reason)
        {
            string result = Send("turtle.attack()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool attackUp()
        {
            string reason = "";
            return attackUp(out reason);
        }
        public bool attackUp(out string Reason)
        {
            string result = Send("turtle.attackUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool attackDown()
        {
            string reason = "";
            return attackDown(out reason);
        }
        public bool attackDown(out string Reason)
        {
            string result = Send("turtle.attackDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool dig()
        {
            string reason = "";
            return dig(out reason);
        }
        public bool dig(out string Reason)
        {
            string result = Send("turtle.dig()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool digUp()
        {
            string reason = "";
            return digUp(out reason);
        }
        public bool digUp(out string Reason)
        {
            string result = Send("turtle.digUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool digDown()
        {
            string reason = "";
            return digDown(out reason);
        }
        public bool digDown(out string Reason)
        {
            string result = Send("turtle.digDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool place()
        {
            string reason = "";
            return place(out reason);
        }
        public bool place(out string Reason)
        {
            string result = Send("turtle.place()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool place(string signText)
        {
            string reason = "";
            return place(signText, out reason);
        }
        public bool place(string signText, out string Reason)
        {
            string result = Send("turtle.place(" + signText + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool placeUp()
        {
            string reason = "";
            return placeUp(out reason);
        }
        public bool placeUp(out string Reason)
        {
            string result = Send("turtle.placeUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool placeDown()
        {
            string reason = "";
            return placeDown(out reason);
        }
        public bool placeDown(out string Reason)
        {
            string result = Send("turtle.placeDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool detect()
        {
            string reason = "";
            return detect(out reason);
        }
        public bool detect(out string Reason)
        {
            string result = Send("turtle.detect()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool detectUp()
        {
            string reason = "";
            return detectUp(out reason);
        }
        public bool detectUp(out string Reason)
        {
            string result = Send("turtle.detectUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool detectDown()
        {
            string reason = "";
            return detectDown(out reason);
        }
        public bool detectDown(out string Reason)
        {
            string result = Send("turtle.detectDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool compare()
        {
            string reason = "";
            return compare(out reason);
        }
        public bool compare(out string Reason)
        {
            string result = Send("turtle.compare()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool compareUp()
        {
            string reason = "";
            return compareUp(out reason);
        }
        public bool compareUp(out string Reason)
        {
            string result = Send("turtle.compareUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool compareDown()
        {
            string reason = "";
            return compareDown(out reason);
        }
        public bool compareDown(out string Reason)
        {
            string result = Send("turtle.compareDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool compareTo()
        {
            string reason = "";
            return compareTo(out reason);
        }
        public bool compareTo(out string Reason)
        {
            string result = Send("turtle.compareTo()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool compareTo(byte slot)
        {
            string reason = "";
            return compareTo(slot, out reason);
        }
        public bool compareTo(byte slot, out string Reason)
        {
            string result = Send("turtle.compareTo(" + slot + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool drop()
        {
            string reason = "";
            return drop(out reason);
        }
        public bool drop(out string Reason)
        {
            string result = Send("turtle.drop()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool drop(byte amount)
        {
            string reason = "";
            return drop(amount, out reason);
        }
        public bool drop(byte amount, out string Reason)
        {
            string result = Send("turtle.drop(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool dropUp()
        {
            string reason = "";
            return dropUp(out reason);
        }
        public bool dropUp(out string Reason)
        {
            string result = Send("turtle.dropUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool dropUp(byte amount)
        {
            string reason = "";
            return dropUp(amount, out reason);
        }
        public bool dropUp(byte amount, out string Reason)
        {
            string result = Send("turtle.dropUp(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool dropDown()
        {
            string reason = "";
            return dropDown(out reason);
        }
        public bool dropDown(out string Reason)
        {
            string result = Send("turtle.dropDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool dropDown(byte amount)
        {
            string reason = "";
            return dropDown(amount, out reason);
        }
        public bool dropDown(byte amount, out string Reason)
        {
            string result = Send("turtle.dropDown(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool suck()
        {
            string reason = "";
            return suck(out reason);
        }
        public bool suck(out string Reason)
        {
            string result = Send("turtle.suck()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool suck(byte amount)
        {
            string reason = "";
            return suck(amount, out reason);
        }
        public bool suck(byte amount, out string Reason)
        {
            string result = Send("turtle.suck(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool suckUp()
        {
            string reason = "";
            return suckUp(out reason);
        }
        public bool suckUp(out string Reason)
        {
            string result = Send("turtle.suckUp()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool suckUp(byte amount)
        {
            string reason = "";
            return suckUp(amount, out reason);
        }
        public bool suckUp(byte amount, out string Reason)
        {
            string result = Send("turtle.suckUp(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool suckDown()
        {
            string reason = "";
            return suckDown(out reason);
        }
        public bool suckDown(out string Reason)
        {
            string result = Send("turtle.suckDown()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool suckDown(byte amount)
        {
            string reason = "";
            return suckDown(amount, out reason);
        }
        public bool suckDown(byte amount, out string Reason)
        {
            string result = Send("turtle.suckDown(" + amount + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public bool refuel()
        {
            string reason = "";
            return suckDown(out reason);
        }
        public bool refuel(out string Reason)
        {
            string result = Send("turtle.refuel()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool refuel(byte quantity)
        {
            string reason = "";
            return suckDown(quantity, out reason);
        }
        public bool refuel(byte quantity, out string Reason)
        {
            string result = Send("turtle.refuel(" + quantity + ")");
            Reason = GetReason(result);
            return GetBool(result);
        }

        public int getFuelLevel()
        {
            string reason = "";
            return getFuelLevel(out reason);
        }
        public int getFuelLevel(out string Reason)
        {
            string result = Send("turtle.getFuelLevel()");
            Reason = GetReason(result);
            return GetInt(result);
        }
        public int getFuelLimit()
        {
            string reason = "";
            return getFuelLimit(out reason);
        }
        public int getFuelLimit(out string Reason)
        {
            string result = Send("turtle.getFuelLimit()");
            Reason = GetReason(result);
            return GetInt(result);
        }

        public bool transferTo(byte slot)
        {
            string reason = "";
            return transferTo(slot, out reason);
        }
        public bool transferTo(byte slot, out string Reason)
        {
            string result = Send("turtle.transferTo()");
            Reason = GetReason(result);
            return GetBool(result);
        }
        public bool transferTo(byte slot, byte quantity)
        {
            string reason = "";
            return transferTo(slot, quantity, out reason);
        }
        public bool transferTo(byte slot, byte quantity, out string Reason)
        {
            string result = Send("turtle.transferTo()");
            Reason = GetReason(result);
            return GetBool(result);
        }
    }
}
