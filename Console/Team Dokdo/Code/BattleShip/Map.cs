using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _Map
{
    class Pos
    {
        public int x = 0;
        public int y = 0;
        public Pos(int _x, int _y) { x = _x;y = _y; }

        public override bool Equals(object obj)
        {
            return obj is Pos pos &&
                   x == pos.x &&
                   y == pos.y;
        }

        static public bool operator ==(Pos a, Pos b)
        {
            if (b.x == a.x && b.y == a.y)
                return true;
            return false;
        }
        static public bool operator !=(Pos a, Pos b)
        {
            return !(a == b);
        }
    }
    class Ship
    {
        public List<Pos> posList; 
        public Ship() {
            posList = new List<Pos>();
        }
        public void AddPosList(int x, int y)
        {
            posList.Add(new Pos(x,y));
        }
    }
    internal class Map
    {
        public static int MAP_HEIGHT = 13;
        public static int MAP_WIDTH = 13;

        enum MapCondition { NONE = 0, HIT, MISS, SHIP }
        List<Ship> shipList;

        public int[,] mapArr = new int[MAP_WIDTH, MAP_HEIGHT];

        public Map()
        {
            shipList = new List<Ship>();
        }
        public void AddShip(Ship ship)
        {
            shipList.Add(ship);
            SetShipMap();
        }
        public void RemoveShip(Ship ship)
        {
            shipList.Remove(ship);
        }

        public void ShipClear()
        {
            shipList.Clear();
        }

        public void SetShipMap()
        {
            for(int i =0;i<shipList.Count;i++)
            {
                for(int j = 0; j < shipList[i].posList.Count;j++){
                    mapArr[shipList[i].posList[j].x, shipList[i].posList[j].y] = (int)MapCondition.SHIP;
                }
            }
        }

        public void PrintEnemyMap(List<Pos> pos,int x)
        {
            int[,] tempMap = new int[MAP_WIDTH, MAP_HEIGHT];

            SetMapNone(tempMap);

            for(int i = 0; i < pos.Count; i++)
            {
                if (mapArr[pos[i].x, pos[i].y] == (int)MapCondition.HIT)
                    tempMap[pos[i].x, pos[i].y] = (int)MapCondition.HIT;
                else
                    tempMap[pos[i].x, pos[i].y] = (int)MapCondition.MISS;
            }

            PrintMapOnOne(tempMap,x);
        }

        public bool CheckAllShipDown()
        {
            for(int i = 0; i < MAP_WIDTH; i++)
            {
                for(int j = 0; j < MAP_HEIGHT; j++)
                {
                    if (mapArr[i, j] == (int)MapCondition.SHIP)
                        return false;
                }
            }
            return true;
        }

        public bool CheckShipAttacked(Pos pos)
        {
            if (mapArr[pos.x, pos.y] == (int)MapCondition.SHIP || mapArr[pos.x, pos.y] == (int)MapCondition.HIT)
            {
                mapArr[pos.x, pos.y] = (int)MapCondition.HIT;
                return true;
            }
            else
            {
                mapArr[pos.x, pos.y] = (int)MapCondition.MISS;
                return false;
            }
        }

        private static void SetMapNone(int[,] tempMap)
        {
            for (int i = 0; i < MAP_WIDTH; i++)
            {
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    tempMap[i, j] = (int)MapCondition.NONE;
                }
            }
        }

        private void PrintMap(int[,] mapArr)
        {
            Console.Write(" ");

            Console.WriteLine();
            for (int i = 0; i < MAP_WIDTH ; i++)
            {
                Console.Write($"{i + 1,3}");
            }
            Console.WriteLine();
            for (int j = 0; j < MAP_HEIGHT; j++)
            {
                Console.Write($"{j + 1,2}");
                for (int i = 0;i < MAP_WIDTH; i++)
                {
                    if (mapArr[i, j] == (int)MapCondition.NONE)
                        Console.Write("□ ");
                    else if (mapArr[i, j] == (int)MapCondition.HIT)
                        Console.Write("▼ ");
                    else if (mapArr[i, j] == (int)MapCondition.MISS)
                        Console.Write("   ");
                    else if (mapArr[i, j] == (int)MapCondition.SHIP)
                        Console.Write("■ ");
                }
                Console.WriteLine();
            }
        }

        public void PrintMap()
        {
            Console.Write(" ");
            for (int i =0;i<MAP_WIDTH ;i++)
            {
                Console.Write($"{i + 1,3}");
            }
            Console.WriteLine();

            for (int j = 0; j < MAP_HEIGHT; j++)
            {
                Console.Write($"{j+1,2}");
                for (int i = 0; i < MAP_WIDTH; i++)
                {
                    if (mapArr[i, j] == (int)MapCondition.NONE)
                        Console.Write("□ ");
                    else if (mapArr[i, j] == (int)MapCondition.HIT)
                        Console.Write("▼ ");
                    else if (mapArr[i, j] == (int)MapCondition.MISS)
                        Console.Write("   ");
                    else if (mapArr[i, j] == (int)MapCondition.SHIP)
                        Console.Write("■ ");
                }
                Console.WriteLine();
            }
        }

        public void PrintMapOnOne(int[,] mapArr , int x = 10)
        {
            int y = 2;
            Console.CursorTop = ++y;
            Console.CursorLeft = x;
            Console.Write(" ");
            for (int i = 0; i < MAP_WIDTH; i++)
            {
                Console.Write($"{i + 1,3}");
            }
            Console.CursorTop = ++y;
            Console.CursorLeft = x;

            for (int j = 0; j < MAP_HEIGHT; j++)
            {
                Console.Write($"{j + 1,2}");
                for (int i = 0; i < MAP_WIDTH; i++)
                {
                    if (mapArr[i, j] == (int)MapCondition.NONE)
                        Console.Write("□ ");
                    else if (mapArr[i, j] == (int)MapCondition.HIT)
                        Console.Write("▼ ");
                    else if (mapArr[i, j] == (int)MapCondition.MISS)
                        Console.Write("   ");
                    else if (mapArr[i, j] == (int)MapCondition.SHIP)
                        Console.Write("■ ");
                }
                Console.CursorTop = ++y;
                Console.CursorLeft = x;
            }
            Console.CursorTop = ++y;
            Console.CursorLeft = x;

        }
    }
}
