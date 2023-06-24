using System;
using _GameManager;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTool
{
	internal class _4_ship_Pan
	{
		public static void Ship()
		{
			Console.Clear();
			Console.WriteLine("                ■  ■                                                                 ■  ■                     ◀■ ");
			Console.WriteLine("              ■  ■  ■                                ■  ■                       ■  ■  ■                     ■ ");
			Console.WriteLine("                                                      ■  ■  ■                                                    ■ ");
			Console.WriteLine("                                                                                                                    ■ ");
			Console.WriteLine("                         ~            ◀■               ~                                    ~             ■■■■■ ");
			Console.WriteLine("    ~                                   ■                                      ■■■                ~       ■■■■ ");
			Console.WriteLine("               ~                        ■                                    ■■■■                          ■■■ ");
			Console.WriteLine("                                   ■■■■■■                                 ■■■                         ~       ");
			Console.WriteLine("                                    ■■■■■       ~         ~                  ■■                                 ");
			Console.WriteLine("                                 ~                                                ■■                  ~              ");
			Console.WriteLine("■      ~                                     ~                 ~                 ■■                                 ");
			Console.WriteLine("■                                                                                ■■           ~                     ");
			Console.WriteLine("■                           ~                            ~               ■■■■■■■■■■                    ~    ");
			Console.WriteLine("■                                   ~                                 ■■■  ■■■■  ■■■                        ");
			Console.WriteLine("■■■■■■■                                              ~    ■■■■■■■■■■■■■■■■■■          ~       ");
			Console.WriteLine("■■■■■■                                ~                      ■■■■■■■■■■■■■■■■                    ");
			Console.WriteLine("■■■■■            ~                                               ■■■■■■■■■■■■■   ~                   ");
			Console.WriteLine("");
			Console.WriteLine("                                          적선이 출몰했다 다들 출정준비!\n\n                                           출정하려면[y] 후퇴하려면[n]");

            ConsoleKey k = Console.ReadKey().Key;
            if (k == ConsoleKey.Y)
			{
                Console.Beep();
                GameManager gm = new GameManager();
				int turn = 0;
				if(gm.MainLoop(ref turn))
				{
					Console.WriteLine("승리하셨습니다.");
					Console.WriteLine("점수판");
					Console.WriteLine("끝낸 턴 수 : {0}",turn);
				}
				else
				{

				}
			}
			else if (k == ConsoleKey.N)
			{
                Console.Beep();
                _2_Menu_Class1.Menu();
			}
		}
	}
}
