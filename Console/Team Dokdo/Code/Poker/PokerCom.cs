using MainTool;
using P_com1;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Manager
    {
        public static void PokerGameView()
		{

			Console.Clear();
			Console.WriteLine("\n\n\n\n\n\n");
			Console.WriteLine("                                   ■    ■ ■■■  ■     ■      ■■■ ");
			Console.WriteLine("                                   ■    ■ ■      ■     ■     ■    ■");
			Console.WriteLine("                                   ■■■■ ■■■  ■     ■     ■    ■ ");
			Console.WriteLine("                                   ■    ■ ■      ■     ■     ■    ■");
			Console.WriteLine("                                   ■    ■ ■■■  ■■■ ■■■  ■■■");
			Console.WriteLine(" ");
			Console.WriteLine("                                                ■  ■■■  ■  ■ ■■■ ■■■ ");
			Console.WriteLine("                                                ■ ■    ■ ■ ■  ■     ■   ■");
			Console.WriteLine("                                                ■ ■    ■ ■■   ■■■ ■■■ ");
			Console.WriteLine("                                            ■  ■ ■    ■ ■ ■  ■     ■  ■ ");
			Console.WriteLine("                                             ■■   ■■■  ■  ■ ■■■ ■   ■");
			Console.WriteLine("");
			Console.WriteLine("                                          당신은 살아남을수 있습니까?\n                                       게임시작은 ☞ y ☜ 퇴장은 ☞ n ☜");

            ConsoleKey k = Console.ReadKey().Key;
            if (k == ConsoleKey.Y)
			{
                Console.Beep();
                Joker();
			}
			else if (k == ConsoleKey.N)
			{
                Console.Beep();
                _2_Menu_Class1.Menu();
            }
			
		}

		private static void Joker()
		{
			for (int s = 1; s <= 7; s++)//횟수 반복
			{
				Console.Clear();
				Computer com = new Computer();


				Console.WriteLine("\n\n\n\n\n");
				Console.WriteLine("                        ■■■■■■■    ■■■     ■  ■ ■■■ ■     ■");
				Console.WriteLine("                      ■■■■■■■■■    ■       ■ ■    ■   ■     ■");
				Console.WriteLine("                     ■■    ■■    ■■   ■       ■■     ■   ■     ■");
				Console.WriteLine("                     ■■    ■■    ■■   ■       ■ ■    ■   ■     ■");
				Console.WriteLine("                      ■■■■■■■■■  ■■■     ■  ■ ■■■ ■■■ ■■■");
				Console.WriteLine(" ");
				Console.WriteLine("                       ■■■■■■■■                  ■    ■ ■■■   ■    ■");
				Console.WriteLine("                        ▼▼▼▼▼▼▼                    ■  ■ ■    ■  ■    ■");
				Console.WriteLine("                        ■          ■                      ■   ■    ■  ■    ■");
				Console.WriteLine("                        ■    ♥    ■                      ■   ■    ■  ■    ■");
				Console.WriteLine("                        ▲▲▲▲▲▲▲                      ■    ■■■    ■■■");
				Console.WriteLine("                        ■■■■■■■                                            ");
                Console.WriteLine(" ");
                com.Computer1();
				com.EnterKey();

				Console.WriteLine("");
				Console.WriteLine($"                                              [{s}번째 차례 입니다.]\n                                       살아 남을수 있는 방법을 의논 하세요.\n                                         의논이 끝나면 아무키나 누르시오\n                                                   포기는:[n]");
		
				if (Console.ReadKey().Key == ConsoleKey.N)
				{
                    Console.Beep();
                    GameOver();
					break;
				}
				else if (s ==7)
				{
                    Console.Beep();
                    Win();
				}
			}
		}
	    private static void Win()
		{
			Console.Clear();
			Console.WriteLine("\n\n\n");
			Console.WriteLine("                        ■    ■  ■■■   ■    ■   ■ ■■■ ");
			Console.WriteLine("                         ■  ■  ■    ■  ■    ■  ■  ■   ■");
			Console.WriteLine("                           ■    ■    ■  ■    ■      ■■■");
			Console.WriteLine("                           ■    ■    ■  ■    ■      ■  ■");
			Console.WriteLine("                           ■     ■■■    ■■■       ■   ■");
			Console.WriteLine("");
			Console.WriteLine("                                      ■          ■ ■■■   ■    ■   ■");
			Console.WriteLine("                                       ■        ■    ■     ■■  ■   ■");
			Console.WriteLine("                                        ■  ■  ■     ■     ■ ■ ■   ■");
			Console.WriteLine("                                        ■ ■■ ■     ■     ■  ■■     ");
			Console.WriteLine("                                         ■    ■    ■■■   ■    ■   ■");
			Console.WriteLine("\n                                             Congratulation\n\n                                           생존을 축하합니다.");
			Console.WriteLine("\n                                            한번더? ReGame?\n\n                                          한번더:Y   메인메뉴로:N");

            ConsoleKey k = Console.ReadKey().Key;
            if (k == ConsoleKey.Y)
			{
                Console.Beep();
                Joker();
			}
			else if (k == ConsoleKey.N)
			{
                Console.Beep();
                Manager.PokerGameView();
            }
			else if(k == ConsoleKey.S)
			{
				//점수판
			}
		}
		
		private static void GameOver()
		{
			Console.Clear();
			Console.WriteLine("\n\n\n\n\n");
			Console.WriteLine("                    ■■■■     ■     ■      ■  ■■■■");
			Console.WriteLine("                    ■          ■■    ■■  ■■  ■");
			Console.WriteLine("                    ■  ■■   ■  ■   ■  ■  ■  ■■■■");
			Console.WriteLine("                    ■    ■  ■■■■  ■      ■  ■");
			Console.WriteLine("                    ■■■■ ■      ■ ■      ■  ■■■■");
			Console.WriteLine("");
			Console.WriteLine("                                 ■■■  ■      ■ ■■■  ■■■");
			Console.WriteLine("                                ■    ■  ■    ■  ■      ■   ■");
			Console.WriteLine("                                ■    ■   ■  ■   ■■■  ■■■");
			Console.WriteLine("                                ■    ■    ■■    ■      ■  ■");
			Console.WriteLine("                                 ■■■      ■     ■■■  ■   ■");

			Console.WriteLine("                               아무도 살아남지 못하였습니다.\n                                       재도전 ☞ y ☜  메인메뉴 ☞ n ☜");

			ConsoleKey k = Console.ReadKey().Key;

            if (k == ConsoleKey.Y)
			{
                Console.Beep();
                Joker();
			}
			else if (k == ConsoleKey.N)
			{
                Console.Beep();
                Manager.PokerGameView();
            }
		}

	
	}
}




