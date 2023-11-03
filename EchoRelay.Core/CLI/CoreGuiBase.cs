using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.Core.CLI
{
    public abstract class CoreGuiBase
    {
        public abstract void OnCreate();
        public abstract void OnUpdate(float delta);
        public abstract void OnDestroy();
    }
}
