using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreAPI.Models;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		// GET: api/<ValuesController>
		[HttpGet]
		[Route("TestDataTableAsync")]
		public async Task<object> TestDataTableAsync()
		{
			DatabaseConnection databaseConnection = new DatabaseConnection();
			Stopwatch sw = new Stopwatch();
			sw.Start();
			DataTable dTable = new DataTable();
			dTable = await databaseConnection.GetTableAsync();
			sw.Stop();
			var obj = new
			{
				ElapsedMilliSeconds = sw.ElapsedMilliseconds
			};
			return obj;
		}


		// GET api/<ValuesController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<ValuesController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<ValuesController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
