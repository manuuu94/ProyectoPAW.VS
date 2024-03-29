﻿using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Validar(LoginViewModel empleado)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/Usuario/ValidarUsuario",empleado);
                response.EnsureSuccessStatusCode();
                //LoginViewModel loginViewModel = response.Content.ReadAsAsync<LoginViewModel>().Result;
                var content = response.Content.ReadAsStringAsync().Result;
                List<LoginViewModel> login = JsonConvert.DeserializeObject<List<LoginViewModel>>(content); //lista
                //ViewBag.Login = login.FirstOrDefault().Id_Empleado;
                //ViewData["ROL"] = login.FirstOrDefault().Id_Empleado;
                if (login.Count > 0)
                {
                    //Session
                    HttpContext.Session.SetInt32("SessionUser", (int)login.FirstOrDefault().Id_Empleado);

                    var ID = login.FirstOrDefault().Id_Empleado;

                    ServiceRepository serviceObj2 = new ServiceRepository();
                    HttpResponseMessage response2 = serviceObj2.GetResponse("api/Empleado/" + ID.ToString());
                    response2.EnsureSuccessStatusCode();
                    var content2 = response2.Content.ReadAsStringAsync().Result;
                    EmpleadoViewModel EmpleadoViewModel = response2.Content.ReadAsAsync<EmpleadoViewModel>().Result;
                    //List<EmpleadoViewModel> login2 = JsonConvert.DeserializeObject<List<EmpleadoViewModel>>(content2);
                    TempData["ROL"] = EmpleadoViewModel.IdRol;
                    TempData["NOMBRE"] = EmpleadoViewModel.Nombre;
                    TempData["APELLIDO"] = EmpleadoViewModel.Apellido1;

                    return RedirectToAction("Index","Home");
                }
                return RedirectToAction("Index");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            catch (Exception
            )
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult changePass(LoginViewModel user)
        {
            try
            {
                if ((int)HttpContext.Session.GetInt32("SessionUser") != null)
                {
                    int sessionUser = (int)HttpContext.Session.GetInt32("SessionUser");
            //ViewBag.user = sessionUser;
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/empleado/" + sessionUser.ToString());
            response.EnsureSuccessStatusCode();
            Models.EmpleadoViewModel EmpleadoViewModel = response.Content.ReadAsAsync<Models.EmpleadoViewModel>().Result;
            user.Id_Empleado = sessionUser;
            user.Username = EmpleadoViewModel.Username;
            user.Password = null;
            return View(user);
                }
                return RedirectToAction("Index", "Login");

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult changePassword(LoginViewModel user)
        {
            try
            {
                LoginHelper helper = new LoginHelper();
                helper.changePass(user);

                return RedirectToAction("changePass", "Login");
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult forgotPass()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Validar2(Login2ViewModel empleado)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/Usuario/validarUsuario2", empleado);
                response.EnsureSuccessStatusCode();
                //LoginViewModel loginViewModel = response.Content.ReadAsAsync<LoginViewModel>().Result;
                //var content = response.Content.ReadAsStringAsync().Result;
                //List<Login2ViewModel> login = JsonConvert.DeserializeObject<List<Login2ViewModel>>(content); //lista
                //if (login.Count > 0)
                //{
                //    //Session
                //    HttpContext.Session.SetInt32("SessionUser", (int)login.FirstOrDefault().Id_Empleado);


                //    return RedirectToAction("Index", "Home");
                //}
                return RedirectToAction("Index");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            catch (Exception
            )
            {

                throw;
            }
        }






        [HttpGet]
        public ActionResult LogOut()
        {
            //modelocarrito.VaciarCarrito();
            HttpContext.Session.Clear();


            return RedirectToAction("Index", "Login");
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
