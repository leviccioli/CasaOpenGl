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
        int parede2, parede3;
        int porta3, porta, porta2;
        int chao, grama, pedrisco;
        int pedras, pedras2;
        int janela, janela2, janela3;
        int portao;
        int telhado, telhado2, telhado3, telhado4, forro;


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

            SetupViewport();                      //configura a janela de pintura

        }

        private void SetupViewport() //configura a janela de projecao 
        {
            int w = glControl1.Width; //largura da janela
            int h = glControl1.Height; //altura da janela

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(1f, w / (float)h, 1f, 2000.0f);
            GL.LoadIdentity(); //zera a matriz de projecao com a matriz identidade
            parede2 = LoadTexture("./Texturas/parede2.jpg");
            parede3 = LoadTexture("./Texturas/parede3.jpg");
            chao = LoadTexture("./Texturas/chao.jpg");
            pedras = LoadTexture("./Texturas/pedras.jpg");
            pedras2 = LoadTexture("./Texturas/pedras2.jpg");
            grama = LoadTexture("./Texturas/grama.jpg");
            pedrisco = LoadTexture("./Texturas/pedrisco.jpg");
            porta = LoadTexture("./Texturas/porta.jpg");
            porta2 = LoadTexture("./Texturas/porta2.jpg");
            janela = LoadTexture("./Texturas/janela.jpg");
            telhado = LoadTexture("./Texturas/telhado.png");
            portao = LoadTexture("./Texturas/portao.jpg");
            porta3 = LoadTexture("./Texturas/porta3.jpg");
            janela2 = LoadTexture("./Texturas/janela2.jpg");
            janela3 = LoadTexture("./Texturas/janela3.png");
            telhado2 = LoadTexture("./Texturas/telhado2.jpg");
            telhado3 = LoadTexture("./Texturas/telhado3.jpg");
            telhado4 = LoadTexture("./Texturas/telhado4.jpg");
            forro = LoadTexture("./Texturas/forro.jpg");


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

            Bitmap bmp = new Bitmap(filename);

            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

             GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                 OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
             bmp.UnlockBits(data);

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
            estruturar.verticaly(0, 2900, 0, -2400, 150, parede2); //ok
            estruturar.verticaly(1200, 2900, 0, -2100, 150, parede2); //ok

            //FECHAMENTO DA PAREDE DO CORREDOR/CASA - FUNDO - HORIZONTAL
            estruturar.verticalx(1200, 2900, 0, -1200, 150, parede2);

            //FRENTE DA CASA COM JANELA
            estruturar.verticalx(150, 500, 0, 175, 150, parede3); 
            estruturar.verticalx(450, 500, 0, 175, 150, parede3); 
            estruturar.verticalx(300, 500, 120, 175, 30, parede3); 
            estruturar.verticalx(300, 500, 0, 175, 30, parede3); 
            estruturar.verticalx(325, 500, 30, 130, 90, janela); //JANELA
            estruturar.verticalx(0, 500, 0, 175, 150, parede2); 
 
            //**********************************************************
            
            
            
            //Parede Direita da casa
            estruturar.verticaly(1050, 800, 0, 730, 150, parede3);
            estruturar.verticaly(1050, 1670, 0, 400, 150, parede3);
            estruturar.verticaly(1050, 2220, 0, 260, 150, parede3);
            estruturar.verticaly(1050, 2560, 0, 40, 150, parede3);
            estruturar.verticaly(1050, 2460, 130, 100, 20, parede3);
            estruturar.verticaly(1050, 2480, 80, 80, 50, janela2);
            estruturar.verticaly(1050, 2460, 0, 100, 80, parede3);
            estruturar.verticaly(1050, 2060, 0, 180, 60, parede3);
            estruturar.verticaly(1050, 2060, 140, 180, 10, parede3);
            estruturar.verticaly(1050, 2070, 60, 150, 80, janela);
            estruturar.verticaly(1050, 1490, 0, 180, 60, parede3);
            estruturar.verticaly(1050, 1490, 140, 180, 10, parede3);
            estruturar.verticaly(1050, 1530, 60, 140, 80, janela);

            //PAREDE FECHAMENTO DA CASA - SAÍDA PARA QUINTAL FUNDOS
            estruturar.verticalx(1050, 2600, 0, -400, 150, parede3);
            estruturar.verticalx(490, 2600, 0, -340, 150, parede3);
            estruturar.verticalx(650, 2600, 0, -80, 150, parede3);
            estruturar.verticalx(570, 2600, 130, -100, 20, parede3);
            estruturar.verticalx(570, 2600, 0, -80, 130, porta3);

           

            //Parede Esquerda da casa
            estruturar.verticaly(150, 1190, 0, -690, 150, parede3);
            estruturar.verticaly(150, 1550, 0, -250, 150, parede3);
            estruturar.verticaly(150, 2290, 0, -490, 150, parede3);
            estruturar.verticaly(150, 2600, 0, -200, 150, parede3);
            estruturar.verticaly(150, 1350, 0, -200, 80, parede3);
            estruturar.verticaly(150, 1350, 130, -200, 20, parede3); //JANELA BANHEIRO
            estruturar.verticaly(150, 1300, 80, -110, 50, janela2);
            estruturar.verticaly(150, 1900, 130, -400, 20, parede3);
            estruturar.verticaly(150, 1800, 60, -250, 80, janela);
            estruturar.verticaly(150, 1900, 0, -400, 70, parede3);
            estruturar.verticaly(150, 2400, 0, -400, 70, parede3);
            estruturar.verticaly(150, 2400, 130, -400, 20, parede3); // JANELA COZINHA
            estruturar.verticaly(150, 2400, 60, -110, 80, janela);
            
            //PAREDE COPA E COZINHA
            estruturar.verticalx(150, 2100, 0, 350, 150, parede3);
            estruturar.verticalx(500, 2100, 130, 125, 20, parede3);
            estruturar.verticalx(590, 2100, 0, 35, 150, parede3);

            

            //PAREDE QUARTO1 E SALA
            estruturar.verticaly(625, 800, 0, -300, 150, parede3);
            estruturar.verticaly(625, 1200, 0, -500, 150, parede3);
            estruturar.verticalx(520, 1150, 0, -370, 150, parede3);
            estruturar.verticalx(625,1150, 0, -20, 150, parede3);
            estruturar.verticalx(610, 1150, 130, -110, 20, parede3);
            estruturar.verticalx(605, 1150, 0, -110, 130, porta3);

            //PAREDE CORREDOR DO QUARTO2 E SUÍTE
            estruturar.verticaly(625, 2600, 0, -1100, 150, parede3);
            estruturar.verticaly(625, 1500, 130, -1000, 20, parede3);
            estruturar.verticaly(625, 1405, 0, -110, 150, parede3);
            estruturar.verticaly(730, 1770, 0, -420, 150, parede3);
            estruturar.verticaly(730, 1880, 0, -20, 150, parede3);
            estruturar.verticaly(730, 1870, 130, -100, 20, parede3);
            estruturar.verticaly(730, 1860, 0, -90, 130, porta3);
            estruturar.verticalx(720, 1880, 0, 330, 150, parede3);//PORTA
            estruturar.verticalx(630, 1880, 130, 100, 20, parede3);
            estruturar.verticalx(645, 1880, 0, 75, 130, porta3);
            estruturar.verticalx(625, 1880, 0, 20, 150, parede3);

            //PAREDE DA SALA C/ PORTA
            estruturar.verticalx(590, 1340, 0, 35, 150, parede3);
            estruturar.verticalx(500, 1340, 0, 20, 150, parede3);
            estruturar.verticalx(500, 1340, 130, 90, 20, parede3);
            estruturar.verticalx(750, 800, 0, 300, 150, parede3); 
            estruturar.verticalx(625, 800, 0, 30, 150, parede3); 
            estruturar.verticalx(650, 800, 130, 120, 20, parede3); 
            estruturar.verticalx(655, 800, 0, 95, 130, porta);//PORTA

            //PAREDE QUARTO2 E SALA
            estruturar.verticalx(625, 1350, 0, 425, 150, parede3); 

            //PAREDE SUÍTE E BANHEIRO 
            estruturar.verticalx(710, 2450, 0, 335, 150, parede3);
            estruturar.verticalx(625, 2450, 130, 80, 20, parede3);
            estruturar.verticalx(635, 2450, 0, 80, 130, porta3);
            estruturar.verticalx(625, 2450, 0, 10, 150, parede3);

            //PAREDE C/ PORTA CORREDOR DIREITO
            estruturar.verticalx(1050, 800, 0, 30, 150, parede2);
            estruturar.verticalx(1170, 800, 0, 30, 150, parede2);
            estruturar.verticalx(1080, 800, 0, 90, 130, porta2);//PORTA
            estruturar.verticalx(1080, 800, 130, 120, 20, parede2);

            //PAREDE BANHEIRO AO LADO DA COPA
            estruturar.verticalx(150, 1340, 0, 350, 150, parede3);
            estruturar.verticaly(500, 1340, 0, -70, 150, parede3);
            estruturar.verticaly(500, 1210, 0, -60, 150, parede3);
            estruturar.verticaly(500, 1270, 0, -60, 130, porta3);
            estruturar.verticaly(500, 1300, 120, -100, 30, parede3);


                   
            //CHAO
            estruturar.horizontal(150, 500, 0, 475, 500, chao);
            estruturar.horizontal(150, 800, 0, 900, 1800, chao);
            estruturar.horizontal(0, 500, 0, 150, 2400, pedras2);
            estruturar.horizontal(1050, 800, 0, 150, 2100, pedras2);
            estruturar.horizontal(150, 2600, 0, 900, 300, pedras);
            estruturar.horizontal(0, 0, 0, 620, 500, grama);
            estruturar.horizontal(620, 0, 0, 580, 800, pedrisco);
            estruturar.verticalx(620, 0, 0, 580, 150, portao);

            //PAREDE PORTAO
            estruturar.verticalx(0, 0, 0, 650, 150, parede2);
            estruturar.verticaly(0, 0, 0, 650, 150, parede2);

            //TELHADO
            estruturar.horizontal(150, 500, 150, 475, 500, telhado2);           
            estruturar.horizontal(620, 0, 152, 580, 800, telhado);
            estruturar.horizontal(620, 580, 150, 430, 800, telhado2);
            estruturar.horizontal(150, 800, 152, 900, 1800, telhado2);

          // estruturar.diagonalx(149, 500, 150, 250, 2100, 30, telhado2);
           //estruturar.diagonalx(600, 800, 150, 250, 1800, 250, telhado2);

            //estruturar.verticalx(0, 0, 0, 350, 1050, parede);
            
        
        }
    }
}
