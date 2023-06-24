using Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = "Helltaker - 헬테이커 브금 OST _Mittsies - Vitality__1.wav";
            sp.Play();
            One m = new One();
            Console.Clear();
            One.Banner();
        }
	}
}
