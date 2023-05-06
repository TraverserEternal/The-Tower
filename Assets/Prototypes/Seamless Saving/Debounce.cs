using System;
using System.Threading;
using System.Threading.Tasks;
public static class Util
{
  public static Action<T> AddDebounce<T>(this Action<T> func, int milliseconds = 300)
  {
    CancellationTokenSource cancelTokenSource = null;

    return arg =>
    {
      cancelTokenSource?.Cancel();
      cancelTokenSource = new CancellationTokenSource();

      Task.Delay(milliseconds, cancelTokenSource.Token)
          .ContinueWith(t =>
          {
            if (t.IsCompletedSuccessfully)
            {
              func(arg);
            }
          }, TaskScheduler.Default);
    };
  }
  public static Action AddDebounce(this Action func, int milliseconds = 300)
  {
    CancellationTokenSource cancelTokenSource = null;

    return () =>
    {
      cancelTokenSource?.Cancel();
      cancelTokenSource = new CancellationTokenSource();

      Task.Delay(milliseconds, cancelTokenSource.Token)
          .ContinueWith(t =>
          {
            if (t.IsCompletedSuccessfully)
            {
              func();
            }
          }, TaskScheduler.Default);
    };
  }
  public static Action AddDebounce<T>(this Func<T> func, int milliseconds = 300)
  {
    CancellationTokenSource cancelTokenSource = null;

    return () =>
    {
      cancelTokenSource?.Cancel();
      cancelTokenSource = new CancellationTokenSource();

      Task.Delay(milliseconds, cancelTokenSource.Token)
          .ContinueWith(t =>
          {
            if (t.IsCompletedSuccessfully)
            {
              func();
            }
          }, TaskScheduler.Default);
    };
  }
}