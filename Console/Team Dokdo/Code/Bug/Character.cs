using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _MiroCharacter
{
	internal class Character
	{
		char ch = '★';
		public int x = 1, y = 1; // 가로 세로축
		int[,] dirArr = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
		int dirNum = 0;
		public void DirChange(ConsoleKeyInfo key)
		{
			if (key.Key == ConsoleKey.LeftArrow)
			{
				dirNum = dirNum - 1;
			}
			else if (key.Key == ConsoleKey.RightArrow)
			{
				dirNum = dirNum + 1;
			}
			else if (key.Key == ConsoleKey.Spacebar)
			{
				dirNum = dirNum - 2;
			}
			if (dirNum <= -1) dirNum += 4;
			if (dirNum >= 4) dirNum -= 4;
		}

        public void PrintDirection()
        {
			Console.SetCursorPosition(85, 14);
            switch (dirNum) {
				case 0: Console.Write("→");
					break;
				case 1:
                    Console.Write("↓");
                    break;
				case 2:
                    Console.Write("←");
                    break;
				case 3:
                    Console.Write("↑");
                    break;
			}
        }

        public void Run()
		{
			x += dirArr[dirNum,0];
			y += dirArr[dirNum,1];
		}

        internal void SetDir()
        {
			dirNum = 0;
        }
    }
}
