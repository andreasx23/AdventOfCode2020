using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day22
{
    public class Game
    {
        public Player player;
        public Boss boss;
        public bool GameOver => player.Hp <= 0 || boss.Hp <= 0;
        public bool IsPlayerWinner => player.Hp > 0;

        public Game(Player player, Boss boss)
        {
            this.player = player;
            this.boss = boss;
        }
    }
}
