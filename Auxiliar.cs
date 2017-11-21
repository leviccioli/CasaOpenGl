using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Casa
{
    class Auxiliar
    {
        public void horizontal(int x, int y, int z, int cx, int cy, Color cor)
        {
            GL.Color3(cor);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x + cx, y, z);
            GL.Vertex3(x + cx, y + cy, z);
            GL.Vertex3(x, y + cy, z);
            GL.End();
        }

        public void verticalx(int x, int y, int z, int cx, int cz, Color cor)
        {
            GL.Color3(cor);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x + cx, y, z);
            GL.Vertex3(x + cx, y, z + cz);
            GL.Vertex3(x, y, z + cz);
            GL.End();
        }
        public void verticaly(int x, int y, int z, int cy, int cz, Color cor)
        {
            GL.Color3(cor);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x, y + cy, z);
            GL.Vertex3(x, y + cy, z + cz);
            GL.Vertex3(x, y, z + cz);
            GL.End();
        }

        public void diagonalx(int x, int y, int z, int cx, int cy, int cz, Color cor)
        {
            GL.Color3(cor);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x + cx, y, z + cz);
            GL.Vertex3(x + cx, y + cy, z + cz);
            GL.Vertex3(x, y + cy, z);
            GL.End();
        }

        public void diagonaly(int x, int y, int z, int cx, int cy, int cz, Color cor)
        {
            GL.Color3(cor);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x, y + cy, z + cz);
            GL.Vertex3(x + cx, y + cy, z + cz);
            GL.Vertex3(x + cx, y, z);
            GL.End();
        }

        public void desenhaparede(int x, int y, int z, int cx, int cy, int cz, Color cor)
        {
            this.verticalx(x, y, z, cx, cz, cor);
            this.verticalx(x, y + 30, z, cx, cz, cor);
            this.horizontal(x, y, z, cx, cy, cor);
            this.horizontal(x, y, z + cz, cx, cy, cor);

        }
    }
}
