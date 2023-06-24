using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Map;


namespace _User
{
    internal class User
    {
        Map map = new Map();
        public List<Pos> attackedPoses = new List<Pos>();
        List<Pos> comAttackPosesList = new List<Pos>();
        Random rand = new Random();

        public User() {
            SetComAttackPosList();
        }
        public void SetShip(Ship ship)
        {
            map.AddShip(ship);
        }
        public virtual bool underAttack(int x, int y)
        {
            Pos pos = new Pos(x, y);
            attackedPoses.Add(pos);
            return map.CheckShipAttacked(pos);
        }
        public void ShowAttackedMap(int x)
        {
            map.PrintEnemyMap(attackedPoses,x);
        }

        public Pos ComAttack()
        {
            int rNum = rand.Next(comAttackPosesList.Count);
            Pos pos = comAttackPosesList[rNum];
            comAttackPosesList.RemoveAt(rNum);
            return pos;
        }

        private void SetComAttackPosList()
        {
            for (int i = 0; i < Map.MAP_WIDTH; i++)
            {
                for (int j = 0; j < Map.MAP_HEIGHT; j++)
                {
                    Pos pos = new Pos(i, j);
                    comAttackPosesList.Add(pos);
                }
            }
        }

        public bool IsAlive()
        {
            if(map.CheckAllShipDown())
                return false;
            return true;
        }
        public void ShowMap(int x)
        {
            map.PrintMapOnOne(map.mapArr,x);
        }

    }
}
