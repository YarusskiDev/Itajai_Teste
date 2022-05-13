using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Teste_Sc.Models;
using System.Text.Json;
using AutoMapper;
using Teste_Sc.ViewModels;
using Teste_Sc.Servicos;

namespace Teste_Sc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExcelServico _excelService;
        private readonly IMapper _mapper;
        private readonly EmailServico _emailServico;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, ExcelServico excelServico, EmailServico emailServico)
        {
            _emailServico = emailServico;
            _excelService = excelServico;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnviaPlanilhaComTodosEstados(DistritosViewModel dados)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("Index");
            }

            var urlEstados = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
            HttpClient client = new();

            var dadosEstados = await client.GetAsync(urlEstados);
            var resultadoDosEstados = await dadosEstados.Content.ReadAsStringAsync();

            var resultSerializadoEstados = _mapper.Map<List<EstadosViewModel>>(JsonSerializer.Deserialize<List<Distritos>>(resultadoDosEstados));

            await _excelService.CriaPlanilhaExcelEstados(resultSerializadoEstados);
            _emailServico.EnviaEmailEstados(dados.Email);

            return Redirect("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EnviaPlanilhaCidadesDoEstado(DistritosViewModel dados_Uf)
        {

            if (!ModelState.IsValid)
            {
                return View(dados_Uf);
            }
            var urlDistritos = "https://servicodados.ibge.gov.br/api/v1/localidades/estados/" + dados_Uf.UF + "/municipios";
            HttpClient client = new();

            var dadosDistritos = await client.GetAsync(urlDistritos);
            var resultadoDasCidades = await dadosDistritos.Content.ReadAsStringAsync();

            var cidadesSerializadas = _mapper.Map<List<CidadesViewModel>>(JsonSerializer.Deserialize<List<Distritos>>(resultadoDasCidades));

            await _excelService.CriaPlanilhaExcelCidades(cidadesSerializadas);
            _emailServico.EnviaEmailCidades(dados_Uf.Email);

            return Redirect("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
