# 🍰 La Dulzura de Mamá

### Aplicación de consola para tomar pedidos de una pastelería

Proyecto de programación en C# que permite preparar pedidos de pasteles, cupcakes y bocadillos. También calcula el precio, cobra al cliente, imprime un ticket y lleva un resumen de la caja mientras el programa está abierto.

## Objetivo del proyecto

Practicar los fundamentos de programación con una aplicación de consola:

- Variables de tipo `int`, `decimal`, `string` y `bool`.
- Condiciones con `if`, `else if` y `else`.
- Bucles `do-while` para repetir los menús hasta recibir una opción válida.
- Lectura y escritura de datos con `Console.ReadLine()` y `Console.WriteLine()`.
- Cálculos de precios, descuentos, cambio y estadísticas.

La lógica del programa está dentro de `Main`.

## Requisitos

- SDK de .NET instalado.
- Un editor compatible con C#, como Visual Studio o Visual Studio Code.
- El archivo del proyecto (`.csproj`) junto con el código fuente.

## Cómo ejecutar

Abre una terminal en la carpeta del proyecto, donde está el archivo `.csproj`, y ejecuta:

```bash
dotnet run
```

## Menú principal

| Opción | Función |
|---|---|
| `1` | Crear y cobrar un pedido. |
| `2` | Consultar los datos de la caja del día. |
| `0` | Mostrar el resumen final y salir. |

El menú principal vuelve a aparecer después de cada acción, hasta que se elige `0`.

## Cómo se hace un pedido

1. Se escribe el nombre del cliente.
2. Se añaden uno o varios productos: pastel, cupcakes o bocadillos.
3. Se elige la forma de entrega.
4. El programa calcula el subtotal, el descuento y el total.
5. Se introduce el importe entregado por el cliente.
6. El programa calcula el cambio, actualiza la caja e imprime el ticket.

No se puede terminar un pedido sin añadir al menos un producto. Para acabar de añadir productos se selecciona `0`.

## Productos y precios

### Pastel

| Concepto | Precio |
|---|---:|
| Pequeño | 20,00 € |
| Mediano | 30,00 € |
| Grande | 40,00 € |
| Cada piso adicional | 10,00 € |
| Decoración temática | 15,00 € |

El primer piso está incluido en el precio base. Si el pastel tiene más de un piso, se elige si van en forma de torre o del mismo tamaño.

Si no se elige una decoración temática, se seleccionan el sabor del bizcocho, el relleno, la crema y los adornos. Los precios adicionales son:

| Opción | Suplemento |
|---|---:|
| Bizcocho Red velvet | 6,00 € |
| Bizcocho de chocolate | 5,00 € |
| Bizcocho de vainilla | Sin suplemento |
| Bizcocho de naranja | 4,50 € |
| Crema de dulce de leche | 3,00 € |
| Crema chantillí | 4,00 € |
| Crema de chocolate | 5,00 € |
| Chocolate por los lados y arriba | 4,00 € |
| Chocolate solo arriba | 2,00 € |
| Chocolate solo abajo | 2,00 € |
| Cerezas | 1,50 € |

Con la crema chantillí se puede elegir un color. El relleno se elige entre dulce de leche y mermelada; esas dos opciones no tienen suplemento en el código.

### Cupcakes

Se indica la cantidad y se elige un sabor. El precio se calcula por unidad:

| Sabor | Precio por unidad |
|---|---:|
| Vainilla | 2,50 € |
| Chocolate | 3,00 € |
| Fresa | 3,25 € |
| Red velvet | 3,50 € |

También se elige el color de la crema. Se puede escoger un color de la lista o escribir otro.

### Bocadillos

Se pueden añadir desde el menú de productos o después de preparar un pastel. Se elige una combinación y luego se indica la cantidad de cada tipo:

| Producto | Precio por unidad |
|---|---:|
| Alfajor | 1,50 € |
| Empanada de pollo | 2,00 € |
| Empanada de carne | 2,00 € |

## Entrega, descuento y pago

| Forma de entrega | Precio |
|---|---:|
| Recogida en el local | Sin suplemento |
| Reparto a domicilio | 3,00 € |

Para el reparto se solicita una dirección. Si el subtotal de los productos es de **50,00 € o más**, se aplica un descuento del **10 %** sobre los productos. El coste del reparto no cuenta para llegar a los 50 € y se suma después del descuento.

```text
Descuento = subtotal de productos × 0,10, si el subtotal es de 50 € o más
Total = subtotal de productos − descuento + reparto
Cambio = importe entregado − total
```

Si el dinero entregado no alcanza para pagar el pedido, el programa vuelve a pedir el importe completo.

## Ticket

El ticket muestra el número del pedido, el cliente, los productos y sus detalles, la entrega y el desglose del precio. También incluye el total, el dinero entregado y el cambio. Los importes aparecen con dos decimales.

## Caja del día

Mientras el programa está abierto, se guardan estos datos:

- Número de pedidos cobrados.
- Recaudación total.
- Importe medio por pedido.
- Pedido de mayor importe y nombre de su cliente.

La recaudación suma el total cobrado, incluido el reparto y después de aplicar descuentos. Si hay un empate en el pedido más caro, se conserva el primer pedido con ese importe. Los datos no se guardan al cerrar el programa.

## Validaciones y límites

- Los menús repiten la pregunta si se introduce una opción fuera del rango indicado.
- El nombre, la temática, los colores escritos a mano y la dirección no pueden estar vacíos ni contener solo espacios.
- Las cantidades deben ser al menos uno.
- No se aceptan pedidos vacíos ni pagos inferiores al total.
- Las entradas numéricas deben tener el formato esperado. Si se escriben letras u otro formato no válido, el programa puede producir un error.

## Archivos

| Archivo | Contenido |
|---|---|
| `Program.cs` | Código de la aplicación. |
| `README.md` | Descripción del proyecto y guía de uso. |
