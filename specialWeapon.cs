/*using System;
using System.Collections.Generic;
using System.Globalization;
using StardewValley.Tools;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Projectiles;
using StardewValley;

namespace MdzsWeapons
{
    public class SpecialWeapon : StardewValley.Tools.MeleeWeapon
    {
        public SpecialWeapon() : base() { }
        public SpecialWeapon(MeleeWeapon meleeWeapon) : base(meleeWeapon.InitialParentTileIndex)
        {
            
        }
        public new void triggerDefenseSwordFunction(Farmer who)
        {
            Game1.addHUDMessage(new HUDMessage("tDSF intercepted!"));
            base.triggerDefenseSwordFunction(who);
        }
        public override void actionWhenBeingHeld(Farmer who)
        {
            Game1.addHUDMessage(new HUDMessage("actionWhenBeingHeld intercepted!"));
            base.actionWhenBeingHeld(who);
        }
        public override void leftClick(Farmer who)
        {
            Game1.addHUDMessage(new HUDMessage("leftclick intercepted!"));
            base.leftClick(who);
        }

    }
}
*/