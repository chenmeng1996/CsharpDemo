namespace Delegate;

// 委托类型表示对具有特定参数列表和返回类型的方法的引用。
// 通过委托，可以将方法视为可分配的变量，并且可以作为参数。
// 委托类似于其他语言的函数指针。
delegate double Function(double x);

class Multiplier
{
    double _factor;

    public Multiplier(double factor) => _factor = factor;

    public double Multiply(double x) => x * _factor;
}

class DelegateExample
{
    public static double[] Apply(double[] a, Function f)
    {
        var result = new double[a.Length];
        for (int i = 0; i < a.Length; i++) result[i] = f(a[i]);
        return result;
    }
}