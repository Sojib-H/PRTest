using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace PRTest.Repository.ApiGateway
{
	public class ApiCalling
	{
		public static dynamic Get<T>(string apiUrl)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(apiUrl);
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					var response = client.GetAsync(apiUrl);
					if (response.Result.IsSuccessStatusCode)
					{
						var data = response.Result.Content.ReadAsStringAsync();
						var result = JsonConvert.DeserializeObject<T>(data.Result);
						return result;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return null;
		}
		public static dynamic GetAll<T>(string apiUrl)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(apiUrl);
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					var response = client.GetAsync(apiUrl);
					if (response.Result.IsSuccessStatusCode)
					{
						var data = response.Result.Content.ReadAsStringAsync();
						var result = JsonConvert.DeserializeObject<IEnumerable<T>>(data.Result);
						return result;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return null;
		}
		public static dynamic Post<T>(string apiUrl, dynamic entity)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(apiUrl);
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					var jsonString = JsonConvert.SerializeObject(entity);
					var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

					var response = client.PostAsync(apiUrl, content);
					if (response.Result.IsSuccessStatusCode)
					{
						var data = response.Result.Content.ReadAsStringAsync();
						var result = JsonConvert.DeserializeObject<T>(data.Result);
						return result;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			var res = typeof(T).Name;
			if (res == "Boolean")
			{
				return false;
			}
			return null;
		}
		public static dynamic PostAll<T>(string apiUrl, dynamic entity)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(apiUrl);
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					var jsonString = JsonConvert.SerializeObject(entity);
					var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

					var response = client.PostAsync(apiUrl, content);
					if (response.Result.IsSuccessStatusCode)
					{
						var data = response.Result.Content.ReadAsStringAsync();
						var result = JsonConvert.DeserializeObject<IEnumerable<T>>(data.Result);
						return result;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			var res = typeof(T).Name;
			if (res == "Boolean")
			{
				return false;
			}
			return null;
		}
	}
}
