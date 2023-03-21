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

        public FiniteFieldElement GetElementFromByte(params byte[] bytes)
        {
            if (N % 8 != 0)
                throw new ArgumentException("The N is not a multiple of 8");

            if (N / 8 != bytes.Length)
                throw new ArgumentException("Wrong count of bytes");

            if (bytes.Length > 4)
                throw new ArgumentException("Too many bytes");

            var zeroArr = new byte[4 - bytes.Length];
            var normalizedArr = bytes.Concat(zeroArr).ToArray();
            var number = BitConverter.ToInt32(normalizedArr);

            var binaryString = Convert.ToString(number, 2);
            var polynomial = binaryString.Select(item => item - '0').ToArray();

            return GetElement(polynomial);
        }

        public byte[] GetBytesFromElement(FiniteFieldElement element)
        {
            if (N % 8 != 0)
                throw new Exception("The N is not a multiple of 8");

            var binaryString = string.Join("", element.Polynomial.Coefficients.Select(item => item.Value));
            var number = Convert.ToInt32(binaryString, 2);
            return BitConverter.GetBytes(number);
        }
    }
}