namespace FiniteFieldLibrary;

public class IntegersModuloN
{
    public int Modulus { get; }
    private ElementIntegerModuloN[] Elements { get; }

    public ElementIntegerModuloN this[int index] => index >= Elements.Length
        ? Elements[index % Modulus]
        : Elements[index];

    public IntegersModuloN(int modulus)
    {
        Modulus = modulus;
        Elements = new ElementIntegerModuloN[modulus];

        InitElements();
    }

    private void InitElements()
    {
        for (var i = 0; i < Elements.Length; i++)
        {
            Elements[i] = new ElementIntegerModuloN(this, i);
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