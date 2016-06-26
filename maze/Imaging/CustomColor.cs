using System.Drawing;

namespace Common.Imaging
{
    public struct CustomColor
    {
        public CustomColor(byte A, byte R, byte G, byte B)
        {
            this.A = A;
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public int ToArgb()
        {
            return A << 24 | R << 16 | G << 8 | B;
        }

        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

    }
}
