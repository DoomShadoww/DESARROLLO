using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PRUEBAS_LOGIN.Models;

namespace PRUEBAS_LOGIN.Models
{
    public class ApplicationDbContext : DbContext
    {
        readonly string cadena = "Data Source=DESKTOP-8K6FEAS;Initial Catalog=DB_ACCESO;Integrated Security=true";
        public ApplicationDbContext() : base("Conn")
        {
             
        }

        public DbSet<Usuario> USUARIO { get; set; }
    }
}