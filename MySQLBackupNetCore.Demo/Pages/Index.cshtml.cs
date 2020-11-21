using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace MySQLBackupNetCore.Demo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IWebHostEnvironment env)
        {
            _logger = logger;
            _configuration = configuration;
            _env = env;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var path = Path.Combine(_env.ContentRootPath, "Content", "backup.sql");
            //Server=localhost;Database=db_name;User=usr;Password=pwd;CharSet=utf8;convertzerodatetime=true;
            var connStr = _configuration.GetConnectionString("DbConnection");

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(path);
                        conn.Close();
                    }
                }
            }
            return Page();
        }
    }
}
