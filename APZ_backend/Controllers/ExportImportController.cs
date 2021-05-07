using Microsoft.AspNetCore.Http;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportImportController : ControllerBase
    {
        private readonly IExportImportService exportImportService;
        public ExportImportController(IExportImportService exportImportService)
        {
            this.exportImportService = exportImportService;
        }

        [HttpGet]
        public async Task<FileResult> Export()
        {

            byte[] cintent = await exportImportService.Export();
            return File(
                cintent,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Db.xlsx"
                );
        }
    }
}
