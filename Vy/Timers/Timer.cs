using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Vy
{
    /// <summary>
    /// A timer based on the <see cref="StopWatch"/> from <see cref="System.Diagnostics"/> but with the useful features of the <see cref="System.Timers.Timer"/>.
    /// </summary>
    public class Timer : IDisposable
    {
        #region Private Members

        /// <summary>
        /// The timer to use
        /// </summary>
        private readonly Stopwatch _StopWatch = new Stopwatch();

        /// <summary>
        /// Boolean to represent wether the <see cref="Timer"/> has been started or not.
        /// </summary>
        private bool _IsRunning = false;

        /// <summary>
        /// The infinitely running <see cref="Task"/>
        /// </summary>
        private Task _InfiniteRun;

        #endregion

        #region Events

        /// <summary>
        /// The event to signal that the interval has been passed
        /// </summary>
        public event EventHandler<EventArgs> Elapsed;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the interval in milliseconds for the <see cref="Timer"/> to raise the <see cref="Elapsed"/> event.
        /// </summary>
        public int Interval { get; set; } = 1;

        /// <summary>
        /// Gets the elepsed time from the <see cref="Timer"/>.
        /// </summary>
        public TimeSpan ElapsedTime => _StopWatch.Elapsed;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Timer()
        {
            _InfiniteRun = Run();
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Starts the <see cref="Timer"/>.
        /// </summary>
        public void Start()
        {
            _StopWatch.Start();
            _IsRunning = true;
        }

        /// <summary>
        /// Stops the <see cref="Timer"/>.
        /// </summary>
        public void Stop()
        {
            _StopWatch.Stop();
            _IsRunning = false;
        }

        /// <summary>
        /// Resets the <see cref="Timer"/>.
        /// </summary>
        public void Reset()
        {
            _StopWatch.Reset();
            _IsRunning = false;
        }

        /// <summary>
        /// Restarts the <see cref="Timer"/>.
        /// </summary>
        public void Restart()
        {
            _StopWatch.Restart();
            _IsRunning = true;
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Runs an infinite function that will call the <see cref="Elapsed"/> event when the <see cref="Timer"/> is running and the <see cref="Stopwatch.ElapsedMilliseconds"/> are a multiple of the <see cref="Interval"/>.
        /// </summary>
        /// <returns></returns>
        private async Task Run()
        {
            await Task.Run(() => {
                while (true)
                {
                    // If the timer isn't running, do nothing
                    if (!_IsRunning) continue;

                    // When the Elapsed Milliseconds are a multiple of the Interval
                    // Raise an Elpsed event
                    if (_StopWatch.ElapsedMilliseconds % Interval == 0)
                        OnElapsed(new EventArgs());
                }
            });
        }

        #endregion

        #region Protected Members

        /// <summary>
        /// Invokes the <see cref="Elapsed"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected void OnElapsed(EventArgs e)
        {
            Elapsed?.Invoke(this, e);
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Disposes the current <see cref="Timer"/>.
        /// </summary>
        public void Dispose()
        {
            _InfiniteRun.Dispose();
        }

        #endregion
    }
}
