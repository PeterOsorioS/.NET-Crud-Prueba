# CRUD .NET - Documentación de API REST

"CRUD .NET" es un backend desarrollado en C# utilizando ASP.NET Web API, que permite a los usuarios registrarse para gestionar productos y realizar compras. Los usuarios pueden registrarse, iniciar sesión, crear, ver, editar y eliminar productos, además de la posibilidad de comprarlos. Las acciones de crear, editar y eliminar productos solo pueden ser realizadas por usuarios autorizados, ya que el sistema implementa autorización basada en roles. La seguridad está garantizada mediante autenticación JWT para proteger las operaciones sensibles.

## Base URL

La URL base de la API es: `http://localhost:8080`

## Tecnologías Utilizadas

- **C#**: Lenguaje de programación utilizado para desarrollar la lógica del backend.
- **.NET Core**: Framework utilizado para construir la API.
- **Entity Framework Core**: ORM utilizado para manejar la interacción con la base de datos.
- **PostgreSQL**: Base de datos relacional utilizada para almacenar los datos de productos, compras y usuarios.
- **DTOs (Data Transfer Objects)**: Objetos diseñados para transferir datos entre capas del sistema, mejorando la claridad y simplicidad del código.
- **Automapper**: Librería para mapear entidades y DTOs.
- **JWT (JSON Web Token)**: Mecanismo para autenticación y autorización, permitiendo a los usuarios iniciar sesión y acceder a recursos protegidos.
- **Middleware para JWT**: Implementación de middleware que verifica los tokens de autenticación en las solicitudes protegidas.
- **Repository Pattern**: Patrón de diseño utilizado para encapsular la lógica de acceso a datos y proporcionar una interfaz limpia y sencilla.
- **Service Layer**: Capa que contiene la lógica de negocio de la aplicación, asegurando que la lógica de la aplicación esté separada de los controladores.
- **Patrones de Diseño**: Uso del patrón Repository y capa de Servicios.
- **Docker**: Para la contenedorización de la aplicación y la base de datos.

## Controladores

Los controladores gestionan las operaciones relacionadas con los usuarios y productos. Se han implementado los siguientes controladores:

- **AuthController**: Maneja el registro e inicio de sesión de los usuarios, incluyendo la validación de credenciales y la generación de tokens JWT.
    
- **ProductController**: Se encarga de las operaciones CRUD (Crear, Leer, Actualizar y Eliminar) de los productos en el sistema.
    
- **PurchaseController**: Permite a los usuarios registrar compras y listar las compras realizadas.
    

## Funcionalidades

La API está diseñada para cumplir con los siguientes requisitos funcionales:

- Registro de usuarios para permitir el acceso a las funcionalidades de compra.
    
- Autenticación mediante JWT, asegurando que solo los usuarios registrados puedan realizar operaciones sensibles.
    
- CRUD completo para productos, permitiendo la gestión de estos desde la creación hasta la eliminación.
    
- Registro de compras, facilitando a los usuarios comprar uno o varios productos a la vez.
    
- Relación de muchos a muchos entre productos y compras, permitiendo flexibilidad en las operaciones de compra.
    

## Buenas Prácticas y Código Limpio

El proyecto sigue buenas prácticas de desarrollo, asegurando que el código sea claro, bien estructurado y fácil de mantener. Esto incluye:

- Uso de comentarios explicativos donde sea necesario.
    
- Separación clara entre las diferentes capas de la aplicación (controladores, servicios, repositorios).

## Requisitos Previos
Tener instalados los siguientes componentes antes de comenzar:
- **Visual Studio:** Para el desarrollo de la aplicación.
- **PostgreSQL:** (Opcional) Si prefieres gestionar la base de datos localmente.
- **Docker y Docker Compose:** (Opcional) Si prefieres usar contenedores, puedes ejecutar tanto la aplicación como la base de datos en Docker. En este caso, no es necesario instalar PostgreSQL localmente, ya que Docker puede levantar los servicios automáticamente.

## Instalación sin Docker
1. Clona el repositorio:
   ```bash
   git clone https://github.com/PeterOsorioS/WebCarrito-PostgreSQL.git
2. Abre el proyecto en Visual Studio.
3. Establece ShopMaster como proyecto de inicio.
4. Restaura los paquetes NuGet.
   - Abre una terminal en la carpeta raíz de tu proyecto.
   - Ejecuta el siguiente comando:

   ```bash
   dotnet restore 
