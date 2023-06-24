using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainTool
{
    public class One
    {
        public static void Banner()
        {
            char ch = '★';
            
            char num1 = '☆';
            
            
            Console.CursorVisible = false;

			Console.WriteLine("\n\n\n\n\n\n");
			Console.WriteLine("                            ■■  ■■    ■        ■   ■■■■   ■");
			Console.WriteLine("                           ■■■■■■   ■   ■   ■   ■         ■");
			Console.WriteLine("                            ■■■■■    ■  ■■  ■   ■■■■   ■");
			Console.WriteLine("                              ■■■       ■■  ■■    ■         ■");
			Console.WriteLine("                                ■          ■    ■     ■■■■   ■■■■");
			Console.WriteLine("");
			Console.WriteLine("                                              ■■■   ■■■  ■■    ■■ ■■■■");
			Console.WriteLine("                                             ■    ■ ■    ■ ■  ■■  ■ ■");
			Console.WriteLine("                                            ■        ■    ■ ■   ■   ■ ■■■■");
			Console.WriteLine("                                             ■    ■ ■    ■ ■        ■ ■");
			Console.WriteLine("                                              ■■■   ■■■  ■        ■ ■■■■");
			Console.WriteLine("");
			Console.WriteLine("                                      반갑습니다. 게임을 시작하시겠습니까?");
			Console.WriteLine("");
			Console.WriteLine("                                           게임진행:[Y]        나가기:[N]");
			Console.WriteLine("       \n\n\n\n                                                                                               Made TEAM : 독도");


			for (int u = 0; u <= 116;u+=2)
			{
				Console.CursorLeft = u;
				Console.CursorTop = 0;
				Console.WriteLine(num1);
				Thread.Sleep(30);

				Console.CursorLeft = u;
				Console.CursorTop = 0;
				Console.WriteLine(ch);
				Thread.Sleep(10);

				Console.CursorLeft = 116 -u;
				Console.CursorTop = 28;
				Console.WriteLine(num1);
				Thread.Sleep(30);

				Console.CursorLeft = 116 -u;
				Console.CursorTop = 28;
				Console.WriteLine(ch);
				Thread.Sleep(10);

			} 
            ConsoleKeyInfo k = Console.ReadKey();
			if (k.Key == ConsoleKey.Y)
			{
				//_2_Menu_Class1 main = new _2_Menu_Class1();
				bool isRunning = true;
				while (isRunning)
				{
					try
					{
						Console.Beep();
						Console.Clear();
						_2_Menu_Class1.Menu();
					}
					catch(Exception ex) { }
				}

			}
			if (k.Key == ConsoleKey.N)
			{
                Console.Beep();
                Console.WriteLine("안녕!"); // 나가기
            }

		}
    }
}


