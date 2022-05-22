namespace Method;

class Method
{
    public static void Swap(ref int x, ref int y)
    {
        int temp = x;
        x = y;
        y = temp;
    }

    public static void Divide(int x, int y, out int result, out int remainder)
    {
        result = x / y;
        remainder = x % y;
    }

    public static void Write(string name, params object[] hobbies)
    {
        Console.WriteLine(name);
        Console.WriteLine(hobbies);
    }
}