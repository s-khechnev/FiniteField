namespace FiniteFieldLibrary;

public partial class FiniteField
{
    public IntegersModuloN PrimeField { get; }
    public int N { get; }
    public int Order { get; }
    public Polynomial<ElementIntegerModuloN> IrreduciblePolynomial { get; }

    private FiniteField(int p, int n, Polynomial<ElementIntegerModuloN> irreduciblePolynomial)
    {
        PrimeField = new IntegersModuloN(p);
        N = n;
        Order = (int)Math.Pow(PrimeField.Modulus, n);
        IrreduciblePolynomial = irreduciblePolynomial;
    }

    private FiniteField(int p, int n, int[] irreduciblePolynomial)
    {
        PrimeField = new IntegersModuloN(p);
        N = n;
        Order = (int)Math.Pow(PrimeField.Modulus, n);
        IrreduciblePolynomial =
            Polynomial<ElementIntegerModuloN>.GetPolynomialOverResidueRingFromIntArray(irreduciblePolynomial,
                PrimeField);
    }

    public static IntegersModuloN Get(int p)
    {
        if (!MyMath.IsPrime(p))
            throw new ArithmeticException("Impossible create residue field with not prime modulus");

        return new IntegersModuloN(p);
    }

    public static FiniteField Get(int p, int n, Polynomial<ElementIntegerModuloN> irreduciblePolynomial)
        => new FiniteField(p, n, irreduciblePolynomial);

    public static FiniteField Get(int p, int n, int[] irreduciblePolynomial)
        => new FiniteField(p, n, irreduciblePolynomial);

    public FiniteFieldElement GetElement(Polynomial<ElementIntegerModuloN> polynomial) => new(polynomial, this);

    public FiniteFieldElement GetElement(int[] intPolynomial)
    {
        var polynomial =
            Polynomial<ElementIntegerModuloN>.GetPolynomialOverResidueRingFromIntArray(intPolynomial, PrimeField);
        return new FiniteFieldElement(polynomial, this);
    }

    public FiniteFieldElement Zero =>
        GetElement(new Polynomial<ElementIntegerModuloN>(new[] { PrimeField[0] }, PrimeField[0]));

    public FiniteFieldElement One =>
        GetElement(new Polynomial<ElementIntegerModuloN>(new[] { PrimeField[1] }, PrimeField[0]));

    public static bool operator ==(FiniteField? left, FiniteField? right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(FiniteField? left, FiniteField? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        var other = (FiniteField)obj;
        return PrimeField == other.PrimeField && N == other.N && IrreduciblePolynomial == other.IrreduciblePolynomial;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PrimeField, N, IrreduciblePolynomial);
    }

    public override string ToString()
    {
        return $"GF({Order})";
    }
}