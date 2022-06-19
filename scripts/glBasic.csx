#load "gl.csx"

using Glfw3;
using Geb.UI.Ogl;

if (!Glfw.Init()) Environment.Exit(-1);
GLWindow window = new GLWindow(600,500,"demo");
window.OnDraw = (item)=>{ Console.WriteLine("Draw!");};
window.ShowDialog();
