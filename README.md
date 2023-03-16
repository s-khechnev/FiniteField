# Библиотека для работы с конечными полями

## Пример использования

```c#
// создание кольца(поля) вычетов по простому модулю
var ring = FiniteField.Get(5);
//создание многочлена над кольцом: x^2 + x + 1
var polynomial = new Polynomial<ElementIntegerModuloN>(new []{ring[1], ring[2], ring[3]}, ring[0]);
// создание поля F_{2^8}, q = x^8 + x^4 + x^3 + x + 1
var field = FiniteField.GetBinary(8, new [] {1, 1, 0, 1, 1, 0, 0, 0, 1});
// получение 1 из этого поля
var one = field.One;
// получение 0 из этого поля
var zero = field.Zero;
// сложение двух единиц
var result = one + one;

var element = field.GetElementFromByte(69);
// и наоборот
var _byte = field.FromElementToNumber(element);
```
