using System;
using _MazeGame;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTool
{
	internal class _6_bug_Pan
	{
		public static void Bugbug()
		{
			Console.Clear();

			Console.WriteLine("\n\n\n\n\n\n");
			Console.WriteLine("                                                                                                                      ");
			Console.WriteLine("                                                                           ○　　 ○┤口├　　 L┤                    ");
			Console.WriteLine("      　 　                                                              　┬ Z│ 　口　　 ┐├                       ");
			Console.WriteLine("            ■■■■■　                                                                      　ㅁ                    ");
			Console.WriteLine("          ■■■■■■■                      ■■■■■                                        ┴ ㅅH ㄱㅕㄷ├┤     ");
			Console.WriteLine("         ■  ■■■■■■                    ■■■■■■■                                     ㅅ　ㅇ  ㅅ            ");
			Console.WriteLine("         ■■■■■■■■                ■■■■■■■■■                                                           ");
			Console.WriteLine("           ■■■■■■■              ■■■■■■■■■■                ○　　 ○┤口├　     L┤已├              ");   
			Console.WriteLine("     　　　 ■■■■■■■   	     ■■■■■       ■■■               ┬ Z│   　口 ┐├　      ○              "); 
			Console.WriteLine("              ■■■■■■■        ■■■■■          ■■■    ■      ㄴ                                          ");
			Console.WriteLine("           　  ■■■■■■■■    ■■■■■            ■■■■■       ┴ ス│ 口├ 已├┤                         ");
			Console.WriteLine("                ■■■■■■■■■■■■■■               ■■■        已                                           ");
			Console.WriteLine("                  ■■■■■■■■■■■■                                                                            ");
			Console.WriteLine("                      ■■■■■■■■■                                                                              ");
			Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
			Console.WriteLine("");
			Console.WriteLine("                                                   Bug다! 도망가! \n\n                                아..왕꿈틀이 안좋아하는데[y]   귀찮아..날잡아먹어[n]");

			ConsoleKey k = Console.ReadKey().Key;

            if (k == ConsoleKey.Y)
			{
                Console.Beep();
                MazeGame mz = new MazeGame();
				int turn = 0;
				List<TimeSpan> gameTime = mz.MainLoop(ref turn);
				Console.Clear();
                Console.WriteLine("점수판");
				Console.WriteLine($"{turn}번 스테이지까지 진행!");
				if(turn == 5)
				{

				}
				for(int i =0;i< gameTime.Count; i++)
				{
					Console.WriteLine(gameTime[i]);
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