5. Configura la cadena de conexión en el archivo de configuración.
   - Modificación del archivo `appsettings.json`
   - El archivo `appsettings.json` es donde se almacenan las configuraciones de la aplicación, incluida la cadena de conexión, modifica el archivo poniendo tus credenciales como se muestra a continuación:
    ```json
    {
      "ConnectionStrings": {
        "ConexionSQL": "Host={NombreHost}; ;Port={NumeroDelPuerto}; Database={NombreBaseDeDatos};User Id={Usuario};Password={Contraseña};Pooling=true;SSL Mode=Disable"
      }
    }
6. La creacion de la base de datos y las tablas se crean automaticamente con la primera ejecucion del codigo.
## Instalación con Docker
1. Clona el repositorio:
   ```bash
   git clone https://github.com/PeterOsorioS/WebCarrito-PostgreSQL.git
2. Abre la terminal en la carpeta raiz.
3. Ejecuta el siguiente comando:
   ```bash
   docker-compose up
4. Accede a la aplicación:

   - Pon a correr el contenedor docker
   - Recordar tener el servicio de Postgres y la aplicacion corriendo
   - Una vez que el contenedor esté corriendo, la aplicación estara disponible en http://localhost:8080.
   
## Endpoints

### Autenticación

- **Registrar Usuario**
  - **Endpoint**: `POST /api/auth/register`
  - **Request Body**:
    ```json
    {
      "name": "string",               // Nombre del usuario
      "email": "string",              // Correo electrónico del usuario
      "password": "string",           // Contraseña del usuario
      "confirmPassword": "string",    // Confirmación de la contraseña
      "money": 0                      // Cantidad de dinero inicial del usuario
    }
    ```
  - **Response**: Mensaje de éxito o error.

- **Iniciar Sesión**
  - **Endpoint**: `POST /api/auth/login`
  - **Request Body**:
    ```json
    {
      "email": "string",              // Correo electrónico del usuario
      "password": "string"            // Contraseña del usuario
    }
    ```
  - **Response**: Token de autenticación o mensaje de error.

### Productos

- **Listar Todos los Productos**
  - **Endpoint**: `GET /api/products`
  - **Response**: Lista de productos.

- **Obtener Producto por ID**
  - **Endpoint**: `GET /api/products/{id}`
  - **Response**: Detalles del producto.

- **Crear Producto**
  - **Endpoint**: `POST /api/products`
  - **Request Body**:
    ```json
    {
      "name": "string",               // Nombre del producto
      "category": "string",           // Categoría del producto
      "price": 0,                     // Precio del producto
      "quantity": 0                   // Cantidad disponible del producto
    }
    ```
  - **Response**: Mensaje de éxito o error.

- **Actualizar Producto**
  - **Endpoint**: `PUT /api/products/{id}`
  - **Request Body**:
    ```json
    {
      "name": "string",               // Nombre del producto
      "category": "string",           // Categoría del producto
      "price": 0,                     // Precio del producto
      "quantity": 0                   // Cantidad disponible del producto
    }
    ```
  - **Response**: Mensaje de éxito o error.

- **Eliminar Producto**
  - **Endpoint**: `DELETE /api/products/{id}`
  - **Response**: Mensaje de éxito o error.

### Compras

- **Listar Compras**
  - **Endpoint**: `GET /api/purchases`
  - **Response**: Lista de compras.

- **Crear Compra**
  - **Endpoint**: `POST /api/purchases`
  - **Request Body**:
    ```json
    {
      "products": [
        { "id": 0 }                     // ID del producto
      ],
      "userID": 0                      // ID del usuario
    }
    ```
  - **Response**: Mensaje de éxito o error.

- **Obtener Compras por Usuario**
  - **Endpoint**: `GET /api/purchases/{userId}`
  - **Response**: Lista de compras del usuario.

## Documentación

La API cuenta con documentación interactiva a través de **Swagger**, a la que puedes acceder al ejecutar la aplicación desde la siguiente URL:

[Swagger - Documentación Interactiva](http://localhost:8080/swagger/index.html)

Además, se ha proporcionado una colección de **Postman** que puedes importar para probar los endpoints fácilmente.