namespace FiniteFieldLibrary;

public class FiniteFieldElement
{
    public Polynomial<ElementIntegerModuloN> Polynomial { get; }
    public FiniteField Parent { get; }

    public FiniteFieldElement(Polynomial<ElementIntegerModuloN> polynomial, FiniteField finiteField)
    {
        Polynomial = polynomial;
        Parent = finiteField;
    }

    public FiniteFieldElement Inverse => Pow(Parent.Order - 2);

    public static FiniteFieldElement operator +(FiniteFieldElement element) => element;

    public static FiniteFieldElement operator -(FiniteFieldElement element) =>
        element.Parent.GetElement(-element.Polynomial);

    public static FiniteFieldElement operator +(FiniteFieldElement left, FiniteFieldElement right)
    {
        if (left.Parent != right.Parent)
            throw new ArgumentException("Elements from different fields");

        return left.Parent.GetElement(left.Polynomial + right.Polynomial);
    }

    public static FiniteFieldElement operator -(FiniteFieldElement left, FiniteFieldElement right) => left + (-right);

    public static FiniteFieldElement operator *(FiniteFieldElement left, FiniteFieldElement right)
    {
        if (left.Parent != right.Parent)
            throw new ArgumentException("Elements from different fields");

        return left.Parent.GetElement(left.Polynomial * right.Polynomial % left.Parent.IrreduciblePolynomial);
    }

    public static FiniteFieldElement operator /(FiniteFieldElement left, FiniteFieldElement right)
    {
        if (left.Parent != right.Parent)
            throw new ArgumentException("Elements from different fields");

        if (right == right.Parent.Zero)
            throw new DivideByZeroException("Attempt to divide by zero");

        return left.Parent.GetElement(left.Polynomial * right.Inverse.Polynomial % left.Parent.IrreduciblePolynomial);
    }

    public static bool operator ==(FiniteFieldElement? left, FiniteFieldElement? right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(FiniteFieldElement? left, FiniteFieldElement? right)
    {
        return !(left == right);
    }

    public FiniteFieldElement Pow(int degree)
    {
        degree %= Parent.Order - 1;
        if (degree == 0) return Parent.One;
        if (degree == 1) return this;

        if (degree % 2 == 0)
            return Pow(degree / 2) * Pow(degree / 2);

        return this * Pow(degree - 1);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;

        var other = (FiniteFieldElement)obj;
        if (Parent != other.Parent)
            throw new ArgumentException("The elements from different fields");

        return Polynomial == other.Polynomial;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Parent, Polynomial);
    }

    public override string ToString()
    {
        var result = $"Element from {Parent}\n";
        result += $"Represented polynomial: {Polynomial}\n";
        return result;
    }
}