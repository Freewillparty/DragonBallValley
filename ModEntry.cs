using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Tools;

namespace DragonBallValley
{
    public class ModEntry : Mod
    {
        private bool rageTriggered;

        public override void Entry(IModHelper helper)
        {

            helper.Events.GameLoop.UpdateTicked += OnUpdatedTicked;

            helper.ConsoleCommands.Add("test_command", "A test command", (cmd, args) =>
            {
                
            });
        }

        public void OnUpdatedTicked(object? sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady || Game1.player == null) return;

            bool isLowHealth = Game1.player.health <= (Game1.player.maxHealth * 0.2);
            
            if(isLowHealth != this.rageTriggered)
            {
                MeleeWeapon zsword = GetMeleeWeapon("ZSword");

                this.rageTriggered = isLowHealth;

                if(this.rageTriggered)
                {
                    zsword.minDamage.Value = 75;
                    zsword.maxDamage.Value = 98;

                    zsword.appearance.Value = "(W)Freewillparty.DBVContent_ZSword_Rage";

                    this.Monitor.Log("Rage activated!", LogLevel.Info);
                }
                else
                {
                    zsword.minDamage.Value = 50;
                    zsword.maxDamage.Value = 65;

                    zsword.appearance.Value = null;
                }
            }
        }

        public MeleeWeapon GetMeleeWeapon(string itemId)
        {
            foreach(Item item in Game1.player.Items)
            {
                if(item is MeleeWeapon weapon && weapon.itemId.Contains(itemId))
                {
                    return weapon;
                }
            }

            this.Monitor.Log($"Item id: {itemId} has not been found.", LogLevel.Warn);
            return null;
        }
    }
}
