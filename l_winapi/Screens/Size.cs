namespace l_winapi.Screens
{
    public struct Size
    {
        // public his.Right - this.Left, this.Bottom - this.Top
        public double Height;
        public double Width;

        public Size(double w, double h)
        {

            Height = h;
            Width = w;
        }

        public override string ToString()
        {
            return $"{Width}px/{Height}px";
        }
    }
}
