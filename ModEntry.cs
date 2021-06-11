using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Characters;
using StardewValley.Network;
using StardewValley.Projectiles;

namespace MdzsWeapons
{
    public class ModEntry : Mod
    {
        /*********
** Public methods
*********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.Player.LevelChanged += this.OnLevelChanged;
            helper.Events.Player.InventoryChanged += this.onInventoryChanged;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnLevelChanged(object sender, LevelChangedEventArgs e)
        {
            Log($"Skill leveled: {e.Skill.ToString()}");
            if (e.Skill.ToString() != "Combat")
            {
                return;
            }

            Log("level up combat! search for chenqing");
            foreach (Item i in Game1.player.Items)
            {
                if(!(i is null)) { 
                Log($"checking {i.Name}");
                    if (i.Name.StartsWith("Chenqing_", StringComparison.Ordinal))
                    {
                        Log($"Found {i.Name}");
                        if (int.Parse(i.Name.Substring(i.Name.Length - 1)) + 1 != e.NewLevel)
                        {
                            JAInterface jAInterface = Helper.ModRegistry.GetApi<JAInterface>("spacechase0.JsonAssets");
                            if (jAInterface != null)
                            {
                                Game1.player.removeItemFromInventory(i);
                                Game1.player.addItemToInventory(new StardewValley.Tools.MeleeWeapon(jAInterface.GetWeaponId($"Chenqing_{e.NewLevel-1}")));
                            }

                        }
                    }
                }
            }
        }


        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;
            Item item = Game1.player.CurrentItem;
            if (!(item is null) && item.Name.StartsWith("Chenqing_", StringComparison.Ordinal) && e.Button.ToString() == "MouseRight")
            {
                this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
                //Taken from Object.OnPerformUseAction
                Game1.player.faceDirection(2);
                Game1.soundBank.PlayCue("horse_flute");
                Game1.player.FarmerSprite.animateOnce(new FarmerSprite.AnimationFrame[6] {
                    new FarmerSprite.AnimationFrame (98, 400, true, false, null, false),
                    new FarmerSprite.AnimationFrame (99, 200, true, false, null, false),
                    new FarmerSprite.AnimationFrame (100, 200, true, false, null, false),
                    new FarmerSprite.AnimationFrame (99, 200, true, false, null, false),
                    new FarmerSprite.AnimationFrame (98, 400, true, false, null, false),
                    new FarmerSprite.AnimationFrame (99, 200, true, false, null, false)
                }, null);
                Game1.player.freezePause = 1500;
                //StardewValley.Tools.MeleeWeapon chenqing = (StardewValley.Tools.MeleeWeapon)item;
                //chenqing.isOnSpecial = true;
                //chenqing.triggerDefenseSwordFunction(Game1.player);
                Game1.player.currentLocation.playSound("ghost", NetAudio.SoundContext.Default);
                BasicProjectile basicProjectile = new BasicProjectile(30, 0, 0, 0, 0f, -1f, -1f, Game1.player.position); //, "", "", false, false, base.currentLocation, this, false, null);
                Game1.player.currentLocation.projectiles.Add(basicProjectile);
                /*//basicProjectile.startingRotation.Value = 0;
                basicProjectile.height.Value = 24f;
                //basicProjectile.debuff.Value = null;
                basicProjectile.ignoreTravelGracePeriod.Value = true;
                basicProjectile.IgnoreLocationCollision = true;
                basicProjectile.maxTravelDistance.Value = 64 * projectileRange;
                //                  location.projectiles.Add (new BasicProjectile ((int)(num2 * (float)(num + Game1.random.Next (-(num / 2), num + 2)) * (1f + who.attackIncreaseModifier)), @object.ParentSheetIndex, 0, 0, (float)(3.1415926535897931 / (double)(64f + (float)Game1.random.Next (-63, 64))), 0f - velocityTowardPoint.X, 0f - velocityTowardPoint.Y, shootOrigin - new Vector2 (32f, 32f), collisionSound, "", false, true, location, who, true, collisionBehavior) {

                Game1.player.currentLocation.projectiles.Add(basicProjectile); */
                //Vector2 velocity = TranslateVector(new Vector2(3, 3), Game1.player.FacingDirection);
                //Vector2 startPos = TranslateVector(new Vector2(0, 0), Game1.player.FacingDirection);
                //Game1.player.currentLocation.projectiles.Add(new BasicProjectile(30, 0, 0, 0, 0, 3, 3, Game1.player.Position + new Vector2(0, -64) + startPos));

            }
        }
        private void onInventoryChanged(object sender, InventoryChangedEventArgs e)
        {
            if (!e.IsLocalPlayer)
                return;
            foreach (Item i in e.Added)
                if (i.Name.StartsWith("Chenqing_", StringComparison.Ordinal))
                {
                    Log($"Found {i.Name} with lvl{int.Parse(i.Name.Substring(i.Name.Length - 1))} vs player combat lvl{Game1.player.CombatLevel}");
                    if (int.Parse(i.Name.Substring(i.Name.Length - 1)) + 1 != Game1.player.CombatLevel)
                    {
                        JAInterface jAInterface = Helper.ModRegistry.GetApi<JAInterface>("spacechase0.JsonAssets");
                        if (jAInterface != null)
                        {
                            Game1.player.removeItemFromInventory(i);
                            Game1.player.addItemToInventory(new StardewValley.Tools.MeleeWeapon(jAInterface.GetWeaponId($"Chenqing_{Game1.player.CombatLevel-1}")));
                        }

                    }
                }
        }
        private void Log(String msg)
        {
            Game1.addHUDMessage(new HUDMessage(msg));
            this.Monitor.Log(msg, LogLevel.Debug);
        }



    }
}
