using System.Collections.Generic;
using System.Threading;

namespace Containers
{
    /// <summary>
    /// Асинхронная очередь, у которой всего 2 метода. Аскетичный дизайн в духе Apple пришел в программинг.
    /// </summary>
    /// <typeparam name="T">Тип элемента очереди</typeparam>
    public class AsyncQueue <T>
    {
        /// <summary>
        /// Период, с которым в случае запроса на выдачу элемента, пустая очередь "просыпается" проверить не появился ли он.
        /// </summary>
        public int PopWaitPeriodMsec = 1;

        private readonly Mutex _mutex;
        private readonly Queue<T> _queue;

        public AsyncQueue()
        {   
            _queue = new Queue<T>();
            _mutex = new Mutex();
        }

        /// <summary>
        /// Добавить элемент в очередь
        /// </summary>
        /// <param name="arg">Элемент для добавления</param>
        public void Push(T arg)
        {
            _mutex.WaitOne();
            _queue.Enqueue(arg);
            _mutex.ReleaseMutex();
        }

        /// <summary>
        /// Извлечь элемент из очереди
        /// </summary>
        /// <returns>Извлечённый элемент</returns>
        public T Pop()
        {
            while (true) //Условия остановки - для слабых духом, ведь мы не обязательно nullable или T()
            {
                _mutex.WaitOne();

                if (_queue.Count > 0)
                {
                    T res = _queue.Dequeue();
                    _mutex.ReleaseMutex();
                    return res;
                }
                else
                {
                    _mutex.ReleaseMutex();
                    Thread.Sleep(PopWaitPeriodMsec);
                }
            }
        }
    }
}
