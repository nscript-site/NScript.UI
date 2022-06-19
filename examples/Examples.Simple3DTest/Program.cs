using System;

namespace Examples.Simple3D
{
    using Examples.Common3D;

    using Geb.UI.D2D;

    class Program
    {
        static void Main(string[] args)
        {
            //FrameBuffObjSample.Run();

            TestD3D();

            Console.ReadKey();
        }

        static void TestD3D()
        {
            D3DDrawContext cxt = new D3DDrawContext();
            cxt.Measure(new Geb.UI.Media.SizeF(100, 100));
            cxt.Draw();
        }
    }
}
