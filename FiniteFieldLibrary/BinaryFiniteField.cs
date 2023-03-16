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

        public FiniteFieldElement GetElementFromByte(params byte[] number)
        {
            if (N % 8 != 0)
                throw new ArgumentException("The N is not a multiple of 8");

            if (N / 8 != number.Length)
                throw new ArgumentException("Too many bytes");

            var binaryString = "";
            for (var i = 0; i < number.Length; i++)
            {
                binaryString += Convert.ToString(number[i], 2);
            }

            var polynomial = binaryString.Select(item => item - '0').ToArray();
            return GetElement(polynomial);
        }

        public byte[] ElementToByte(FiniteFieldElement element)
        {
            if (N % 8 != 0)
                throw new Exception("The N is not a multiple of 8");

            var binaryString = string.Join("", element.Polynomial.Coefficients.Select(item => item.Value));
            var bytes = new byte[N / 8];
            for (var i = 0; i < N / 8; i++)
            {
                bytes[i] = Convert.ToByte(binaryString.Substring(i * 8, 7), 2);
            }

            return bytes;
        }
    }
}