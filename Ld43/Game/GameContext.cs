using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game {
	class GameContext {
		#region Singletone
		static GameContext gameContext;

		static public GameContext GetGCState() => gameContext;
		static GameContext() => gameContext = new GameContext();

		private GameContext() {
			isRunning = true;
		}
		#endregion

		bool isRunning;

		public void StartGame() {
			ProcessGame();
		}

		public void StopGame() {
			isRunning = false;
		}

		void ProcessGame() {
			const int tps = 25;
			const int skipTicks = 1000 / tps;
			const int maxFrameskip = 5;

			int next_game_tick = Environment.TickCount;
			int loops;
			double interpolation;

			while (isRunning) {
				loops = 0;
				while (Environment.TickCount > next_game_tick && loops < maxFrameskip) {
					Update();
					next_game_tick += skipTicks;
					loops++;
				}

				interpolation = (double)(Environment.TickCount + skipTicks - next_game_tick) / skipTicks;
				Display(interpolation);
			}
		}

		void Update() {
			ReadPlayersInput();
			ProcessMessages();
		}

		void ReadPlayersInput() {

		}

		void ProcessMessages() {

		}

		void Display(double interpolation) {

		}
	}
}
