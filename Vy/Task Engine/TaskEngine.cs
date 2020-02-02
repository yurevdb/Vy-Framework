using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Vy
{
    /// <summary>
    /// An engine to run task and manage the lifetime of those tasks.
    /// </summary>
    public static class TaskEngine
    {
        #region Private Members

        /// <summary>
        /// The local list of all tasks managed by the <see cref="TaskEngine"/>
        /// </summary>
        private static ObservableCollection<KeyValuePair<string, Task>> mTaskList = new ObservableCollection<KeyValuePair<string, Task>>();

        /// <summary>
        /// The local <see cref="CancellationTokenSource"/> to enable cancellation for any <see cref="Task"/> created without a specified <see cref="CancellationToken"/>
        /// </summary>
        private static CancellationTokenSource mCancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// The local <see cref="CancellationToken"/> used for cancellation of all <see cref="Task"/>s
        /// </summary>
        private static CancellationToken mCancellationToken = mCancellationTokenSource.Token;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the collection of tasks managed by the <see cref="TaskEngine"/>
        /// </summary>
        public static ObservableCollection<KeyValuePair<string, Task>> Tasks => mTaskList;

        /// <summary>
        /// Gets wether or not any <see cref="Task"/> is still in progress of being executed
        /// </summary>
        public static bool InProgress => !Tasks.Select(kv => kv.Value).Any(t => t.Status == TaskStatus.WaitingToRun || t.Status == TaskStatus.Running);

        #endregion

        #region Public Methods

        #region Task Creation

        /// <summary>
        /// Start a <see cref="Task"/>
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to execute</param>
        /// <returns></returns>
        public static Task Run(Action action)
        {
            // Create the task with the global cancellationtoken
            var t = Task.Run(() =>
            {
                if (!mCancellationToken.IsCancellationRequested)
                    action();
            }, mCancellationToken);

            // Add the task to the collection of tasks
            Tasks.Add(new KeyValuePair<string, Task>(Guid.NewGuid().ToString(), t));

            // Return the task
            return t;
        }

        /// <summary>
        /// Start a <see cref="Task"/>
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to execute</param>
        /// <param name="key">The key for the task</param>
        /// <returns></returns>
        public static Task Run(String key, Action action)
        {
            // Create the task with the global cancellationtoken
            var t = Task.Run(() =>
            {
                if (!mCancellationToken.IsCancellationRequested)
                    action();
            }, mCancellationToken);

            // Add the task to the collection of tasks
            Tasks.Add(new KeyValuePair<string, Task>(key, t));

            // Return the task
            return t;
        }

        /// <summary>
        /// Start a <see cref="Task"/>
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to execute</param>
        /// <param name="key">The key for the task</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to create a custom cancellation point for the <see cref="Action"/> provided</param>
        /// <returns></returns>
        public static Task Run(String key, Action action, CancellationToken cancellationToken)
        {
            // Create the task with the global cancellationtoken
            var t = Task.Run(action, cancellationToken);

            // Add the task to the collection of tasks
            Tasks.Add(new KeyValuePair<string, Task>(key, t));

            // Return the task
            return t;
        }

        /// <summary>
        /// Starts a <see cref="Task{TResult}"/>
        /// </summary>
        /// <typeparam name="T">The return type of the <see cref="Task"/></typeparam>
        /// <param name="func">The <see cref="Func{T, TResult}"/> to execute</param>
        /// <returns></returns>
        public static Task<T> Run<T>(Func<T> func)
        {
            // Create the task with the global cancellationtoken
            Task<T> t = Task.Run(() =>
            {
                if (!mCancellationToken.IsCancellationRequested)
                    return func();
                else
                    return default;
            }, mCancellationToken);

            // Add the task to the collection of tasks
            Tasks.Add(new KeyValuePair<string, Task>(Guid.NewGuid().ToString(), t));

            // Return the task
            return t;
        }

        /// <summary>
        /// Starts a <see cref="Task{TResult}"/>
        /// </summary>
        /// <typeparam name="T">The return type of the <see cref="Task"/></typeparam>
        /// <param name="func">The <see cref="Func{T, TResult}"/> to execute</param>
        /// <param name="key">The key for the task</param>
        /// <returns></returns>
        public static Task<T> Run<T>(string key, Func<T> func)
        {
            // Create the task with the global cancellationtoken
            Task<T> t = Task.Run(() =>
            {
                if (!mCancellationToken.IsCancellationRequested)
                    return func();
                else
                    return default;
            }, mCancellationToken);

            // Add the task to the collection of tasks
            Tasks.Add(new KeyValuePair<string, Task>(key, t));

            // Return the task
            return t;
        }

        /// <summary>
        /// Starts a <see cref="Task{TResult}"/>
        /// </summary>
        /// <typeparam name="T">The return type of the <see cref="Task"/></typeparam>
        /// <param name="func">The <see cref="Func{T, TResult}"/> to execute</param>
        /// <param name="key">The key for the task</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to create a custom cancellation point for the <see cref="Action"/> provided</param>
        /// <returns></returns>
        public static Task<T> Run<T>(string key, Func<T> func, CancellationToken cancellationToken)
        {
            // Create the task with the global cancellationtoken
            Task<T> t = Task.Run(func, cancellationToken);

            // Add the task to the collection of tasks
            Tasks.Add(new KeyValuePair<string, Task>(key, t));

            // Return the task
            return t;
        }

        #endregion

        #region Task Collection Manipulation

        /// <summary>
        /// Waits for all <see cref="Task"/>s in the <see cref="Tasks"/> to be completed or cancelled
        /// </summary>
        public static void WaitAll()
        {
            // Wait while all task are being completed or cancelled
            while (!InProgress)
            {
                 // Rest the thread for a moment to not load the processor at 100%
                 Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Cancels all future execution of <see cref="Task"/>s in the <see cref="Tasks"/>
        /// </summary>
        public static void CancelAll()
        {
            // Cancel the further execution of tasks
            mCancellationTokenSource.Cancel();
        }

        #endregion

        #region Getters

        /// <summary>
        /// Gets the first <see cref="Task"/> from the <see cref="Tasks"/>
        /// </summary>
        /// <param name="key">The key of the <see cref="Task"/> to get</param>
        /// <returns></returns>
        public static Task GetTask(string key) => Tasks.Where(kv => kv.Key == key).FirstOrDefault().Value;

        /// <summary>
        /// Gets the <see cref="Task"/>s where the given key is matched
        /// </summary>
        /// <param name="key">The key of the <see cref="Task"/>s to get</param>
        /// <returns></returns>
        public static IEnumerable<Task> GetTasks(string key) => Tasks.Where(kv => kv.Key == key).Select(kv => kv.Value);

        /// <summary>
        /// Gets the <see cref="Task{TResult}"/> from the <see cref="Tasks"/>
        /// </summary>
        /// <typeparam name="T">The return type of the <see cref="Task"/> to get</typeparam>
        /// <param name="key">The key of the <see cref="Task"/> to get</param>
        /// <returns></returns>
        public static Task<T> GetTask<T>(string key) => (Task<T>)Tasks.Where(kv => kv.Key == key).FirstOrDefault().Value;

        /// <summary>
        /// Gets the <see cref="Task{TResult}"/>s where the given key is matched
        /// </summary>
        /// <typeparam name="T">The return type of the Tasks</typeparam>
        /// <param name="key">The key of the <see cref="Task{TResult}"/>s to get</param>
        /// <returns></returns>
        public static IEnumerable<Task<T>> GetTasks<T>(string key) => (IEnumerable<Task<T>>)Tasks.Where(kv => kv.Key == key).Select(kv => kv.Value);

        #endregion

        #endregion
    }
}
