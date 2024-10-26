using System.Collections.Generic;
using System.Linq;
using tp.Models;
using Microsoft.Data.Sqlite;

namespace tp.Repositorios;


public class PresupuestosRepository {

    private string connectionString = "Data Source=db\\Tienda.db;";

    public void Create(Presupuestos item){
        using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string insertQuery = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@NombreDestinatario, @FechaCreacion);";
                SqliteCommand command = new SqliteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@NombreDestinatario", item.NombreDestinatario);
                command.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);
                connection.Open();
                command.ExecuteNonQuery();
                string insertQuery2 = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto,Cantidad) VALUES (@idPresupuesto,@idProducto,@Cantidad);";
                SqliteCommand command2 = new SqliteCommand(insertQuery2, connection);
                command2.Parameters.AddWithValue("@idPresupuesto", item.IdPresupuesto);
                foreach(PresupuestoDetalle detalle in item.Detalle){
                command2.Parameters.AddWithValue("@idPresupuesto", item.IdPresupuesto);
                command2.Parameters.AddWithValue("@idProducto", detalle.Productos.IdProducto);
                command2.Parameters.AddWithValue("@idCantidad", detalle.Cantidad);
                command2.ExecuteNonQuery();
                }
                connection.Close();
            }
    }

    public Presupuestos GetByID(int id)
{
    Presupuestos presupuesto = null;
    using (SqliteConnection connection = new SqliteConnection(connectionString))
    {
        string query = "SELECT idPresupuesto, NombreDestinatario, FechaCreacion FROM Presupuestos WHERE idPresupuesto = @IdPresupuesto;";
        SqliteCommand command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@IdPresupuesto", id);

        connection.Open();
        using (var reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                presupuesto = new Presupuestos
                {
                    IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                    NombreDestinatario = reader["NombreDestinatario"].ToString(),
                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                    Detalle = new List<PresupuestoDetalle>() // Inicializa la lista de detalles
                };
            }
        }

        connection.Close();
    }

    return presupuesto;
}

}