﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuardaCapitaldaCultura2027.Models;
using GuardaCapitaldaCultura2027.Models.Context;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Mail;

namespace GuardaCapitaldaCultura2027.Controllers
{
    public class ContactosController : Controller
    {
        private readonly GuardaEventosBdContext _context;

        public ContactosController(GuardaEventosBdContext context)
        {
            _context = context;
        }

        // GET: Contactos
        [Authorize(Roles = "Admin, GestorEventos")]
        public IActionResult Index(string nome = null, int page = 1)
        {
            var pagination = new PagingInfoMunicipio
            {
                CurrentPage = page,
                PageSize = PagingInfoMunicipio.DEFAULT_PAGE_SIZE,
                TotalItems = _context.Contactos.Where(p => nome == null || p.Assunto.Contains(nome)).Count()
            };

            return View(
               new ListaContacto

               {
                   //await _context.Contactos.ToListAsync()
                   Contactos = _context.Contactos.Where(p => nome == null || p.Assunto.Contains(nome))
                                .OrderBy(p => p.Assunto)
                                .Skip((page - 1) * pagination.PageSize)
                                .Take(pagination.PageSize),
                   pagination = pagination,
                   SearchNome = nome
               }
                );
        }

        // GET: Contactos/Details/5
        [Authorize(Roles = "Admin, GestorEventos")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos
                .FirstOrDefaultAsync(m => m.ContactoId == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // GET: Contactos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactoId,Name,Sobrenome,Email,Assunto,Mensagem")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacto);
               await _context.SaveChangesAsync();
                /*****Mensagem de sucesso ******/
                ViewBag.title = "Contacto enviado Com Sucesso!";
                ViewBag.type = "alert-success";
                ViewBag.message = "Em breve entraremos em Contacto!";
                ViewBag.redirect = "/Contactos/Create"; // Request.Path
                return View("Mensagem");
                /*return RedirectToAction(nameof(Index));*/
            }
            return View(contacto);
        }

        // GET: Contactos/Edit/5
        [Authorize(Roles = "Admin, GestorEventos")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            return View(contacto);
        }

        // POST: Contactos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin, GestorEventos")]
        public async Task<IActionResult> Edit(int id, [Bind("ContactoId,Name,Sobrenome,Email,Assunto,Mensagem,Verificado,Resposta")] Contacto contacto)
        {
            if (id != contacto.ContactoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Contacto VerificarDados = await _context.Contactos.FindAsync(id);
                    VerificarDados.Verificado = true;
                    VerificarDados.Resposta = contacto.Resposta;
                    contacto = VerificarDados;

                    using (MailMessage message = new MailMessage("guardaeventtos@gmail.com", contacto.Email))
                    {
                        message.Subject = contacto.Assunto;
                        message.Body = contacto.Resposta;
                        message.IsBodyHtml = false;

                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            //NetworkCredential credencial =
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = new NetworkCredential("guardaeventtos@gmail.com", "Guarda@@1");
                            smtp.Port = 587;
                            smtp.Send(message);
                            //Permitir aplicações menos seguras: ATIVADO
                        }
                    }
                    contacto = VerificarDados;
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                    ViewBag.title = "Contacto Respondido Com Sucesso!";
                    ViewBag.type = "alert-success";
                    ViewBag.message = "Em breve entraremos em Contacto!";
                    ViewBag.redirect = "/Contactos/Index"; // Request.Path
                    return View("Mensagem");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.ContactoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contacto);
        }

        // GET: Contactos/Delete/5
        [Authorize(Roles = "Admin, GestorEventos")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos
                .FirstOrDefaultAsync(m => m.ContactoId == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, GestorEventos")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactoExists(int id)
        {
            return _context.Contactos.Any(e => e.ContactoId == id);
        }


        //  Contactos/Responder
        
        public IActionResult Responder()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Responder(Contacto model)
        {
            /*using (MailAddress message = new MailAddress("guardaeventtos@gmail.com", model.Email))
            {
                message.Subject = model.Assunto;
            }*/
            using (MailMessage message = new MailMessage("guardaeventtos@gmail.com", model.Email))
            {
                message.Subject = model.Assunto;
                message.Body = model.Mensagem;
                message.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    //NetworkCredential credencial =
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential("guardaeventtos@gmail.com", "Guarda@@1");
                    smtp.Port = 587;
                    smtp.Send(message);
                    //smtp.S(message);
                    //Permitir aplicações menos seguras: ATIVADO
                    ViewBag.title = "Contacto Respondido Com Sucesso!";
                    ViewBag.type = "alert-success";
                    ViewBag.message = "Em breve entraremos em Contacto!";
                    ViewBag.redirect = "/Contactos/Index"; // Request.Path
                    return View("Mensagem");
                }
                
            }
            
            //return View(Contacto);
        }


    }
}
