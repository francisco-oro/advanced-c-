using System;

namespace _02_Reflection
{
    public class Demo
    {
        public event EventHandler<int> MyEvent;

        public void Handler(object sender, int e)
        {
            Console.WriteLine($"I just got {e}");
        }

        public static void Demoo(string[] args)
        {
            var demo = new Demo();

            var eventInfo = typeof(Demo).GetEvent("MyEvent");
            var handlerMethod = demo.GetType().GetMethod("Handler");

            var handler = Delegate.CreateDelegate(
                eventInfo.EventHandlerType,
                null,
                handlerMethod);

            eventInfo.AddEventHandler(demo, handler);
            demo.MyEvent?.Invoke(null, 321);
        }
    }
}
