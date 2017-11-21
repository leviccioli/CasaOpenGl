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
    class Estrutura : Paredes
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
        public void FazerCasaPedro(int texParede , int texPorta , int texPortao ,
            int texJanela,int texLaranja,int  texRosa , int  texAzul , int texBranca, int texChaoCinza,int  texMadeira, int texIluminacao , int texVidro)
        {
            int h = 100;


            
            paredeTextura(0, h, 0, 2000, 800, 0, texBranca);        //fazerParede(0, h, 0, 2000, 1000);
            paredeTextura(0, h, 2000, 0, 0, 1,texBranca);         //fazerParede(0, h, 2000, 0, 0, 1000);        
            paredeTextura(0, h, 0, 0, 700, 100, texPortao);
            paredeTextura(0, h, 0, 2000, 0, 0, texBranca);           //fazerParede(0, h, 0, 2000,0);  

           

 









        }

        public void janelas(int texJanelas)
        {

        }



    }
}

