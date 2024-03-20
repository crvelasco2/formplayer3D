using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System;

public class FormularioController : MonoBehaviour
{
    public InputField txtNombre;
    public InputField txtUsuario;
    public InputField txtDireccion;
    public InputField txtCorreo;
    public InputField txtContraseña;

    public Button btnEnviar; // Asocia el botón en el Inspector de Unity

    private string connectionString;

    private void Start()
    {
        // Ruta a tu archivo de base de datos SQLite
        string dbPath = "URI=file:C:/Users/ligac/OneDrive/Escritorio/DBBVideojuegos/registro.db";
        connectionString = dbPath;

        // Llama al método OnBtnEnviarClick automáticamente al inicio del juego
        btnEnviar.onClick.AddListener(OnBtnEnviarClick);

        // Desactiva el botón después de ejecutar la consulta
    }

    private void OnBtnEnviarClick()
    {
        try
        {
            // Lee el contenido de los campos de texto
            string nombre = txtNombre.text;
            string usuario = txtUsuario.text;
            string direccion = txtDireccion.text;
            string correo = txtCorreo.text;
            string contraseña = txtContraseña.text;

            // Ejemplo: Insertar todos los campos en la base de datos con parámetros
            string insertQuery = "INSERT INTO usuarios (nombre, usuario, direccion, correo, contrasena) " +
                                "VALUES (@nombre, @usuario, @direccion, @correo, @contrasena)";

            // Ejecuta la consulta de inserción con parámetros
            ExecuteQueryWithParameters(insertQuery, nombre, usuario, direccion, correo, contraseña);

            // Puedes mostrar un mensaje de éxito o realizar otras acciones aquí
            Debug.Log("Datos enviados a la base de datos.");

            // Borra los campos del formulario después de enviar los datos
            LimpiarCampos();
        }
        catch (Exception e)
        {
            // Muestra un mensaje de error en la consola
            Debug.LogError($"Error al enviar datos a la base de datos: {e.Message}");
        }
    }

    private void ExecuteQueryWithParameters(string query, string nombre, string usuario, string direccion, string correo, string contraseña)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (SqliteCommand cmd = new SqliteCommand(query, connection))
            {
                // Agrega parámetros a la consulta
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@direccion", direccion);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@contrasena", contraseña);

                cmd.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    private void LimpiarCampos()
    {
        // Establece el texto de los campos de entrada a cadena vacía
        txtNombre.text = "";
        txtUsuario.text = "";
        txtDireccion.text = "";
        txtCorreo.text = "";
        txtContraseña.text = "";
    }
}
