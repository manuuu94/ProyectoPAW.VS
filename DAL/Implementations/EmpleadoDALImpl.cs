﻿using DAL.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class EmpleadoDALImpl : IEmpleadoDAL
    {

        PROYECTO_PAWContext context;

        //constructor vacio (sin argumento o parametro)
        public EmpleadoDALImpl()
        {
            context = new PROYECTO_PAWContext();
        }
        public EmpleadoDALImpl(PROYECTO_PAWContext proyectoPAWContext)
        {
            this.context = proyectoPAWContext;
        }

        public bool Add(Empleado entity)
        {
            throw new NotImplementedException();
        }

        public bool AñadeEmpleado(EmpleadoNuevo empleado)
        {
            try
            {
                string sql = "[dbo].[añadeEmpleado] @PNOMBRE,@PAPELLIDO1," +
                    "@PAPELLIDO2,@PUSERNAME,@PPASSWORD," +
                    "@PFECHAINGRESO,@PCORREO,@PIDROL";
                var param = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                    ParameterName = "@PNOMBRE",
                    //tipo de variable en el sql
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = empleado.Nombre
                    },
                    new SqlParameter()
                    {
                    ParameterName = "@PAPELLIDO1",
                    //tipo de variable en el sql
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = empleado.Apellido1
                    },
                    new SqlParameter()
                    {
                    ParameterName = "@PAPELLIDO2",
                    //tipo de variable en el sql
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = empleado.Apellido2
                    },
                    new SqlParameter()
                    {
                    ParameterName = "@PUSERNAME",
                    //tipo de variable en el sql
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = empleado.Username
                    },
                    new SqlParameter()
                    {
                    ParameterName = "@PPASSWORD",
                    //tipo de variable en el sql
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = empleado.Password
                    },
                    new SqlParameter()
                    {
                    ParameterName = "@PFECHAINGRESO",
                    //tipo de variable en el sql
                    SqlDbType = System.Data.SqlDbType.Date,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = DateTime.Now
                    },
                    new SqlParameter()
                    {
                    ParameterName = "@PCORREO",
                    //tipo de variable en el sql
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 40,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = empleado.Correo
                    },
                    new SqlParameter()
                    {
                    ParameterName = "@PIDROL",
                    //tipo de variable en el sql
                    SqlDbType = System.Data.SqlDbType.Int,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = empleado.IdRol
                    }
                };

                context.Database.ExecuteSqlRaw(sql, param);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ////public bool ValidarUsuario(Usuario usuario)
        //{
        //    using (var conexion = new PROYECTO_PAWContext())
        //    {
        //        try
        //        {
        //            Usuario result;
        //            string sql = "[dbo].[validarUsuario] @PUSERNAME, @PPASSWORD";
        //            var param = new SqlParameter[]
        //            {
        //                new SqlParameter()
        //                {
        //                    ParameterName = "@PUSERNAME",
        //                    SqlDbType = System.Data.SqlDbType.VarChar,
        //                    Size = 20,
        //                    Direction = System.Data.ParameterDirection.Input,
        //                    Value = usuario.Username
        //                },
        //                new SqlParameter()
        //                {
        //                    ParameterName = "@PPASSWORD",
        //                    SqlDbType = System.Data.SqlDbType.VarChar,
        //                    Size = 20,
        //                    Direction = System.Data.ParameterDirection.Input,
        //                    Value = usuario.Username
        //                }
        //            };
        //            result = context.usuario.FromSqlRaw(sql, param).FirstAsync().Result;
        //            if(result != null)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            conexion.Dispose();
        //            throw ex;
        //        }
        //    }
        //}


        //public bool Add(Empleado entity)
        //{
        //    try
        //    {
        //        using (UnidadDeTrabajo<Empleado> unidad = new UnidadDeTrabajo<Empleado>(context))
        //        {

        //            unidad.genericDAL.Add(entity);
        //            return unidad.Complete();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public void AddRange(IEnumerable<Empleado> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Empleado> Find(Expression<Func<Empleado, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Empleado Get(int id)
        {
            try
            {
               Empleado empleado;
               using (UnidadDeTrabajo<Empleado> unidad = new UnidadDeTrabajo<Empleado>(context))
                {
                    //instancia el metodo de IDAL get a partir de unidaddetrabajo
                    empleado = unidad.genericDAL.Get(id);
                }
                return empleado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Empleado> Get()
        {
            using (var conexion = new PROYECTO_PAWContext())
            {
                try
                {
                    var datos = (from x in conexion.Empleados
                                 select x).ToList();
                    List<Empleado> lista = new List<Empleado>();
                    foreach (var dato in datos)
                    {
                        lista.Add(new Empleado
                        {
                            IdEmpleado = dato.IdEmpleado,
                            Nombre = dato.Nombre,
                            Apellido1 = dato.Apellido1,
                            Apellido2 = dato.Apellido2,
                            Username = dato.Username,
                            Passhash = null,
                            FechaIngreso = dato.FechaIngreso,
                            IdRol = dato.IdRol,
                            Correo = dato.Correo
                        });
                    }
                    return lista;
                }
                catch (Exception ex)
                {
                    conexion.Dispose();
                    throw ex;
                }
            }
        }
        
        public IEnumerable<Empleado> GetAll()
        {
            try
            {
                IEnumerable<Empleado> empleados;
                using (UnidadDeTrabajo<Empleado> unidad = new UnidadDeTrabajo<Empleado>(context))
                {
                    empleados = unidad.genericDAL.GetAll();
                }
                return empleados;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(Empleado entity)
        {
            bool result = false;
            try
            {
                using (UnidadDeTrabajo<Empleado> unidad = new UnidadDeTrabajo<Empleado>(context))
                {
                    unidad.genericDAL.Remove(entity);
                    result = unidad.Complete();
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public void RemoveRange(IEnumerable<Empleado> entities)
        {
            throw new NotImplementedException();
        }

        public Empleado SingleOrDefault(Expression<Func<Empleado, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        public bool Update(Empleado empleado)
        {
            using (var conexion = new PROYECTO_PAWContext())
            {
                try
                {
                    var datos = (from x in conexion.Empleados
                                 where x.IdEmpleado == empleado.IdEmpleado
                                 select x).FirstOrDefault();
                    if (datos != null)
                    {
                        datos.Nombre = empleado.Nombre;
                        datos.Apellido1 = empleado.Apellido1;
                        datos.Apellido2 = empleado.Apellido2;
                        datos.Username = empleado.Username;
                        // datos.PASSWORD = null;
                        //datos.FECHA_INGRESO = empleado.FECHA_INGRESO;
                        datos.IdRol = empleado.IdRol;
                        datos.Correo = empleado.Correo;

                        conexion.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    conexion.Dispose();
                    throw ex;

                }
            }
        }
        //{
        //    bool result = false;
        //    try
        //    {
        //        using (UnidadDeTrabajo<Empleado> unidad = new UnidadDeTrabajo<Empleado>(context))
        //        {
        //            unidad.genericDAL.Update(empleado);
        //            result = unidad.Complete();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    return result;
        //}
        /*
                public bool Update(Empleado entity)
                {
                    throw new NotImplementedException();
                }
        */
        //public List<Empleado> GetUser(string user)
        //{
        //    using (var conexion = new PROYECTO_PAWContext())
        //    {
        //        try
        //        {
        //            var datos = (from x in conexion.Empleados
        //                         where x.Username == user
        //                         select x).ToList();
        //            List<Empleado> lista = new List<Empleado>();
        //            foreach (var dato in datos)
        //            {
        //                lista.Add(new Empleado
        //                {
        //                    IdEmpleado = dato.IdEmpleado,
        //                    Nombre = dato.Nombre,
        //                    Apellido1 = dato.Apellido1,
        //                    Apellido2 = dato.Apellido2,
        //                    Username = dato.Username,
        //                    Passhash = null,
        //                    FechaIngreso = dato.FechaIngreso,
        //                    IdRol = dato.IdRol,
        //                    Correo = dato.Correo
        //                });
        //            }
        //            return lista;
        //        }
        //        catch (Exception ex)
        //        {
        //            conexion.Dispose();
        //            throw ex;
        //        }
        //    }
        //}
    }
}
