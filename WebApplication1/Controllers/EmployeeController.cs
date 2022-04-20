using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	[Route("/api/[controller]")]
	public class EmployeeController : Controller
	{
		private static List<EmployeeModel> users = new List<EmployeeModel>() {
			new EmployeeModel
				{
				Id = 1,
				ProfileName = "Иванов Иван",
				ProfileFileName = "persone1.jpg",
				ProsonAge = "21",
				ProsonCity = "Москва",
				ProsonWork = "Университет синергия"
			},
			new EmployeeModel
			{
				Id = 2,
				ProfileName = "Гирасимов Семен",
				ProfileFileName = "persone1.jpg",
				ProsonAge = "22",
				ProsonCity = "Санкт-Питербург",
				ProsonWork = ""
			},
			new EmployeeModel
			{
				Id = 3,
				ProfileName = "Климов Антон",
				ProfileFileName = "persone1.jpg",
				ProsonAge = "23",
				ProsonCity = "Москва",
				ProsonWork = "Университет синергия"
			},
			new EmployeeModel
			{
				Id = 4,
				ProfileName = "Семенов Дмитрий",
				ProfileFileName = "persone1.jpg",
				ProsonAge = "24",
				ProsonCity = "Санкт-Питербург",
				ProsonWork = "Университет синергия"
			}
		};

		[HttpGet("{employeeId}")]
		public IActionResult GetEmployee(int employeeId)
		{
			return Ok(users.SingleOrDefault(_ => _.Id == employeeId));
		}

		[HttpGet]
		public IActionResult GetEmployees()
		{
			return Ok(users);
		}
	}
}
