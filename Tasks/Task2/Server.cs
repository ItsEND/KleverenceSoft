namespace Task2;

public static class Server
{
    private static int _count = 0;
    private static readonly ReaderWriterLockSlim _lock = new();

    public static int GetCount()
    {
        _lock.EnterReadLock();
        try
        {
            return _count;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public static void AddToCount(int count)
    {
        _lock.EnterWriteLock();
        try
        {
            _count += count;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    //Для тестов
    public static void Reset() 
    {
        _lock.EnterWriteLock();
        try
        {
            _count = 0;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
}
