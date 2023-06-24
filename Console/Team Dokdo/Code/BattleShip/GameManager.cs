using _Map;
using _User;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GameManager
{
    internal class GameManager
    {
        bool inGame = true;
        enum DIR { UP, DOWN, LEFT, RiGHT }
        int userSize = 0;
        //List<User> users = new List<User>();
        User user = new User();
        User com = new User();
        public GameManager()
        {
            Console.Clear();
            SetUserSize();
        }

        private void SetUserSize()
        {
            Console.Write("유저 수를 입력하세요 : ");
            userSize = Int32.Parse(Console.ReadLine());
        }

        void SetCom()
        {
            Ship temp = new Ship();
            temp.AddPosList(10,10);
            com.SetShip(temp);
        }
        public void askShipPos(User user, int i = 1)
        {
            int line = 18;
            Console.SetCursorPosition(35, line++);
            Console.WriteLine("{0}번째 플레이어의 배 위치를 겹치지 않게 지정하세요", i+1);
            Console.SetCursorPosition(40,line++);
            Console.Write("좌표값을 입력하세요 범위 : {0} X : ", Map.MAP_WIDTH);
            int x = GetInputValue(ref inGame, Map.MAP_WIDTH);
            Console.SetCursorPosition(40, line++);
            Console.Write("좌표값을 입력하세요 범위 : {0} Y : ", Map.MAP_HEIGHT);
            int y = GetInputValue(ref inGame, Map.MAP_HEIGHT);
            // TODO : 배 겹치거나 맵 밖이면 오류?
            Console.SetCursorPosition(40, line++);
            Console.Write("위 : 0, 아래 : 1, 왼쪽 : 2, 오른쪽 : 3  ");
            DIR dir = (DIR)GetInputValue(ref inGame,3,0);
            if(inGame == true)
                SetShipInUser(user, x -1, y -1, dir);
        }

        private static void SetShipInUser(User user, int x, int y, DIR dir, int size = 3)
        {
            Ship ship = new Ship();
            for (int i = 0; i < size; i++)
            {
                switch (dir)
                {
                    case DIR.UP:
                        ship.AddPosList(x, y - i);
                        break;
                    case DIR.DOWN:
                        ship.AddPosList(x, y + i);
                        break;
                    case DIR.LEFT:
                        ship.AddPosList(x - i, y);
                        break;
                    case DIR.RiGHT:
                        ship.AddPosList(x + i, y);
                        break;
                }
            }
            user.SetShip(ship);
        }

        private int GetInputValue(ref bool inGame, int max, int min = 1)
        {
            try
            {
                string a = Console.ReadLine();
                if (a == "Q" || a == "q")
                {
                    inGame = false;
                    return 0;
                }
                int result = Int32.Parse(a);
                if (result < min || result > max)
                    throw new Exception();
                return result;
            }
            catch { 
                Console.Write("  다시 입력하세요 ");
                return GetInputValue(ref inGame,max);
            }
        }

        private void CheckHitMiss(User target, int x, int y)
        {
            switch (target.underAttack(x, y))
            {
                case true:
                    Console.WriteLine("명중");
                    break;
                case false:
                    Console.WriteLine("미쓰!");
                    break;
                default:
                    break;
            }
        }
        public bool MainLoop(ref int turn)
        {
            int line = 18;
            int x, y;
            ShipSetting();
            while (inGame)
            {
                turn++;
                Console.SetCursorPosition(40, line++);
                for (int i = 0; i < userSize; i++)
                {
                    if (inGame == true)
                    {
                        Console.SetCursorPosition(40, line++);
                        Console.WriteLine("{0}번 플레이어의 공격 시간입니다. 공격 좌표를 입력해주세요.", i + 1);
                        Console.SetCursorPosition(40, line++);
                        Console.Write("좌표값을 입력하세요 범위 : {0} X : ", Map.MAP_WIDTH);
                        x = GetInputValue(ref inGame, Map.MAP_WIDTH);
                        Console.SetCursorPosition(40, line++);
                        Console.Write("좌표값을 입력하세요 범위 : {0} Y : ", Map.MAP_HEIGHT);
                        y = GetInputValue(ref inGame, Map.MAP_HEIGHT);
                        if (inGame == true)
                        {
                            Console.SetCursorPosition(50, line++);
                            CheckHitMiss(com, x - 1, y - 1);
                            Console.ReadKey();
                            PrintOnOneConsole();
                        }
                        line = 18;
                    }
                }
                if (com.IsAlive() == false)
                {
                    Console.SetCursorPosition(50, 10);
                    Console.Write("유저 승리!");
                    Console.ReadKey();
                    return true;
                }
                Console.SetCursorPosition(50, line++);
                Console.WriteLine("컴퓨터의 공격 시간입니다.");

                for(int i =0;i<3;i++)
                    ComAttack(line);

                if (user.IsAlive() == false)
                {
                    Console.SetCursorPosition(50, ++line);
                    Console.Write("유저 패배!");
                    Console.ReadKey();
                    return false;
                }
            }
            return false;
        }

        private void ComAttack(int line)
        {
            Pos pos = com.ComAttack();
            Console.SetCursorPosition(50, line);
            CheckHitMiss(user, pos.x, pos.y);
            Console.ReadKey();
            PrintOnOneConsole();
        }

        private void ShipSetting()
        {
            PrintOnOneConsole();
            for (int i = 0; i < userSize; i++)
            {
                PrintOnOneConsole();

                askShipPos(user, i);
            }
            PrintOnOneConsole();
            SetShipInUser(com, 2 - 1, 2 - 1, DIR.RiGHT, 5);
            SetShipInUser(com, 6 - 1, 4 - 1, DIR.RiGHT, 5);
            SetShipInUser(com, 3 - 1, 5 - 1, DIR.DOWN, 5);
            SetShipInUser(com, 8 - 1, 8 - 1, DIR.RiGHT, 5);
        }

        private void PrintOnOneConsole()
        {
            Console.Clear();
            Console.SetCursorPosition(23, 1);
            Console.Write("독도");
            user.ShowMap(5);
            Console.SetCursorPosition(90, 1);
            Console.Write("COM");
            com.ShowAttackedMap(70);

            Console.SetCursorPosition(90, 25);
            Console.Write("항복: Q");
        }
    }
}
