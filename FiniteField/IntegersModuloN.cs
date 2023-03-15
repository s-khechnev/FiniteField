namespace FiniteField;

public class IntegersModuloN
{
    public readonly int Modulus;
    private readonly ElementIntegerModuloN[] _elements;

    public ElementIntegerModuloN this[int index] => index >= _elements.Length
        ? new ElementIntegerModuloN(this, index % Modulus)
        : _elements[index];

    public IntegersModuloN(int modulus)
    {
        Modulus = modulus;
        _elements = new ElementIntegerModuloN[modulus];

        InitElements();
    }

    private void InitElements()
    {
        for (int i = 0; i < _elements.Length; i++)
        {
            _elements[i] = new ElementIntegerModuloN(this, i);
        }
    }
    
    public static bool operator ==(IntegersModuloN? left, IntegersModuloN? right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(IntegersModuloN? left, IntegersModuloN? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        var other = (IntegersModuloN)obj;
        return Modulus == other.Modulus;
    }

    public override int GetHashCode()
    {
        return Modulus.GetHashCode();
    }
}