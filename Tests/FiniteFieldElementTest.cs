using FiniteFieldLibrary;

namespace Tests;

public class FiniteFieldElementTest
{
    private FiniteField _field;

    [SetUp]
    public void SetUp()
    {
        _field = FiniteField.BinaryFiniteField.Get(8, new[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 });
    }

    [Test]
    public void EqualsTest1()
    {
        var element1 = _field.GetElement(new[] { 1, 1, 1, 1, 1 });
        var element2 = _field.GetElement(new[] { 1, 1, 1, 1, 1 });

        var actual = element1 == element2;
        var expectedResult = true;

        Assert.That(actual, Is.EqualTo(expectedResult));
    }

    [Test]
    public void EqualsTest2()
    {
        var field = FiniteField.BinaryFiniteField.Get(8, new[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 });
        var element1 = field.GetElement(new[] { 1, 1, 0, 1, 1 });
        var element2 = field.GetElement(new[] { 1, 0, 1, 1, 0 });

        var actual = element1 == element2;
        var expectedResult = false;

        Assert.That(actual, Is.EqualTo(expectedResult));
    }

    [Test]
    public void AdditionTest1()
    {
        var element1 = _field.GetElement(new[] { 1, 0, 1, 1, 1 });
        var element2 = _field.GetElement(new[] { 1, 1, 1, 0 });

        var sum = element1 + element2;
        var expectedResult = _field.GetElement(new[] { 0, 1, 0, 1, 1 });

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void AdditionTest2()
    {
        var element1 = _field.GetElement(new[] { 1, 0, 1, 1, 1 });
        var element2 = _field.GetElement(new[] { 1, 1, 1, 0, 1 });

        var sum = element1 + element2;
        var expectedResult = _field.GetElement(new[] { 0, 1, 0, 1 });

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void MultiplicationTest1()
    {
        var element1 = _field.GetElement(new[] { 1, 0, 1, 0, 1 });
        var element2 = _field.GetElement(new[] { 1, 1, 1 });

        var mul = element1 * element2;
        var expectedResult = _field.GetElement(new[] { 1, 1, 0, 1, 0, 1, 1 });

        Assert.That(mul, Is.EqualTo(expectedResult));
    }

    [Test]
    public void MultiplicationTest2()
    {
        var element1 = _field.GetElement(new[] { 1, 0, 1, 0, 1, 1, 1, 1 });
        var element2 = _field.GetElement(new[] { 1, 1, 1 });

        var mul = element1 * element2;
        var expectedResult = _field.GetElement(new[] { 1, 0, 1, 1, 1, 1, 1, 1 });

        Assert.That(mul, Is.EqualTo(expectedResult));
    }

    [Test]
    public void PowTest1()
    {
        var element1 = _field.GetElement(new[] { 1, 0, 1 });

        var powered = element1.Pow(3);
        var expectedResult = _field.GetElement(new[] { 1, 0, 1, 0, 1, 0, 1 });

        Assert.That(powered, Is.EqualTo(expectedResult));
    }

    [Test]
    public void PowTest2()
    {
        var element1 = _field.GetElement(new[] { 1, 0, 1 });

        var powered = element1.Pow(6);
        var expectedResult = _field.GetElement(new[] { 1, 0, 0, 0, 0, 1, 0, 1 });

        Assert.That(powered, Is.EqualTo(expectedResult));
    }
}