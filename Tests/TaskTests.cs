namespace TaskTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestTaskCreate()
    {
        Task<string> task = new Task<string>(() => {
            Thread.Sleep(100);
            return $"task1的线程ID为{Thread.CurrentThread.ManagedThreadId}";
        });
        task.Start();

        Task<string> task2 = Task<string>.Factory.StartNew(() => {
            Thread.Sleep(100);
            return $"task2的线程ID为{Thread.CurrentThread.ManagedThreadId}";
        });

        Task<string> task3 = Task.Run<string>(() => {
            Thread.Sleep(100);
            return $"task3的线程ID为{Thread.CurrentThread.ManagedThreadId}";
        });

        Console.WriteLine("执行主线程！");
        // 如果task还没运行完，会阻塞
        Console.WriteLine(task.Result);
        Console.WriteLine(task2.Result);
        Console.WriteLine(task3.Result);
        
        Thread.Sleep(2000);
    }

    [TestMethod]
    public void TestTaskWaitAll()
    {
        Task task = new Task(() => {
            Thread.Sleep(100);
            Console.WriteLine("task1 done");
        });
        task.Start();

        Task task2 = new Task(() => {
            Thread.Sleep(1000);
            Console.WriteLine("task2 done");
        });
        task2.Start();

        // 等待所有Task执行完后，再执行主线程
        Task.WaitAll(new Task[]{task, task2});
        Console.WriteLine("执行主线程！");
    }

    [TestMethod]
    public void TestTaskWaitAny()
    {
        Task task = new Task(() => {
            Thread.Sleep(100);
            Console.WriteLine("task1 done");
        });
        task.Start();

        Task task2 = new Task(() => {
            Thread.Sleep(1000);
            Console.WriteLine("task2 done");
        });
        task2.Start();

        // 等待任意一个Task执行完后，执行主线程
        Task.WaitAny(new Task[]{task, task2});
        Console.WriteLine("执行主线程！");

        Thread.Sleep(1000);
    }

    [TestMethod]
    public void TestTaskWhenAny()
    {
        Task task = new Task(() => {
            Thread.Sleep(100);
            Console.WriteLine("task1 done");
        });
        task.Start();

        Task task2 = new Task(() => {
            Thread.Sleep(1000);
            Console.WriteLine("task2 done");
        });
        task2.Start();

        // 等待任意一个Task执行完后，执行主线程
        Task.WhenAll(new Task[]{task, task2}).ContinueWith((t) => {
            Thread.Sleep(100);
            Console.WriteLine("执行后续操作 done");
        });


        Console.WriteLine("执行主线程！");

        Thread.Sleep(2000);
    }

    [TestMethod]
    public void TestTaskCancel()
    {
        CancellationTokenSource source = new CancellationTokenSource();
        source.Token.Register(() => {
            Console.WriteLine("取消后的回调操作");
        });

        int index = 0;
        Task task = new Task(() => {
            while (!source.IsCancellationRequested)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"第{++index}次执行，线程运行中...");
            }
            Console.WriteLine("已取消");
        });
        task.Start();
        
        // Thread.Sleep(5000);
        // source.Cancel();

        // 5s后取消。会新创建一个新的Task运行，不会阻塞当前线程。
        source.CancelAfter(5000);
        Console.WriteLine("主线程继续运行");
        Thread.Sleep(7000);
    }
}