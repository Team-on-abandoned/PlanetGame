using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ComponentMessage {
	class RenderMessage : BaseComponentMessage{
		public double Interpolation{ get; private set; }

		public RenderMessage(double interpolation) : base(ComponentMessageType.Render) {
			Interpolation = interpolation;
		}
	}
}
