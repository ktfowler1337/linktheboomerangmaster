using System;

namespace LinkTheBoomerangMaster
{
	public class Level
	{
		private GameController _game;
		private int EnemyHeight = 40;
		private int EnemyWidth = 50;
		private int GameWidth = 550;
		private int GameTopHeight = 120;
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
			if (level == 1) {
				Create_Row ("wizard", 0);
				Create_Row ("archer", 1);
				Create_Row ("soldier", 2);
			} else {
				Random_Level (level + 2);
			}
		}

		public void Random_Level(int numberOfRows)
		{

			for(int row = 0; row < numberOfRows; row++)
			{
				Create_Row("random", row);
			}
		}

		public void Create_Row(string rowType, int rowNumber)
		{
			int typePicker = 0;
			string classPicker = "";
			//if soldier create row of soldiers
			if (rowType != "random") {
				for (int x=25; x < GameWidth; x=x+EnemyWidth) {
					Enemy temp = new Enemy (_game, x, GameTopHeight + (EnemyHeight * (rowNumber)), rowType, random);
				}
			} else {

				for (int x=25; x < GameWidth; x=x+EnemyWidth) {
					typePicker = random.Next (1, 10);
					if (typePicker <= 3) {
						classPicker = "soldier";
					}
					if(typePicker >3 && typePicker <= 6){
						classPicker = "archer";
					}
					if(typePicker > 6 && typePicker <= 8){
						classPicker = "wizard";
					}
					if(typePicker <= 8){
						Enemy temp = new Enemy (_game, x, GameTopHeight + (EnemyHeight * (rowNumber)), classPicker, random);
					}

				}

			
			
			}
		}
	}
}

