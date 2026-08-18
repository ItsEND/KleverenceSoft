using Task2;

namespace Task2Tests;

public class UnitTest1
{
    [Fact]
    public void AddToCount_ParallelCalls_ShouldNotLoseUpdates()
    {
        Server.Reset();


        Parallel.For(0, 100_000, _ =>
        {
            Server.AddToCount(1);
        });

        Assert.Equal(100_000, Server.GetCount());
    }

    [Fact]
    public void Server_ShouldHandleConcurrentReadsAndWrites()
    {
        Server.Reset();

        const int writers = 10_000;
        const int readers = 10_000;

        Parallel.Invoke(() =>
        {
            Parallel.For(0, writers, _ =>
            {
                Server.AddToCount(1);
            });
        }, () =>
        {
            Parallel.For(0, readers, _ =>
            {
                _ = Server.GetCount();
            });
        });

        Assert.Equal(writers, Server.GetCount());
    }
}