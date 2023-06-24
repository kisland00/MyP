using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CreateMaze
{
    internal class MazeCreator
    {
        Random rand = new Random();
        static public int MAP_WIDTH = 29;
        static public int MAP_HEIGHT = 29;
        public enum Direction { LEFT = 0, UP, RIGHT, DOWN }
        public enum MapCondition { WALL = 0, EMPTY, VISITED, CHARACTER, GALL}
        int[,] DIR = { {0,-2},{0,2},{-2,0},{2,0} };
        public int[,] map = new int[MAP_WIDTH, MAP_HEIGHT];

        void shuffleArray(int[] array)
        {
            int i, r, temp;

            for (i = 0; i < (array.Length - 1); ++i)
            {
                r = i + (rand.Next(array.Length - i));
                temp = array[i];
                array[i] = array[r];
                array[r] = temp;
            }
        }

        bool inRange(int x, int y)
        {
            return (x < MAP_WIDTH - 1 && x > 0) && (y < MAP_HEIGHT - 1 && y > 0);
        }

        void generateMap(int x, int y, int[,] map)
        {
            int i, nx, ny;
            int[] directions = { 0, 1, 2, 3 };

            map[x, y] = (int)MapCondition.VISITED;

            shuffleArray(directions);

            for (i = 0; i < 4; i++)
            {
                nx = x + DIR[directions[i],0];
                ny = y + DIR[directions[i],1];

                if (inRange(nx, ny) && map[nx, ny] == (int)MapCondition.WALL)
                {
                    generateMap(nx, ny, map);
                    if (ny != y)
                        map[x, (ny + y) / 2] = (int)MapCondition.EMPTY;
                    else
                        map[(x + nx) / 2, y] = (int)MapCondition.EMPTY;
                    map[nx, ny] = (int)MapCondition.EMPTY;
                }
            }
        }

        void getRandomStartingPoint(out int x, out int y)
        {
            x = 1 + rand.Next(MAP_WIDTH - 1);
            y = 1 + rand.Next(MAP_HEIGHT - 1);
            if (x % 2 == 0)
                x--;
            if (y % 2 == 0)
                y--;
        }
        public void Generate()
        {
            SetMapWall();

            int x, y;
            getRandomStartingPoint(out x, out y);

            generateMap(x, y, map);

            for (int i = 1; i < MAP_HEIGHT-1; i++)
            {
                map[1,i] = (int)MapCondition.EMPTY;
            }
            for (int i = 1; i < MAP_WIDTH-1; i++)
            {
                map[i,1] = (int)MapCondition.EMPTY;
            }
            map[MAP_WIDTH-2,MAP_HEIGHT-2] = (int)MapCondition.GALL;

            map[1,1] = (int)MapCondition.CHARACTER;

            //PrintMap();
        }

        private void SetMapWall()
        {
            for (int i = 0; i < MAP_WIDTH; i++)
            {
                for (int j = 0; j < MAP_HEIGHT; j++)
                {
                    map[i, j] = (int)MapCondition.WALL;
                }
            }
        }

        public void PrintMap()
        {
            for (int i = 0; i < MAP_WIDTH; ++i)
            {
                for (int j = 0; j < MAP_HEIGHT; ++j)
                {
                    //Console.Write(map[i, j] == (int)MapCondition.WALL ? "ⓦ" : "  ");
                    switch (map[j, i]) {
                        case (int)MapCondition.WALL:
                            Console.Write("□");
                            break;
                        case (int)MapCondition.CHARACTER:
                            Console.Write("★");
                            break;
                        case (int)MapCondition.GALL:
                            Console.Write("○");
                            break;
                        default:
                            Console.Write("  ");
                            break;
                    }

                }
                Console.WriteLine();
            }
        }

        //private string GetRandomString()
        //{
        //    char c = 'A';
        //    c += (char)rand.Next(5);
        //    string a = "" + c;
        //    a += "" + c + (char)rand.Next(5);
        //    return a;
        //}

    }
}
