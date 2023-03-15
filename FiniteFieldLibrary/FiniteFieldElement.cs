﻿namespace FiniteFieldLibrary;

public class FiniteFieldElement
{
    public Polynomial<ElementIntegerModuloN> Polynomial { get; }
    public FiniteField Parent { get; }

    public FiniteFieldElement(Polynomial<ElementIntegerModuloN> polynomial, FiniteField finiteField)
    {
        Polynomial = polynomial;
        Parent = finiteField;
    }

    public FiniteFieldElement Inverse => Pow(Parent.Characteristic - 2);

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

    public FiniteFieldElement Pow(int degree)
    {
        if (degree == 0) return Parent.One;
        if (degree == 1) return this;

        if (degree % 2 == 0)
            return Pow(degree / 2) * Pow(degree / 2);

        return this * Pow(degree - 1);
    }

    public override string ToString()
    {
        var result = $"Element from {Parent}\n";
        result += $"Represented polynomial: {Polynomial}\n";
        return result;
    }
}