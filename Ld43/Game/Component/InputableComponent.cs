using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.ComponentMessage;
using Game.GameObject;

namespace Game.Component {
	class InputableComponent : BaseComponent{
		public InputableComponent(BaseGameObject owner) : base(owner) {

		}

		public override void ProcessMessage(BaseComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.Input)
				ProcessInput(msg as InputMessage);
		}

		void ProcessInput(InputMessage msg) {

		}
	}
}
