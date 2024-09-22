using ClickableTransparentOverlay;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Greed.Core
{
    internal class Cheese : Overlay
    {
        Vector2 size = new Vector2(400, 250);
        protected override void Render()
        {
            ImGui.SetNextWindowSize(size);
            ImGuiStylePtr style = ImGui.GetStyle();
            style.Alpha = 1f;
            style.WindowRounding = 6f;
            style.FrameRounding = 6f;
            style.Colors[(int)ImGuiCol.Border] = new Vector4(4, 2, 3, 1);

            ImGui.Begin("Greed", ImGuiWindowFlags.NoResize);
            ImGui.Text(DateTime.Now.ToString());

            ImGui.End();
        }
    }
}
