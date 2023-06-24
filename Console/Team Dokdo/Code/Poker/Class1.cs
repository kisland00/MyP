using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace P_com1
{
    internal class Computer
    {
        public Random rand = new Random();
        public List<string> CardList = new List<string>();
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        public void Computer1()//카드를 리스트에 넣기
        {


            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    switch (i)
                    {
                        case 0:
                            switch (j)
                            {
                                case 1:
                                    CardList.Add("♠Ace");
                                    break;
                                case 11:
                                    CardList.Add("♠Jack");
                                    break;
                                case 12:
                                    CardList.Add("♠Queen");
                                    break;
                                case 13:
                                    CardList.Add("♠King ");
                                    break;
                                default:
                                    CardList.Add($"♠{j}");
                                    break;
                            }
                            break;
                        case 1:
                            switch (j)
                            {
                                case 1:
                                    CardList.Add("♣Ace");
                                    break;
                                case 11:
                                    CardList.Add("♣Jack");
                                    break;
                                case 12:
                                    CardList.Add("♣Queen");
                                    break;
                                case 13:
                                    CardList.Add("♣King ");
                                    break;
                                default:
                                    CardList.Add($"♣{j}");
                                    break;
                            }
                            break;
                        case 2:
                            switch (j)
                            {
                                case 1:
                                    CardList.Add("◆Ace");
                                    break;
                                case 11:
                                    CardList.Add("◆Jack");
                                    break;
                                case 12:
                                    CardList.Add("◆Queen");
                                    break;
                                case 13:
                                    CardList.Add("◆King ");
                                    break;
                                default:
                                    CardList.Add($"◆{j}");
                                    break;
                            }
                            break;
                        case 3:
                            switch (j)
                            {
                                case 1:
                                    CardList.Add("♥Ace");
                                    break;
                                case 11:
                                    CardList.Add("♥Jack");
                                    break;
                                case 12:
                                    CardList.Add("♥Queen");
                                    break;
                                case 13:
                                    CardList.Add("♥King ");
                                    break;
                                default:
                                    CardList.Add($"♥{j}");
                                    break;
                            }
                            break;
                    }
                }
            }
        }
        public string RandomNum()//랜덤카드뽑기
        {
            return (string)CardList[rand.Next(CardList.Count)];
        }
        public void EnterKey()//카드3장뽑고 해당카드 삭제
        {

            //if (keyInfo.Key == ConsoleKey.Enter)
            //{
                string str1 = RandomNum();
                string str2 = RandomNum();
                string str3 = RandomNum();

                Console.Write("\n                                               ["+ str1+    ","    +str2+    ","    +str3+"]\n"); 

                CardList.Remove(str1);
                CardList.Remove(str2);                
                CardList.Remove(str3);
            //};

        }
    }
}
