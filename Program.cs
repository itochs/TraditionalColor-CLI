namespace HelloWorld;

class Program
{
  static void Main(string[] args)
  {
    GameManager gm = new GameManager();
    gm.Start();
    gm.Update();
  }
}