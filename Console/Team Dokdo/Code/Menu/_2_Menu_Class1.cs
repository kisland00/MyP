using Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTool
{
	internal class _2_Menu_Class1
	{
		public static void Menu()
		{
			int user = 0;

			Console.Clear();
			Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");
			Console.WriteLine("★                                                                                                                  ★");
			Console.WriteLine("★                                                                                                                  ★");
			Console.WriteLine("★                             ■■■■    ■■■    ■■■   ■■■                                                ★");
			Console.WriteLine("★                             ■         ■    ■  ■    ■  ■    ■                                              ★");
			Console.WriteLine("★                             ■  ■■   ■    ■  ■    ■  ■    ■                                              ★");
			Console.WriteLine("★                             ■    ■   ■    ■  ■    ■  ■    ■                                              ★");
			Console.WriteLine("★                             ■■■■    ■■■    ■■■   ■■■                                                ★");
			Console.WriteLine("★                                                                                                                  ★");
			Console.WriteLine("★                                             ■       ■    ■    ■■■   ■   ■                                ★");
			Console.WriteLine("★                                             ■       ■    ■   ■    ■  ■  ■                                 ★");
			Console.WriteLine("★                                             ■       ■    ■   ■        ■■                                   ★");
			Console.WriteLine("★                                             ■       ■    ■   ■    ■  ■  ■                                 ★");
			Console.WriteLine("★                                             ■■■■   ■■      ■■■   ■   ■                                ★");
			Console.WriteLine("★                                                                                                                  ★");
			Console.WriteLine("★                                                 게임을 선택하시오                                                ★");
			Console.WriteLine("★                                                                                                                  ★");
			Console.WriteLine("★                                                  『같이 놀꺼야』                                                 ★");
			Console.WriteLine("★                                                                                                                  ★");
			Console.WriteLine("★                                                ①【게임을 시작하지】                                             ★");
			Console.WriteLine("★                                                ②【독도를 휘날리며】                                             ★");
			Console.WriteLine("★                                                                                                                  ★");
			Console.WriteLine("★                                                  『혼자 놀꺼야』                                                 ★");
			Console.WriteLine("★                                                                                                                  ★");
			Console.WriteLine("★                                                ③【구성! 기계어!】                                               ★");
			Console.WriteLine("★                                                ④【Bug를 피해라!】                                               ★");
			Console.WriteLine("★                                                                                                                  ★");
			Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");

			user = Int32.Parse(Console.ReadLine());

			switch(user)
			{
				case 1:
                    Console.Beep();
                    Manager.PokerGameView();
                    break;
				case 2:
                    Console.Beep();
                    _4_ship_Pan.Ship();
					break;
				case 3:
                    Console.Beep();
                    _5_ball_Pan.Basket();
					break;
				case 4:
                    Console.Beep();
                    _6_bug_Pan.Bugbug();
					break;
			}
		}
	} 
}
