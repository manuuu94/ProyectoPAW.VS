﻿namespace FrontEnd.Models
{
    public class InventarioServiciosViewModel
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int CantidadDisponible { get; set; }
        public string UrlImage { get; set; }
        public int IdServicio { get; set; }
    }
}