namespace tp.Models
{
public class PresupuestoDetalle
{
    private Productos productos;
    private int cantidad;

    public PresupuestoDetalle(Productos productos, int cantidad)
    {
        this.Productos = productos;
        this.Cantidad = cantidad;
    }

    public int Cantidad { get => cantidad; set => cantidad = value; }
    internal Productos Productos { get => productos; set => productos = value; }
}
}