﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Casa
{
    public abstract class Paredes
    {
        public void fazerParede(float hi, float hf, float xi,
                                 float comprX, float yi, float yf)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(xi, yi, hi);
            GL.Vertex3(xi, yi, hf);
            GL.Vertex3(xi + comprX, yi + yf, hf);
            GL.Vertex3(xi + comprX, yi + yf, hi);
            GL.End();
        }
        public void paredeTextura(float hi, float hf, float xi,
                                         float comprX, float yi, float yf,
                                        int textura)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textura);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1); GL.Vertex3(xi, yi, hi);
            GL.TexCoord2(0, 0); GL.Vertex3(xi, yi, hf);
            GL.TexCoord2(1, 0); GL.Vertex3(xi + comprX, yi + yf, hf);
            GL.TexCoord2(1, 1); GL.Vertex3(xi + comprX, yi + yf, hi);
            GL.End();
            GL.Disable(EnableCap.Blend);

        }

        public void fazerChao(float hi, float hf, float xi,
                              float comprX, float yi, float yf)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(xi, yi, hi);
            GL.Vertex3(xi, yi + yf, hf);
            GL.Vertex3(xi + comprX, yi + yf, hf);
            GL.Vertex3(xi + comprX, yi, hi);
            GL.End();
        }

        public void fazerChao2(float hi, float hf, float xi,
                              float comprX, float yi, float yf)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(xi, yi, hi);
            GL.Vertex3(xi + comprX, yi, hf);
            GL.Vertex3(xi + comprX, yi + yf, hf);
            GL.Vertex3(xi, yi + yf, hi);
            GL.End();
        }

        public void chaoTextura(float hi, float hf, float xi,
                              float comprX, float yi, float yf,
                              int textura)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textura);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1); GL.Vertex3(xi, yi, hi);
            GL.TexCoord2(0, 0); GL.Vertex3(xi, yi + yf, hf);
            GL.TexCoord2(1, 0); GL.Vertex3(xi + comprX, yi + yf, hf);
            GL.TexCoord2(1, 1); GL.Vertex3(xi + comprX, yi, hi);
            GL.End();
            GL.Disable(EnableCap.Blend);
        }

        public void fazerParede(float hi, float hf, float xi,
                                 float comprX, float posY) //Apenas criar a parede indo no X
        {
            //GL.Color3(Color.Gray);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(xi, posY, hi);
            GL.Vertex3(xi, posY, hf);
            GL.Vertex3(xi + comprX, posY, hf);
            GL.Vertex3(xi + comprX, posY, hi);
            GL.End();
        }

        public void paredeBuraco(float hi, float hf, float xi,
                                 float comprX, float yi, float yf,
                                 float buracoHi, float buracoHf,
                                 float buracoXi, float buracoXf,
                                 float buracoYi, float buracoYf)
        {


            

            fazerParede(hi, buracoHi, xi, comprX, yi, yf);
            fazerParede(buracoHf, hf, xi, comprX, yi, yf);

            if (xi != comprX) //cria na diagonal
            {
                buracoYi = yf * buracoXi / comprX; //eq. para achar valor adequado do y do buraco
                buracoYf = yf * buracoXf / comprX;

            }

            fazerParede(buracoHi, buracoHf, xi, buracoXi, yi, buracoYi);
            fazerParede(buracoHi, buracoHf, buracoXf, buracoXi, buracoYf, yf - buracoYf);

                }

        public void fazerEscada(int deg, float altura, float hf, float xi,
                              float comprX, float yi, float yf)  //começando de cima para baixo
        {
            float alturaDeg;
            int cont;

            GL.Begin(PrimitiveType.Quads);
            for (cont = 0; cont < deg; cont++)
            {
                GL.Vertex3(xi + comprX, yi + yf, altura);
                GL.Vertex3(xi + comprX, yi, altura);
                GL.Vertex3(xi, yi, altura);
                GL.Vertex3(xi, yi + yf, altura);


                alturaDeg = altura - hf;

                GL.Vertex3(xi, yi, altura);
                GL.Vertex3(xi, yi + yf, altura);
                GL.Vertex3(xi, yi + yf, alturaDeg);
                GL.Vertex3(xi, yi, alturaDeg);

                altura = alturaDeg;
                xi -= comprX;
            }

            GL.End();
        }
    }
}
