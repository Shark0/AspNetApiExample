using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Resources;
using WebApplication1.Attribute;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

	[JwtAuthAction]
	public class ProductController : ApiController
	{

		ExampleEntities exampleEntities = new ExampleEntities();

		[Route("product")]
		[HttpGet]
		public IEnumerable<PRODUCT> Get()
		{
			return exampleEntities.PRODUCT;
		}

		[Route("product/{id}")]
		[HttpGet]
		public PRODUCT Get(int id)
		{
			return exampleEntities.PRODUCT.Find(id);
		}

		/// <summary>
		/// 搜尋產品
		/// </summary>
		/// <param name="pageNumber">頁碼</param>
		/// <param name="pageSize">大小</param>
		/// <returns>產品列表</returns>
		[Route("product/search")]
		[HttpGet]
		public IEnumerable<PRODUCT> Get(int pageNumber, int pageSize)
		{
			return exampleEntities.PRODUCT.Skip(pageNumber * pageSize).Take(pageSize);
		}

		[Route("product")]
		[HttpPost]
		public void Post([FromBody] PRODUCT product)
		{
			exampleEntities.PRODUCT.Add(product);
			exampleEntities.SaveChanges();
		}

		[Route("product")]
		[HttpPut]
		public void Put([FromBody] PRODUCT product)
		{
			exampleEntities.PRODUCT.Find(product.ID).NAME = product.NAME;
			exampleEntities.SaveChanges();
		}

		[Route("product/{id}")]
		[HttpDelete]
		public void Delete(int id)
		{
			PRODUCT product = exampleEntities.PRODUCT.Find(id);
			exampleEntities.PRODUCT.Remove(product);
			exampleEntities.SaveChanges();
		}

		[Route("test")]
		[HttpGet]
		public string Test()
		{
			return LanguageResource.test;
		}
	}
}
