using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Containers.Tests
{
    [TestClass()]
    public class AsyncQueueTests
    {
        [TestMethod()]
        public void AsyncQueueTest()
        {
            AsyncQueue<int> queue = new AsyncQueue<int>();

            Thread pusher = new Thread(() =>
            {
                queue.Push(1);
                queue.Push(2);
                queue.Push(3);
                
                Thread.Sleep(100);

                queue.Push(4);
            });

            pusher.Start();
            
            Assert.IsTrue(queue.Pop() == 1, "1");
            Assert.IsTrue(queue.Pop() == 2, "2");
            Assert.IsTrue(queue.Pop() == 3, "3");
            Assert.IsTrue(queue.Pop() == 4, "4");
            //Да, юнит тест может повиснуть, и это не очень красиво, но ошибка тем не менее будет обнаружена

            Thread.Sleep(150);

            Assert.IsFalse(pusher.IsAlive);
        }
    }
}