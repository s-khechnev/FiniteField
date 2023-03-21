using FiniteFieldLibrary;

Console.WriteLine("Hello world");

var field = FiniteField.GetBinary(8, new [] {1, 1, 0, 1, 1, 0, 0, 0, 1});
// получение 1 из этого поля
var one = field.One;
// получение 0 из этого поля
var zero = field.Zero;
// сложение двух единиц
var result = one + one;

var element = field.GetElementFromByte(69);
// и наоборот
var _byte = field.GetBytesFromElements(element);