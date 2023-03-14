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
}