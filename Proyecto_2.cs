namespace Proyecto_2; // agrupa el código bajo el nombre del proyecto

using System; // permite usar Console, Convert... sin escribir "System." delante

class Program
{
    static void Main(string[] args) // punto de entrada: el programa empieza a ejecutarse aquí
    {
        // VARIABLES DE LA CAJA
            int pedidosRealizados = 0;           // cuántos pedidos se han cobrado
            decimal recaudacion = 0;             // dinero total ingresado (decimal = números con céntimos)
            decimal pedidoMasCaro = 0;           // importe del pedido más caro
            string clientePedidoMasCaro = "";    // quién hizo ese pedido

        // Guarda la opción que el usuario elige en el menú principal.
        int opcionPrincipal = 0;

        // MENÚ PRINCIPAL
        do
        {
            // Se repite hasta que el usuario elige 0 (Salir).
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("             LA DULZURA DE MAMÁ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Crear un pedido");
            Console.WriteLine("2. Consultar la caja del día");
            Console.WriteLine("0. Salir");
            Console.Write("Selecciona una opción: ");

            opcionPrincipal = int.Parse(Console.ReadLine()!);

            if (opcionPrincipal == 1)// OPCIÓN 1: CREAR UN PEDIDO
            {
                Console.WriteLine();
                Console.WriteLine("--- NUEVO PEDIDO ---");// FASE 1. DATOS DEL CLIENTE

                string nombreCliente;

                do
                {
                    Console.Write("Nombre del cliente: ");// Pide el nombre y no deja continuar si está vacío.
                    nombreCliente = Console.ReadLine() ?? ""; // ?? "", si no se lee nada (null), usa texto vacío

                    // Trim() quita los espacios; si queda vacío, es inválido.
                    if (nombreCliente.Trim() == "") // Trim() quita espacios de los extremos; "   " pasa a ser ""
                    {
                        Console.WriteLine("El nombre no puede estar vacío.");
                    }
                }
                while (nombreCliente.Trim() == ""); // repite mientras el nombre esté vacío


                // FASE 2. PRODUCTOS DEL PEDIDO
                // El cliente puede añadir varios productos (pastel, cupcakes, bocadillos) hasta que elige 0 para terminar. Reiniciandose por cada nuevo pedido
                decimal subtotal = 0;          // suma del precio de todos los productos
                string detalleTicket = "";     // texto que se imprimirá en el ticket
                int productosEnPedido = 0;     // cuántos productos lleva el pedido

                int opcionProducto;

                do
                {
                    // Menú de productos, con el subtotal que lleva hasta ahora.
                    Console.WriteLine();
                    Console.WriteLine("--- ¿QUÉ QUIERES PEDIR? ---");
                    Console.WriteLine("1. Pastel");
                    Console.WriteLine("2. Cupcakes");
                    Console.WriteLine("3. Bocadillos");
                    Console.WriteLine("0. Terminar el pedido");
                    Console.WriteLine();
                    Console.WriteLine($"Productos añadidos: {productosEnPedido}"); // + une texto con número
                    Console.WriteLine($"Subtotal actual: {subtotal:F2} euros"); // "F2" es siempre 2 decimales

                    Console.Write("Selecciona una opción: ");
                    opcionProducto = int.Parse(Console.ReadLine()!);

                    // Se pone a true si hay que preguntar por los bocadillos (porque los pidió directamente o después de un pastel).
                    bool agregarBocadillos = false; // Se reinicia a false en cada vuelta del bucle.

                    // PRODUCTO 1: PASTEL
                    if (opcionProducto == 1)
                    {
                        // Aquí se va construyendo el texto del pastel para el ticket.
                        string detallePastel = "PASTEL\n"; // "\n" es el salto de línea dentro del texto
                        decimal precioPastel = 0;

                        // Define el precio base del pastel por tamaño.
                        int opcionTamano;
                        string tamano = "";

                        do
                        {
                            Console.WriteLine();
                            Console.WriteLine("--- TAMAÑO DEL PASTEL ---");
                            Console.WriteLine("1. Pequeño: 20,00 euros");
                            Console.WriteLine("2. Mediano: 30,00 euros");
                            Console.WriteLine("3. Grande: 40,00 euros");
                            Console.Write("Selecciona un tamaño: ");
                            opcionTamano = int.Parse(Console.ReadLine()!);

                            if (opcionTamano < 1 || opcionTamano > 3)
                            {
                                Console.WriteLine("Debes seleccionar una opción entre 1 y 3.");
                            }
                        }
                        while (opcionTamano < 1 || opcionTamano > 3);

                        // Según la opción, se guarda el nombre y el precio base.
                        if (opcionTamano == 1)
                        {
                            tamano = "Pequeño";
                            precioPastel = 20m; // m es el 20 es de tipo decimal (necesario para dinero)
                        }
                        else if (opcionTamano == 2)
                        {
                            tamano = "Mediano";
                            precioPastel = 30m;
                        }
                        else
                        {
                            tamano = "Grande";
                            precioPastel = 40m;
                        }

                        detallePastel += $"  Tamaño: {tamano}\n";

                        //Pisos del pastel, desde 1 a 5.
                        int pisos;

                        do
                        {
                            Console.WriteLine();
                            Console.WriteLine("--- PISOS ---");
                            Console.WriteLine("Cada piso extra cuesta 10,00 euros.");// Cada piso extra suma 10 euros.
                            Console.Write("¿Cuántos pisos quieres (1 a 5)? ");
                            pisos = int.Parse(Console.ReadLine()!);

                            if (pisos < 1 || pisos > 5)
                            {
                                Console.WriteLine("Debes seleccionar entre 1 y 5 pisos.");
                            }
                        }
                        while (pisos < 1 || pisos > 5);

                        // El primer piso ya está incluido en el precio del tamaño.
                        precioPastel += (pisos - 1) * 10m; // suma al precio, (pisos - 1) = pisos extra x 10 euros cada uno; con 1 piso no se cobra extra
                        detallePastel += $"  Pisos: {pisos}\n";

                        // --- DISPOSICIÓN DE LOS PISOS ---
                        if (pisos > 1) // con un solo piso no hay nada que acomodar, así que solo se pregunta si hay 2 o más
                        {
                            int opcionDisposicion;
                            string disposicion = "";

                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("--- DISPOSICIÓN DE LOS PISOS ---");
                                Console.WriteLine("1. En forma de torre: del más grande) al más pequeño.");
                                Console.WriteLine("2. Todos los pisos del mismo tamaño.");
                                Console.Write("¿Cómo quieres acomodar los pisos?: ");
                                opcionDisposicion = int.Parse(Console.ReadLine()!);

                                if (opcionDisposicion < 1 || opcionDisposicion > 2)
                                {
                                    Console.WriteLine("Debes seleccionar la opción 1 o 2.");
                                }
                            }
                            while (opcionDisposicion < 1 || opcionDisposicion > 2);

                            if (opcionDisposicion == 1)
                            {
                                disposicion = "En forma de torre (del más grande al más pequeño)";
                            }
                            else
                            {
                                disposicion = "Todos los pisos del mismo tamaño";
                            }

                            detallePastel += $"  Disposición: {disposicion}\n"; // Añade la disposición al texto del pastel para el ticket
                        }

                        //En este punto se pregunta si el pastel es temático o no.
                        int opcionTematica;
                        string tema = "";

                        do
                        {
                            Console.WriteLine();
                            Console.WriteLine("--- FIESTA TEMÁTICA ---");
                            Console.WriteLine("1. Sí: Se aumenta el precio a 15,00 euros");// Si es temática se pide el tema y se cobra el aumento.
                            Console.WriteLine("2. No");// Si no, se pregunta sabor, relleno, crema y adornos.
                            Console.Write("¿Es para una fiesta temática? ");
                            opcionTematica = int.Parse(Console.ReadLine()!);

                            if (opcionTematica < 1 || opcionTematica > 2)
                            {
                                Console.WriteLine("Debes seleccionar la opción 1 o 2.");
                            }
                        }
                        while (opcionTematica < 1 || opcionTematica > 2);

                        if (opcionTematica == 1)
                        {
                            // en el caso de que si es temática pide el tema y cobra el aumento.
                            do
                            {
                                Console.Write("¿Cuál es la temática? ");
                                tema = Console.ReadLine() ?? ""; // ?? "" si es null, texto vacío

                                if (tema.Trim() == "") // Trim() evita que valga con escribir solo espacios
                                {
                                    Console.WriteLine("La temática no puede estar vacía.");
                                }
                            }
                            while (tema.Trim() == "");

                            precioPastel += 15m; // suma el aumento de temática
                            detallePastel += $"  Temática: {tema}\n";
                        }
                        else
                        {
                            // En caso de que no es temática se elige sabor, relleno, crema y adornos (el sabor, la crema y los adornos suman su precio).

                            // --- SABOR ---
                            int opcionSabor;
                            string sabor = "";

                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("--- SABOR ---");// Cada sabor tiene un precio distinto que se suma al precio del pastel. La vainilla es el sabor estándar.
                                Console.WriteLine("1. Red velvet: 6,00 euros");
                                Console.WriteLine("2. Chocolate: 5,00 euros");
                                Console.WriteLine("3. Vainilla (estándar): sin coste extra");
                                Console.WriteLine("4. Naranja: 4,50 euros");
                                Console.Write("Selecciona un sabor: ");
                                opcionSabor = int.Parse(Console.ReadLine()!);

                                if (opcionSabor < 1 || opcionSabor > 4)
                                {
                                    Console.WriteLine("Debes seleccionar una opción entre 1 y 4.");
                                }
                            }
                            //Opciones de sabor del biscocho
                            while (opcionSabor < 1 || opcionSabor > 4);

                            if (opcionSabor == 1)
                            {
                                sabor = "Red velvet";
                                precioPastel += 6m; // += suma el precio del sabor al precio del pastel
                            }
                            else if (opcionSabor == 2)
                            {
                                sabor = "Chocolate";
                                precioPastel += 5m;
                            }
                            else if (opcionSabor == 3)
                            {
                                sabor = "Vainilla"; // sabor estándar: no suma nada al precio
                            }
                            else
                            {
                                sabor = "Naranja";
                                precioPastel += 4.50m;
                            }

                            //Relleno del pastel
                            int opcionRelleno;
                            string relleno = "";

                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("--- RELLENO ---");
                                Console.WriteLine("1. Dulce de leche");
                                Console.WriteLine("2. Mermelada");
                                Console.Write("Selecciona un relleno: ");
                                opcionRelleno = int.Parse(Console.ReadLine()!);

                                if (opcionRelleno < 1 || opcionRelleno > 2)
                                {
                                    Console.WriteLine("Debes seleccionar la opción 1 o 2.");
                                }
                            }
                            while (opcionRelleno < 1 || opcionRelleno > 2);

                            if (opcionRelleno == 1)
                            {
                                relleno = "Dulce de leche";//Primer relleno de dulce de leche
                            }
                            else
                            {
                                relleno = "Mermelada"; //Segundo relleno de mermelada
                            }

                            //Crema decorativa
                            int opcionCrema;
                            string crema = "";
                            int maxOpcionCrema = 2; // última opción válida del menú (normalmente 2)

                            if (opcionSabor == 2) // opcionSabor 2 = Chocolate
                            {
                                maxOpcionCrema = 3; // con sabor chocolate aparece una tercera opción
                            }

                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("--- CREMA ---");// Primero se elige el tipo de crema: dulce de leche o crema chantillí y chocolate , en caso de que se aya escogido pastel de chocolate.
                                Console.WriteLine("1. Dulce de leche: 3,00 euros");
                                Console.WriteLine("2. Crema chantillí: 4,00 euros");

                                if (opcionSabor == 2) // la opción 3 solo se muestra si el sabor del bizcocho es chocolate
                                {
                                    Console.WriteLine("3. Chocolate: 5,00 euros");
                                }

                                Console.Write("Selecciona una crema: ");
                                opcionCrema = int.Parse(Console.ReadLine()!);

                                if (opcionCrema < 1 || opcionCrema > maxOpcionCrema)
                                {
                                    Console.WriteLine($"Debes seleccionar una opción entre 1 y {maxOpcionCrema} .");
                                }
                            }
                            while (opcionCrema < 1 || opcionCrema > maxOpcionCrema); // el límite cambia según el sabor

                            if (opcionCrema == 1)
                            {
                                crema = "Dulce de leche";
                                precioPastel += 3m; // Suma el precio de la crema al precio del pastel
                            }
                            else if (opcionCrema == 2)// En caso de elegir chantillí se debe escoger el color   .
                            {
                                precioPastel += 4m;

                                int opcionColorChantilli;
                                string colorChantilli = "";

                                do
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("--- COLOR DE LA CREMA CHANTILLÍ ---"); //Se escoge el color de la crema chantillí, con 4 colores fijos y la opción 5 permite escribir otro.
                                    Console.WriteLine("1. Blanca.");
                                    Console.WriteLine("2. Azul.");
                                    Console.WriteLine("3. Roja.");
                                    Console.WriteLine("4. Amarilla.");
                                    Console.WriteLine("5. Otro (lo escribes tú).");
                                    Console.Write("Selecciona un color: ");
                                    opcionColorChantilli = int.Parse(Console.ReadLine()!);//Interpreta la opcion de número de color que elige el usuario a texto.

                                    if (opcionColorChantilli < 1 || opcionColorChantilli > 5)//En caso de que elija un color no válido, se le indica que debe elegir entre 1 y 5.
                                    {
                                        Console.WriteLine("Debes seleccionar una opción entre 1 y 5.");
                                    }
                                }
                                while (opcionColorChantilli < 1 || opcionColorChantilli > 5);//Siendo este el blucle que se repetirá hasta que elija un color válido.
                                //Las opciones del color que uno elija
                                if (opcionColorChantilli == 1)
                                {
                                    colorChantilli = "Blanca";
                                }
                                else if (opcionColorChantilli == 2)
                                {
                                    colorChantilli = "Azul";
                                }
                                else if (opcionColorChantilli == 3)
                                {
                                    colorChantilli = "Roja";
                                }
                                else if (opcionColorChantilli == 4)
                                {
                                    colorChantilli = "Amarilla";
                                }
                                else
                                {
                                    do
                                    {
                                        Console.Write("Escribe el color que quieres: ");
                                        colorChantilli = Console.ReadLine() ?? "";

                                        if (colorChantilli.Trim() == "")
                                        {
                                            Console.WriteLine("El color no puede estar vacío.");
                                        }
                                    }
                                    while (colorChantilli.Trim() == "");
                                }

                                crema = $"Crema chantillí ({colorChantilli})";
                            }
                            else
                            {
                                crema = "Chocolate";
                                precioPastel += 5m;
                            }

                            detallePastel += $"  Relleno: {relleno}\n";
                            detallePastel += $"  Crema: {crema}\n";

                            // --- ADORNOS ---
                            int opcionAdorno;
                            string adorno = "";

                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("--- ADORNOS ---");
                                Console.WriteLine("1. Fruta: 4,00 euros");
                                Console.WriteLine("2. Vela: 1,00 euro");
                                Console.WriteLine("3. Flores: 6,00 euros");
                                Console.WriteLine("4. Fruta y vela");
                                Console.WriteLine("5. Fruta y flores");
                                Console.WriteLine("6. Vela y flores");
                                Console.WriteLine("7. Todos");
                                Console.Write("Selecciona un adorno: ");
                                opcionAdorno = int.Parse(Console.ReadLine()!);

                                if (opcionAdorno < 1 || opcionAdorno > 7)
                                {
                                    Console.WriteLine("Debes seleccionar una opción entre 1 y 7.");
                                }
                            }
                            while (opcionAdorno < 1 || opcionAdorno > 7);

