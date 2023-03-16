namespace FiniteFieldLibrary;

public partial class FiniteField
{
    public class BinaryFiniteField : FiniteField
    {
        private BinaryFiniteField(int n, Polynomial<ElementIntegerModuloN> irreduciblePolynomial) : base(2, n,
            irreduciblePolynomial)
        {
        }

        private BinaryFiniteField(int n, int[] irreduciblePolynomial) : base(2, n, irreduciblePolynomial)
        {
        }

        public static BinaryFiniteField Get(int n, Polynomial<ElementIntegerModuloN> irreduciblePolynomial)
            => new BinaryFiniteField(n, irreduciblePolynomial);

        public static BinaryFiniteField Get(int n, int[] irreduciblePolynomial)
            => new BinaryFiniteField(n, irreduciblePolynomial);
    }
}