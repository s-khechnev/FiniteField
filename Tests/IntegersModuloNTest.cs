using FiniteFieldLibrary;

namespace Tests;

public class IntegersModuloNTest
{
    [Test]
    public void EqualTest1()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[3];
        var element2 = ring[3];

        Assert.That(element1, Is.EqualTo(element2));
    }

    [Test]
    public void EqualTest2()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[3];
        var element2 = ring[4];

        Assert.That(element1, Is.Not.EqualTo(element2));
    }

    [Test]
    public void EqualTest_FromDifferentRings()
    {
        var ring1 = new IntegersModuloN(5);
        var ring2 = new IntegersModuloN(4);

        var element1 = ring1[3];
        var element2 = ring2[3];

        var ex = Assert.Throws<ArgumentException>(() => element1.Equals(element2));

        Assert.That(ex?.Message, Is.EqualTo("The elements from different rings"));
    }

    [Test]
    public void AdditionTest1()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[3];
        var element2 = ring[4];
        var sum = element1 + element2;
        var expectedResult = ring[2];

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void AdditionTest2()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[3];
        var element2 = ring[0];
        var sum = element1 + element2;
        var expectedResult = ring[3];

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void SubtractionTest1()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[3];
        var element2 = ring[1];
        var sum = element1 - element2;
        var expectedResult = ring[2];

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void SubtractionTest2()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[3];
        var element2 = ring[0];
        var sum = element1 - element2;
        var expectedResult = ring[3];

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void UnaryPlusTest1()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[3];
        var element2 = +element1;
        var expectedResult = ring[3];

        Assert.That(element2, Is.EqualTo(expectedResult));
    }

    [Test]
    public void UnaryMinusTest1()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[3];
        var element2 = -element1;
        var expectedResult = ring[2];

        Assert.That(element2, Is.EqualTo(expectedResult));
    }

    [Test]
    public void MultiplicationTest()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[3];
        var element2 = ring[2];
        var mul = element1 * element2;
        var expectedResult = ring[1];

        Assert.That(mul, Is.EqualTo(expectedResult));
    }

    [Test]
    public void DivisionTest1()
    {
        var ring = new IntegersModuloN(11);

        var element1 = ring[3];
        var element2 = ring[4];
        var mul = element1 / element2;
        var expectedResult = ring[9];

        Assert.That(mul, Is.EqualTo(expectedResult));
    }

    [Test]
    public void DivisionTest2()
    {
        var ring = new IntegersModuloN(6);

        var element1 = ring[3];
        var element2 = ring[4];


        var ex = Assert.Throws<ArithmeticException>(() =>
        {
            var mul = element1 / element2;
        });

        Assert.That(ex?.Message, Is.EqualTo("Impossible find inverse"));
    }
    
    [Test]
    public void InverseTest()
    {
        var ring = new IntegersModuloN(5);

        var element1 = ring[2].Inverse;
        var expectedResult = ring[3];

        Assert.That(element1, Is.EqualTo(expectedResult));
    }
}