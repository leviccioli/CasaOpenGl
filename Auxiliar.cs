using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Drawing;

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

        public void horizontal (int x, int y, int z, int cx, int cy, int textura)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textura);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex3(x, y, z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(x + cx, y, z);
            GL.TexCoord2(1, 0);
            GL.Vertex3(x + cx, y + cy, z);
            GL.TexCoord2(0, 0);
            GL.Vertex3(x, y + cy, z);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
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

        public void verticalx(int x, int y, int z, int cx, int cz, int textura)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textura);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex3(x, y, z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(x + cx, y, z);
            GL.TexCoord2(1, 0);
            GL.Vertex3(x + cx, y, z + cz);
            GL.TexCoord2(0, 0);
            GL.Vertex3(x, y, z + cz);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
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

        public void verticaly(int x, int y, int z, int cy, int cz, int textura)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textura);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex3(x, y, z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(x, y + cy, z);
            GL.TexCoord2(1, 0);
            GL.Vertex3(x, y + cy, z + cz);
            GL.TexCoord2(0, 0);
            GL.Vertex3(x, y, z + cz);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
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

        public void diagonalx(int x, int y, int z, int cx, int cy, int cz, int textura)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textura);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex3(x, y, z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(x + cx, y, z + cz);
            GL.TexCoord2(1, 0);
            GL.Vertex3(x + cx, y + cy, z + cz);
            GL.TexCoord2(0, 0);
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

        public void desenhaparedex(int x, int y, int z, int cx, int cz, int espessura, Color cor1, Color cor2, Color cor3)
        {
            this.verticalx(x, y, z, cx, cz, cor1);
            this.verticalx(x, y + espessura, z, cx, cz, cor2);
            this.horizontal(x, y, z, cx, espessura, cor3);
            this.horizontal(x, y, z + cz, cx, espessura, cor3);
            this.verticaly(x, y, z, espessura, cz, cor3);
            this.verticaly(x + cx, y, z, espessura, cz, cor3);
        }

        public void desenhaparedey(int x, int y, int z, int cy, int cz, int espessura, Color cor1, Color cor2, Color cor3)
        {
            this.verticaly(x, y, z, cy, cz, cor1);
            this.verticaly(x + espessura, y, z, cy, cz, cor2);
            this.horizontal(x, y, z, espessura, cy, cor3);
            this.horizontal(x, y, z + cz, espessura, cy, cor3);
            this.verticalx(x, y, z, espessura, cz, cor3);
            this.verticalx(x, y + cy, z, espessura, cz, cor3);
        }

        public void desenhaparedex(int x, int y, int z, int cx, int cz, int espessura, Color cor, int textura)
        {
            this.verticalx(x, y, z, cx, cz, textura);
            this.verticalx(x, y + espessura, z, cx, cz, textura);
            this.horizontal(x, y, z, cx, espessura, cor);
            this.horizontal(x, y, z + cz, cx, espessura, cor);
            this.verticaly(x, y, z, espessura, cz, cor);
            this.verticaly(x + cx, y, z, espessura, cz, cor);
        }

        public void desenhaparedey(int x, int y, int z, int cy, int cz, int espessura, Color cor, int textura)
        {
            this.verticaly(x, y, z, cy, cz, textura);
            this.verticaly(x + espessura, y, z, cy, cz, textura);
            this.horizontal(x, y, z, espessura, cy, cor);
            this.horizontal(x, y, z + cz, espessura, cy, cor);
            this.verticalx(x, y, z, espessura, cz, cor);
            this.verticalx(x, y + cy, z, espessura, cz, cor);
        }


    }
}
