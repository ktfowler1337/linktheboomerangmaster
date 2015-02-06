using System;

namespace LinkTheBoomerangMaster
{
	public class Level
	{
		private GameController _game;
		private int EnemyHeight = 40;
		private int EnemyWidth = 50;
		Random random;

        public bool WizardHit = false;
        public bool ArcherHit = false;

		public Level (GameController game)
		{
			_game = game;
			random = new Random();
		}

		public void Generate_Level(int level)
		{
			if (level==1)
			{
				for (int x=25; x < 550; x=x+EnemyWidth) {
					Enemy temp = new Enemy(_game, x, 120, "wizard");
				}

                for (int x = 25; x < 550; x = x + EnemyWidth)
                {
					Enemy temp = new Enemy(_game, x, 120 +(EnemyHeight), "archer");
				}

                for (int x = 25; x < 550; x = x + EnemyWidth)
                {
					Enemy temp = new Enemy(_game, x, 120 +(EnemyHeight*2), "soldier");
				}
			}
		}
	}
}

