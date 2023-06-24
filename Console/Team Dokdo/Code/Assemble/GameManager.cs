using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _Assembley
{
	internal class GameManager
	{
		bool inGame = true;
		bool isWin = false;
		Random random = new Random();
		static public int MAP_WIDTH = 50;
		static public int MAP_HEIGHT = 25;
		TimeSpan time;
		int result;
		Basket basket = new Basket();
		Ball[] balls = new Ball[12];
		List<int> resultList = new List<int>();

		private void PrintResultList()
		{
			Console.SetCursorPosition(75, 9);
			for (int i = 0; i < resultList.Count; i++)
			{
				Console.Write("{0} ", resultList[i]);
			}
		}

		private void PrintBasketList()
		{
			basket.PrintList(75, 10);
		}

		public void SetList()
		{
			resultList.Clear();
			for(int i =0;i<random.Next(5,9);i++)
			{
				resultList.Add(random.Next(2));
			}
		}

		public void MainLoop()
		{
			basket.Print();
			SetList();
			PrintResultList();
			Console.CursorVisible = false;
			Task.Factory.StartNew(() =>
			{
				GenBallAndMove();
			});
			MoveBasket();
			PrintGameEnd();
			Console.ReadKey();
		}

		private void PrintGameEnd()
		{
			Console.Clear();
			Console.SetCursorPosition(30, 10);
			Console.WriteLine("생존 시간 : " + time.ToString());
			Console.SetCursorPosition(30, 11);
			Console.WriteLine(isWin?"성공!!" : "실패");
		}

		private void MoveBasket()
		{
			while (inGame)
			{
				ConsoleKeyInfo k = Console.ReadKey();
				if (k.Key == ConsoleKey.LeftArrow || k.Key == ConsoleKey.RightArrow)
				{
					basket.Erase();
					basket.Move(k.Key);
					basket.Print();
				}
			}
		}

		private void GenBallAndMove()
		{
			DateTime exTime = DateTime.Now;
			while (inGame)
			{
				time = DateTime.Now - exTime;
				Console.SetCursorPosition(90, 1);
				Console.Write(time);
				SetBall();
				for (int i = 0; i < balls.Length; i++)
				{
					if (balls[i] == null || balls[i].isAlive == false)
						continue;
					if (balls[i].isAlive == true)
					{
						Console.CursorLeft = balls[i].x;
						Console.CursorTop = balls[i].y;
						Console.Write("{0}", balls[i].rand);
					}
					if (balls[i].y > MAP_HEIGHT)
					{
						balls[i].isAlive = false;
					}
				}
				Thread.Sleep(100);
				for (int i = 0; i < balls.Length; i++)
				{
					if (balls[i] == null || balls[i].isAlive == false)
						continue;
					if (balls[i].isAlive == true)
					{
						Console.CursorLeft = balls[i].x;
						Console.CursorTop = balls[i].y;
						Console.Write(' ');
						balls[i].Move();
						if (balls[i].IsCollapsed(basket.x, basket.y))
						{
							basket.AddAssem(balls[i].rand);
							PrintBasketList();
							if (!(basket.CheckAliveAndWin(resultList, ref isWin)))
								inGame = false;
							if (isWin == true) { inGame = false; }
						}
					}
					if (balls[i].y > MAP_HEIGHT)
					{
						balls[i].isAlive = false;
					}
				}
			}
		}

		private void EraseLists()
		{
			Console.SetCursorPosition(75, 10);
			for (int i = 0; i < resultList.Count * 3; i++)
				Console.Write(' ');
			Console.SetCursorPosition(75, 2);
			for (int i = 0; i < resultList.Count * 3; i++)
				Console.Write(' ');
		}

		private void SetBall()
		{
			for(int i =0;i < balls.Length;i++)
			{
				if (balls[i] == null)
				{
					balls[i] = new Ball(random);
					return;
				}
				if (balls[i].isAlive == false)
				{
					balls[i].ReSet(random);
				}
			}
		}


	}
}
