namespace Delegate;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Test1()
    {
        double[] a = { 0.0, 0.5, 1.0 };
        // 传递匿名方法
        double[] squares = UseDelegateExample.Apply(a, (x) => x * x);
        // 传递静态方法
        double[] sines = UseDelegateExample.Apply(a, Math.Sin);
        // 传递实例方法
        Multiplier m = new(2.0);
        double[] doubles =UseDelegateExample.Apply(a, m.Multiply);
    }

    
    [TestMethod]
    public void Test2()
    {
        Logger.WriteMessage += LoggingMethods.LogToConsole;
        FileLogger logger = new FileLogger("log.txt");
        Logger.LogMessage(Severity.Verbose, "abc", "test");
    }
}