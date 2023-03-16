namespace FiniteFieldLibrary;

public static class MyMath
{
    public static bool IsPrime(int number)
    {
        if (number < 2) return false;
        if (number % 2 == 0) return number == 2;
        var root = (int)Math.Sqrt(number);
        for (var i = 3; i <= root; i += 2)
        {
            if (number % i == 0) return false;
        }

        return true;
    }

    public static int ExtendedGcd(int a, int b, out int x, out int y)
    {
        if (a == 0)
        {
            x = 0;
            y = 1;
            return b;
        }

        var d = ExtendedGcd(b % a, a, out var x1, out var y1);
        x = y1 - (b / a) * x1;
        y = x1;
        return d;
    }
}