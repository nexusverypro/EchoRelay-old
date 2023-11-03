using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.Core.CLI
{
    public static class CoreGui
    {
        private static async Task GuiUpdateTask(CoreGuiBase coreGui, CancellationToken cancellationToken)
        {
            const float DELTA = 0.0333333f;

            coreGui.OnCreate();

            // We can update the GUI while waiting for the cancellation
            // If we've received a cancel event, discard the loop and destroy the GUI
            while (!cancellationToken.IsCancellationRequested)
            {
                coreGui.OnUpdate(DELTA);
                await Task.Delay((int)(DELTA * 1000));
            }
            coreGui.OnDestroy();
        }

        public static void Load<TGui>() 
            where TGui : CoreGuiBase
        {
            var activation = Activator.CreateInstance<TGui>();
            m_CoreGuiUpdateTask = Task.Run(async () => await GuiUpdateTask(activation, m_CoreGuiCancellationSource.Token));
        }

        public static void Unload<TGui>()
            where TGui : CoreGuiBase
        {
            m_CoreGuiCancellationSource.Cancel();
            m_CoreGuiUpdateTask = null;
        }

        private static Task? m_CoreGuiUpdateTask;
        private static CancellationTokenSource m_CoreGuiCancellationSource = new CancellationTokenSource();
    }
}
