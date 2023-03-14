using System.Numerics;

namespace FiniteField;

public class ElementIntegerModuloN :
    IUnaryPlusOperators<ElementIntegerModuloN, ElementIntegerModuloN>,
    IUnaryNegationOperators<ElementIntegerModuloN, ElementIntegerModuloN>,
    IAdditionOperators<ElementIntegerModuloN, ElementIntegerModuloN, ElementIntegerModuloN>,
    ISubtractionOperators<ElementIntegerModuloN, ElementIntegerModuloN, ElementIntegerModuloN>,
    IMultiplyOperators<ElementIntegerModuloN, ElementIntegerModuloN, ElementIntegerModuloN>,
    IDivisionOperators<ElementIntegerModuloN, ElementIntegerModuloN, ElementIntegerModuloN>,
    IEqualityOperators<ElementIntegerModuloN, ElementIntegerModuloN, bool>
{
    public readonly IntegersModuloN Parent;
    public readonly int Value;

    public ElementIntegerModuloN Inverse => GetInverseElement();

    public ElementIntegerModuloN(IntegersModuloN parent, int value)
    {
        Parent = parent;
        Value = value;
    }

    public static ElementIntegerModuloN operator +(ElementIntegerModuloN element) => element;

    public static ElementIntegerModuloN operator -(ElementIntegerModuloN element)
    {
        var value = -element.Value;
        value %= element.Parent.Modulus;
        value = value >= 0 ? value : value + element.Parent.Modulus;
        return new ElementIntegerModuloN(element.Parent, value);
    }

    public static ElementIntegerModuloN operator +(ElementIntegerModuloN? left, ElementIntegerModuloN? right)
    {
        if (left == null || right == null)
            throw new ArgumentNullException();

        if (left.Parent != right.Parent)
            throw new ArgumentException("The elements from different rings");

        var value = (left.Value + right.Value) % left.Parent.Modulus;
        return new ElementIntegerModuloN(left.Parent, value);
    }

    public static ElementIntegerModuloN operator -(ElementIntegerModuloN? left, ElementIntegerModuloN? right)
    {
        if (left == null || right == null)
            throw new ArgumentNullException();

        if (left.Parent != right.Parent)
            throw new ArgumentException("The elements from different rings");

        return left + (-right);
    }

    public static ElementIntegerModuloN operator *(ElementIntegerModuloN? left, ElementIntegerModuloN? right)
    {
        if (left == null || right == null)
            throw new ArgumentNullException();

        if (left.Parent != right.Parent)
            throw new ArgumentException("The elements from different rings");

        var value = (left.Value * right.Value) % left.Parent.Modulus;
        return new ElementIntegerModuloN(left.Parent, value);
    }

    public static ElementIntegerModuloN operator /(ElementIntegerModuloN? left, ElementIntegerModuloN? right)
    {
        if (left == null || right == null)
            throw new ArgumentNullException();

        if (left.Parent != right.Parent)
            throw new ArgumentException("The elements from different rings");

        return left * right.Inverse;
    }

    public static bool operator ==(ElementIntegerModuloN? left, ElementIntegerModuloN? right)
    {
        if (left == null || right == null)
            throw new ArgumentNullException();

        if (left.Parent != right.Parent)
            throw new ArgumentException("The elements from different rings");

        return left.Value % left.Parent.Modulus == right.Value % right.Parent.Modulus;
    }

    public static bool operator !=(ElementIntegerModuloN? left, ElementIntegerModuloN? right)
    {
        return !(left == right);
    }

    private ElementIntegerModuloN GetInverseElement()
    {
        var g = MyMath.ExtendedGcd(Value, Parent.Modulus, out var x, out var y);

        if (g != 1)
            throw new ArgumentException("Impossible find inverse");

        var value = (x % Parent.Modulus + Parent.Modulus) % Parent.Modulus;
        return new ElementIntegerModuloN(Parent, value);
    }
}