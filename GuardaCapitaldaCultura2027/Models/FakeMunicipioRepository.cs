﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCapitaldaCultura2027.Models
{
    public class FakeMunicipioRepository : IMunicipioRepository
    {
        public IEnumerable<Muicipio> Muicipios =>
            new List<Muicipio>
            {
                new Muicipio
                {
                    Nome ="Guarda",
                    ImagemNome= "guarda202028813.jpg"

                },
                new Muicipio
                {
                    Nome = "Aguiar da Beira",
                    ImagemNome = "Aguiar-da-Beira-Largo-Monumentos203011164.jpg"
                },
                new Muicipio
                {
                    Nome = "Celorico da Beira",
                    ImagemNome = "celorico203044890.jpg"
                },
                  new Muicipio
                  {
                      Nome = "Covilhã",
                      ImagemNome = "covilha202901619.jpg"
                  },
                new Muicipio
                {
                    Nome = "Fundão",
                    ImagemNome = "fundao203213648.jpg"
                },
                new Muicipio
                {
                    Nome = "Sabugal",
                    ImagemNome = "Sabugal202838363.jpg"
                },
                new Muicipio
                {
                    Nome = "Trancoso",
                    ImagemNome = "trancoso202743150.jpg"
                }
            };
    }
}
