using ClickableTransparentOverlay;
using ImGuiNET;
using System.Net.Http.Headers;
using System.Numerics;

namespace Greed.Core
{
    public class Renderer : Overlay
    {
        Vector2 size = new Vector2(400, 256);
        public int maxHealth;
        public int health;
        public int potions;
        public int lastDamage;
        public double healthState;
        public bool aimbot = false;
        public bool autoNexus = false;
        public bool autoPot = false;
        public float speed;

        protected override void Render()
        {
            ImGui.SetNextWindowSize(size);
            ImGui.Begin("Bomboclat", ImGuiWindowFlags.NoResize);

            ImGui.Text($"{health.ToString()}/{maxHealth.ToString()} :: {(healthState * 100):0.00}/100.00");
            ImGui.Text(lastDamage.ToString());
            ImGui.Checkbox("AutoFire", ref aimbot);
            ImGui.Checkbox("Auto Nexus", ref autoNexus);
            ImGui.Checkbox("Auto Pot", ref autoPot);
            ImGui.SliderFloat("Speed", ref speed, 0, 60);

            if (healthState <= 0.2)
            {
                ImGui.NewLine();
                ImGui.Text("Below than 20% :: Returned to Nexus");
            }

            ImGui.End();
        }
    }
}
