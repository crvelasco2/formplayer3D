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
    public InputField txtContrase�a;

    public Button btnEnviar; // Asocia el bot�n en el Inspector de Unity

    private string connectionString;

    private void Start()
    {
        // Ruta a tu archivo de base de datos SQLite
        string dbPath = "URI=file:C:/Users/ligac/OneDrive/Escritorio/DBBVideojuegos/registro.db";
        connectionString = dbPath;

        // Llama al m�todo OnBtnEnviarClick autom�ticamente al inicio del juego
        btnEnviar.onClick.AddListener(OnBtnEnviarClick);

        // Desactiva el bot�n despu�s de ejecutar la consulta
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
            string contrase�a = txtContrase�a.text;

            // Ejemplo: Insertar todos los campos en la base de datos con par�metros
            string insertQuery = "INSERT INTO usuarios (nombre, usuario, direccion, correo, contrasena) " +
                                "VALUES (@nombre, @usuario, @direccion, @correo, @contrasena)";

            // Ejecuta la consulta de inserci�n con par�metros
            ExecuteQueryWithParameters(insertQuery, nombre, usuario, direccion, correo, contrase�a);

            // Puedes mostrar un mensaje de �xito o realizar otras acciones aqu�
            Debug.Log("Datos enviados a la base de datos.");

            // Borra los campos del formulario despu�s de enviar los datos
            LimpiarCampos();
        }
        catch (Exception e)
        {
            // Muestra un mensaje de error en la consola
            Debug.LogError($"Error al enviar datos a la base de datos: {e.Message}");
        }
    }

    private void ExecuteQueryWithParameters(string query, string nombre, string usuario, string direccion, string correo, string contrase�a)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (SqliteCommand cmd = new SqliteCommand(query, connection))
            {
                // Agrega par�metros a la consulta
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@direccion", direccion);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@contrasena", contrase�a);

                cmd.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    private void LimpiarCampos()
    {
        // Establece el texto de los campos de entrada a cadena vac�a
        txtNombre.text = "";
        txtUsuario.text = "";
        txtDireccion.text = "";
        txtCorreo.text = "";
        txtContrase�a.text = "";
    }
}
