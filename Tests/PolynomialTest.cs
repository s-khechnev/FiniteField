using FiniteFieldLibrary;

namespace Tests;

public class PolynomialTest
{
    [Test]
    public void EqualTest1()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);

        Assert.That(polynomial1, Is.EqualTo(polynomial2));
    }

    [Test]
    public void EqualTest2()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 0, 4, 5, 6 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);

        Assert.That(polynomial1, Is.Not.EqualTo(polynomial2));
    }

    [Test]
    public void UnaryPlusTest()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);
        var polynomial2 = +polynomial1;
        var expectedResult = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);

        Assert.That(polynomial2, Is.EqualTo(expectedResult));
    }

    [Test]
    public void UnaryMinusTest()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);
        var polynomial2 = -polynomial1;
        var expectedResult = new Polynomial<int>(new[] { -1, -2, -3, -4, -5, -6 }, 0);

        Assert.That(polynomial2, Is.EqualTo(expectedResult));
    }

    [Test]
    public void AdditionTest1()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);

        var sum = polynomial1 + polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 2, 4, 6, 8, 10, 12 }, 0);

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void AdditionTest2()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);

        var sum = polynomial1 + polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 2, 4, 6, 4, 5, 6 }, 0);

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void SubtractionTest1()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 5, 6, 7 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3, 4, 5, 6 }, 0);

        var diff = polynomial1 - polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 0, 0, 0, 1, 1, 1 }, 0);

        Assert.That(diff, Is.EqualTo(expectedResult));
    }

    [Test]
    public void SubtractionTest2()
    {
        var polynomial1 = new Polynomial<int>(new[] { 1, 2, 3, 5, 6, 7 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 1, 2, 3 }, 0);

        var diff = polynomial1 - polynomial2;
        var expectedResult = new Polynomial<int>(new[] { 0, 0, 0, 5, 6, 7 }, 0);

        Assert.That(diff, Is.EqualTo(expectedResult));
    }

    [Test]
    public void MultiplicationTest1()
    {
        var polynomial1 = new Polynomial<int>(new[] { -5, 2, 8, -3, -3, 0, 1, 0, 1 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { 21, -9, -4, 0, 5, 0, 3 }, 0);

        var mul = polynomial1 * polynomial2;
        var expectedResult =
            new Polynomial<int>(new[] { -105, 87, 170, -143, -93, 49, 58, -18, 26, -18, -8, 0, 8, 0, 3 }, 0);

        Assert.That(mul, Is.EqualTo(expectedResult));
    }

    [Test]
    public void FindRemainderTest()
    {
        var polynomial1 = new Polynomial<int>(new[] { -42, 0, -12, 1 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { -3, 1 }, 0);

        var remainder = polynomial1 % polynomial2;
        var expectedResult = new Polynomial<int>(new[] { -123 }, 0);

        Assert.That(remainder, Is.EqualTo(expectedResult));
    }

    [Test]
    public void FindRemainderTest1()
    {
        var polynomial1 = new Polynomial<int>(new[] { -42, 0, -12, 0 }, 0);
        var polynomial2 = new Polynomial<int>(new[] { -3, 1 }, 0);

        var remainder = polynomial1 % polynomial2;
        var expectedResult = new Polynomial<int>(new[] { -150 }, 0);

        Assert.That(remainder, Is.EqualTo(expectedResult));
    }
}