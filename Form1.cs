using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace Casa
{
    public partial class Form1 : Form
    {
        Auxiliar estruturar = new Auxiliar();
        int lateral = 0, lateral2 = 0;
        int size_muro = 100;
        Vector3d dir = new Vector3d(0, -450, 120);        //dire��o da c�mera
        Vector3d pos = new Vector3d(0, -550, 120);     //posição da camera
        float camera_rotation = 0;                     //rotação no eixo Z
        float camera_rotation2 = 0;
        float valor = 0f;
        int textParede;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //limpa os buffers
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity(); //zera a matriz de projeção com a matriz identidade

            Matrix4d lookat = Matrix4d.LookAt(pos.X, pos.Y, pos.Z, dir.X, dir.Y, dir.Z, 0, 0, 1);

            //aplica a transformacao na matriz de rotacao
            GL.LoadMatrix(ref lookat);
            //GL.Rotate(camera_rotation, 0, 0, 1);

            GL.Enable(EnableCap.DepthTest);


            //EIXOS X, Y, Z
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0); GL.Vertex3(500, 0, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 500, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 500);
            GL.End();

            casaDraw();



            glControl1.SwapBuffers(); //troca os buffers de frente e de fundo 

        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.DarkBlue);         // definindo a cor de limpeza do fundo da tela
            GL.Enable(EnableCap.Light0);

            textParede = LoadTexture("../../textura/fotografia.jpg"); /*
            texPorta = LoadTexture("../../textura/porta.jpg");
            texPiso = LoadTexture("../../textura/download.jpg");
            texParede = LoadTexture("../../textura/wall1.jpg");
            texParede2 = LoadTexture("../../textura/wall2.jpg");
            texTelhado = LoadTexture("../../textura/telhado.jpg");*/
            SetupViewport();                      //configura a janela de pintura

        }

        private void SetupViewport() //configura a janela de projecao 
        {
            int w = glControl1.Width; //largura da janela
            int h = glControl1.Height; //altura da janela

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(1f, w / (float)h, 1f, 2000.0f);
            GL.LoadIdentity(); //zera a matriz de projecao com a matriz identidade

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Viewport(0, 0, w, h); // usa toda area de pintura da glcontrol
            lateral = w / 2;
            lateral2 = w / 2;

        }

        static int LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int id;//= GL.GenTexture(); 

            GL.GenTextures(1, out id);
            GL.BindTexture(TextureTarget.Texture2D, id);

            /*Bitmap bmp = new Bitmap(filename);

            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);*/

           /* GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);*/

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return id;
        }

        private void calcula_direcao()
        {
            dir.X = pos.X + (Math.Sin(camera_rotation * Math.PI / 180) * 1000);
            dir.Y = pos.Y + (Math.Cos(camera_rotation * Math.PI / 180) * 1000);
            dir.Z = pos.Z + (Math.Tan(camera_rotation2 * Math.PI / 180) * 1000);
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > lateral)
            {
                camera_rotation += 2;
            }
            if (e.X < lateral)
            {
                camera_rotation -= 2;
            }
            if (e.Y > lateral2)
            {
                camera_rotation2 -= 0.6f;
            }
            if (e.Y < lateral2)
            {
                camera_rotation2 += 0.6f;
            }
            lateral = e.X;
            lateral2 = e.Y;
            calcula_direcao();
            glControl1.Invalidate();
        }

        private void glControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            float a = camera_rotation;
            int tipoTecla = 0;
            int sinal = 1;

            if (e.KeyCode == Keys.Q)
            {
                valor += 10;
                pos.Z += 100;
                glControl1.Invalidate();
            }
            if (e.KeyCode == Keys.E)
            {
                valor -= 10;
                pos.Z -= 100;
                glControl1.Invalidate();
            }
            if (e.KeyCode == Keys.A)
            {
                sinal = 0;
                a -= 90;
                tipoTecla = 1;
            }
            if (e.KeyCode == Keys.D)
            {
                sinal = 0;
                a += 90;
                tipoTecla = 1;
            }
            if (e.KeyCode == Keys.W)
            {
                tipoTecla = 1;
            }
            if (e.KeyCode == Keys.S)
            {
                sinal = -1;
                a += 180;
                tipoTecla = 1;
            }

            if (e.KeyCode == Keys.Right)
            {
                a += 3;
                tipoTecla = 2;
            }
            if (e.KeyCode == Keys.Left)
            {
                a -= 3;
                tipoTecla = 2;
            }
            if (e.KeyCode == Keys.Up)
            {
                pos.Z += 2;
            }
            if (e.KeyCode == Keys.Down)
            {
                pos.Z -= 2;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (tipoTecla == 1)
            {
                if (a < 0) a += 360;
                if (a > 360) a -= 360;
                //label2.Text = lateral.ToString();
                pos.X += (Math.Sin(a * Math.PI / 180) * 100);
                pos.Y += (Math.Cos(a * Math.PI / 180) * 100);
                pos.Z += (Math.Sin(camera_rotation2 * Math.PI / 180) * 100) * sinal;
                calcula_direcao();
                glControl1.Invalidate();
            }

            if (tipoTecla == 2)
            {
                camera_rotation = a;
                calcula_direcao();
                glControl1.Invalidate();
            }
            //txtPosX.Text = Convert.ToInt16(pos.X).ToString();
            //txtPosY.Text = Convert.ToInt16(pos.Y).ToString();
            //txtPosZ.Text = Convert.ToInt16(pos.Z).ToString();

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            glControl1.Width = Form1.ActiveForm.Width - 10;
            glControl1.Height = Form1.ActiveForm.Height - 10;
            if (Form1.ActiveForm.Visible)
            {
                SetupViewport();
            }
            glControl1.Invalidate();
        }

        private void casaDraw()
        {
            //PAREDES
           // VERTICAL Y = posição na linha Y, posição na profundidade, altura plana, tamanho, altura da parede;

           //VERTICAL X = posição na linha X, posição na profundidade, altura plana, tamanho, altura da parede;

            //CONTORNO DA CASA
            //**********************************************************
            //PAREDES CORREDOR - VERTICAL
            estruturar.verticaly(0, 2900, 0, -2400, 150, Color.Aqua); //ok
            estruturar.verticaly(1200, 2900, 0, -2100, 150, Color.Aqua); //ok

            //FECHAMENTO DA PAREDE DO CORREDOR/CASA - FUNDO - HORIZONTAL
            estruturar.verticalx(1200, 2900, 0, -1200, 150, Color.DarkOliveGreen);

            //FRENTE DA CASA COM JANELA
            estruturar.verticalx(150, 500, 0, 175, 150, Color.Purple); 
            estruturar.verticalx(450, 500, 0, 175, 150, Color.Purple); 
            estruturar.verticalx(300, 500, 120, 175, 30, Color.Purple); 
            estruturar.verticalx(300, 500, 0, 175, 30, Color.Purple); 
            estruturar.verticalx(325, 500, 30, 130, 90, Color.White); //JANELA
 
            //**********************************************************
            
            
            
            //Parede Direita
            estruturar.verticaly(1050, 800, 0, 730, 150, Color.Brown);
            estruturar.verticaly(1050, 1670, 0, 400, 150, Color.Brown);
            estruturar.verticaly(1050, 2220, 0, 260, 150, Color.Brown);
            estruturar.verticaly(1050, 2560, 0, 40, 150, Color.Brown);
            estruturar.verticaly(1050, 2460, 130, 100, 20, Color.Brown);
            estruturar.verticaly(1050, 2460, 0, 100, 80, Color.Brown);
            estruturar.verticaly(1050, 2060, 0, 180, 60, Color.Brown);
            estruturar.verticaly(1050, 2060, 140, 180, 10, Color.Brown);
            estruturar.verticaly(1050, 1490, 0, 180, 60, Color.Brown);
            estruturar.verticaly(1050, 1490, 140, 180, 10, Color.Brown);

            //PAREDE FECHAMENTO DA CASA - SAÍDA PARA QUINTAL FUNDOS
            estruturar.verticalx(1050, 2600, 0, -400, 150, Color.Blue);
            estruturar.verticalx(470, 2600, 0, -320, 150, Color.Blue);
            estruturar.verticalx(650, 2600, 0, -80, 150, Color.Blue);
            estruturar.verticalx(570, 2600, 130, -100, 20, Color.Blue);

           

            //estruturar.verticalx(1050, 2600, 0, -900, 150, Color.Blue);

            //Parede Esquerda
            estruturar.verticaly(150, 1190, 0, -690, 150, Color.Lime);
            estruturar.verticaly(150, 1550, 0, -250, 150, Color.Lime);
            estruturar.verticaly(150, 2290, 0, -490, 150, Color.Lime);
            estruturar.verticaly(150, 2600, 0, -200, 150, Color.Lime);
            estruturar.verticaly(150, 1350, 0, -200, 80, Color.Lime);
            estruturar.verticaly(150, 1350, 130, -200, 20, Color.Lime);
            estruturar.verticaly(150, 1900, 130, -400, 20, Color.Lime);
            estruturar.verticaly(150, 1900, 0, -400, 70, Color.Lime);
            estruturar.verticaly(150, 2400, 0, -400, 70, Color.Lime);
            estruturar.verticaly(150, 2400, 130, -400, 20, Color.Lime);
            
            //PAREDE COPA E COZINHA
            estruturar.verticalx(150, 2100, 0, 350, 150, Color.Olive);
            estruturar.verticalx(500, 2100, 130, 125, 20, Color.Olive);
            estruturar.verticalx(590, 2100, 0, 35, 150, Color.Olive);

            

            //PAREDE QUARTO1 E SALA
            estruturar.verticaly(625, 1200, 0, -700, 150, Color.Lime);
            estruturar.verticalx(520, 1150, 0, -370, 150, Color.Blue);
            estruturar.verticalx(625,1150, 0, -20, 150, Color.Blue);
            estruturar.verticalx(610, 1150, 130, -110, 20, Color.Blue);

            //PAREDE CORREDOR DO QUARTO2 E SUÍTE
            estruturar.verticaly(625, 2600, 0, -1100, 150, Color.Lime);
            estruturar.verticaly(625, 1500, 130, -1000, 20, Color.Lime);
            estruturar.verticaly(625, 1405, 0, -110, 150, Color.Lime);
            estruturar.verticaly(730, 1770, 0, -420, 150, Color.Lime);
            estruturar.verticaly(730, 1880, 0, -20, 150, Color.Lime);
            estruturar.verticaly(730, 1870, 130, -100, 20, Color.Lime);
            estruturar.verticalx(720, 1880, 0, 330, 150, Color.Black);//PORTA
            estruturar.verticalx(630, 1880, 130, 100, 20, Color.Black);
            estruturar.verticalx(625, 1880, 0, 20, 150, Color.Black);

            //PAREDE DA SALA C/ PORTA
            estruturar.verticalx(590, 1340, 0, 35, 150, Color.Olive);
            estruturar.verticalx(500, 1340, 0, 20, 150, Color.Olive);
            estruturar.verticalx(500, 1340, 130, 90, 20, Color.Olive);
            estruturar.verticalx(750, 800, 0, 300, 150, Color.Olive); 
            estruturar.verticalx(625, 800, 0, 30, 150, Color.Olive); 
            estruturar.verticalx(650, 800, 130, 120, 20, Color.Olive); 
            estruturar.verticalx(655, 800, 0, 95, 130, Color.White);//PORTA

            //PAREDE QUARTO2 E SALA
            estruturar.verticalx(625, 1350, 0, 425, 150, Color.Olive); 

            //PAREDE SUÍTE E BANHEIRO 
            estruturar.verticalx(710, 2450, 0, 335, 150, Color.Olive);
            estruturar.verticalx(625, 2450, 130, 100, 20, Color.Olive);
            estruturar.verticalx(625, 2450, 0, 10, 150, Color.Olive);

            //PAREDE C/ PORTA CORREDOR DIREITO
            estruturar.verticalx(1050, 800, 0, 30, 150, Color.DarkViolet);
            estruturar.verticalx(1170, 800, 0, 30, 150, Color.DarkViolet);
            estruturar.verticalx(1080, 800, 0, 90, 130, Color.White);//PORTA
            estruturar.verticalx(1080, 800, 130, 120, 20, Color.DarkViolet);

            //PAREDE BANHEIRO AO LADO DA COPA
            estruturar.verticalx(150, 1340, 0, 350, 150, Color.Olive);
            estruturar.verticaly(500, 1340, 0, -70, 150, Color.Black);
            estruturar.verticaly(500, 1210, 0, -60, 150, Color.Black);
            estruturar.verticaly(500, 1300, 120, -100, 30, Color.Black);


                   
            //CHAO
            estruturar.horizontal(150, 500, 0, 480, 500, Color.Gray);
            estruturar.horizontal(150, 800, 0, 900, 1800, Color.Gray);
            estruturar.horizontal(0, 500, 0, 150, 2400, Color.FloralWhite);
            estruturar.horizontal(1050, 800, 0, 150, 2100, Color.FloralWhite);
            estruturar.horizontal(150, 2600, 0, 900, 300, Color.FloralWhite);

            estruturar.horizontal(0, 0, 0, 620, 500, Color.Green);
            estruturar.horizontal(620, 0, 0, 580, 800, Color.Gray);
            
        
        }
    }
}
