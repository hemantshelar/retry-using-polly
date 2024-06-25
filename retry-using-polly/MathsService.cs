using System.Security.Cryptography;

namespace retry_using_polly
{
	public class MathsService
	{
		public async Task<int> DoOperation()
		{
			int result = 0;

			Random random = new Random();
			int r = random.Next(100);

			if (r % 9 != 0)
			{
				throw new Exception("Not successfull");
			}
			else
			{
				result = 99;
			}

			return result;
		}
	}
}
