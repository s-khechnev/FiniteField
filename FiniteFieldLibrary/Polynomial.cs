﻿using System.Numerics;

namespace FiniteFieldLibrary;

public class Polynomial<T> where T :
    IUnaryPlusOperators<T, T>,
    IUnaryNegationOperators<T, T>,
    IAdditionOperators<T, T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IDivisionOperators<T, T, T>,
    IEqualityOperators<T, T, bool>
{
    public int Degree => _coefficients.Length - 1;
    public T ZeroElement { get; }

    private T[] _coefficients;

    public T[] Coefficients => _coefficients;

    public T this[int index] => index >= _coefficients.Length ? ZeroElement : _coefficients[index];

    public Polynomial(T[] coefficients, T zeroElement)
    {
        ZeroElement = zeroElement;

        _coefficients = new T[coefficients.Length];
        Array.Copy(coefficients, _coefficients, coefficients.Length);
        
        if (_coefficients[^1] == ZeroElement)
            CutHighZero(ref _coefficients);
    }

    private void CutHighZero(ref T[] coefficients)
    {
        int newDegree;
        for (newDegree = coefficients.Length - 1; newDegree > 0; newDegree--)
        {
            if (coefficients[newDegree] != ZeroElement)
                break;
        }

        Array.Resize(ref coefficients, newDegree + 1);
    }

    public static Polynomial<ElementIntegerModuloN> GetPolynomialOverResidueRingFromIntArray(int[] coefficients,
        IntegersModuloN ring)
    {
        var coefficientsOverResidueRing =
            IntegersModuloN.GetArrayWithCoefficientsFromResidueRingFromIntArray(coefficients, ring);
        return new Polynomial<ElementIntegerModuloN>(coefficientsOverResidueRing, ring[0]);
    }

    public static Polynomial<T> operator +(Polynomial<T> polynomial) => new (polynomial._coefficients, polynomial.ZeroElement);

    public static Polynomial<T> operator -(Polynomial<T> polynomial)
    {
        var coefficients = new T[polynomial.Degree + 1];
        for (var i = 0; i <= polynomial.Degree; i++)
        {
            coefficients[i] = -polynomial[i];
        }

        return new Polynomial<T>(coefficients, polynomial.ZeroElement);
    }

    public static Polynomial<T> operator +(Polynomial<T> left, Polynomial<T> right)
    {
        if (left == null || right == null)
            throw new ArgumentNullException();

        var degree = Math.Max(left.Degree, right.Degree);
        var coefficients = new T[degree + 1];
        for (var i = 0; i < degree + 1; i++)
        {
            coefficients[i] += left[i] + right[i];
        }

        return new Polynomial<T>(coefficients, left.ZeroElement);
    }

    public static Polynomial<T> operator -(Polynomial<T> left, Polynomial<T> right) => left + (-right);

    public static Polynomial<T> operator *(Polynomial<T> left, Polynomial<T> right)
    {
        if (left == null || right == null)
            throw new ArgumentNullException();

        var coefficients = new T[left.Degree + right.Degree + 1];

        for (var i = 0; i < left._coefficients.Length; ++i)
        for (var j = 0; j < right._coefficients.Length; ++j)
            coefficients[i + j] += left[i] * right[j];

        return new Polynomial<T>(coefficients, left.ZeroElement);
    }

    public static Polynomial<T> operator /(Polynomial<T> dividend, Polynomial<T> divisor)
    {
        if (dividend == null || divisor == null)
            throw new ArgumentNullException();

        if (dividend.Degree < divisor.Degree)
            return dividend;

        var remainder = new Polynomial<T>((T[])dividend._coefficients.Clone(), dividend.ZeroElement);
        var quotient = new T[dividend.Degree - divisor.Degree + 1];

        for (var i = 0; i < quotient.Length; i++)
        {
            var coefficient = remainder[remainder.Degree - i] / divisor._coefficients.Last();
            quotient[quotient.Length - 1 - i] = coefficient;
            for (var j = 0; j < divisor._coefficients.Length; j++)
            {
                remainder._coefficients[remainder.Degree - i - j] -= coefficient * divisor[divisor.Degree - j];
            }
        }

        return new Polynomial<T>(quotient, dividend.ZeroElement);
    }
    
    public static Polynomial<T> operator %(Polynomial<T> dividend, Polynomial<T> divisor)
    {
        if (dividend == null || divisor == null)
            throw new ArgumentNullException();

        if (dividend.Degree < divisor.Degree)
            return dividend;

        var remainder = new Polynomial<T>((T[])dividend._coefficients.Clone(), dividend.ZeroElement);
        var quotient = new T[dividend.Degree - divisor.Degree + 1];

        for (var i = 0; i < quotient.Length; i++)
        {
            var coefficient = remainder[remainder.Degree - i] / divisor._coefficients.Last();
            quotient[quotient.Length - 1 - i] = coefficient;
            for (var j = 0; j < divisor._coefficients.Length; j++)
            {
                remainder._coefficients[remainder.Degree - i - j] -= coefficient * divisor[divisor.Degree - j];
            }
        }

        remainder.CutHighZero(ref remainder._coefficients);

        return remainder;
    }

    public static bool operator ==(Polynomial<T>? left, Polynomial<T>? right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(Polynomial<T>? left, Polynomial<T>? right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        var result = "";
        result += _coefficients[0];

        if (_coefficients.Length > 1)
        {
            result += " + " + _coefficients[1] + "x + ";
            for (var i = 2; i < _coefficients.Length; i++)
            {
                result += _coefficients[i] + "x^" + i + " + ";
            }
        }

        return result.TrimEnd(' ', '+');
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Degree, _coefficients, ZeroElement);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        var other = (Polynomial<T>)obj;
        return Degree == other.Degree && _coefficients.SequenceEqual(other._coefficients) &&
               ZeroElement == other.ZeroElement;
    }
}