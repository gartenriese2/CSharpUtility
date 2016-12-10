using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpUtility.Threading
{
    public static class TaskExtensions
    {
        public static void SwallowingWait(this Task task)
        {
            task.SwallowExceptions().Wait();
        }

        public static T SwallowingWait<T>(this Task<T> task)
        {
            task.SwallowExceptions().Wait();
            return task.Result;
        }

        public static Task SwallowExceptions(this Task task)
        {
            return task.ContinueWith(t => { });
        }

        public static Task<T> SwallowExceptions<T>(this Task<T> task)
        {
            return task.ContinueWith(completedTask => completedTask.IsFaulted ? default(T) : completedTask.Result);
        }

        public static void CancelableWait(this Task task, CancellationToken ct)
        {
            try
            {
                task.Wait(ct);
            }
            catch (OperationCanceledException)
            {
            }
        }

        public static void CancelableWait<T>(this Task<T> task, CancellationToken ct)
        {
            try
            {
                task.Wait(ct);
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}
