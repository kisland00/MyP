using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Assembley;

namespace MainTool
{
	internal class _5_ball_Pan
	{
		public static void Basket()
		{
			Console.Clear();

			Console.WriteLine("\n\n\n");
			Console.WriteLine("                               ⑥                                                 ⊂_ヽ                               ");
			Console.WriteLine("                                                           ③                        ＼＼ Λ＿Λ                      ");
			Console.WriteLine("                 ①                                                                    ＼( 'ㅅ' ) 둠칫                ");
			Console.WriteLine("                                         ⑩                                          　 >　⌒ヽ                       ");
			Console.WriteLine("     ⑤                                                                               　/ 　 へ＼                     ");
			Console.WriteLine("                      ⑨                                            ⑬                 /　　/　＼＼ 두둠칫            ");
			Console.WriteLine("                                              ⑭                                       ﾚ　ノ　　 ヽ_つ                ");
			Console.WriteLine("                                                                                      /　/                            ");
			Console.WriteLine("                                      ⑧                                          　 /　/|                            ");
			Console.WriteLine("           ④                                                                     　(　(ヽ 두둠칫                     ");
			Console.WriteLine("                           ■■■■■■■■■■                                   　|　|、＼                          ");
			Console.WriteLine("                           ■                ■           ④                        | 丿 ＼ ⌒)                       ");
			Console.WriteLine("                             ■            ■                                       | |　　) /                        ");
			Console.WriteLine("   ⑫                         ■■■■■■■                                       ノ )　　Lﾉ                         ");
			Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
			Console.WriteLine("");
			Console.WriteLine("                                                   받을준비 됫나! \n\n                                          준비됬다![y] 아...기..기다려![n]");

            ConsoleKey k = Console.ReadKey().Key;
            if (k == ConsoleKey.Y)
			{
                Console.Beep();
                Console.Clear();
				GameManager gm = new GameManager();
				gm.MainLoop();
			}
			else if (k == ConsoleKey.N)
			{
                Console.Beep();
                _2_Menu_Class1.Menu();
			}
		}
	}
}
