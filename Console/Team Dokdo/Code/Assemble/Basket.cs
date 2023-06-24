using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _Assembley
{
	internal class Basket
	{
		public int x =0, y =0;
		List<int> list = new List<int>();
		public Basket() { 
			y = GameManager.MAP_HEIGHT- 1;
			x= GameManager.MAP_WIDTH/2;
		}
		public void Move(ConsoleKey k)
		{
			if (k == ConsoleKey.LeftArrow) x--;
			else if(k == ConsoleKey.RightArrow) x++;    
			if(x > GameManager.MAP_WIDTH) x--;
			if(x < 0) x = 0;
		}
		public void Print()
		{
			Console.SetCursorPosition(x, y);
			Console.Write("(__)");
		}
		public void Erase()
		{
			Console.SetCursorPosition(x, y);
			Console.Write("    ");
		}
		public void AddAssem(int a)
		{
			list.Add(a);
		}
		public bool CheckAssem(List<int> resultList) { 
			for(int i =0;i <list.Count;i++) {
				if (list[i] != resultList[i])
				{
					return false;
				}
			}
			return true;
		}
		public bool CheckAliveAndWin(List<int> resultList,ref bool isWin)
		{
			if (CheckAssem(resultList))
			{
				if (resultList.Count == list.Count)
				{
					isWin = true;
					return true;
				}
				return true;
			}
			else
			{
				return false;
			}
		}
		public void PrintList(int x, int y)
		{
			Console.SetCursorPosition(x, y);
			for(int i =0;i<list.Count;i++)
			{
				Console.Write("{0} ", list[i]);
			}
		}
	}
}
