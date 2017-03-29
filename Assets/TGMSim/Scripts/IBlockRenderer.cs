using UnityEngine;

namespace TGMSim.Scripts
{
    public interface IBlockRenderer
    {
        void Add(Point point, Color color);
        void Render();
    }
}