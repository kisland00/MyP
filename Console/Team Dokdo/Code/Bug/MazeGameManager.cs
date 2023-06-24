using _CreateMaze;
using _MiroCharacter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _MazeGame
{
    internal class MazeGame
    {
        MazeCreator creator = new MazeCreator();
        Character user = new Character();

        public MazeGame()
        {
            SetMap();
        }

        void SetMap()
        {
            creator.Generate();
        }

        bool MoveCharacter()
        {
            creator.map[user.x, user.y] = (int)MazeCreator.MapCondition.EMPTY;
            user.Run();
            if (user.x <=0 || user.y <=0 || user.x >=MazeCreator.MAP_WIDTH || user.y >= MazeCreator.MAP_HEIGHT) {
                return false;
            }
            if (creator.map[user.x, user.y] == (int)MazeCreator.MapCondition.WALL)
                return false;
            if (creator.map[user.x, user.y] == (int)MazeCreator.MapCondition.GALL)
                return false;
            creator.map[user.x, user.y] = (int)MazeCreator.MapCondition.CHARACTER;
            return true;
        }
        public List<TimeSpan> MainLoop(ref int turn)
        {
            turn = 0;
            List<TimeSpan> timeList = InGameLoop(ref turn);
            ConsoleKeyInfo input = Console.ReadKey();
            if(turn == 4)
            {
                //Congrate
            }
            if (input.Key == ConsoleKey.R)
            {
                timeList.Clear();
                MainLoop(ref turn);
            }
            else if (input.Key == ConsoleKey.Q)
            {
                return timeList;
            }
            return timeList;
        }

        private List<TimeSpan> InGameLoop(ref int turn)
        {
            DateTime time = DateTime.Now;
            Console.CursorVisible = false;
            bool inGame = true;
            int i = 0;
            List<TimeSpan> timeList = new List<TimeSpan>();

            Task.Factory.StartNew(() =>
            {
                while (inGame)
                {
                    ConsoleKeyInfo k = Console.ReadKey();
                    while (inGame == true && k.Key != ConsoleKey.LeftArrow && k.Key != ConsoleKey.RightArrow && k.Key != ConsoleKey.Spacebar
                            && k.Key != ConsoleKey.Q && k.Key != ConsoleKey.R) ;
                    if (k.Key == ConsoleKey.LeftArrow || k.Key == ConsoleKey.RightArrow || k.Key == ConsoleKey.Spacebar)
                        user.DirChange(k);
                    else if (k.Key == ConsoleKey.R)
                    {
                        i = 0;
                        ReSet();
                        time = DateTime.Now;
                    }
                    else if (k.Key == ConsoleKey.Q)
                    {
                        inGame = false;
                        break;
                    }
                }
            });

            while (inGame)
            {
                if (i == 4)
                {
                    turn = ++i;
                    inGame = false;
                    return timeList;
                }
                Console.Clear();
                creator.PrintMap();


                Console.CursorLeft = 60;
                Console.CursorTop = 1;
                Console.Write("{0}번째 판", i + 1);

                Console.CursorLeft = 90;
                Console.CursorTop = 1;

                Console.Write((DateTime.Now - time));

                Console.CursorLeft = 60;
                Console.CursorTop = 27;
                Console.Write("재시작 : RR");

                Console.CursorLeft = 97;
                Console.CursorTop = 27;
                Console.Write("종료 : QQ");

                user.PrintDirection();

                Thread.Sleep(GetTime(i));
                if (!MoveCharacter())
                {
                    if (creator.map[user.x, user.y] == (int)MazeCreator.MapCondition.GALL)
                    {
                        ReSet();

                        time = AddTime(time, timeList);
                        i++;
                        continue;
                    }
                    Console.CursorLeft = 72;
                    Console.CursorTop = 12;
                    Console.WriteLine("목적지에 도달하지 못하였습니다!");
                    time = AddTime(time, timeList);
                    inGame = false;
                }
            }
            turn = i;
            return timeList;
        }

        private void ReSet()
        {
            user.x = 1;
            user.y = 1;
            user.SetDir();
            SetMap();
        }

        private static DateTime AddTime(DateTime time, List<TimeSpan> timeList)
        {
            timeList.Add(DateTime.Now - time);
            time = DateTime.Now;
            return time;
        }

        private int GetTime(int i)
        {
            switch (i)
            {
                case 0:
                    return 300;
                case 1:
                    return 200;
                case 3:
                    return 100;
                case 4:
                    return 80;
            }
            return 200;
        }
    }
}
