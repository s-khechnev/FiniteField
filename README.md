# Библиотека для работы с элементами конечных полей

В библиотеке реализованы конечные поля, кольца вычетов по любому модулю, полиномы
над любым типом, у которого определены стандартные алгебраические операции.

## Пример использования

# 1. Простое поле
```c#
// создание кольца(поля) вычетов по модулю 5
var primeField = FiniteField.Get(5);
var element1 = primeField[2];//2
var element2 = primeField[3];//3
var minusElement1 = -element1; //3
var diff = element1 - element2;//4
var sum = element1 + element2;//0
var mul = element1 * element2;//1
var div = element1 / element2;//4
var isEqual = element2 == minusElement1;//true
var isNotEqual = element2 != sum;//true
```
# 2. Полином
```c#
//создание многочлена над полем F_5, 
//для создания необоходимо дополнительно передать нулевой элемент, над которым построен многочлен 
var polynomial1 = new Polynomial<ElementIntegerModuloN>(new []{primeField[1], primeField[2], primeField[3]}, primeField[0]);//3x^2 + 2x + 1
var polynomial2 = new Polynomial<ElementIntegerModuloN>(new []{primeField[1], primeField[4]}, primeField[0]);//4x+1
var degree = polynomial1.Degree;
var minusPolynomial1 = -polynomial1;// 2x^2 + 3x + 4
var diff = polynomial2 - polynomial1;//3x^2 + 2x
var sum = polynomial1 + polynomial2;//3x^2 + x + 2
var mul = polynomial1 * polynomial2;//2x^3 + x^2 + 1
var remainder = polynomial1 % polynomial2;//1
var isNotEqual = polynomial1 == polynomial2;//false
```

# 3. Конечные поля
```c#
//создание GF(9) 
//(для создания поля не обязательно создавать полином над F_3, 
// полином можно задать целочисленным массивом)
var field = FiniteField.Get(3, 2, new[] { 1, 0, 1 });
var primeField = field.PrimeField; //F_3
var one = field.One;
var zero = field.Zero;
var element1 = field.GetElement(new[] { 0, 1 }); //x
var element2 = field.GetElement(new[] { 2, 1 }); //2+x
var minusElement1 = -element1; //2x
var inverse = element1.Inverse; //2x
var diff = element2 - element1; //2
var sum = element1 + element2; //2 + 2x
var myl = element1 * element2; //2 + 2x
var div = element1 / element2; //2 + x
var isNotEqual = element1 == element2; //false

//создание F_2^n
var field = FiniteField.GetBinary(8, new[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 });
//байты
var element = field.GetElementFromByte(69);
var bytes = field.GetBytesFromElement(element);
```
