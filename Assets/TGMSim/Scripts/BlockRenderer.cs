using System.Collections.Generic;
using UnityEngine;

namespace TGMSim.Scripts
{
    public class BlockRenderer : IBlockRenderer
    {
        private readonly Material _material;
        private readonly List<int> _indices = new List<int>();
        private readonly List<Vector3> _vertices = new List<Vector3>();
        private readonly List<Color> _colors = new List<Color>();
        private readonly Mesh _mesh = new Mesh();

        public BlockRenderer(Material material)
        {
            _material = material;
        }

        public void Add(Point point, Color color)
        {
            point.X -= 5;
            point.Y -= 10;

            const float size = 0.45f;
            var left = point.X - size;
            var right = point.X + size;
            var top = -point.Y - size;
            var bottom = -point.Y + size;

            var indexOffset = _vertices.Count;

            _vertices.Add(new Vector3(left, top));
            _vertices.Add(new Vector3(right, top));
            _vertices.Add(new Vector3(left, bottom));
            _vertices.Add(new Vector3(right, bottom));

            for (var i = 0; i < 4; ++i)
            {
                _colors.Add(color);
            }

            _indices.Add(indexOffset + 0);
            _indices.Add(indexOffset + 2);
            _indices.Add(indexOffset + 1);
            _indices.Add(indexOffset + 1);
            _indices.Add(indexOffset + 2);
            _indices.Add(indexOffset + 3);
        }

        public void Render()
        {
            _mesh.Clear();
            _mesh.SetVertices(_vertices);
            _mesh.SetTriangles(_indices, 0);
            _mesh.SetColors(_colors);

            Graphics.DrawMesh(_mesh, Matrix4x4.identity, _material, 0);

            _vertices.Clear();
            _indices.Clear();
            _colors.Clear();
        }
    }
}