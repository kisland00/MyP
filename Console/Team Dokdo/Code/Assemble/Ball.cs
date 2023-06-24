using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _Assembley
{
	internal class Ball
	{
		public bool isAlive = true;
		public int x , y = 0;
		public int rand = 0;
		public Ball(Random random)
		{
			ReSet(random);
		}

		public void ReSet(Random random)
		{
			isAlive = true;
			y = 0;
			x = random.Next(GameManager.MAP_WIDTH);
			rand = random.Next(2);
		}

		public void SetX(int _x) { x = _x; }
		public void Move()
		{
			
			if (!isAlive)
			{
				x = 90;
				y = 0;
			}	
			else if(isAlive)
				y++;		
		}
		public bool IsCollapsed(int _x, int _y)
		{
			if (isAlive == false)
				return false;
			if(y == _y && (x == _x+1 || x == _x||x==_x+2||x==_x+3))
			{
				return true;
			}
			return false;
		}
	}
}
