using HalalFramework.Solver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Problem
{
    class ImageSegmentationProblem
    {
        private int width, height;

        private Bitmap color, grayscale;
        private byte[] raw_color, raw_grayscale;
        private int[] gs_histogram = new int[256];
        private List<int> pix_cluster = new List<int>();


        public ImageSegmentationProblem(string filename)
        {
            this.loadImageFromFile(filename);
        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public byte[] RawColor { get => raw_color; set => raw_color = value; }
        public byte[] RawGrayscale { get => raw_grayscale; set => raw_grayscale = value; }
        public int[] Histogram { get => gs_histogram; set => gs_histogram = value; }
        public List<int> PixCluster { get => pix_cluster; set => pix_cluster = value; }
        public List<Cluster> PixCluster2 { get; set; }
        public Bitmap Color { get => color; set => color = value; }
        public Bitmap Grayscale { get => grayscale; set => grayscale = value; }

        int pixg(int x, int y)
        {
            return raw_grayscale[y * width + x];
        }

        public void setPixCluster(int x, int y, int cluster)
        {
            pix_cluster[y * width + x] = cluster;
        }

        public void loadImageFromFile(string filename)
        {
            Color = new Bitmap(filename);

            Width = Color.Width;
            Height = Color.Height;

            Grayscale = MakeGrayscale3(Color);

            // create histogram
            for (int i = 0; i < 255; i++)
                Histogram[i] = 0;

            RawGrayscale = new byte[Height * Width];


            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    RawGrayscale[i * Width + j] = Grayscale.GetPixel(i,j).G;
                }
            }

            for (int i = 0; i < Width * Height; i++)
                Histogram[raw_grayscale[i]]++;

            // cluster data
            for (int i = 0; i < width * Height; i++)
                PixCluster.Add(0);
        }

        public void savePixelClusterToFile(string filename)
        {
            Bitmap clustered = new Bitmap(Width, Height);
            for (int i = 0; i < width * height; i++)
            {
                Color data = new Color();
                switch (pix_cluster[i])
                {
                    case 0:
                        data = System.Drawing.Color.FromArgb(255, 100, 100);
                        break;
                    case 1:
                        data = System.Drawing.Color.FromArgb(100, 255, 100);
                        break;
                    case 2:
                        data = System.Drawing.Color.FromArgb(100, 100, 255);
                        break;
                    case 3:
                        data = System.Drawing.Color.FromArgb(100, 255, 255);
                        break;
                    case 4:
                        data = System.Drawing.Color.FromArgb(255, 100, 255);
                        break;
                }
                clustered.SetPixel(i % width, i / width, data);
            }
            clustered.Save(filename);
        }

        //PerProgról nyúlva
        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
    }
}
