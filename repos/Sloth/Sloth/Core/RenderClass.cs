using ClickableTransparentOverlay;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sloth.Core
{
    public class RenderClass : Overlay
    {
        Vector2 size = new Vector2(600, 400);
        bool aimbot = false;
        protected override void Render()
        {
            ImGuiStylePtr style = ImGui.GetStyle();
            ImGui.SetWindowSize(size);
            style.Alpha = 0.8f;
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.09469f, 0.09469f, 0.09469f, 1f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.09469f, 0.09469f, 0.09469f, 1f);

            ImGui.Begin("Janela", ImGuiWindowFlags.NoResize);
            ImGui.Checkbox("Aimbot", ref aimbot);
            ImGui.End();
        }
    }
}
