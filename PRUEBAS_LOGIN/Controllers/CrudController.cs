using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using PRUEBAS_LOGIN.Models;

namespace PRUEBAS_LOGIN.Controllers
{
    public class CrudController : Controller
    {
        static string cadena = AccesoController.cadena;

        // GET: Usuarios
        public ActionResult About()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM USUARIO", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                        Correo = reader["Usuario"].ToString(),
                    };
                    usuarios.Add(usuario);
                }
            }

           
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            Usuario usuario = ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                InsertarUsuario(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            Usuario usuario = ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                ActualizarUsuario(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            Usuario usuario = ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EliminarUsuario(id);
            return RedirectToAction("Index");
        }

        private Usuario ObtenerUsuarioPorId(int id)
        {
            Usuario usuario = null;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM USUARIO WHERE IdUsuario = @IdUsuario", cn);
                cmd.Parameters.AddWithValue("@IdUsuario", id);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                        Correo = reader["Usuario"].ToString(),
                    };
                }
            }
            return usuario;
        }

        private void InsertarUsuario(Usuario usuario)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO USUARIO (Usuario, Clave) VALUES (@Usuario, @Clave)", cn);
                cmd.Parameters.AddWithValue("@Usuario", usuario.Correo);
                cmd.Parameters.AddWithValue("@Clave", usuario.Clave); // Aquí debes proporcionar la contraseña
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void ActualizarUsuario(Usuario usuario)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("UPDATE USUARIO SET Usuario = @Usuario WHERE IdUsuario = @IdUsuario", cn);
                cmd.Parameters.AddWithValue("@Usuario", usuario.Correo);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void EliminarUsuario(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM USUARIO WHERE IdUsuario = @IdUsuario", cn);
                cmd.Parameters.AddWithValue("@IdUsuario", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
