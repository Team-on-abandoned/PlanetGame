using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.Common;
using Game.Component;
using Game.ComponentMessage;
using Game.GameObject;
using Game.Interfaces;

namespace Game {
	class GameContext {
		#region Singletone
		static GameContext gameContext;

		static public GameContext GetGCState() => gameContext;
		static GameContext() => gameContext = new GameContext();

		private GameContext() {
			isRunning = true;
			gameObjects = new List<BaseGameObject>();
			graphicsRenderers = new List<IGraphics>();
			inputListeners = new List<IInputListener>();
			renderQueue = new ConcurrentQueue<GameObjectRenderState>();
		}
		#endregion

		#region Render 

		ConcurrentQueue<GameObjectRenderState> renderQueue;
		List<IGraphics> graphicsRenderers;

		public void SendRenderState(GameObjectRenderState msg) => renderQueue.Enqueue(msg);
		bool TryReadRenderState(out GameObjectRenderState msg) => renderQueue.TryDequeue(out msg);

		#endregion

		#region Input
		List<IInputListener> inputListeners;
		#endregion

		#region Game
		List<BaseGameObject> gameObjects;

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

			DisposeAllGame();
		}

		void Update() {
			ReadInput();
			ProcessMessages();
		}

		void ReadInput() {
			//foreach (var go in gameObjects)
			//	go.SendMessage(new InputMessage());
		}

		void ProcessMessages() {
			foreach (var i in gameObjects)
				i.Process();
		}

		void Display(double interpolation) {
			foreach (var go in gameObjects)
				go.SendMessage(new RenderMessage(interpolation));
		}

		void DisposeAllGame() {
			foreach (var go in gameObjects)
				go.Dispose();
			gameObjects.Clear();

			inputListeners.Clear();
			graphicsRenderers.Clear();
			while (TryReadRenderState(out GameObjectRenderState rs)) ;
		}

		#endregion
	}
}