                            bool quiereFruta = opcionAdorno == 1 || opcionAdorno == 4 || opcionAdorno == 5 || opcionAdorno == 7;
                            bool quiereVela = opcionAdorno == 2 || opcionAdorno == 4 || opcionAdorno == 6 || opcionAdorno == 7;
                            bool quiereFlores = opcionAdorno == 3 || opcionAdorno == 5 || opcionAdorno == 6 || opcionAdorno == 7;

                            decimal precioAdornos = 0;

                            if (quiereFruta)
                            {
                                precioAdornos += 4m;
                                adorno += "Fruta, ";
                            }
                            if (quiereVela)
                            {
                                precioAdornos += 1m;
                                adorno += "Vela, ";
                            }
                            if (quiereFlores)
                            {
                                precioAdornos += 6m;
                                adorno += "Flores, ";
                            }

                            if (adorno.EndsWith(", "))
                            {
                                adorno = adorno.Substring(0, adorno.Length - 2);
                            }

                            precioPastel += precioAdornos;
                            detallePastel += $"  Adornos: {adorno}\n";
                        }

                        detallePastel += $"  Precio del pastel: {precioPastel:F2} euros\n";
                        subtotal += precioPastel;
                        detalleTicket += detallePastel;
                        productosEnPedido++;

                        Console.WriteLine("Pastel añadido al pedido.");

                        // Si el cliente pide bocadillos después del pastel, los puede añadir a continuación.
                        Console.Write("¿Quieres añadir bocadillos al pastel? (S/N): ");
                        string respuestaBocadillos = Console.ReadLine() ?? "";

                        if (respuestaBocadillos.ToUpper() == "S")
                        {
                            agregarBocadillos = true;
                        }
                    }

                    // PRODUCTO 2: CUPCAKES
                    else if (opcionProducto == 2)
                    {
                        int cantidadCupcakes;

                        do
                        {
                            Console.Write("¿Cuántos cupcakes quieres? ");
                            cantidadCupcakes = int.Parse(Console.ReadLine()!);

                            if (cantidadCupcakes < 1)
                            {
                                Console.WriteLine("Debes pedir al menos 1 cupcake.");
                            }
                        }
                        while (cantidadCupcakes < 1);

                        // Precio unitario según el sabor elegido.
                        int opcionSaborCupcake;
                        decimal precioPorCupcake = 0;
                        string saborCupcake = "";

                        do
                        {
                            Console.WriteLine();
                            Console.WriteLine("--- SABOR DE LOS CUPCAKES ---");
                            Console.WriteLine("1. Chocolate: 2,00 euros cada uno");
                            Console.WriteLine("2. Vainilla: 1,50 euros cada uno");
                            Console.WriteLine("3. Red velvet: 2,50 euros cada uno");
                            Console.Write("Selecciona un sabor: ");
                            opcionSaborCupcake = int.Parse(Console.ReadLine()!);

                            if (opcionSaborCupcake < 1 || opcionSaborCupcake > 3)
                            {
                                Console.WriteLine("Debes seleccionar una opción entre 1 y 3.");
                            }
                        }
                        while (opcionSaborCupcake < 1 || opcionSaborCupcake > 3);

                        if (opcionSaborCupcake == 1)
                        {
                            saborCupcake = "Chocolate";
                            precioPorCupcake = 2m;
                        }
                        else if (opcionSaborCupcake == 2)
                        {
                            saborCupcake = "Vainilla";
                            precioPorCupcake = 1.50m;
                        }
                        else
                        {
                            saborCupcake = "Red velvet";
                            precioPorCupcake = 2.50m;
                        }

                        // Color de la crema.
                        int opcionColor;
                        string colorCrema = "";
                        int maxOpcionColor = 4; // en los sabores normales solo hay 4 colores fijos; con sabor vainilla se añade "otro"

                        if (opcionSaborCupcake == 2) // vainilla
                        {
                            maxOpcionColor = 5;
                        }

                        do
                        {
                            Console.WriteLine();
                            Console.WriteLine("--- COLOR DE LA CREMA ---");
                            Console.WriteLine("1. Blanca");
                            Console.WriteLine("2. Azul");
                            Console.WriteLine("3. Roja");
                            Console.WriteLine("4. Amarilla");
                            Console.WriteLine("5. Otro (lo escribes tú)");
                            Console.Write("Selecciona un color: ");
                            opcionColor = int.Parse(Console.ReadLine()!);

                            if (opcionColor < 1 || opcionColor > maxOpcionColor)
                            {
                                Console.WriteLine("Debes seleccionar una opción entre 1 y 5.");
                            }
                        }
                        while (opcionColor < 1 || opcionColor > maxOpcionColor);

                        if (opcionColor == 1)
                        {
                            colorCrema = "Blanca";
                        }
                        else if (opcionColor == 2)
                        {
                            colorCrema = "Azul";
                        }
                        else if (opcionColor == 3)
                        {
                            colorCrema = "Roja";
                        }
                        else if (opcionColor == 4)
                        {
                            colorCrema = "Amarilla";
                        }
                        else
                        {
                            // Opción 5: el cliente escribe su propio color.
                            do
                            {
                                Console.Write("Escribe el color que quieres: ");
                                colorCrema = Console.ReadLine() ?? ""; // ?? "" -> si es null, texto vacío

                                if (colorCrema.Trim() == "") // Trim() -> quita espacios de los extremos
                                {
                                    Console.WriteLine("El color no puede estar vacío.");
                                }
                            }
                            while (colorCrema.Trim() == "");
                        }

                        //Precio y ticket de los cupcakes
                        decimal precioCupcakes = cantidadCupcakes * precioPorCupcake; // precio = cantidad x precio del sabor elegido; int x decimal da decimal

                        subtotal += precioCupcakes; // suma los cupcakes al total del pedido
                        detalleTicket += "CUPCAKES\n"; // += añade al texto del ticket; \n salta de línea
                        detalleTicket += $"  Cantidad: {cantidadCupcakes}\n";
                        detalleTicket += $"  Sabor: {saborCupcake}\n";
                        detalleTicket += $"  Crema: {colorCrema}\n";
                        detalleTicket += $"  Precio de los cupcakes: {precioCupcakes:F2} euros\n";
                        productosEnPedido++; // suma 1 al contador de productos

                        Console.WriteLine("Cupcakes añadidos al pedido.");
                    }

                    // PRODUCTO 3: BOCADILLOS (elegidos directamente)
                    else if (opcionProducto == 3)
                    {
                        agregarBocadillos = true; // Los bocadillos se piden en el bloque de más abajo, que también se usa cuando se añaden junto al pastel; enciende el interruptor
                    }

                    // TERMINAR EL PEDIDO (opción 0)
                    else if (opcionProducto == 0 && productosEnPedido == 0) // && = "y": quiere salir (0) Y el pedido está vacío
                    {
                        Console.WriteLine("El pedido está vacío. Añade al menos un producto."); // No se permite terminar un pedido vacío.
                        opcionProducto = -1; //el bucle solo acaba con 0, así que al poner -1 se obliga a repetir
                    }

                    // OPCIÓN NO VÁLIDA
                    else if (opcionProducto != 0) // != significa "distinto de"
                    {
                        Console.WriteLine("Debes seleccionar una opción entre 0 y 3.");
                    }

                    //Bloque de bocadillos: solo se ejecuta si agregarBocadillos es true, es decir, si los pidió directamente o después de un pastel.
                    if (agregarBocadillos) // equivale a "if (agregarBocadillos == true)"
                    {
                        int opcionBocadillos;

                        do// Menú con las 7 combinaciones posibles (0 = ninguno).
                        {
                            Console.WriteLine();
                            Console.WriteLine("--- BOCADILLOS ---");
                            Console.WriteLine("1. Alfajores: 1,50 euros cada uno");
                            Console.WriteLine("2. Empanadas de pollo: 2,00 euros cada una");
                            Console.WriteLine("3. Empanadas de carne: 2,00 euros cada una");
                            Console.WriteLine("4. Empanadas de pollo y de carne");
                            Console.WriteLine("5. Alfajores y empanadas de pollo");
                            Console.WriteLine("6. Alfajores y empanadas de carne");
                            Console.WriteLine("7. Los tres bocadillos");
                            Console.WriteLine("0. Ninguno");
                            Console.Write("Selecciona una opción: ");
                            opcionBocadillos = int.Parse(Console.ReadLine()!);

                            if (opcionBocadillos < 0 || opcionBocadillos > 7)
                            {
                                Console.WriteLine("Debes seleccionar una opción entre 0 y 7.");
                            }
                        }
                        while (opcionBocadillos < 0 || opcionBocadillos > 7);

                        if (opcionBocadillos != 0)// Si eligió 0, no se añade nada y se sigue con el pedido.
                        {
                            //Según la opción elegida se marca qué bocadillos lleva, por ejemplo la opción 5 activa alfajores y pollo.
                            bool quiereAlfajores = // bool = true/false; vale true si la opción es 1, 5, 6 o 7
                                opcionBocadillos == 1 || opcionBocadillos == 5
                                || opcionBocadillos == 6 || opcionBocadillos == 7; // || = "o": basta con que se cumpla una
                            bool quierePollo = // true si la opción es 2, 4, 5 o 7
                                opcionBocadillos == 2 || opcionBocadillos == 4
                                || opcionBocadillos == 5 || opcionBocadillos == 7;
                            bool quiereCarne = // true si la opción es 3, 4, 6 o 7
                                opcionBocadillos == 3 || opcionBocadillos == 4
                                || opcionBocadillos == 6 || opcionBocadillos == 7;

                            decimal precioBocadillos = 0;
                            string detalleBocadillos = "BOCADILLOS\n";

                            //Para cada bocadillo elegido se pregunta la cantidad, se suma su precio y se añade su línea al ticket.
                            if (quiereAlfajores)
                            {
                                int cantidadAlfajores;

                                do
                                {
                                    Console.Write("¿Cuántos alfajores quieres? ");
                                    cantidadAlfajores = int.Parse(Console.ReadLine()!);

                                    if (cantidadAlfajores < 1)
                                    {
                                        Console.WriteLine("Debes pedir al menos 1.");
                                    }
                                }
                                while (cantidadAlfajores < 1);

                                precioBocadillos += cantidadAlfajores * 1.50m; // += suma cantidad x precio al total de bocadillos
                                detalleBocadillos += "  - " + cantidadAlfajores + " alfajores\n";
                            }

                            if (quierePollo)
                            {
                                int cantidadPollo;

                                do
                                {
                                    Console.Write("¿Cuántas empanadas de pollo quieres? ");
                                    cantidadPollo = int.Parse(Console.ReadLine()!);

                                    if (cantidadPollo < 1)
                                    {
                                        Console.WriteLine("Debes pedir al menos 1.");
                                    }
                                }
                                while (cantidadPollo < 1);

                                precioBocadillos += cantidadPollo * 2m; // 2m -> el 2 es decimal
                                detalleBocadillos += $"  - {cantidadPollo} empanadas de pollo\n";
                            }

                            if (quiereCarne)
                            {
                                int cantidadCarne;

                                do
                                {
                                    Console.Write("¿Cuántas empanadas de carne quieres? ");
                                    cantidadCarne = int.Parse(Console.ReadLine()!);

                                    if (cantidadCarne < 1)
                                    {
                                        Console.WriteLine("Debes pedir al menos 1.");
                                    }
                                }
                                while (cantidadCarne < 1);

                                precioBocadillos += cantidadCarne * 2m;
                                detalleBocadillos += $"  - {cantidadCarne} empanadas de carne\n";
                            }

                            detalleBocadillos += $"  Precio de los bocadillos: {precioBocadillos:F2} euros\n"; 

                            //Se suman los bocadillos al pedido.
                            subtotal += precioBocadillos;       // suma los bocadillos al total del pedido
                            detalleTicket += detalleBocadillos; // pega el texto de bocadillos al ticket general
                            productosEnPedido++;                

                            Console.WriteLine("Bocadillos añadidos al pedido.");
                        }
                    }
                }
                while (opcionProducto != 0); // El bucle de productos termina cuando se elige 0 o se reinicia con cada nuevo pedido.

                //FASE 3. FORMA DE ENTREGA: recogida en local (gratis) o reparto (3 euros + dirección).
                int opcionEntrega;
                string formaEntrega;
                string direccion = "";
                decimal suplementoReparto = 0;

                do
                {
                    Console.WriteLine();
                    Console.WriteLine("--- FORMA DE ENTREGA ---");
                    Console.WriteLine("1. Recogida en local: sin suplemento");
                    Console.WriteLine("2. Reparto a domicilio: 3,00 euros");
                    Console.Write("Selecciona una opción: ");
                    opcionEntrega = int.Parse(Console.ReadLine()!);

                    if (opcionEntrega < 1 || opcionEntrega > 2)
                    {
                        Console.WriteLine("Debes seleccionar la opción 1 o 2.");
                    }
                }
                while (opcionEntrega < 1 || opcionEntrega > 2);

                if (opcionEntrega == 1)
                {
                    formaEntrega = "Recogida en local";
                }
                else
                {
                    formaEntrega = "Reparto a domicilio"; 
                    suplementoReparto = 3m; 

                    do
                    {
                        Console.Write("Dirección de entrega: ");
                        direccion = Console.ReadLine() ?? ""; 

                        if (direccion.Trim() == "") 
                        {
                            Console.WriteLine("La dirección no puede estar vacía.");
                        }
                    }
                    while (direccion.Trim() == "");
                }

                //FASE 4. CÁLCULO DEL IMPORTE Y DEL DESCUENTO: si los productos suman 50 euros o más, 10 % de descuento.
                decimal descuento = 0;

                if (subtotal >= 50m) // >= significa "mayor o igual que"; el descuento se calcula sobre los productos, el reparto no cuenta para alcanzar los 50 euros
                {
                    descuento = subtotal * 0.10m; // 0.10m = 10 % (decimal); multiplicar por 0,10 equivale a calcular el 10 %
                }

                decimal totalPedido = subtotal - descuento + suplementoReparto; // Total = productos - descuento + reparto.

                Console.WriteLine();
                Console.WriteLine("--- RESUMEN DEL IMPORTE ---");
                Console.WriteLine($"Productos: {subtotal:F2} euros"); 
                Console.WriteLine($"Descuento: {descuento:F2} euros");
                Console.WriteLine($"Reparto: {suplementoReparto:F2} euros");
                Console.WriteLine($"TOTAL A PAGAR: {totalPedido:F2} euros");

                //FASE 5. COBRO DEL PEDIDO: pide el dinero entregado hasta que cubra el total.
                decimal importeEntregado;

                do
                {
                    Console.Write("Pago del cliente: ");
                    importeEntregado = Convert.ToDecimal(Console.ReadLine()); // convierte el texto en decimal (permitiendo centimos)

                    if (importeEntregado < totalPedido)
                    {
                        Console.WriteLine("Importe insuficiente. Debes introducir de nuevo el importe completo entregado.");
                    }
                }
                while (importeEntregado < totalPedido); // repite mientras el dinero no alcance

                decimal cambio = importeEntregado - totalPedido; // Cambio a devolver al cliente.

                //FASE 6. ACTUALIZACIÓN DE LA CAJA: se suma el pedido a las estadísticas del día, solo cuando ya se ha cobrado.
                pedidosRealizados++;       
                recaudacion += totalPedido;   

                if (pedidosRealizados == 1 || totalPedido > pedidoMasCaro) // el primer pedido siempre es el más caro hasta ahora; si hay empate se conserva el primer pedido de ese importe
                {
                    pedidoMasCaro = totalPedido;
                    clientePedidoMasCaro = nombreCliente;
                }

                //FASE 7. TICKET DEL PEDIDO: imprime el resumen completo del pedido cobrado.
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.WriteLine("       LA DULZURA DE MAMÁ - TICKET");
                Console.WriteLine("========================================");
                Console.WriteLine($"Pedido número: {pedidosRealizados}");
                Console.WriteLine($"Cliente: {nombreCliente}");
                Console.WriteLine("----------------------------------------");

                Console.Write(detalleTicket); // detalleTicket ya contiene las líneas de cada producto pedido; Write (sin Line) porque cada línea del texto ya termina en \n

                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Entrega: {formaEntrega}");

                if (opcionEntrega == 2) // La dirección solo se muestra si fue reparto a domicilio.
                {
                    Console.WriteLine($"Dirección: {direccion}");
                }

                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Subtotal productos:   {subtotal:F2} euros"); // los espacios del texto alinean las columnas del ticket
                Console.WriteLine($"Descuento:           -{descuento:F2} euros");
                Console.WriteLine($"Suplemento reparto:   {suplementoReparto:F2} euros");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"TOTAL:                {totalPedido:F2} euros");
                Console.WriteLine($"Entregado:            {importeEntregado:F2} euros");
                Console.WriteLine($"Cambio:               {cambio:F2} euros");
                Console.WriteLine("========================================");
                Console.WriteLine("¡Gracias por comprar en La Dulzura de Mamá!");
            }
            else if (opcionPrincipal == 2)// OPCIÓN 2: CONSULTAR LA CAJA DEL DÍA, muestra las estadísticas acumuladas de todos los pedidos.
            {
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.WriteLine("             CAJA DEL DÍA");
                Console.WriteLine("========================================");

                if (pedidosRealizados == 0)//Si el cliente no hace ningún pedido, no hay estadísticas que mostrar.
                {
                    Console.WriteLine("Todavía no se ha realizado ningún pedido.");
                }
                else
                {
                    decimal importeMedio = recaudacion / pedidosRealizados; // Importe medio = dinero total entre número de pedidos; / divide, y decimal / int da decimal

                    Console.WriteLine($"Pedidos realizados: {pedidosRealizados}");
                    Console.WriteLine($"Recaudación: {recaudacion:F2} euros");
                    Console.WriteLine($"Importe medio: {importeMedio:F2} euros");
                    Console.WriteLine($"Pedido más caro: {pedidoMasCaro:F2} euros");
                    Console.WriteLine($"Cliente del pedido más caro: {clientePedidoMasCaro}");
                }
            }
            else if (opcionPrincipal == 0)// LA OPCIÓN 0 o SALIR, muestra un resumen final y el bucle principal termina.
            {
                Console.WriteLine();
                Console.WriteLine("Cerrando La Dulzura de Mamá...");
                Console.WriteLine($"Pedidos realizados: {pedidosRealizados}");
                Console.WriteLine($"Recaudación final: {recaudacion:F2} euros");
                Console.WriteLine("¡Hasta pronto!");
            }
            else// Cualquier otra opción que no sea válida.
            {
                Console.WriteLine("Opción incorrecta. Selecciona 1, 2 o 0.");
            }
        }
        while (opcionPrincipal != 0); // El programa se repite hasta que se elige 0; != "distinto de": sigue mientras no se elija salir

    }
}