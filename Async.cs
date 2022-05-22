namespace Async;

public class AsyncDemo
{
    // async声明是异步方法。
    // await通知编译器异步等待结果完成
    public static async Task<int> RetrieveDocsHomePage()
    {
        var client = new HttpClient();
        byte[] content = await client.GetByteArrayAsync("https://docs.microsoft.com/");

        Console.WriteLine($"{nameof(RetrieveDocsHomePage)}: Finished downloading.");
        return content.Length;
    }
}