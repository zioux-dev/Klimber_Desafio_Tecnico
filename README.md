Desafío
Tenemos un método que genera un reporte en base a una colección de formas geométricas, procesando algunos datos para presentar como reporte información extra. La firma del método es: public static string Imprimir(List<FormaGeometrica> formas, int idioma) y se encuentra ubicado en la clase FormaGeometrica.cs

En consecuencia a como fue codificado este módulo, al equipo de desarrollo se le hace muy difícil el poder agregar una nueva forma geométrica o implementar la impresión del reporte en otro idioma. Nos gustaría poder dar soporte para que en el futuro los desarrolladores puedan agregar otros tipos de formas y obtener el reporte en otros idiomas con más agilidad. ¿Nos podrías ayudar a refactorizar la clase FormaGeometrica?

Acompañando al proyecto encontrarás una serie de tests unitarios (librería NUnit) que describen el comportamiento actual del método Imprimir. Se puede modificar absolutamente cualquier cosa del código y de los tests, con la única condición que los tests deben pasar correctamente al entregar la solución.
Dentro del código hay un TODO con los requerimientos técnicos y del usuario a satisfacer.

Tecnología
La solución fue creada con Visual Studio 2019, consta de dos proyectos en C# sobre .NET Framework 4.6.2, uno específico para Tests y otro que contiene el negocio a mejorar.
