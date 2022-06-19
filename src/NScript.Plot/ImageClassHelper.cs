namespace NScript.Plot
{
    using Geb.Image;
    using NScript.UI;
    using NScript.UI.Media;
    using NScript.UI.Controls;
    using NScript.UI.Input;

    public static class ImageClassHelper
    {
        public static void ShowDialog(this ImageBgra32 image, String title = null, Func<Window, bool> loop = null)
        {
            if (image == null) return;

            int width = Math.Min(1200, image.Width);
            int height = Math.Min(700, image.Height);
            Window window = new Window(width, height, title);
            ImageView img = new ImageView
            {
                Size = new NScript.UI.Media.SizeF(width, height),
                Image = image
            };
            window.Add(img);
            window.ShowDialog(loop);
        }
    }
}